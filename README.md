## General Info

- <b> Objective: </b> Development of a .NET 8 WebAPI using ASP.NET Core for a quick reference when building future projects and highlighting important framework features.

## Features of this project
- Usage of 
- Custom Validations;
- Custom Filters.

## How to start a .NET 8 WebAPI project

1. Make sure .NET 8 SDK is installed in your OS. Instructions can be found on the [Microsoft Downloads Page](https://dotnet.microsoft.com/en-us/download/dotnet/8.0);

2. Open a command prompt and run `dotnet new webapi --o ProjectName`;

3. Alternatively, you can:
   - Create a new WebAPI .NET project on Visual Studio;
   - Download the C# Dev Extension Kit on VS Code and execute the command ".NET New Project".

## Executing migrations

1. Make sure the EF Core Tools package is installed:

```
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
dotnet ef
```

2. Create a migration script:

```
dotnet ef migrations add <name>
```

3. Run migration script:

```
dotnet ef migrations update
```

4. To remove a migration script, use:

```
dotnet ef migrations remove <name>
```

5. Alternatively, you can use the package manager console on Visual Studio:

```
add-migration <name>
remove-migration <name>
update-database
```

## Run Project

1. Make sure you have a local MySQL Database running. Instructions can be found [on this link](https://dev.mysql.com/doc/mysql-getting-started/en/);

2. Define the proper connection string for the database on `appsettings.json` and `appsettings.Development.json`;

3. Run the project (make sure the CLI path is on the project folder):

```
dotnet run
```

## Links and References

- This project was based on the [Web API ASP.NET Core Essential (.NET 8 / .NET 9) Course](https://compassuol.udemy.com/course/curso-web-api-asp-net-core-essencial/) by Jose Carlos Macoratti on Udemy;

- [DOTNET CLI Documentation](https://learn.microsoft.com/en-us/dotnet/core/tools);
