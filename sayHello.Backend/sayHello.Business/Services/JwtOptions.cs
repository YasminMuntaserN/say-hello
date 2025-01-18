namespace sayHello.api;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int RefreshTokenLifetimeDays { get; set; }
    public int AccessTokenLifetimeMinutes { get; set; }
    public string SignKey { get; set; }
}

