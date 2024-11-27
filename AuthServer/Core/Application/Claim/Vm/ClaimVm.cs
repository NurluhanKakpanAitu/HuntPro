namespace AuthServer.Core.Application.Claim.Vm;

/// <summary>
/// Модель представления для прав.
/// </summary>
public sealed class ClaimVm
{
    public string Type { get; set; }
    public string? Value { get; set; }
}