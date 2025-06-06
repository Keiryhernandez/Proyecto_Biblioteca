using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoBiblioteca.Services
{
    public class PrestamoService
    {
        // Lista que almacena todos los préstamos realizados (activos, históricos y reservas)
        private List<Prestamo> prestamos = new List<Prestamo>();

        // Contador para asignar IDs únicos a cada préstamo
        private int idCounter = 1;

        // Servicios auxiliares para gestionar libros y usuarios
        private readonly LibroService libroService;
        private readonly UsuarioService usuarioService;

        // Constructor que inyecta los servicios de libros y usuarios
        public PrestamoService(LibroService libroService, UsuarioService usuarioService)
        {
            this.libroService = libroService;
            this.usuarioService = usuarioService;
        }

        // Método para registrar un nuevo préstamo de un libro a un usuario
        public void RegistrarPrestamo(Usuario usuario, Libro libro)
        {
            // Verificar si hay disponibilidad del libro
            if (libro.CantidadDisponible <= 0)
            {
                Console.WriteLine("El libro no está disponible para préstamo.");
                return;
            }

            // Crear un nuevo préstamo con los datos necesarios
            Prestamo nuevo = new Prestamo
            {
                PrestamoID = idCounter++,                  // Asignar ID único
                Usuario = usuario,                          // Usuario que realiza el préstamo
                Libro = libro,                              // Libro que se presta
                FechaPrestamo = DateTime.Now,               // Fecha actual como fecha de préstamo
                FechaDevolucion = DateTime.Now.AddDays(7), // Fecha límite de devolución (7 días después)
                Estado = EstadoPrestamo.EN_CURSO            // Estado del préstamo: en curso
            };

            prestamos.Add(nuevo);           // Agregar el préstamo a la lista
            libro.CantidadDisponible--;     // Disminuir la cantidad disponible del libro

            Console.WriteLine("Préstamo registrado con éxito.");
        }

        // Método para mostrar todos los préstamos activos (en curso)
        public void MostrarPrestamosActivos()
        {
            // Filtrar los préstamos cuyo estado es EN_CURSO
            var activos = prestamos.Where(p => p.Estado == EstadoPrestamo.EN_CURSO).ToList();

            if (activos.Count == 0)
            {
                Console.WriteLine("No hay préstamos activos.");
                return;
            }

            // Mostrar información básica de cada préstamo activo
            foreach (var p in activos)
            {
                Console.WriteLine($"ID: {p.PrestamoID}, Usuario: {p.Usuario.Nombre}, Libro: {p.Libro.Titulo}, Fecha: {p.FechaPrestamo.ToShortDateString()}");
            }
        }

        // Método para mostrar un reporte de préstamos activos
        public void MostrarReportesPrestamos()
        {
            Console.WriteLine("=== Reportes de préstamos ===");
            var prestamosActivos = prestamos.Where(p => p.Estado == EstadoPrestamo.EN_CURSO).ToList();

            if (prestamosActivos.Count == 0)
            {
                Console.WriteLine("No hay préstamos activos.");
            }
            else
            {
                foreach (var p in prestamosActivos)
                {
                    Console.WriteLine($"Préstamo ID: {p.PrestamoID}, Usuario: {p.Usuario.Nombre}, Libro: {p.Libro.Titulo}, Fecha préstamo: {p.FechaPrestamo.ToShortDateString()}");
                }
            }

            Console.ReadKey(); // Pausa la consola hasta que se presione una tecla
        }

        // Método para mostrar el historial completo de préstamos (todos los préstamos registrados)
        public void MostrarHistorialPrestamos()
        {
            Console.WriteLine("=== Historial de préstamos ===");

            if (prestamos.Count == 0)
            {
                Console.WriteLine("No hay historial de préstamos.");
            }
            else
            {
                // Mostrar detalles de todos los préstamos, sin importar estado
                foreach (var p in prestamos)
                {
                    Console.WriteLine($"ID: {p.PrestamoID}, Usuario: {p.Usuario.Nombre}, Libro: {p.Libro.Titulo}, Fecha préstamo: {p.FechaPrestamo.ToShortDateString()}, Estado: {p.Estado}");
                }
            }
        }

        // Método para cancelar una solicitud de préstamo pendiente (estado SOLICITADO)
        public void CancelarSolicitud(int usuarioId)
        {
            // Buscar una solicitud pendiente para el usuario específico
            var solicitud = prestamos.FirstOrDefault(p =>
                p.Usuario.UsuarioID == usuarioId &&
                p.FechaDevolucion == null &&
                p.Estado == EstadoPrestamo.SOLICITADO);

            if (solicitud != null)
            {
                prestamos.Remove(solicitud); // Eliminar la solicitud de la lista
                Console.WriteLine("La solicitud de préstamo ha sido cancelada.");
            }
            else
            {
                Console.WriteLine("No tienes solicitudes pendientes para cancelar.");
            }
        }

        // Método para reservar un libro que actualmente no está disponible
        public void ReservarLibroNoDisponible(int libroId, int usuarioId)
        {
            var libro = libroService.ObtenerLibroPorId(libroId);     // Obtener libro por ID
            var usuario = usuarioService.ObtenerUsuarioPorId(usuarioId); // Obtener usuario por ID

            // Validar que el libro y usuario existan
            if (libro == null || usuario == null)
            {
                Console.WriteLine("Libro o usuario no encontrado.");
                return;
            }

            // Si el libro está disponible, indicar que puede solicitarse directamente
            if (libro.CantidadDisponible > 0)
            {
                Console.WriteLine("El libro está disponible, puedes solicitarlo directamente.");
                return;
            }

            // Crear una reserva con estado SOLICITADO y sin fecha de devolución aún
            Prestamo reserva = new Prestamo
            {
                PrestamoID = idCounter++,
                Usuario = usuario,
                Libro = libro,
                FechaPrestamo = DateTime.Now,
                FechaDevolucion = null,
                Estado = EstadoPrestamo.SOLICITADO
            };

            prestamos.Add(reserva); // Agregar la reserva a la lista
            Console.WriteLine("Reserva realizada. Se te notificará cuando esté disponible.");
        }
    }
}
