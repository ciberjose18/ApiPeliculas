using Microsoft.IdentityModel.Tokens;
using static System.Net.WebRequestMethods;

namespace ApiJuegos.Utilidades
{
    // Declaramos una clase estática para poder usar sus métodos sin instanciarla
    public static class HttpContextExtensionsUtilidades
    {
        // Declaramos un método de extensión para la clase HttpContext
        public static T ExtraerValorXdefecto<T> (this HttpContext context, string nombreCampo, T valorDefecto)
            where T : IParsable<T> // Restringimos el tipo T a aquellos que implementen la interfaz IParsable
        {
            // Intentamos obtener el valor del parámetro de la solicitud HTTP
            var valor = context.Request.Query[nombreCampo];
            // Si el valor no existe o está vacío, devolvemos el valor por defecto
            if (valor.IsNullOrEmpty())
            {
                return valorDefecto;
            }
            // Si el valor existe, intentamos convertirlo al tipo T y lo devolvemos
            return T.Parse(valor!, null);
        }

        //Este código es una función que se utiliza para extraer un valor de tipo booleano de una solicitud HTTP.
        public static bool? ExtraerValorXdefectoBool(this HttpContext context, string nombreCampo)
        {
            //La función toma un parámetro, que es el nombre del valor que estamos filtrando.
            var valor = context.Request.Query[nombreCampo];

            // Comprobamos si el valor existe y si puede ser convertido a un booleano
            if (valor.Count > 0 && bool.TryParse(valor, out var valorBool))
            {
                // Si el valor existe y es un booleano, lo devolvemos
                return valorBool;
            }
            // Si el valor no existe o no es un booleano, devolvemos null
            return null;
        }
    }
}
