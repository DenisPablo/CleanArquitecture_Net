using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.commands;

public record ActualizarSubscripcionCommand(
    Guid Id,
    Guid PlanId,
    Guid ApplicationUserId,
    DateTime FechaInicio,
    DateTime FechaFin,
    Estados Estado
);
