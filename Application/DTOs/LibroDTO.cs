using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.DTOs;

public record LibroDTO
{
    public required string Titulo { get; set; }
    public required string Descripcion { get; set; }
    public required DateTime FechaPublicacion { get; set; }
    public required ICollection<AutorDTO> Autores { get; set; }
    public Estados Estado { get; set; }
}
