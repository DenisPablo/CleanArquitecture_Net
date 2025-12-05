namespace BibliotecaDigital.Domain.Exceptions;

public class DomainValidationException : Exception
{
    public IReadOnlyList<string> Errors { get; }
    public DomainValidationException(string message)
        : base("Fallos de validación del dominio: " + message)
    {
        Errors = new List<string> { message };
    }

    public DomainValidationException(IReadOnlyList<string> errors)
        : base("Múltiples fallos de validación del dominio han ocurrido.")
    {
        Errors = errors;
    }
}