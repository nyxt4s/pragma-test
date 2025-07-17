using pragma_api.DTOs;
using pragma_api.helpers;
using pragma_api.Models;
using pragma_api.Repositories.Interfaces;
using pragma_api.Services.Interfaces;

namespace pragma_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<MessageResponse<IEnumerable<Usuario>>> AddAsync(UserDTO usuario)
        {
            try
            {
                // Validar si el RUT ya existe
                var exists = await _userRepository.ExistsByRutAsync(usuario.Rut);
                if (exists)
                {
                    return new MessageResponse<IEnumerable<Usuario>>
                    {
                        Message = "Ya existe un usuario con el mismo RUT.",
                        Status = false,
                        Data = Array.Empty<Usuario>()
                    };
                }

                var result = await _userRepository.AddAsync(usuario);

                if (result == null)
                {
                    return new MessageResponse<IEnumerable<Usuario>>
                    {
                        Message = ParamsMessages.ErrorCrearUsuario,
                        Status = false,
                        Data = new[] { new Usuario() }
                    };
                }

                return new MessageResponse<IEnumerable<Usuario>>
                {
                    Message = ParamsMessages.OperacionExitosa,
                    Status = true,
                    Data = new[] { result }
                };
            }
            catch (Exception)
            {
                return new MessageResponse<IEnumerable<Usuario>>
                {
                    Message = ParamsMessages.ErrorCrearUsuario,
                    Status = false,
                    Data = Array.Empty<Usuario>()
                };
            }
        }

        public async Task<MessageResponse<Usuario>> DeleteAsync(int id)
        {
            try
            {
                // Buscar el usuario antes de eliminar
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new MessageResponse<Usuario>
                    {
                        Message = ParamsMessages.RecursoNoEncontrado,
                        Status = false,
                        Data = null
                    };
                }

                var result = await _userRepository.DeleteAsync(id);

                if (!result)
                {
                    return new MessageResponse<Usuario>
                    {
                        Message = ParamsMessages.ErrorEliminarUsuario,
                        Status = false,
                        Data = null
                    };
                }

                return new MessageResponse<Usuario>
                {
                    Message = ParamsMessages.UsuarioEliminado,
                    Status = true,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse<Usuario>
                {
                    Message = ParamsMessages.ErrorEliminarUsuario + ex.Message,
                    Status = false,
                    Data = null
                };
            }
        }


        public async Task<MessageResponse<IEnumerable<Usuario>>> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return new MessageResponse<IEnumerable<Usuario>>
                {
                    Message = ParamsMessages.OperacionExitosa,
                    Status = true,
                    Data = users
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse<IEnumerable<Usuario>>
                {
                    Message = ParamsMessages.ErrorObtenerUsuario + ex.Message,
                    Status = false,
                    Data = []
                };
            }
        }

        public async Task<MessageResponse<IEnumerable<Usuario>>> GetNextPageAsync(CursorPaginationParams cursorParams)
        {
            try
            {
                var users = await _userRepository.GetNextPageAsync(cursorParams);
                return new MessageResponse<IEnumerable<Usuario>>
                {
                    Message = ParamsMessages.OperacionExitosa,
                    Status = true,
                    Data = users
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse<IEnumerable<Usuario>>
                {
                    Message = ParamsMessages.ErrorObtenerUsuario + ex.Message,
                    Status = false,
                    Data = new List<Usuario>()
                };
            }
        }

        public async Task<MessageResponse<IEnumerable<Usuario?>>> GetByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);

                if (user == null)
                {
                        return new MessageResponse<IEnumerable<Usuario?>>
                    {
                        Message = ParamsMessages.RecursoNoEncontrado,
                        Status = false,
                        Data = []
                    };
                }
                return new MessageResponse<IEnumerable<Usuario?>>
                {
                    Message = ParamsMessages.OperacionExitosa,
                    Status = true,
                    Data = [user]
                };

            }
            catch (Exception ex)
            {
                return new MessageResponse<IEnumerable<Usuario?>>
                {
                    Message = ParamsMessages.ErrorObtenerUsuario + ex.Message,
                    Status = false,
                    Data = new List<Usuario>()
                };
            }
        }


        public async Task<MessageResponse<Usuario>> UpdateAsync(UserUpdateDTO usuarioDto)
        {
            try
            {
                var updatedUser = await _userRepository.UpdateAsync(usuarioDto);
                if (updatedUser == null)
                {
                    return new MessageResponse<Usuario>
                    {
                        Message = ParamsMessages.RecursoNoEncontrado,
                        Status = false,
                        Data = null
                    };
                }

                return new MessageResponse<Usuario>
                {
                    Message = ParamsMessages.OperacionExitosa,
                    Status = true,
                    Data = updatedUser
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse<Usuario>
                {
                    Message = ParamsMessages.ErrorActualizarUsuario + ex.Message,
                    Status = false,
                    Data = null
                };
            }
        }
    }
}
