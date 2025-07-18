import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    host: '0.0.0.0',
    port: 3000,
    watch: {
      usePolling: true, // Necesario para Docker en Windows
    }
  },
  build: {
    rollupOptions: {
      // Configuración específica para evitar errores de Rollup
      external: []
    }
  }
})