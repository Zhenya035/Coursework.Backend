namespace Coursework.Infrastructure.Jwt;

public class AuthSettings
{
    public TimeSpan Expires { get; set; }
    public string SecretKey { get; set; } = string.Empty;
}