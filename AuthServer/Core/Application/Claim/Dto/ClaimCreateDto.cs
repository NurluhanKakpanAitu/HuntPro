namespace AuthServer.Core.Application.Claim.Dto;

/// <summary>
/// Дто для создания права
/// </summary>
public sealed record ClaimCreateDto(string RoleId, string Type, string Value);