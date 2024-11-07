using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using NtierEF.BLL.Interfaces;
using NtierEF.BLL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NtierEF.BLL.Configurations;
using NtierEF.BLL.Validations;
using NtierEF.DAL;
using NtierEF.DAL.Interfaces.Repositories;
using NtierEF.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Додати сервіси до контейнера
builder.Services.AddControllers();

// Додати AutoMapper, вказуючи профіль конфігурації
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Додати DbContext з рядком підключення з appsettings.json
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0))
    ));

// Зареєструвати репозиторії
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Зареєструвати сервіси
builder.Services.AddScoped<ICategoryService, CategoriesService>();
builder.Services.AddScoped<IProductService, ProductsService>();

// Додати FluentValidation для автоматичної валідації
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryRequestValidator>();

// Налаштувати Swagger для документації API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShopDb API",
        Version = "v1",
        Description = "API для управління товарами та категоріями магазину",
        Contact = new OpenApiContact
        {
            Name = "Ваше Ім'я",
            Email = "ваша.почта@example.com",
            Url = new Uri("https://ваш-сайт.com")
        }
    });
});

var app = builder.Build();

// Налаштувати HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopDb API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
