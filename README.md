# ASP_Project

# Libraries used in project:

- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore   v5.0.6
Middleware for detecting and diagnosing errors with EFC migrations

- Microsoft.AspNetCore.Identity.EntityFrameworkCore	     v5.0.6
Contains types for using Entity Framework Core with Identity on ASP.NET Core.

- Microsoft.AspNetCore.Identity.UI					     v5.0.6
Default Razor Pages built-in UI for ASP.NET Core Framework

 
- Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation      v5.0.6
Runtime compilation support for Razor views and Razor Pages in ASP.NET Core MVC.

- Microsoft.EntityFrameworkCore						     v5.0.6
Modern object-database mapper for .NET. It supports LINQ queries, change tracking,
updates, and schema migrations. EF Core works with SQL Server, Azure SQL Database,
SQLite, Azure Cosmos DB, MySQL, PostgreSQL, and other databases through a provider plugin API.

- Microsoft.EntityFrameworkCore.SqlServer			     v5.0.6
Microsoft SQL Server database provider for Entity Framework Core.

- Microsoft.EntityFrameworkCore.Tools					 v5.0.6
 Entity Framework Core Tools for the NuGet Package Manager Console in Visual Studio.

- Microsoft.VisualStudio.Web.CodeGeneration.DesignCode Generation tool for ASP.NET Core. 
Contains the dotnet-aspnet-codegenerator command used for generating controllers and views.

- SendGrid											     v9.23.2
C# client library and examples for using Twilio SendGrid API's to send mail and access Web API v3 endpoints
with .NET Standard 1.3 and .NET Core support.

Every package was installed through NuGet Package Manager by typing in NuGet terminal Install-Package "NameofThePackage"

To use SendGrid it was necessery to configure account and Email Sender on the https://app.sendgrid.com website, after configuration
it is mandatory to save SendGridUser and SendGridKey in user-secrets via .NET CLI:
dotnet user-secrets set SendGridUser  ia.kowalczyk5@gmail.com --project "path to project"
dotnet user-secrets set SendGridKey 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
