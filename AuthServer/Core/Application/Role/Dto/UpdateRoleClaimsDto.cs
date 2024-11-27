using AuthServer.Core.Application.Claim.Dto;

namespace AuthServer.Core.Application.Role.Dto;

public class UpdateRoleClaimsDto
{
    public string RoleId { get; set; }
    public List<ClaimDto> Claims { get; set; }
}