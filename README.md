# SampleAPI

A lightweight, modern ASP.NET Core Web API built on **.NET 10**, featuring **Swagger/OpenAPI**, **EF Core InMemory**, and clean, minimal architecture. Designed as a **portfolio‑ready backend demo** showcasing API design, environment configuration, and modern .NET development practices.

---

## 📌 Overview

SampleAPI is a demonstration backend built to highlight:

- Clean ASP.NET Core API structure
- Modern .NET 10 OpenAPI pipeline
- EF Core InMemory database usage
- Environment‑aware Swagger configuration
- Simple, readable Program.cs setup
- Controller‑based routing
- Developer‑friendly API exploration via Swagger UI

This project is intentionally lightweight, making it ideal for portfolio review, interviews, and technical demonstrations.

---

## ✨ Features

- Modern .NET 10 API using the latest ASP.NET Core hosting model
- Swagger/OpenAPI enabled for all environments (demo mode)
- EF Core InMemory database for fast, dependency‑free testing
- Clean Program.cs with minimal boilerplate
- RESTful controller structure
- Zero external dependencies beyond EF Core + Swagger

---

## 🛠 Tech Stack

- .NET 10
- ASP.NET Core Web API
- EF Core InMemory
- Swagger / OpenAPI
- C# 13
- Minimal Hosting Model

---

## 📁 Project Structure

<pre>
SampleAPI/
│
├── Controllers/
│   └── MoviesController.cs
├── Data/
│   └── AppDbContext.cs
├── DTO/
│   └── MovieDTOAdd.cs
│   └── MovieDTORead.cs
│   └── MovieDTOUpdate.cs
├── Interfaces/
│   └── IMovieRepositiory.cs
│   └── IMovieService.cs
│   └── IMovieValidator.cs
├── Mappings/
│   └── MovieMaping.cs
├── Models/
│   └── Movie.cs
├── Repositories/
│   └── MovieRepository.cs
├── Results/
│   └── ServiceResults.cs
├── Services/
│   └── MovieService.cs
├── Validators/
│   └── MovieValidator.cs
│   └── ValidationResult.cs
├── Program.cs
├── SampleAPI.csproj
└── Properties/
    └── launchSettings.json
</pre>

---

## 🚀 Running the Project

### 1. Restore packages
dotnet restore

### 2. Run the API
dotnet run

### 3. Open Swagger UI

Check the console output for the port, then open:

http://localhost:<port>/swagger/index.html

Swagger UI will display all available endpoints.

Ex: http://localhost:5138/swagger/index.html

---

## 🔍 API Documentation (Swagger)

SampleAPI uses the modern .NET 10 OpenAPI pipeline, which means:

- No `Microsoft.OpenApi.Models`
- No `OpenApiInfo`
- Swagger is generated automatically
- UI is available at `/swagger`

Swagger is intentionally enabled for all environments to make the API easy to explore during portfolio review.

---

## 📡 Example Endpoints

Once you add controllers, they will automatically appear in Swagger.

GET /api/items  
POST /api/items  
GET /api/items/{id}  
DELETE /api/items/{id}

---

## 🧠 Design Decisions

- Repository Pattern added to isolate data access logic and keep persistence concerns out of controllers.
- Service Layer introduced to encapsulate business logic and orchestrate operations between controller and repository.
- Dependency Injection used throughout the application to register repositories, services, and validators, ensuring loose coupling and testability.
- DTOs added to safely exchange data between controller and service without exposing domain models
- Validation Layer added to enforce business rules using entity validators and service‑level result objects.
- ServiceResult pattern adopted to standardize service responses, ensuring controllers receive structured success flags, data payloads, and validation errors instead of raw DTOs or entities.
- EF Core InMemory chosen for simplicity and zero setup during development.
- Swagger enabled only in Development to avoid exposing API metadata in Production.
- Minimal Program.cs kept to highlight clarity over complexity.
- No external database to keep onboarding friction low.
- Modern OpenAPI pipeline aligned with .NET 10 best practices.


---

## 📈 Future Improvements

- Add validation
- Add a real database (SQL Server or PostgreSQL)
- Add authentication (JWT)
- Add unit tests
- Add CI/CD pipeline
- Add versioned API endpoints

---

## 📜 License

This project is open‑source and free to use for learning or portfolio purposes.
