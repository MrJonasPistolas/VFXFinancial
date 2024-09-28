# VFX Financial - João Santos

The goal of this implementation is to replicate a scenario of a .NET backend application that allows the management of foreign exchange rates.
Futhermore, this application needs to perform CRUD operations and also perform communication with a third-party API to fetch all the specific currency pairs that are not stored in the database.

## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (latest version)

Once installed and the application opened on a `IDE`, you need to configure the application for running:

* `src/Web/appsettings.json` apply the connection string and the `ApiKey` for the communication with the third-party API.
* `tests/Application.FunctionalTests/appsettings.json` apply the connection string. 

## Database

The template is configured to use SQL Server by default. You need to apply your desired connection string for database purposes on `appsettings.json` file.

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "SQL Server connection string"
    }
}
```

You need to run the database migrations for the database to be created.

## Technologies

* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/devlooped/moq) & [Respawn](https://github.com/jbogard/Respawn)