using ApiJuegos.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Error = ApiJuegos.Entidades.Error;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiJuegos
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<Genero>().Property(p => p.Nombre).HasMaxLength(50); Hace lo mismo que [StringLength(50)]
            //Actor lo vamos a configurar de esta forma
            modelBuilder.Entity<Actor>().Property(p => p.Nombre).HasMaxLength(150);
            modelBuilder.Entity<Actor>().Property(p => p.Foto).IsUnicode();



            modelBuilder.Entity<Pelicula>().Property(p => p.Poster).IsUnicode();
            modelBuilder.Entity<Pelicula>().Property(p => p.Titulo).HasMaxLength(150);

            // 'HasKey(g => new { g.GeneroId, g.PeliculaId })' se utiliza para definir la clave primaria de la entidad GeneroPelicula.
            // En este caso, estamos diciendo que la clave primaria de la entidad GeneroPelicula está compuesta por dos campos: GeneroId y PeliculaId.
            // Estamos utilizando una expresión lambda (g => new { g.GeneroId, g.PeliculaId }) para especificar qué campos formarán parte de la clave primaria.

            // Por ejemplo, imagina que tienes una tabla llamada GeneroPelicula que registra las relaciones entre géneros y películas.
            // Entonces, la combinación única de GeneroId y PeliculaId identificará cada relación en esa tabla.
            modelBuilder.Entity<GeneroPelicula>().HasKey(g => new { g.GeneroId, g.PeliculaId });

            modelBuilder.Entity<ActorPelicula>().HasKey(g => new { g.ActorId, g.PeliculaId });
            //Este código se utiliza para personalizar las tablas de la base de datos que se utilizarán para almacenar la información de Identity.
            //Por ejemplo, esta linea cambia el nombre de la tabla que se utilizará para almacenar la información de los usuarios de "AspNetUsers"(el nombre predeterminado) a "Usuarios".

            modelBuilder.Entity<IdentityUser>().ToTable("Usuarios");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsuariosClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsuariosLogins");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsuariosRoles");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsuariosTokens");

        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<GeneroPelicula> GeneroPeliculas { get; set; }
        public DbSet<ActorPelicula> ActoresPeliculas { get; set; }
        public DbSet<Error> Errores { get; set; }
    }
}
