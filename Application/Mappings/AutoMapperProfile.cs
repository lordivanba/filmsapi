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
            CreateMap<Film, FilmResponse>().ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Director.Nombre} {src.Director.Apellido}"));
            CreateMap<FilmCreateRequest, Film>();
            CreateMap<FilmUpdateRequest, Film>();

            //Directors   
            CreateMap<Director, DirectorResponse>();
            CreateMap<DirectorCreateRequest, Director>();
            CreateMap<DirectorUpdateRequest, Director>();

            //DirectorFilms
            // CreateMap<Film, DirectorFilmsResponse>().ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Director.Nombre} {src.Director.Apellido}")).ForMember(dest => dest.Films, opt => opt.MapFrom(src => src));

            CreateMap<Film, DirectorFilmsResponse>().ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Director.Nombre} {src.Director.Apellido}")).ForMember(dest => dest.Films, opt => opt.MapFrom(src => src));

            CreateMap<Director, DirectorFilmsResponse>().ForMember(dest => dest.Director, opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}")).ForMember(dest => dest.Films, opt => opt.MapFrom(src => src.Films));

        }
    }
}