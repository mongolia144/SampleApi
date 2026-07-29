# 🚀 SampleApi — Modern .NET 10 REST API (Azure‑Ready, Dockerized, SQL‑Backed)

A clean‑architecture **.NET 10 Web API** deployed using a full Azure cloud pipeline:

- **Azure Container Registry (ACR)** for image storage  
- **Azure Container Apps (ACA)** for hosting  
- **Azure SQL** as the production database  
- **Azure DevOps Pipelines** for CI/CD  
- **Docker** for containerized builds  
- **JWT Authentication** for secure access  

This project is designed as a **portfolio‑ready, production‑style API** showcasing modern cloud deployment practices.

---

## ✨ Features

- Modern **.NET 10** hosting model  
- Clean **RESTful** controller structure  
- **JWT Authentication** with Bearer tokens  
- **EF Core + Azure SQL** (replaces InMemory)  
- **Repository + Service Layer** architecture  
- **DTOs + Validation Layer**  
- **ServiceResult pattern** for consistent responses  
- **Password hashing + salting**  
- **Dockerfile included**  
- **Azure DevOps CI/CD pipeline**  
- **Azure Container Apps deployment**  
- **No Swagger** (cleaner architecture, Postman‑friendly)

---

## 🛠 Cloud Architecture Overview

### **Azure Container Registry (ACR)**
Stores the Docker image built by the Azure DevOps pipeline.

### **Azure Container Apps (ACA)**
Runs the API using revisions, secrets, and environment variables.

### **Azure SQL**
Production database replacing EF Core InMemory.

### **Azure DevOps Pipeline**
Builds → Tests → Coverage → Docker Build → Push → ACA Deploy  
Includes secret injection and environment variable binding.

---

## 🛠 Tech Stack

- **.NET 10**
- **ASP.NET Core Web API**
- **EF Core + Azure SQL**
- **C# 13**
- **Docker**
- **Azure DevOps**
- **Azure Container Apps**
- **Azure Container Registry**

---

## 📁 Project Structure

```
SampleApi/
├── SampleApi/
│   ├── Controllers/
│   ├── Data/
│   ├── DTO/
│   ├── Interfaces/
│   ├── Mappings/
│   ├── Models/
│   ├── Repositories/
│   ├── Results/
│   ├── Services/
│   ├── Validators/
│   ├── Program.cs
│   └── SampleApi.csproj
└── SampleApi.Test/
```

---

## 🧪 Running Locally

### 1. Restore packages  
```
dotnet restore
```

### 2. Run the API  
```
dotnet run
```

### 3. Local URL  
```
http://localhost:<port>
```

---

## 🔐 Authentication (JWT)

Authentication is performed using Postman or any REST client.

### Configure JWT Signing Key (User Secrets)

```
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "your-secret-key"
dotnet user-secrets set "Jwt:Issuer" "SampleApi"
dotnet user-secrets set "Jwt:Audience" "SampleApiClient"
```

---

## 🔐 Azure SQL Connection String

In Azure Container Apps, the connection string is stored as a **secret**:

```
defaultconnectionstring
```

And injected into the app via environment variable:

```
ConnectionStrings__sampleApi = secretref:defaultconnectionstring
```

EF Core automatically picks this up using the standard configuration pattern.

---

## 🎬 Movies API Endpoints

All movie endpoints require a valid JWT token.

- **POST** `/api/movies`  
- **GET** `/api/movies`  
- **GET** `/api/movies/{id}`  
- **PUT** `/api/movies/{id}`  
- **DELETE** `/api/movies/{id}`  

---

## 🧪 Tests

Unit tests cover:

- MovieService logic  
- Validation  
- Mapping  
- Repository interactions  
- ServiceResult patterns  

Run tests:

```
dotnet test
```

---

## 🔄 CI/CD Pipeline (Azure DevOps)

The pipeline performs:

- Build  
- Test + Coverage  
- Docker image build  
- Push to ACR  
- Inject SQL connection string secret  
- Bind secret to environment variable  
- Deploy new ACA revision  

This mirrors real enterprise deployment workflows.

---

## 📈 Future Improvements

- Role‑based authorization  
- Registration endpoint  
- API versioning  
- More unit tests  
- Health checks  
- Logging + Application Insights  

---

## 📜 License

Open‑source for learning and portfolio use.
