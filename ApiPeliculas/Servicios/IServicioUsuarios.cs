using ApiJuegos.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ApiJuegos.Servicios
{
    public interface IServicioUsuarios
    {
        Task<RespuestaAuthDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuario);
        Task<List<UsuarioTokenDTO>> ObtenerTodosLosUsuariosConTokens();
        Task<IdentityUser?> ObtenerUsuario();
    }
}