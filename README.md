# PJATK-APBD-Cw4-s229844

REST API w ASP.NET Core 8 + Entity Framework Core 8 (Code First) do zarzadzania komputerami i komponentami. Cwiczenie 4 APBD, PJATK.

## Wymagania

- .NET 8 SDK
- SQL Server LocalDB (instancja `MSSQLLocalDB`)
- `dotnet-ef` 8.x (`dotnet tool install -g dotnet-ef --version 8.*`)

## Uruchomienie

```powershell
dotnet ef database update --project RestApi.Api
dotnet run --project RestApi.Api
```

Swagger: `https://localhost:5001/swagger` (lub port z `launchSettings.json`).

## Endpointy

- `GET    /api/pcs`
- `GET    /api/pcs/{id}/components`
- `POST   /api/pcs`
- `PUT    /api/pcs/{id}`
- `DELETE /api/pcs/{id}`

## Connection string

Domyslny w `appsettings.json`:

```
Server=(localdb)\MSSQLLocalDB;Database=RestApiDb;Trusted_Connection=True;TrustServerCertificate=True;
```
