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
    public class DirectorSqlRepository : IDirectorSqlRepository
    {
        private readonly filmsdbContext _context;

        public DirectorSqlRepository()
        {
            _context = new filmsdbContext();
        }

        public async Task<IEnumerable<Director>> GetDirectors()
        {

            var query = _context.Directors.Select(x => x);

            return await query.ToListAsync();
        }

        public async Task<Director> GetDirectorById(int id)
        {
            var query = _context.Directors.FindAsync(id);
            return await query;
        }


        public async Task<int> CreateDirector(Director director)
        {
            if (director == null)
                throw new ArgumentNullException("No se pudo registrar el director a falta de informacion");
            try
            {
                var entity = director;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();

                if (rows <= 0)
                    throw new Exception("Ocurrio un fallo al intentar registrar el director, verifica tu informacion");

                return entity.Id;
            }
            catch (DbUpdateException exEf)
            {
                throw new Exception("No se pudo registrar el director a falta de informacion");
            }
        }

        public async Task<bool> UpdateDirector(int id, Director director)
        {
            if (id <= 0 || director == null)
                throw new ArgumentNullException("La actualizacion no se pudo realizar a falta de informacion");

            var entity = await GetDirectorById(id);

            entity.Nombre = director.Nombre;
            entity.Apellido = director.Apellido;
            entity.Nacionalidad = director.Nacionalidad;

            _context.Update(entity);

            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> DeleteDirector(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException("No se pudo eliminar el registro");

            var director = await GetDirectorById(id);
            try
            {
                _context.Remove(director);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo eliminar el registro");
            }
        }
    }
}