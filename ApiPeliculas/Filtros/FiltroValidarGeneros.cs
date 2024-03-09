
using ApiJuegos.DTOs;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiJuegos.Filtros
{
    public class FiltroValidarGeneros : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            // Obtenemos el validador para CrearGeneroDTO del contenedor de servicios.
            var validator = context.HttpContext.RequestServices.GetService<IValidator<CrearGeneroDTO>>();
            // Si no hay un validador para CrearGeneroDTO, continuamos con el siguiente filtro o endpoint.
            if (validator is null)
            {
                return await next(context);
            }
            // Obtenemos el CrearGeneroDTO de los argumentos del endpoint.
            //context.Arguments es una colección que contiene todos los argumentos que se pasan al endpoint. Estos argumentos son los datos de entrada que el cliente envía en la solicitud HTTP.
            //OfType<CrearGeneroDTO>() es un método de LINQ que filtra la colección context.Arguments para incluir solo los elementos que son del tipo CrearGeneroDTO.Esto devuelve una colección de elementos CrearGeneroDTO.
            //FirstOrDefault() es otro método de LINQ que obtiene el primer elemento de la colección filtrada. Si la colección no tiene elementos(es decir, si no hay argumentos de tipo CrearGeneroDTO), FirstOrDefault() devuelve null.
            var insumeValidacion = context.Arguments.OfType<CrearGeneroDTO>().FirstOrDefault();
            // Si no hay un CrearGeneroDTO, devolvemos un problema indicando que no se pudo encontrar la entidad a validar.
            if (insumeValidacion is null)
            {
                return TypedResults.Problem("No pudo ser encontrada la entidad a validad");
            }

            // Validamos el CrearGeneroDTO.
            var resultadoValida = await validator.ValidateAsync(insumeValidacion);

            if (!resultadoValida.IsValid)
            {
                return TypedResults.ValidationProblem(resultadoValida.ToDictionary());
            }
            // Si la validación es exitosa, continuamos con el siguiente filtro o endpoint.
            return await next(context);
        }
    }
}
