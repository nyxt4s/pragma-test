namespace pragma_api.helpers
{
    public class CursorPaginationParams
    {
        /// <summary>
        /// Tamaño máximo de página
        /// </summary>
        private const int MaxPageSize = 50;

        private int _pageSize = 10;

        /// <summary>
        /// ID del último usuario de la página anterior (0 para primera página)
        /// </summary>
        public int LastId { get; set; } = 0;

        /// <summary>
        /// Cantidad de elementos por página
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}