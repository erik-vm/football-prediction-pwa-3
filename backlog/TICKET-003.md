# TICKET-003: Domain Entities

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Backend Developer
**Depends on**: TICKET-001
**User Story**: none (infrastructure)
**Complexity**: S

## Description
Create all domain entities in `FootballPrediction.Domain`. Entities: User, Tournament, GameWeek, Match, Prediction. Enums: UserRole, TournamentStage (with GetMultiplier extension). No dependencies on other layers.

## Acceptance Criteria
- [ ] `User` entity with all fields from REQUIREMENTS.md §6.1
- [ ] `Tournament` entity with all fields
- [ ] `GameWeek` entity with all fields
- [ ] `Match` entity with all fields including nullable scores
- [ ] `Prediction` entity with all fields
- [ ] `UserRole` enum: USER, ADMIN
- [ ] `TournamentStage` enum: GROUP_STAGE=1 … FINAL=5 with `GetMultiplier()` extension
- [ ] Navigation properties are nullable (`?`)
- [ ] Domain project has zero NuGet dependencies

## Test Plan
- [ ] `dotnet build` succeeds

## Technical Notes
- ERROR-PREVENTION #16: All navigation properties must be nullable (`?`)
- `TournamentStage.GetMultiplier()` returns `(int)stage` (1-5)
- `Match.StageMultiplier` computed property using extension

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Domain/Entities/User.cs`
- [ ] `backend/src/FootballPrediction.Domain/Entities/Tournament.cs`
- [ ] `backend/src/FootballPrediction.Domain/Entities/GameWeek.cs`
- [ ] `backend/src/FootballPrediction.Domain/Entities/Match.cs`
- [ ] `backend/src/FootballPrediction.Domain/Entities/Prediction.cs`
- [ ] `backend/src/FootballPrediction.Domain/Enums/UserRole.cs`
- [ ] `backend/src/FootballPrediction.Domain/Enums/TournamentStage.cs`

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
**Message**: `feat(db): add domain entities and enums`
**Pushed by**: Lead Developer
**Date**:
