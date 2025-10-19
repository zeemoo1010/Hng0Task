# Hng0Task — Project Summary

Short description
- Minimal ASP.NET Core Web API (Target: .NET 9, C# 13) that exposes a single endpoint `/me` and returns a `Profile` JSON object.
- `Profile` is built from static user info and a cat fact fetched from `https://catfact.ninja/fact`.

Flow (concise)
1. Client sends GET /me.
2. `ProfileController` handles the request and calls `ProfileService.GetProfileAsync()`.
3. `ProfileService` performs an HTTP GET to the external API, extracts the `fact` value (or uses a fallback).
4. `ProfileService` constructs a `Profile` (Status, User, Timestamp, Fact).
5. Controller returns `200 OK` with the `Profile` serialized to JSON.

Key files
- `Program.cs` — registers controllers, configures middleware (`UseHttpsRedirection`, `UseAuthorization`, `MapControllers`).
- `Controllers/ProfileController.cs` — GET `/me` endpoint.
- `Service/ProfileService.cs` — fetches the cat fact and builds the `Profile`.
- `Entity/Profile.cs` — model definition for the returned JSON.
- `Dockerfile` — containerization (present in repo root).

Run locally
- .NET CLI:
  - From project folder: `dotnet run`
- Visual Studio:
  - Open the solution and use __Debug > Start Debugging__ or __Debug > Start Without Debugging__.

Example request
- curl:
  - `curl -s https://localhost:5001/me`
- Example response (trimmed):
  - {
      "status":"success",
      "user":{ "email":"ismailagboola130@gmail.com", "name":"Ibrahim Ismail", "stack":"C#/ASP.NET Core" },
      "timestamp":"2025-10-19T12:34:56.789Z",
      "fact":"A cat fact..."
    }

Quick notes & recommended improvements
- Current issues:
  - `ProfileController` creates `ProfileService` with `new` instead of using DI.
  - `ProfileService` creates a raw `HttpClient` (risk of socket exhaustion).
- Recommendations:
  - Register `ProfileService` in DI and inject it in the controller (use `builder.Services.AddScoped<IProfileService, ProfileService>()` or `AddHttpClient<ProfileService>()`).
  - Use `IHttpClientFactory` (via `AddHttpClient`) for `HttpClient` instances.
  - Add `ILogger<T>`, configuration for external API URL/timeouts, and better error handling.
  - Add unit tests for `ProfileService` by mocking HTTP calls.

This summary is intentionally concise; if a more detailed README (with examples, badges, API contract, or CI instructions) is required, a longer version can be generated.