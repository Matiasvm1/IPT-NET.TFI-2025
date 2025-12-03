# 🏛️ IPT-NET — Sistema de Gestión Institucional (TFI 2025)

![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2019+-CC2927?logo=microsoftsqlserver)
![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)
![WinForms](https://img.shields.io/badge/UI-WinForms-blue)

Descripción
-----------
IPT-NET es una aplicación de escritorio desarrollada en WinForms sobre .NET 8, creada como Trabajo Final Integrador (TFI) para modernizar los procesos administrativos del Instituto Privado Tucumán. Integra gestión de ventas de indumentaria y cobro de cuotas de alumnos, con persistencia en SQL Server usando Entity Framework Core.

Características principales
--------------------------
- Gestión de stock e indumentaria (ABM, talles, talles por prenda).
- Proceso de ventas con carrito, validación de stock y generación de comprobantes.
- Gestión de alumnos (ABM) y generación automática de cuotas mensuales.
- Cálculo de recargos por mora (5% acumulativo por vencimiento atrasado, configurable).
- Generación simulada de códigos de barras para cuotas.
- Arquitectura en capas con patrón MVP (Model–View–Presenter) y contenedor de DI (Unity).
- Base de datos mediante EF Core (migrations) y soporte para SQL Server / LocalDB.

Arquitectura y estructura del repo
----------------------------------
El proyecto está organizado por capas para separar responsabilidades:

- TFI.Dominio/              — Entidades POCO y reglas de negocio (Alumno, Empleado, Venta, Cuota, Indumentaria, etc.)
- TFI.AccesoADatos/         — DbContext (IPTNetContext), repositorios, migraciones (Entity Framework Core)
- TFI.Vista/                — Interfaz (WinForms), Presentadores (MVP), DTOs y configuración (App.config)

Requisitos
----------
- Visual Studio 2022 (17.8+) o Visual Studio 2022 Preview con soporte .NET 8
- .NET 8.0 SDK
- SQL Server 2019+, Express o LocalDB
- Git
- (Opcional) Package Manager Console en Visual Studio para migraciones

Instalación y puesta en marcha
------------------------------

1) Clonar el repositorio
```bash
git clone https://github.com/Matiasvm1/IPT-NET.TFI-2025.git
cd IPT-NET.TFI-2025
```

2) Abrir la solución en Visual Studio y seleccionar el proyecto TFI.Vista como proyecto de inicio.

3) Configurar cadena de conexión
- Editar TFI.Vista/App.config o el archivo de configuración correspondiente y ajustar la connectionString para apuntar a tu servidor SQL.

Ejemplos:
```xml
<!-- SQL Server Express -->
<add name="IvcDb"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=IvcDb;Integrated Security=True;Encrypt=False;"
     providerName="System.Data.SqlClient" />

<!-- LocalDB -->
<add name="IvcDb"
     connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=IvcDb;Integrated Security=True;"
     providerName="System.Data.SqlClient" />
```

4) Crear la base de datos (migraciones)
- Usando Package Manager Console en Visual Studio:
  - Establecer `TFI.AccesoADatos` como proyecto predeterminado en PMC.
  - Ejecutar:
  ```
  Update-Database
  ```
- O usando dotnet-ef (si está instalado):
  ```bash
  dotnet ef database update --project TFI.AccesoADatos --startup-project TFI.Vista
  ```

5) (Opcional) Ejecutar seeders
- Si el proyecto incluye un seeder para crear usuarios/empleados iniciales, habilitarlo o ejecutar la rutina correspondiente para crear al menos un usuario administrador.

6) Compilar y ejecutar
- Desde Visual Studio: F5
- O por línea de comandos:
```bash
dotnet build
# Ejecutar el proyecto de la UI desde Visual Studio o configurar el comando 'dotnet run' apuntando al proyecto TFI.Vista si aplica.
```

Operación y notas de uso
-----------------------
- Inicio de sesión: el sistema requiere un empleado registrado. Si no hay usuarios, crear uno directamente en la base de datos o mediante el seeder.
- Módulo Ventas:
  - Buscar indumentaria por código o descripción.
  - Seleccionar talle y cantidad; el sistema valida el stock en tiempo real.
  - Al finalizar, se genera un comprobante y se decrementa el stock.
- Módulo Cuotas:
  - ABM de alumnos y generación automática de cuotas mensuales.
  - Recargos aplican automáticamente por cuotas vencidas (configurable por constantes del dominio).
  - Código de barras: funcionalidad de simulación para identificación.

Tecnologías y librerías
-----------------------
- Plataforma: .NET 8 (C# 12)
- UI: WinForms (MVP)
- ORM: Entity Framework Core 8 (v8.0.11)
- Base de datos: SQL Server 2019+ / LocalDB
- DI: Unity
- Herramientas: Visual Studio 2022, dotnet-ef (opcional)

Buenas prácticas y recomendaciones
----------------------------------
- Usar migraciones para versionar el esquema de la base de datos.
- No subir credenciales en App.config (usar variables de entorno o secretos en producción).
- Mantener la lógica de negocio en TFI.Dominio y la persistencia en TFI.AccesoADatos.
- Es recomendable usar control de versiones de las migraciones en conjunto con los cambios en modelos.

Contacto
--------
- Equipo: Grupo 6 – Diseño de Sistemas
- Institución: UTN – Facultad Regional Tucumán
- Comision: 3k4
- Año: 2025
- Autor del repositorio: Matiasvm1

Agradecimientos
---------------
TFI desarrollado por el grupo para la cátedra y presentación de 2025. Cualquier mejora o reporte de errores es bienvenido.


