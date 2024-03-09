using ApiJuegos.Utilidades;

namespace ApiJuegos.DTOs
{
    public class PeliculasFiltrarDTO
    {
        public int Pagina { get; set; }
        public int RecordsXpagina { get; set; }
        public PaginacionDTO PaginacionDTO {
            get
            {
                return new PaginacionDTO { Pagina = Pagina, RecordsXpagina = RecordsXpagina };
            }
        }

        public string? Titulo { get; set; }
        public int GeneroId { get; set; }
        public bool? EnCines { get; set; }
        public bool ProximosEstrenos { get; set; }
        public string? CampoOrdenar { get; set;}
        public bool OrdenAscendente { get; set; } = true;

        public static ValueTask<PeliculasFiltrarDTO> BindAsync(HttpContext context)
        {
            var pagina = context.ExtraerValorXdefecto(nameof(Pagina), 1);
            var recordsXpagina = context.ExtraerValorXdefecto(nameof(RecordsXpagina), 10);
            var titulo = context.ExtraerValorXdefecto(nameof(Titulo), string.Empty);
            var generoId = context.ExtraerValorXdefecto(nameof(GeneroId), 0);
            var enCines = context.ExtraerValorXdefectoBool(nameof(EnCines));
            var proximosEstrenos = context.ExtraerValorXdefecto(nameof(ProximosEstrenos), false);
            var campoOrdenar = context.ExtraerValorXdefecto(nameof(CampoOrdenar), string.Empty);
            var ordenAscendente = context.ExtraerValorXdefecto(nameof(OrdenAscendente), true);

            var resultado = new PeliculasFiltrarDTO
            {
                Pagina = pagina,
                RecordsXpagina = recordsXpagina,
                Titulo = titulo,
                GeneroId = generoId,
                EnCines = enCines,
                ProximosEstrenos = proximosEstrenos,
                CampoOrdenar = campoOrdenar,
                OrdenAscendente = ordenAscendente
            };

            return ValueTask.FromResult(resultado);
        }

        //Recordar implementar el repositorio y la interfaz Flitrar
    }

}
