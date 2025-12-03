# ?? Guía Rápida: Puesta en Marcha después de la Migración a EF Core

## ? **Paso 1: Restaurar Paquetes NuGet**

Abre una terminal en la raíz del proyecto y ejecuta:

```bash
dotnet restore
```

O en Visual Studio:
- **Clic derecho en la solución** ? **Restore NuGet Packages**

---

## ? **Paso 2: Compilar la Solución**

```bash
dotnet build
```

O en Visual Studio:
- **Build** ? **Rebuild Solution** (Ctrl + Shift + B)

**Resultado esperado:**
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

---

## ? **Paso 3: Crear la Base de Datos**

### **Opción A: Usando el Script SQL (Recomendado)**

1. **Abre SQL Server Management Studio (SSMS)**
2. **Conéctate a**: `.\SQLEXPRESS`
3. **Abre el archivo**: `Database\CreateDatabase_IvcDb.sql`
4. **Ejecuta** el script (F5)

### **Opción B: Usando dotnet ef CLI**

```bash
# Instalar la herramienta dotnet-ef globalmente (una sola vez)
dotnet tool install --global dotnet-ef

# Crear migración inicial
dotnet ef migrations add InitialCreate --project TFI.AccesoADatos --startup-project TFI.Vista

# Aplicar migración (crea la BD y tablas)
dotnet ef database update --project TFI.AccesoADatos --startup-project TFI.Vista
```

---

## ? **Paso 4: Verificar la Base de Datos**

### **En Visual Studio:**
1. **View** ? **SQL Server Object Explorer**
2. Expande: **SQL Server** ? **.\SQLEXPRESS** ? **Databases**
3. Verifica que existe: **IvcDb**
4. Expande **Tables** y verifica:
   - Empleados
   - Indumentarias
   - Talles
   - Stock
   - Ventas
   - LineasDeVenta
   - Pagos
   - Facturas

### **Verificar datos de prueba:**

```sql
USE IvcDb;
GO

-- Verificar empleado de prueba
SELECT * FROM Empleados;
-- Debería mostrar: Legajo 1234, Contraseña: admin

-- Verificar talles
SELECT * FROM Talles;
-- Debería mostrar: XS, S, M, L, XL, XXL

-- Verificar productos
SELECT * FROM Indumentarias;
-- Debería mostrar 4 productos

-- Verificar stock
SELECT * FROM Stock;
-- Debería tener varias filas con stock inicial
```

---

## ? **Paso 5: Ejecutar la Aplicación**

1. **Asegúrate de que `TFI.Vista` es el proyecto de inicio**
   - Clic derecho en `TFI.Vista` ? **Set as Startup Project**
   - Debería aparecer en negrita

2. **Presiona F5** o **Debug** ? **Start Debugging**

3. **Debería aparecer la pantalla de login**

4. **Ingresa las credenciales de prueba:**
   - **Legajo**: `1234`
   - **Contraseña**: `admin`

5. **Si todo está bien**, deberías entrar al menú principal! ??

---

## ?? **Solución de Problemas Comunes**

### **Error: "Could not load file or assembly 'EntityFramework'"**

**Causa**: Caché de compilación con referencias antiguas.

**Solución:**
```bash
# Limpia la solución
dotnet clean

# Elimina las carpetas bin y obj
rm -r **/bin
rm -r **/obj

# Restaura y compila de nuevo
dotnet restore
dotnet build
```

### **Error: "Cannot open database 'IvcDb'"**

**Causa**: La base de datos no existe aún.

**Solución:**
- Ejecuta el script SQL: `Database\CreateDatabase_IvcDb.sql`

### **Error: "A network-related or instance-specific error"**

**Causa**: SQL Server no está corriendo o el nombre del servidor es incorrecto.

**Solución:**
1. Abre **Services** (services.msc)
2. Busca "SQL Server (SQLEXPRESS)"
3. Asegúrate de que está **Running**
4. Si no existe, verifica el nombre de tu servidor SQL

### **Error: "Login failed for user"**

**Causa**: Problema de autenticación de Windows.

**Solución:**
1. Verifica que tu usuario de Windows tenga permisos en SQL Server
2. O cambia a autenticación SQL en `App.config`:
```xml
connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=IvcDb;User Id=sa;Password=TU_PASSWORD;..."
```

### **Error: "No DbContext was found"**

**Causa**: El archivo `IvcNetContext.cs` no compiló correctamente.

**Solución:**
1. Abre `TFI.AccesoADatos\IvcNetContext.cs`
2. Verifica que esté usando `Microsoft.EntityFrameworkCore`
3. Recompila solo ese proyecto: Clic derecho ? **Rebuild**

---

## ?? **Testing de Funcionalidades**

### **Test 1: Login**
- ? Debería aceptar Legajo: 1234, Contraseña: admin
- ? Debería rechazar credenciales incorrectas

### **Test 2: Ver Productos**
- ? Debería mostrar los 4 productos de ejemplo
- ? Cada producto debe tener código, descripción y precio

### **Test 3: Verificar Stock**
- ? Debería poder consultar stock disponible
- ? Debería mostrar talles disponibles para cada producto

### **Test 4: Crear una Venta (si tienes la funcionalidad)**
- ? Debería poder seleccionar productos
- ? Debería calcular el total correctamente
- ? Debería guardar en la BD

---

## ?? **Comandos Útiles de EF Core**

### **Ver todas las migraciones:**
```bash
dotnet ef migrations list --project TFI.AccesoADatos --startup-project TFI.Vista
```

### **Crear una nueva migración:**
```bash
dotnet ef migrations add NombreDeLaMigracion --project TFI.AccesoADatos --startup-project TFI.Vista
```

### **Ver el SQL que se ejecutará:**
```bash
dotnet ef migrations script --project TFI.AccesoADatos --startup-project TFI.Vista
```

### **Revertir última migración:**
```bash
dotnet ef migrations remove --project TFI.AccesoADatos --startup-project TFI.Vista
```

### **Actualizar la base de datos:**
```bash
dotnet ef database update --project TFI.AccesoADatos --startup-project TFI.Vista
```

### **Ver información del DbContext:**
```bash
dotnet ef dbcontext info --project TFI.AccesoADatos --startup-project TFI.Vista
```

---

## ?? **Diferencias Clave para los Desarrolladores**

### **Antes (EF6)**
```csharp
// Consulta simple
var empleado = context.Empleados.FirstOrDefault(e => e.Legajo == legajo);

// Con relaciones (lazy loading automático)
var venta = context.Ventas.Find(id);
var lineas = venta.LineasDeVenta; // Se carga automáticamente
```

### **Ahora (EF Core 8)**
```csharp
// Consulta simple (igual)
var empleado = context.Empleados.FirstOrDefault(e => e.Legajo == legajo);

// Con relaciones (DEBE usar Include explícitamente)
var venta = context.Ventas
    .Include(v => v.LineasDeVenta)
    .ThenInclude(l => l.Indumentaria)
    .FirstOrDefault(v => v.Id == id);

// O habilitar lazy loading (requiere configuración)
```

### **Operaciones Async (Recomendadas en EF Core)**
```csharp
// Antes
var empleados = context.Empleados.ToList();

// Ahora (mejor rendimiento)
var empleados = await context.Empleados.ToListAsync();
```

---

## ?? **Checklist Final**

- [ ] Paquetes NuGet restaurados
- [ ] Solución compila sin errores
- [ ] Base de datos `IvcDb` existe
- [ ] Tablas creadas (8 tablas + __EFMigrationsHistory)
- [ ] Datos de prueba insertados
- [ ] Aplicación inicia correctamente
- [ ] Login funciona con Legajo 1234
- [ ] Menú principal se muestra
- [ ] No hay errores en la consola de salida

---

## ?? **¡Felicidades!**

Si completaste todos los pasos, tu proyecto está ahora corriendo con:
- ? **.NET 8.0** (versión más reciente)
- ? **Entity Framework Core 8.0** (ORM moderno)
- ? **SQL Server Express** (base de datos)
- ? **Windows Forms .NET 8** (UI)
- ? **Unity Container** (Inyección de dependencias)

**Tu aplicación está lista para seguir desarrollándose! ??**

---

## ?? **¿Necesitas Ayuda?**

Si encuentras problemas:
1. Revisa esta guía nuevamente
2. Consulta: `Database\MIGRATION_EF6_TO_EFCORE.md`
3. Verifica los logs en la consola de salida de Visual Studio
4. Revisa la documentación oficial: https://learn.microsoft.com/en-us/ef/core/
