namespace ApiJuegos.DTOs
{
    public class CrearPeliculaDTO
    {
        //public string Id { get; set; }
        public string Titulo { get; set; } = null!;
        public bool EnCines { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public IFormFile? Poster { get; set; }

    }
}
