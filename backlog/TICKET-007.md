# TICKET-007: Scoring Service

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Backend Developer
**Depends on**: TICKET-003
**User Story**: FR-SCORE-1
**Complexity**: S

## Description
Implement `IScoringService` / `ScoringService` with the exact scoring logic from GAME-RULES.md §8.2. This is a pure domain service with no EF/DB dependency.

## Acceptance Criteria
- [ ] `CalculatePoints(predictedHome, predictedAway, actualHome, actualAway)` returns correct base points (0-5)
- [ ] `CalculatePoints` with stage multiplier overload returns `base × (int)stage`
- [ ] All 11 test cases from GAME-RULES.md §11 pass
- [ ] Null inputs return 0

## Test Plan
- [ ] Unit test: exact score → 5 pts (Predicted 2:1, Actual 2:1)
- [ ] Unit test: winner + diff → 4 pts (Predicted 1:0, Actual 2:1)
- [ ] Unit test: winner only → 3 pts (Predicted 3:0, Actual 2:1)
- [ ] Unit test: one score correct → 1 pt (Predicted 0:1, Actual 0:2)
- [ ] Unit test: no match → 0 pts (Predicted 1:0, Actual 0:1)
- [ ] Unit test: draw prediction (1:1 vs 2:2) → 3 pts
- [ ] Unit test: Final exact score → 25 pts (5 × 5)
- [ ] Unit test: null inputs → 0 pts

## Technical Notes
- Implementation must match reference Java/C# code in GAME-RULES.md §8
- `HasSameWinner`: both draws (diff==0) OR both positive OR both negative diffs
- Live in `Application/Services/ScoringService.cs`

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Services/IScoringService.cs`
- [ ] `backend/src/FootballPrediction.Application/Services/ScoringService.cs`
- [ ] `backend/tests/FootballPrediction.Tests/Services/ScoringServiceTests.cs`

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
**Message**: `feat(scoring): implement scoring service with full test coverage`
**Pushed by**: Lead Developer
**Date**:
