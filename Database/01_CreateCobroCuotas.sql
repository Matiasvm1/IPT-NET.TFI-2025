-- ===============================================
-- Script de Base de Datos para Módulo de Cobro de Cuotas
-- IPT-NET - Sistema de Gestión
-- ===============================================

USE IvcDb;
GO

-- ===============================================
-- 1. CREAR TABLAS
-- ===============================================

-- Tabla Alumnos
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Alumnos')
BEGIN
    CREATE TABLE [dbo].[Alumnos] (
      [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [DNI] INT NOT NULL UNIQUE,
        [Nombre] NVARCHAR(100) NOT NULL,
        [Apellido] NVARCHAR(100) NOT NULL,
        [Email] NVARCHAR(150) NULL,
  [Telefono] NVARCHAR(20) NULL,
        CONSTRAINT [UK_Alumnos_DNI] UNIQUE ([DNI])
    );
    PRINT 'Tabla Alumnos creada correctamente';
END
ELSE
    PRINT 'Tabla Alumnos ya existe';
GO

-- Tabla Cuotas
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cuotas')
BEGIN
    CREATE TABLE [dbo].[Cuotas] (
   [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [CodigoBarras] NVARCHAR(50) NOT NULL UNIQUE,
        [Mes] INT NOT NULL,
        [Anio] INT NOT NULL,
        [MontoOriginal] FLOAT NOT NULL,
        [PrimerVencimiento] DATETIME NOT NULL,
        [SegundoVencimiento] DATETIME NOT NULL,
        [TercerVencimiento] DATETIME NOT NULL,
        [Estado] INT NOT NULL DEFAULT 1, -- 1=Pendiente, 2=Vencida, 3=Pagada
  [AlumnoId] INT NOT NULL,
        [PagoCuotaId] INT NULL,
        CONSTRAINT [FK_Cuotas_Alumnos] FOREIGN KEY ([AlumnoId]) 
            REFERENCES [Alumnos]([Id]),
      CONSTRAINT [UK_Cuotas_CodigoBarras] UNIQUE ([CodigoBarras])
    );
    PRINT 'Tabla Cuotas creada correctamente';
END
ELSE
    PRINT 'Tabla Cuotas ya existe';
GO

-- Tabla PagosCuotas
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PagosCuotas')
BEGIN
    CREATE TABLE [dbo].[PagosCuotas] (
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [FechaPago] DATETIME NOT NULL,
     [MontoAbonado] FLOAT NOT NULL,
        [Recargo] FLOAT NOT NULL DEFAULT 0,
        [MedioPago] NVARCHAR(50) NOT NULL DEFAULT 'Efectivo',
        [Observaciones] NVARCHAR(500) NULL,
        [CuotaId] INT NOT NULL,
    CONSTRAINT [FK_PagosCuotas_Cuotas] FOREIGN KEY ([CuotaId]) 
      REFERENCES [Cuotas]([Id])
    );
    PRINT 'Tabla PagosCuotas creada correctamente';
END
ELSE
    PRINT 'Tabla PagosCuotas ya existe';
GO

-- Agregar FK de Cuotas a PagosCuotas (relación 1:1 opcional)
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Cuotas_PagosCuotas')
BEGIN
  ALTER TABLE [dbo].[Cuotas]
    ADD CONSTRAINT [FK_Cuotas_PagosCuotas] FOREIGN KEY ([PagoCuotaId])
     REFERENCES [PagosCuotas]([Id]);
    PRINT 'Foreign Key FK_Cuotas_PagosCuotas agregada';
END
GO

-- ===============================================
-- 2. CREAR ÍNDICES PARA OPTIMIZACIÓN
-- ===============================================

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Cuotas_AlumnoId')
BEGIN
    CREATE INDEX [IX_Cuotas_AlumnoId] ON [Cuotas]([AlumnoId]);
    PRINT 'Índice IX_Cuotas_AlumnoId creado';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Cuotas_Estado')
BEGIN
    CREATE INDEX [IX_Cuotas_Estado] ON [Cuotas]([Estado]);
    PRINT 'Índice IX_Cuotas_Estado creado';
END
GO

-- ===============================================
-- 3. DATOS DE PRUEBA (SEED DATA)
-- ===============================================

-- Insertar alumnos de prueba si no existen
IF NOT EXISTS (SELECT * FROM Alumnos WHERE DNI = 40123456)
BEGIN
    INSERT INTO Alumnos (DNI, Nombre, Apellido, Email, Telefono) VALUES
    (40123456, 'Juan', 'Pérez', 'juan.perez@email.com', '351-1234567'),
    (40234567, 'María', 'González', 'maria.gonzalez@email.com', '351-2345678'),
    (40345678, 'Carlos', 'Rodríguez', 'carlos.rodriguez@email.com', '351-3456789'),
    (40456789, 'Ana', 'Martínez', 'ana.martinez@email.com', '351-4567890'),
    (40567890, 'Lucas', 'Fernández', 'lucas.fernandez@email.com', '351-5678901');
    
    PRINT 'Alumnos de prueba insertados';
END
GO

-- Insertar cuotas de prueba para el alumno Juan Pérez (DNI: 40123456)
DECLARE @AlumnoId INT;
SELECT @AlumnoId = Id FROM Alumnos WHERE DNI = 40123456;

IF @AlumnoId IS NOT NULL AND NOT EXISTS (SELECT * FROM Cuotas WHERE AlumnoId = @AlumnoId)
BEGIN
    -- Cuota de Enero 2025 (vencida)
    INSERT INTO Cuotas (CodigoBarras, Mes, Anio, MontoOriginal, PrimerVencimiento, SegundoVencimiento, TercerVencimiento, Estado, AlumnoId)
    VALUES ('CB202501001', 1, 2025, 5000.00, '2025-01-10', '2025-01-20', '2025-01-31', 2, @AlumnoId);
    
    -- Cuota de Febrero 2025 (pendiente)
    INSERT INTO Cuotas (CodigoBarras, Mes, Anio, MontoOriginal, PrimerVencimiento, SegundoVencimiento, TercerVencimiento, Estado, AlumnoId)
    VALUES ('CB202502001', 2, 2025, 5000.00, '2025-02-10', '2025-02-20', '2025-02-28', 1, @AlumnoId);
    
    -- Cuota de Marzo 2025 (pendiente)
    INSERT INTO Cuotas (CodigoBarras, Mes, Anio, MontoOriginal, PrimerVencimiento, SegundoVencimiento, TercerVencimiento, Estado, AlumnoId)
    VALUES ('CB202503001', 3, 2025, 5000.00, '2025-03-10', '2025-03-20', '2025-03-31', 1, @AlumnoId);
    
    PRINT 'Cuotas de prueba insertadas para Juan Pérez';
END
GO

-- Insertar cuotas para María González (DNI: 40234567)
DECLARE @AlumnoId2 INT;
SELECT @AlumnoId2 = Id FROM Alumnos WHERE DNI = 40234567;

IF @AlumnoId2 IS NOT NULL AND NOT EXISTS (SELECT * FROM Cuotas WHERE AlumnoId = @AlumnoId2)
BEGIN
    INSERT INTO Cuotas (CodigoBarras, Mes, Anio, MontoOriginal, PrimerVencimiento, SegundoVencimiento, TercerVencimiento, Estado, AlumnoId)
    VALUES 
    ('CB202501002', 1, 2025, 6000.00, '2025-01-10', '2025-01-20', '2025-01-31', 1, @AlumnoId2),
    ('CB202502002', 2, 2025, 6000.00, '2025-02-10', '2025-02-20', '2025-02-28', 1, @AlumnoId2);
    
PRINT 'Cuotas de prueba insertadas para María González';
END
GO

-- ===============================================
-- 4. VERIFICACIÓN DE DATOS
-- ===============================================

SELECT 'ALUMNOS' AS Tabla, COUNT(*) AS Total FROM Alumnos
UNION ALL
SELECT 'CUOTAS', COUNT(*) FROM Cuotas
UNION ALL
SELECT 'PAGOS_CUOTAS', COUNT(*) FROM PagosCuotas;

PRINT '';
PRINT '===============================================';
PRINT 'Script ejecutado correctamente';
PRINT 'Base de datos lista para el módulo de Cobro de Cuotas';
PRINT '===============================================';
GO
