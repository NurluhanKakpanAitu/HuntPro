namespace AuthServer.Core.Application.Claim.Dto;

public sealed record RemoveClaimDto(string RoleId, string Type, string Value);