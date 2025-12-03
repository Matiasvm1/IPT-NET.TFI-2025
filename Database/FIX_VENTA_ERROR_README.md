# ?? Solución al Error de Guardado de Ventas

## ?? Problema Identificado

El error **"An error occurred while saving the entity changes"** y **"Invalid column name 'Pago_Id'"** ocurrían al intentar confirmar una venta. Esto se debía a:

1. **Falta de foreign key `Venta_Id` en la tabla `Pagos`**
2. **Falta de foreign key `Pago_Id` en la tabla `Facturas`** ? **CRÍTICO**
3. **Tracking duplicado de entidades `Indumentaria` en Entity Framework Core**
4. **Falta de constructores sin parámetros en `Pago` y `Factura`**
5. **Columnas faltantes en la tabla `Stock`**: `CantidadMaxima` y `CantidadMinima`

## ? Soluciones Aplicadas

### 1. **Código C# Corregido**

#### ?? `Pago.cs` y `Factura.cs`
- ? Agregados constructores sin parámetros para Entity Framework Core

#### ?? `Repositorio.cs - GuardarVenta()`
- ? Manejo correcto del tracking de entidades relacionadas
- ? Desconexión de entidades `Indumentaria` ya rastreadas
- ? Marcado explícito de entidades como "sin cambios"
- ? Captura de excepciones con mensajes detallados

#### ?? `IvcNetContext.cs`
- ? Configuración correcta de la relación `Venta` ? `Pago` (1 a 1)
- ? Configuración correcta de la relación `Pago` ? `Factura` (1 a 1)
- ? Foreign key `Venta_Id` configurada con `DeleteBehavior.Cascade`
- ? Foreign key `Pago_Id` configurada con `DeleteBehavior.Cascade`
- ? Propiedades `CantidadMaxima` y `CantidadMinima` mapeadas en `Stock`

### 2. **Scripts SQL Creados**

#### ?? `MASTER_FIX_DATABASE.sql` ? **EJECUTAR PRIMERO**
Script maestro que aplica todos los fixes necesarios:
- ? Agrega `CantidadMaxima` y `CantidadMinima` a la tabla `Stock`
- ? Agrega columna `Venta_Id` a la tabla `Pagos`
- ? Crea foreign key `FK_dbo.Pagos_dbo.Ventas_Venta_Id`
- ? Agrega columna `Pago_Id` a la tabla `Facturas` ? **NUEVO**
- ? Crea foreign key `FK_dbo.Facturas_dbo.Pagos_Pago_Id` ? **NUEVO**
- ? Crea índices necesarios

#### ?? `fix_pagos_venta_fk.sql`
Script individual para corregir la tabla `Pagos`.

#### ?? `fix_facturas_pago_fk.sql` ? **NUEVO**
Script individual para corregir la tabla `Facturas`.

#### ?? `fix_stock_columns.sql`
Script individual para agregar columnas a `Stock`.

#### ?? `add_missing_stock.sql`
Script para agregar stock de productos faltantes (Campera Deportiva y Buzo con Capucha).

## ?? Pasos para Aplicar la Solución

### **Paso 1: Ejecutar el Script Maestro**

Abre SQL Server Management Studio (SSMS) o Azure Data Studio y ejecuta:

```sql
-- Ubicación: Database\MASTER_FIX_DATABASE.sql
USE IvcDb;
GO
-- (copiar y ejecutar el contenido completo del archivo)
```

? Esto actualizará las tablas `Stock`, `Pagos` y `Facturas` con las columnas y relaciones faltantes.

### **Paso 2: (Opcional) Agregar Stock Faltante**

Si necesitas agregar stock para productos que no tienen:

```sql
-- Ubicación: Database\add_missing_stock.sql
USE IvcDb;
GO
-- (copiar y ejecutar el contenido completo del archivo)
```

### **Paso 3: Verificar la Aplicación**

1. **Compilar la solución**: `Ctrl + Shift + B` en Visual Studio
2. **Ejecutar la aplicación**: `F5`
3. **Probar una venta**:
 - Login con credenciales: `Legajo: 1234`, `Contraseña: admin`
   - Ir a "Venta de Indumentaria"
   - Seleccionar un producto
   - Agregar al carrito
   - Ingresar importe pagado
   - Presionar "Concretar Venta"

? **Resultado esperado**: La venta debe guardarse correctamente sin errores.

## ?? Verificación de la Base de Datos

Puedes verificar que los cambios se aplicaron correctamente ejecutando:

```sql
USE IvcDb;
GO

-- Verificar columnas de Stock
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Stock'
ORDER BY ORDINAL_POSITION;

-- Verificar columnas de Pagos
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Pagos'
ORDER BY ORDINAL_POSITION;

-- Verificar columnas de Facturas ? NUEVO
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Facturas'
ORDER BY ORDINAL_POSITION;

-- Verificar foreign keys de Pagos
SELECT 
    fk.name AS ForeignKeyName,
    OBJECT_NAME(fk.parent_object_id) AS TableName,
  COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
    OBJECT_NAME(fk.referenced_object_id) AS ReferencedTable,
    COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferencedColumn
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fc ON fk.object_id = fc.constraint_object_id
WHERE OBJECT_NAME(fk.parent_object_id) = 'Pagos';

-- Verificar foreign keys de Facturas ? NUEVO
SELECT 
    fk.name AS ForeignKeyName,
    OBJECT_NAME(fk.parent_object_id) AS TableName,
    COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
    OBJECT_NAME(fk.referenced_object_id) AS ReferencedTable,
    COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferencedColumn
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fc ON fk.object_id = fc.constraint_object_id
WHERE OBJECT_NAME(fk.parent_object_id) = 'Facturas';
```

## ?? Estructura Esperada

### Tabla `Stock`
| Columna | Tipo | Requerido |
|---------|------|-----------|
| Id | int | ? |
| Cantidad | int | ? |
| **CantidadMaxima** | int | ? (DEFAULT: 100) |
| **CantidadMinima** | int | ? (DEFAULT: 10) |
| Indumentaria_Id | int | ? |
| Talle_Id | int | ? |

### Tabla `Pagos`
| Columna | Tipo | Requerido |
|---------|------|-----------|
| Id | int | ? |
| FechaHora | datetime | ? |
| Total | float | ? |
| **Venta_Id** | int | ? (FK a Ventas) |

### Tabla `Facturas` ? **ACTUALIZADA**
| Columna | Tipo | Requerido |
|---------|------|-----------|
| Id | int | ? |
| FechaHora | datetime | ? |
| Total | float | ? |
| **Pago_Id** | int | ? (FK a Pagos) ? **NUEVO** |

## ?? Flujo de Relaciones

```
Venta (1) ?? (1) Pago (1) ?? (1) Factura
  ?
LineaDeVenta (*) ? Indumentaria (1)
```

- **Venta** tiene un **Pago** (FK: `Venta_Id` en `Pagos`)
- **Pago** tiene una **Factura** (FK: `Pago_Id` en `Facturas`)
- **Venta** tiene múltiples **LineasDeVenta** (FK: `Venta_Id` en `LineasDeVenta`)
- **LineaDeVenta** referencia una **Indumentaria** (FK: `Indumentaria_Id` en `LineasDeVenta`)

## ?? Cambios en el Flujo de Ventas

### Antes ?
```
Usuario confirma venta
  ? GuardarVenta()
    ? EF Core intenta rastrear Indumentaria
      ? ERROR: "An error occurred while saving..."
    ? Intenta guardar Factura
      ? ERROR: "Invalid column name 'Pago_Id'"
```

### Después ?
```
Usuario confirma venta
  ? GuardarVenta()
    ? Desconectar entidades Indumentaria rastreadas
    ? Marcar Indumentaria como "sin cambios"
  ? Guardar Venta con LineasDeVenta y Pago
      ? Guardar Factura con Pago_Id
        ? ? Guardado exitoso
```

## ?? Referencias

- **Entity Framework Core 8**: [Docs Microsoft](https://learn.microsoft.com/es-es/ef/core/)
- **Change Tracking**: [Tracking vs. No-Tracking](https://learn.microsoft.com/es-es/ef/core/querying/tracking)
- **Relationships**: [One-to-Many & One-to-One](https://learn.microsoft.com/es-es/ef/core/modeling/relationships)

## ?? Troubleshooting

### Error: "Cannot insert duplicate key"
**Causa**: Entidad `Indumentaria` está siendo rastreada dos veces.  
**Solución**: Ya corregido en `Repositorio.GuardarVenta()` con `EntityState.Detached`.

### Error: "Column 'Venta_Id' is invalid"
**Causa**: No se ejecutó el script `MASTER_FIX_DATABASE.sql`.  
**Solución**: Ejecutar el script maestro en SQL Server.

### Error: "Column 'Pago_Id' is invalid" ? **NUEVO**
**Causa**: Columna `Pago_Id` no agregada a la tabla `Facturas`.  
**Solución**: Ejecutar el script maestro actualizado en SQL Server.

### Error: "Column 'CantidadMaxima' is invalid"
**Causa**: Columnas no agregadas a la tabla `Stock`.  
**Solución**: Ejecutar el script maestro en SQL Server.

## ? Checklist de Verificación

- [ ] Script `MASTER_FIX_DATABASE.sql` ejecutado exitosamente
- [ ] Tabla `Pagos` tiene columna `Venta_Id` con FK
- [ ] Tabla `Facturas` tiene columna `Pago_Id` con FK ? **NUEVO**
- [ ] Tabla `Stock` tiene `CantidadMaxima` y `CantidadMinima`
- [ ] Compilación sin errores en Visual Studio
- [ ] Login funciona correctamente
- [ ] Venta se puede concretar sin errores
- [ ] Venta aparece en la base de datos
- [ ] Pago se guarda correctamente con `Venta_Id`
- [ ] Factura se guarda correctamente con `Pago_Id` ? **NUEVO**
- [ ] Stock se reduce correctamente después de la venta

---

**?? ¡Problema resuelto!** Ahora puedes confirmar ventas sin errores de Entity Framework.
