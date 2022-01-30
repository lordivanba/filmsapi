using System;
using System.Collections.Generic;

#nullable disable

namespace filmsapi.domain.entities
{
    public partial class Film
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int? DirectorId { get; set; }
        public string Genero { get; set; }
        public decimal? Puntuacion { get; set; }
        public string Rating { get; set; }
        public string YearPublicacion { get; set; }

        public virtual Director Director { get; set; }
    }
}
