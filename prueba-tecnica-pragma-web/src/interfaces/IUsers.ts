// Interfaces de Usuarios
export interface IUser {
  id?: number;
  nombre: string;
  rut: string;
  correo: string;
  fechaNacimiento: string;
}

export interface IUserResponse {
  data: IUser[];
  total?: number;
  page?: number;
  pageSize?: number;
}

export interface PaginationParams {
  page?: number;
  pageSize?: number;
}

export interface UserForm {
  nombre: string;
  rut: string;
  correo?: string;
  fechaNacimiento: string;
}