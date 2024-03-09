namespace ApiJuegos.DTOs
{
    public class RespuestaAuthDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expiracion { get; set; }
    }
}
