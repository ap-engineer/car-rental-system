# Car Rental Service

A fullstack car rental management system built with .NET 9 (Web API) and React (TypeScript).  
The project follows a layered architecture separating domain logic, application services, infrastructure persistence, and the presentation layer.

---

## Project Structure


```
RentalService/
├── src/
├── Api/                  → ASP.NET Core Web API project
├── Application/          → Business use cases, DTOs, services
├── Domain/               → Core entities, logic
├── Infrastructure/       → EF Core + SQLite implementation
└── Tests/                → unit tests
```

---

## Backend

### Overview
The backend exposes a REST API for managing car rentals, including registration, pickup, return, and listing of all rentals.  
It uses Entity Framework Core with SQLite as a lightweight persistent store and implements a Domain-Driven Design approach.

### Technologies
- .NET 9 Web API
- Entity Framework Core
- SQLite
- Dependency Injection
- Repository and Service pattern
- Swagger (OpenAPI)

### Key Components
- **Domain Layer**: Contains entities such as `Rental` and `CarCategory`, encapsulating business rules.
- **Application Layer**: Contains `IRentalService` and related DTOs for interaction between controllers and domain.
- **Infrastructure Layer**: Provides EF Core `DbContext`, entity mappings, repositories, and a design-time factory.
- **API Layer**: Provides endpoints for all operations through `RentalsController`.

### API Endpoints
- `POST /api/rentals/pickup` – Registers a new rental pickup.
- `POST /api/rentals/return` – Registers a rental return.
- `GET /api/rentals` – Lists all registered rentals.

### Data Flow
1. The controller receives HTTP requests and delegates to `IRentalService`.
2. The service performs validation, domain logic, and calls the repository.
3. The repository uses EF Core to persist and retrieve data.
4. Responses are returned as typed DTOs.

### Database
The application uses a local SQLite database (`car-rental.db`) generated in .Api folder.
Migrations can be added and updated using:
```bash
dotnet ef migrations add ***
dotnet ef database update
```
---
## Frontend

### Overview
The frontend is a React + TypeScript single-page application that communicates with the backend API.
It allows users to register pickups, register returns, and view all existing rentals in a responsive interface.

### Technologies
- React with TypeScript
- Vite
- Axios
- Tailwind
- MUI

```
src/
├── api/           → API service (Axios)
├── components/    → UI components
├── pages/         → Page-level containers
├── types/         → TypeScript DTO definitions
├── App.tsx        → Root component
├── main.tsx       → Entry point
└── index.css      → Tailwind and base styles
```

### Running the Frontend
```bash
cd src/Client
npm install
npm run dev
```


## Development Setup

### Prerequisites
- .NET 9 SDK
- Node.js 18+
- SQLite

### Quick Start
1. **Backend**:
   ```bash
   cd src/Server/RentalService.Api
   dotnet restore
   dotnet run
   ```

2. **Frontend**:
   ```bash
   cd src/Client
   npm install
   npm run dev
   ```

3. **Access**:
    - API: http://localhost:5000
    - Swagger: http://localhost:5000/swagger/index.html
    - Frontend: http://localhost:5173

## Testing

The system includes comprehensive unit tests covering:
- Model validation with DataAnnotations
- Business rule validation
- Price calculation for all car categories
- Configuration-aware testing
- Edge cases and error scenarios

Run tests:
```bash
cd src/Server/RentalService.Tests
dotnet test
```