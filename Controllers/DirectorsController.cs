using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using filmsapi.domain.dtos.requests;
using filmsapi.domain.dtos.responses;
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
        private readonly IMapper _mapper;


        public DirectorsController(IDirectorSqlRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetDirectors()
        {

            var directors = await _repository.GetDirectors();
            var response = _mapper.Map<IEnumerable<Director>, IEnumerable<DirectorResponse>>(directors);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetDirectorById([FromRoute] int id)
        {
            var director = await _repository.GetDirectorById(id);
            if (director == null)
                return NotFound("No se ha encontrado un director que corresponda con el ID proporcionado");

            var response = _mapper.Map<Director, DirectorResponse>(director);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}/films")]
        public async Task<IActionResult> GetDirectorFilms([FromRoute] int id)
        {
            var director = await _repository.GetDirectorFilms(id);
            if (director == null)
                return NotFound("No se ha encontrado un director que corresponda con el ID proporcionado");

            // var films = _mapper.Map<IEnumerable<Film>, IEnumerable<FilmAltResponse>>(director.Films);
            var response = _mapper.Map<Director, DirectorFilmsResponse>(director);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirector([FromBody] DirectorCreateRequest director)
        {
            int id;
            try
            {
                var obj = _mapper.Map<DirectorCreateRequest, Director>(director);
                id = await _repository.CreateDirector(obj);
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
        public async Task<IActionResult> UpdateDirector([FromRoute] int id, [FromBody] DirectorUpdateRequest director)
        {

            if (id <= 0)
                return NotFound("No se encontro un registro que coincida con la informacion proporcionada");
            if (director == null)
                return UnprocessableEntity("La actualizacion no puede ser realizada a falta de informacion");
            director.Id = id;

            try
            {
                var obj = _mapper.Map<DirectorUpdateRequest, Director>(director);
                var result = await _repository.UpdateDirector(id, obj);

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

            try
            {
                await _repository.DeleteDirector(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}