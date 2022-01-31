
namespace filmsapi.domain.dtos.requests
{
    public class FilmResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Genero { get; set; }
        public decimal? Puntuacion { get; set; }
        public string Rating { get; set; }
        public string YearPublicacion { get; set; }
    }
}