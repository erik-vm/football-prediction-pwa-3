using FootballPrediction.Application.DTOs.Matches;
using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Application.Interfaces.Services;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Infrastructure.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matches;
    private readonly IGameWeekRepository _gameWeeks;
    private readonly IPredictionRepository _predictions;
    private readonly IScoringService _scoring;
    private readonly ILeaderboardService _leaderboard;
    private readonly IFootballDataClient _footballData;

    public MatchService(
        IMatchRepository matches,
        IGameWeekRepository gameWeeks,
        IPredictionRepository predictions,
        IScoringService scoring,
        ILeaderboardService leaderboard,
        IFootballDataClient footballData)
    {
        _matches = matches;
        _gameWeeks = gameWeeks;
        _predictions = predictions;
        _scoring = scoring;
        _leaderboard = leaderboard;
        _footballData = footballData;
    }

    public async Task<List<MatchDto>> GetUpcomingAsync(Guid? gameWeekId = null) =>
        (await _matches.GetUpcomingAsync(gameWeekId)).Select(MapToDto).ToList();

    public async Task<List<MatchDto>> GetByGameWeekIdAsync(Guid gameWeekId) =>
        (await _matches.GetByGameWeekIdAsync(gameWeekId)).Select(MapToDto).ToList();

    public async Task<MatchDto> GetByIdAsync(Guid id)
    {
        var match = await _matches.GetByIdAsync(id) ?? throw new KeyNotFoundException("Match not found.");
        return MapToDto(match);
    }

    public async Task<MatchDto> CreateAsync(CreateMatchRequest request)
    {
        if (await _gameWeeks.GetByIdAsync(request.GameWeekId) == null)
            throw new KeyNotFoundException("Game week not found.");

        var match = new Match
        {
            GameWeekId = request.GameWeekId,
            HomeTeam = request.HomeTeam,
            AwayTeam = request.AwayTeam,
            KickoffTime = DateTime.SpecifyKind(request.KickoffTime, DateTimeKind.Utc),
            Stage = request.Stage
        };

        await _matches.AddAsync(match);
        return MapToDto(await _matches.GetByIdAsync(match.Id) ?? match);
    }

    public async Task<MatchDto> UpdateAsync(Guid id, UpdateMatchRequest request)
    {
        var match = await _matches.GetByIdAsync(id) ?? throw new KeyNotFoundException("Match not found.");

        if (match.KickoffTime <= DateTime.UtcNow)
            throw new InvalidOperationException("Cannot edit match after kickoff.");

        match.HomeTeam = request.HomeTeam;
        match.AwayTeam = request.AwayTeam;
        match.KickoffTime = DateTime.SpecifyKind(request.KickoffTime, DateTimeKind.Utc);
        match.Stage = request.Stage;

        await _matches.UpdateAsync(match);
        return MapToDto(await _matches.GetByIdAsync(match.Id) ?? match);
    }

    public async Task<MatchDto> EnterResultAsync(Guid id, EnterResultRequest request)
    {
        var match = await _matches.GetByIdAsync(id) ?? throw new KeyNotFoundException("Match not found.");

        match.HomeScore = request.HomeScore;
        match.AwayScore = request.AwayScore;
        match.IsFinished = true;

        await _matches.UpdateAsync(match);

        var preds = await _predictions.GetByMatchIdAsync(id);
        foreach (var pred in preds)
        {
            pred.PointsEarned = _scoring.CalculatePoints(
                pred.HomeScore, pred.AwayScore,
                request.HomeScore, request.AwayScore,
                match.Stage);
            await _predictions.UpdateAsync(pred);
        }

        if (await _matches.AllFinishedInGameWeekAsync(match.GameWeekId))
            await _leaderboard.CalculateWeeklyBonusesAsync(match.GameWeekId);

        return MapToDto(await _matches.GetByIdAsync(match.Id) ?? match);
    }

    public async Task<int> ImportFromApiAsync(Guid gameWeekId)
    {
        var gameWeek = await _gameWeeks.GetByIdAsync(gameWeekId)
            ?? throw new KeyNotFoundException("Game week not found.");

        var externalMatches = await _footballData.GetMatchesForCompetitionAsync("CL", "2025");
        var imported = 0;

        foreach (var ext in externalMatches)
        {
            if (ext.KickoffTime < gameWeek.StartDate || ext.KickoffTime > gameWeek.EndDate)
                continue;

            if (await _matches.GetByExternalIdAsync(ext.ExternalId) != null)
                continue;

            var match = new Match
            {
                GameWeekId = gameWeekId,
                HomeTeam = ext.HomeTeam,
                AwayTeam = ext.AwayTeam,
                HomeTeamCrest = ext.HomeTeamCrest,
                AwayTeamCrest = ext.AwayTeamCrest,
                KickoffTime = ext.KickoffTime,
                Stage = ext.Stage,
                ExternalId = ext.ExternalId,
                HomeScore = ext.HomeScore,
                AwayScore = ext.AwayScore,
                IsFinished = ext.IsFinished
            };

            await _matches.AddAsync(match);
            imported++;
        }

        return imported;
    }

    private static MatchDto MapToDto(Match m) => new(
        m.Id,
        m.GameWeekId,
        m.GameWeek?.Name ?? string.Empty,
        m.GameWeek?.TournamentId ?? Guid.Empty,
        m.GameWeek?.Tournament?.Name ?? string.Empty,
        m.HomeTeam,
        m.AwayTeam,
        m.HomeTeamCrest,
        m.AwayTeamCrest,
        m.KickoffTime,
        m.Stage,
        m.Stage.GetDisplayName(),
        m.StageMultiplier,
        m.HomeScore,
        m.AwayScore,
        m.IsFinished,
        m.ExternalId
    );
}
