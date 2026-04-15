# TICKET-022: Backend Dockerfile & Render Deployment

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: DevOps Engineer
**Depends on**: TICKET-012
**User Story**: none (deployment)
**Complexity**: M

## Description
Create Dockerfile for .NET 9 API. Configure Render service with PostgreSQL. Handle production environment variables, Render's `postgres://` URL, CORS for Vercel domain.

## Acceptance Criteria
- [ ] Multi-stage `Dockerfile` in `backend/` builds successfully
- [ ] `render.yaml` or Render dashboard config documented
- [ ] PostgreSQL URI parser handles Render's `postgres://` format (already in TICKET-004 code)
- [ ] CORS configured to allow Vercel domain + localhost:4200
- [ ] JWT secret, DB URL, Football Data API key all via Render env vars (not in code)
- [ ] Health check endpoint `GET /health` returns 200
- [ ] Production build runs DB migration on startup via `EnsureCreated` or startup migration runner
- [ ] `docker build -t football-api .` succeeds locally

## Test Plan
- [ ] `docker build` succeeds
- [ ] `docker run` starts and `/health` returns 200
- [ ] Deployed to Render, `/api/v1/auth/register` returns valid response

## Technical Notes
- ERROR-PREVENTION #7: `postgres://` URI parser (from TICKET-004)
- ERROR-PREVENTION #13: CORS `.SetIsOriginAllowed(origin => origin.Contains("vercel.app") || origin.Contains("localhost"))`
- ERROR-PREVENTION #14: Copy ALL .csproj files before restore in Dockerfile
- Apply pending migrations at startup: `dbContext.Database.Migrate()`

## Files to Create/Modify
- [ ] `backend/Dockerfile`
- [ ] `backend/src/FootballPrediction.Api/Program.cs` (startup migration, CORS update)
- [ ] `.env.example` (document required env vars)

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
**Message**: `chore(deploy): add Dockerfile and Render deployment configuration`
**Pushed by**: Lead Developer
**Date**:
