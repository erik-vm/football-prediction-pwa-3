# TICKET-010: Match Admin API (Create, Update, Result Entry)

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Backend Developer
**Depends on**: TICKET-009
**User Story**: FR-MATCH-3, FR-MATCH-5, FR-RESULT-1, FR-RESULT-2, US-10, US-11, US-12, US-13
**Complexity**: M

## Description
Implement Match management API: admin creates/updates matches, enters results. On result entry, system automatically calculates points for all predictions using ScoringService.

## Acceptance Criteria
- [ ] `POST /api/v1/admin/matches` creates match (admin only), returns 201
- [ ] `PUT /api/v1/admin/matches/{id}` updates match (only before kickoff)
- [ ] `PUT /api/v1/admin/matches/{id}/result` enters/updates final scores
- [ ] On result entry: match marked IsFinished=true, all predictions recalculated
- [ ] On result update: all predictions recalculated from scratch
- [ ] `GET /api/v1/matches/upcoming` returns matches with kickoff > now (authenticated)
- [ ] `GET /api/v1/matches/{id}` returns match details
- [ ] `GET /api/v1/matches/gameweek/{id}` returns all matches for a game week
- [ ] Cannot update match after kickoff (returns 400)
- [ ] FluentValidation on create/update requests

## Test Plan
- [ ] Unit test: entering result triggers point calculation for all predictions
- [ ] Unit test: updating result recalculates all prediction points
- [ ] Unit test: update match after kickoff returns 400
- [ ] Unit test: upcoming matches sorted by kickoff time ascending

## Technical Notes
- `IMatchService` in Application, `MatchService` in Infrastructure
- When result entered: load all predictions for match → call `IScoringService.CalculatePoints` → save
- Stage multiplier: `match.Stage.GetMultiplier()`
- ERROR-PREVENTION #15: Navigation property cycles already handled in TICKET-001

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Application/DTOs/Matches/`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Services/IMatchService.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Services/MatchService.cs`
- [ ] `backend/src/FootballPrediction.Api/Controllers/Admin/MatchAdminController.cs`
- [ ] `backend/src/FootballPrediction.Api/Controllers/MatchController.cs`
- [ ] `backend/tests/FootballPrediction.Tests/Services/MatchServiceTests.cs`

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
**Message**: `feat(admin): implement match management and result entry API`
**Pushed by**: Lead Developer
**Date**:
