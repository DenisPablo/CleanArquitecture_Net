using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces;

public interface IPlanRepository
{
    Task<Plan?> CrearPlanAsync(Plan plan);
    Task<Plan?> ObtenerPlanPorIdAsync(Guid id);
    Task<IEnumerable<Plan>> ObtenerTodosLosPlanesAsync();
    Task<Plan?> ActualizarPlanAsync(Plan plan);
    Task<Plan?> EliminarPlanAsync(Guid id);   
}