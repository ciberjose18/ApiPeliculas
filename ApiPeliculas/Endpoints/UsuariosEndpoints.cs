using ApiJuegos.DTOs;
using ApiJuegos.Filtros;
using ApiJuegos.Servicios;
using ApiJuegos.Utilidades;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiJuegos.Endpoints
{
    public static class UsuariosEndpoints
    {
        public static RouteGroupBuilder MapUsuarios(this RouteGroupBuilder group)
        {
            group.MapPost("/registrar", Registrar).AddEndpointFilter<FiltroValidaciones<CredencialesUsuarioDTO>>();
            group.MapPost("/login", Login).AddEndpointFilter<FiltroValidaciones<CredencialesUsuarioDTO>>();

            group.MapPost("/hacer-admin", HacerAdmin).AddEndpointFilter<FiltroValidaciones<EditarClaimDTO>>();
            group.MapPost("/remover-admin", RemoverAdmin).AddEndpointFilter<FiltroValidaciones<EditarClaimDTO>>();
            group.MapGet("/renovarToken", RenovarToken).RequireAuthorization();
            group.MapGet("/todos-token", ObtenerTodosLosUsuariosConTokens).RequireAuthorization();
            return group;
        }

        // Esta función registra un nuevo usuario
        static async Task<Results<Ok<RespuestaAuthDTO>, BadRequest<IEnumerable<IdentityError>>>> Registrar(CredencialesUsuarioDTO credencialesUsuario
            , [FromServices] UserManager<IdentityUser> userManager, [FromServices] IServicioUsuarios servicioUsuarios,IConfiguration configuration)
        {
            // Primero, creamos un nuevo usuario con el correo electrónico proporcionado
            var usuario = new IdentityUser
            {
                UserName = credencialesUsuario.Email,
                Email = credencialesUsuario.Email
            };
            // Luego, intentamos crear el usuario en la base de datos con la contraseña proporcionada
            var resultado = await userManager.CreateAsync(usuario, credencialesUsuario.Password);
            // Si la creación del usuario fue exitosa...
            if (resultado.Succeeded)
            {
                // ...construimos un token para el usuario...
                var credencialesRespuesta = await servicioUsuarios.ConstruirToken(credencialesUsuario);
                // ...y devolvemos el token como respuesta
                return TypedResults.Ok(credencialesRespuesta);
            }
            else
            {
                // Si la creación del usuario falló, devolvemos un error
                return TypedResults.BadRequest(resultado.Errors);
            } 
            

        }

        // Esta función permite a un usuario iniciar sesión
        static async Task<Results<Ok<RespuestaAuthDTO>, BadRequest<string>>> Login(CredencialesUsuarioDTO credencialesUsuarioDTO,
            [FromServices] SignInManager<IdentityUser> signInManager, [FromServices] UserManager<IdentityUser> userManager,
            IConfiguration configuration, [FromServices] IServicioUsuarios servicioUsuarios)
        {
            // Primero, buscamos al usuario en la base de datos usando su correo electrónico
            var usuario = await userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
            // Si no encontramos al usuario, devolvemos un error
            if (usuario is null)
            {
                return TypedResults.BadRequest("Login incorrecto");
            }

            // Si encontramos al usuario, verificamos que la contraseña sea correcta
            var resultado = await signInManager.CheckPasswordSignInAsync(usuario,credencialesUsuarioDTO.Password, lockoutOnFailure: false);
            // Si la contraseña es correcta...
            if (resultado.Succeeded)
            {
                // ...construimos un token para el usuario.
                var respuestaAutenticacion = await servicioUsuarios.ConstruirToken(credencialesUsuarioDTO);
                //var respuestaAutenticacion = await servicioUsuarios.ConstruirToken(credencialesUsuarioDTO, configuration, userManager);
                // ...y devolvemos el token como respuesta
                return TypedResults.Ok(respuestaAutenticacion);
            }
            else
            { 
                // Si la contraseña es incorrecta, devolvemos un error
                return TypedResults.BadRequest("Login incorrecto");
            }

        }

        // Esta función crea un token para un usuario
        /*private async static Task<RespuestaAuthDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuario,
            IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            // El FromService en este caso al ser un método privado estático no es necesario, ya que no es un método de acción.
            // Primero, creamos una lista de "claims". Los claims son como detalles sobre el usuario que se guardan en el token.
            var claims = new List<Claim>
            {
                // Aquí estamos guardando el correo electrónico del usuario en el token
                new Claim("email", credencialesUsuario.Email),
                // Este es solo un claim de ejemplo, puedes guardar cualquier información que quieras aquí
                new Claim("lo que yo quiera", "cualquier otro valor")
            };
            var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            var claimDB = await userManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimDB);
            // Luego, obtenemos la "llave" que se usará para firmar el token. Esta llave es secreta y solo la conoce la aplicación.
            var llave = Llaves.ObtenerLlave(configuration);
            var credenciales = new SigningCredentials(llave.First(), SecurityAlgorithms.HmacSha256);
            // Establecemos cuándo expirará el token. En este caso, el token expirará en 1 año.
            var expiracion = DateTime.UtcNow.AddYears(1);
            // Creamos el token con todos los detalles que hemos definido
            var tokenDeSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: credenciales);
            
            // Finalmente, convertimos el token a un formato que se puede enviar a través de internet
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);
            // Devolvemos el token y la fecha de expiración como respuesta
            return new RespuestaAuthDTO
            {
                Token = token,
                Expiracion = expiracion
            };
        }*/

        static async Task<Results<NoContent, NotFound>> HacerAdmin(EditarClaimDTO editarClaimDTO,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            // El FromService solo lo usamos para métodos de acción.
            // Primero, buscamos al usuario por su correo electrónico.
            var usuario = await userManager.FindByEmailAsync(editarClaimDTO.Email);
            if (usuario is null)
            {
                return TypedResults.NotFound();
            }
            // Si encontramos al usuario, le damos el claim de ser un administrador.
            await userManager.AddClaimAsync(usuario, new Claim("admin", "true"));
            //Un claim tiene dos partes: el tipo y el valor. El tipo es "admin" y el valor es "true".
            //Esto es como decir "este usuario es un administrador".
            return TypedResults.NoContent();
        }


        static async Task<Results<NoContent, NotFound>> RemoverAdmin(EditarClaimDTO editarClaimDTO,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var usuario = await userManager.FindByEmailAsync(editarClaimDTO.Email);
            if (usuario is null)
            {
                return TypedResults.NotFound();
            }

            await userManager.RemoveClaimAsync(usuario, new Claim("admin", "true"));
            return TypedResults.NoContent();
        }

         async static Task<Results<Ok<RespuestaAuthDTO>, NotFound>> RenovarToken([FromServices] IServicioUsuarios servicioUsuarios, IConfiguration configuration,
             [FromServices] UserManager<IdentityUser> userManager)
        {
            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
            {
                return TypedResults.NotFound();
            }
            var credencialesUsuarioDTO = new CredencialesUsuarioDTO
            {
                Email = usuario.Email!
            };

            var respuestaAuthDTO = await servicioUsuarios.ConstruirToken(credencialesUsuarioDTO);

            return TypedResults.Ok(respuestaAuthDTO);
        }

        static async Task<Results<Ok<List<UsuarioTokenDTO>>, NotFound>> ObtenerTodosLosUsuariosConTokens([FromServices] IServicioUsuarios servicioUsuarios)
        {
            var usuariosConTokens = await servicioUsuarios.ObtenerTodosLosUsuariosConTokens();
            if (usuariosConTokens is null || !usuariosConTokens.Any())
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(usuariosConTokens);
        }


    }
}
