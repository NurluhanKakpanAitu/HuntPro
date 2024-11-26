using System.Net.Http.Headers;
using AuthServer;
using AuthServer.Configurations;
using AuthServer.Constants;
using AuthServer.Core.Domain.Entities;
using AuthServer.Infrastructure.Persistence;
using AuthServer.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var appName = Environment.GetEnvironmentVariable("APP_NAME") ?? "auth-server";

// Add services to the container.
builder.Services.AddControllers();

// Set up the connection string for your database
var connectionString = builder.Configuration.GetConnectionString(AppSettingsDefaults.DefaultConnection) ?? string.Empty;

// Add Health Checks
builder.Services.AddHealthChecks();

// Add Persistence
builder.Services.AddPersistence(builder.Configuration);

// Add DB context with Npgsql
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

// Add Identity
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

// Bind JWT Configuration from app settings
builder.Services.Configure<JwtConfig>(o => builder.Configuration.GetSection(AppSettingsDefaults.JwtConfig).Bind(o));

// Register HttpClient for API calls
builder.Services.AddHttpClient(AppSettingsDefaults.AuthServerHttpClient, option => 
    option.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")));

// Configure API Behavior
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// Add CORS support
builder.Services.AddCors();

// Configure Authentication (ensure this is a method that configures authentication in the ConfigureServices)
builder.Services.ConfigureAuthentication(builder.Configuration);

// Add Swagger services
builder.Services.AddSwaggerGen();  // Ensure this is added for Swagger functionality

var app = builder.Build();

// Use Swagger in the middleware pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocumentTitle = "Swagger UI - Auth Server";  // Optional: customize Swagger UI title
});

// Health check endpoint
app.UseHealthChecks("/api/healthz");
app.MapHealthChecks("/api/healthz");

// Use routing
app.UseRouting();

app.UseCors(b => b.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader().SetIsOriginAllowed(_ => true));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
}

// Run the application
app.Run();
