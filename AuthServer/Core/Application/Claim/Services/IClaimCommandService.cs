using AuthServer.Core.Application.Claim.Dto;

namespace AuthServer.Core.Application.Claim.Services;

public interface IClaimCommandService
{
    Task<string> CreateAsync(ClaimCreateDto claimCreateDto);
    
    Task DeleteAsync(RemoveClaimDto removeClaimDto);
}