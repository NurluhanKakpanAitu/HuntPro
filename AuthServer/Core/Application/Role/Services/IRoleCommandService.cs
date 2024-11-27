using AuthServer.Core.Application.Claim.Dto;
using AuthServer.Core.Application.Role.Dto;

namespace AuthServer.Core.Application.Role.Services;

/// <summary>
/// Сервис для работы с ролями
/// </summary>
public interface IRoleCommandService
{
    /// <summary>
    /// Создание роли
    /// </summary>
    /// <param name="roleCreateDto">Данные для создания</param>
    /// <returns>Идентификатор созданной роли</returns>
    Task<string> CreateRoleAsync(RoleCreateDto roleCreateDto);
    
    /// <summary>
    /// Обновление роли
    /// </summary>
    /// <param name="roleUpdateDto">Данные для обновления</param>
    /// <returns>Идентификатор обновленной роли</returns>
    Task<string> UpdateRoleAsync(RoleUpdateDto roleUpdateDto);
    
    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="roleId">Идентификатор роли</param>
    /// <returns>Задача</returns>
    Task DeleteRoleAsync(string roleId);
    
    /// <summary>
    /// Обновление прав доступа роли
    /// </summary>
    /// <param name="updateRoleClaimsDto"></param>
    /// <returns></returns>
    Task<string> UpdateRoleClaimsAsync(UpdateRoleClaimsDto updateRoleClaimsDto);
}