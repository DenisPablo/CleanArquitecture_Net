namespace BibliotecaDigital.Application.commands;

public record AutorActualizarCommand()
{
    public Guid Id {get; set;}
    public string Nombre {get; set;}
    public string Apellido {get; set;}
    public DateTime FechaNacimiento {get; set;}
}