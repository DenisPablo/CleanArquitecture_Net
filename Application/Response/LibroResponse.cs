using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.Response;

public record LibroResponse(
    string Titulo,
    string Descripcion,
    DateTime FechaPublicacion,
    ICollection<AutorResponse> Autores,
    Estados Estado
);
