using FootballPrediction.Application.DTOs.Predictions;
using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Application.Interfaces.Services;
using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Infrastructure.Services;

public class PredictionService : IPredictionService
{
    private readonly IPredictionRepository _predictions;
    private readonly IMatchRepository _matches;

    public PredictionService(IPredictionRepository predictions, IMatchRepository matches)
    {
        _predictions = predictions;
        _matches = matches;
    }

    public async Task<List<PredictionDto>> GetMyPredictionsAsync(Guid userId) =>
        (await _predictions.GetByUserIdAsync(userId)).Select(MapToDto).ToList();

    public async Task<List<PredictionDto>> GetMatchPredictionsAsync(Guid matchId, Guid requestingUserId)
    {
        var match = await _matches.GetByIdAsync(matchId) ?? throw new KeyNotFoundException("Match not found.");

        if (!match.IsFinished)
        {
            var myPred = await _predictions.GetByUserAndMatchAsync(requestingUserId, matchId);
            return myPred == null ? [] : [MapToDto(myPred)];
        }

        return (await _predictions.GetByMatchIdAsync(matchId)).Select(MapToDto).ToList();
    }

    public async Task<PredictionDto> SubmitAsync(Guid userId, SubmitPredictionRequest request)
    {
        var match = await _matches.GetByIdAsync(request.MatchId) ?? throw new KeyNotFoundException("Match not found.");

        if (match.KickoffTime <= DateTime.UtcNow)
            throw new InvalidOperationException("Prediction deadline has passed.");

        var existing = await _predictions.GetByUserAndMatchAsync(userId, request.MatchId);
        if (existing != null)
            throw new InvalidOperationException("Prediction already exists. Use PUT to update.");

        var prediction = new Prediction
        {
            UserId = userId,
            MatchId = request.MatchId,
            HomeScore = request.HomeScore,
            AwayScore = request.AwayScore
        };

        await _predictions.AddAsync(prediction);
        return MapToDto(await _predictions.GetByIdAsync(prediction.Id) ?? prediction);
    }

    public async Task<PredictionDto> UpdateAsync(Guid predictionId, Guid userId, UpdatePredictionRequest request)
    {
        var prediction = await _predictions.GetByIdAsync(predictionId)
            ?? throw new KeyNotFoundException("Prediction not found.");

        if (prediction.UserId != userId)
            throw new UnauthorizedAccessException("Cannot edit another user's prediction.");

        var match = await _matches.GetByIdAsync(prediction.MatchId)
            ?? throw new KeyNotFoundException("Match not found.");

        if (match.KickoffTime <= DateTime.UtcNow)
            throw new InvalidOperationException("Prediction deadline has passed.");

        prediction.HomeScore = request.HomeScore;
        prediction.AwayScore = request.AwayScore;

        await _predictions.UpdateAsync(prediction);
        return MapToDto(await _predictions.GetByIdAsync(predictionId) ?? prediction);
    }

    private static PredictionDto MapToDto(Prediction p) => new(
        p.Id,
        p.UserId,
        p.User?.Username ?? string.Empty,
        p.MatchId,
        p.Match?.HomeTeam ?? string.Empty,
        p.Match?.AwayTeam ?? string.Empty,
        p.Match?.KickoffTime ?? DateTime.MinValue,
        p.HomeScore,
        p.AwayScore,
        p.PointsEarned,
        p.Match?.HomeScore,
        p.Match?.AwayScore,
        p.Match?.IsFinished ?? false,
        p.CreatedAt,
        p.UpdatedAt
    );
}
