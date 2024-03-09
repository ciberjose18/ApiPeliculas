
using ApiJuegos.Repositorios;
using AutoMapper;

namespace ApiJuegos.Filtros
{
    public class FiltroDePrueba : IEndpointFilter
    {
        // Este es el método InvokeAsync que se llama cuando se ejecuta un endpoint.
        // Recibe un contexto que contiene información sobre la solicitud HTTP y un delegado next que representa
        // el próximo filtro o endpoint en la cadena de ejecución.
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //Este codigo se ejecuta antes de que se ejecute el endpoint
            // Aquí se pone cualquier lógica que quiera ejecutar antes del endpoint,
            // como la autorización, la manipulación de errores, el registro, etc.
            var paramRepositorioGenero = context.Arguments.OfType<IRepositorioGeneros>().FirstOrDefault();
            var paramInt = context.Arguments.OfType<int>().FirstOrDefault();
            var paramRMapper = context.Arguments.OfType<IMapper>().FirstOrDefault();


            var resultado = await next(context);
            //este codigo se ejecuta despues de que se ejecuta el endpoint
            // Aquí se pone cualquier lógica que se quiera ejecutar después del endpoint, como la manipulación de errores, el registro, etc.



            // Devolvemos el resultado del endpoint.
            // Esto será enviado de vuelta al cliente como la respuesta HTTP.
            return resultado;
        }
    } 
}
