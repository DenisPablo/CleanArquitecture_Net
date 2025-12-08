namespace BibliotecaDigital.Application.Commands;

public record ActualizarAutorCommand(
    Guid Id,
     string Nombre,
     string Apellido,
     DateTime FechaNacimiento     
);