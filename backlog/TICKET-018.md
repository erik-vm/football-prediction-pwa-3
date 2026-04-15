# TICKET-018: Navigation Shell & Bottom Nav

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Frontend Developer
**Depends on**: TICKET-013
**User Story**: US-1, US-2
**Complexity**: S

## Description
Build the app shell with bottom navigation bar for mobile. Tabs: Predictions, Leaderboard, Profile, Admin (conditionally shown for admins). Header with app name and logout.

## Acceptance Criteria
- [ ] Bottom navigation bar with: Predictions, Leaderboard, Profile tabs
- [ ] Admin tab only visible to ADMIN role users
- [ ] Active tab highlighted
- [ ] Header shows app name and logout button
- [ ] Responsive: bottom nav on mobile, sidebar or top nav optional on desktop
- [ ] Logout clears localStorage and redirects to /login

## Test Plan
- [ ] Component test: admin tab hidden for USER role
- [ ] Component test: logout clears auth state

## Technical Notes
- Shell component wraps all authenticated routes
- Use Angular signals for current user state

## Files to Create/Modify
- [ ] `frontend/src/app/shared/components/shell/shell.component.ts`
- [ ] `frontend/src/app/shared/components/bottom-nav/bottom-nav.component.ts`

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
**Message**: `feat(ui): add navigation shell and bottom nav bar`
**Pushed by**: Lead Developer
**Date**:
