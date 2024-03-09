using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace ApiJuegos.Swagger
{
    //Esta clase sirve para agregar un requisito de seguridad solo a los endpoints que tengan el atributo RequireAuthorization

    //Se utiliza la interfaz IOperationFilter para poder implementar el método Apply
    public class FiltroAutorizacion : IOperationFilter
    {
        // Este es el método Apply, donde se realiza toda la comprobación. Es parte de la interfaz.
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // comprobamos si el endpoint tiene el atributo AuthorizeAttribute osea el RequireAuthorization
            if (!context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<AuthorizeAttribute>().Any())
            {
                // Si no lo tiene, simplemente terminamos el método y no hacemos nada más.
                return;
            }
            // Si la operación tiene el atributo AuthorizeAttribute, entonces añadimos un requisito de seguridad.
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                         // Este es el tipo de seguridad que estamos añadiendo. En este caso, es un token Bearer.
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new string[]{}
                    }
                }
            };

        }
    }
}
