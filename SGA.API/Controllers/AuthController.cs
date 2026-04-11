using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SGA.Application.Dtos.Personas;
using SGA.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SGA.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IPersonaService _personaService;

    public AuthController(IConfiguration configuration, IPersonaService personaService)
    {
        _configuration = configuration;
        _personaService = personaService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (string.IsNullOrEmpty(dto.Usuario) || string.IsNullOrEmpty(dto.Contrasena))
            return BadRequest(new { Message = "Usuario y contraseña son requeridos." });

        var credencialesValidas = await _personaService.ValidarCredencialesAsync(dto.Usuario, dto.Contrasena);
        if (!credencialesValidas)
            return Unauthorized(new { Message = "Credenciales inválidas." });

        var persona = await _personaService.GetByDocumentoAsync(dto.Usuario);
        var rol = ResolverRol(persona);


        var claims = new[]
        {
            new Claim(ClaimTypes.Name, dto.Usuario),
            new Claim(ClaimTypes.Role, rol)
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
            Expiration = expiration,
            Rol = rol,
            PersonaId = persona?.Id ?? 0
        });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] SavePersonaDto dto)
    {
        var result = await _personaService.SaveAsync(dto);
        return result.Success
            ? Ok(new { result.Message })
            : BadRequest(new { result.Message });
    }
    private static string ResolverRol(PersonaDto? persona)
    {
        if (persona == null) return "Estudiante";

        if (!string.IsNullOrWhiteSpace(persona.TipoPersonaNombre))
            return persona.TipoPersonaNombre;

        return persona.TipoPersonaId switch
        {
            1 => "Estudiante",
            2 => "Empleado",
            3 => "Administrador",
            4 => "Conductor",
            _ => "Estudiante"
        };
    }
}

public class LoginDto
{
    public string Usuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
}
