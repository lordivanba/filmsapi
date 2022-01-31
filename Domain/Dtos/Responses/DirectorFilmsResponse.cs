using System.Collections.Generic;
using filmsapi.domain.entities;

namespace filmsapi.domain.dtos.responses{
    public class DirectorFilmsResponse{
        public string Director { get; set; }
        public object Films { get; set; }
    }
}