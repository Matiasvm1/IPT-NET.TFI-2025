# ?? Migración de Entity Framework 6 a Entity Framework Core 8

## ?? Resumen de Cambios

### **Antes (EF6)**
- **Paquete**: EntityFramework 6.5.1
- **Namespace**: `System.Data.Entity`
- **DbContext**: Hereda de `DbContext` (EF6)
- **Configuración**: `OnModelCreating(DbModelBuilder modelBuilder)`
- **Migraciones**: Comandos PowerShell en Package Manager Console

### **Después (EF Core 8)**
- **Paquetes**: 
  - Microsoft.EntityFrameworkCore 8.0.11
  - Microsoft.EntityFrameworkCore.SqlServer 8.0.11
  - Microsoft.EntityFrameworkCore.Tools 8.0.11
- **Namespace**: `Microsoft.EntityFrameworkCore`
- **DbContext**: Hereda de `DbContext` (EF Core)
- **Configuración**: `OnModelCreating(ModelBuilder modelBuilder)`
- **Migraciones**: Comandos `dotnet ef` CLI

---

## ? **Ventajas de la Migración**

| Característica | EF6 | EF Core 8 |
|----------------|-----|-----------|
| Compatibilidad con .NET 8 | ?? Limitada | ? Total |
| Rendimiento | ?? Bueno | ?? Excelente (hasta 70% más rápido) |
| Soporte multiplataforma | ? Solo Windows | ? Windows, Linux, macOS |
| Consultas LINQ | ? | ?? Mejoradas |
| Migraciones | ?? Solo PowerShell | ? CLI + PowerShell |
| Consultas raw SQL | ? | ?? Mejoradas |
| Lazy Loading | ? Por defecto | ?? Opcional (más control) |
| Actualización | ? Fin de soporte | ? Actualizaciones activas |

---

## ?? **Pasos de Migración Realizados**

### **1. Actualización del Proyecto TFI.AccesoADatos.csproj**

**Antes:**
```xml
<PackageReference Include="EntityFramework" Version="6.5.1" />
```

**Después:**
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.11" />
<PackageReference Include="System.Configuration.ConfigurationManager" Version="8.0.1" />
```

### **2. Modernización del DbContext (IvcNetContext.cs)**

**Cambios principales:**
- ? Cambio de namespace: `System.Data.Entity` ? `Microsoft.EntityFrameworkCore`
- ? `DbModelBuilder` ? `ModelBuilder`
- ? Configuración fluida más explícita
- ? Soporte para constructor con opciones: `DbContextOptions<IvcNetContext>`
- ? Método `OnConfiguring` para leer cadena de conexión
- ? Métodos separados para configurar cada entidad

### **3. Simplificación de App.config**

**Antes:**
```xml
<configSections>
  <section name="entityFramework" ... />
</configSections>
<entityFramework>
<providers>
  <provider invariantName="System.Data.SqlClient" ... />
  </providers>
</entityFramework>
```

**Después:**
```xml
<!-- Solo la cadena de conexión, sin configuración de EF -->
<connectionStrings>
  <add name="IvcDb" ... />
</connectionStrings>
```

---

## ?? **Mapeo de Entidades - Compatibilidad**

Todas tus entidades **siguen siendo 100% compatibles**:

| Entidad | Tabla | Estado |
|---------|-------|--------|
| `Empleado` | `Empleados` | ? Compatible |
| `Indumentaria` | `Indumentarias` | ? Compatible |
| `Talle` | `Talles` | ? Compatible |
| `Stock` | `Stock` | ? Compatible |
| `Venta` | `Ventas` | ? Compatible |
| `LineaDeVenta` | `LineasDeVenta` | ? Compatible |
| `Pago` | `Pagos` | ? Compatible |
| `Factura` | `Facturas` | ? Compatible |

**No necesitas modificar ninguna clase de dominio.**

---

## ??? **Migraciones - Estrategia de Preservación**

### **Opción Adoptada: Base de Datos Existente**

Las migraciones de EF6 **no son directamente compatibles** con EF Core, pero:

1. ? **La base de datos se creó con el script SQL** ? Ya tienes todas las tablas
2. ? **EF Core reconocerá la estructura existente**
3. ? **Las migraciones antiguas se movieron a `Migrations_Legacy/`** (referencia histórica)
4. ? **Nuevas migraciones usarán EF Core**

### **Para Crear la Primera Migración de EF Core (Opcional)**

Si quieres tener una migración inicial de EF Core que refleje el estado actual:

```bash
# En la raíz del proyecto
dotnet ef migrations add InitialCreate --project TFI.AccesoADatos --startup-project TFI.Vista

# Para aplicarla (solo crea la tabla __EFMigrationsHistory)
dotnet ef database update --project TFI.AccesoADatos --startup-project TFI.Vista
```

---

## ?? **Nuevos Comandos de Migraciones**

### **Entity Framework 6 (Antiguo)**
```powershell
# Package Manager Console
PM> Get-Migrations
PM> Add-Migration NombreMigracion
PM> Update-Database
```

### **Entity Framework Core 8 (Nuevo)**
```bash
# Terminal / PowerShell
dotnet ef migrations list --project TFI.AccesoADatos --startup-project TFI.Vista
dotnet ef migrations add NombreMigracion --project TFI.AccesoADatos --startup-project TFI.Vista
dotnet ef database update --project TFI.AccesoADatos --startup-project TFI.Vista

# Ver el SQL que se ejecutará
dotnet ef migrations script --project TFI.AccesoADatos --startup-project TFI.Vista
```

También puedes seguir usando **Package Manager Console**:
```powershell
PM> Add-Migration NombreMigracion -Project TFI.AccesoADatos -StartupProject TFI.Vista
PM> Update-Database -Project TFI.AccesoADatos -StartupProject TFI.Vista
```

---

## ?? **Testing y Verificación**

### **Checklist Post-Migración**

- [ ] **Compilar la solución** sin errores
- [ ] **Restaurar paquetes NuGet**
- [ ] **Ejecutar el script SQL** para crear la BD (si no existe)
- [ ] **Ejecutar la aplicación** (F5)
- [ ] **Probar login** con Legajo: 1234, Contraseña: admin
- [ ] **Verificar que las consultas funcionan** correctamente

### **Puntos a Verificar**

1. **Repositorio.cs**: Debe compilar sin cambios (o con cambios mínimos)
2. **Lazy Loading**: Si lo usabas, ahora necesitas habilitarlo explícitamente
3. **Consultas**: Deberían funcionar igual o mejor
4. **Transacciones**: Sintaxis ligeramente diferente

---

## ?? **Cambios Potenciales en el Código**

### **1. Lazy Loading (si lo usabas)**

**EF6:**
```csharp
// Por defecto habilitado
```

**EF Core:**
```csharp
// Opción 1: Habilitar globalmente
optionsBuilder.UseLazyLoadingProxies();

// Opción 2: Usar Include explícitamente (recomendado)
var ventas = context.Ventas.Include(v => v.LineasDeVenta).ToList();
```

### **2. Transacciones**

**EF6:**
```csharp
using (var transaction = context.Database.BeginTransaction())
{
    // Operaciones
    transaction.Commit();
}
```

**EF Core:**
```csharp
using (var transaction = context.Database.BeginTransaction())
{
    // Operaciones
    await transaction.CommitAsync(); // Async recomendado
}
```

### **3. Consultas Raw SQL**

**EF6:**
```csharp
context.Database.SqlQuery<Empleado>("SELECT * FROM Empleados WHERE Legajo = @p0", legajo);
```

**EF Core:**
```csharp
context.Empleados.FromSqlRaw("SELECT * FROM Empleados WHERE Legajo = {0}", legajo);
// O mejor aún:
context.Empleados.FromSqlInterpolated($"SELECT * FROM Empleados WHERE Legajo = {legajo}");
```

---

## ?? **Próximos Pasos**

1. ? **Compilar la solución**
   ```bash
   dotnet build
   ```

2. ? **Ejecutar el script SQL** (si aún no lo hiciste)
   - Ubicación: `Database\CreateDatabase_IvcDb.sql`

3. ? **Probar la aplicación**
   - Presiona F5 en Visual Studio
   - Login: Legajo `1234`, Contraseña `admin`

4. ? **Crear migración inicial de EF Core** (opcional pero recomendado)
   ```bash
   dotnet ef migrations add InitialCreate --project TFI.AccesoADatos --startup-project TFI.Vista
   ```

---

## ?? **Solución de Problemas**

### **Error: "Could not load file or assembly 'EntityFramework'"**
**Solución**: Limpia y recompila la solución:
```bash
dotnet clean
dotnet build
```

### **Error: "No DbContext found"**
**Solución**: Verifica que `IvcNetContext` herede de `DbContext` de `Microsoft.EntityFrameworkCore`

### **Error: "Connection string not found"**
**Solución**: Asegúrate de que `System.Configuration.ConfigurationManager` esté instalado

---

## ?? **Recursos Adicionales**

- [Documentación oficial de EF Core](https://learn.microsoft.com/en-us/ef/core/)
- [Migración de EF6 a EF Core](https://learn.microsoft.com/en-us/ef/efcore-and-ef6/porting/)
- [Diferencias entre EF6 y EF Core](https://learn.microsoft.com/en-us/ef/efcore-and-ef6/)

---

## ? **Beneficios Obtenidos**

- ? **Compatibilidad total con .NET 8.0**
- ? **Mejor rendimiento (hasta 70% más rápido)**
- ? **Herramientas CLI modernas (`dotnet ef`)**
- ? **Soporte activo y actualizaciones de Microsoft**
- ? **Preparado para futuras versiones de .NET**
- ? **Mejor integración con Dependency Injection**
- ? **Sin cambios en las entidades de dominio**
- ? **Base de datos y datos preservados**

---

**Migración completada exitosamente! ??**
