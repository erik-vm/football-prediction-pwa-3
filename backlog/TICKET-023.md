# TICKET-023: Frontend Vercel Deployment

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: DevOps Engineer
**Depends on**: TICKET-021, TICKET-022
**User Story**: none (deployment)
**Complexity**: S

## Description
Deploy Angular PWA to Vercel. Configure SPA routing rewrite, set `environment.prod.ts` API URL to Render backend, set output directory correctly.

## Acceptance Criteria
- [ ] `vercel.json` with SPA rewrite rule (all non-asset paths → index.html)
- [ ] `environment.prod.ts` `apiUrl` points to Render backend URL
- [ ] Vercel output directory set to `dist/frontend/browser`
- [ ] `ng build --configuration production` output deploys correctly
- [ ] Deep links (e.g. `/predictions`) work on page refresh
- [ ] CORS on backend allows the Vercel production URL
- [ ] Lighthouse PWA score 100 on deployed site

## Test Plan
- [ ] Navigate to `/leaderboard` directly → no 404
- [ ] Refresh on `/predictions` → app loads correctly
- [ ] Install to home screen works on deployed URL
- [ ] API calls reach Render backend successfully

## Technical Notes
- ERROR-PREVENTION #12: `vercel.json` rewrite: `{ "rewrites": [{ "source": "/((?!assets|.*\\.).*)", "destination": "/index.html" }] }`
- ERROR-PREVENTION #9: output dir `dist/frontend/browser`
- ERROR-PREVENTION #13: add Vercel production URL to backend CORS

## Files to Create/Modify
- [ ] `frontend/vercel.json`
- [ ] `frontend/src/environments/environment.prod.ts` (update apiUrl)

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
**Message**: `chore(deploy): configure Vercel deployment for Angular PWA`
**Pushed by**: Lead Developer
**Date**:
