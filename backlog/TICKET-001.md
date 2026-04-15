# TICKET-001: .NET Solution & Project Structure Setup

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Backend Developer
**Depends on**: none
**User Story**: none (infrastructure)
**Complexity**: M

## Description
Create the .NET 9 solution with Clean Architecture layers: Domain, Application, Infrastructure, Api. Install all NuGet packages at exact versions from TECH-STACK.md. Set up global.json to pin .NET 9 SDK.

## Acceptance Criteria
- [ ] Solution file `FootballPrediction.sln` created under `backend/`
- [ ] Four projects created: `FootballPrediction.Domain`, `FootballPrediction.Application`, `FootballPrediction.Infrastructure`, `FootballPrediction.Api`
- [ ] Dependency direction correct: Api → Infrastructure → Application → Domain
- [ ] All NuGet packages installed at exact versions (TECH-STACK.md)
- [ ] `global.json` pins .NET 9 SDK
- [ ] `dotnet build` passes with 0 errors and 0 warnings

## Test Plan
- [ ] `dotnet build` succeeds

## Technical Notes
- ERROR-PREVENTION #1: Create `global.json` with `{ "sdk": { "version": "9.0.100", "rollForward": "latestMinor" } }`
- ERROR-PREVENTION #4: All EF Core packages must be 9.0.x
- Project structure per TECH-STACK.md
- Add JSON serialization cycle fix at startup (ERROR-PREVENTION #15)

## Files to Create/Modify
- [ ] `backend/global.json`
- [ ] `backend/FootballPrediction.sln`
- [ ] `backend/src/FootballPrediction.Domain/FootballPrediction.Domain.csproj`
- [ ] `backend/src/FootballPrediction.Application/FootballPrediction.Application.csproj`
- [ ] `backend/src/FootballPrediction.Infrastructure/FootballPrediction.Infrastructure.csproj`
- [ ] `backend/src/FootballPrediction.Api/FootballPrediction.Api.csproj`
- [ ] `backend/src/FootballPrediction.Api/Program.cs`

---

## Implementation Log
_Updated by developer during implementation_

---

## Review Notes
_Updated by Code Reviewer_

**Reviewer**: Code Reviewer
**Result**:
**Findings**:

---

## QA Notes
_Updated by QA Engineer_

**Tester**: QA Engineer
**Result**:
**Evidence**:

---

## Git Info
**Commit**:
**Message**: `chore(setup): initialize .NET 9 Clean Architecture solution`
**Pushed by**: Lead Developer
**Date**:
