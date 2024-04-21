using System.Reflection;
using System.Text;
using Api;
using Api.Domain.Methods;
using Api.Domain.Properties;
using Api.Domain.Users;
using Api.Features.Auth;
using Api.Infrastructure.Authentication;
using Api.Infrastructure.Database;
using Api.Infrastructure.Database.Repositories;
using Api.Infrastructure.Services;
using Api.OptionsSetup;
using Api.Shared.Behaviours;
using Api.Shared.Interfaces;
using Carter;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

// builder.Services.AddAutoMapper(assembly);
builder.Services.AddMapster();
builder.Services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

builder.Services.AddCarter();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(assembly);

    options.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
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

builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

builder.Services.AddScoped<IMethodRepository, MethodRepository>();
builder.Services.AddScoped<IMethodParameterRepository, MethodParameterRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

builder.Services.AddScoped<IDomainEventService, DomainEventService>();
builder.Services.AddScoped<IPasswordManager, PasswordManager>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureOptions<JwtOptionsSetup>();

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

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies[AuthConstants.AccessTokenKey];

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Role.Admin.ToString(), policy => { policy.RequireRole(Role.Admin.ToString()); });


var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
});

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