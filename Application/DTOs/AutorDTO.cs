using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.DTOs;

public record AutorDTO
{
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required DateTime FechaNacimiento { get; set; }
    public required ICollection<LibroDTO> Libros { get; set; }
    public Estados Estado { get; set; }
    public DateTime FechaAlta { get; set; }
}