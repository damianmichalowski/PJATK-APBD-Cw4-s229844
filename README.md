# PJATK-APBD-Cw4-s229844

REST API na ASP.NET - Komputery i komponenty. Ćwiczenie 4, APBD PJATK.

## Uruchomienie

```powershell
dotnet ef database update --project RestApi.Api
dotnet run --project RestApi.Api
```

Swagger: `http://localhost:5072/swagger`

## Endpointy

- `GET    /api/pcs`
- `GET    /api/pcs/{id}/components`
- `POST   /api/pcs`
- `PUT    /api/pcs/{id}`
- `DELETE /api/pcs/{id}`
