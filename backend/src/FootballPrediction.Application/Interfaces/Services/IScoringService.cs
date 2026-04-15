using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Application.Interfaces.Services;

public interface IScoringService
{
    int CalculateBasePoints(int predictedHome, int predictedAway, int actualHome, int actualAway);
    int CalculatePoints(int predictedHome, int predictedAway, int actualHome, int actualAway, TournamentStage stage);
}
