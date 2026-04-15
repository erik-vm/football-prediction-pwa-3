# TICKET-017: Admin UI (Tournament, GameWeek, Match Management)

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Frontend Developer
**Depends on**: TICKET-016
**User Story**: FR-TOURNAMENT-1, FR-GAMEWEEK-2, FR-MATCH-3, FR-MATCH-5, FR-RESULT-1, FR-RESULT-2, US-9, US-11, US-12, US-13
**Complexity**: L

## Description
Build admin panel accessible only to users with ADMIN role. Pages: tournament list/create/edit, game week list/create, match list/create/edit, result entry.

## Acceptance Criteria
- [ ] `/admin` route protected by AdminGuard (redirects non-admins to `/predictions`)
- [ ] Tournament list page with create/edit forms
- [ ] Game week list page per tournament with create form
- [ ] Match list page per game week with create/edit forms
- [ ] Match stage selector (GROUP_STAGE → FINAL)
- [ ] Result entry form: home score + away score for finished matches
- [ ] Confirmation on result entry showing calculated points preview (optional)
- [ ] Matches past kickoff show read-only in edit view
- [ ] Error messages from API displayed inline

## Test Plan
- [ ] Component test: admin guard redirects USER role to /predictions
- [ ] Component test: result entry form validates score range 0-99

## Technical Notes
- ERROR-PREVENTION #17: all IDs as `string`
- `AdminService` in core/services calls admin API endpoints
- Feature folder: `features/admin/`

## Files to Create/Modify
- [ ] `frontend/src/app/features/admin/admin.routes.ts`
- [ ] `frontend/src/app/features/admin/tournaments/`
- [ ] `frontend/src/app/features/admin/game-weeks/`
- [ ] `frontend/src/app/features/admin/matches/`
- [ ] `frontend/src/app/core/services/admin.service.ts`
- [ ] `frontend/src/app/shared/models/admin.models.ts`

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
**Message**: `feat(ui): implement admin panel for tournament and match management`
**Pushed by**: Lead Developer
**Date**:
