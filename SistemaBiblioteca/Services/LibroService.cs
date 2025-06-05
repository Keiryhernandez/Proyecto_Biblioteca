using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Services
{
    
    public class LibroService
    {
        //Lista que almacena los libros registrados 
        private List<Libro> libros = new List<Libro>();

        //Contador que asigna un ID unico a cada libros 
        private int idLibro = 1;

        //Metodo para agregar un nuevo libro al sistema 
        public void AgregarLibro()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            libros.Add(new Libro
            {
                LibroID = idLibro++,
                Titulo = titulo,
                CantidadTotal = 120, //se coloco un valor predeterminado
                CantidadDisponible = 120 //se coloco un valor predeterminado
            });

            Console.WriteLine("Libro agregado con éxito.");
            Console.ReadKey();
        }

        //Muestra la lista de libros registrados junto con su disponibilidad 

        public void MostrarLibros()
        {
            foreach (var libro in libros)
            {
                Console.WriteLine($"{libro.LibroID} - {libro.Titulo} - Disponible: {libro.CantidadDisponible}");
            }
            Console.ReadKey();
        }
    }
}