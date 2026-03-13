# Asklepios

A comprehensive system supporting medical facilities in their day-to-day operations. It is a full-stack application that allows you to control and manage medical facilities in everyday activities such as treating patients, system administration, performing tests, and more. The backend is built with **.NET** and the frontend is a GUI created with **Vue 3** and **TypeScript**.

![CI Pipeline](https://github.com/Kamil-Matusz/Asklepios/actions/workflows/ci.yml/badge.svg)

---

## Table of Contents
- [Stack & Technologies](#stack--technologies)
- [Architecture](#architecture)
- [Scope of the Application](#scope-of-the-application)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [Backend](#backend)
  - [Frontend](#frontend)
  - [Running with Docker](#running-with-docker)
  - [Running with Kubernetes](#running-with-kubernetes)
- [Configuration](#configuration)
  - [Database (PostgreSQL)](#database-postgresql)
  - [Redis](#redis)
  - [SendGrid](#sendgrid)
  - [Seq](#seq)
  - [Hangfire](#hangfire)
- [Tests](#tests)
- [Service URLs](#service-urls)
- [Database Diagram](#database-diagram)

---

## Stack & Technologies

**Backend:**
- C# 11 / .NET 7.0
- ASP.NET Core Web API
- Entity Framework Core
- FluentValidation
- JWT Authentication
- Hangfire (background jobs)
- SignalR (real-time communication)
- Health Checks

**Infrastructure:**
- PostgreSQL
- Redis
- Docker / Docker Compose
- Kubernetes (Minikube) / kubectl
- Seq (structured logging)
- SendGrid (email notifications)

**Frontend:**
- Vue 3
- TypeScript
- Vuetify 3
- Pinia
- Vue Router
- Vue I18n
- Vite
- Axios
- SignalR

---

## Architecture

The backend follows a **Clean Architecture** pattern, split into the following projects:

| Project | Description |
|---|---|
| `Asklepios.Api` | REST API layer – controllers, middleware, program entry point |
| `Asklepios.Application` | Application layer – use cases, DTOs, services |
| `Asklepios.Core` | Domain layer – entities, domain logic, interfaces |
| `Asklepios.Infrastructure` | Infrastructure layer – EF Core, repositories, external integrations |
| `Asklepios.Web` | Vue 3 frontend application |
| `Asklepios.Tests` | Unit and integration tests |

---

## Scope of the Application

- **User Registration and Login** – Secure registration and authentication for system users.
- **User Account Generation with Role Assignment** – Automatic creation of user accounts with roles such as doctor, nurse, patient, and administrator.
- **User Access Level Management** – Configuration and enforcement of user-specific access permissions.
- **Administrative Functions** – System management, issue identification and resolution, employee account generation, and role assignment.
- **Storage of Personal and Professional Data** – Secure storage of personal information and medical staff specializations.
- **Patient Management** – Collecting, analyzing, and processing patient data, including treatment history, test results, and surgeries.
- **Integrated Medical Documentation** – Archiving patient data, generating discharge summaries and prescriptions, and reducing paper documentation.
- **Facility Structure Management** – Managing departments, operating room lists, and patient wards.
- **Staff Assignment and Infrastructure Monitoring** – Assigning doctors to departments and tracking resource utilization across the facility.
- **Procedure and Surgery Management** – Planning surgeries and procedures, allocating operating rooms, and scheduling staff.
- **Diagnostic Integration** – Incorporating test results to streamline diagnostic and outpatient processes.
- **Real-Time Communication** – Facilitating the exchange of information and notifications for urgent incidents (via SignalR).
- **Outpatient Clinic Operations** – Patient registration for appointments and efficient visit management.
- **Email Notifications** – Sending reminders for scheduled appointments (via SendGrid).
- **Data Cleanup and Optimization** – Scheduled deletion of unnecessary data (old discharge summaries, appointments) via Hangfire.

---

## Prerequisites

Make sure you have the following installed before running the project:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Node.js](https://nodejs.org/) (v18+) and [Yarn](https://yarnpkg.com/)
- [Docker](https://www.docker.com/) and Docker Compose

---

## Getting Started

### Backend

1. Clone the repository:
   ```bash
   git clone https://github.com/Kamil-Matusz/Asklepios.git
   cd Asklepios
   ```

2. Configure the database connection string in `Asklepios/Asklepios.Api/appsettings.json`:
   ```json
   "Postgres": {
     "connectionString": "Host=localhost;Database=asklepios;Username=postgres;Password=YOUR_PASSWORD"
   }
   ```

3. Apply database migrations:
   ```bash
   cd Asklepios/Asklepios.Api
   dotnet ef database update
   ```

4. Run the API:
   ```bash
   dotnet run --project Asklepios/Asklepios.Api
   ```

### Frontend

1. Navigate to the frontend directory:
   ```bash
   cd Asklepios.Web
   ```

2. Install dependencies:
   ```bash
   yarn install
   ```

3. Start the development server:
   ```bash
   yarn dev
   ```

### Running with Docker

The easiest way to run the entire stack (API, frontend, PostgreSQL, Redis, Seq) is via Docker Compose:

```bash
docker compose up --build
```

This will start all services. See [Service URLs](#service-urls) for the available endpoints.

### Running with Kubernetes

You can also run the application locally using [Minikube](https://minikube.sigs.k8s.io/docs/start/).  
All manifests and helper scripts are located in the [`k8s/`](./k8s/) directory.

**Additional prerequisites:**
- [Minikube](https://minikube.sigs.k8s.io/docs/start/)
- [kubectl](https://kubernetes.io/docs/tasks/tools/)

**Quick start:**

1. Build Docker images locally:
   ```bash
   sh k8s/build-images.sh
   ```

2. Start Minikube, load images and deploy all services:
   ```bash
   sh k8s/start_services.sh
   ```

3. Open the frontend in your browser:
   ```bash
   minikube service asklepios-web
   ```

4. Stop and remove all resources:
   ```bash
   sh k8s/stop_services.sh
   ```

> See [`k8s/README.md`](./k8s/README.md) for full details on the manifests and configuration.

---

## Configuration

### Database (PostgreSQL)

The project uses PostgreSQL deployed as a Docker container. Docker settings are defined in `docker-compose.yaml`.

For local development (without Docker), update the connection string in `Asklepios/Asklepios.Api/appsettings.json`:
```json
"Postgres": {
  "connectionString": "Host=localhost;Database=asklepios;Username=postgres;Password=YOUR_PASSWORD"
}
```

### Redis

Redis is used for caching. The connection is configured in `appsettings.json`:
```json
"Redis": {
  "connectionString": "localhost:6379",
  "instanceName": "Asklepios_"
}
```

When using Docker Compose, Redis is available automatically.

### SendGrid

To enable email notifications, configure your SendGrid API key in `secrets.json`:
```json
{
  "SendGrid": {
    "ApiKey": "YOUR_API_KEY"
  }
}
```

Obtain a free API key at [sendgrid.com](https://sendgrid.com).

### Seq

Seq is used for structured logging. When running via Docker Compose, Seq is available automatically.

For local development, update `appsettings.json`:
```json
"Logging": {
  "Seq": {
    "ServerUrl": "http://localhost:5341"
  }
}
```

Access the Seq web interface at `http://localhost:8081`.

### Hangfire

Hangfire handles background and scheduled jobs (e.g., data cleanup). It is disabled by default.

To enable it, set the following in `appsettings.json`:
```json
"Hangfire": {
  "Enable": true
}
```

---

## Tests

The project includes both unit and integration tests in the `Asklepios.Tests` project.

Run all tests:
```bash
dotnet test Asklepios.sln
```

Run only unit tests:
```bash
dotnet test Asklepios.Tests/UnitTests
```

Run only integration tests:
```bash
dotnet test Asklepios.Tests/IntegrationTests
```

---

## Service URLs

When running via Docker Compose, the following services are available:

| Service | URL |
|---|---|
| Frontend (Web) | http://localhost:8080 |
| Backend (API) | http://localhost:5102 |
| Swagger (API docs) | http://localhost:5102/swagger |
| Health Checks UI | http://localhost:5102/healthchecks-ui |
| Hangfire Dashboard | http://localhost:5102/hangfire |
| Seq (Logs UI) | http://localhost:8081 |
| PostgreSQL | localhost:5432 |
| Redis | localhost:6379 |

---

## Database Diagram

![Database Diagram](/assets/DatabaseDiagram.png)