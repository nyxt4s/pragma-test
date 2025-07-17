namespace pragma_api.DTOs
{
    /// <summary>
    /// DTO para la creación y transferencia de datos de usuario.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// RUT chileno del usuario (único).
        /// </summary>
        public string Rut { get; set; } = string.Empty;

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