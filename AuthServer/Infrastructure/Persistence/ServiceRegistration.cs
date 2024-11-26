using AuthServer.Core.Application.Role.Services;
using AuthServer.Infrastructure.Persistence.Services.Role;

namespace AuthServer.Infrastructure.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRoleCommandService, RoleCommandService>();
        services.AddScoped<IRoleQueryService, RoleQueryService>();
    }
}