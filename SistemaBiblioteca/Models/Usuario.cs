using ProyectoBiblioteca.Enums;

//Representa un usuario (lector o admin)
namespace ProyectoBiblioteca.Models
{
    public class Usuario
    {
        //Datos 
        public int UsuarioID { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
    }
}