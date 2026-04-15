using FootballPrediction.Application.DTOs.Predictions;

namespace FootballPrediction.Application.Interfaces.Services;

public interface IPredictionService
{
    Task<List<PredictionDto>> GetMyPredictionsAsync(Guid userId);
    Task<List<PredictionDto>> GetMatchPredictionsAsync(Guid matchId, Guid requestingUserId);
    Task<PredictionDto> SubmitAsync(Guid userId, SubmitPredictionRequest request);
    Task<PredictionDto> UpdateAsync(Guid predictionId, Guid userId, UpdatePredictionRequest request);
}
