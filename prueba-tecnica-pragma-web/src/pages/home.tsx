import { DataGrid, GridToolbar } from '@mui/x-data-grid';
import type { GridColDef } from '@mui/x-data-grid';
import Paper from '@mui/material/Paper';
import Button from '@mui/material/Button';
import { useState, useEffect } from 'react';
import axios from 'axios';



const columns: GridColDef[] = [
  { field: 'nombre', headerName: 'Nombre', width: 120, editable: true },
  { field: 'rut', headerName: 'Rut', width: 110, editable: true },
  {
    field: 'correo',
    headerName: 'Correo',
    width: 240,
    editable: true
  },
  {
    field: 'fechaNacimiento',
    headerName: 'Fecha Nac.',
    width: 120,
    editable: true
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
          onClick={() => {
            console.log('Editar usuario:', params.row);
          }}
        >
          Editar
        </Button>
        <Button
          variant="contained"
          size="small"
          color="error"
          onClick={() => {
            console.log('Eliminar usuario:', params.row);
          }}
        >
          Eliminar
        </Button>
      </div>
    ),
  },

];

// Función para obtener usuarios desde la API
const fetchUsers = async () => {
  try {
    const response = await axios.get('http://localhost:8080/api/User/getAllUser', {
      headers: {
        'accept': 'text/plain'
      }
    });
    console.log('Usuarios obtenidos:', response.data);
    return response.data;
  } catch (error) {
    console.error('Error al obtener usuarios:', error);
    throw error;
  }
};

const rows :any = [];

const paginationModel = { page: 0, pageSize: 5 };

export default function Home() {
  const [users, setUsers] = useState(rows); // Inicializar con datos estáticos
  const [loading, setLoading] = useState(false);

  // Función para cargar usuarios desde la API
  const loadUsersFromAPI = async () => {
    setLoading(true);
    try {
      const apiUsers = await fetchUsers();
      setUsers(apiUsers.data);
    } catch (error) {
      console.error('Error cargando usuarios desde API:', error);
      // Si falla, mantener los datos estáticos
    } finally {
      setLoading(false);
    }
  };

  // Cargar datos al montar el componente
  useEffect(() => {
    loadUsersFromAPI();
  }, []);

  return (
    <>  
    <h1>busqueda</h1>
    
    <div style={{ marginBottom: '20px' }}>
      <Button 
        variant="contained" 
        onClick={loadUsersFromAPI}
        disabled={loading}
      >
        {loading ? 'Cargando...' : 'Actualizar desde API'}
      </Button>
    </div>

     <Paper sx={{ height: 600, width: '100%' }}>
  
      <DataGrid
        rows={users}
        columns={columns}
        initialState={{ pagination: { paginationModel }}}
        pageSizeOptions={[5, 10]}
        sx={{ border: 0 }}
        showToolbar={true}
        loading={loading}
      
      />
    </Paper>
    </>
   
  );
}
