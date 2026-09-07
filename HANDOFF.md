# Handoff — Fleet Reservation System

Everything a fresh session (Claude Code, another tool, or a human teammate) needs to pick this
up without re-deriving any of it. Read this + `PROGRESS.md` first.

## What this is

A full-stack vehicle/fleet reservation system — ASP.NET Core Web API + EF Core/PostgreSQL +
Angular — being built in ~1 week to get genuinely interview-ready for a backend .NET role at
a company running that exact stack. Builder knows backend concepts cold (real full-stack
production experience) but started this project at zero .NET/C# syntax.

## The plan

- [x] Phase 1 — Environment & tooling setup (Docker Postgres 17 + .NET 10 SDK verified)
- [x] Phase 2 — Core architecture decisions (Controllers, EF Core Npgsql, snake_case)
- [ ] Phase 3 — Domain modeling & EF Core setup → **in progress (models built, DbContext next)**
- [ ] Phase 4 — Core CRUD & business rules (state machine, conflict checks)
- [ ] Phase 5 — Authentication & authorization
- [ ] Phase 6 — Testing
- [ ] Phase 7 — Angular frontend
- [ ] Phase 8 — Polish & interview-prep consolidation

## Decisions already made (reasoning in full in `concepts.html` / `interview_qa.html`)

1. **.NET 10** — current LTS, supported into 2028. Nearly everything here transfers cleanly to
   .NET 8, which a lot of production shops (possibly the one interviewing) are still running.
2. **Controllers, not Minimal APIs** — Microsoft's default recommendation is now Minimal APIs,
   but controllers are still what most long-lived enterprise ASP.NET Core codebases actually
   run, and they exercise more of the classic C# syntax (attributes, constructor injection,
   model binding) that's the whole point of this project.
3. **Single project, folder-based layering** (`Controllers/`, `Services/`, `Data/`, `Models/`,
   `DTOs/`) instead of a multi-project Clean Architecture split. Chosen for the one-week
   timeline — a deliberate, explainable tradeoff, not a shortcut taken silently. Worth being
   ready to defend as a choice, not hide.
4. **EF Core + Npgsql.EntityFrameworkCore.PostgreSQL + EFCore.NamingConventions** — Postgres
   and EF Core fight on naming by default (PascalCase C# vs. Postgres's lowercase-unless-
   quoted identifiers); fixed once at the provider level with
   `.UseSnakeCaseNamingConvention()` instead of annotating every entity.
5. **Docker Compose for local Postgres** — root `docker-compose.yml` runs PostgreSQL 17 on port 5432,
   matching connection string in `src/FleetReservation.Api/appsettings.Development.json`.
6. **Enum Storage Strategy** — enums stored as strings in Postgres via `.HasConversion<string>()` in
   the DbContext to prevent silent corruption upon enum reordering.
7. **UTC Timestamps** — all dates stored in UTC (`timestamptz`) as strictly enforced by Npgsql.

Package versions in `FleetReservation.Api.csproj`: `Npgsql.EntityFrameworkCore.PostgreSQL` 10.0.3,
`Microsoft.EntityFrameworkCore.Design` 10.0.11, `EFCore.NamingConventions` 10.0.1.

## Domain models — built and compiled (0 errors, 0 warnings)

Located in `src/FleetReservation.Api/Models/`:
- `Vehicle.cs` & `VehicleStatus.cs` (Available, Reserved, CheckedOut, UnderMaintenance)
- `ApplicationUser.cs` & `UserRole.cs` (FleetManager, Employee)
- `Reservation.cs` & `ReservationStatus.cs` (Requested, Approved, CheckedOut, Returned, Cancelled, Overdue)
- `MaintenanceWindow.cs`

Relationships wired via Navigation Properties (`ICollection<Reservation>`, `Vehicle`, `ApplicationUser`).

## Working agreement & Familiarity Calibration

- **User writes:** anything genuinely important to understand or plausible interview
  material — domain entities, business logic (state machine, conflict-check queries),
  auth/authorization logic, tests.
- **Assistant writes:** boilerplate and anything unlikely to come up in the interview —
  project scaffolding, config wiring, DTOs, setup docs.
- **Pedagogical Stance:** User is learning C# syntax, type safety nuances (nullable reference types, `= null!;`),
  EF Core mechanics (navigation properties, DbContext), and SQL translation. Explain syntax and mechanics thoroughly.
- **Verification Active:** Interactive retrieval check-ins and mock interview preparation enabled.

## Companion docs status

- `concepts.html` — 6 entries (Controllers vs Minimal APIs, EF Core snake_case naming, Enum String Mapping, Navigation Properties & Nullable Reference Types, ORM & EF Core, PostgreSQL UTC Timestamps)
- `interview_qa.html` — 6 questions (Q001-Q003 from kickoff/Phase 2, Q004 Enum string storage, Q005 ICollection navigation properties, Q006 Trick question on Npgsql DateTimeKind)
- `walkthrough.html` — 2 phase beats (Phase 1 & Phase 2)

## What's already in this folder

```
fleet-reservation-system/
  PROGRESS.md          — live status: phases, calibration, verification, friction log
  HANDOFF.md           — this file (handoff bridge across sessions)
  SETUP.md             — local dev setup instructions
  README.md            — portfolio-grade project documentation
  concepts.html        — interactive concepts log (6 entries)
  interview_qa.html    — interactive Q&A flashcards (6 questions)
  walkthrough.html     — high-level narrative walkthrough (2 beats)
  docker-compose.yml   — Postgres 17 container definition
  src/
    FleetReservation.slnx
    FleetReservation.Api/
      Models/          — Vehicle, VehicleStatus, ApplicationUser, UserRole,
                         Reservation, ReservationStatus, MaintenanceWindow
      Controllers/     — (empty, ready for Phase 4)
      appsettings.Development.json
      Program.cs
```

## Immediate next step for the next session

**Phase 3 completion:**
1. Create `src/FleetReservation.Api/Data/FleetReservationDbContext.cs`:
   - Inherit from `DbContext`.
   - Add `DbSet<Vehicle> Vehicles`, `DbSet<ApplicationUser> Users`, `DbSet<Reservation> Reservations`, `DbSet<MaintenanceWindow> MaintenanceWindows`.
   - In `OnModelCreating`, configure `.HasConversion<string>()` for all enums.
2. Register `FleetReservationDbContext` in `src/FleetReservation.Api/Program.cs` using `.UseNpgsql(...)` with `.UseSnakeCaseNamingConvention()`.
3. Generate the initial EF Core migration:
   `dotnet ef migrations add InitialCreate --project FleetReservation.Api`
4. Apply the migration to the running PostgreSQL database:
   `dotnet ef database update --project FleetReservation.Api`
5. Check off Phase 3 in `PROGRESS.md`, record Phase 3 beat in `walkthrough.html`, and proceed to Phase 4 (Core CRUD & Business Rules).
