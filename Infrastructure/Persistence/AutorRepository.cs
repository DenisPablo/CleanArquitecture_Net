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

    public async Task<Autor?> ObtenerAutorPorIdAsync(int id)
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

    public async Task<IEnumerable<Autor>> ObtenerTodosLosAutoresAsync()
    {
        try
        {
            return await _context.Autores.ToListAsync();
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

    public async Task<Autor?> EliminarAutorAsync(int id)
    {
        try{
            var autor = await _context.Autores.FindAsync(id);
            if (autor != null)
            {
                autor.Desactivar();
                _context.Autores.Update(autor);
                await _context.SaveChangesAsync();
                return autor;
            }
            return await Task.FromResult<Autor?>(null);
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al eliminar autor", ex);
        }
    }
}
