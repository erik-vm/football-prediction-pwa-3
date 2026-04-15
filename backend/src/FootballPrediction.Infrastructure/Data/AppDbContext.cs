using FootballPrediction.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Tournament> Tournaments => Set<Tournament>();
    public DbSet<GameWeek> GameWeeks => Set<GameWeek>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Prediction> Predictions => Set<Prediction>();
    public DbSet<WeeklyBonus> WeeklyBonuses => Set<WeeklyBonus>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is User u) { u.CreatedAt = now; }
                else if (entry.Entity is Tournament t) { t.CreatedAt = now; t.UpdatedAt = now; }
                else if (entry.Entity is GameWeek gw) { gw.CreatedAt = now; gw.UpdatedAt = now; }
                else if (entry.Entity is Match m) { m.CreatedAt = now; m.UpdatedAt = now; }
                else if (entry.Entity is Prediction p) { p.CreatedAt = now; p.UpdatedAt = now; }
                else if (entry.Entity is RefreshToken rt) { rt.CreatedAt = now; }
                else if (entry.Entity is WeeklyBonus wb) { wb.CreatedAt = now; wb.UpdatedAt = now; }
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is Tournament t) t.UpdatedAt = now;
                else if (entry.Entity is GameWeek gw) gw.UpdatedAt = now;
                else if (entry.Entity is Match m) m.UpdatedAt = now;
                else if (entry.Entity is Prediction p) p.UpdatedAt = now;
                else if (entry.Entity is WeeklyBonus wb) wb.UpdatedAt = now;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
