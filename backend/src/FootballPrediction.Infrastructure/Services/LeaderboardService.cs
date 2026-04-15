using FootballPrediction.Application.DTOs.Leaderboard;
using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Application.Interfaces.Services;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Infrastructure.Services;

public class LeaderboardService : ILeaderboardService
{
    private readonly IUserRepository _users;
    private readonly IPredictionRepository _predictions;
    private readonly IWeeklyBonusRepository _bonuses;
    private readonly IGameWeekRepository _gameWeeks;

    public LeaderboardService(
        IUserRepository users,
        IPredictionRepository predictions,
        IWeeklyBonusRepository bonuses,
        IGameWeekRepository gameWeeks)
    {
        _users = users;
        _predictions = predictions;
        _bonuses = bonuses;
        _gameWeeks = gameWeeks;
    }

    public async Task<List<LeaderboardEntryDto>> GetOverallAsync()
    {
        var users = await _users.GetAllAsync();
        var entries = new List<LeaderboardEntryDto>();

        foreach (var user in users)
        {
            var preds = await _predictions.GetByUserIdAsync(user.Id);
            var bonusList = await _bonuses.GetByUserIdAsync(user.Id);

            var finishedPreds = preds.Where(p => p.Match?.IsFinished == true).ToList();
            var totalPoints = finishedPreds.Sum(p => p.PointsEarned ?? 0);
            var totalBonus = bonusList.Sum(b => b.BonusPoints);

            entries.Add(new LeaderboardEntryDto(
                0,
                user.Id,
                user.Username,
                totalPoints,
                totalBonus,
                preds.Count,
                CountCategory(finishedPreds, 5),
                CountCategory(finishedPreds, 4),
                CountCategory(finishedPreds, 3),
                CountCategory(finishedPreds, 1),
                finishedPreds.Count(p => p.PointsEarned == 0)
            ));
        }

        var sorted = entries
            .OrderByDescending(e => e.TotalPoints + (int)e.TotalBonusPoints)
            .ThenByDescending(e => e.ExactScores)
            .ThenByDescending(e => e.CorrectWinners)
            .ThenBy(e => e.Username)
            .ToList();

        return sorted.Select((e, i) => e with { Rank = i + 1 }).ToList();
    }

    public async Task<List<WeeklyLeaderboardEntryDto>> GetWeeklyAsync(Guid gameWeekId)
    {
        var preds = await _predictions.GetByGameWeekIdAsync(gameWeekId);
        var bonusList = await _bonuses.GetByGameWeekIdAsync(gameWeekId);

        var grouped = preds
            .Where(p => p.Match?.IsFinished == true)
            .GroupBy(p => p.UserId)
            .Select(g => new
            {
                UserId = g.Key,
                Username = g.First().User?.Username ?? string.Empty,
                Points = g.Sum(p => p.PointsEarned ?? 0)
            })
            .OrderByDescending(g => g.Points)
            .ToList();

        var result = new List<WeeklyLeaderboardEntryDto>();
        var rank = 1;

        for (int i = 0; i < grouped.Count; i++)
        {
            if (i > 0 && grouped[i].Points < grouped[i - 1].Points)
                rank = i + 1;

            var bonus = bonusList.FirstOrDefault(b => b.UserId == grouped[i].UserId);
            result.Add(new WeeklyLeaderboardEntryDto(
                rank,
                grouped[i].UserId,
                grouped[i].Username,
                grouped[i].Points,
                bonus?.BonusPoints ?? 0
            ));
        }

        return result;
    }

    public async Task<UserStatsDto> GetUserStatsAsync(Guid userId)
    {
        var user = await _users.GetByIdAsync(userId) ?? throw new KeyNotFoundException("User not found.");
        var preds = await _predictions.GetByUserIdAsync(userId);
        var bonusList = await _bonuses.GetByUserIdAsync(userId);
        var overall = await GetOverallAsync();

        var rank = overall.FirstOrDefault(e => e.UserId == userId)?.Rank ?? 0;
        var finishedPreds = preds.Where(p => p.Match?.IsFinished == true).ToList();
        var totalPoints = finishedPreds.Sum(p => p.PointsEarned ?? 0);
        var totalBonus = bonusList.Sum(b => b.BonusPoints);

        var weeklyHistory = preds
            .Where(p => p.Match?.GameWeek != null && p.Match.IsFinished)
            .GroupBy(p => p.Match!.GameWeekId)
            .Select(g =>
            {
                var weekBonus = bonusList.FirstOrDefault(b => b.GameWeekId == g.Key);
                return new WeeklyPointsDto(
                    g.Key,
                    g.First().Match?.GameWeek?.Name ?? string.Empty,
                    g.Sum(p => p.PointsEarned ?? 0),
                    weekBonus?.BonusPoints ?? 0
                );
            })
            .OrderByDescending(w => w.Points)
            .ToList();

        return new UserStatsDto(
            userId,
            user.Username,
            rank,
            totalPoints,
            totalBonus,
            preds.Count,
            CountCategory(finishedPreds, 5),
            CountCategory(finishedPreds, 4),
            CountCategory(finishedPreds, 3),
            CountCategory(finishedPreds, 1),
            finishedPreds.Count(p => p.PointsEarned == 0),
            weeklyHistory
        );
    }

    public async Task CalculateWeeklyBonusesAsync(Guid gameWeekId)
    {
        await _bonuses.DeleteByGameWeekIdAsync(gameWeekId);

        var preds = await _predictions.GetByGameWeekIdAsync(gameWeekId);
        var grouped = preds
            .Where(p => p.Match?.IsFinished == true)
            .GroupBy(p => p.UserId)
            .Select(g => new { UserId = g.Key, Points = g.Sum(p => p.PointsEarned ?? 0) })
            .OrderByDescending(g => g.Points)
            .ToList();

        if (!grouped.Any()) return;

        var bonusPool = new[] { (1, 5m), (2, 3m), (3, 1m) };
        var newBonuses = new List<WeeklyBonus>();

        foreach (var (rank, bonus) in bonusPool)
        {
            if (grouped.Count < rank) break;

            var rankScore = grouped[rank - 1].Points;
            var tied = grouped.Where(g => g.Points == rankScore).ToList();
            var splitBonus = bonus / tied.Count;

            foreach (var user in tied)
            {
                if (newBonuses.Any(b => b.UserId == user.UserId)) continue;
                newBonuses.Add(new WeeklyBonus
                {
                    UserId = user.UserId,
                    GameWeekId = gameWeekId,
                    BonusPoints = splitBonus,
                    Rank = rank
                });
            }
        }

        if (newBonuses.Any())
            await _bonuses.AddRangeAsync(newBonuses);
    }

    private static int CountCategory(List<Prediction> preds, int points) =>
        preds.Count(p => p.PointsEarned.HasValue &&
            (p.Match != null ? GetBasePoints(p.PointsEarned.Value, p.Match.Stage.GetMultiplier()) : p.PointsEarned.Value) == points);

    private static int GetBasePoints(int totalPoints, int multiplier) =>
        multiplier > 0 ? totalPoints / multiplier : 0;
}
