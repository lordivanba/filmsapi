using AutoMapper;
using filmsapi.domain.dtos.requests;
using filmsapi.domain.dtos.responses;
using filmsapi.domain.entities;

namespace filmsapi.application.mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Films
            CreateMap<Film, FilmResponse>()
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Director.Nombre} {src.Director.Apellido}"));
            CreateMap<FilmCreateRequest, Film>();
            CreateMap<FilmUpdateRequest, Film>();

            //Directors   
            CreateMap<Director, DirectorResponse>();
            CreateMap<DirectorCreateRequest, Director>();
            CreateMap<DirectorUpdateRequest, Director>();

            //DirectorFilms
            CreateMap<Director, DirectorFilmsResponse>()
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"))
                .ForPath(dest => dest.Films, opt => opt.MapFrom(src => src.Films));
        
            CreateMap<Film, FilmAltResponse>()
                .ForMember(dest => dest.Film, opt => opt.MapFrom(src => $"{src.Titulo} ({src.YearPublicacion})"));
        }
    }
}