using AuthServer.Core.Application.Claim.Services;
using AuthServer.Core.Application.Role.Services;
using AuthServer.Infrastructure.Persistence.Services.Claim;
using AuthServer.Infrastructure.Persistence.Services.Role;

namespace AuthServer.Infrastructure.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRoleCommandService, RoleCommandService>();
        services.AddScoped<IRoleQueryService, RoleQueryService>();
        services.AddScoped<IClaimCommandService, ClaimCommandService>();
        services.AddScoped<IClaimQueryService, ClaimQueryService>();
    }
}