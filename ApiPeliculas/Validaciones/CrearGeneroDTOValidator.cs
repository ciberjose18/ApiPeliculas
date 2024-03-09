using ApiJuegos.DTOs;
using ApiJuegos.Repositorios;
using FluentValidation;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiJuegos.Validaciones
{
    // Esta es una clase de validación que hereda de AbstractValidator.
    // AbstractValidator es una clase de la biblioteca FluentValidation que permite definir reglas de validación para un tipo específico de objeto.
    // En este caso, estamos definiendo reglas para el objeto CrearGeneroDTO.
    public class CrearGeneroDTOValidator : AbstractValidator<CrearGeneroDTO>
    {
        // Este es el constructor de la clase CrearGeneroDTOValidator.
        // Dentro del constructor, definimos las reglas de validación para el objeto CrearGeneroDTO.
        public CrearGeneroDTOValidator(IRepositorioGeneros repositorio, IHttpContextAccessor contextAccessor)
        {
            // Aquí estamos obteniendo el valor de "id" de la ruta de la solicitud HTTP actual.
            // IHttpContextAccessor es una interfaz que permite acceder al contexto HTTP actual.
            // HttpContext.Request.RouteValues es un diccionario que contiene los valores de ruta de la solicitud HTTP actual, en este caso el id de la url.
            var valorDeLaRutaId = contextAccessor.HttpContext?.Request.RouteValues["id"];
            // Inicializamos la variable id con 0.
            var id = 0;
            //En el contexto de una solicitud HTTP, los valores de ruta y los parámetros de consulta siempre se reciben como strings.
            // Aquí estamos comprobando si el valor de la ruta "id" es un string.
            // Si es un string, intentamos convertirlo a un entero y lo almacenamos en la variable id.
            // int.TryParse es un método que intenta convertir un string a un entero. Si la conversión es exitosa,
            // el método devuelve true y almacena el entero en la variable de salida (en este caso, id).
            // Si la conversión no es exitosa, el método devuelve false y la variable de salida se establece en 0.
            if (valorDeLaRutaId is string valorString)
            {
                int.TryParse(valorString, out id);
            }


            // Aquí estamos definiendo una regla para la propiedad Nombre del objeto CrearGeneroDTO.
            // La regla dice que el campo Nombre no puede estar vacío (NotEmpty).
            // Si esta regla se viola, FluentValidation generará un mensaje de error que dice "El campo {PropertyName} es requerido".
            // {PropertyName} será reemplazado por el nombre de la propiedad que violó la regla, en este caso, Nombre.
            RuleFor(g => g.Nombre).NotEmpty().WithMessage(Utilidades.CampoRequeridoMS)
                .MaximumLength(50).WithMessage(Utilidades.MaxLengthMS)
                .Must(Utilidades.PrimeraLetraMayuscula).WithMessage(Utilidades.PrimeraLetraMayusculaMS)
                .MustAsync(async (nombre, _) =>
                {
                    // La función recibe dos parámetros: el valor de la propiedad que se está validando (nombre) y un token de cancelación (_).
                    var existe = await repositorio.YaExiste(id, nombre); // El 0 no es un id válido en la base de datos. Por lo tanto, la verificación se realizará en todos los géneros existentes.

                    return !existe;
                }).WithMessage(g => $"Ya existe un genero con el nombre {g.Nombre}"); 
            /* esta es otra forma de hacer la validacion de si existe un genero con el mismo nombre
             * .MustAsync((nombre, cancellation) => repositorio.YaExiste2(nombre, cancellation)).WithMessage(g => $"Ya existe un genero con el nombre {g.Nombre}");*/
            /**/
        }



    }
}
