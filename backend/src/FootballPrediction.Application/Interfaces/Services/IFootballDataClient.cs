using FootballPrediction.Application.DTOs.Matches;

namespace FootballPrediction.Application.Interfaces.Services;

public interface IFootballDataClient
{
    Task<List<ExternalMatchDto>> GetMatchesForCompetitionAsync(string competitionCode, string season);
}
