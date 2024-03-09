using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System;

namespace ApiJuegos.Utilidades
{
    public static class SwaggerExtensions
    {
        //TBuilder es un tipo genérico usado para reprensentar un objeto de un endpoint
        public static TBuilder AgregarParametrosPeliculasFiltroAOpenAPI<TBuilder>(this TBuilder builder)
            where TBuilder : IEndpointConventionBuilder      // Nos aseguramos de que TBuilder sea un constructor de convenciones endpoint

        {
            // Devolvemos el objeto builder con nuevas opciones de API
            return builder.WithOpenApi(opciones =>
            {
                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "pagina",
                    In = ParameterLocation.Query,// El parámetro se encuentra en la consulta
                    Schema = new OpenApiSchema // Definimos el esquema del parámetro
                    {
                        Type = "integer",
                        Default = new OpenApiInteger(1)   // El valor por defecto
                    }
                });

                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "recordsXpagina",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "integer",
                        Default = new OpenApiInteger(10)
                    }
                });

                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "titulo",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                    }
                });
                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "enCines",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "boolean",
                    }
                });

                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "proximosEstrenos",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "boolean",
                    }
                });

                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "generoId",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "integer",
                    }
                });

                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "campoOrdenar",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = new List<IOpenApiAny> // Lista de valores permitidos
                        { new OpenApiString("titulo"), new OpenApiString("fechaLanzamiento") }
                    }
                });

                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "ordenAscendente",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "boolean",
                        Default = new OpenApiBoolean(true)
                    }
                });



                return opciones;
            });

        }

        public static TBuilder AgregarParametrosPaginacionAOpenAPI<TBuilder>(this TBuilder builder)
           where TBuilder : IEndpointConventionBuilder      // Nos aseguramos de que TBuilder sea un constructor de convenciones endpoint

        {
            // Devolvemos el objeto builder con nuevas opciones de API
            return builder.WithOpenApi(opciones =>
            {
                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "pagina",
                    In = ParameterLocation.Query,// El parámetro se encuentra en la consulta
                    Schema = new OpenApiSchema // Definimos el esquema del parámetro
                    {
                        Type = "integer",
                        Default = new OpenApiInteger(1)   // El valor por defecto
                    }
                });

                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "recordsXpagina",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "integer",
                        Default = new OpenApiInteger(10)
                    }
                });

                return opciones;
            });

        }
    }
}
