using ApiJuegos;
using ApiJuegos.Endpoints;
using ApiJuegos.Entidades;
using ApiJuegos.Repositorios;
using ApiJuegos.Servicios;
using ApiJuegos.Swagger;
using ApiJuegos.Utilidades;
using ApiPeliculas.GraphQL;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;
using Error = ApiJuegos.Entidades.Error;


var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!;
//Inicio area servicios

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddGraphQLServer().AddQueryType<Query>()
    .AddAuthorization()
    .AddMutationType<Mutacion>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();
//Utilizando CORS

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(config =>
    {
        config.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });

    options.AddPolicy("libre", config =>
    {
        config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

});


//builder.Services.AddOutputCache();
builder.Services.AddStackExchangeRedisOutputCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(op =>
{
    op.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiPeliculas",
        Description = "Una API para el manejo de Peliculas, generos y actores",
        Contact = new OpenApiContact
        {
            Name = "Jose David Triana",
            Email = "trianajose1218@gmail.com",
            Url = new Uri("https://github.com/ciberjose18")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }

    });
    op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat ="JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization, para ingresar digite: Bearer + el Token de autorizacion."
    });

    //Llamamos al filtro de autorizacion
    op.OperationFilter<FiltroAutorizacion>();

    /*op.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });*/
});

//repositorios

builder.Services.AddScoped<IRepositorioGeneros, RepositorioGeneros>();
builder.Services.AddScoped<IRepositorioActores, RepositorioActores>();
builder.Services.AddScoped<IRepositorioPeliculas, RepositorioPeliculas>();
builder.Services.AddScoped<IRepositorioComentarios, RepositorioComentarios>();
builder.Services.AddScoped<IRepositorioErrores, RepositorioErrores>();
builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();

builder.Services.AddScoped<IAlmacenarArchivos, AlmacenadorArchivosLocal>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));


//FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();  //AgregarValidadoresDesdeElEnsambladoQueContiene

builder.Services.AddProblemDetails();
// Aquí estamos diciendo al autenticador cómo verificar las llaves.
builder.Services.AddAuthentication().AddJwtBearer(opciones => {
    // Aquí le decimos al autenticador que no debe mapear las reclamaciones que vienen en el token.
    opciones.MapInboundClaims = false;
    // Aquí le decimos al autenticador que debe buscar las llaves en el encabezado de autorización.
    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        // No necesitamos verificar quién emitió la llave.
        ValidateIssuer = false,
        // No necesitamos verificar a quién está destinada la llave.
        ValidateAudience = false,
        // Pero sí necesitamos verificar que la llave no haya expirado.
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //IssuerSigningKey = Llaves.ObtenerLlave(builder.Configuration).First(), Esta linea solo se una para una unica llave
        // Aquí le decimos al autenticador dónde puede encontrar todas las llaves.
        IssuerSigningKeys = Llaves.ObtenerTodasLasLlaves(builder.Configuration), //Esta linea se usa para multiples llaves
                                                                                 // Y finalmente, le decimos al auth que no debe haber ninguna diferencia de tiempo al verificar las llaves.
        ClockSkew = TimeSpan.Zero
    };


});
//Definir politicas de autorizacion
builder.Services.AddAuthorization(opciones =>
{
    // Aquí estamos definiendo una política llamada "admin". Esta política requiere que el usuario tenga una reclamación llamada "admin".
    opciones.AddPolicy("admin", policy => policy.RequireClaim("admin"));
});


//Fin area servicios


var app = builder.Build();

//Area de los middlewares

//if (builder.Environment.IsDevelopment())
//{
//Se encarga de manejar las excepciones no controladas que se producen durante el procesamiento de una solicitud HTTP.
// Por defecto, genera una respuesta con el código de estado HTTP 500 (Error interno del servidor).
app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context =>
{
    // Obtenemos los detalles de la excepción no controlada que ha ocurrido.
    var exceptionHandleFeature = context.Features.Get<IExceptionHandlerFeature>();
    var excepcion = exceptionHandleFeature?.Error!;

    // Creamos un nuevo objeto Error y lo llenamos con los detalles de la excepción.
    var error = new Error();
    error.Fecha = DateTime.UtcNow;
    error.MensajeDeError = excepcion.Message;
    error.StackTrace = excepcion.StackTrace;
    // Obtenemos el servicio del repositorio de errores.
    var repositorio = context.RequestServices.GetRequiredService<IRepositorioErrores>();
    // Guardamos los detalles del error en el repositorio.
    await repositorio.Crear(error);
    // Devolvemos una respuesta de Bad Request (400) con un mensaje de error personalizado.
    await TypedResults.BadRequest(
        new { tipo = "error", mensaje = "ha ocurrido un error inesperado", estatus = 500 })
    .ExecuteAsync(context);
}));
//Se encarga de generar páginas de respuesta para códigos de estado HTTP como 404 (No encontrado) y 500 (Error interno del servidor)
// cuando no se ha generado ninguna respuesta para la solicitud. Esto es útil para proporcionar respuestas amigables al usuario cuando ocurre un error.
app.UseStatusCodePages();


//}
app.UseStaticFiles();

app.UseCors();

app.UseOutputCache();


app.UseAuthorization();

app.MapGraphQL();

app.MapGet("/", [EnableCors(policyName: "libre")] () => "Hello World!");

app.MapPost("/modelbinding", ([FromQuery] string? nombre) =>
{
    if (nombre is null)
    {
        nombre = "Vacío";
    }

    return TypedResults.Ok(nombre);
});

app.MapGet("/Error", () =>
{
    throw new InvalidOperationException("error");

});
app.MapGroup("/generos").MapGeneros();
app.MapGroup("/actores").MapActores();
app.MapGroup("/peliculas").MapPeliculas();
app.MapGroup("/pelicula/{peliculaId:int}/comentarios").MapComentarios();
app.MapGroup("/usuarios").MapUsuarios();


app.Run();


