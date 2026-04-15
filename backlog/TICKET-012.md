# TICKET-012: Leaderboard API (Overall + Weekly + Weekly Bonuses)

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Backend Developer
**Depends on**: TICKET-011
**User Story**: FR-LEADER-1, FR-LEADER-2, FR-SCORE-2, US-2
**Complexity**: M

## Description
Implement leaderboard endpoints and weekly bonus calculation. Overall leaderboard ranks users by total points + bonuses. Weekly leaderboard shows points per game week. Bonuses awarded when all matches in a week are finished.

## Acceptance Criteria
- [ ] `GET /api/v1/leaderboard/overall` returns all users ranked by total points
- [ ] `GET /api/v1/leaderboard/weekly/{gameWeekId}` returns weekly ranking
- [ ] `GET /api/v1/leaderboard/user/{userId}` returns user statistics
- [ ] Overall ranking tie-break: most exact scores → most correct winners → username alphabetically
- [ ] Weekly bonuses: 1st +5, 2nd +3, 3rd +1 (ties split equally per GAME-RULES.md §4.2)
- [ ] Bonuses calculated automatically when all matches in a game week are finished
- [ ] Weekly leaderboard shows points BEFORE bonuses (bonus column separate)
- [ ] User statistics: overall rank, total points, predictions made, accuracy breakdown by point category

## Test Plan
- [ ] Unit test: overall leaderboard correctly ranks users
- [ ] Unit test: tie-break by exact scores works
- [ ] Unit test: weekly bonus: two users tied for 1st each get 2.5 points
- [ ] Unit test: weekly bonus not awarded until all matches finished
- [ ] Unit test: user statistics accuracy breakdown sums correctly

## Technical Notes
- Bonus calculation triggered by `MatchService.EnterResult` when all week's matches are done
- `ILeaderboardService` in Application, `LeaderboardService` in Infrastructure
- Store WeeklyBonus in DB: `WeeklyBonus` entity (UserId, GameWeekId, BonusPoints)
- Weekly bonus: load all predictions for game week → group by user → rank → award

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Domain/Entities/WeeklyBonus.cs`
- [ ] `backend/src/FootballPrediction.Application/DTOs/Leaderboard/`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Services/ILeaderboardService.cs`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Repositories/IWeeklyBonusRepository.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Services/LeaderboardService.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Repositories/WeeklyBonusRepository.cs`
- [ ] `backend/src/FootballPrediction.Api/Controllers/LeaderboardController.cs`
- [ ] `backend/tests/FootballPrediction.Tests/Services/LeaderboardServiceTests.cs`

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
**Message**: `feat(leaderboard): implement overall and weekly leaderboard with bonuses`
**Pushed by**: Lead Developer
**Date**:
