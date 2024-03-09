using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ApiJuegos.Utilidades
{
    // Clase estática que contiene una extensión para HttpContext
    public static class HttpContextExtensions
    {
        // Método de extensión para insertar el parámetro de paginación en la cabecera de la respuesta HTTP
        public static async Task InsertarParamPaginacionCabecera<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            // Verifica si el objeto HttpContext es nulo
            if (httpContext is null)
            {
                // Lanza una excepción si es nulo
                throw new ArgumentNullException(nameof(httpContext));
            }

            // Obtiene la cantidad total de elementos en el IQueryable de forma asíncrona
            double cantidad = await queryable.CountAsync();
            // Añade un encabezado a la respuesta HTTP con la cantidad total de registros
            httpContext.Response.Headers.Append("cantidadTotalRegistros", cantidad.ToString());
        }
    }
}
