# TICKET-014: Auth UI (Register + Login Pages)

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Frontend Developer
**Depends on**: TICKET-013
**User Story**: FR-AUTH-1, FR-AUTH-2, US-1
**Complexity**: M

## Description
Build register and login pages. On success, store JWT + refresh token in localStorage, redirect to predictions page. Display validation errors from API inline.

## Acceptance Criteria
- [ ] `/register` page with username, email, password fields
- [ ] `/login` page with email, password fields
- [ ] Client-side validation mirrors server rules (username 3-20, email format, password 8-64)
- [ ] JWT + refresh token stored in localStorage on login/register success
- [ ] Redirect to `/predictions` after successful auth
- [ ] Display API error messages (409 duplicate, 401 invalid credentials) inline
- [ ] "Already have an account?" / "Don't have an account?" navigation links
- [ ] Auth pages redirect to `/predictions` if already logged in
- [ ] Mobile-friendly form layout

## Test Plan
- [ ] Component test: register form shows validation errors for invalid input
- [ ] Component test: login form calls auth service on submit
- [ ] Component test: successful login navigates to /predictions

## Technical Notes
- ERROR-PREVENTION #17: Entity IDs must be `string` in all TypeScript models
- Use Angular signals for form state
- `AuthService` in core/services handles register/login/logout/token storage
- Token refresh logic lives in interceptor (TICKET-013)

## Files to Create/Modify
- [ ] `frontend/src/app/features/auth/login/login.component.ts`
- [ ] `frontend/src/app/features/auth/register/register.component.ts`
- [ ] `frontend/src/app/core/services/auth.service.ts` (update)
- [ ] `frontend/src/app/shared/models/auth.models.ts`

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
**Message**: `feat(ui): implement register and login pages`
**Pushed by**: Lead Developer
**Date**:
