using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.Commands;

public record CrearLibroCommand(
    string Titulo,
    string Descripcion,
    DateTime FechaPublicacion,
    ICollection<Guid> AutoresIds,
    Estados Estado
);
