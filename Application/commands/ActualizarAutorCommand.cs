namespace BibliotecaDigital.Application.commands;

public record AutorActualizarCommand(
    Guid Id,
     string Nombre,
     string Apellido,
     DateTime FechaNacimiento     
);