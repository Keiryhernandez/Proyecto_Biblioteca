using ProyectoBiblioteca.Enums;

namespace ProyectoBiblioteca.Models
{
    // Clase que representa un usuario del sistema, puede ser lector o administrador
    public class Usuario
    {
        // Identificador único del usuario
        public int UsuarioID { get; set; }

        // Nombre completo del usuario (puede ser nulo)
        public string? Nombre { get; set; }

        // Correo electrónico del usuario (puede ser nulo)
        public string? Email { get; set; }

        // Contraseña del usuario para autenticación (puede ser nulo)
        public string? Contraseña { get; set; }

        // Tipo de usuario: ADMIN o LECTOR, usando la enumeración TipoUsuario
        public TipoUsuario TipoUsuario { get; set; }

        // Fecha en que el usuario se registró en el sistema
        public DateTime FechaRegistro { get; set; }

        // Estado del usuario: true si está activo, false si está deshabilitado o inactivo
        public bool Estado { get; set; }
    }
}
