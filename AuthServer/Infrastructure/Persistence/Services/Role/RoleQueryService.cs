using AuthServer.Core.Application.Claim.Vm;
using AuthServer.Core.Application.Extensions;
using AuthServer.Core.Application.Role.Services;
using AuthServer.Core.Application.Role.Vm;
using AuthServer.Core.Application.Wrappers;
using AuthServer.Core.Domain.Errors;
using AuthServer.Exceptions;
using AuthServer.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Infrastructure.Persistence.Services.Role;

public sealed class RoleQueryService(ApplicationDbContext context) : IRoleQueryService
{
    public async Task<PagedContent<RoleVm>> GetRolesAsync(int pageNum, int pageSize)
    {
        var query = context.Roles
            .OrderBy(q => q.Name)
            .Select(q => new RoleVm
            {
                Name = q.Name!
            });
        
        var total = await query.CountAsync();
        var content = await query.Paged(pageNum, pageSize).ToListAsync();
        
        return new PagedContent<RoleVm>(content, pageNum, pageSize)
        {
            TotalCount = total
        };
    }

    public async Task<List<RoleVm>> GetAllRolesAsync()
    {
        return await context.Roles
            .OrderBy(q => q.Name)
            .Select(q => new RoleVm
            {
                Name = q.Name!
            })
            .ToListAsync();
    }

    public async Task<RoleGetByIdVm> GetRoleByIdAsync(string roleId)
    {
        var query = context.Roles
            .Where(q => q.Id == roleId)
            .Select(q => new RoleGetByIdVm
            {
                Name = q.Name!,
                Claims = context.RoleClaims
                    .Where(claim => claim.RoleId == roleId)
                    .OrderBy(claim => claim.ClaimType)
                    .Select(claim => new ClaimVm
                    {
                        Type = claim.ClaimType!,
                        Value = claim.ClaimValue!
                    })
                    .ToList()
            });
            
        var role = await query.FirstOrDefaultAsync();
        
        if (role == null)
            throw new ValidationException(RoleErrors.NotFound);
        
        return role;
    }
}