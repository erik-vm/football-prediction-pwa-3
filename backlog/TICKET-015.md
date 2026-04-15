# TICKET-015: Predictions UI (View, Submit, Update)

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Frontend Developer
**Depends on**: TICKET-014
**User Story**: FR-PREDICT-1, FR-PREDICT-2, FR-PREDICT-3, FR-PREDICT-4, US-1, US-3, US-4
**Complexity**: L

## Description
Build the predictions feature: upcoming matches list with prediction input, prediction history with points earned, locked indicator after kickoff.

## Acceptance Criteria
- [ ] `/predictions` page shows upcoming matches sorted by kickoff time
- [ ] Each match card shows: home/away teams, kickoff time (local timezone), tournament stage, prediction status
- [ ] User can enter home/away score (0-9 integers) and submit before kickoff
- [ ] Existing prediction pre-filled for editing
- [ ] "Locked" indicator shown for matches past kickoff
- [ ] Confirmation message on successful submission/update
- [ ] `/predictions/history` (or tab) shows past predictions with: match result, predicted score, points earned, match stage
- [ ] Filter by game week (dropdown)
- [ ] Matches without predictions highlighted
- [ ] Scores limited to 0-9 (input validation)

## Test Plan
- [ ] Component test: locked prediction shows read-only state after kickoff
- [ ] Component test: submit button disabled for past-kickoff matches
- [ ] Component test: score input rejects non-integer values
- [ ] Component test: successful submit shows confirmation

## Technical Notes
- ERROR-PREVENTION #17: All IDs as `string` in TypeScript models
- ERROR-PREVENTION #18: Handle 204 NoContent from update (result may be null)
- Use Angular signals for state management
- `PredictionService` in core/services calls prediction API endpoints

## Files to Create/Modify
- [ ] `frontend/src/app/features/predictions/predictions.component.ts`
- [ ] `frontend/src/app/features/predictions/match-card/match-card.component.ts`
- [ ] `frontend/src/app/features/predictions/prediction-history/prediction-history.component.ts`
- [ ] `frontend/src/app/core/services/prediction.service.ts`
- [ ] `frontend/src/app/shared/models/prediction.models.ts`
- [ ] `frontend/src/app/shared/models/match.models.ts`

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
**Message**: `feat(ui): implement predictions view and submission UI`
**Pushed by**: Lead Developer
**Date**:
