using System;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Services;
using ProyectoBiblioteca.Enums;

class Program
{
    /// <summary>
    /// Punto de entrada principal del sistema de biblioteca.
    /// Inicializa los servicios necesarios y muestra el menú principal para que el usuario
    /// pueda iniciar sesión, registrarse o salir del sistema.
    /// </summary>
    static void Main(string[] args)
    {
        // Se inicializa el servicio de usuarios y se crea el administrador predeterminado
        UsuarioService usuarioService = new UsuarioService();
        usuarioService.CrearUsuarioAdmin();

        // Se inicializan los servicios de libros y préstamos
        LibroService libroService = new LibroService();
        PrestamoService prestamoService = new PrestamoService(libroService, usuarioService);

        // Variable de control del bucle principal del menú
        bool activo = true;

        // Bucle principal del sistema
        while (activo)
        {
            // Limpia la consola y muestra el menú principal
            Console.Clear();
            Console.WriteLine("=== Sistema de Biblioteca ===");
            Console.WriteLine("1. Iniciar sesión");
            Console.WriteLine("2. Registrarse");
            Console.WriteLine("0. Salir");
            Console.Write("Opción: ");
            var opcion = Console.ReadLine();

            // Se evalúa la opción seleccionada por el usuario
            switch (opcion)
            {
                case "1":
                    // Llama al método de login, que gestiona la lógica de sesión
                    usuarioService.Login(libroService, prestamoService);
                    break;

                case "2":
                    // Permite al usuario registrarse en el sistema
                    usuarioService.RegistrarUsuario();
                    break;

                case "0":
                    // Sale del sistema
                    activo = false;
                    break;

                default:
                    // Mensaje en caso de opción inválida
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
