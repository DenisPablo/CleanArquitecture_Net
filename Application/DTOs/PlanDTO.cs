using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.DTOs;

public record PlanDTO
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public ICollection<SubscripcionDTO> Subscripciones { get; set; }
    public Estados Estado { get; set; }
}