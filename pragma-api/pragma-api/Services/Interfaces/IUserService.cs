using pragma_api.DTOs;
using pragma_api.helpers;
using pragma_api.Models;
using pragma_api.DTOs;

namespace pragma_api.Services.Interfaces
{
    public interface IUserService
    {
        Task<MessageResponse<IEnumerable<Usuario>>> GetAllAsync(); //done
        Task<MessageResponse<IEnumerable<Usuario?>>> GetByIdAsync(int id); //done
        Task<MessageResponse<IEnumerable<Usuario>>> GetNextPageAsync(CursorPaginationParams cursorParams); //done
        Task <MessageResponse<IEnumerable<Usuario>>> AddAsync(UserDTO usuario); //doine
        Task<MessageResponse<Usuario>> UpdateAsync(UserUpdateDTO usuario); //done
        Task<MessageResponse<Usuario>> DeleteAsync(int id);
    }
}
