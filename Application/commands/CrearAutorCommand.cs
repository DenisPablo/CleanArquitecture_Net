namespace BibliotecaDigital.Application.commands;

public record AutorCrearCommand(
     string Nombre,
     string Apellido,
     DateTime FechaNacimiento
);