using System.Threading.Tasks;
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
        public async Task<IActionResult> GetFilms(){

            var films = await _repository.GetFilms();
            
            return Ok(films);
        }
    }
}
