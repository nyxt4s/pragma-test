using Microsoft.AspNetCore.Mvc;
using pragma_api.DTOs;
using pragma_api.helpers;
using pragma_api.Models;
using pragma_api.Services.Interfaces;


namespace pragma_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        /// <response code="200">Devuelve la lista de usuarios</response>
        [HttpGet("getAllUser")]
        [ProducesResponseType(typeof(MessageResponse<IEnumerable<Usuario>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _userService.GetAllAsync();
            return Ok(usuarios);
        }
        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        /// <param name="id">ID del usuario a buscar</param>
        /// <returns>Usuario encontrado</returns>
        /// <response code="200">Si el usuario fue encontrado</response>
        [HttpGet("getUser/{id}")]
        [ProducesResponseType(typeof(MessageResponse<Usuario>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var usuarios = await _userService.GetByIdAsync(id);
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtiene una página de usuarios usando paginación por cursor (Id).
        /// </summary>
        /// <param name="lastId">Id del último usuario de la página anterior (0 para la primera página)</param>
        /// <param name="pageSize">Cantidad de elementos por página</param>
        /// <returns>Página de usuarios</returns>
        /// <response code="200">Devuelve la página de usuarios</response>

        [HttpGet("paginacion-cursor")]
        [ProducesResponseType(typeof(MessageResponse<IEnumerable<Usuario>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNextPage([FromQuery] int lastId = 0, [FromQuery] int pageSize = 10)
        {
            var cursorParams = new CursorPaginationParams
            {
                LastId = lastId,
                PageSize = pageSize
            };

            var response = await _userService.GetNextPageAsync(cursorParams);
            return Ok(response);
        }

        [HttpPost("crear-usuario")]
        [ProducesResponseType(typeof(MessageResponse<Usuario>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageResponse<Usuario>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest(new MessageResponse<Usuario>
                {
                    Message = "El usuario no puede ser nulo",
                    Status = false
                });
            }
            var response = await _userService.AddAsync(userDto);

            return Ok(response);
        }

        [HttpPut("editar-usuario")]
        [ProducesResponseType(typeof(MessageResponse<Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse<Usuario>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UserUpdateDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest(new MessageResponse<Usuario>
                {
                    Message = "El usuario no puede ser nulo",
                    Status = false,
                    Data = null
                });
            }

            var response = await _userService.UpdateAsync(userDto);
            // Si el usuario no existe o la operación no fue exitosa
            if (!response.Status)
            {
                // Puedes personalizar el mensaje para distinguir entre "no encontrado" y otros errores
                if (response.Message == ParamsMessages.RecursoNoEncontrado)
                    return NotFound(response);

                // Otros errores (por ejemplo, validación)
                return BadRequest(response);
            }

            // Si todo salió bien
            return Ok(response);
        }

        [HttpDelete("eliminar-usuario/{id}")]
        [ProducesResponseType(typeof(MessageResponse<Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageResponse<Usuario>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.DeleteAsync(id);

            if (!response.Status)
            {
                // Si el usuario no existe, retorna 404
                if (response.Message == ParamsMessages.RecursoNoEncontrado)
                    return NotFound(response);

                // Otros errores, retorna 400
                return BadRequest(response);
            }

            // Eliminación exitosa
            return Ok(response);
        }

    }
}
