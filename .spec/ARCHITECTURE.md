# Architecture

## Clean Architecture (4 Layers)

```
ProjectName.Domain        -> Entities only, zero dependencies
ProjectName.Application   -> Interfaces, DTOs, service contracts, validators
ProjectName.Infrastructure-> EF Core, repositories, external API clients
ProjectName.Api           -> Controllers, middleware, background jobs, Program.cs
```

**Dependency direction**: Api -> Infrastructure -> Application -> Domain

## Backend Patterns

| Pattern | Usage |
|---------|-------|
| Repository | Data access abstraction (one per aggregate root) |
| Service Layer | Business logic in service classes |
| Background Service | IHostedService for periodic tasks |
| JWT Bearer Auth | Stateless authentication with refresh tokens |
| Request Validation | FluentValidation for input DTOs |
| CQRS-lite | Separate read/write models where beneficial |

## Database

- **PostgreSQL 16** via Docker (dev) / Render (prod)
- **EF Core 9** with code-first migrations
- **Auto-timestamping**: CreatedAt/UpdatedAt set in DbContext.SaveChangesAsync()
- **Unique constraints**: On business keys, not just PKs

## Frontend Patterns

| Pattern | Usage |
|---------|-------|
| Standalone Components | No NgModules - Angular 19 default |
| Signals | Reactive state management (no NgRx needed) |
| Lazy Loading | Route-level code splitting |
| Feature Folders | Organize by feature, not by type |
| Core Services | Singleton services in core/ (API, Auth, Storage) |
| Smart/Dumb Components | Container components handle data, child components display |

## Authentication Flow

```
Register/Login -> Backend validates -> Returns JWT (60min) + RefreshToken (7 days)
                                        |
Frontend stores in localStorage -> Attaches Bearer token via interceptor
                                        |
On 401 -> Try refresh token -> If expired -> Redirect to /login
```

## Deployment Architecture

```
[Vercel] <- Angular PWA (static files)
    | API calls
[Render] <- ASP.NET Core API + PostgreSQL
    | External API (if applicable)
[External Services]
```
