using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.commands;

public record CrearPlanCommand(
    string Nombre,
    string Descripcion,
    decimal Precio,
    Estados Estado
);
