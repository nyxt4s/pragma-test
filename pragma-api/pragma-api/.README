# PRAGMA API

API REST desarrollada en .NET 8 para la gestión de usuarios, siguiendo patrones de diseño Repository y Service, con arquitectura en capas.

## Características

- Backend en .NET 8 con Entity Framework Core
- Implementación de patrones Repository y Service
- API RESTful con operaciones CRUD completas
- Paginación de resultados con cursores
- Documentación con Swagger
- Validación de datos
- Manejo de excepciones
- Dockerización para fácil despliegue

## Requisitos previos

### Para ejecución local
- [.NET 8 SDK]
- [SQL Server] o contenedor Docker con SQL Server
- [Visual Studio 2022]

### Para ejecución con Docker
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Configuración de la base de datos

1. Ejecuta el siguiente script SQL para crear la tabla de usuarios:

-- Crear la base de datos solo si no existe
IF NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = 'pragmadb'
)
BEGIN
    CREATE DATABASE pragmadb;
END
GO

-- Usar la base de datos
USE pragmadb;
GO

-- Crear la tabla Usuario solo si no existe
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_NAME = 'Usuario' AND TABLE_SCHEMA = 'dbo'
)
BEGIN
    CREATE TABLE Usuario (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(50) NOT NULL,
        Rut NVARCHAR(12) NOT NULL UNIQUE,
        Correo NVARCHAR(100) NULL,
        FechaNacimiento DATE NOT NULL
    );
END
GO


-- Asegrate de estar usando la base de datos correcta
USE PPragmaDB;
GO

-- Insertar datos de ejemplo
INSERT INTO Usuario (Nombre, Rut, Correo, FechaNacimiento)
VALUES 
('Roderick', '19985308-9', 'garaymedina@gmail.com', '1998-07-15'),
('Maria Torres', '20456789-0', 'maria.torres@example.com', '1995-03-22'),
('Juan Perez', '18543210-5', 'juan.perez@example.com', '1990-11-10'),
('Ana Gomez', '20123456-7', 'ana.gomez@example.com', '2000-05-05');


SELECT * FROM Usuario;


2. **Configura la cadena de conexión**  
   Edita el archivo `appsettings.json` con tu cadena de conexión:

   "ConnectionStrings": { "SqlConnectionString": "Server=localhost,1433;Database=pragmadb;User Id=sa;Password=TuPassword;TrustServerCertificate=True;" }

3.1. en caso de levantar con contenedor docker ubicarse en la raiz del proyecto y ejecutar el siguiente comando:

3.2. **Construye la imagen Docker**

docker build -t pragma-api .

3.3. **Ejecuta el contenedor**

docker run -d -p 8080:8080 --name pragma-api pragma-api

4. **Accede a Swagger**  
   Abre en tu navegador: `http://localhost:8080/swagger`

## Estructura del proyecto

- **Controllers/**: Controladores de la API
- **Models/**: Entidades y contexto de la base de datos
- **DTOs/**: Objetos de transferencia de datos
- **Services/**: Lógica de negocio
- **Repositories/**: Acceso a datos
- **Helpers/**: Clases utilitarias

## Endpoints de la API

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/User/getAllUser` | Obtiene todos los usuarios |
| GET | `/api/User/getUser/{id}` | Obtiene un usuario por su ID |
| GET | `/api/User/paginacion-cursor` | Obtiene usuarios con paginación |
| POST | `/api/User/crear-usuario` | Crea un nuevo usuario |
| PUT | `/api/User/editar-usuario` | Actualiza un usuario existente |
| DELETE | `/api/User/eliminar-usuario/{id}` | Elimina un usuario |

## Solución de problemas comunes

### Conexión a la base de datos desde Docker
Si tienes problemas para conectar desde el contenedor Docker a la base de datos:
- Asegúrate de que SQL Server permite conexiones remotas
- Usa `host.docker.internal` en lugar de `localhost` en la cadena de conexión
- Verifica que el puerto 1433 esté abierto en tu firewall
