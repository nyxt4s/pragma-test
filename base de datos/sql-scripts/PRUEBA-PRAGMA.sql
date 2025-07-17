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

-- Insertar datos de ejemplo
INSERT INTO Usuario (Nombre, Rut, Correo, FechaNacimiento)
VALUES 
('Roderick', '19985308-9', 'garaymedina@gmail.com', '1998-07-15'),
('Maria Torres', '20456789-0', 'maria.torres@example.com', '1995-03-22'),
('Juan Perez', '18543210-5', 'juan.perez@example.com', '1990-11-10'),
('Ana Gomez', '20123456-7', 'ana.gomez@example.com', '2000-05-05');

-- Verificar que los datos se insertaron correctamente
SELECT * FROM Usuario;
