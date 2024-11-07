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

// ������ ������ �� ����������
builder.Services.AddControllers();

// ������ AutoMapper, �������� ������� ������������
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// ������ DbContext � ������ ���������� � appsettings.json
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0))
    ));

// ������������ ���������
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// ������������ ������
builder.Services.AddScoped<ICategoryService, CategoriesService>();
builder.Services.AddScoped<IProductService, ProductsService>();

// ������ FluentValidation ��� ����������� ��������
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryRequestValidator>();

// ����������� Swagger ��� ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShopDb API",
        Version = "v1",
        Description = "API ��� ��������� �������� �� ���������� ��������",
        Contact = new OpenApiContact
        {
            Name = "���� ��'�",
            Email = "����.�����@example.com",
            Url = new Uri("https://���-����.com")
        }
    });
});

var app = builder.Build();

// ����������� HTTP request pipeline
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
