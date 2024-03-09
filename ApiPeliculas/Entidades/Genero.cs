using System.ComponentModel.DataAnnotations;

namespace ApiJuegos.Entidades
{
    public class Genero
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string? Nombre { get; set; }
        public List<GeneroPelicula> GeneroPeliculas { get; set; } = new List<GeneroPelicula>();
    }
}
