using FluentAssertions;
using FootballPrediction.Application.Services;
using FootballPrediction.Domain.Enums;
using Xunit;

namespace FootballPrediction.Tests.Services;

public class ScoringServiceTests
{
    private readonly ScoringService _sut = new();

    [Fact]
    public void ExactScore_Returns5Points()
    {
        _sut.CalculateBasePoints(2, 1, 2, 1).Should().Be(5);
    }

    [Fact]
    public void ExactDraw_Returns5Points()
    {
        _sut.CalculateBasePoints(0, 0, 0, 0).Should().Be(5);
    }

    [Fact]
    public void CorrectWinnerAndDiff_Returns4Points()
    {
        _sut.CalculateBasePoints(1, 0, 2, 1).Should().Be(4);
    }

    [Fact]
    public void CorrectWinnerAndDiff_Away_Returns4Points()
    {
        _sut.CalculateBasePoints(0, 1, 1, 2).Should().Be(4);
    }

    [Fact]
    public void BothDrawsMatch_Returns4Points()
    {
        _sut.CalculateBasePoints(2, 2, 1, 1).Should().Be(4);
    }

    [Fact]
    public void BothDrawsDifferentScores_Returns4Points()
    {
        _sut.CalculateBasePoints(1, 1, 2, 2).Should().Be(4);
    }

    [Fact]
    public void CorrectWinnerOnly_Returns3Points()
    {
        _sut.CalculateBasePoints(3, 0, 2, 1).Should().Be(3);
    }

    [Fact]
    public void CorrectWinnerSameHome_Returns3Points()
    {
        // Home score matches, but still classified as correct winner (away team wins both)
        _sut.CalculateBasePoints(0, 1, 0, 2).Should().Be(3);
    }

    [Fact]
    public void OneScoreCorrect_DifferentWinner_Returns1Point()
    {
        // Predicted home wins 2:1, actual away wins 0:1 - away score 1 matches, different winner
        _sut.CalculateBasePoints(2, 1, 0, 1).Should().Be(1);
    }

    [Fact]
    public void OneScoreCorrect_HomeScore_DifferentWinner_Returns1Point()
    {
        // Predicted home wins 1:0, actual away wins 1:3 - home score 1 matches, different winner
        _sut.CalculateBasePoints(1, 0, 1, 3).Should().Be(1);
    }

    [Fact]
    public void NoMatch_Returns0Points()
    {
        _sut.CalculateBasePoints(1, 0, 0, 1).Should().Be(0);
    }

    [Fact]
    public void FinalStage_ExactScore_Returns25Points()
    {
        _sut.CalculatePoints(2, 1, 2, 1, TournamentStage.Final).Should().Be(25);
    }

    [Fact]
    public void RoundOf16_CorrectWinner_Returns6Points()
    {
        _sut.CalculatePoints(3, 0, 2, 1, TournamentStage.RoundOf16).Should().Be(6);
    }

    [Fact]
    public void GroupStage_ExactScore_Returns5Points()
    {
        _sut.CalculatePoints(2, 1, 2, 1, TournamentStage.GroupStage).Should().Be(5);
    }

    [Fact]
    public void QuarterFinals_NoMatch_Returns0Points()
    {
        _sut.CalculatePoints(1, 0, 0, 3, TournamentStage.QuarterFinals).Should().Be(0);
    }
}
