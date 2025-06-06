using ProyectoBiblioteca.Enums;

namespace ProyectoBiblioteca.Models
{
    /// <summary>
    /// Representa un préstamo de un libro dentro del sistema de biblioteca.
    /// Contiene información sobre el usuario que realiza el préstamo, el libro prestado,
    /// las fechas asociadas al préstamo y su estado actual.
    /// </summary>
    public class Prestamo
    {
        /// <summary>
        /// Identificador único del préstamo.
        /// </summary>
        public int PrestamoID { get; set; }

        /// <summary>
        /// Identificador del usuario que ha realizado el préstamo.
        /// Relacionado con la entidad Usuario.
        /// </summary>
        public int UsuarioID { get; set; }

        /// <summary>
        /// Identificador del libro que ha sido prestado.
        /// Relacionado con la entidad Libro.
        /// </summary>
        public int LibroID { get; set; }

        /// <summary>
        /// Objeto de tipo Libro que representa el libro prestado.
        /// Se utiliza para la navegación entre entidades en Entity Framework.
        /// </summary>
        public Libro Libro { get; set; }

        /// <summary>
        /// Objeto de tipo Usuario que representa al usuario que realiza el préstamo.
        /// Se utiliza para la navegación entre entidades en Entity Framework.
        /// </summary>
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Fecha en la que se realizó el préstamo.
        /// </summary>
        public DateTime FechaPrestamo { get; set; }

        /// <summary>
        /// Fecha límite para la devolución del libro.
        /// Es opcional (nullable) porque podría calcularse posteriormente.
        /// </summary>
        public DateTime? FechaDevolucion { get; set; }

        /// <summary>
        /// Fecha real en la que el usuario devolvió el libro.
        /// Es opcional porque puede no haberse devuelto aún.
        /// </summary>
        public DateTime? FechaDevolucionReal { get; set; }

        /// <summary>
        /// Estado actual del préstamo, representado por un valor del enum EstadoPrestamo.
        /// Por ejemplo: Pendiente, Devuelto, Retrasado, etc.
        /// </summary>
        public EstadoPrestamo Estado { get; set; }
    }
}
