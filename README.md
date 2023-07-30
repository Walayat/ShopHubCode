# .NET Core 3.1 MVC Project with Code-First Approach Migrations

This repository contains a .NET Core 3.1 MVC-based project that follows the Code-First approach for database migrations.
This project is designed to showcase how to set up a web application using .NET Core MVC and manage database schema changes through code-first migrations.

# Prerequisites

To run this project, you need to have the following installed on your machine:
.NET Core 3.1 SDK

# Getting Started
1: Clone this repository to your local machine using Git:
   git clone https://github.com/Walayat/ShopHubCode.git

2: Restore the NuGet packages:
    dotnet restore

3: Apply database migrations to create the database:
   dotnet ef database update

4: Run the application:
   dotnet run

5: Open your web browser and navigate to https://localhost:5001 to access the application.

# Code-First Approach and Migrations
In this project, we have implemented the Code-First approach for managing the database schema. The models for the application are defined in the Models directory. 
Entity Framework Core is used to create the database based on these model classes.

To add a new entity or make changes to existing entities, follow these steps:
1. Open the Package Manager Console or the .NET CLI and execute the following command to create a migration:
   dotnet ef migrations add YourMigrationName.
2. The migration will generate the necessary code to apply the changes to the database. You can review the generated code in the Migrations folder.
3. Apply the migration to the database by executing the following command:
   dotnet ef database update.
4. The database will now be updated with the changes defined in the migration.


# Acknowledgments
We would like to thank the .NET Core community and the Entity Framework Core team for their excellent tools and resources.

