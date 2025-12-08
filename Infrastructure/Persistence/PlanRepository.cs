using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Persistence;

public class PlanRepository(ApplicationDbContext context) : IPlanRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Plan?> CrearPlanAsync(Plan plan)
    {
        try
        {
            _context.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al crear plan", ex);
        }
    }

    public async Task<Plan?> ObtenerPlanPorIdAsync(Guid id)
    {
        try
        {
            return await _context.Planes.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al obtener plan por ID", ex);
        }
    }

    public async Task<IEnumerable<Plan>> ListarPlanesAsync(int pageNumber, int pageSize)
    {
        try
        {
            return await _context.Planes
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al obtener los planes", ex);
        }
    }

    public async Task<Plan?> ActualizarPlanAsync(Plan plan)
    {
        try
        {
            _context.Update(plan);
            await _context.SaveChangesAsync();
            return plan;
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al actualizar plan", ex);
        }
    }

    public async Task<Plan?> EliminarPlanAsync(Guid id)
    {
        try
        {
            var plan = await ObtenerPlanPorIdAsync(id);
            if (plan != null)
            {
                plan.Desactivar();
                _context.Update(plan);
                await _context.SaveChangesAsync();
            }
            return plan;
        }
        catch (Exception ex)
        {
            throw new PersistenceExeption("Error al eliminar plan", ex);
        }
   }
}