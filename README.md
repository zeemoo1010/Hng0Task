# Hng0Task

Minimal ASP.NET Core Web API (Target: .NET 9, C# 13) that exposes a single endpoint to return a user `Profile` assembled from static user data and a cat fact fetched from `https://catfact.ninja/fact`.

## Project at a glance
- Target framework: `.NET 9`
- Language version: `C# 13`
- Pattern: Controller -> Service -> External HTTP API
- Single public endpoint: `GET /me`

## Repository layout
- `Program.cs` — application bootstrap, service registration and middleware (`UseHttpsRedirection`, `UseAuthorization`, `MapControllers`).
- `Controllers/ProfileController.cs` — defines the `GET /me` endpoint and returns a `Profile`.
- `Service/ProfileService.cs` — fetches an external cat fact and builds the `Profile` model.
- `Entity/Profile.cs` — `Profile` model (properties: `Status`, `User`, `Timestamp`, `Fact`).
- `appsettings.json` — configuration (API URLs, timeouts, etc. — present in workspace).
- `Dockerfile` — containerization instructions (root).
- `README.md` — this file.

## High-level flow

## How it works (step-by-step)
1. Client calls `GET /me`.
2. `ProfileController.GetProfile()` invokes `ProfileService.GetProfileAsync()`.
3. `ProfileService` performs an HTTP GET to `https://catfact.ninja/fact`. If successful it reads the `fact` field; otherwise a fallback message is used.
4. A `Profile` object is created:
   - `Status` = `"success"`
   - `User` = hard-coded contact info (`Email`, `Name`, `Stack`)
   - `Timestamp` = UTC timestamp in ISO format
   - `Fact` = fetched cat fact (or fallback)
5. Controller returns `200 OK` with the serialized JSON `Profile`.

## Run locally

Using .NET CLI
- From project folder:
  - `dotnet run`
- The console will show the listening URL (eg `https://localhost:5001`).

Using Visual Studio
- Open the solution and run via __Debug > Start Debugging__ or __Debug > Start Without Debugging__.

Test the endpoint
- Example:
  - `curl -s https://localhost:5001/me`
- Sample response:

## Docker
- Build: `docker build -t hng0task .`
- Run: `docker run -p 5000:80 -e ASPNETCORE_ENVIRONMENT=Production hng0task`

(Adjust ports and environment variables to match your `Program.cs` and container port configuration.)

## Configuration
- Keep external API URLs, timeouts, and other settings in `appsettings.json` (or environment variables) rather than hard-coding in `ProfileService`.

## Shortcomings & recommended improvements
- Current issues:
- `ProfileController` constructs `ProfileService` with `new` instead of using DI.
- `ProfileService` instantiates `HttpClient` directly (risk of socket exhaustion).
- Recommendations:
- Introduce an interface (`IProfileService`) and register service in DI:
  - `builder.Services.AddScoped<IProfileService, ProfileService>();`
- Use `IHttpClientFactory`:
  - `builder.Services.AddHttpClient<ProfileService>();`
- Add `ILogger<T>` to services for structured logging.
- Move external API URL to configuration (`appsettings.json`) and use typed options.
- Add unit tests for `ProfileService` (mock `HttpMessageHandler`) and controller tests (use `WebApplicationFactory<T>`).
- Add health checks if used in production.

## Contributing
- Open a branch per feature: `git checkout -b feat/your-feature`
- Create PR to branch `Zeemo` (current repo branch).
- Add unit tests for behaviour change.

## License & Contact
- See repository root for license (if present).
- Maintainer email (from code): `ismailagboola130@gmail.com`

If you want, I can:
- Refactor the project to use DI and `IHttpClientFactory`.
- Add unit tests and a health-check endpoint.
- 
