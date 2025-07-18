# Pragma - Sistema de Gestión de Usuarios

Sistema web desarrollado con React + TypeScript + Vite para la gestión de usuarios con operaciones CRUD completas.

## 🚀 Características

- **Frontend**: React 18 + TypeScript + Vite
- **UI Framework**: Material-UI (MUI) con DataGrid
- **Estado**: Hooks de React para manejo de estado
- **HTTP Client**: Axios para comunicación con API
- **Modales**: Componentes para Crear, Editar y Eliminar usuarios
- **Validación**: Validación de formularios en tiempo real
- **Responsive**: Diseño adaptable a diferentes dispositivos

## 🛠️ Tecnologías Utilizadas

- **React 18** - Biblioteca de interfaz de usuario
- **TypeScript** - Superset tipado de JavaScript
- **Vite** - Build tool y dev server ultrarrápido
- **Material-UI (MUI)** - Componentes de interfaz de usuario
- **Axios** - Cliente HTTP para llamadas a la API
- **ESLint** - Linter para mantener calidad del código

## 📋 Prerequisitos

- **Node.js** v18 o superior
- **npm** v8 o superior
- **Docker** (opcional, para contenedores)
- **API Backend** ejecutándose en `http://localhost:8080`

## 🚀 Instalación y Ejecución

###  Desarrollo con npm (Recomendado)

```bash
# 1. Clonar el repositorio
git clone <repository-url>
cd prueba-tecnica-pragma-web

# 2. Instalar dependencias
npm install

# 3. Iniciar servidor de desarrollo
npm run dev
```

La aplicación estará disponible en: `http://localhost:5173`

### Opción 2: Con Docker

```bash
# 1. Construir la imagen
docker build -t pragma-frontend .

# 2. Ejecutar el contenedor
docker run -p 3000:3000 pragma-frontend
```

La aplicación estará disponible en: `http://localhost:3000`

## 📝 Scripts Disponibles

```bash
# Desarrollo
npm run dev          # Inicia servidor de desarrollo en puerto 5173

# Construcción
npm run build        # Construye la aplicación para producción
npm run preview      # Previsualiza la build de producción

# Calidad de código
npm run lint         # Ejecuta ESLint para revisar el código
```

## 🏗️ Estructura del Proyecto

```
src/
├── components/          # Componentes reutilizables
│   ├── ModalCreate.tsx  # Modal para crear usuarios
│   ├── ModalEdit.tsx    # Modal para editar usuarios
│   └── ModalDelete.tsx  # Modal para eliminar usuarios
├── hooks/               # Custom hooks
│   └── useUsers.ts      # Hook para manejo de usuarios
├── interfaces/          # Definiciones de tipos TypeScript
│   ├── index.ts         # Exportaciones de interfaces
│   └── IUsers.ts        # Interface de usuario
├── pages/               # Páginas de la aplicación
│   └── home.tsx         # Página principal con DataGrid
├── services/            # Servicios para APIs
│   ├── axiosUsers.ts    # Servicio para operaciones de usuarios
│   ├── HttpService.ts   # Configuración base de HTTP
│   └── UserService.ts   # Servicio específico de usuarios
└── assets/              # Recursos estáticos
```
