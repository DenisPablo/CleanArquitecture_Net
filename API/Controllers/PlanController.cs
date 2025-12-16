using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Application.Queries;
using BibliotecaDigital.Application.Response;
using BibliotecaDigital.Application.Services;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaDigital.API.Controllers;

/// <summary>
/// Controlador para gestionar operaciones relacionadas con planes
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PlanController(PlanService planService) : ControllerBase
{
    private readonly PlanService _planService = planService;

    /// <summary>
    /// Crea un nuevo plan en el sistema
    /// </summary>
    [HttpPost("CrearPlan", Name = "CrearPlan")]
    [ProducesResponseType(typeof(Plan), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CrearPlan(CrearPlanCommand command)
    {
        try
        {
            var plan = await _planService.Handle(command);

            return CreatedAtAction(nameof(BuscarPlan), new { id = plan!.Id }, plan);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest("Error al crear plan: " + ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, "Error al crear plan: " + ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Actualiza los datos de un plan existente
    /// </summary>
    [HttpPut("ActualizarPlan/{id:guid}", Name = "ActualizarPlan")]
    [ProducesResponseType(typeof(PlanResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ActualizarPlan(Guid id, ActualizarPlanCommand command)
    {
        try
        {
            var commandToHandle = command with { Id = id };
            var plan = await _planService.Handle(commandToHandle);

            if (plan == null) return NotFound();

            return Ok(plan);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest("Error al actualizar plan: " + ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, "Error al actualizar plan: " + ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error al actualizar plan: " + ex.Message);
        }
    }

    /// <summary>
    /// Desactiva un plan (eliminación lógica)
    /// </summary>
    [HttpDelete("EliminarPlan/{id:guid}", Name = "EliminarPlan")]
    [ProducesResponseType(typeof(PlanResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> EliminarPlan(Guid id)
    {
        try
        {
            var commandToHandle = new EliminarPlanCommand(id);

            var plan = await _planService.Handle(commandToHandle);

            if (plan == null) return NotFound();

            return Ok(plan);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Busca un plan específico por su ID
    /// </summary>
    [HttpGet("BuscarPlan/{id:guid}", Name = "BuscarPlan")]
    [ProducesResponseType(typeof(PlanResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> BuscarPlan(Guid id)
    {
        try
        {
            var commandToHandle = new BuscarPlanQuery(id);

            var plan = await _planService.Handle(commandToHandle);

            if (plan == null) return NotFound();

            return Ok(plan);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest("Error al buscar plan: " + ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, "Error al buscar plan: " + ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Obtiene una lista paginada de planes
    /// </summary>
    [HttpGet("ListarPlanes", Name = "ListarPlanes")]
    [ProducesResponseType(typeof(List<PlanResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ListarPlanes([FromQuery] ListarPlanesQuery query)
    {
        try
        {
            var planes = await _planService.Handle(query);
            return Ok(planes);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (PersistenceExeption ex)
        {
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
