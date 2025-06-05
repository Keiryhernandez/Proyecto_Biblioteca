using ProyectoBiblioteca.Models;
using System;

//gestiona la visualizacion de los prestamos en la biblioteca 
namespace ProyectoBiblioteca.Services
{
    public class PrestamoService
    {
        //Lista donde se guardan los prestamos que se realizan 
        private List<Prestamo> prestamos = new List<Prestamo>();
        //Contador que asigna un ID unico a cada prestamo
        private int idCounter = 1;

        //Metodo que registra un nuevo prestamos si el libro se encuentra disponble y disminuye la existencia del libro 
        public void RegistrarPrestamo(Usuario usuario, Libro libro)
        {
            //Valida la disponiblidad del libro 
            if (libro.CantidadDisponible <= 0)
            {
                Console.WriteLine("El libro no está disponible para préstamo.");
                return;
            }

            //Crea un nuevo registro 

            Prestamo nuevo = new Prestamo
            {
                PrestamoID = idCounter++,
                Usuario = usuario,
                Libro = libro,
                FechaPrestamo = DateTime.Now,
                FechaDevolucion = DateTime.Now.AddDays(7), // Por ejemplo, 7 días de préstamo
                Estado = Enums.EstadoPrestamo.EN_CURSO
            };

            prestamos.Add(nuevo);
            libro.CantidadDisponible--;

            Console.WriteLine("Préstamo registrado con éxito.");
        }
        //Metodo que muestra los prestamos que se encuentran activos o en curso 
        public void MostrarPrestamosActivos()
        {
            //Prestamos activos
            var activos = prestamos.Where(p => p.Estado == Enums.EstadoPrestamo.EN_CURSO).ToList();

            if (activos.Count == 0)
            {
                Console.WriteLine("No hay préstamos activos.");
                return;
            }
              //Muestra cada prestamo acivo 
            foreach (var p in activos)
            {
                Console.WriteLine($"ID: {p.PrestamoID}, Usuario: {p.Usuario.Nombre}, Libro: {p.Libro.Titulo}, Fecha: {p.FechaPrestamo.ToShortDateString()}");
            }
        }
    }
}