using ApiJuegos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ApiJuegos.Repositorios
{
    public class RepositorioGeneros : IRepositorioGeneros
    {
        private ApplicationDbContext context;

        public RepositorioGeneros(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Genero?> ObtenerPorId(int id)
        {
            return await context.Generos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Genero>> ObtenerTodos()
        {
            return await context.Generos.OrderBy(x => x.Id).ToListAsync();  // el orderby es para ordenar en este caso ordenar por el nombre
        }


        public async Task<int> Crear(Genero genero)
        {
            context.Add(genero);
            await context.SaveChangesAsync();
            return genero.Id;
        }

        public async Task<bool> YaExiste(int id, string nombre)
        {
            return await context.Generos.AnyAsync(x => x.Id != id && x.Nombre == nombre);
        }

       /* public async Task<bool> YaExiste2(string nombre, CancellationToken cancellation)
        * esta es otra forma de hacer la validacion de si existe un genero con el mismo nombre
        {
            return await context.Generos.AllAsync(n => n.Nombre != nombre, cancellation);  // el metodo AllAsync nos permite verificar si todos los elementos de la lista cumplen con una condición
        }*/

        public async Task<bool> Existe(int id)
        {
            return await context.Generos.AnyAsync(x => x.Id == id);
        }

        public async Task Actualizar(Genero genero)
        {
            context.Update(genero);
            await context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            await context.Generos.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        // La tarea devolverá una lista de números enteros (List<int>).
        public async Task<List<int>> Existen(List<int> ids)
        {
            // Buscamos en la base de datos los géneros que coincidan con los IDs de la lista
            // "Where" nos permite filtrar los resultados
            // De la lista de géneros encontrados, seleccionamos solo los IDs
            // "Select" nos permite transformar la lista
            return await context.Generos.Where(g => ids.Contains(g.Id)).Select(g => g.Id).ToListAsync();
        }
    }
}
