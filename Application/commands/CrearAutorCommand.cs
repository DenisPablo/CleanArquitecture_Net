namespace BibliotecaDigital.Application.commands;

public record AutorCrearCommand()
{
    public required string Nombre {get; set;}
    public required string Apellido {get; set;}
    public required DateTime FechaNacimiento {get; set;}
}