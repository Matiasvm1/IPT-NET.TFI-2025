-- =============================================
-- Script de Creación de Base de Datos: IvcDb
-- Sistema de Ventas de Indumentaria IVC.NET
-- Generado para .NET 8.0 con Entity Framework 6.5.1
-- =============================================

USE master;
GO

-- Crear base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'IvcDb')
BEGIN
    CREATE DATABASE IvcDb;
    PRINT 'Base de datos IvcDb creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos IvcDb ya existe.';
END
GO

USE IvcDb;
GO

-- =============================================
-- TABLA: Empleados
-- Descripción: Almacena información de empleados del sistema
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Empleados]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Empleados] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Legajo] INT NOT NULL,
        [Contraseña] NVARCHAR(MAX) NULL,
  CONSTRAINT [PK_dbo.Empleados] PRIMARY KEY CLUSTERED ([Id] ASC)
  );
    PRINT 'Tabla Empleados creada.';
END
GO

-- =============================================
-- TABLA: Talles
-- Descripción: Catálogo de talles disponibles (XS, S, M, L, XL, etc.)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Talles]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Talles] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Descripcion] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_dbo.Talles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
    PRINT 'Tabla Talles creada.';
END
GO

-- =============================================
-- TABLA: Indumentarias
-- Descripción: Catálogo de productos de indumentaria
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Indumentarias]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Indumentarias] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Codigo] INT NOT NULL,
        [Descripcion] NVARCHAR(MAX) NULL,
        [Precio] FLOAT NOT NULL,
   CONSTRAINT [PK_dbo.Indumentarias] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Tabla Indumentarias creada.';
END
GO

-- =============================================
-- TABLA: Stock
-- Descripción: Control de inventario por producto y talle
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stock]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Stock] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Cantidad] INT NOT NULL DEFAULT 0,
        [Indumentaria_Id] INT NULL,
    [Talle_Id] INT NULL,
        CONSTRAINT [PK_dbo.Stock] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Tabla Stock creada.';
END
GO

-- =============================================
-- TABLA: Ventas
-- Descripción: Cabecera de las ventas realizadas
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ventas]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Ventas] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [FechaHora] DATETIME NOT NULL,
        CONSTRAINT [PK_dbo.Ventas] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Tabla Ventas creada.';
END
GO

-- =============================================
-- TABLA: LineasDeVenta
-- Descripción: Detalle de cada venta (productos vendidos)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LineasDeVenta]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[LineasDeVenta] (
 [Id] INT IDENTITY(1,1) NOT NULL,
        [Cantidad] INT NOT NULL,
        [Indumentaria_Id] INT NULL,
        [Venta_Id] INT NULL,
      CONSTRAINT [PK_dbo.LineasDeVenta] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Tabla LineasDeVenta creada.';
END
GO

-- =============================================
-- TABLA: Pagos
-- Descripción: Registro de pagos realizados
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pagos]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Pagos] (
[Id] INT IDENTITY(1,1) NOT NULL,
        [FechaHora] DATETIME NOT NULL,
        [Total] FLOAT NOT NULL,
        CONSTRAINT [PK_dbo.Pagos] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Tabla Pagos creada.';
END
GO

-- =============================================
-- TABLA: Facturas
-- Descripción: Información de facturación
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Facturas]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Facturas] (
     [Id] INT IDENTITY(1,1) NOT NULL,
     [FechaHora] DATETIME NOT NULL,
        [Total] FLOAT NOT NULL,
        CONSTRAINT [PK_dbo.Facturas] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Tabla Facturas creada.';
END
GO

-- =============================================
-- FOREIGN KEYS (Relaciones entre tablas)
-- =============================================

-- FK: Stock -> Indumentarias
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Stock_dbo.Indumentarias_Indumentaria_Id]'))
BEGIN
  ALTER TABLE [dbo].[Stock]
    ADD CONSTRAINT [FK_dbo.Stock_dbo.Indumentarias_Indumentaria_Id]
    FOREIGN KEY([Indumentaria_Id]) REFERENCES [dbo].[Indumentarias] ([Id]);
 PRINT 'FK Stock -> Indumentarias creada.';
END
GO

-- FK: Stock -> Talles
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Stock_dbo.Talles_Talle_Id]'))
BEGIN
    ALTER TABLE [dbo].[Stock]
    ADD CONSTRAINT [FK_dbo.Stock_dbo.Talles_Talle_Id]
    FOREIGN KEY([Talle_Id]) REFERENCES [dbo].[Talles] ([Id]);
    PRINT 'FK Stock -> Talles creada.';
END
GO

-- FK: LineasDeVenta -> Indumentarias
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.LineasDeVenta_dbo.Indumentarias_Indumentaria_Id]'))
BEGIN
    ALTER TABLE [dbo].[LineasDeVenta]
    ADD CONSTRAINT [FK_dbo.LineasDeVenta_dbo.Indumentarias_Indumentaria_Id]
    FOREIGN KEY([Indumentaria_Id]) REFERENCES [dbo].[Indumentarias] ([Id]);
    PRINT 'FK LineasDeVenta -> Indumentarias creada.';
END
GO

-- FK: LineasDeVenta -> Ventas
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.LineasDeVenta_dbo.Ventas_Venta_Id]'))
BEGIN
    ALTER TABLE [dbo].[LineasDeVenta]
    ADD CONSTRAINT [FK_dbo.LineasDeVenta_dbo.Ventas_Venta_Id]
    FOREIGN KEY([Venta_Id]) REFERENCES [dbo].[Ventas] ([Id]);
    PRINT 'FK LineasDeVenta -> Ventas creada.';
END
GO

-- =============================================
-- ÍNDICES (Mejoran el rendimiento de las consultas)
-- =============================================

-- Índice en Stock.Indumentaria_Id
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Indumentaria_Id' AND object_id = OBJECT_ID('dbo.Stock'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Indumentaria_Id]
    ON [dbo].[Stock]([Indumentaria_Id] ASC);
    PRINT 'Índice IX_Indumentaria_Id en Stock creado.';
END
GO

-- Índice en Stock.Talle_Id
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Talle_Id' AND object_id = OBJECT_ID('dbo.Stock'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Talle_Id]
    ON [dbo].[Stock]([Talle_Id] ASC);
    PRINT 'Índice IX_Talle_Id en Stock creado.';
END
GO

-- Índice en LineasDeVenta.Indumentaria_Id
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Indumentaria_Id' AND object_id = OBJECT_ID('dbo.LineasDeVenta'))
BEGIN
  CREATE NONCLUSTERED INDEX [IX_Indumentaria_Id]
    ON [dbo].[LineasDeVenta]([Indumentaria_Id] ASC);
    PRINT 'Índice IX_Indumentaria_Id en LineasDeVenta creado.';
END
GO

-- Índice en LineasDeVenta.Venta_Id
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Venta_Id' AND object_id = OBJECT_ID('dbo.LineasDeVenta'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Venta_Id]
    ON [dbo].[LineasDeVenta]([Venta_Id] ASC);
    PRINT 'Índice IX_Venta_Id en LineasDeVenta creado.';
END
GO

-- =============================================
-- TABLA: __MigrationHistory
-- Descripción: Tabla de control de Entity Framework para rastrear migraciones
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[__MigrationHistory] (
   [MigrationId] NVARCHAR(150) NOT NULL,
    [ContextKey] NVARCHAR(300) NOT NULL,
        [Model] VARBINARY(MAX) NOT NULL,
        [ProductVersion] NVARCHAR(32) NOT NULL,
        CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
        (
            [MigrationId] ASC,
   [ContextKey] ASC
        )
    );
    PRINT 'Tabla __MigrationHistory creada.';
END
GO

-- =============================================
-- DATOS INICIALES DE PRUEBA (OPCIONAL)
-- =============================================

-- Insertar un empleado de prueba para login
-- Usuario: admin, Legajo: 1234, Contraseña: admin
IF NOT EXISTS (SELECT * FROM Empleados WHERE Legajo = 1234)
BEGIN
    INSERT INTO Empleados (Legajo, Contraseña)
    VALUES (1234, 'admin');
    PRINT 'Empleado de prueba insertado (Legajo: 1234, Contraseña: admin)';
END
GO

-- Insertar talles estándar
IF NOT EXISTS (SELECT * FROM Talles)
BEGIN
    INSERT INTO Talles (Descripcion) VALUES ('XS');
    INSERT INTO Talles (Descripcion) VALUES ('S');
    INSERT INTO Talles (Descripcion) VALUES ('M');
    INSERT INTO Talles (Descripcion) VALUES ('L');
    INSERT INTO Talles (Descripcion) VALUES ('XL');
    INSERT INTO Talles (Descripcion) VALUES ('XXL');
    PRINT 'Talles estándar insertados.';
END
GO

-- Insertar productos de ejemplo
IF NOT EXISTS (SELECT * FROM Indumentarias)
BEGIN
    INSERT INTO Indumentarias (Codigo, Descripcion, Precio) VALUES (1001, 'Remera Básica', 5000.00);
    INSERT INTO Indumentarias (Codigo, Descripcion, Precio) VALUES (1002, 'Pantalón Jean', 12000.00);
    INSERT INTO Indumentarias (Codigo, Descripcion, Precio) VALUES (1003, 'Campera Deportiva', 18000.00);
    INSERT INTO Indumentarias (Codigo, Descripcion, Precio) VALUES (1004, 'Buzo con Capucha', 9500.00);
    PRINT 'Productos de ejemplo insertados.';
END
GO

-- Insertar stock inicial
IF NOT EXISTS (SELECT * FROM Stock)
BEGIN
    -- Stock para Remera Básica
    INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id) VALUES (20, 1, 1); -- XS
    INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id) VALUES (30, 1, 2); -- S
    INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id) VALUES (40, 1, 3); -- M
    INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id) VALUES (35, 1, 4); -- L
    INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id) VALUES (25, 1, 5); -- XL
    
    -- Stock para Pantalón Jean
    INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id) VALUES (15, 2, 2); -- S
INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id) VALUES (25, 2, 3); -- M
    INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id) VALUES (20, 2, 4); -- L
    INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id) VALUES (10, 2, 5); -- XL
    
    PRINT 'Stock inicial insertado.';
END
GO

PRINT '=================================================';
PRINT 'Base de datos IvcDb creada y configurada exitosamente!';
PRINT '=================================================';
PRINT 'Credenciales de prueba:';
PRINT 'Legajo: 1234';
PRINT '  Contraseña: admin';
PRINT '=================================================';
GO
