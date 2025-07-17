using Microsoft.EntityFrameworkCore;
using pragma_api.DTOs;
using pragma_api.helpers;
using pragma_api.Models;
using pragma_api.Repositories.Interfaces;

namespace pragma_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PragmadbContext _context;

        public UserRepository(PragmadbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AddAsync(UserDTO usuario)
        {
            try
            {
                var user = new Usuario
                {
                    Nombre = usuario.Nombre,
                    Rut = usuario.Rut,
                    Correo = usuario.Correo,
                    FechaNacimiento = usuario.FechaNacimiento
                };

                await _context.Usuarios.AddAsync(user);
                var result = await _context.SaveChangesAsync();

                // Validar que se haya guardado al menos un registro
                if (result == 0 || user.Id == 0)
                {
                    throw new Exception("No se pudo crear el usuario en la base de datos.");
                }

                var createdUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Rut == usuario.Rut);
                return createdUser ?? throw new Exception("No se pudo recuperar el usuario creado.");

            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error al agregar el usuario a la base de datos: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al agregar el usuario: {ex.Message}", ex);
            }
        }

        public async Task<Boolean> DeleteAsync(int id)
        {
            try
            {
                var user = await _context.Usuarios.FindAsync(id);
                if (user == null)
                {
                    return false;
                }

                _context.Usuarios.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error al eliminar el usuario de la base de datos: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al eliminar el usuario: {ex.Message}", ex);
            }
        }

        public Task<bool> ExistsByRutAsync(string rut)
        {
            try
            {
               var user = _context.Usuarios.AnyAsync(u => u.Rut == rut);
                
                return user;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error al consultar la base de datos: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al verificar el RUT: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            try
            {
                return await _context.Usuarios.ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Excepción específica de Entity Framework
                throw new Exception($"Error al consultar la base de datos: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                // Excepción genérica
                throw new Exception($"Error inesperado al obtener usuarios: {ex.Message}", ex);
            }
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Usuarios.FindAsync(id);
            }
            catch (DbUpdateException dbEx)
            {
                // Excepción específica de Entity Framework
                throw new Exception($"Error al consultar la base de datos: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                // Excepción genérica
                throw new Exception($"Error inesperado al obtener usuarios: {ex.Message}", ex);
            }
        }


        public async Task<Usuario?> UpdateAsync(UserUpdateDTO usuarioDto)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(usuarioDto.Id);
                if (usuario == null)
                    return null;

                usuario.Nombre = usuarioDto.Nombre;
                usuario.Correo = usuarioDto.Correo;
                usuario.FechaNacimiento = usuarioDto.FechaNacimiento;

                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error al actualizar el usuario: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al actualizar el usuario: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Usuario>> GetNextPageAsync(CursorPaginationParams cursorParams)
        {
            try
            {
                if (cursorParams.LastId == 0)
                {
                    return await _context.Usuarios
                        .OrderBy(u => u.Id)
                        .Take(cursorParams.PageSize)
                        .ToListAsync();
                }

                return await _context.Usuarios
                    .Where(u => u.Id > cursorParams.LastId)
                    .OrderBy(u => u.Id)
                    .Take(cursorParams.PageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la página de usuarios: {ex.Message}", ex);
            }
        }
    }
}
