using System;
using System.Threading.Tasks;
using filmsapi.domain.entities;
using filmsapi.domain.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace filmsapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorSqlRepository _repository;

        public DirectorsController(IDirectorSqlRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> GetDirectors()
        {

            var directors = await _repository.GetDirectors();

            return Ok(directors);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetDirectorById([FromRoute] int id)
        {
            var director = await _repository.GetDirectorById(id);
            if (director == null)
                return NotFound("No se ha encontrado un director que corresponda con el ID proporcionado");
            return Ok(director);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirector([FromBody] Director director)
        {
            int id;
            try
            {
                id = await _repository.CreateDirector(director);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            if (id <= 0)
                return Conflict("El registro no puede ser realizado, verifica tu información");
            return Ok();
        }

        [HttpPut]
        [Route("{id::int}")]
        public async Task<IActionResult> UpdateDirector([FromRoute] int id, [FromBody] Director director)
        {

            if (id <= 0)
                return NotFound("No se encontro un registro que coincida con la informacion proporcionada");
            if (director == null)
                return UnprocessableEntity("La actualizacion no puede ser realizada a falta de informacion");
            director.Id = id;

            try
            {
                var result = await _repository.UpdateDirector(id, director);

                if (!result)
                    return Conflict("No fue posible realizar la actualización, verifica tu información");
            }
            catch (Exception e)
            {
                 return NotFound("No se ha encontrado un director que corresponda con el ID proporcionado");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id::int}")]
        public async Task<IActionResult> DeleteDirector([FromRoute] int id)
        {
            var director = await _repository.GetDirectorById(id);
            if (director == null)
                return NotFound("No se ha encontrado un director que corresponda con el ID proporcionado");
                
            try{
                await _repository.DeleteDirector(id);
                return NoContent();
            } catch(Exception e){
                return BadRequest();
            }
        }
    }
}