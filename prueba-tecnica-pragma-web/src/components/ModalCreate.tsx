import React, { useState } from 'react';
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
import type { UserForm } from '../interfaces/IUsers';
import axiosUsers from '../services/axiosUsers';

interface ModalCreateProps {
  open: boolean;
  onClose: () => void;
  onUserCreated: () => void;
}


const ModalCreate: React.FC<ModalCreateProps> = ({ open, onClose, onUserCreated }) => {
  const [formData, setFormData] = useState<UserForm>({
    nombre: '',
    rut: '',
    correo: '',
    fechaNacimiento: ''
  });
  
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string>('');


   const handleChange = (field: keyof UserForm) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData(prev => ({
      ...prev,
      [field]: event.target.value
    }));
  };

  
  const handleSubmit = async () => {
    if (!formData.nombre || !formData.rut || !formData.fechaNacimiento) {
      setError('Todos los campos son requeridos');
      return;
    }

    setLoading(true);
    setError('');

    try {

    await axiosUsers.post('/crear-usuario', formData);

      setFormData({
        nombre: '',
        rut: '',
        correo: '',
        fechaNacimiento: ''
      });
      
      onUserCreated(); // Actualizar lista
      onClose(); // Cerrar modal
      
    } catch (error: any) {
      console.error('Error creando usuario:', error);
      setError('Error al crear el usuario. Intenta nuevamente.');
    } finally {
      setLoading(false);
    }
  };

  // Cerrar modal
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
        Crear Nuevo Usuario
      </DialogTitle>
      
      <DialogContent>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, pt: 1 }}>
          {error && (
            <Alert severity="error">
              {error}
            </Alert>
          )}
          
          <TextField
            label="Nombre"
            value={formData.nombre}
            onChange={handleChange('nombre')}
            fullWidth
            disabled={loading}
          />
          
          <TextField
            label="RUT"
            value={formData.rut}
            onChange={handleChange('rut')}
            fullWidth
            disabled={loading}
            placeholder="12345678-9"
          />
          
          <TextField
            label="Correo"
            type="email"
            value={formData.correo}
            onChange={handleChange('correo')}
            fullWidth
            disabled={loading}
          />
          
          <TextField
            label="Fecha de Nacimiento"
            type="date"
            value={formData.fechaNacimiento}
            onChange={handleChange('fechaNacimiento')}
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
          {loading ? 'Creando...' : 'Crear Usuario'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ModalCreate;