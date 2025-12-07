using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.DTOs;

public record LibroDTO
{
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaPublicacion { get; set; }
    public ICollection<AutorDTO> Autores { get; set; }
    public Estados Estado { get; set; }
}
