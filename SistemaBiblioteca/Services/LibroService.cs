using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoBiblioteca.Services
{
    public class LibroService
    {
        // Lista que almacena los libros registrados 
        private List<Libro> libros = new List<Libro>();

        // Contador que asigna un ID único a cada libro 
        private int idLibro = 1;

        // Lista de categorías disponibles
        private List<string> categorias = new List<string>();

        // Método para agregar un nuevo libro al sistema 
        public void AgregarLibro()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            libros.Add(new Libro
            {
                LibroID = idLibro++,
                Titulo = titulo,
                CantidadTotal = 120,       // Valor predeterminado
                CantidadDisponible = 120   // Valor predeterminado
            });

            Console.WriteLine("Libro agregado con éxito.");
            Console.ReadKey();
        }

        // Muestra la lista de libros registrados junto con su disponibilidad 
        public void MostrarLibros()
        {
            Console.WriteLine("Listado de libros:");
            foreach (var libro in libros)
            {
                Console.WriteLine($"{libro.LibroID} - {libro.Titulo} - Disponible: {libro.CantidadDisponible}");
            }
            Console.ReadKey();
        }

        // Obtener libro por ID (mayúscula y minúscula)
        public Libro ObtenerLibroPorID(int id)
        {
            return libros.FirstOrDefault(l => l.LibroID == id);
        }

        public Libro ObtenerLibroPorId(int id)
        {
            return ObtenerLibroPorID(id); // Reusar método
        }

        // Editar información de un libro existente
        public void EditarLibro()
        {
            Console.Write("ID del libro a editar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var libro = ObtenerLibroPorID(id);
                if (libro != null)
                {
                    Console.Write("Nuevo título: ");
                    libro.Titulo = Console.ReadLine();

                    Console.Write("Nueva cantidad total: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidadTotal))
                    {
                        int diferencia = cantidadTotal - libro.CantidadTotal;
                        libro.CantidadTotal = cantidadTotal;
                        libro.CantidadDisponible += diferencia; // Ajustar disponible según cambio total
                        if (libro.CantidadDisponible < 0) libro.CantidadDisponible = 0;
                    }
                    Console.WriteLine("Libro actualizado.");
                }
                else
                {
                    Console.WriteLine("Libro no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.ReadKey();
        }

        // Eliminar un libro del sistema
        public void EliminarLibro()
        {
            Console.Write("ID del libro a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var libro = ObtenerLibroPorID(id);
                if (libro != null)
                {
                    libros.Remove(libro);
                    Console.WriteLine("Libro eliminado.");
                }
                else
                {
                    Console.WriteLine("Libro no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.ReadKey();
        }

        // Agregar una nueva categoría
        public void AgregarCategoria()
        {
            Console.Write("Nombre de la nueva categoría: ");
            string nombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nombre) && !categorias.Contains(nombre))
            {
                categorias.Add(nombre);
                Console.WriteLine("Categoría agregada.");
            }
            else
            {
                Console.WriteLine("Categoría inválida o ya existe.");
            }
            Console.ReadKey();
        }

        // Editar una categoría existente
        public void EditarCategoria()
        {
            MostrarCategorias();
            Console.Write("Nombre de la categoría a editar: ");
            string vieja = Console.ReadLine();

            if (categorias.Contains(vieja))
            {
                Console.Write("Nuevo nombre: ");
                string nueva = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nueva) && !categorias.Contains(nueva))
                {
                    int index = categorias.IndexOf(vieja);
                    categorias[index] = nueva;
                    Console.WriteLine("Categoría actualizada.");
                }
                else
                {
                    Console.WriteLine("Nombre inválido o ya existe.");
                }
            }
            else
            {
                Console.WriteLine("Categoría no encontrada.");
            }
            Console.ReadKey();
        }

        // Eliminar una categoría
        public void EliminarCategoria()
        {
            MostrarCategorias();
            Console.Write("Nombre de la categoría a eliminar: ");
            string nombre = Console.ReadLine();
            if (categorias.Remove(nombre))
            {
                Console.WriteLine("Categoría eliminada.");
            }
            else
            {
                Console.WriteLine("Categoría no encontrada.");
            }
            Console.ReadKey();
        }

        // Mostrar todas las categorías disponibles
        public void MostrarCategorias()
        {
            Console.WriteLine("Categorías:");
            foreach (var cat in categorias)
            {
                Console.WriteLine("- " + cat);
            }
            Console.ReadKey();
        }
    }
}
