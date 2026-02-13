# SonarCloud SAST Lab (.NET) - Lab 4.4

This repository is an intentionally insecure training repo for practicing:
- SonarQube Cloud (SonarCloud) CI-based analysis
- Pull Request (PR) decoration
- Quality Gates and "Clean as You Code"
- Fixing security issues found by SAST (taint, hotspots, secrets, crypto)

## Repo layout

```
/src
  /OrdersWeb          ASP.NET Core (.NET 8) Razor Pages + Minimal APIs
  /OrdersSecurity     Class library (password hashing demo)
/tests
  /OrdersWeb.Tests    xUnit tests + OpenCover coverage output
/.github/workflows
  sonarcloud.yml      SonarScanner for .NET (begin -> build/test -> end)
```

## Intentionally insecure areas

| Category | File |
|---|---|
| SQL Injection | src/OrdersWeb/Data/OrdersRepository.cs |
| Reflected XSS | src/OrdersWeb/Pages/XssDemo.cshtml |
| Path Traversal | src/OrdersWeb/SecurityDemos/FilesDemoService.cs |
| Insecure deserialization | src/OrdersWeb/SecurityDemos/InsecureJson.cs |
| Hardcoded secret | src/OrdersWeb/Program.cs |
| Weak password hashing | src/OrdersSecurity/PasswordHashing.cs |
| TLS cert validation bypass | src/OrdersWeb/SecurityDemos/InsecureHttpClientFactory.cs |

> WARNING: Do not deploy this code anywhere public. It is insecure by design.

## Local run (optional)

```bash
dotnet restore
dotnet build SonarLab.sln
dotnet test SonarLab.sln
dotnet run --project src/OrdersWeb
```

## CI / SonarCloud

The GitHub Actions workflow expects:
- Secret: `SONAR_TOKEN`
- Variables: `SONAR_ORG`, `SONAR_PROJECT_KEY`

See the Student Lab PDF for step-by-step setup instructions.
