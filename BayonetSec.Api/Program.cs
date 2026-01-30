using BayonetSec.Api.Extensions;
using BayonetSec.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add application services
builder.Services.AddApplicationServices();

// Add FluentValidation
builder.Services.AddFluentValidation();

// Add JWT Authentication (ready for use)
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add OpenAPI
// builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.MapOpenApi();
    
    // Disable HTTPS redirection in development for Docker
    // app.UseHttpsRedirection();
}
else
{
    // Enforce HTTPS in production
    app.UseHttpsRedirection();
}

// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Add global exception handling middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
