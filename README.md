# Forecast Weather App

## Version

v0.0.1 - alpha version

## Requirement

- .NET Core 2.1
- Visual Studio Code
- VueJs 2 with CLI. If not: `npm install -g @vue/cli`

## Installation

At project root:

```cmd
dotnet restore
```

For restoring .NET Core dependencies

```cmd
cd .\src\ForecastWeather.Web

npm install
```

For installing node modules

### Check if it's OK

.NET Core

```cmd
dotnet build

dotnet run --project .\src\ForecastWeather.WebApi\
```

Web

```cmd
cd .\src\ForecastWeather.Web

npm run serve
```
