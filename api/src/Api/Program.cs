using System.Text;
using Api.Extensions;
using Api.Infrastructure;
using Api.OptionsSetup;
using Application;
using Carter;
using Domain.Users;
using Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

// Настройка приложения и регистрация сервисов

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// добавляет поддержку Endpoint Routing и интеграцию с API Explorer.
builder.Services.AddEndpointsApiExplorer();

// регистрируют сервисы из инфраструктуры и слоя приложения соответственно.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// настраивает CORS (Cross-Origin Resource Sharing) для разрешения запросов с определенных источников.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

//  добавляет поддержку Swagger для документации API.
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme,
                }
            },
            Array.Empty<string>()
        }
    });
});

// добавляют обработку исключений и детализацию проблем для API.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// регистрирует Mapster для маппинга объектов.
builder.Services.AddMapster();

// добавляет Carter для создания маршрутов API.
builder.Services.AddCarter();

// добавляет доступ к контексту HTTP.
builder.Services.AddHttpContextAccessor();

//  настраивает параметры JWT токена.
builder.Services.ConfigureOptions<JwtOptionsSetup>();

//  добавляют аутентификацию с использованием JWT токенов.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    JwtBearerDefaults.AuthenticationScheme,
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!))
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Role.Admin.ToString(), policy => { policy.RequireRole(Role.Admin.ToString()); });


WebApplication? app = builder.Build();

//  применяет все ожидающие миграции базы данных.
app.ApplyMigrations();

// добавляет CORS middleware.
app.UseCors();

// перенаправляет HTTP запросы на HTTPS.
app.UseHttpsRedirection();

//  добавляет политику cookie, определяющую правила использования файлов cookie.
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
});

if (app.Environment.IsDevelopment())
{
    // включают Swagger и его пользовательский интерфейс для документации API в режиме разработки.
    app.UseSwagger();
    app.UseSwaggerUI();
    // использует middleware для обработки исключений и перенаправления на страницу ошибки в режиме разработки.
    app.UseExceptionHandler("/error-dev");
}
else
{
    app.UseExceptionHandler("/error");
}

// добавляют middleware для аутентификации и авторизации.
app.UseAuthentication();
app.UseAuthorization();

// добавляет Carter middleware для обработки маршрутов API.
app.MapCarter();

// добавляет middleware для обработки ошибок.
app.UseExceptionHandler();

app.Run();

namespace Api
{
    public class Program;
}
