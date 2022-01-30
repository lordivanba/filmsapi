using System;
using System.Collections.Generic;

#nullable disable

namespace filmsapi.domain.entities
{
    public partial class Director
    {
        public Director()
        {
            Films = new HashSet<Film>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Nacionalidad { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
