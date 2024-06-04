using Aspire.Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

// SQL Server container is configured with an auto-generated password by default
// but doesn't support any auto-creation of databases or running scripts on startup so we have to do it manually.
var NotesDb = builder.AddSqlServer("sqlserver")
    // Mount the init scripts directory into the container.
    .WithBindMount("./sqlserverconfig", "/usr/config")
    // Mount the SQL scripts directory into the container so that the init scripts run.
    .WithBindMount("../DatabaseContainers.ApiService/data/sqlserver", "/docker-entrypoint-initdb.d")
    // Run the custom entrypoint script on startup.
    .WithEntrypoint("/usr/config/entrypoint.sh")
    // Add the database to the application model so that it can be referenced by other resources.
    .AddDatabase("NotesDb");

builder.AddProject<Projects.OOP_PROJECT_Server>("apiservice")
    .WithReference(NotesDb);

builder.AddNpmApp("vue", "../oop_project.client")
    .WithHttpsEndpoint(isProxied: true, env:"PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
