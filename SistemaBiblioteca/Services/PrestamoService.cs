using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoBiblioteca.Services
{
    public class PrestamoService
    {
        private List<Prestamo> prestamos = new List<Prestamo>();
        private int idCounter = 1;

        private readonly LibroService libroService;
        private readonly UsuarioService usuarioService;

        public PrestamoService(LibroService libroService, UsuarioService usuarioService)
        {
            this.libroService = libroService;
            this.usuarioService = usuarioService;
        }

        public void RegistrarPrestamo(Usuario usuario, Libro libro)
        {
            if (libro.CantidadDisponible <= 0)
            {
                Console.WriteLine("El libro no está disponible para préstamo.");
                return;
            }

            Prestamo nuevo = new Prestamo
            {
                PrestamoID = idCounter++,
                Usuario = usuario,
                Libro = libro,
                FechaPrestamo = DateTime.Now,
                FechaDevolucion = DateTime.Now.AddDays(7),
                Estado = EstadoPrestamo.EN_CURSO
            };

            prestamos.Add(nuevo);
            libro.CantidadDisponible--;

            Console.WriteLine("Préstamo registrado con éxito.");
        }

        public void MostrarPrestamosActivos()
        {
            var activos = prestamos.Where(p => p.Estado == EstadoPrestamo.EN_CURSO).ToList();

            if (activos.Count == 0)
            {
                Console.WriteLine("No hay préstamos activos.");
                return;
            }

            foreach (var p in activos)
            {
                Console.WriteLine($"ID: {p.PrestamoID}, Usuario: {p.Usuario.Nombre}, Libro: {p.Libro.Titulo}, Fecha: {p.FechaPrestamo.ToShortDateString()}");
            }
        }

        public void MostrarReportesPrestamos()
        {
            Console.WriteLine("=== Reportes de préstamos ===");
            var prestamosActivos = prestamos.Where(p => p.Estado == EstadoPrestamo.EN_CURSO).ToList();
            if (prestamosActivos.Count == 0)
                Console.WriteLine("No hay préstamos activos.");
            else
            {
                foreach (var p in prestamosActivos)
                {
                    Console.WriteLine($"Préstamo ID: {p.PrestamoID}, Usuario: {p.Usuario.Nombre}, Libro: {p.Libro.Titulo}, Fecha préstamo: {p.FechaPrestamo.ToShortDateString()}");
                }
            }
            Console.ReadKey();
        }


        public void MostrarHistorialPrestamos()
        {
            Console.WriteLine("=== Historial de préstamos ===");
            if (prestamos.Count == 0)
                Console.WriteLine("No hay historial de préstamos.");
            else
            {
                foreach (var p in prestamos) ;
            }
        }


        public void CancelarSolicitud(int usuarioId)
        {
            var solicitud = prestamos.FirstOrDefault(p =>
                p.Usuario.UsuarioID == usuarioId &&
                p.FechaDevolucion == null &&
                p.Estado == EstadoPrestamo.SOLICITADO);

            if (solicitud != null)
            {
                prestamos.Remove(solicitud);
                Console.WriteLine("La solicitud de préstamo ha sido cancelada.");
            }
            else
            {
                Console.WriteLine("No tienes solicitudes pendientes para cancelar.");
            }
        }

        public void ReservarLibroNoDisponible(int libroId, int usuarioId)
        {
            var libro = libroService.ObtenerLibroPorId(libroId);
            var usuario = usuarioService.ObtenerUsuarioPorId(usuarioId);

            if (libro == null || usuario == null)
            {
                Console.WriteLine("Libro o usuario no encontrado.");
                return;
            }

            if (libro.CantidadDisponible > 0)
            {
                Console.WriteLine("El libro está disponible, puedes solicitarlo directamente.");
                return;
            }

            Prestamo reserva = new Prestamo
            {
                PrestamoID = idCounter++,
                Usuario = usuario,
                Libro = libro,
                FechaPrestamo = DateTime.Now,
                FechaDevolucion = null,
                Estado = EstadoPrestamo.SOLICITADO
            };

            prestamos.Add(reserva);
            Console.WriteLine("Reserva realizada. Se te notificará cuando esté disponible.");
        }
    }
}
