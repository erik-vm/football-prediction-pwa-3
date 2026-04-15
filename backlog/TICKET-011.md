# TICKET-011: Prediction Submission API

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Backend Developer
**Depends on**: TICKET-010
**User Story**: FR-PREDICT-1, FR-PREDICT-2, FR-PREDICT-3, FR-PREDICT-4, US-1, US-3, US-4
**Complexity**: M

## Description
Implement prediction submission and management API. Users submit/update predictions before kickoff. After kickoff, predictions are locked.

## Acceptance Criteria
- [ ] `POST /api/v1/predictions` submits prediction (authenticated)
- [ ] `PUT /api/v1/predictions/{id}` updates prediction (authenticated, owner only)
- [ ] `GET /api/v1/predictions/my` returns all user's predictions with points
- [ ] `GET /api/v1/predictions/match/{matchId}` returns all predictions for a match (after kickoff only)
- [ ] Cannot submit/update prediction after match kickoff (returns 400)
- [ ] Cannot submit duplicate prediction for same match (returns 409, use update instead)
- [ ] Scores validated: 0-9 integers
- [ ] Prediction points null until match finished
- [ ] IDOR protection: users can only update their own predictions

## Test Plan
- [ ] Unit test: submit prediction before kickoff succeeds
- [ ] Unit test: submit prediction after kickoff returns 400
- [ ] Unit test: duplicate prediction returns 409
- [ ] Unit test: user cannot update another user's prediction (returns 403)
- [ ] Unit test: get-my-predictions includes point values for finished matches

## Technical Notes
- ERROR-PREVENTION #17: Match predictions endpoint only returns data after kickoff (privacy)
- `IPredictionService` in Application, `PredictionService` in Infrastructure
- Check `match.KickoffTime < DateTime.UtcNow` for deadline enforcement

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Application/DTOs/Predictions/`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Services/IPredictionService.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Services/PredictionService.cs`
- [ ] `backend/src/FootballPrediction.Api/Controllers/PredictionController.cs`
- [ ] `backend/tests/FootballPrediction.Tests/Services/PredictionServiceTests.cs`

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
**Message**: `feat(predictions): implement prediction submission and management API`
**Pushed by**: Lead Developer
**Date**:
