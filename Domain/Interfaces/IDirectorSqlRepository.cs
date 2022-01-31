using System.Collections.Generic;
using System.Threading.Tasks;
using filmsapi.domain.entities;

namespace  filmsapi.domain.interfaces{
    public interface IDirectorSqlRepository{
         Task<IEnumerable<Director>> GetDirectors();
         Task<Director> GetDirectorById(int id);
         Task<Director> GetDirectorFilms(int id);
         Task<int> CreateDirector(Director director);
         Task<bool> UpdateDirector(int id, Director director);
         Task<bool> DeleteDirector(int id);
    }
}