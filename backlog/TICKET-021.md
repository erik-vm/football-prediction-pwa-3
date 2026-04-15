# TICKET-021: PWA Setup (Manifest, Service Worker, Offline)

**Status**: BACKLOG
**Priority**: P2
**Assigned to**: Frontend Developer
**Depends on**: TICKET-017, TICKET-018
**User Story**: FR-PWA-1, FR-PWA-2, US-6
**Complexity**: M

## Description
Configure Angular PWA: add `@angular/pwa`, create web app manifest, set up service worker for offline caching of leaderboard and predictions. Add install prompt.

## Acceptance Criteria
- [ ] `@angular/pwa` package added and configured
- [ ] `manifest.webmanifest` with app name, icons, theme color, display=standalone
- [ ] Service worker caches: leaderboard data, own predictions, static assets
- [ ] Offline: cached leaderboard/predictions viewable without connection
- [ ] Install prompt shown to eligible users
- [ ] Lighthouse PWA score: 100
- [ ] `ng build --configuration production` includes service worker

## Test Plan
- [ ] Manual: install app to home screen on Android Chrome
- [ ] Manual: disable network, cached data still visible
- [ ] Lighthouse PWA audit passes

## Technical Notes
- `ng add @angular/pwa`
- `ngsw-config.json` configure cache strategies (network-first for API, cache-first for assets)
- ERROR-PREVENTION #9: output dir is `dist/frontend/browser`

## Files to Create/Modify
- [ ] `frontend/src/manifest.webmanifest`
- [ ] `frontend/ngsw-config.json`
- [ ] `frontend/src/app/core/services/pwa.service.ts` (install prompt)
- [ ] `frontend/angular.json` (serviceWorker: true in production config)

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
**Message**: `feat(pwa): add PWA manifest, service worker, and offline support`
**Pushed by**: Lead Developer
**Date**:
