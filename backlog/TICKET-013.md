# TICKET-013: Angular Project Setup (Tailwind, Routing, Guards, Interceptors)

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Frontend Developer
**Depends on**: TICKET-008
**User Story**: none (infrastructure)
**Complexity**: M

## Description
Create Angular 19 project under `frontend/`. Configure Tailwind CSS 3.4.17, set up routing with lazy-loaded feature modules, auth guard, JWT interceptor, and error interceptor. Configure environment files for dev/prod API URLs.

## Acceptance Criteria
- [ ] Angular 19 project created under `frontend/` with strict TypeScript
- [ ] Tailwind CSS 3.4.17 installed and configured (NOT v4)
- [ ] `environment.ts` and `environment.prod.ts` with `apiUrl`
- [ ] `angular.json` production build has `fileReplacements` for env swap
- [ ] `AuthInterceptor` attaches Bearer token to all API requests
- [ ] On 401 response: interceptor attempts token refresh; on failure redirects to `/login`
- [ ] `AuthGuard` protects authenticated routes
- [ ] `AdminGuard` protects admin routes (requires ADMIN role)
- [ ] Wildcard route `{ path: '**', redirectTo: '' }` added last
- [ ] Desktop layout: `<div class="max-w-lg mx-auto">` wrapping `<router-outlet>`
- [ ] `ng build` succeeds

## Test Plan
- [ ] `ng build` succeeds with 0 errors
- [ ] `ng serve` starts on port 4200

## Technical Notes
- ERROR-PREVENTION #8: `npm install tailwindcss@3.4.17 postcss autoprefixer`
- ERROR-PREVENTION #9: Angular 19 output dir is `dist/frontend/browser`
- ERROR-PREVENTION #10: `fileReplacements` in angular.json production config
- ERROR-PREVENTION #11: wildcard route last in app.routes.ts
- ERROR-PREVENTION #19: max-w-lg mx-auto wrapper for desktop
- ERROR-PREVENTION #20: match `environment.ts` apiUrl port to `launchSettings.json`
- Standalone components only (no NgModules)
- Feature folders: `core/`, `features/auth/`, `features/predictions/`, `features/leaderboard/`, `features/admin/`, `shared/`

## Files to Create/Modify
- [ ] `frontend/` (new Angular project)
- [ ] `frontend/src/environments/environment.ts`
- [ ] `frontend/src/environments/environment.prod.ts`
- [ ] `frontend/src/app/core/interceptors/auth.interceptor.ts`
- [ ] `frontend/src/app/core/guards/auth.guard.ts`
- [ ] `frontend/src/app/core/guards/admin.guard.ts`
- [ ] `frontend/src/app/core/services/auth.service.ts`
- [ ] `frontend/src/app/app.routes.ts`
- [ ] `frontend/src/app/app.component.ts`
- [ ] `frontend/tailwind.config.js`
- [ ] `frontend/postcss.config.js`

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
**Message**: `chore(setup): initialize Angular 19 frontend with Tailwind and routing`
**Pushed by**: Lead Developer
**Date**:
