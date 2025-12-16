using AutoMapper;
using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Application.Queries;
using BibliotecaDigital.Application.Response;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Domain.Interfaces;

namespace BibliotecaDigital.Application.Services;

public class SubscripcionService(ISubscripcionRepository subscripcionRepository, IPlanRepository planRepository, IMapper mapper)
{
    public readonly ISubscripcionRepository _subscripcionRepository = subscripcionRepository;
    public readonly IPlanRepository _planRepository = planRepository;
    public readonly IMapper _mapper = mapper;

    public async Task<Subscripcion?> Handle(CrearSubscripcionCommand command)
    {
        var plan = await _planRepository.ObtenerPlanPorIdAsync(command.PlanId);
        if (plan == null)
        {
            // We could throw exception, or let the validation inside Subscripcion constructor fail (it checks for null plan).
            // However, for cleaner error, we could throw manually.
            throw new DomainValidationException(new List<string> { "El plan especificado no existe." });
        }

        var nuevaSubscripcion = new Subscripcion(command.PlanId, command.ApplicationUserId, plan, command.FechaInicio, command.FechaFin);

        var subscripcionCreada = await _subscripcionRepository.CrearSubscripcionAsync(nuevaSubscripcion);

        return subscripcionCreada;
    }

    public async Task<SubscripcionResponse?> Handle(ActualizarSubscripcionCommand command)
    {
        var subscripcion = await _subscripcionRepository.ObtenerSubscripcionPorIdAsync(command.Id);

        if (subscripcion != null)
        {
            var plan = await _planRepository.ObtenerPlanPorIdAsync(command.PlanId);
            if (plan == null)
            {
                throw new DomainValidationException(new List<string> { "El plan especificado no existe." });
            }

            subscripcion.ActualizarSubscripcion(command.PlanId, command.ApplicationUserId, plan, command.FechaInicio, command.FechaFin);

            var subscripcionActualizada = await _subscripcionRepository.ActualizarSubscripcionAsync(subscripcion);

            var subscripcionResponse = _mapper.Map<SubscripcionResponse>(subscripcionActualizada);

            return subscripcionResponse;
        }

        return null;
    }

    public async Task<SubscripcionResponse?> Handle(EliminarSubscripcionCommand command)
    {
        var subscripcion = await _subscripcionRepository.ObtenerSubscripcionPorIdAsync(command.Id);

        if (subscripcion != null)
        {
            subscripcion.Desactivar();
            subscripcion = await _subscripcionRepository.ActualizarSubscripcionAsync(subscripcion);

            var subscripcionResponse = _mapper.Map<SubscripcionResponse>(subscripcion);
            return subscripcionResponse;
        }

        return null;
    }

    public async Task<SubscripcionResponse?> Handle(BuscarSubscripcionQuery query)
    {
        var subscripcion = await _subscripcionRepository.ObtenerSubscripcionPorIdAsync(query.Id);

        return _mapper.Map<SubscripcionResponse>(subscripcion);
    }

    public async Task<List<SubscripcionResponse>> Handle(ListarSubscripcionesQuery query)
    {
        var pageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
        var pageSize = query.PageSize < 1 ? 10 : query.PageSize;

        var subscripciones = await _subscripcionRepository.ListarSubscripcionesAsync(pageNumber, pageSize);

        return _mapper.Map<List<SubscripcionResponse>>(subscripciones);
    }
}
