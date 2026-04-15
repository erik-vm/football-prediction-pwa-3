# TICKET-016: Leaderboard UI (Overall + Weekly)

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Frontend Developer
**Depends on**: TICKET-015
**User Story**: FR-LEADER-1, FR-LEADER-2, US-2
**Complexity**: M

## Description
Build leaderboard pages: overall leaderboard with all users ranked, weekly leaderboard with game week selector. Show rank, username, points, exact scores, correct winners.

## Acceptance Criteria
- [ ] `/leaderboard` page shows overall leaderboard table
- [ ] Table columns: Rank, Username, Total Points, Predictions Made, Exact Scores, Correct Winners
- [ ] Current user's row highlighted
- [ ] Weekly leaderboard tab/view with game week dropdown selector
- [ ] Weekly table: Rank, Username, Week Points, Weekly Bonus
- [ ] Tie positions shown correctly (e.g. two users at rank 1, next at rank 3)
- [ ] Loading state while fetching data
- [ ] Empty state when no data

## Test Plan
- [ ] Component test: current user row has highlight class
- [ ] Component test: tie positions render correct rank numbers
- [ ] Component test: week dropdown triggers leaderboard reload

## Technical Notes
- `LeaderboardService` in core/services
- Use Angular signals for reactive state
- ERROR-PREVENTION #17: all IDs as `string`

## Files to Create/Modify
- [ ] `frontend/src/app/features/leaderboard/leaderboard.component.ts`
- [ ] `frontend/src/app/features/leaderboard/overall-leaderboard/overall-leaderboard.component.ts`
- [ ] `frontend/src/app/features/leaderboard/weekly-leaderboard/weekly-leaderboard.component.ts`
- [ ] `frontend/src/app/core/services/leaderboard.service.ts`
- [ ] `frontend/src/app/shared/models/leaderboard.models.ts`

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
**Message**: `feat(ui): implement overall and weekly leaderboard pages`
**Pushed by**: Lead Developer
**Date**:
