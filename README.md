# SampleApi — Modern .NET 8 REST API (No Swagger)  

A lightweight, modern, clean‑architecture Web API built with **.NET 8**, **ASP.NET Core**, **Azure SQL**, **EF Core**, and **JWT Authentication**. Designed for clarity, testability, and cloud‑ready deployment using **Docker**, **Azure Container Apps**, **Azure DevOps CI/CD**, and **secure secret management**.


A lightweight, modern, clean‑architecture Web API built with **.NET 8**, **ASP.NET Core**, **EF Core InMemory**, and **JWT Authentication**.
Designed for clarity, testability, and minimal dependencies.

---

## ✨ Features

- Modern **.NET 8** hosting model (Minimal Hosting)
- Clean **RESTful** controller structure
- **JWT Authentication** with Bearer tokens
- **EF Core + Azure SQL** (production database)
- **Repository + Service Layer** architecture
- **DTOs + Validation Layer**
- **ServiceResult** pattern for consistent responses
- **Password hashing** for secure credential storage
- **Connection string stored as a secret in Azure Container Apps**
- **Full Azure DevOps CI/CD pipeline** (build → test → Docker → ACR → ACA deploy)
- **Dockerized** application for cloud deployment
- **No Swagger / OpenAPI** (cleaner architecture)
- Fully testable using **Postman**, **Insomnia**, or any REST client

---

## 🛠 Tech Stack

- **.NET 8**
- **ASP.NET Core Web API**
- **Minimal Hosting Model**
- **C# 13**
- **Azure SQL (Production Database)**
- **Azure Container Apps (ACA)**
- **Azure Container Registry (ACR)**
- **Azure DevOps CI/CD Pipeline**
- **Docker (Containerized Deployment)**

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
│   │     │   ├── AuthResponseDTO.cs
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
│   │      └──IPasswordHasher.cs
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
│   │      ├── AuthServices/
│   │      │   ├─── AuthService.cs
│   │      │   ├─── PasswordHasher.cs
│   │      └── MovieService.cs
│   ├───Validators/
│   │      ├── MovieValidator.cs
│   │      └── ValidationResult.cs
│   ├───Program.cs
│   ├───SampleApi.csproj
│   └───Properties/
│          └── launchSettings.json
└── SampleApi.Test/
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

### JWT Authentication
Secures protected endpoints using Bearer tokens.

### 🔐 Password Security (Hashing + Salting)
User credentials in this API are never stored in plaintext.
Passwords are protected using a dedicated Password Hasher service that applies industry‑standard hashing and salting.

How password storage works
Each user receives a unique salt when their password is created.

The plaintext password is combined with the salt.

The combined value is hashed using a deterministic hashing algorithm.

Only the salt and the hashed password are stored in the database.

During login, the same hashing process is repeated and compared to the stored hash.

Why this matters
Plaintext passwords are never persisted or logged.

Salting prevents rainbow‑table attacks.

Hashing ensures passwords cannot be reversed.

Even if the database is compromised, attackers cannot recover original passwords.

Example stored fields
<pre>
Id: seed-user-1
Email: test@example.com
Salt: somesalt
HashedPassword: ef92b778ba5c9c3a5e8f1a9e4f4e8e2b6d5c1f2a3b4c5d6e7f8a9b0c1d2e3f4
</pre>

## 🗄️ Azure SQL Database Integration

This project uses an **Azure SQL Database** as the production data store.  
The connection string is **not stored in the code** — it is securely managed in **Azure Container Apps** as a secret.

### Connection String Secret
In Azure Container Apps, the SQL connection string is stored under the secret name:

defaultconnectionstring

It is injected into the application using an environment variable:

ConnectionStrings__sampleApi = secretref:defaultconnectionstring


EF Core automatically reads this value through the standard `ConnectionStrings` configuration pattern, enabling secure and cloud‑ready database access.


### Minimal Program.cs
Focused, clean, and free of Swagger/OpenAPI dependencies.

## 🧪 Tests

The project includes a dedicated **Tests** folder containing unit tests for the `MovieService`.

### ✔ What is tested

- **GetAll**
  - Returns mapped DTOs
  - Returns empty list

- **GetById**
  - Returns DTO when found
  - Returns null when not found

- **Add**
  - Success path
  - Validation failure
  - Mapping correctness (DTOAdd → Entity → DTORead)

- **Update**
  - Success path
  - Entity not found
  - Validation failure
  - Mapping correctness

- **Delete**
  - Success path
  - Entity not found
  - Repository interaction correctness

### ✔ Tools & Patterns Used

- **Moq** for mocking repository and validator dependencies  
- **Callback capture** to verify mapping correctness  
- **Arrange–Act–Assert** test structure  
- **Repository interaction verification** (`Times.Once`, `Times.Never`)  
- **ServiceResult<T>** success/error assertions  

### ✔ Running the Tests

From the project root: dotnet test

## 🔄 Azure DevOps CI/CD Pipeline

This project includes a full **Azure DevOps CI/CD pipeline** that builds, tests, packages, and deploys the API automatically to **Azure Container Apps**.

### Pipeline Workflow

1. **Restore & Build**
   - Restores NuGet packages
   - Builds the .NET 8 solution

2. **Run Unit Tests + Coverage**
   - Executes all tests in `SampleApi.Test`
   - Generates code coverage reports

3. **Docker Image Build**
   - Builds the API Docker image using the project’s Dockerfile

4. **Push Image to Azure Container Registry (ACR)**
   - Authenticates using Azure DevOps service connection
   - Pushes the built image to your ACR instance

5. **Inject Secrets**
   - The Azure SQL connection string is stored in Azure as a **secret**
   - The pipeline does **not** expose it in logs or YAML
   - ACA reads it via:
     ```
     ConnectionStrings__sampleApi = secretref:defaultconnectionstring
     ```

6. **Deploy to Azure Container Apps**
   - Creates a new ACA revision
   - Applies environment variables
   - Uses the latest image from ACR
   - Ensures zero‑downtime rollout

### Why This Matters

This CI/CD setup mirrors real enterprise workflows:
- No manual deployments  
- No secrets in code  
- Automatic versioning  
- Cloud‑ready, production‑style pipeline  


### 📊 View the Coverage Report
A coverage report is generated in Azure similar to this one:

👉 https://mongolia144.github.io/SampleApi/coverage-report/index.html 
or here
👉 https://mongolia144.github.io/SampleApi/



## 📈 Future Improvements

- Add role‑based authorization
- Add registration
- Add unit tests ( task ongoing).
- Add API versioning

---

## 📜 License

This project is open‑source and free to use for learning or portfolio purposes.
