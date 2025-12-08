using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.Commands;

public record CrearPlanCommand(
    string Nombre,
    string Descripcion,
    decimal Precio,
    Estados Estado
);
