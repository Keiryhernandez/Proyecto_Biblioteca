using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Enums;

namespace ProyectoBiblioteca.Services
{
    /// <summary>
    /// Servicio encargado de gestionar los usuarios de la biblioteca,
    /// incluyendo registro, inicio de sesión, edición, eliminación y roles.
    /// </summary>
    public class UsuarioService
    {
        // Lista de usuarios registrados en el sistema.
        private List<Usuario> usuarios = new List<Usuario>();

        // Contador para asignar IDs únicos a los usuarios.
        private int idCounter = 1;

        /// <summary>
        /// Crea un usuario administrador por defecto al iniciar el sistema.
        /// </summary>
        public void CrearUsuarioAdmin()
        {
            usuarios.Add(new Usuario
            {
                UsuarioID = idCounter++,
                Nombre = "Admin",
                Email = "admin@gmail.com",
                Contraseña = "admin1234",
                TipoUsuario = TipoUsuario.ADMIN,
                FechaRegistro = DateTime.Now,
                Estado = true
            });

            Console.WriteLine("Usuario administrador creado.");
        }

        /// <summary>
        /// Permite registrar un nuevo usuario lector solicitando datos por consola.
        /// </summary>
        public void RegistrarUsuario()
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Contraseña: ");
            string contraseña = Console.ReadLine();

            var nuevo = new Usuario
            {
                UsuarioID = idCounter++,
                Nombre = nombre,
                Email = email,
                Contraseña = contraseña,
                TipoUsuario = TipoUsuario.LECTOR,
                FechaRegistro = DateTime.Now,
                Estado = true
            };

            usuarios.Add(nuevo);
            Console.WriteLine("Usuario registrado con éxito.");
            Console.ReadKey();
        }

        /// <summary>
        /// Método de autenticación de usuarios. Redirige al menú correspondiente según el tipo de usuario.
        /// </summary>
        public void Login(LibroService libroService, PrestamoService prestamoService)
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Contraseña: ");
            string password = Console.ReadLine();

            var user = usuarios.FirstOrDefault(u => u.Email == email && u.Contraseña == password);

            if (user == null)
            {
                Console.WriteLine("Credenciales inválidas.");
                Console.ReadKey();
                return;
            }

            if (user.TipoUsuario == TipoUsuario.ADMIN)
                MenuAdmin(libroService, this, prestamoService);
            else
                MenuLector(user, libroService, prestamoService);
        }

        /// <summary>
        /// Muestra el menú de opciones para usuarios administradores.
        /// </summary>
        private void MenuAdmin(LibroService libroService, UsuarioService usuarioService, PrestamoService prestamoService)
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Menú Administrador ===");
                Console.WriteLine("1. Gestión de libros");
                Console.WriteLine("2. Gestión de categorías");
                Console.WriteLine("3. Gestión de usuarios");
                Console.WriteLine("4. Consultar reportes de préstamos");
                Console.WriteLine("5. Consultar historial de préstamos");
                Console.WriteLine("6. Asignar roles / Actualizar perfil");
                Console.WriteLine("0. Cerrar sesión");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MenuGestionLibros(libroService);
                        break;
                    case "2":
                        MenuGestionCategorias(libroService);
                        break;
                    case "3":
                        MenuGestionUsuarios(usuarioService);
                        break;
                    case "4":
                        prestamoService.MostrarReportesPrestamos();
                        Console.ReadKey();
                        break;
                    case "5":
                        prestamoService.MostrarHistorialPrestamos();
                        Console.ReadKey();
                        break;
                    case "6":
                        usuarioService.AsignarRolesYActualizarPerfil();
                        Console.ReadKey();
                        break;
                    case "0":
                        Console.WriteLine("Cerrando sesión...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != "0");
        }

        /// <summary>
        /// Submenú de gestión de libros.
        /// </summary>
        private void MenuGestionLibros(LibroService libroService)
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Gestión de libros ===");
                Console.WriteLine("1. Agregar libro");
                Console.WriteLine("2. Editar libro");
                Console.WriteLine("3. Eliminar libro");
                Console.WriteLine("4. Consultar libros");
                Console.WriteLine("0. Volver");
                Console.Write("Opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        libroService.AgregarLibro();
                        break;
                    case "2":
                        libroService.EditarLibro();
                        break;
                    case "3":
                        libroService.EliminarLibro();
                        break;
                    case "4":
                        libroService.MostrarLibros();
                        Console.ReadKey();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != "0");
        }

        /// <summary>
        /// Submenú de gestión de categorías.
        /// </summary>
        private void MenuGestionCategorias(LibroService libroService)
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Gestión de categorías ===");
                Console.WriteLine("1. Agregar categoría");
                Console.WriteLine("2. Editar categoría");
                Console.WriteLine("3. Eliminar categoría");
                Console.WriteLine("4. Consultar categorías");
                Console.WriteLine("0. Volver");
                Console.Write("Opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        libroService.AgregarCategoria();
                        break;
                    case "2":
                        libroService.EditarCategoria();
                        break;
                    case "3":
                        libroService.EliminarCategoria();
                        break;
                    case "4":
                        libroService.MostrarCategorias();
                        Console.ReadKey();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != "0");
        }

        /// <summary>
        /// Submenú para la gestión de usuarios.
        /// </summary>
        private void MenuGestionUsuarios(UsuarioService usuarioService)
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Gestión de usuarios ===");
                Console.WriteLine("1. Listar usuarios");
                Console.WriteLine("2. Editar usuario");
                Console.WriteLine("3. Eliminar usuario");
                Console.WriteLine("0. Volver");
                Console.Write("Opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        usuarioService.ListarUsuarios();
                        Console.ReadKey();
                        break;
                    case "2":
                        usuarioService.EditarUsuario();
                        break;
                    case "3":
                        usuarioService.EliminarUsuario();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != "0");
        }

        /// <summary>
        /// Muestra todos los usuarios registrados en el sistema.
        /// </summary>
        public void ListarUsuarios()
        {
            if (usuarios.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
            }
            else
            {
                foreach (var u in usuarios)
                {
                    Console.WriteLine($"ID: {u.UsuarioID}, Nombre: {u.Nombre}, Email: {u.Email}, Tipo: {u.TipoUsuario}");
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Permite editar el nombre y correo electrónico de un usuario existente.
        /// </summary>
        public void EditarUsuario()
        {
            Console.Write("ID del usuario a editar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = ObtenerUsuarioPorId(id);
                if (user != null)
                {
                    Console.Write("Nuevo nombre: ");
                    user.Nombre = Console.ReadLine();
                    Console.Write("Nuevo email: ");
                    user.Email = Console.ReadLine();

                    Console.WriteLine("Usuario actualizado.");
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Permite eliminar un usuario por ID.
        /// </summary>
        public void EliminarUsuario()
        {
            Console.Write("ID del usuario a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = ObtenerUsuarioPorId(id);
                if (user != null)
                {
                    usuarios.Remove(user);
                    Console.WriteLine("Usuario eliminado.");
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Asigna un nuevo rol a un usuario o permite actualizar su perfil.
        /// </summary>
        public void AsignarRolesYActualizarPerfil()
        {
            Console.Write("ID del usuario para asignar rol o actualizar perfil: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = ObtenerUsuarioPorId(id);
                if (user != null)
                {
                    Console.WriteLine($"Usuario: {user.Nombre}, Rol actual: {user.TipoUsuario}");
                    Console.WriteLine("Roles disponibles:");
                    foreach (var tipo in Enum.GetValues(typeof(TipoUsuario)))
                    {
                        Console.WriteLine((int)tipo + ". " + tipo);
                    }
                    Console.Write("Seleccione nuevo rol (número): ");
                    if (int.TryParse(Console.ReadLine(), out int rolNum) && Enum.IsDefined(typeof(TipoUsuario), rolNum))
                    {
                        user.TipoUsuario = (TipoUsuario)rolNum;
                        Console.WriteLine("Rol actualizado.");
                    }
                    else
                    {
                        Console.WriteLine("Rol inválido.");
                    }
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Muestra el menú para usuarios lectores y sus opciones.
        /// </summary>
        private void MenuLector(Usuario usuario, LibroService libroService, PrestamoService prestamoService)
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Menú Lector ===");
                Console.WriteLine("1. Buscar libros");
                Console.WriteLine("2. Solicitar préstamo");
                Console.WriteLine("3. Ver préstamos activos");
                Console.WriteLine("4. Reservar libro");
                Console.WriteLine("5. Actualizar perfil");
                Console.WriteLine("6. Dejar comentario");
                Console.WriteLine("7. Cancelar solicitud de reserva");
                Console.WriteLine("0. Cerrar sesión");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        libroService.MostrarLibros();
                        Console.ReadKey();
                        break;

                    case "2":
                        libroService.MostrarLibros();
                        Console.Write("ID del libro a prestar: ");
                        if (int.TryParse(Console.ReadLine(), out int idPrestamo))
                        {
                            var libro = libroService.ObtenerLibroPorID(idPrestamo);
                            if (libro != null)
                                prestamoService.RegistrarPrestamo(usuario, libro);
                            else
                                Console.WriteLine("Libro no encontrado.");
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        Console.ReadKey();
                        break;

                    case "3":
                        prestamoService.MostrarPrestamosActivos();
                        Console.ReadKey();
                        break;

                    case "4":
                        libroService.MostrarLibros();
                        Console.Write("ID del libro a reservar: ");
                        if (int.TryParse(Console.ReadLine(), out int idReserva))
                        {
                            prestamoService.ReservarLibroNoDisponible(idReserva, usuario.UsuarioID);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        Console.ReadKey();
                        break;

                    case "5":
                        Console.Write("Nuevo nombre: ");
                        usuario.Nombre = Console.ReadLine();
                        Console.WriteLine("Perfil actualizado.");
                        Console.ReadKey();
                        break;

                    case "6":
                        Console.Write("Comentario: ");
                        string comentario = Console.ReadLine();
                        Console.WriteLine("Gracias por tu comentario.");
                        Console.ReadKey();
                        break;

                    case "7":
                        prestamoService.CancelarSolicitud(usuario.UsuarioID);
                        Console.ReadKey();
                        break;
                }

            } while (opcion != "0");
        }

        /// <summary>
        /// Busca un usuario por su ID.
        /// </summary>
        public Usuario ObtenerUsuarioPorId(int id)
        {
            return usuarios.FirstOrDefault(u => u.UsuarioID == id);
        }
    }
}
