using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.DTOs;

public record PlanDTO
{
    public Guid Id { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public required ICollection<SubscripcionDTO> Subscripciones { get; set; }
    public Estados Estado { get; set; }
}