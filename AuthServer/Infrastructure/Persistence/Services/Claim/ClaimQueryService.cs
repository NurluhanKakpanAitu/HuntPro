using AuthServer.Core.Application.Claim.Services;
using AuthServer.Core.Application.Claim.Vm;
using AuthServer.Core.Application.Extensions;
using AuthServer.Core.Application.Wrappers;
using AuthServer.Core.Domain.Errors;
using AuthServer.Exceptions;
using AuthServer.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace AuthServer.Infrastructure.Persistence.Services.Claim;

public sealed class ClaimQueryService(
    RoleManager<IdentityRole> roleManager,
    ApplicationDbContext dbContext
    ) : IClaimQueryService
{
    public async Task<PagedContent<ClaimVm>> GetClaimsAsync(string roleId, int pageNum, int pageSize)
    {
        var role = await roleManager.FindByIdAsync(roleId);
        if (role == null)
            throw new ValidationException(RoleErrors.NotFound);

        var query = dbContext.RoleClaims
            .Where(q => q.RoleId == roleId)
            .OrderBy(q => q.ClaimType)
            .AsQueryable()
            .Select(q => new ClaimVm
            {
                Type = q.ClaimType!,
                Value = q.ClaimValue!
            });
       
        var total = await query.CountAsync();
        var content = await query.Paged(pageNum, pageSize).ToListAsync();
        
        return new PagedContent<ClaimVm>(content, pageNum, pageSize)
        {
            TotalCount = total
        };
    }

    public async Task<List<ClaimVm>> GetAllClaimsAsync(string roleId)
    {
        return await dbContext.RoleClaims
            .Where(q => q.RoleId == roleId)
            .OrderBy(q => q.ClaimType)
            .Select(q => new ClaimVm
            {
                Type = q.ClaimType!,
                Value = q.ClaimValue!
            })
            .ToListAsync();
    }
}