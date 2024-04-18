using System.Reflection;
using Api;
using Api.Infrastructure.Authentication;
using Api.Infrastructure.Database;
using Api.Infrastructure.Services;
using Api.OptionsSetup;
using Api.Shared.Behaviours;
using Api.Shared.Interfaces;
using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAutoMapper(assembly);
builder.Services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

builder.Services.AddCarter();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(assembly);

    options.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    options.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(
            builder.Configuration.GetConnectionString("Database"),
            b => { b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName); })
        .UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped<IDomainEventService, DomainEventService>();
builder.Services.AddScoped<IPasswordManager, PasswordManager>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-dev");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();
app.UseExceptionHandler();
app.Run();

public partial class Program;