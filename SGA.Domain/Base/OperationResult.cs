namespace SGA.Domain.Base;

public class OperationResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();

    public static OperationResult Ok(string message = "Operación exitosa")
    {
        return new OperationResult { Success = true, Message = message };
    }

    public static OperationResult Fail(string error)
    {
        return new OperationResult
        {
            Success = false,
            Errors = new List<string> { error }
        };
    }

    public static OperationResult Fail(List<string> errors)
    {
        return new OperationResult
        {
            Success = false,
            Errors = errors
        };
    }
}

public class OperationResult<T> : OperationResult
{
    public T? Data { get; set; }

    public static OperationResult<T> Ok(T data, string message = "Operación exitosa")
    {
        return new OperationResult<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public new static OperationResult<T> Fail(string error)
    {
        return new OperationResult<T>
        {
            Success = false,
            Errors = new List<string> { error }
        };
    }
}