using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SGA.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        // TODO: Validar credenciales contra la base de datos
        // Por ahora, se genera un token de prueba
        if (string.IsNullOrEmpty(dto.Usuario) || string.IsNullOrEmpty(dto.Contrasena))
            return BadRequest(new { Message = "Usuario y contraseña son requeridos." });

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, dto.Usuario),
            new Claim(ClaimTypes.Role, dto.Rol ?? "Estudiante")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(
            double.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "60"));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        });
    }
}

public class LoginDto
{
    public string Usuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string? Rol { get; set; }
}
