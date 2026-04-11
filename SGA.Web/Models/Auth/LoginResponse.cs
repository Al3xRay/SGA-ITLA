namespace SGA.Web.Models.Auth;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string Rol { get; set; } = string.Empty;
    public int PersonaId { get; set; }
}
