using ApiJuegos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiJuegos.Repositorios
{
    public class RepositorioComentarios : IRepositorioComentarios
    {
        private readonly ApplicationDbContext context;

        public RepositorioComentarios(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Comentario>> ObtenerTodos(int peliculaId)
        {
            return await context.Comentarios.Where(x => x.PeliculaId == peliculaId).ToListAsync();
        }

        public async Task<Comentario?> ObtenerPorId(int id)
        {
            return await context.Comentarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Crear(Comentario comentario)
        {
            context.Add(comentario);
            await context.SaveChangesAsync();
            return comentario.Id;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Comentarios.AnyAsync(x => x.Id == id);
        }
        public async Task Borrar(int id)
        {
            await context.Comentarios.Where(d => d.Id == id).ExecuteDeleteAsync();
        }
        public async Task Actualizar(Comentario comentario)
        {
            context.Update(comentario);
            await context.SaveChangesAsync();
        }


    }
}
