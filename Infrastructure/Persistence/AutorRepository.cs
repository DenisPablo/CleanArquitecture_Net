using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BibliotecaDigital.Infrastructure.Persistence;

public class AutorRepository(ApplicationDbContext context) : IAutorRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Autor?> CrearAutorAsync(Autor autor)
    {
        try
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return autor;
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al crear autor", ex);
        }
    }

    public async Task<Autor?> ObtenerAutorPorIdAsync(Guid id)
    {
        try
        {
            return await _context.Autores.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al obtener autor por ID", ex);
        }
    }

    public async Task<IEnumerable<Autor>> ListarAutoresAsync(int pageNumber, int pageSize)
    {
        try
        {
            return await _context.Autores.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al obtener todos los autores", ex);
        }
    }

    public async Task<Autor?> ActualizarAutorAsync(Autor autor)
    {
        try
        {
            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
            return autor;
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al actualizar autor", ex);
        }
    }

    public async Task<Autor?> EliminarAutorAsync(Guid id)
    {
        try
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return null;

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return autor;
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al eliminar autor", ex);
        }
    }
}
