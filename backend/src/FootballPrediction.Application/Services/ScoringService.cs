using FootballPrediction.Application.Interfaces.Services;
using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Application.Services;

public class ScoringService : IScoringService
{
    public int CalculateBasePoints(int predictedHome, int predictedAway, int actualHome, int actualAway)
    {
        if (predictedHome == actualHome && predictedAway == actualAway)
            return 5;

        var predictedDiff = predictedHome - predictedAway;
        var actualDiff = actualHome - actualAway;

        if (HasSameWinner(predictedDiff, actualDiff) && Math.Abs(predictedDiff) == Math.Abs(actualDiff))
            return 4;

        if (HasSameWinner(predictedDiff, actualDiff))
            return 3;

        if (predictedHome == actualHome || predictedAway == actualAway)
            return 1;

        return 0;
    }

    public int CalculatePoints(int predictedHome, int predictedAway, int actualHome, int actualAway, TournamentStage stage)
    {
        var basePoints = CalculateBasePoints(predictedHome, predictedAway, actualHome, actualAway);
        return basePoints * stage.GetMultiplier();
    }

    private static bool HasSameWinner(int predictedDiff, int actualDiff)
    {
        if (predictedDiff == 0 && actualDiff == 0)
            return true;

        return (predictedDiff > 0 && actualDiff > 0) || (predictedDiff < 0 && actualDiff < 0);
    }
}
