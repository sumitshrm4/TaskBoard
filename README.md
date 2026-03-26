# TaskBoard

## Overview
TaskBoard is a minimal, well‑layered .NET Core Web API that implements a simple task board (Trello/Jira style) with a clean architecture: Controllers → Services → Repositories → EF Core DbContext. The solution includes an in‑memory default configuration for local development and a test project with NUnit tests that exercise services and repositories.

## Features
Create, read, update, delete tasks with name, description, deadline, favoriting, sort order, and optional attachment URL.

Create and list columns (ToDo, In Progress, Done seeded).

Move tasks between columns and list tasks per column with favorited tasks sorted to the top.

Layered architecture: controllers are thin; business logic lives in services; data access is encapsulated in repositories.

Generic repository plus domain repositories for expressive queries.

Unit and integration style tests using NUnit and EF Core InMemory provider.

Swagger enabled for quick API exploration in development.

## Prerequisites
.NET 8 SDK installed.

Optional for production: SQL Server or Azure SQL if you swap the DbContext provider.

## Recommended IDE: Visual Studio 2022/2023 or VS Code.

## Quick Start
Clone or extract the repository and open the solution TaskBoard.sln in Visual Studio or use the CLI.

## Restore and build:

bash
dotnet restore
dotnet build
Run the API (development uses InMemory DB):

bash
dotnet run --project TaskBoard.Api
Open Swagger UI at the URL printed in the console (e.g., https://localhost:5xxxx/swagger) to explore endpoints.

Run tests:

bash
dotnet test
Project Structure
Top level

TaskBoard.Api — Web API project with controllers, services, repositories, DTOs, models, and EF Core DbContext.

TaskBoard.Tests — NUnit test project referencing the API project.

Key folders in TaskBoard.Api

#### Controllers — 
HTTP endpoints (thin controllers).

#### Services — 
ITaskService, IColumnService, TaskService, ColumnService.

#### Repositories — 
IRepository<T>, GenericRepository<T>, ITaskRepository, IColumnRepository, TaskRepository, ColumnRepository.

#### Data — 
AppDbContext with seeded columns.

#### Models — 
TaskItem, Column.

#### Dtos — 
request DTOs for create operations.

#### Testing CI and Azure Notes
Tests: NUnit tests use EF Core InMemory to validate service and repository behavior. Run dotnet test locally or in CI.
