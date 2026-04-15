# TICKET-019: User Statistics API & UI

**Status**: BACKLOG
**Priority**: P1
**Assigned to**: Backend Developer + Frontend Developer
**Depends on**: TICKET-012, TICKET-016
**User Story**: FR-LEADER-3, US-8
**Complexity**: M

## Description
Implement user statistics endpoint and profile page. Show overall rank, total points, accuracy breakdown (exact/winner+diff/winner/one-score/miss counts), weekly performance.

## Acceptance Criteria
- [ ] `GET /api/v1/leaderboard/user/{userId}` returns full user stats
- [ ] Stats include: rank, totalPoints, predictionsCount, exactScores, winnerPlusDiff, correctWinners, oneScoreCorrect, misses
- [ ] `/profile` page shows current user's statistics
- [ ] Accuracy breakdown shows count and percentage for each category
- [ ] Overall rank shown prominently
- [ ] Weekly points history list (game week → points earned that week)

## Test Plan
- [ ] Unit test: accuracy breakdown sums to total predictions
- [ ] Unit test: user with no predictions returns all zeros

## Technical Notes
- Extend `LeaderboardService.GetUserStatsAsync`
- Frontend: `features/profile/profile.component.ts`

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Application/DTOs/Leaderboard/UserStatsDto.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Services/LeaderboardService.cs` (update)
- [ ] `frontend/src/app/features/profile/profile.component.ts`

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
**Message**: `feat(stats): implement user statistics API and profile page`
**Pushed by**: Lead Developer
**Date**:
