using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces;

public interface ILibroRepository
{
    Task<Libro?> CrearLibroAsync(Libro libro);
    Task<Libro?> ObtenerLibroPorIdAsync(Guid id);
    Task<IEnumerable<Libro>> ListarLibrosAsync(int pageNumber, int pageSize);
    Task<Libro?> ActualizarLibroAsync(Libro libro);
    Task EliminarLibroAsync(Guid id);   
}