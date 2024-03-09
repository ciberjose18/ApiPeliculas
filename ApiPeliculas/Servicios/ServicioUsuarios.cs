using ApiJuegos.DTOs;
using ApiJuegos.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiJuegos.Servicios
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        private readonly IHttpContextAccessor httpContextAccessor; //nos permite acceder a información sobre la solicitud HTTP actual
        private readonly UserManager<IdentityUser> userManager; //nos permite interactuar con el sistema de autenticación
        private readonly IConfiguration configuration;

        //Toma estos dos objetos como argumentos. Esto es como decir: "Para construir un ServicioUsuarios, necesito un httpContextAccessor y un userManager".
        public ServicioUsuarios(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        // Este método busca el email del usuario en las reclamaciones (claims) de la solicitud HTTP actual.
        // Si no encuentra un email, devuelve null. Si encuentra un email, utiliza userManager para buscar al usuario con ese email y lo devuelve.
        public async Task<IdentityUser?> ObtenerUsuario()
        {
            // Buscamos el email en las reclamaciones.
            var emailClaim = httpContextAccessor.HttpContext?.User.Claims.Where(c => c.Type == "email").FirstOrDefault();

            if (emailClaim is null)
            {
                return null;
            }

            // Si encontramos un email, utilizamos userManager para buscar al usuario con ese email.
            var email = emailClaim.Value;
            return await userManager.FindByEmailAsync(email);
        }

        // Esta función crea un token para un usuario
        public async Task<RespuestaAuthDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuario)
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
        }

        public async Task<List<UsuarioTokenDTO>> ObtenerTodosLosUsuariosConTokens()
        {
            var usuarios = userManager.Users.ToList();
            var usuariosConTokens = new List<UsuarioTokenDTO>();

            foreach (var usuario in usuarios)
            {
                var credencialesUsuario = new CredencialesUsuarioDTO { Email = usuario.Email! };
                var token = await ConstruirToken(credencialesUsuario);
                usuariosConTokens.Add(new UsuarioTokenDTO { Email = usuario.Email!, Token = token.Token });
            }

            return usuariosConTokens;
        }
    }
}
