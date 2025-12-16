using AutoMapper;
using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Application.Queries;
using BibliotecaDigital.Application.Response;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Domain.ValueObjects;

namespace BibliotecaDigital.Application.Services;

public class PlanService(IPlanRepository planRepository, IMapper mapper)
{
    public readonly IPlanRepository _planRepository = planRepository;
    public readonly IMapper _mapper = mapper;

    public async Task<Plan?> Handle(CrearPlanCommand command)
    {
        var nombreVO = new NombreApellidoTituloValueObject(command.Nombre);
        var descripcionVO = new DescripcionValueObject(command.Descripcion);

        var nuevoPlan = new Plan(nombreVO, descripcionVO, command.Precio, new List<Subscripcion>());

        var planCreado = await _planRepository.CrearPlanAsync(nuevoPlan);

        return planCreado;
    }

    public async Task<PlanResponse?> Handle(ActualizarPlanCommand command)
    {
        var plan = await _planRepository.ObtenerPlanPorIdAsync(command.Id);

        if (plan != null)
        {
            var nombreVO = new NombreApellidoTituloValueObject(command.Nombre);
            var descripcionVO = new DescripcionValueObject(command.Descripcion);

            plan.ActualizarPlan(nombreVO, descripcionVO, command.Precio);

            var planActualizado = await _planRepository.ActualizarPlanAsync(plan);

            var planResponse = _mapper.Map<PlanResponse>(planActualizado);

            return planResponse;
        }

        return null;
    }

    public async Task<PlanResponse?> Handle(EliminarPlanCommand command)
    {
        var plan = await _planRepository.ObtenerPlanPorIdAsync(command.Id);

        if (plan != null)
        {
            plan.Desactivar();
            plan = await _planRepository.ActualizarPlanAsync(plan);

            var planResponse = _mapper.Map<PlanResponse>(plan);
            return planResponse;
        }

        return null;
    }

    public async Task<PlanResponse?> Handle(BuscarPlanQuery query)
    {
        var plan = await _planRepository.ObtenerPlanPorIdAsync(query.Id);

        return _mapper.Map<PlanResponse>(plan);
    }

    public async Task<List<PlanResponse>> Handle(ListarPlanesQuery query)
    {
        var pageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
        var pageSize = query.PageSize < 1 ? 10 : query.PageSize;

        var planes = await _planRepository.ListarPlanesAsync(pageNumber, pageSize);

        return _mapper.Map<List<PlanResponse>>(planes);
    }
}
