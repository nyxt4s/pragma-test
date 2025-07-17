namespace pragma_api.helpers
{
    /// <summary>
    /// Estructura estándar para las respuestas de la API.
    /// </summary>
    /// <typeparam name="T">Tipo de los datos devueltos.</typeparam>
    public class MessageResponse<T>
    {
        /// <summary>
        /// Mensaje descriptivo de la respuesta.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Datos devueltos por la operación.
        /// </summary>
        public T Data { get; set; } = default!;
    }


}