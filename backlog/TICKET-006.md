# TICKET-006: Repository Pattern (Interfaces + Implementations)

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Backend Developer
**Depends on**: TICKET-004
**User Story**: none (infrastructure)
**Complexity**: M

## Description
Define repository interfaces in Application layer, implement them in Infrastructure. One repository per aggregate root: IUserRepository, ITournamentRepository, IGameWeekRepository, IMatchRepository, IPredictionRepository. Register in DI.

## Acceptance Criteria
- [ ] Interface `IUserRepository` in Application with: GetByIdAsync, GetByEmailAsync, GetByUsernameAsync, AddAsync, UpdateAsync
- [ ] Interface `ITournamentRepository` with: GetAllAsync, GetByIdAsync, GetActiveAsync, AddAsync, UpdateAsync, DeleteAsync
- [ ] Interface `IGameWeekRepository` with: GetByTournamentIdAsync, GetByIdAsync, AddAsync, UpdateAsync
- [ ] Interface `IMatchRepository` with: GetUpcomingAsync, GetByIdAsync, GetByGameWeekIdAsync, AddAsync, UpdateAsync
- [ ] Interface `IPredictionRepository` with: GetByUserIdAsync, GetByMatchIdAsync, GetByUserAndMatchAsync, AddAsync, UpdateAsync
- [ ] All interfaces implemented in Infrastructure using `AppDbContext`
- [ ] Registered in DI in `Program.cs` (scoped lifetime)

## Test Plan
- [ ] Unit test: UserRepository.GetByEmailAsync returns correct user
- [ ] Unit test: PredictionRepository.GetByUserAndMatchAsync returns null when not found

## Technical Notes
- Interfaces live in `Application/Interfaces/Repositories/`
- Implementations live in `Infrastructure/Repositories/`
- Use `AsNoTracking()` for read-only queries

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Repositories/IUserRepository.cs`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Repositories/ITournamentRepository.cs`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Repositories/IGameWeekRepository.cs`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Repositories/IMatchRepository.cs`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Repositories/IPredictionRepository.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Repositories/UserRepository.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Repositories/TournamentRepository.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Repositories/GameWeekRepository.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Repositories/MatchRepository.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Repositories/PredictionRepository.cs`
- [ ] `backend/src/FootballPrediction.Api/Program.cs` (DI registrations)

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
**Message**: `feat(db): add repository interfaces and implementations`
**Pushed by**: Lead Developer
**Date**:
