
using Path = System.IO.Path;

namespace ApiJuegos.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacenarArchivos
    {
        private readonly IWebHostEnvironment webHost;
        private readonly IHttpContextAccessor httpContext;

        public AlmacenadorArchivosLocal(IWebHostEnvironment webHost, IHttpContextAccessor httpContext)
        {
            this.webHost = webHost;
            this.httpContext = httpContext;
        }

        // Método para almacenar un archivo en una carpeta
        public async Task<string> Almacenar(string carpeta, IFormFile archivo)
        {
            // Obtener la extensión del archivo
            var extension = Path.GetExtension(archivo.FileName);
            //Generar un nombre único para el archivo utilizando un identificador único global (Guid)
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            // Combinar la carpeta proporcionada con la ruta del directorio raíz del servidor web
            string folder = Path.Combine(webHost.WebRootPath, carpeta);
            // Verificar si la carpeta no existe y crearla si es necesario
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            // Combinar la carpeta y el nombre del archivo para obtener la ruta completa de almacenamiento
            string ruta = Path.Combine(folder, nombreArchivo);
            // Utilizar un MemoryStream para copiar el contenido del archivo cargado
            using (var ms = new MemoryStream())
            {
                //Copia el contenido del archivo cargado al MemoryStream.
                await archivo.CopyToAsync(ms);
                // Convierte el contenido del MemoryStream a un array de bytes.
                var contenido = ms.ToArray();
                // Escribir el contenido del archivo en la ruta de almacenamiento
                await File.WriteAllBytesAsync(ruta, contenido);

            }
            // Construir la URL completa del archivo almacenado
            var url = $"{httpContext.HttpContext!.Request.Scheme}://{httpContext.HttpContext.Request.Host}";
            //Construye la URL completa del archivo almacenado utilizando la URL base, la carpeta y el nombre del archivo
            var urlArchivo = Path.Combine(url, carpeta, nombreArchivo).Replace("\\", "/");

            // Devolver la URL del archivo almacenado
            return urlArchivo;
        }

        // Método para borrar un archivo dado su ruta y carpeta
        public Task Borrar(string? ruta, string carpeta)
        {
            // Verificar si la ruta del archivo es nula o vacía
            if (string.IsNullOrEmpty(ruta))
            {
                // Si es así, no hay nada que borrar, devolver una tarea completada
                return Task.CompletedTask;
            }
            // Obtener el nombre del archivo a partir de la ruta proporcionada
            var nombreArchivo = Path.GetFileName(ruta);
            // Combinar la carpeta, el nombre del archivo y la ruta del directorio raíz del servidor web
            var directorioArchivo = Path.Combine(webHost.WebRootPath, carpeta, nombreArchivo);
            // Verificar si el archivo existe en la ruta especificada
            if (File.Exists(directorioArchivo))
            {
                // Si existe, borrar el archivo
                File.Delete(directorioArchivo);
            }

            // Devolver una tarea completada
            return Task.CompletedTask;
        }
    }
}
