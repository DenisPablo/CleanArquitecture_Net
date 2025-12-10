using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces;

public interface IPlanRepository
{
    Task<Plan?> CrearPlanAsync(Plan plan);
    Task<Plan?> ObtenerPlanPorIdAsync(Guid id);
    Task<IEnumerable<Plan>> ListarPlanesAsync(int pageNumber, int pageSize);
    Task<Plan?> ActualizarPlanAsync(Plan plan);

}