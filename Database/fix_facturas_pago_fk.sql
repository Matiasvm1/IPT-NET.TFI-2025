-- =============================================
-- Script para agregar la foreign key Pago_Id a la tabla Facturas
-- =============================================

USE IvcDb;
GO

PRINT 'Actualizando tabla Facturas...';

-- Verificar si la columna Pago_Id existe en Facturas
IF NOT EXISTS (SELECT * FROM sys.columns 
   WHERE object_id = OBJECT_ID(N'[dbo].[Facturas]') 
          AND name = 'Pago_Id')
BEGIN
    -- Agregar la columna Pago_Id
    ALTER TABLE [dbo].[Facturas]
    ADD [Pago_Id] INT NULL;
    
    PRINT '  ? Columna Pago_Id agregada a la tabla Facturas.';
END
ELSE
BEGIN
    PRINT '  - Pago_Id ya existe.';
END
GO

-- Verificar si la foreign key ya existe
IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
            WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Facturas_dbo.Pagos_Pago_Id]'))
BEGIN
    -- Crear la foreign key
    ALTER TABLE [dbo].[Facturas]
    ADD CONSTRAINT [FK_dbo.Facturas_dbo.Pagos_Pago_Id]
    FOREIGN KEY([Pago_Id]) REFERENCES [dbo].[Pagos] ([Id])
    ON DELETE CASCADE;
    
    PRINT '  ? Foreign key FK_dbo.Facturas_dbo.Pagos_Pago_Id creada.';
END
ELSE
BEGIN
    PRINT '  - Foreign key ya existe.';
END
GO

-- Verificar si el índice existe
IF NOT EXISTS (SELECT * FROM sys.indexes 
      WHERE name = 'IX_Pago_Id' 
  AND object_id = OBJECT_ID('dbo.Facturas'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Pago_Id]
    ON [dbo].[Facturas]([Pago_Id] ASC);
    
    PRINT '  ? Índice IX_Pago_Id en Facturas creado.';
END
ELSE
BEGIN
PRINT '  - Índice ya existe.';
END
GO

PRINT '=================================================';
PRINT '? Tabla Facturas actualizada exitosamente!';
PRINT '=================================================';
GO
