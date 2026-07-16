SampleAPI
A lightweight, modern ASP.NET Core Web API built on .NET 10, featuring Swagger/OpenAPI, EF Core InMemory, and clean, minimal architecture. Designed as a portfolio‑ready backend demo showcasing API design, environment configuration, and modern .NET development practices.

📌 Overview
SampleAPI is a demonstration backend built to highlight:

Clean ASP.NET Core API structure

Modern .NET 10 OpenAPI pipeline

EF Core InMemory database usage

Environment‑aware Swagger configuration

Simple, readable Program.cs setup

Controller‑based routing

Developer‑friendly API exploration via Swagger UI

This project is intentionally lightweight, making it ideal for portfolio review, interviews, and technical demonstrations.

✨ Features
Modern .NET 10 API using the latest ASP.NET Core hosting model

Swagger/OpenAPI enabled for all environments (demo mode)

EF Core InMemory database for fast, dependency‑free testing

Clean Program.cs with minimal boilerplate

RESTful controller structure

Zero external dependencies beyond EF Core + Swagger

🛠 Tech Stack
.NET 10

ASP.NET Core Web API

EF Core InMemory

Swagger / OpenAPI

C# 13

Minimal Hosting Model

📁 Project Structure
Code
SampleAPI/
│
├── Controllers/
│   └── YourControllersHere.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Program.cs
├── SampleAPI.csproj
└── Properties/
    └── launchSettings.json
🚀 Running the Project
1. Restore packages
Code
dotnet restore
2. Run the API
Code
dotnet run
3. Open Swagger UI
Check the console output for the port, then open:

Code
https://localhost:<port>/swagger
Swagger UI will display all available endpoints.

🔍 API Documentation (Swagger)
SampleAPI uses the modern .NET 10 OpenAPI pipeline, which means:

No Microsoft.OpenApi.Models

No OpenApiInfo

Swagger is generated automatically

UI is available at /swagger

Swagger is intentionally enabled for all environments to make the API easy to explore during portfolio review.

📡 Example Endpoints
Once you add controllers, they will automatically appear in Swagger.

Example:

Code
GET /api/items
POST /api/items
GET /api/items/{id}
DELETE /api/items/{id}
Swagger will generate documentation and test forms for each endpoint.

🧠 Design Decisions
EF Core InMemory chosen for simplicity and zero setup

Swagger enabled globally because this is a demo project

Minimal Program.cs to highlight clarity over complexity

No external database to keep onboarding friction low

Modern OpenAPI pipeline to align with .NET 10 best practices

📈 Future Improvements
Add DTOs and validation

Add a real database (SQL Server or PostgreSQL)

Add authentication (JWT)

Add unit tests

Add CI/CD pipeline

Add versioned API endpoints

📜 License
This project is open‑source and free to use for learning or portfolio purposes.