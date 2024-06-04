using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Loading configuration from appsettings.json
var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

// Connecting to local SQLite db.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//        options.UseSqlServer(builder.Environment.IsDevelopment() ? configuration.GetConnectionString("DbConnectionDev") : configuration.GetConnectionString("DbConnection")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Добавление доверенных прокси
if (builder.Environment.IsDevelopment())
{
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.KnownProxies.Add(IPAddress.Parse("localhost:80"));
    });

}

builder.AddSqlServerDbContext<ApplicationDbContext>("NotesDb");

var app = builder.Build();

//app.UseDefaultFiles();
//app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Use(async (context, next) =>
    {
        // Логирование информации о входящем запросе
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

        await next.Invoke();

        // Логирование информации об исходящем ответе
        Console.WriteLine($"Response: {context.Response.StatusCode}");
    });
} else
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
