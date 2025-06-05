namespace ProyectoBiblioteca.Models
{
    public class Multa
    {
        public int MultaID { get; set; }
        public int PrestamoID { get; set; }
        public double Monto { get; set; }
        public DateTime FechaGenerada { get; set; }
        public bool Pagada { get; set; }
    }
}