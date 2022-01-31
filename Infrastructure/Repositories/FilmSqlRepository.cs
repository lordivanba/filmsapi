
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filmsapi.domain.entities;
using filmsapi.domain.infrastrucutre.data;
using filmsapi.domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace filmsapi.infrastructure.repositories
{
    public class FilmSqlRepository : IFilmSqlRepository
    {
        private readonly filmsdbContext _context;

        public FilmSqlRepository()
        {
            _context = new filmsdbContext();
        }

        public async Task<IEnumerable<Film>> GetFilms()
        {

            var query = _context.Films.Include(x => x.Director);
            return await query.ToListAsync();
        }

        public async Task<Film> GetFilmById(int id)
        {
            var query = _context.Films.Include(x => x.Director).FirstOrDefaultAsync(x => x.Id == id);
            return await query;
        }

        // public async Task<IEnumerable<Film>> GetDirectorFilms(int id){
        //     var query = _context.Films.Include(x => x.Director).Where(x => x.DirectorId == id).ToListAsync();

        //     return await query;
        // }

        public async Task<int> CreateFilm(Film film)
        {
            if (film == null)
                throw new ArgumentNullException("No se pudo registrar el film a falta de informacion");
            try
            {
                var entity = film;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();

                if (rows <= 0)
                    throw new Exception("Ocurrio un fallo al intentar registrar el film, verifica tu informacion");

                return entity.Id;
            }
            catch (DbUpdateException exEf)
            {
                throw new Exception("No se pudo registrar el film a falta de informacion");
            }
        }

        public async Task<bool> UpdateFilm(int id, Film film)
        {
            if (id <= 0 || film == null)
                throw new ArgumentNullException("La actualizacion no se pudo realizar a falta de informacion");

            var entity = await GetFilmById(id);

            entity.Titulo = film.Titulo;
            entity.DirectorId = film.DirectorId;
            entity.Genero = film.Genero;
            entity.Puntuacion = film.Puntuacion;
            entity.Rating = film.Rating;
            entity.YearPublicacion = film.YearPublicacion;

            _context.Update(entity);

            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> DeleteFilm(int id){
            if(id <= 0)
                throw new ArgumentNullException("No se pudo eliminar el registro");

            var film = await GetFilmById(id);
            try{
                _context.Remove(film);
                await _context.SaveChangesAsync();

                return true;
            } catch(Exception e){
                throw new Exception("No se pudo eliminar el registro");
            }
        }
    }
}