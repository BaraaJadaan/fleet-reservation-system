# Fleet Reservation System — Progress

## Goal
A full-stack vehicle/fleet reservation system (ASP.NET Core Web API + EF Core/PostgreSQL + Angular) built in ~1 week to get genuinely interview-ready for a backend .NET role on that exact stack (.NET + Postgres + Angular), starting from zero .NET syntax but real backend/full-stack concepts already in hand.

## Why this idea
Reservation domain forces a real state machine (available → reserved → checked out → returned/overdue) plus two distinct kinds of conflict-checking — against other reservations and against maintenance windows — so there's genuine business logic to defend, not just wired-up CRUD. Originally pitched as an equipment-lending system; user liked the technical shape but wanted a different topic, so the domain moved to vehicles/fleet (also beat a strength-tracker angle and a bilingual-content-library angle — the latter had the highest ceiling but the most one-week schedule risk via full-text search tuning).

## Working agreement
- **Local execution required.** This planning session's sandbox can install the .NET SDK and scaffold via `dotnet new`, but can't reach nuget.org — nothing past a bare scaffold builds here. All real `dotnet restore`/`build`/`run`/`test` happens on the user's own machine. See `SETUP.md`.
- **Split of who writes what:** user writes anything genuinely important to understand or plausible interview material — domain entities, business logic (state machine, conflict-check queries), auth/authorization logic, tests. The assistant (this session, or whatever continues it) writes boilerplate and anything unlikely to come up in the interview — project scaffolding, config wiring, DTOs, setup docs. Explanation happens either way, regardless of who types it.
- **This session's output is a handoff.** Files were generated here, then carried into the user's own local coding harness to continue. See `HANDOFF.md` for the full context needed to bring a fresh session up to speed without re-deriving any of this.
- Keep logging concepts/questions/friction the moment they happen — don't batch it for later, even across the session boundary.
- Phase roadmap checkboxes below track *verified working*, not *decided*. Phase 2's decisions are genuinely locked and already reflected in the scaffold and companion docs; it's left unchecked only because it hasn't run on real hardware yet.

## Familiarity calibration
- **Domain/industry:** Intermediate/High — understands core backend concepts, state machines, and relational modeling.
- **Tools/practices:** Low (Learning) — new to C# / .NET 10, EF Core 10, and PostgreSQL provider conventions. Explicitly requests syntax and mechanics explanations.
- **Coding fundamentals:** Intermediate — comfortable with programming concepts, but wants C# syntax, type safety, and SQL translation explained thoroughly.

## Verification agreement
- Retrieval practice during check-ins: Active (default on)
- Wrap-up mock interview: Active (default on)

## Phase roadmap
- [x] Phase 1 — Environment & tooling setup
- [x] Phase 2 — Core architecture decisions
- [ ] Phase 3 — Domain modeling & EF Core setup
- [ ] Phase 4 — Core CRUD & business rules (reservation state machine, conflict checks)
- [ ] Phase 5 — Authentication & authorization
- [ ] Phase 6 — Testing
- [ ] Phase 7 — Angular frontend
- [ ] Phase 8 — Polish & interview-prep consolidation

## Friction log
_Real things that didn't work on the first try, logged the moment they happen — this is the honest source for the "what was hardest" question later. Don't backfill from memory at the end._

_(none yet)_

## Companion docs
- `concepts.html` — 6 entries
- `interview_qa.html` — 6 questions
- `walkthrough.html` — 2 phase beats

## Mock interview weak spots
_Filled in after a live mock interview (SKILL.md section 7) — which questions needed work
and why, so the next mock interview starts there instead of from scratch._

_(none yet)_
