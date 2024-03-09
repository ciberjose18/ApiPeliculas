using Microsoft.AspNetCore.Identity;

namespace ApiJuegos.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Cuerpo { get; set; } = null!;
        public int PeliculaId { get; set;}
        public string UsuarioId { get; set; } = null!;  //Se guarda es el identificador del usuario que hizo el comentario
        public IdentityUser Usuario { get; set; } = null!; //Se guarda al usuario en el sistema de autenticación
    }
}
