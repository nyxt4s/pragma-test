import { DataGrid, GridToolbar } from '@mui/x-data-grid';
import type { GridColDef } from '@mui/x-data-grid';
import Paper from '@mui/material/Paper';
import Button from '@mui/material/Button';
import Alert from '@mui/material/Alert';
import Snackbar from '@mui/material/Snackbar';
import { useState, useEffect } from 'react';
import ModalCreate from '../components/ModalCreate';
import ModalEdit from '../components/ModalEdit';
import ModalDelete from '../components/ModalDelete';
import { UserService } from '../services/axiosUsers';
import type { IUser } from '../interfaces';

export default function Home() {
  const [users, setUsers] = useState<IUser[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalCreateOpen, setModalCreateOpen] = useState(false);

  // Estados para editar usuario
  const [modalEditOpen, setModalEditOpen] = useState(false);
  const [userToEdit, setUserToEdit] = useState<IUser | null>(null);

  // Estados para eliminar usuario
  const [modalDeleteOpen, setModalDeleteOpen] = useState(false);
  const [userToDelete, setUserToDelete] = useState<IUser | null>(null);
  const [deleting, setDeleting] = useState(false);
  
  // Estados para notificaciones
  const [snackbar, setSnackbar] = useState({
    open: false,
    message: '',
    severity: 'success' as 'success' | 'error'
  });

  // Función para abrir modal de edición
  const handleEditClick = (user: IUser) => {
    setUserToEdit(user);
    setModalEditOpen(true);
  };

  // Función para cerrar modal de edición
  const handleEditClose = () => {
    setModalEditOpen(false);
    setUserToEdit(null);
  };

  // Función para manejar actualización exitosa
  const handleUserUpdated = () => {
    setSnackbar({
      open: true,
      message: 'Usuario actualizado correctamente',
      severity: 'success'
    });
    loadUsers();
  };

  // Función para abrir modal de confirmación de eliminación
  const handleDeleteClick = (user: IUser) => {
    setUserToDelete(user);
    setModalDeleteOpen(true);
  };

  // Función para cerrar modal de eliminación
  const handleDeleteClose = () => {
    if (!deleting) {
      setModalDeleteOpen(false);
      setUserToDelete(null);
    }
  };

  // Función para confirmar eliminación
  const handleDeleteConfirm = async () => {
    if (!userToDelete || !userToDelete.id) {
      setSnackbar({
        open: true,
        message: 'Error: No se puede eliminar el usuario (ID faltante)',
        severity: 'error'
      });
      return;
    }

    setDeleting(true);
    
    try {
      await UserService.deleteUser(userToDelete.id);
      
      // Mostrar mensaje de éxito
      setSnackbar({
        open: true,
        message: `Usuario ${userToDelete.nombre} eliminado correctamente`,
        severity: 'success'
      });

      // Recargar la lista de usuarios
      loadUsers();
      
    } catch (error: any) {
      console.error('Error eliminando usuario:', error);
      
      // Mostrar mensaje de error
      let errorMessage = 'Error al eliminar el usuario';
      if (error.response?.data?.message) {
        errorMessage = error.response.data.message;
      }
      
      setSnackbar({
        open: true,
        message: errorMessage,
        severity: 'error'
      });
    } finally {
      setDeleting(false);
      setModalDeleteOpen(false);
      setUserToDelete(null);
    }
  };

  // Función para cerrar snackbar
  const handleSnackbarClose = () => {
    setSnackbar(prev => ({ ...prev, open: false }));
  };

  // Definir columnas del DataGrid
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 70, editable: false },
    { field: 'nombre', headerName: 'Nombre', width: 150, editable: false },
    { field: 'rut', headerName: 'Rut', width: 120, editable: false },
    {
      field: 'correo',
      headerName: 'Correo',
      width: 220,
      editable: false
    },
    {
      field: 'fechaNacimiento',
      headerName: 'Fecha Nac.',
      width: 140,
      editable: false,
      renderCell: (params) => {
        if (params.value) {
          return new Date(params.value).toLocaleDateString('es-CL');
        }
        return '';
      }
    },
    {
      field: 'Acciones',
      headerName: 'Acciones',
      description: 'Esta columna muestra acciones disponibles.',
      sortable: false,
      width: 180,
      flex: 0,
      renderCell: (params) => (
        <div style={{ display: 'flex', gap: '4px' }}>
          <Button
            variant="contained"
            size="small"
            color="primary"
            onClick={() => handleEditClick(params.row)}
          >
            Editar
          </Button>
          <Button
            variant="contained"
            size="small"
            color="error"
            onClick={() => handleDeleteClick(params.row)}
            disabled={deleting}
          >
            Eliminar
          </Button>
        </div>
      ),
    },
  ];

  // ✅ Función simplificada para cargar TODOS los usuarios
  const loadUsers = async () => {
    setLoading(true);
    try {
      const allUsers = await UserService.getAllUsers();
      setUsers(allUsers);
      console.log('Usuarios cargados:', allUsers.length);
    } catch (error) {
      console.error(' Error cargando usuarios:', error);
      setUsers([]);
      
      // Mostrar notificación de error
      setSnackbar({
        open: true,
        message: 'Error al cargar los usuarios',
        severity: 'error'
      });
    } finally {
      setLoading(false);
    }
  };

  // Cargar usuarios al montar el componente
  useEffect(() => {
    loadUsers();
  }, []);

  // Función para actualizar manualmente
  const handleRefresh = () => {
    loadUsers();
  };

  return (
    <>  
      <h1>Gestión de Usuarios</h1>
      
      <div style={{ marginBottom: '20px', display: 'flex', gap: '10px', justifyContent: 'flex-end' }}>
        <Button 
          variant="outlined"
          onClick={handleRefresh}
          disabled={loading}
        >
          {loading ? 'Cargando...' : 'Actualizar'}
        </Button>
        
        <Button
          variant="contained"
          color="success"
          onClick={() => setModalCreateOpen(true)}
          disabled={loading}
        >
          + Crear Usuario
        </Button>
      </div>

      {/* Información de debug simplificada */}
      <div style={{ marginBottom: '10px', fontSize: '12px', color: '#666' }}>
        Total usuarios: {users.length}
      </div>

      <Paper sx={{ height: 600, width: '100%' }}>
        <DataGrid
          rows={users}
          showToolbar={true}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: { page: 0, pageSize: 10 }
            }
          }}
          pageSizeOptions={[5, 10, 25, 50]}
          sx={{ border: 0 }}
          slots={{ toolbar: GridToolbar }}
          loading={loading}
          disableRowSelectionOnClick
        />
      </Paper>

      {/* Modal de creación de usuario */}
      <ModalCreate 
        open={modalCreateOpen}
        onClose={() => setModalCreateOpen(false)}
        onUserCreated={() => {
          setSnackbar({
            open: true,
            message: 'Usuario creado correctamente',
            severity: 'success'
          });
          loadUsers(); 
        }}
      />

      {/* Modal de edición de usuario */}
      <ModalEdit
        open={modalEditOpen}
        onClose={handleEditClose}
        onUserUpdated={handleUserUpdated}
        user={userToEdit}
      />

      {/* Modal de confirmación de eliminación */}
      <ModalDelete
        open={modalDeleteOpen}
        onClose={handleDeleteClose}
        onConfirm={handleDeleteConfirm}
        user={userToDelete}
        deleting={deleting}
      />

      {/* Snackbar para notificaciones */}
      <Snackbar
        open={snackbar.open}
        autoHideDuration={4000}
        onClose={handleSnackbarClose}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
      >
        <Alert 
          onClose={handleSnackbarClose} 
          severity={snackbar.severity}
          sx={{ width: '100%' }}
        >
          {snackbar.message}
        </Alert>
      </Snackbar>
    </>
  );
}