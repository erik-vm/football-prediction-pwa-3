using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public ICollection<Prediction>? Predictions { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
    public ICollection<WeeklyBonus>? WeeklyBonuses { get; set; }
}
