namespace pragma_api.helpers
{
    public static class ParamsMessages
    {
        #region Mensajes Genéricos
        public const string OperacionExitosa = "La operación se realizó con éxito.";
        public const string OperacionFallida = "La operación no pudo ser completada.";
        public const string ErrorInesperado = "Ocurrió un error inesperado. Por favor, inténtelo nuevamente.";
        public const string RecursoNoEncontrado = "El recurso solicitado no fue encontrado.";
        #endregion

        #region Mensajes de Usuario
        public const string UsuariosObtenidos = "Usuarios obtenidos correctamente.";
        public const string UsuarioObtenido = "Usuario obtenido correctamente.";
        public const string UsuarioNoEncontrado = "No se encuentra el usuario.";
        public const string UsuarioCreado = "Usuario creado correctamente.";
        public const string UsuarioActualizado = "Usuario actualizado correctamente.";
        public const string UsuarioEliminado = "Usuario eliminado correctamente.";
        public const string RutDuplicado = "Ya existe un usuario registrado con este RUT.";
        #endregion

        #region Mensajes de Error
        public const string ErrorObtenerUsuarios = "Error al obtener los usuarios: {0}";
        public const string ErrorObtenerUsuario = "Error al obtener el usuario: {0}";
        public const string ErrorCrearUsuario = "Error al crear el usuario: {0}";
        public const string ErrorActualizarUsuario = "Error al actualizar el usuario: {0}";
        public const string ErrorEliminarUsuario = "Error al eliminar el usuario: {0}";
        #endregion
    }
}
