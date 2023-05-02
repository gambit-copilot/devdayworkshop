# Dev Day workshop

![TechItEasy logo](https://raw.githubusercontent.com/gambit-copilot/devdayworkshop/main/TechItEasy%20logo.png)

This repository contains a .NET Web API backend for the imaginary electronics store **Tech IT Easy**. Its purpose is only to serve as training material.

There are endpoints for:

- Customers
- Products
- Orders

Open the solution in Visual Studio or in VS Code. A local SQLite database is used for storage. Before running it the first time, the database needs to be initialized by executing the following commands in a PowerShell console:

    dotnet tool install dotnet-ef --global
    dotnet ef database update

When running the solution, a web browser window will open with the Swagger UI where you can experiment with calls to the endpoints.

