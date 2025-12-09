using BibliotecaDigital.Application.Commands;
using BibliotecaDigital.Application.Exeptions;
using BibliotecaDigital.Application.Queries;
using BibliotecaDigital.Application.Services;
using BibliotecaDigital.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaDigital.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AutorController(AutorService autorService): ControllerBase
{
    public readonly AutorService _autorService = autorService;

    [HttpPost]
    public async Task<ActionResult> CrearAutor(CrearAutorCommand command)
    {
        try{
            var autor = await _autorService.Handle(command);
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

    [HttpPatch]
    public async Task<ActionResult> ActualizarAutor(ActualizarAutorCommand command)
    {
        try{
            var autor = await _autorService.Handle(command);

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

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> EliminarAutor(Guid id)
    {
        try{
            var command = new EliminarAutorCommand(id);

            var autor = await _autorService.Handle(command);
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

    [HttpGet]
    public async Task<ActionResult> BuscarAutor(BuscarAutorQuery query)
    {
        try{
            var autor = await _autorService.Handle(query);
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

    [HttpGet]
    public async Task<ActionResult> ListarAutores(ListarAutoresQuery query)
    {
        try{
            var autor = await _autorService.Handle(query);
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
}