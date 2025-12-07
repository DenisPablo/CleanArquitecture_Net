using System.ComponentModel.DataAnnotations;
using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Application.DTOs;

public record AutorDTO
{
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Apellido { get; set; }
    [Required]
    public DateTime FechaNacimiento { get; set; }
    [Required]
    public ICollection<LibroDTO> Libros { get; set; }
    [Required]
    public Estados Estado { get; set; }
    [Required]
    public DateTime FechaAlta { get; set; }
}