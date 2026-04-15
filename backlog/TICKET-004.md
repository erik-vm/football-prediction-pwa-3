# TICKET-004: EF Core DbContext & Entity Configuration

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: DB Specialist
**Depends on**: TICKET-003
**User Story**: none (infrastructure)
**Complexity**: M

## Description
Create `AppDbContext` in Infrastructure with entity configurations (Fluent API), unique indexes, and auto-timestamping in `SaveChangesAsync`. Add PostgreSQL connection string parsing to handle Render's `postgres://` URI format.

## Acceptance Criteria
- [ ] `AppDbContext` with DbSets for all 5 entities
- [ ] `OnModelCreating` configures all unique indexes from REQUIREMENTS.md §6.2
- [ ] Unique indexes: Users(Email), Users(Username), Predictions(UserId+MatchId)
- [ ] Regular indexes: Matches(GameWeekId), Matches(KickoffTime), Matches(IsFinished), Predictions(UserId), Predictions(MatchId)
- [ ] `SaveChangesAsync` auto-sets `CreatedAt` / `UpdatedAt`
- [ ] PostgreSQL URI parser handles `postgres://` and `postgresql://` formats
- [ ] `POSTGRES_URL` env var used in production; fallback to `ConnectionStrings:Default` for dev

## Test Plan
- [ ] Unit test: auto-timestamping sets CreatedAt on new entity
- [ ] Unit test: auto-timestamping updates UpdatedAt on modified entity

## Technical Notes
- ERROR-PREVENTION #7: Add URI parser in Program.cs for Render `postgres://` URLs
- ERROR-PREVENTION #3: Use SQL script migration method (not `database update` directly)
- `Npgsql.EntityFrameworkCore.PostgreSQL` version 9.0.2

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Infrastructure/Data/AppDbContext.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Data/Configurations/UserConfiguration.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Data/Configurations/MatchConfiguration.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Data/Configurations/PredictionConfiguration.cs`
- [ ] `backend/src/FootballPrediction.Api/Program.cs` (connection string registration + URI parser)

---

## Implementation Log

---

## Review Notes
**Reviewer**: Code Reviewer
**Result**:
**Findings**:

---

## QA Notes
**Tester**: QA Engineer
**Result**:
**Evidence**:

---

## Git Info
**Commit**:
**Message**: `feat(db): add EF Core DbContext with entity configurations and indexes`
**Pushed by**: Lead Developer
**Date**:
