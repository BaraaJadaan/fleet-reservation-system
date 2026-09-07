# Setup — Fleet Reservation System

Full from-scratch environment setup (Phase 1). Written assuming Windows, since that's the
likely target given your hardware — Mac/Linux notes are inline where they differ. If you're
not on Windows, skip straight to those notes in each section.

## 1. .NET 10 SDK

**Windows:** `winget install Microsoft.DotNet.SDK.10` — or grab the installer from
https://dotnet.microsoft.com/download if you don't have winget.

**Mac:** `brew install --cask dotnet-sdk`, or the installer from the same download page.

**Linux:** Either the official install script from the same page, or your distro's package
manager — Ubuntu's own repos carry `dotnet-sdk-10.0` (confirmed while building this).

Verify:
```
dotnet --version
```
Should print something starting with `10.`.

## 2. An IDE

Pick one:
- **VS Code + "C# Dev Kit" extension** — free, fast, cross-platform. Install VS Code, then
  install "C# Dev Kit" from the Extensions panel (pulls in the base C# extension too).
  Good default if you don't already have a strong preference.
- **Visual Studio Community** (Windows only, free) — heavier install, but it's the actual
  standard editor at a lot of .NET shops, so there's real value in being comfortable with it
  regardless of what you use day to day.
- **JetBrains Rider** (paid, free trial) — worth it if you already like JetBrains tooling;
  probably the best EF Core / debugging experience of the three.

## 3. Postgres, via Docker

Install Docker Desktop (Windows/Mac) or Docker Engine (Linux). Then, from this project's
root folder:
```
docker compose up -d
```
This starts a Postgres 17 container that already matches the connection string sitting in
`src/FleetReservation.Api/appsettings.Development.json` (db `fleet_reservation`, user
`fleet_dev`). Verify with `docker ps` — you should see `fleet-reservation-db` running.

Don't want Docker? Install Postgres natively and either create a database/user matching those
same values, or edit the connection string in `appsettings.Development.json` to match whatever
you set up.

## 4. Node.js + Angular CLI (for Phase 7 — not needed yet, but might as well set it up now)

Install Node.js LTS from https://nodejs.org (or via `nvm` if you like managing versions).
Then:
```
npm install -g @angular/cli
ng version
```

## 5. First verification run

From `src/`:
```
dotnet restore
dotnet build
dotnet run --project FleetReservation.Api
```
It should print a `localhost` URL. Hit it (or check the console output for the OpenAPI/Swagger
UI link) to confirm the API actually boots. This is the first real checkpoint — if `dotnet
restore` throws anything, that's genuine friction, not a sign you did something wrong. Log it
in `PROGRESS.md`'s friction log with what the error said and what fixed it, rather than
smoothing over it once it's resolved.

## Known heads-up

There's an open upstream issue where `dotnet ef database update` can throw an
`ObjectDisposedException` on some EF Core 10 + Npgsql 10.0.x version combinations. If you hit
exactly that exception once migrations enter the picture (Phase 3), it isn't something you did
wrong — check whether a newer `Npgsql.EntityFrameworkCore.PostgreSQL` patch release fixes it
before assuming your own code is at fault.

## Once all five steps check out

You're through Phase 1. Move to `HANDOFF.md` for the full plan and Phase 3 (domain modeling).
