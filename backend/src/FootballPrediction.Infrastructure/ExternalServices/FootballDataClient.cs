using System.Text.Json;
using System.Text.Json.Serialization;
using FootballPrediction.Application.DTOs.Matches;
using FootballPrediction.Application.Interfaces.Services;
using FootballPrediction.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace FootballPrediction.Infrastructure.ExternalServices;

public class FootballDataClient : IFootballDataClient
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public FootballDataClient(HttpClient http, IConfiguration config)
    {
        _http = http;
        _apiKey = config["FootballData:ApiKey"] ?? throw new InvalidOperationException("FootballData:ApiKey not configured.");
    }

    public async Task<List<ExternalMatchDto>> GetMatchesForCompetitionAsync(string competitionCode, string season)
    {
        _http.DefaultRequestHeaders.Remove("X-Auth-Token");
        _http.DefaultRequestHeaders.Add("X-Auth-Token", _apiKey);

        var response = await _http.GetAsync($"https://api.football-data.org/v4/competitions/{competitionCode}/matches?season={season}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var root = JsonSerializer.Deserialize<FootballDataResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return root?.Matches?.Select(MapToDto).ToList() ?? [];
    }

    private static ExternalMatchDto MapToDto(FootballDataMatch m)
    {
        var stage = MapStage(m.Stage);
        int? homeScore = null, awayScore = null;
        bool isFinished = false;

        if (m.Score?.FullTime != null)
        {
            homeScore = m.Score.FullTime.Home;
            awayScore = m.Score.FullTime.Away;
            isFinished = m.Status == "FINISHED";
        }

        return new ExternalMatchDto(
            m.Id.ToString(),
            m.HomeTeam?.ShortName ?? m.HomeTeam?.Name ?? "Unknown",
            m.AwayTeam?.ShortName ?? m.AwayTeam?.Name ?? "Unknown",
            m.HomeTeam?.Crest,
            m.AwayTeam?.Crest,
            m.UtcDate,
            stage,
            homeScore,
            awayScore,
            isFinished
        );
    }

    private static TournamentStage MapStage(string? stage) => stage switch
    {
        "GROUP_STAGE" => TournamentStage.GroupStage,
        "LEAGUE_PHASE" => TournamentStage.GroupStage,
        "ROUND_OF_16" => TournamentStage.RoundOf16,
        "LAST_16" => TournamentStage.RoundOf16,
        "QUARTER_FINALS" => TournamentStage.QuarterFinals,
        "SEMI_FINALS" => TournamentStage.SemiFinals,
        "FINAL" => TournamentStage.Final,
        _ => TournamentStage.GroupStage
    };
}

internal class FootballDataResponse
{
    public List<FootballDataMatch>? Matches { get; set; }
}

internal class FootballDataMatch
{
    public int Id { get; set; }
    public DateTime UtcDate { get; set; }
    public string? Status { get; set; }
    public string? Stage { get; set; }
    public FootballDataTeam? HomeTeam { get; set; }
    public FootballDataTeam? AwayTeam { get; set; }
    public FootballDataScore? Score { get; set; }
}

internal class FootballDataTeam
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public string? Crest { get; set; }
}

internal class FootballDataScore
{
    public FootballDataFullTime? FullTime { get; set; }
}

internal class FootballDataFullTime
{
    public int? Home { get; set; }
    public int? Away { get; set; }
}
