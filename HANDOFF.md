# Handoff — Fleet Reservation System

Everything a fresh session (Claude Code, another tool, or a human teammate) needs to pick this
up without re-deriving any of it. Read this + `PROGRESS.md` first.

## What this is

A full-stack vehicle/fleet reservation system — ASP.NET Core Web API + EF Core/PostgreSQL +
Angular — being built in ~1 week to get genuinely interview-ready for a backend .NET role at
a company running that exact stack. Builder knows backend concepts cold (real full-stack
production experience) but started this project at zero .NET/C# syntax.

## The plan

- [ ] Phase 1 — Environment & tooling setup → `SETUP.md`
- [ ] Phase 2 — Core architecture decisions → **done, see below**
- [ ] Phase 3 — Domain modeling & EF Core setup → **next step**
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
5. **Docker Compose for local Postgres** — reproducible across machines, sidesteps native
   install differences, and mirrors how a lot of real teams run local dev databases anyway.

Package versions already in `FleetReservation.Api.csproj` (confirmed current at the time this
was written): `Npgsql.EntityFrameworkCore.PostgreSQL` 10.0.3, `Microsoft.EntityFrameworkCore.
Design` 10.0.11, `EFCore.NamingConventions` 10.0.1. `Microsoft.AspNetCore.Authentication.
JwtBearer` and the testing packages (xunit, `Microsoft.AspNetCore.Mvc.Testing`) are deliberately
*not* added yet — add those with `dotnet add package` when Phases 5 and 6 actually start, both
because that command is itself worth the reps and because pinning versions this far ahead of
using them tends to go stale.

## Domain model — planned, not yet built (this is Phase 3)

**Entities:**
- `Vehicle` — Id, Make, Model, LicensePlate, Status (enum: Available / Reserved / CheckedOut /
  UnderMaintenance)
- `Driver` (or `ApplicationUser`) — Id, Name, Email, Role (enum: FleetManager / Employee)
- `Reservation` — Id, VehicleId, DriverId, StartTime, EndTime, Status (enum: Requested /
  Approved / CheckedOut / Returned / Overdue / Cancelled)
- `MaintenanceWindow` — Id, VehicleId, StartTime, EndTime, Reason

**Relationships:** Vehicle 1—* Reservation, Driver 1—* Reservation, Vehicle 1—* MaintenanceWindow

**Core business rules for Phase 4:**
- A reservation can't be created if its date range overlaps an existing active reservation
  *or* a maintenance window on the same vehicle — two conflict checks, same underlying overlap
  logic, genuinely good material for a "how did you avoid repeating yourself" answer.
- State machine: Requested → Approved → CheckedOut → Returned. **Open question, decide together
  in Phase 4:** should `Overdue` be a stored status that something has to actively set, or a
  computed value (`CheckedOut` + `EndTime` already passed)? Both are defensible; picking one
  deliberately, with a reason, is the point — don't let it default silently.
- Only `FleetManager` can register vehicles, schedule maintenance, and approve reservations;
  `Employee` can request reservations and view their own.

This list is a strong starting point, not a spec to implement blindly — revisit it together
once actually modeling in Phase 3. Real modeling usually surfaces something the plan missed.

## Working agreement

- **User writes:** anything genuinely important to understand or plausible interview
  material — domain entities, business logic (state machine, conflict-check queries),
  auth/authorization logic, tests.
- **Assistant writes:** boilerplate and anything unlikely to come up in the interview —
  project scaffolding, config wiring, DTOs, setup docs. Explanation happens either way,
  regardless of who's typing.
- **Execution happens locally.** The planning session that produced these files runs in a
  sandbox that can't reach nuget.org — nothing past a bare scaffold builds there. All real
  `dotnet restore` / `build` / `run` / `test` happens on the user's own machine.

## How to keep building this the same way

- Update `PROGRESS.md`'s phase roadmap, friction log, and companion-doc counts as you go —
  don't let them go stale.
- `concepts.html` — add an entry the moment a concept is genuinely used, not batched up.
- `interview_qa.html` — log a technical-decision question right when a real "why X, not Y"
  choice gets made. Trick questions sparingly (roughly one per three or four technical ones,
  only where there's a real misconception at stake). General/whole-project questions once
  there's enough built to reflect on.
- `walkthrough.html` — add a beat once a phase actually wraps and runs, not before.
- Friction log entries need to be real ("this broke, this is what fixed it"), logged when it
  happens — it's the honest source for "what was hardest" later, and can't be reconstructed
  from memory afterward.
- If continuing with a Claude session that has the `build-to-learn` skill available, just point
  it at this folder — the skill will pick up the same conventions. If it doesn't have the
  skill installed, this file plus `PROGRESS.md` should be enough to keep the same rhythm going
  without it.

## What's already in this folder

```
fleet-reservation-system/
  PROGRESS.md          — live status: phases, friction log, working agreement
  HANDOFF.md            — this file
  SETUP.md              — Phase 1 install/verify steps
  concepts.html         — 2 entries so far (Controllers vs Minimal APIs; EF Core/Postgres naming)
  interview_qa.html     — 3 questions so far (2 technical, 1 trick)
  walkthrough.html      — empty, first beat lands once Phase 1 verification actually runs
  src/
    FleetReservation.slnx
    docker-compose.yml
    FleetReservation.Api/   — scaffolded controller-based Web API, EF Core packages referenced,
                              WeatherForecast sample removed, dev connection string in place
```

## Immediate next step

Work through `SETUP.md`. Once `dotnet run` boots cleanly against the Dockerized Postgres,
Phase 1 is genuinely done — check it off in `PROGRESS.md` — and Phase 3 (modeling the entities
above for real) is next.
