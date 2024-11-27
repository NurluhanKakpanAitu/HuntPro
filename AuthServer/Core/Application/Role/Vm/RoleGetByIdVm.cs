using AuthServer.Core.Application.Claim.Vm;

namespace AuthServer.Core.Application.Role.Vm;

/// <summary>
/// Модель представления для получения роли по идентификатору
/// </summary>
public class RoleGetByIdVm
{
    public string Name { get; set; }
    
    public List<ClaimVm> Claims { get; set; }
}