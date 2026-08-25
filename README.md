# Fleet Reservation System

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![EF Core 10](https://img.shields.io/badge/EF%20Core-10.0-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)
[![PostgreSQL 17](https://img.shields.io/badge/PostgreSQL-17-336791?logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)

A full-stack enterprise vehicle and fleet reservation management system built with **ASP.NET Core 10 Web API**, **Entity Framework Core**, **PostgreSQL 17**, and **Angular**.

This project models real-world enterprise booking workflows, showcasing explicit domain modeling, state machine lifecycle management, multi-condition date-range conflict detection, and role-based access control.

---

## Architecture & Technical Decisions

- **ASP.NET Core 10 Web API (Controllers):** Utilizes controller-based architecture for structured resource grouping, explicit dependency injection, model binding, and filter pipelines suited for long-lived enterprise APIs.
- **EF Core 10 & PostgreSQL 17:** Backed by Npgsql with `EFCore.NamingConventions` (`.UseSnakeCaseNamingConvention()`), bridging C# PascalCase models with idiomatic PostgreSQL `snake_case` identifiers without per-entity annotation overhead.
- **Containerized Infrastructure:** Docker Compose orchestrates a local PostgreSQL 17 instance for deterministic, zero-config onboarding across environments.
- **Single-Project Layered Structure:** Clean folder-based separation (`Controllers/`, `Services/`, `Data/`, `Models/`, `DTOs/`) designed for clear domain boundaries and high development velocity.

---

## Domain Model & Core Business Logic

### Entities & Relationships
- **`Vehicle`**: Fleet inventory tracking (`Id`, `Make`, `Model`, `LicensePlate`, `Status`).
- **`Driver` / `ApplicationUser`**: System actors with role-based permissions (`FleetManager`, `Employee`).
- **`Reservation`**: Vehicle bookings with time intervals (`StartTime`, `EndTime`, `Status`).
- **`MaintenanceWindow`**: Scheduled service intervals taking vehicles out of circulation.

### Business Rules
1. **Dual Conflict Prevention:** A reservation cannot be scheduled if its time range overlaps with:
   - An existing active reservation on the same vehicle.
   - An active maintenance window on the same vehicle.
2. **Reservation Lifecycle State Machine:**
   $$\text{Requested} \longrightarrow \text{Approved} \longrightarrow \text{CheckedOut} \longrightarrow \text{Returned}$$
   *(with paths for `Cancelled` and `Overdue` handling)*.
3. **Role-Based Authorization:**
   - **`FleetManager`**: Register vehicles, schedule maintenance windows, approve/reject reservation requests.
   - **`Employee`**: Request vehicle reservations, check out/in assigned vehicles, view personal booking history.

---

## Interactive Companion Documentation

This repository includes standalone interactive documentation to review decisions, concepts, and technical defense:

- **[`concepts.html`](concepts.html)** — Plain-language explanations and tradeoff comparisons for every major pattern and library choice.
- **[`interview_qa.html`](interview_qa.html)** — Interactive flashcard/quiz interface with interview questions, technical tradeoff defenses, and trick questions.
- **[`walkthrough.html`](walkthrough.html)** — High-level, narrative walkthrough of how each phase was designed and assembled.

---

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (or a local PostgreSQL 17 instance)
- [Node.js](https://nodejs.org/) & [Angular CLI](https://angular.dev/) *(for frontend phase)*

### Local Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/BaraaJadaan/fleet-reservation-system.git
   cd fleet-reservation-system
   ```

2. **Start the database:**
   ```bash
   cd src
   docker compose up -d
   ```

3. **Restore and run the API:**
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project FleetReservation.Api
   ```

4. **Access the API:**
   - OpenAPI endpoint: `http://localhost:5213/openapi/v1.json`

---

## Roadmap

- [x] **Phase 1:** Environment & Tooling Setup (Docker Postgres + .NET 10 SDK)
- [x] **Phase 2:** Core Architecture Decisions (Controllers, EF Core Npgsql, snake_case)
- [ ] **Phase 3:** Domain Modeling & EF Core DbContext Setup
- [ ] **Phase 4:** Core CRUD & Business Logic (State machine & conflict validation)
- [ ] **Phase 5:** Authentication & Role-Based Authorization (JWT)
- [ ] **Phase 6:** Automated Testing (Unit & Integration tests)
- [ ] **Phase 7:** Angular Frontend Client
- [ ] **Phase 8:** Polish & Production Packaging
