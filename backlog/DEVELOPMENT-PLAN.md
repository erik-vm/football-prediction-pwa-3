# Development Plan

**Created**: 2026-04-15
**Status**: ACTIVE

## Ticket Overview

### Phase 2: Foundation (P0) — Critical Path

| Ticket | Title | Agent | Depends On | Complexity |
|--------|-------|-------|------------|------------|
| TICKET-001 | .NET Solution & Project Structure Setup | Backend Dev | — | M |
| TICKET-002 | Docker Setup (PostgreSQL) | DevOps | TICKET-001 | S |
| TICKET-003 | Domain Entities | Backend Dev | TICKET-001 | S |
| TICKET-004 | EF Core DbContext & Entity Configuration | DB Specialist | TICKET-003 | M |
| TICKET-005 | Initial EF Core Migration | DB Specialist | TICKET-004, TICKET-002 | S |
| TICKET-006 | Repository Pattern | Backend Dev | TICKET-004 | M |
| TICKET-007 | Scoring Service | Backend Dev | TICKET-003 | S |

### Phase 3: Core Features Backend (P0)

| Ticket | Title | Agent | Depends On | Complexity |
|--------|-------|-------|------------|------------|
| TICKET-008 | JWT Authentication API | Backend Dev | TICKET-006 | L |
| TICKET-009 | Tournament & GameWeek Admin API | Backend Dev | TICKET-008 | M |
| TICKET-010 | Match Admin API + Result Entry | Backend Dev | TICKET-009 | M |
| TICKET-011 | Prediction Submission API | Backend Dev | TICKET-010 | M |
| TICKET-012 | Leaderboard API + Weekly Bonuses | Backend Dev | TICKET-011 | M |

### Phase 3: Core Features Frontend (P0)

| Ticket | Title | Agent | Depends On | Complexity |
|--------|-------|-------|------------|------------|
| TICKET-013 | Angular Project Setup | Frontend Dev | TICKET-008 | M |
| TICKET-014 | Auth UI (Register + Login) | Frontend Dev | TICKET-013 | M |
| TICKET-018 | Navigation Shell & Bottom Nav | Frontend Dev | TICKET-013 | S |
| TICKET-015 | Predictions UI | Frontend Dev | TICKET-014 | L |
| TICKET-016 | Leaderboard UI | Frontend Dev | TICKET-015 | M |
| TICKET-017 | Admin UI | Frontend Dev | TICKET-016 | L |

### Phase 4: Enhancement Features (P1)

| Ticket | Title | Agent | Depends On | Complexity |
|--------|-------|-------|------------|------------|
| TICKET-019 | User Statistics API & UI | Backend Dev + Frontend Dev | TICKET-012, TICKET-016 | M |
| TICKET-020 | Match Import from football-data.org | Backend Dev | TICKET-009 | M |

### Phase 5: PWA & Polish (P2)

| Ticket | Title | Agent | Depends On | Complexity |
|--------|-------|-------|------------|------------|
| TICKET-021 | PWA Setup (Manifest, Service Worker, Offline) | Frontend Dev | TICKET-017, TICKET-018 | M |

### Phase 6: Deployment (P0)

| Ticket | Title | Agent | Depends On | Complexity |
|--------|-------|-------|------------|------------|
| TICKET-022 | Backend Dockerfile & Render Deployment | DevOps | TICKET-012 | M |
| TICKET-023 | Frontend Vercel Deployment | DevOps | TICKET-021, TICKET-022 | S |

---

## Critical Path

```
001 → 003 → 004 → 005
              ↓
001 → 002 → 005
              ↓
004 → 006 → 008 → 009 → 010 → 011 → 012 → 022
003 → 007
008 → 013 → 014 → 015 → 016 → 017 → 021 → 023
013 → 018
```

**Total tickets**: 23
**P0 (critical path)**: 18
**P1 (important)**: 3
**P2 (nice-to-have)**: 2

---

## Execution Order

Execute strictly in this order (respecting dependencies):

1. TICKET-001 (solution setup)
2. TICKET-002 + TICKET-003 (parallel — docker and domain entities)
3. TICKET-004 (DbContext, needs entities)
4. TICKET-005 (migration, needs docker + DbContext)
5. TICKET-006 + TICKET-007 (parallel — repositories and scoring service)
6. TICKET-008 (auth, needs repositories)
7. TICKET-009 (tournament/gameweek, needs auth)
8. TICKET-010 (match management, needs gameweeks)
9. TICKET-011 (predictions, needs matches)
10. TICKET-012 (leaderboard, needs predictions)
11. TICKET-013 (Angular setup, can start when TICKET-008 API contracts defined)
12. TICKET-014 + TICKET-018 (parallel — auth UI and nav shell)
13. TICKET-015 (predictions UI)
14. TICKET-016 (leaderboard UI)
15. TICKET-017 (admin UI)
16. TICKET-019 + TICKET-020 (parallel — P1 features)
17. TICKET-021 (PWA, needs all UI done)
18. TICKET-022 + TICKET-023 (parallel deployment — need backend/frontend complete)
