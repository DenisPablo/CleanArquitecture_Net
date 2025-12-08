using BibliotecaDigital.Application.DTOs;

namespace BibliotecaDigital.Application.commands;

public record BuscarAutoresQuery()
{
    public required ICollection<AutorDTO> Autores {get; set;}
}