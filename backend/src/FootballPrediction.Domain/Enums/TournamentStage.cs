namespace FootballPrediction.Domain.Enums;

public enum TournamentStage
{
    GroupStage = 1,
    RoundOf16 = 2,
    QuarterFinals = 3,
    SemiFinals = 4,
    Final = 5
}

public static class TournamentStageExtensions
{
    public static int GetMultiplier(this TournamentStage stage) => (int)stage;

    public static string GetDisplayName(this TournamentStage stage) => stage switch
    {
        TournamentStage.GroupStage => "Group Stage",
        TournamentStage.RoundOf16 => "Round of 16",
        TournamentStage.QuarterFinals => "Quarter-Finals",
        TournamentStage.SemiFinals => "Semi-Finals",
        TournamentStage.Final => "Final",
        _ => stage.ToString()
    };
}
