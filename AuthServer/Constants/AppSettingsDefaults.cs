namespace AuthServer.Constants;

public static class AppSettingsDefaults
{
    public const string JwtConfig = "JwtConfig";
    public const string DefaultConnection = "DefaultConnection";
    public const string AuthServerHttpClient = "AuthServerHttpClient";
    public const string JwtSecret = $"{JwtConfig}:Secret";
    public const string JwtIssuer = $"{JwtConfig}:Issuer";
    public const string JwtAudience = $"{JwtConfig}:Audience";
}