namespace pragma_api.DTOs
{
    /// <summary>
    /// DTO para la actualización de datos de un usuario existente.
    /// </summary>
    public class UserUpdateDTO
    {
        /// <summary>
        /// Identificador único del usuario a actualizar.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario (opcional).
        /// </summary>
        public string? Correo { get; set; }

        /// <summary>
        /// Fecha de nacimiento del usuario.
        /// </summary>
        public DateOnly FechaNacimiento { get; set; }
    }
}