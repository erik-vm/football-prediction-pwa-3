# TICKET-009: Tournament & GameWeek Admin API

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Backend Developer
**Depends on**: TICKET-008
**User Story**: FR-TOURNAMENT-1, FR-GAMEWEEK-2, US-9
**Complexity**: M

## Description
Implement CRUD API endpoints for Tournament and GameWeek management. Admin-only. Enforce one active tournament at a time.

## Acceptance Criteria
- [ ] `POST /api/v1/admin/tournaments` creates tournament, returns 201
- [ ] `GET /api/v1/admin/tournaments` returns all tournaments
- [ ] `GET /api/v1/admin/tournaments/{id}` returns tournament by ID
- [ ] `PUT /api/v1/admin/tournaments/{id}` updates tournament
- [ ] `DELETE /api/v1/admin/tournaments/{id}` deletes tournament (no matches)
- [ ] Only one tournament can be `IsActive = true` at a time (enforced in service)
- [ ] `POST /api/v1/admin/gameweeks` creates game week, returns 201
- [ ] `GET /api/v1/admin/tournaments/{id}/gameweeks` lists game weeks for tournament
- [ ] `GET /api/v1/admin/gameweeks/{id}` returns game week
- [ ] `PUT /api/v1/admin/gameweeks/{id}` updates game week
- [ ] All endpoints require `[Authorize(Roles = "ADMIN")]`
- [ ] FluentValidation on create/update requests

## Test Plan
- [ ] Unit test: creating second active tournament deactivates the first
- [ ] Unit test: get tournament by ID returns 404 when not found
- [ ] Unit test: game week belongs to correct tournament

## Technical Notes
- DTOs in Application/DTOs/Tournaments/ and Application/DTOs/GameWeeks/
- Services: ITournamentService, IGameWeekService in Application layer

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Application/DTOs/Tournaments/`
- [ ] `backend/src/FootballPrediction.Application/DTOs/GameWeeks/`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Services/ITournamentService.cs`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Services/IGameWeekService.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Services/TournamentService.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Services/GameWeekService.cs`
- [ ] `backend/src/FootballPrediction.Api/Controllers/Admin/TournamentAdminController.cs`
- [ ] `backend/src/FootballPrediction.Api/Controllers/Admin/GameWeekAdminController.cs`
- [ ] `backend/tests/FootballPrediction.Tests/Services/TournamentServiceTests.cs`

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
**Message**: `feat(admin): implement tournament and game week CRUD API`
**Pushed by**: Lead Developer
**Date**:
