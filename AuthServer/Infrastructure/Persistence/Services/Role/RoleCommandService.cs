using AuthServer.Core.Application.Claim.Dto;
using AuthServer.Core.Application.Role.Dto;
using AuthServer.Core.Application.Role.Services;
using AuthServer.Core.Domain.Errors;
using AuthServer.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Infrastructure.Persistence.Services.Role;

public sealed class RoleCommandService(
    RoleManager<IdentityRole> roleManager) : IRoleCommandService
{
    public async Task<string> CreateRoleAsync(RoleCreateDto roleCreateDto)
    {
         var existingRole = await roleManager.FindByNameAsync(roleCreateDto.Name);
         if (existingRole != null)
             throw new ValidationException(RoleErrors.AlreadyExists);
         
         var role = new IdentityRole(roleCreateDto.Name);
         var result = await roleManager.CreateAsync(role);
         if (!result.Succeeded)
             throw new ValidationException(result.Errors.First().Code, result.Errors.First().Description);
         
         return role.Id;
    }

    public async Task<string> UpdateRoleAsync(RoleUpdateDto roleUpdateDto)
    {
        var role = await roleManager.FindByIdAsync(roleUpdateDto.Id);
        
        if (role == null)
            throw new ValidationException(RoleErrors.NotFound);
        
        role.Name = roleUpdateDto.Name;
        var result = await roleManager.UpdateAsync(role);
        if (!result.Succeeded)
            throw new ValidationException(result.Errors.First().Code, result.Errors.First().Description);
        
        return role.Id;
    }

    public async Task DeleteRoleAsync(string roleId)
    {
        var role = await roleManager.FindByIdAsync(roleId);
        if (role == null)
            throw new ValidationException(RoleErrors.NotFound);
        
        var result = await roleManager.DeleteAsync(role);
        if (!result.Succeeded)
            throw new ValidationException(result.Errors.First().Code, result.Errors.First().Description);
    }
}