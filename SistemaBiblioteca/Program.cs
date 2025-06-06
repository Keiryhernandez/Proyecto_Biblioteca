using System;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Services;
using ProyectoBiblioteca.Enums;

class Program
{
    static void Main(string[] args)
    {
        UsuarioService usuarioService = new UsuarioService();
        usuarioService.CrearUsuarioAdmin();

        LibroService libroService = new LibroService();
        PrestamoService prestamoService = new PrestamoService(libroService, usuarioService);

        bool activo = true;

        while (activo)
        {
            Console.Clear();
            Console.WriteLine("=== Sistema de Biblioteca ===");
            Console.WriteLine("1. Iniciar sesión");
            Console.WriteLine("2. Registrarse");
            Console.WriteLine("0. Salir");
            Console.Write("Opción: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    usuarioService.Login(libroService, prestamoService);
                    break;
                case "2":
                    usuarioService.RegistrarUsuario();
                    break;
                case "0":
                    activo = false;
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
