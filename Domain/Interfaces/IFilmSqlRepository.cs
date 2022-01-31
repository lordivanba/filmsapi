
using System.Collections.Generic;
using System.Threading.Tasks;
using filmsapi.domain.entities;

namespace filmsapi.domain.interfaces{
    public interface IFilmSqlRepository{
        Task<IEnumerable<Film>> GetFilms();
        Task<Film> GetFilmById(int id);
        Task<IEnumerable<Film>> GetDirectorFilms(int id);
        Task<int> CreateFilm(Film film);
        Task<bool> UpdateFilm(int id, Film film);
        Task<bool> DeleteFilm(int id);
    }
}