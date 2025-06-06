namespace ProyectoBiblioteca.Models
{
    // Clase que representa un libro dentro de la biblioteca
    public class Libro
    {
        // Identificador único del libro
        public int LibroID { get; set; }

        // Título del libro
        public string Titulo { get; set; }

        // Autor o autores del libro
        public string Autor { get; set; }

        // Código ISBN del libro (identificador internacional estándar)
        public string ISBN { get; set; }

        // Editorial que publicó el libro
        public string Editorial { get; set; }

        // Año en que se publicó el libro
        public int AñoPublicacion { get; set; }

        // Categoría o género literario del libro (ejemplo: novela, ciencia, historia, etc.)
        public string Categoria { get; set; }

        // Cantidad total de ejemplares del libro que posee la biblioteca
        public int CantidadTotal { get; set; }

        // Cantidad de ejemplares disponibles actualmente para préstamo
        public int CantidadDisponible { get; set; }
    }
}
