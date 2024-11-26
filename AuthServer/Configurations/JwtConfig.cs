namespace AuthServer.Configurations;

public class JwtConfig
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public TimeSpan Expiration { get; set; }
    public TimeSpan? RefreshTokenExpiration { get; set; }
}