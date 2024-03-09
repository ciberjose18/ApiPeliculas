namespace ApiJuegos.Servicios
{
    public interface IAlmacenarArchivos
    {
        // Método para borrar un archivo
        Task Borrar(string? ruta, string carpeta);
        //Este es un método que toma como parámetros una carpeta y un objeto IFormFile (representa un archivo cargado en un formulario).
        //Se espera que este método realice la acción de almacenar el archivo en la carpeta indicada y devuelva
        //la ruta o nombre del archivo almacenado.
        Task<string> Almacenar(string carpeta, IFormFile archivo);
        //Este es un método asíncrono que toma como parámetros una ruta existente, una carpeta y un objeto IFormFile.
        //La intención es editar un archivo existente, lo que implica borrar el archivo en la ruta especificada
        //y carpeta indicada, y luego almacenar el nuevo archivo en esa carpeta.
        async Task<string> Editar(string ruta, string carpeta, IFormFile archivo)
        {
            await Borrar(ruta, carpeta);
            return await Almacenar(carpeta, archivo);
        }
    }
}
