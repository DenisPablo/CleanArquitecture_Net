using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.Commands;

public record ActualizarPlanCommand(
    Guid Id,
    string Nombre,
    string Descripcion,
    decimal Precio,
    Estados Estado
);
