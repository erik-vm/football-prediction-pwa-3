# TICKET-002: Docker Setup (PostgreSQL)

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: DevOps Engineer
**Depends on**: TICKET-001
**User Story**: none (infrastructure)
**Complexity**: S

## Description
Create `docker-compose.yml` in `backend/` that runs PostgreSQL 16 on port 5433 (to avoid conflict with any local Postgres on 5432).

## Acceptance Criteria
- [ ] `backend/docker-compose.yml` created with PostgreSQL 16 service
- [ ] Maps host port 5433 → container port 5432
- [ ] Environment variables: POSTGRES_DB, POSTGRES_USER, POSTGRES_PASSWORD
- [ ] Health check configured
- [ ] `docker compose up -d` starts without errors
- [ ] Database reachable at `localhost:5433`

## Test Plan
- [ ] `docker compose up -d` exits 0
- [ ] `netstat -ano | findstr :5433` shows listening port

## Technical Notes
- ERROR-PREVENTION #5: Use port 5433 to avoid conflict with local Postgres on 5432
- ERROR-PREVENTION #6: Document that Docker Desktop must be started manually before `docker compose up`
- Connection string for dev: `Host=localhost;Port=5433;Database=footballprediction;Username=postgres;Password=postgres`

## Files to Create/Modify
- [ ] `backend/docker-compose.yml`
- [ ] `backend/README.md` (brief dev setup instructions)

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
**Message**: `chore(deploy): add docker-compose for local PostgreSQL`
**Pushed by**: Lead Developer
**Date**:
