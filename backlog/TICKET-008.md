# TICKET-008: JWT Authentication API

**Status**: BACKLOG
**Priority**: P0
**Assigned to**: Backend Developer
**Depends on**: TICKET-006
**User Story**: FR-AUTH-1, FR-AUTH-2, FR-AUTH-3, FR-AUTH-4
**Complexity**: L

## Description
Implement full JWT authentication: register, login, refresh token, logout, get current user, change password. BCrypt for password hashing. Role-based access with USER/ADMIN roles.

## Acceptance Criteria
- [ ] `POST /api/v1/auth/register` creates user, returns 201 with JWT + refresh token
- [ ] `POST /api/v1/auth/login` validates credentials, returns 200 with JWT + refresh token
- [ ] `POST /api/v1/auth/refresh` exchanges valid refresh token for new JWT pair
- [ ] `POST /api/v1/auth/logout` invalidates refresh token
- [ ] `GET /api/v1/auth/me` returns current user (requires auth)
- [ ] `PUT /api/v1/auth/change-password` updates password (requires current password)
- [ ] JWT access token: 15-minute expiration
- [ ] Refresh token: 7-day expiration, stored in DB with rotation
- [ ] Passwords hashed with BCrypt cost factor 12
- [ ] FluentValidation on register (username 3-20, email valid, password 8-64)
- [ ] Admin-only endpoints protected with `[Authorize(Roles = "ADMIN")]`
- [ ] Duplicate email/username returns 409 Conflict
- [ ] Invalid credentials return 401

## Test Plan
- [ ] Unit test: register with valid data creates user
- [ ] Unit test: register with duplicate email returns 409
- [ ] Unit test: login with wrong password returns 401
- [ ] Unit test: refresh with expired token returns 401
- [ ] Unit test: change-password with wrong current password returns 400
- [ ] Integration test: full register → login → refresh flow

## Technical Notes
- ERROR-PREVENTION #15: Add `ReferenceHandler.IgnoreCycles` to JSON options
- JWT signing key from `JwtSettings:Secret` env var (min 32 chars)
- Store `RefreshToken` entity in DB (UserId, Token, ExpiresAt, IsRevoked)
- BCrypt.Net-Next version 4.0.3, cost factor 12
- Add `IAuthService` interface + `AuthService` implementation

## Files to Create/Modify
- [ ] `backend/src/FootballPrediction.Domain/Entities/RefreshToken.cs`
- [ ] `backend/src/FootballPrediction.Application/DTOs/Auth/RegisterRequest.cs`
- [ ] `backend/src/FootballPrediction.Application/DTOs/Auth/LoginRequest.cs`
- [ ] `backend/src/FootballPrediction.Application/DTOs/Auth/AuthResponse.cs`
- [ ] `backend/src/FootballPrediction.Application/Interfaces/Services/IAuthService.cs`
- [ ] `backend/src/FootballPrediction.Application/Validators/RegisterRequestValidator.cs`
- [ ] `backend/src/FootballPrediction.Infrastructure/Services/AuthService.cs`
- [ ] `backend/src/FootballPrediction.Api/Controllers/AuthController.cs`
- [ ] `backend/src/FootballPrediction.Api/Program.cs` (JWT + auth middleware setup)
- [ ] `backend/tests/FootballPrediction.Tests/Services/AuthServiceTests.cs`

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
**Message**: `feat(auth): implement JWT authentication with register, login, refresh`
**Pushed by**: Lead Developer
**Date**:
