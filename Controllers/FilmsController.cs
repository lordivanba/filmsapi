using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using filmsapi.domain.dtos.requests;
using filmsapi.domain.dtos.responses;
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
        private readonly IDirectorSqlRepository _repository2;
        private readonly IMapper _mapper;

        public FilmsController(IFilmSqlRepository repository, IMapper mapper, IDirectorSqlRepository repository2)
        {
            _repository = repository;
            _mapper = mapper;
            _repository2 = repository2;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {

            var films = await _repository.GetFilms();
            var response = _mapper.Map<IEnumerable<Film>,IEnumerable<FilmResponse>>(films);
        
            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetFilmById([FromRoute] int id)
        {

            var film = await _repository.GetFilmById(id);
            if (film == null)
                return NotFound("No se ha encontrado un film que corresponda con el ID proporcionado");
            var response = _mapper.Map<Film, FilmResponse>(film);
            return Ok(response);
        }

        [HttpGet]
        [Route("director/{id::int}")]
        public async Task<IActionResult> GetDirectorFilms([FromRoute] int id ){
            var directorFilms = await _repository.GetDirectorFilms(id);
            if(!directorFilms.Any())
                return NotFound("No se ha encontrado un director que corresponda con el ID proporcionado o el director no tiene films registrados");
            var response = _mapper.Map<IEnumerable<Film>,IEnumerable<FilmResponse>>(directorFilms);

            // var director = await _repository2.GetDirectorById(id);

            // var respuesta = new DirectorFilmsResponse();
            // respuesta.Director = $"{director.Nombre} {director.Apellido}";
            // respuesta.Films = response;
            
            // var director = await _repository2.GetDirectorById(id);

            // var respuesta = new DirectorFilmsResponse();
            // respuesta.Director = $"{director.Nombre} {director.Apellido}";
            // respuesta.Films = response;
            return Ok(response);
        }        

        [HttpPost]
        public async Task<IActionResult> CreateFilm([FromBody] FilmCreateRequest film)
        {
        
            int id;
            try
            {
                var obj = _mapper.Map<FilmCreateRequest, Film>(film);
                id = await _repository.CreateFilm(obj);
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
        public async Task<IActionResult> UpdateFilm([FromRoute] int id, [FromBody] FilmUpdateRequest film)
        {

            if (id <= 0)
                return NotFound("No se encontro un registro que coincida con la informacion proporcionada");
            if (film == null)
                return UnprocessableEntity("La actualizacion no puede ser realizada a falta de informacion");
            film.Id = id;

            try
            {
                var obj = _mapper.Map<FilmUpdateRequest, Film>(film);
                var result = await _repository.UpdateFilm(id, obj);

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
