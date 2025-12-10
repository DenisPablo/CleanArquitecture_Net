using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.Response;

public record AutorResponse(
    string Nombre,
    string Apellido,
    DateTime FechaNacimiento,
    ICollection<LibroResponse> Libros,
    Estados Estado
);