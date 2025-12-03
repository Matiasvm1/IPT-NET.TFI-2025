-- =============================================
-- Script para agregar columnas CantidadMaxima y CantidadMinima a Stock
-- =============================================

USE IvcDb;
GO

-- Agregar columna CantidadMaxima si no existe
IF NOT EXISTS (SELECT * FROM sys.columns 
            WHERE object_id = OBJECT_ID(N'[dbo].[Stock]') 
      AND name = 'CantidadMaxima')
BEGIN
    ALTER TABLE [dbo].[Stock]
ADD [CantidadMaxima] INT NULL DEFAULT 100;
    
    PRINT 'Columna CantidadMaxima agregada a la tabla Stock.';
    
    -- Actualizar registros existentes
    UPDATE [dbo].[Stock]
    SET [CantidadMaxima] = 100
    WHERE [CantidadMaxima] IS NULL;
    
    PRINT 'Valores por defecto establecidos para CantidadMaxima.';
END
ELSE
BEGIN
    PRINT 'La columna CantidadMaxima ya existe en Stock.';
END
GO

-- Agregar columna CantidadMinima si no existe
IF NOT EXISTS (SELECT * FROM sys.columns 
        WHERE object_id = OBJECT_ID(N'[dbo].[Stock]') 
         AND name = 'CantidadMinima')
BEGIN
    ALTER TABLE [dbo].[Stock]
    ADD [CantidadMinima] INT NULL DEFAULT 10;
    
    PRINT 'Columna CantidadMinima agregada a la tabla Stock.';
    
    -- Actualizar registros existentes
    UPDATE [dbo].[Stock]
    SET [CantidadMinima] = 10
    WHERE [CantidadMinima] IS NULL;
    
    PRINT 'Valores por defecto establecidos para CantidadMinima.';
END
ELSE
BEGIN
    PRINT 'La columna CantidadMinima ya existe en Stock.';
END
GO

PRINT '=================================================';
PRINT 'Script completado: Columnas agregadas a Stock';
PRINT '=================================================';
GO
