using System;
using System.Threading.Tasks;
using filmsapi.domain.entities;
using filmsapi.domain.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace filmsapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmSqlRepository _repository;

        public FilmsController(IFilmSqlRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {

            var films = await _repository.GetFilms();

            return Ok(films);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetFilmById([FromRoute] int id)
        {

            var film = await _repository.GetFilmById(id);
            if (film == null)
                return NotFound("No se ha encontrado un film que corresponda con el ID proporcionado");

            return Ok(film);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFilm([FromBody] Film film)
        {

            int id;
            try
            {
                id = await _repository.CreateFilm(film);
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
        public async Task<IActionResult> UpdateFilm([FromRoute] int id, [FromBody] Film film)
        {

            if (id <= 0)
                return NotFound("No se encontro un registro que coincida con la informacion proporcionada");
            if (film == null)
                return UnprocessableEntity("La actualizacion no puede ser realizada a falta de informacion");
            film.Id = id;

            try
            {
                var result = await _repository.UpdateFilm(id, film);

                if (!result)
                    return Conflict("No fue posible realizar la actualización, verifica tu información");
            }
            catch (Exception e)
            {
                return NotFound("No se ha encontrado un film que corresponda con el ID proporcionado");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id::int}")]
        public async Task<IActionResult> DeleteFilm([FromRoute] int id)
        {
            var film = await _repository.GetFilmById(id);
            if (film == null)
                return NotFound("No se ha encontrado un film que corresponda con el ID proporcionado");

            try
            {
                await _repository.DeleteFilm(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
