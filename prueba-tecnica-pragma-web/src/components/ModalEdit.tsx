import React, { useState, useEffect } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  Box,
  Alert
} from '@mui/material';
import type { IUser } from '../interfaces';
import { UserService } from '../services/axiosUsers';

interface ModalEditProps {
  open: boolean;
  onClose: () => void;
  onUserUpdated: () => void;
  user: IUser | null;
}

const ModalEdit: React.FC<ModalEditProps> = ({ open, onClose, onUserUpdated, user }) => {
  const [formData, setFormData] = useState({
    nombre: '',
    rut: '',
    correo: '',
    fechaNacimiento: ''
  });
  
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string>('');

  // Llenar el formulario cuando se abre el modal con datos del usuario
  useEffect(() => {
    if (open && user) {
      setFormData({
        nombre: user.nombre || '',
        rut: user.rut || '',
        correo: user.correo || '',
        fechaNacimiento: user.fechaNacimiento || ''
      });
      setError('');
    }
  }, [open, user]);

  // Handler para cambios en el formulario
  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async () => {
    if (!user?.id) {
      setError('Error: ID de usuario no válido');
      return;
    }

    if (!formData.nombre || !formData.rut || !formData.correo || !formData.fechaNacimiento) {
      setError('Todos los campos son requeridos');
      return;
    }

    setLoading(true);
    setError('');

    try {
      await UserService.updateUser( {
        id: user.id,
        nombre: formData.nombre,
        correo: formData.correo,
        fechaNacimiento: formData.fechaNacimiento
        });

      onUserUpdated();
      onClose();
      
    } catch (error: any) {
      console.error('Error actualizando usuario:', error);
      
      let errorMessage = 'Error al actualizar el usuario';
      if (error.response?.data?.message) {
        errorMessage = error.response.data.message;
      } else if (error.response?.status === 404) {
        errorMessage = 'Usuario no encontrado';
      } else if (error.response?.status === 400) {
        errorMessage = 'Datos inválidos';
      }
      
      setError(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  const handleClose = () => {
    if (!loading) {
      setFormData({
        nombre: '',
        rut: '',
        correo: '',
        fechaNacimiento: ''
      });
      setError('');
      onClose();
    }
  };

  return (
    <Dialog 
      open={open} 
      onClose={handleClose}
      maxWidth="sm"
      fullWidth
    >
      <DialogTitle>
        Editar Usuario
      </DialogTitle>
      
      <DialogContent>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, pt: 1 }}>
          {error && (
            <Alert severity="error">
              {error}
            </Alert>
          )}
          
          <TextField
            name="nombre"
            label="Nombre"
            value={formData.nombre}
            onChange={handleChange}
            fullWidth
            disabled={loading}
          />
          
          <TextField
            name="rut"
            label="RUT (No editable)"
            value={formData.rut}
            fullWidth
            InputProps={{
                readOnly: true,
            }}
            sx={{
                '& .MuiInputBase-input': {
                backgroundColor: '#f5f5f5',
                color: '#666',
                }
            }}
            placeholder="12345678-9"
            />
                    
          <TextField
            name="correo"
            label="Correo"
            type="email"
            value={formData.correo}
            onChange={handleChange}
            fullWidth
            disabled={loading}
          />
          
          <TextField
            name="fechaNacimiento"
            label="Fecha de Nacimiento"
            type="date"
            value={formData.fechaNacimiento}
            onChange={handleChange}
            fullWidth
            disabled={loading}
            InputLabelProps={{
              shrink: true,
            }}
          />
        </Box>
      </DialogContent>
      
      <DialogActions>
        <Button 
          onClick={handleClose}
          disabled={loading}
        >
          Cancelar
        </Button>
        <Button 
          onClick={handleSubmit}
          variant="contained"
          disabled={loading}
        >
          {loading ? 'Actualizando...' : 'Actualizar Usuario'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ModalEdit;