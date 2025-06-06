namespace ProyectoBiblioteca.Enums
{
    // Enumeración que representa los posibles estados de un préstamo de libro
    public enum EstadoPrestamo
    {
        EN_CURSO,   // Préstamo activo, libro actualmente en posesión del usuario
        DEVUELTO,   // Préstamo completado, libro devuelto a la biblioteca
        ATRASADO,   // Préstamo vencido, libro no devuelto a tiempo
        SOLICITADO  // Préstamo solicitado pero aún no aprobado o entregado
    }
}
