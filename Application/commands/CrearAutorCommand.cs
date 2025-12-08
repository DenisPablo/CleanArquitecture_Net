namespace BibliotecaDigital.Application.Commands;

public record CrearAutorCommand(
     string Nombre,
     string Apellido,
     DateTime FechaNacimiento
);