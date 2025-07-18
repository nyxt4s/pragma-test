import axios from 'axios';
import type { IUser, IUserResponse, PaginationParams } from '../interfaces';


// Configuración base para usuarios
const userAPI = axios.create({
  baseURL: 'http://localhost:8080/api/User',
  headers: {
    'Content-Type': 'application/json',
    'accept': 'text/plain'
  }
});


// Servicios de usuario
export class UserService {
  
  // Obtener todos los usuarios
  static async getAllUsers(): Promise<IUser[]> {
    try {
      const response = await userAPI.get('/getAllUser');
      return response.data.data || response.data;
    } catch (error) {
      console.error('Error obteniendo usuarios:', error);
      throw error;
    }
  }


  // Obtener usuario por ID
  static async getUserById(id: number): Promise<IUser> {
    try {
      const response = await userAPI.get(`/${id}`);
      return response.data;
    } catch (error) {
      console.error('Error obteniendo usuario por ID:', error);
      throw error;
    }
  }

  // Crear nuevo usuario
  static async createUser(usuario: Omit<IUser, 'Rut'>): Promise<IUser> {
    try {
      const response = await userAPI.post('', usuario);
      return response.data;
    } catch (error) {
      console.error('Error creando usuario:', error);
      throw error;
    }
  }

  // Actualizar usuario
  static async updateUser(usuario: Partial<IUser>): Promise<IUser> {
    try {
      const response = await userAPI.put(`/editar-usuario/`, usuario);
      return response.data;
    } catch (error) {
      console.error('Error actualizando usuario:', error);
      throw error;
    }
  }

  // Eliminar usuario
  static async deleteUser(id: number): Promise<void> {
    try {
      await userAPI.delete(`/eliminar-usuario/${id}`);
    } catch (error) {
      console.error('Error eliminando usuario:', error);
      throw error;
    }
  }

}

// Exportar instancia configurada para uso directo
export default userAPI;

