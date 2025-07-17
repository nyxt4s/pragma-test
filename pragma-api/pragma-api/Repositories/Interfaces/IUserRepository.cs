using pragma_api.DTOs;
using pragma_api.helpers;
using pragma_api.Models;

namespace pragma_api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync(); //done
        Task<IEnumerable<Usuario>> GetNextPageAsync(CursorPaginationParams cursorParams);  //done
        Task<Usuario?> GetByIdAsync(int id);  //done
        Task<Usuario> AddAsync(UserDTO usuario); //done
        Task<Usuario?> UpdateAsync(UserUpdateDTO usuario); //done
        Task<Boolean> DeleteAsync(int id);
        Task<bool> ExistsByRutAsync(string rut);
    }
}

