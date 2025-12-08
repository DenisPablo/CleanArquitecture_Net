using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.commands;

public record ActualizarLibroCommand(
    Guid Id,
    string Titulo,
    string Descripcion,
    DateTime FechaPublicacion,
    ICollection<Guid> AutoresIds,
    Estados Estado
);
