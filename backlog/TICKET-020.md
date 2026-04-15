# TICKET-020: Match Import from football-data.org API

**Status**: BACKLOG
**Priority**: P1
**Assigned to**: Backend Developer
**Depends on**: TICKET-009
**User Story**: FR-MATCH-4, US-10
**Complexity**: M

## Description
Admin can import upcoming matches from football-data.org API into a selected game week. Map competition phases to TournamentStage enum. Rate limit: 10 requests/minute.

## Acceptance Criteria
- [ ] `POST /api/v1/admin/gameweeks/{id}/import-matches` triggers import
- [ ] Fetches matches from football-data.org for the configured competition
- [ ] Maps competition phase to TournamentStage (GROUP_STAGE, ROUND_OF_16, etc.)
- [ ] Creates Match records in DB, assigned to specified game week
- [ ] Skips already-imported matches (idempotent by external match ID)
- [ ] API key configured via `FootballData:ApiKey` env var
- [ ] Rate limiting respected (max 10 req/min)
- [ ] Returns count of imported/skipped matches

## Test Plan
- [ ] Unit test: phase mapping covers all known competition phases
- [ ] Unit test: duplicate import skips existing matches
- [ ] Integration test: mock API response → correct matches created

## Technical Notes
- `IFootballDataClient` interface in Application, `FootballDataClient` in Infrastructure
- Store external match ID to detect duplicates
- Add `ExternalId` nullable string field to Match entity (requires new migration)
- Use `HttpClient` with typed client pattern

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Domain/Entities/Match.cs` (add ExternalId)
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Services/IFootballDataClient.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/ExternalServices/FootballDataClient.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Services/MatchImportService.cs`
- [ ] `backend/src/FootballPrediction.Api/Controllers/Admin/MatchAdminController.cs` (add import endpoint)
- [ ] `backend/src/FootballPrediction.Infrastructure/Migrations/` (ExternalId migration)

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
**Message**: `feat(admin): implement match import from football-data.org API`
**Pushed by**: Lead Developer
**Date**:
