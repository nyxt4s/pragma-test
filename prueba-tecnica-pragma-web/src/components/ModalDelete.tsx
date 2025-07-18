import React from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  DialogContentText,
  Button
} from '@mui/material';
import type { IUser } from '../interfaces';

interface ModalDeleteProps {
  open: boolean;
  onClose: () => void;
  onConfirm: () => void;
  user: IUser | null;
  deleting: boolean;
}

const ModalDelete: React.FC<ModalDeleteProps> = ({
  open,
  onClose,
  onConfirm,
  user,
  deleting
}) => {
  return (
    <Dialog
      open={open}
      onClose={onClose}
      aria-labelledby="delete-dialog-title"
      aria-describedby="delete-dialog-description"
      maxWidth="sm"
      fullWidth
    >
      <DialogTitle id="delete-dialog-title">
        Confirmar Eliminación
      </DialogTitle>
      
      <DialogContent>
        <DialogContentText id="delete-dialog-description">
          ¿Estás seguro de que deseas eliminar al usuario <strong>{user?.nombre}</strong>?
          <br />
           <strong>id:</strong> {user?.id}
          <br />
          <strong>nombre:</strong> {user?.nombre}
          <br />
          <strong>RUT:</strong> {user?.rut}
          <br />
       
          Esta acción no se puede deshacer.
        </DialogContentText>
      </DialogContent>
      
      <DialogActions>
        <Button 
          onClick={onClose}
          disabled={deleting}
        >
          Cancelar
        </Button>
        <Button 
          onClick={onConfirm}
          color="error"
          variant="contained"
          disabled={deleting}
        >
          {deleting ? 'Eliminando...' : 'Eliminar'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ModalDelete;