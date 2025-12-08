using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.Response;

public record SubscripcionResponse(
    Guid PlanId,
    Guid ApplicationUserId,
    DateTime FechaInicio,
    DateTime FechaFin,
    Estados Estado
);