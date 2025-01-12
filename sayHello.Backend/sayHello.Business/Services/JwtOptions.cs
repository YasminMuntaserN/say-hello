namespace sayHello.api;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int lifeTime { get; set; }
    public string SignKey { get; set; }
}

