namespace ProyectoBiblioteca.Models
{
    public class Libro
    {
        public int LibroID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public string Editorial { get; set; }
        public int AñoPublicacion { get; set; }
        public string Categoria { get; set; }
        public int CantidadTotal { get; set; }
        public int CantidadDisponible { get; set; }
    }
}