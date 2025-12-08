using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.Commands;

public record CrearSubscripcionCommand(
    Guid PlanId,
    Guid ApplicationUserId,
    DateTime FechaInicio,
    DateTime FechaFin,
    Estados Estado
);
