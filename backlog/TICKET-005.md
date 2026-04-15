# TICKET-005: Initial EF Core Migration

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: DB Specialist
**Depends on**: TICKET-004, TICKET-002
**User Story**: none (infrastructure)
**Complexity**: S

## Description
Generate the initial EF Core migration and apply it to the local Docker PostgreSQL database using the SQL script method.

## Acceptance Criteria
- [ ] Migration `InitialCreate` generated under `Infrastructure/Migrations/`
- [ ] SQL script `migration.sql` generated and applied
- [ ] All tables created: Users, Tournaments, GameWeeks, Matches, Predictions
- [ ] All unique and regular indexes created
- [ ] Database connection verified with `dotnet ef dbcontext info`

## Test Plan
- [ ] All 5 tables visible in database after applying migration
- [ ] `dotnet build` still passes

## Technical Notes
- ERROR-PREVENTION #2: Install exact dotnet-ef version: `dotnet tool install --global dotnet-ef --version 9.0.0`
- ERROR-PREVENTION #3: Use SQL script: `dotnet ef migrations script -o migration.sql` then apply via psql
- Run migration from `backend/` directory pointing to Infrastructure project
- Command: `dotnet ef migrations add InitialCreate --project src/FootballPrediction.Infrastructure --startup-project src/FootballPrediction.Api`

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Infrastructure/Migrations/` (auto-generated)

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
**Message**: `feat(db): add initial EF Core migration`
**Pushed by**: Lead Developer
**Date**:
