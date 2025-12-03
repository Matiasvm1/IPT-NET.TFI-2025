-- =============================================
-- SCRIPT MAESTRO: Aplicar todos los fixes a la base de datos IvcDb
-- Ejecutar este script para actualizar la base de datos
-- =============================================

USE IvcDb;
GO

PRINT '=================================================';
PRINT 'Iniciando actualización de la base de datos IvcDb';
PRINT '=================================================';
GO

-- =============================================
-- 1. AGREGAR COLUMNAS A STOCK
-- =============================================

PRINT 'Paso 1: Actualizando tabla Stock...';

-- Agregar CantidadMaxima
IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE object_id = OBJECT_ID(N'[dbo].[Stock]') 
        AND name = 'CantidadMaxima')
BEGIN
    ALTER TABLE [dbo].[Stock]
    ADD [CantidadMaxima] INT NOT NULL DEFAULT 100;
    PRINT '  ? Columna CantidadMaxima agregada.';
END
ELSE
BEGIN
    PRINT '  - CantidadMaxima ya existe.';
END
GO

-- Agregar CantidadMinima
IF NOT EXISTS (SELECT * FROM sys.columns 
   WHERE object_id = OBJECT_ID(N'[dbo].[Stock]') 
      AND name = 'CantidadMinima')
BEGIN
    ALTER TABLE [dbo].[Stock]
  ADD [CantidadMinima] INT NOT NULL DEFAULT 10;
    PRINT '  ? Columna CantidadMinima agregada.';
END
ELSE
BEGIN
    PRINT '  - CantidadMinima ya existe.';
END
GO

-- =============================================
-- 2. AGREGAR VENTA_ID A PAGOS
-- =============================================

PRINT 'Paso 2: Actualizando tabla Pagos...';

-- Agregar columna Venta_Id
IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE object_id = OBJECT_ID(N'[dbo].[Pagos]') 
    AND name = 'Venta_Id')
BEGIN
    ALTER TABLE [dbo].[Pagos]
    ADD [Venta_Id] INT NULL;
    PRINT '  ? Columna Venta_Id agregada.';
END
ELSE
BEGIN
    PRINT '  - Venta_Id ya existe.';
END
GO

-- Crear foreign key
IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
       WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Pagos_dbo.Ventas_Venta_Id]'))
BEGIN
    ALTER TABLE [dbo].[Pagos]
    ADD CONSTRAINT [FK_dbo.Pagos_dbo.Ventas_Venta_Id]
    FOREIGN KEY([Venta_Id]) REFERENCES [dbo].[Ventas] ([Id])
    ON DELETE CASCADE;
    PRINT '  ? Foreign key creada.';
END
ELSE
BEGIN
    PRINT '  - Foreign key ya existe.';
END
GO

-- Crear índice
IF NOT EXISTS (SELECT * FROM sys.indexes 
       WHERE name = 'IX_Venta_Id' 
 AND object_id = OBJECT_ID('dbo.Pagos'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Venta_Id]
    ON [dbo].[Pagos]([Venta_Id] ASC);
  PRINT '  ? Índice IX_Venta_Id creado.';
END
ELSE
BEGIN
    PRINT '  - Índice ya existe.';
END
GO

-- =============================================
-- 3. AGREGAR PAGO_ID A FACTURAS
-- =============================================

PRINT 'Paso 3: Actualizando tabla Facturas...';

-- Agregar columna Pago_Id
IF NOT EXISTS (SELECT * FROM sys.columns 
   WHERE object_id = OBJECT_ID(N'[dbo].[Facturas]') 
  AND name = 'Pago_Id')
BEGIN
    ALTER TABLE [dbo].[Facturas]
    ADD [Pago_Id] INT NULL;
    PRINT '  ? Columna Pago_Id agregada.';
END
ELSE
BEGIN
    PRINT '  - Pago_Id ya existe.';
END
GO

-- Crear foreign key
IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
            WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Facturas_dbo.Pagos_Pago_Id]'))
BEGIN
    ALTER TABLE [dbo].[Facturas]
 ADD CONSTRAINT [FK_dbo.Facturas_dbo.Pagos_Pago_Id]
    FOREIGN KEY([Pago_Id]) REFERENCES [dbo].[Pagos] ([Id])
    ON DELETE CASCADE;
    PRINT '  ? Foreign key creada.';
END
ELSE
BEGIN
PRINT '  - Foreign key ya existe.';
END
GO

-- Crear índice
IF NOT EXISTS (SELECT * FROM sys.indexes 
      WHERE name = 'IX_Pago_Id' 
  AND object_id = OBJECT_ID('dbo.Facturas'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Pago_Id]
    ON [dbo].[Facturas]([Pago_Id] ASC);
    PRINT '  ? Índice IX_Pago_Id creado.';
END
ELSE
BEGIN
    PRINT '  - Índice ya existe.';
END
GO

PRINT '=================================================';
PRINT '? Actualización completada exitosamente!';
PRINT '=================================================';
PRINT '';
PRINT 'Resumen de cambios:';
PRINT '  • Tabla Stock: CantidadMaxima, CantidadMinima';
PRINT '  • Tabla Pagos: Venta_Id (FK a Ventas)';
PRINT '• Tabla Facturas: Pago_Id (FK a Pagos)';
PRINT '';
PRINT 'Puedes ejecutar ahora:';
PRINT '  • add_missing_stock.sql (para agregar stock faltante)';
PRINT '=================================================';
GO
