using AuthServer.Core.Application.Claim.Vm;
using AuthServer.Core.Application.Wrappers;

namespace AuthServer.Core.Application.Claim.Services;

public interface IClaimQueryService
{
    Task<PagedContent<ClaimVm>> GetClaimsAsync(string roleId, int pageNum, int pageSize);
    
    Task<List<ClaimVm>> GetAllClaimsAsync(string roleId);
}