using AuthServer.Core.Application.Claim.Dto;
using AuthServer.Core.Application.Claim.Services;
using AuthServer.Core.Domain.Errors;
using AuthServer.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Infrastructure.Persistence.Services.Claim;

public sealed class ClaimCommandService(RoleManager<IdentityRole> roleManager) : IClaimCommandService
{
    public async Task<string> CreateAsync(ClaimCreateDto claimCreateDto)
    {
        var role = await roleManager.FindByIdAsync(claimCreateDto.RoleId);
        if (role == null)
            throw new ValidationException(RoleErrors.NotFound);
        
        var claim = new System.Security.Claims.Claim(claimCreateDto.Type, claimCreateDto.Value);
        
        var result = await roleManager.AddClaimAsync(role, claim);
        
        if (!result.Succeeded)
            throw new ValidationException(result.Errors.First().Code, result.Errors.First().Description);
        
        return claimCreateDto.RoleId;
    }

    public async Task DeleteAsync(RemoveClaimDto removeClaimDto)
    {
        var role = await roleManager.FindByIdAsync(removeClaimDto.RoleId);
        if (role == null)
            throw new ValidationException(RoleErrors.NotFound);
        
        var claim = new System.Security.Claims.Claim(removeClaimDto.Type, removeClaimDto.Value);
        var result = await roleManager.RemoveClaimAsync(role, claim);
        
        if (!result.Succeeded)
            throw new ValidationException(result.Errors.First().Code, result.Errors.First().Description);
    }
}