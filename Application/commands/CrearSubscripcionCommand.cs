using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.commands;

public record CrearSubscripcionCommand(
    Guid PlanId,
    Guid ApplicationUserId,
    DateTime FechaInicio,
    DateTime FechaFin,
    Estados Estado
);
