using BayonetSec.Application.Interfaces;
using BayonetSec.Application.Services;
using BayonetSec.Application.Validators;
using BayonetSec.Infrastructure.Data.DbContext;
using BayonetSec.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BayonetSec.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? configuration["ConnectionStrings:DefaultConnection"]
            ?? configuration["ConnectionStrings__DefaultConnection"];

        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            services.AddDbContext<BayonetSecDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
        else
        {
            services.AddDbContext<BayonetSecDbContext>(options =>
                options.UseInMemoryDatabase("BayonetSecDevDb"));
        }

        // Register repositories
        services.AddScoped<IRepository<BayonetSec.Domain.Entities.Tenant>, TenantRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IVulnerabilityRepository, VulnerabilityRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();

        // Register services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IVulnerabilityService, VulnerabilityService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<ITenantService, TenantService>();

        return services;
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
                // .AddValidatorsFromAssemblyContaining(typeof(BaseValidator));

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured.")))
            };
        });

        return services;
    }
}