namespace filmsapi.domain.dtos.requests
{
    public class DirectorUpdateRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Nacionalidad { get; set; }
    }
}