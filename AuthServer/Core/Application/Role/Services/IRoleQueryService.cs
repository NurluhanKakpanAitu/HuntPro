using AuthServer.Core.Application.Role.Vm;
using AuthServer.Core.Application.Wrappers;

namespace AuthServer.Core.Application.Role.Services;

/// <summary>
/// Сервис для работы с ролями 
/// </summary>
public interface IRoleQueryService
{
    Task<PagedContent<RoleVm>> GetRolesAsync(int pageNum, int pageSize);
    
    Task<List<RoleVm>> GetAllRolesAsync();
    Task<RoleGetByIdVm> GetRoleByIdAsync(string roleId);
}