# Development Workflow

## Phase 1: Project Bootstrap

### Step 1: Orchestrator reads START-HERE.md
### Step 2: Orchestrator spawns Analyst + Architect + Researcher

### Step 3: Planning Session
1. **Analyst** reviews USER-STORIES.md, validates acceptance criteria
2. **Architect** reviews ARCHITECTURE.md and PROJECT-SPEC.md
3. **Researcher** investigates any unknowns (API docs, framework features)
4. Together they create the **Development Plan**:
   - Ordered list of tickets with dependencies
   - Each ticket assigned to an agent type
   - Critical path identified

### Step 4: Orchestrator creates tickets in backlog/

## Phase 2: Foundation (P0 tickets)

### Infrastructure Setup
1. **DB Specialist** creates entity configurations and initial migration
2. **Backend Developer** sets up project structure (Clean Architecture)
3. **DevOps Engineer** creates docker-compose.yml and Dockerfile
4. **Lead Developer** pushes foundation code

### Order of Foundation Tickets
```
1. Project setup (solution, projects, packages)          -> Backend Dev
2. Database schema + migration                           -> DB Specialist
3. Docker setup (PostgreSQL)                             -> DevOps
4. Entity configurations (DbContext, OnModelCreating)    -> DB Specialist
5. Repository interfaces + implementations               -> Backend Dev
6. JWT authentication (service, controller, middleware)   -> Backend Dev
7. Core business logic services                          -> Backend Dev + QA
```

## Phase 3: Core Features (P0 tickets)

### Backend first, then Frontend
```
8-13.  Backend CRUD, services, background jobs          -> Backend Dev
14.    Frontend foundation (routing, guards, interceptors) -> Frontend Dev
15-18. Feature UIs                                       -> Frontend Dev + UI Designer
```

## Phase 4: Enhancement Features (P1 tickets)

```
Additional views, preferences, navigation, polish       -> Frontend Dev + Backend Dev
```

## Phase 5: PWA & Polish (P2 tickets)

```
PWA setup, offline support, theming, tooltips           -> Frontend Dev + DevOps
```

## Phase 6: Deployment (P0)

```
Backend deployment (Render), Frontend deployment (Vercel),
production config, CORS, end-to-end verification        -> DevOps + QA
```

## Test-First Development Flow

For each feature ticket:
```
1.  QA reads acceptance criteria
2.  QA writes failing tests
3.  Developer receives ticket + failing tests
4.  Developer implements code until tests pass
5.  Developer runs full test suite (must all pass)
6.  Developer requests Code Review
7.  Code Reviewer reviews quality/design (APPROVED / CHANGES_REQUESTED)
8.  Security Engineer reviews (MANDATORY for auth, CORS, secrets, new endpoints)
9.  QA does final verification
10. Lead Developer pushes to git
11. Orchestrator marks ticket DONE
```

**Security review is mandatory** for tickets touching: auth, CORS, new API endpoints, environment config, user data handling. For other tickets, Security Engineer review is optional but Code Reviewer must still check basic security items.

## Quality Gates (Non-Negotiable)

Before ANY ticket can be marked DONE:
- [ ] All acceptance criteria met
- [ ] All tests pass (existing + new)
- [ ] Build succeeds with 0 errors, 0 warnings
- [ ] Code reviewed and approved
- [ ] QA verified
- [ ] Committed with proper message format

## Error Prevention

Before EVERY phase, check ERROR-PREVENTION.md for relevant warnings:
- Phase 2 (Foundation): Tooling, infrastructure errors
- Phase 3 (Backend): Serialization, logic, idempotency errors
- Phase 3 (Frontend): Tailwind, Angular errors
- Phase 6 (Deployment): URLs, CORS, Dockerfile errors
