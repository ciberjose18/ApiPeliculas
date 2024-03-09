using ApiJuegos.Entidades;
using Error = ApiJuegos.Entidades.Error;


namespace ApiJuegos.Repositorios
{
    public class RepositorioErrores : IRepositorioErrores
    {
        private readonly ApplicationDbContext context;

        public RepositorioErrores(ApplicationDbContext contexto)
        {
            context = contexto;
        }

        public async Task Crear(Error error)
        {
            // La línea 'context.Add(error);' está añadiendo el objeto 'error' al contexto de la base de datos.
            // Esto no significa que el error se haya guardado en la base de datos, simplemente que está listo para ser guardado.
            context.Add(error);
            await context.SaveChangesAsync();
        }
    }
}
