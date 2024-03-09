using ApiJuegos.Utilidades;
using Microsoft.IdentityModel.Tokens;

namespace ApiJuegos.DTOs
{
    public class PaginacionDTO
    {
        private const int pageValorInicial = 1;
        private const int recordsPageValorInicial = 10;
        public int Pagina { get; set; } = pageValorInicial;
        private int recordsXpagina = recordsPageValorInicial;
        private readonly int cantidadMaxRecordsXpagina = 50;


        public int RecordsXpagina
        {
            get
            {
                return recordsXpagina; 
            }
            set
            {
                recordsXpagina = (value > cantidadMaxRecordsXpagina) ? cantidadMaxRecordsXpagina : value;
            }
        }

        // Este es un método que ayuda a preparar la información de paginación para nuestra aplicación web
        public static ValueTask<PaginacionDTO> BindAsync(HttpContext context)
        {
            // Aquí estamos buscando un número de página en la solicitud que nos envió el usuario.
            // Si no podemos encontrarlo, simplemente usamos el número 1 como nuestro número de página.
            var pagina = context.ExtraerValorXdefecto(nameof(Pagina), pageValorInicial);
            // Aquí estamos buscando cuántos registros por página quiere el usuario en la solicitud.
            // Si no podemos encontrarlo, simplemente usamos el número 10 como nuestro número de registros por página.
            var recordsXpagina = context.ExtraerValorXdefecto(nameof(RecordsXpagina), recordsPageValorInicial);

            // Ahora, con el número de página y el número de registros por página que encontramos(o los valores por defecto que establecimos),
            // vamos a crear una nueva "hoja de paginación" para nuestra aplicación web.
            
            var resultado = new PaginacionDTO {
                Pagina = pagina, 
                RecordsXpagina = recordsXpagina
            };

            // Finalmente, devolvemos nuestra "hoja de paginación"
            // para que nuestra aplicación web pueda usarla para mostrar la información correcta al usuario.
            return ValueTask.FromResult(resultado);
        }


    }
}
