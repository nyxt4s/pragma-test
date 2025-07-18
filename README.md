# pragma-test

## 🏗️ Arquitectura del Sistema

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Frontend      │    │   API .NET      │    │   SQL Server    │
│   React + TS    │◄──►│   Core 8        │◄──►│   Database      │
│   Port: 5173    │    │   Port: 8080    │    │   Port: 1433    │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

## 📁 Estructura del Proyecto

```
pragma/
├── prueba-tecnica-pragma-web/     # 🌐 Frontend React + TypeScript
├── prueba-tecnica-pragma-api/     # ⚡ API .NET Core 8
├── base de datos/                 # 🗄️ Configuración Docker & SQL
└── README.md                     # 📖 Este archivo
```

## 🚀 Tecnologías Utilizadas

### Frontend
- **React 18** + **TypeScript**
- **Vite** para build y desarrollo
- **Material-UI (MUI)** para componentes
- **Axios** para llamadas HTTP
- **DataGrid** para tablas avanzadas

### Backend
- **.NET Core 8** WebAPI
- **Entity Framework Core**
- **SQL Server** como base de datos
- **Docker** para contenedores

### DevOps
- **Docker** y **Docker Compose**
- **GitHub** para control de versiones


levantar base de datos:

En la raíz del repositorio, ejecuta:

   ```bash
   docker-compose up -d
   ```

3. La base de datos SQL Server estará disponible en el puerto **1433**.  
   - Usuario: `sa`
   - Contraseña: `SuperSecret123!`


4. Conecta tu backend .NET usando la cadena de conexión:

   ```
   Server=localhost,1433;Database=master;User Id=sa;Password=SuperSecret123!;
   ```
5. ¡Listo para usar!


2./levantar api:

En caso de levantar con contenedor docker ubicarse en la raiz del proyecto y ejecutar el siguiente comando:

2.1- **Construye la imagen Docker**

docker build -t pragma-api .

2.2- **Ejecuta el contenedor**

docker run -d -p 8080:8080 --name pragma-api pragma-api

levantar front:

# 1. Clonar el repositorio
git clone <repository-url>
cd prueba-tecnica-pragma-web

# 2. Instalar dependencias
npm install

# 3. Iniciar servidor de desarrollo
npm run dev
