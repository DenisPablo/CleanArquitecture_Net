using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces;

public interface ILibroRepository
{
    Task<Libro?> CrearLibroAsync(Libro libro);
    Task<Libro?> ObtenerLibroPorIdAsync(int id);
    Task<IEnumerable<Libro>> ObtenerTodosLosLibrosAsync();
    Task<Libro?> ActualizarLibroAsync(Libro libro);
    Task EliminarLibroAsync(int id);   
}