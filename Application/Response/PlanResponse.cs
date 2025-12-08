using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.Response;

public record PlanResponse(
    Guid Id,
    string Nombre,
    string Descripcion,
    decimal Precio,
    ICollection<SubscripcionResponse> Subscripciones,
    Estados Estado
);