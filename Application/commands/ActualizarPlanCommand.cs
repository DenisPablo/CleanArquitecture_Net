using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.commands;

public record ActualizarPlanCommand(
    Guid Id,
    string Nombre,
    string Descripcion,
    decimal Precio,
    Estados Estado
);
