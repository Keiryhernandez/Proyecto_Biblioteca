namespace ProyectoBiblioteca.Models
{
    // Clase que representa una multa asociada a un préstamo en la biblioteca
    public class Multa
    {
        // Identificador único de la multa
        public int MultaID { get; set; }

        // Identificador del préstamo asociado a esta multa
        public int PrestamoID { get; set; }

        // Monto económico de la multa (por ejemplo, valor a pagar por retraso)
        public double Monto { get; set; }

        // Fecha en la que se generó la multa
        public DateTime FechaGenerada { get; set; }

        // Estado que indica si la multa ya fue pagada o no
        public bool Pagada { get; set; }
    }
}
