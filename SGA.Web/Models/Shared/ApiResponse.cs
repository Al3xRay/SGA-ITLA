namespace SGA.Web.Models.Shared;


// Esto es un wrapper genérico para respuestas de la API que no retornan datos.

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();

    public static ApiResponse Ok(string message = "Operación exitosa")
        => new() { Success = true, Message = message };

    public static ApiResponse Fail(string message, List<string>? errors = null)
        => new() { Success = false, Message = message, Errors = errors ?? new() };
}

// Representa la estructura de error retornada por SGA.API.

public class ApiErrorResponse
{
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
}
