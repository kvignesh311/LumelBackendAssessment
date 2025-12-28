# Sales Analytics Backend

This project is a backend service built as part of a Lumel backend assessment.  
It loads sales data from a CSV file into a SQL Server database and exposes APIs to perform basic analytics on the data.

The application supports:
- Loading and refreshing data from a CSV file
- Daily background data refresh
- Manual data refresh through an API
- Analytics APIs like customer count, order count, and average order value

---

## Tech Stack

- Language: C# (.NET 8)
- Framework: ASP.NET Core Web API
- Database: Microsoft SQL Server
- API Documentation: Swagger

---

## Prerequisites

Before running the project, make sure the following are installed:

- .NET SDK 8
- Microsoft SQL Server 
---

## Database Setup

1. Create a database in SQL Server and configure the connection string in appsettings.json:
2. Execute the sql file in the repo