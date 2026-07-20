# SampleAPI — Modern .NET 10 REST API (No Swagger)  

A lightweight, modern, clean‑architecture Web API built with **.NET 10**, **ASP.NET Core**, **EF Core InMemory**, and **JWT Authentication**.
Designed for clarity, testability, and minimal dependencies.

---

## ✨ Features

- Modern **.NET 10** hosting model
- Clean **RESTful** controller structure
- **JWT Authentication** with Bearer tokens
- **EF Core InMemory** database (zero setup)
- **Repository + Service** Layer architecture
- **DTOs + Validation Layer**
- **ServiceResult pattern** for consistent responses
- Minimal Program.cs
- **No Swagger / OpenAPI** (removed for cleaner architecture)
- Fully testable using **Postman, Insomnia**, or any REST client

---

## 🛠 Tech Stack

- **.NET 10**
- **ASP.NET Core Web API**
- **EF Core InMemory**
- **Swagger / OpenAPI**
- **C# 13**
- **Minimal Hosting Model**

---

## 📁 Project Structure

<pre>
SampleApi/
├── SampleApi/
│   ├──Controllers/
│   │     ├── AuthController.cs
│   │     └── MoviesController.cs
│   ├── Data/
│   │     └── AppDbContext.cs
├   ├── DTO/
│   │     ├── Auth/
│   │     │    └──AuthResponseDTO.cs
│   │     ├── LoginDTO.cs
│   │     ├── MovieDTOAdd.cs
│   │     ├── MovieDTOAdd.cs
│   │     ├── MovieDTORead.cs
│   │     └── MovieDTOUpdate.cs
│   ├───Interfaces/
│   │      ├──IAuthService.cs
│   │      ├──IMovieRepositiory.cs
│   │      ├──IMovieService.cs
│   │      ├──IMovieValidator.cs
│   │      └──IUserRepository.cs
│   ├───Mappings/
│   │      └── MovieMaping.cs
│   ├───Models/
│   │      ├── Movie.cs
│   │      └── User.cs
│   ├───Repositories/
│   │      ├── MovieRepository.cs
│   │      └── UserRepository.cs
│   ├───Results/
│   │      └── ServiceResults.cs
│   ├───Services/
│   │      ├── AuthServices
│   │      │    └── AuthService.cs
│   │      └── MovieService.cs
│   ├───Validators/
│   │      ├── MovieValidator.cs
│   │      └── ValidationResult.cs
│   ├───Program.cs
│   ├───SampleAPI.csproj
│   └───Properties/
│          └── launchSettings.json
└── SampleAPI.Api/
</pre>

---

## 🚀 Running the Project

### 1. Restore packages
dotnet restore

### 2. Run the API
dotnet run

### 3. API Base URL

Check the console output for the port, then open:

http://localhost:<port>

---

## 🔐 Authentication (JWT)

Swagger has been removed, so authentication is performed using Postman or similar tools.

## 🔐 Register the JWT Signing Key (Required)
Before running the API, you must configure the JWT signing key using .NET User Secrets.
This keeps sensitive values out of source control and ensures each developer can use their own local key.

### 🗝️ 1. Navigate to the project folder
Run this from the terminal, pointing to the folder containing your .csproj file:

<pre>
cd YourProject.Api
</pre>
Ex:
<pre>
cd SampleApi
</pre>

### 🗝️ 2. Initialize User Secrets (only needed once)
<pre>
dotnet user-secrets init
</pre>
This links a secure local secrets store to your project.

### 🗝️ 3. Add the JWT signing key
<pre>
dotnet user-secrets set "Jwt:Key" "your-super-secret-key-here"
</pre>
Use any long random string. Example:

<pre>
dotnet user-secrets set "Jwt:Key" "A9F3C1D8-SECRET-KEY-XYZ-2026"
</pre>
### 🗝️ 4. (Optional) Add issuer and audience
<pre>
dotnet user-secrets set "Jwt:Issuer" "YourApi"
dotnet user-secrets set "Jwt:Audience" "YourApiClient"
</pre>
### 🗝️ 5. Verify the stored secrets
<pre>
dotnet user-secrets list
</pre>
Expected output:

Code
Jwt:Key = your-super-secret-key-here
Jwt:Issuer = YourApi
Jwt:Audience = YourApiClient
### 🔧 How the API reads these values
Your Program.cs should contain:

<pre>
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings.GetValue<string>("Key")
    ?? throw new Exception("JWT Key is missing in configuration");

var issuer = jwtSettings.GetValue<string>("Issuer")
    ?? throw new Exception("JWT Issuer is missing in configuration");

var audience = jwtSettings.GetValue<string>("Audience")
    ?? throw new Exception("JWT Audience is missing in configuration");
</pre> 

These values come from User Secrets, not from appsettings.json.

### 1. Login to obtain a JWT token

POST: http://localhost:5138/auth/login
- Body (JSON):
{
  "Email": "test@example.com",
  "Password": "password123"
}
- Response: 
{
  "token": "<your JWT token>"
}

### 2. 🔑 Using the JWT Token in Postman

Add this header to any protected request:
Authorization: Bearer <your token>
No quotes around the token.

## 🎬 Movies API Endpoints
All movie endpoints require a valid JWT token.

### 1. ➕ Create a Movie

- POST: http://localhost:5138/api/movies
- Headers:
Authorization: Bearer <token>
Content-Type: application/json
- Body:
{
  "title": "Inception",
  "year": 2010
}

### 2. 📄 Get All Movies
GET: http://localhost:5138/api/movies

### 3. 🔍 Get Movie by ID
GET: http://localhost:5138/api/movies/{id}

### 4. ✏️ Update Movie
PUT: http://localhost:5138/api/movies/{id}
- Headers:
Authorization: Bearer <token>
Content-Type: application/json
- Body:
{
  "title": "Matrix",
  "year": 1999
}

### ❌ Delete Movie
http://localhost:5138/api/movies/{id}
- Headers:
Authorization: Bearer <token>
Content-Type: application/json

## Architecture Overview


### Repository Pattern
Keeps persistence logic isolated and testable.

### Service Layer
Encapsulates business logic and orchestrates operations.

### DTOs
Prevent leaking domain models to API consumers.

### Validation Layer
Ensures business rules are enforced consistently.

### ServiceResult Pattern
Standardizes service responses:
Success
Data
Errors

### EF Core InMemory
Perfect for development and testing without external dependencies.

### JWT Authentication
Secures protected endpoints using Bearer tokens.

### Minimal Program.cs
Focused, clean, and free of Swagger/OpenAPI dependencies.


## 📈 Future Improvements

- Add a real database (SQL Server / PostgreSQL)
- Add role‑based authorization
- Add password hashing + registration
- Add unit tests
- Add CI/CD pipeline
- Add API versioning

---

## 📜 License

This project is open‑source and free to use for learning or portfolio purposes.
