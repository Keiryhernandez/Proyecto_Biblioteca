using ProyectoBiblioteca.Enums;

namespace ProyectoBiblioteca.Models
{
    public class Prestamo
    {
        public int PrestamoID { get; set; }
        public int UsuarioID { get; set; }
        public int LibroID { get; set; }
        public Libro Libro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public EstadoPrestamo Estado { get; set; }
    }
}
