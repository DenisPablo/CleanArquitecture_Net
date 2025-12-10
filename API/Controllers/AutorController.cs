using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Application.Queries;
using BibliotecaDigital.Application.Services;
using BibliotecaDigital.Application.Response;
using BibliotecaDigital.Domain.Exceptions;
using BibliotecaDigital.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaDigital.API.Controllers;

/// <summary>
/// Controlador para gestionar operaciones relacionadas con autores
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AutorController(AutorService autorService): ControllerBase
{
    private readonly AutorService _autorService = autorService;

    /// <summary>
    /// Crea un nuevo autor en el sistema
    /// </summary>
    /// <param name="command">Datos del autor a crear (Nombre, Apellido, FechaNacimiento)</param>
    /// <returns>El autor creado con su ID asignado</returns>
    /// <response code="201">Autor creado exitosamente</response>
    /// <response code="400">Error de validación en los datos proporcionados</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpPost("CrearAutor", Name = "CrearAutor")]
    [ProducesResponseType(typeof(Autor), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CrearAutor(CrearAutorCommand command)
    {
        try{
            var autor = await _autorService.Handle(command);

            return CreatedAtAction(nameof(BuscarAutor), new { id = autor!.Id }, autor);
            }
        catch(DomainValidationException ex){
            return BadRequest("Error al crear autor: " + ex.Message);
            }
        catch(PersistenceExeption ex){
            return StatusCode(500, "Error al crear autor: " + ex.Message);
            }
        catch(Exception ex){
            return StatusCode(500, ex.Message);
            }
    }

    /// <summary>
    /// Actualiza los datos de un autor existente
    /// </summary>
    /// <param name="id">ID único del autor a actualizar</param>
    /// <param name="command">Nuevos datos del autor (Nombre, Apellido, FechaNacimiento)</param>
    /// <returns>El autor actualizado</returns>
    /// <response code="200">Autor actualizado exitosamente</response>
    /// <response code="400">Error de validación en los datos proporcionados</response>
    /// <response code="404">Autor no encontrado</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpPut("ActualizarAutor/{id:guid}", Name = "ActualizarAutor")]
    [ProducesResponseType(typeof(AutorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ActualizarAutor(Guid id, ActualizarAutorCommand command)
    {
        try{
            var commandToHandle = new ActualizarAutorCommand(id, command.Nombre, command.Apellido, command.FechaNacimiento);
            var autor = await _autorService.Handle(commandToHandle);

            return Ok(autor);
            }
        catch(DomainValidationException ex){
            return BadRequest("Error al actualizar autor: " + ex.Message);
            }
        catch(PersistenceExeption ex){
            return StatusCode(500, "Error al actualizar autor: " + ex.Message);
            }
        catch(Exception ex){
            return StatusCode(500, "Error al actualizar autor: " + ex.Message);
            }
    }

    /// <summary>
    /// Desactiva un autor (eliminación lógica)
    /// </summary>
    /// <param name="id">ID único del autor a eliminar</param>
    /// <returns>El autor desactivado</returns>
    /// <response code="200">Autor desactivado exitosamente</response>
    /// <response code="400">Error de validación</response>
    /// <response code="404">Autor no encontrado</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpDelete("EliminarAutor/{id:guid}", Name = "EliminarAutor")]
    [ProducesResponseType(typeof(AutorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> EliminarAutor(Guid id)
    {
        try{
            var commandToHandle = new EliminarAutorCommand(id);

            var autor = await _autorService.Handle(commandToHandle);
            return Ok(autor);
            }
        catch(DomainValidationException ex){
            return BadRequest(ex.Message);
            }
        catch(PersistenceExeption ex){
            return StatusCode(500, ex.Message);
            }
        catch(Exception ex){
            return StatusCode(500, ex.Message);
            }
    }

    /// <summary>
    /// Busca un autor específico por su ID
    /// </summary>
    /// <param name="id">ID único del autor a buscar</param>
    /// <returns>Los datos del autor solicitado</returns>
    /// <response code="200">Autor encontrado exitosamente</response>
    /// <response code="400">Error de validación</response>
    /// <response code="404">Autor no encontrado</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpGet("BuscarAutor/{id:guid}", Name = "BuscarAutor")]
    [ProducesResponseType(typeof(AutorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> BuscarAutor(Guid id)
    {
        try{
            var commandToHandle = new BuscarAutorQuery(id);

            var autor = await _autorService.Handle(commandToHandle);
            return Ok(autor);
            }
        catch(DomainValidationException ex){
            return BadRequest("Error al buscar autor: " + ex.Message);
            }
        catch(PersistenceExeption ex){
            return StatusCode(500, "Error al buscar autor: " + ex.Message);
            }
        catch(Exception ex){
            return StatusCode(500, ex.Message);
            }
    }

    /// <summary>
    /// Obtiene una lista paginada de autores
    /// </summary>
    /// <param name="query">Parámetros de paginación (PageNumber y PageSize). Si no se especifican, usa valores por defecto: Página 1, Tamaño 10</param>
    /// <returns>Lista de autores según los parámetros de paginación</returns>
    /// <response code="200">Lista de autores obtenida exitosamente</response>
    /// <response code="400">Error de validación en los parámetros</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Ejemplo de uso:
    /// 
    ///     GET /api/Autor/ListarAutores?PageNumber=1&amp;PageSize=10
    /// 
    /// Si no se proporcionan parámetros, se usarán los valores por defecto (Página 1, 10 elementos)
    /// </remarks>
    [HttpGet("ListarAutores", Name = "ListarAutores")]
    [ProducesResponseType(typeof(List<AutorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ListarAutores([FromQuery] ListarAutoresQuery query)
    {
        try{
            var autores = await _autorService.Handle(query);
            return Ok(autores);
            }
        catch(DomainValidationException ex){
            return BadRequest(ex.Message);
            }
        catch(PersistenceExeption ex){
            return StatusCode(500, ex.Message);
            }
        catch(Exception ex){
            return StatusCode(500, ex.Message);
            }
    
    }
}
