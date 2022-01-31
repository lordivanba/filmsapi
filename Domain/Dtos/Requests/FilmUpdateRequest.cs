
namespace filmsapi.domain.dtos.requests
{
    public class FilmUpdateRequest
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int? DirectorId { get; set; }
        public string Genero { get; set; }
        public decimal? Puntuacion { get; set; }
        public string Rating { get; set; }
        public string YearPublicacion { get; set; }
    }
}