-- =============================================
-- Script para agregar la foreign key Venta_Id a la tabla Pagos
-- =============================================

USE IvcDb;
GO

-- Verificar si la columna Venta_Id existe en Pagos
IF NOT EXISTS (SELECT * FROM sys.columns 
          WHERE object_id = OBJECT_ID(N'[dbo].[Pagos]') 
      AND name = 'Venta_Id')
BEGIN
    -- Agregar la columna Venta_Id
 ALTER TABLE [dbo].[Pagos]
    ADD [Venta_Id] INT NULL;
    
    PRINT 'Columna Venta_Id agregada a la tabla Pagos.';
END
ELSE
BEGIN
    PRINT 'La columna Venta_Id ya existe en la tabla Pagos.';
END
GO

-- Verificar si la foreign key ya existe
IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
            WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Pagos_dbo.Ventas_Venta_Id]'))
BEGIN
    -- Crear la foreign key
    ALTER TABLE [dbo].[Pagos]
    ADD CONSTRAINT [FK_dbo.Pagos_dbo.Ventas_Venta_Id]
    FOREIGN KEY([Venta_Id]) REFERENCES [dbo].[Ventas] ([Id])
    ON DELETE CASCADE;
    
    PRINT 'Foreign key FK_dbo.Pagos_dbo.Ventas_Venta_Id creada.';
END
ELSE
BEGIN
    PRINT 'La foreign key FK_dbo.Pagos_dbo.Ventas_Venta_Id ya existe.';
END
GO

-- Verificar si el índice existe
IF NOT EXISTS (SELECT * FROM sys.indexes 
       WHERE name = 'IX_Venta_Id' 
           AND object_id = OBJECT_ID('dbo.Pagos'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Venta_Id]
    ON [dbo].[Pagos]([Venta_Id] ASC);
    
    PRINT 'Índice IX_Venta_Id en Pagos creado.';
END
ELSE
BEGIN
    PRINT 'El índice IX_Venta_Id ya existe en Pagos.';
END
GO

PRINT '=================================================';
PRINT 'Script completado: Venta_Id agregada a Pagos';
PRINT '=================================================';
GO
