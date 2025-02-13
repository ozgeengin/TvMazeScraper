# TVMazeScraper
[![Build](https://github.com/ozgeengin/TvMazeScraper/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ozgeengin/TvMazeScraper/actions/workflows/dotnet.yml)

TVMazeScraper is a .NET based application that fetches TV show data from the [TVMaze API](https://www.tvmaze.com/api) and stores them in it's database. It provides a REST API endpoint to access the stored TV show information.

TVMazeScraper is built using Clean Architecture that promotes separation of concerns by organizing the code into different layers, ensuring maintainability, scalability, and testability. 

<img src="./images/clean-architecture.png" width="400" />

## Technology Stack

TVMazeScraper leverages a variety of modern technologies:

### Core Frameworks & Libraries
- **.NET 9** - High-performance, modern framework for building web applications.
- **ASP.NET Core Web API** - Enables the creation of RESTful services.
- **Entity Framework Core** - ORM for seamless database interactions.

### Database & Persistence
- **SQL Server** - Relational database for storing TV show data.
- **Hangfire** - Background job processing and scheduling.

### Resilience & Object Mapping
- **Polly** - Provides resilience strategies such as retries and circuit breakers.
- **AutoMapper** - Simplifies object-to-object mapping.

### Testing & Fake Data Generation
- **xUnit** - Unit and integration testing framework.
- **FluentAssertions** - Enhances test readability with intuitive assertions.
- **Bogus** - Generates realistic fake data for testing.

## Installation

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- SQL Server (installed and running)

### Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/TVMazeScraper.git
   cd RTL.TvMazeScraper
   ```
2. Install dependencies:
   ```sh
   dotnet restore
   ```
3. Configure the database connection in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "your-sqlserver-connection-string"
   }
   ```
4. Run migrations and update the database:
   ```sh
   dotnet ef database update
   ```
5. Run the Web API application:
   ```sh
   cd src/RTL.TvMazeScraper.WebApi
   dotnet run
   ```
6. Run the Hangfire job scheduler:
   ```sh
   cd src/RTL.TvMazeScraper.SyncProcessor
   dotnet run
   ```

## API Endpoints

### Get TV Shows

- **GET** `api/v1/shows`

- It provides a paginated list of all tv shows containing the id of the TV show and a list of
all the cast that are playing in that TV show. The list of the cast is ordered by birthday descending.

- Example response:

  ```json
  [
    {
      "id": 4,
      "name": "Big Bang Theory",
      "cast": [
        {
          "id": 6,
          "name": "Michael Emerson",
          "birthday": "1950-01-01"
        }
      ]
    }
  ]
  ```

## Testing

### Run Unit Tests

```sh
   dotnet test
```

### Run Integration Tests

```sh
   dotnet test --filter Category=Integration
```