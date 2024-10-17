using Microsoft.OpenApi.Models;
using SportShopC_.Repositories;
using MySql.Data.MySqlClient; // Додайте цей простір імен для MySQL
using System.Data;
using SportShopC_.Repositories.Interfaces;
using Dapper.Repositories;

var builder = WebApplication.CreateBuilder(args);

/////////////////////////////////
// Add services to the container.
/////////////////////////////////

builder.Services.AddControllers();

// CONFIG FILES
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

// SWAGGER
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product API",
        Description = "An ASP.NET Core Web API for managing Product-Category items",
    });
});

// DATABASE
builder.Services.AddScoped<MySqlConnection>(s => new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDbTransaction>(s =>
{
    var conn = s.GetRequiredService<MySqlConnection>();
    conn.Open();
    return conn.BeginTransaction();
});

// DEPENDENCY INJECTION
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

///////////////////////////////////////
// Configure the HTTP request pipeline.
///////////////////////////////////////

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
