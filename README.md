 IVC-NET - Sistema de Gestión del Instituto Vocacional Concepción
https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet https://img.shields.io/badge/EF%20Core-8.0-512BD4 https://img.shields.io/badge/SQL%20Server-2019+-CC2927?logo=microsoftsqlserver https://img.shields.io/badge/C%23-12.0-239120?logo=csharp
Sistema integral de gestión para el Instituto Vocacional Concepción que permite administrar la venta de indumentaria y el cobro de cuotas de estudiantes.
---
🎯 Propósito
IVC-NET es una solución de escritorio desarrollada en Windows Forms que moderniza la gestión del instituto mediante dos módulos principales:
•	🛒 Venta de Indumentaria: Control de stock, ventas, facturación y pagos de productos del instituto
•	💰 Cobro de Cuotas: Gestión de alumnos, generación de cuotas mensuales, control de vencimientos y registro de pagos con recargos automáticos
---
🏗️ Arquitectura
El proyecto sigue una arquitectura en capas con separación de responsabilidades:
IVC-NET/
├── 📂 TFI.Dominio/              # Capa de Dominio (Entidades de negocio)
│   ├── Empleado.cs
│   ├── Venta.cs, LineaDeVenta.cs, Pago.cs, Factura.cs
│   ├── Stock.cs, Indumentaria.cs, Talle.cs
│   └── Alumno.cs, Cuota.cs, PagoCuota.cs
│
├── 📂 TFI.AccesoADatos/         # Capa de Acceso a Datos (EF Core 8)
│   ├── IPTNetContext.cs         # DbContext principal
│   ├── Repositorio.cs           # Implementación del patrón Repository
│   └── Migrations/              # Migraciones de Entity Framework Core
│
└── 📂 TFI.Vista/                # Capa de Presentación (Windows Forms + MVP)
    ├── Vistas/                  # Formularios de usuario
    ├── Presentadores/           # Lógica de presentación (Patrón MVP)
    ├── DTOs/                    # Objetos de transferencia de datos
    └── Styles/                  # Estilos visuales modernos
    Tecnologías Clave
•	Framework: .NET 8.0 (migrado desde .NET Framework 4.x)
•	ORM: Entity Framework Core 8.0.11 (migrado desde EF 6.5.1)
•	Base de Datos: SQL Server (LocalDB o instancia completa)
•	UI: Windows Forms con estilos modernos personalizados
•	IoC: Unity Container para inyección de dependencias
•	Patrón: MVP (Model-View-Presenter) para separación de lógica
---
⚙️ Instalación
Requisitos Previos
•	Visual Studio 2022 (17.8 o superior)
•	.NET 8.0 SDK instalado
•	SQL Server 2019+ o SQL Server Express LocalDB
•	Git para clonar el repositorio
Pasos de Instalación
1️⃣Clonar repositorio
git clone https://github.com/tu-usuario/IVC-NET.git
cd IVC-NET
2️⃣ Configurar la Cadena de Conexión
El proyecto utiliza App.config para mantener compatibilidad con la arquitectura legacy. Edita el archivo App.config en el proyecto TFI.Vista:
<configuration>
  <connectionStrings>
    <add name="IvcDb" 
         connectionString="Data Source=.;Initial Catalog=IvcDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;" 
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
Opciones de configuración:
•	LocalDB: Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=IvcDb;Integrated Security=True;
•	SQL Server Express: Data Source=.\SQLEXPRESS;Initial Catalog=IvcDb;Integrated Security=True;
•	SQL Server con autenticación: Data Source=servidor;Initial Catalog=IvcDb;User Id=usuario;Password=contraseña;
3️⃣ Restaurar Paquetes NuGet
Abre la solución en Visual Studio 2022 y restaura automáticamente los paquetes, o ejecuta:
dotnet restore
4️⃣ Aplicar Migraciones de Base de Datos
Abre la Consola del Administrador de Paquetes en Visual Studio (Tools > NuGet Package Manager > Package Manager Console) y ejecuta:
# Asegurarse de estar en el proyecto TFI.AccesoADatos
cd TFI.AccesoADatos

# Crear la base de datos y aplicar todas las migraciones
Update-Database
5️⃣ Compilar y Ejecutar
dotnet build
dotnet run --project TFI.Vista
🚀 Uso del Sistema
Inicio de Sesión
1.	Al iniciar la aplicación, se mostrará la pantalla de Login
2.	Ingresa las credenciales de empleado (configuradas en la base de datos)
3.	Presiona Enter o haz clic en Ingresar
Módulo de Venta de Indumentaria
•	Buscar productos: Ingresa el código de indumentaria
•	Agregar al carrito: Selecciona talle y cantidad
•	Procesar venta: Genera pago y factura automáticamente
•	Control de stock: El sistema actualiza automáticamente el inventario
Módulo de Cobro de Cuotas
•	Gestión de alumnos: Alta, baja y modificación de estudiantes
•	Generación de cuotas: Creación automática con código de barras
•	Control de vencimientos: Cálculo automático de recargos (5% acumulativo por vencimiento)
•	Registro de pagos: Múltiples medios de pago (efectivo, tarjeta, transferencia)
---
🗄️ Estructura de Base de Datos
Tablas Principales
Módulo de Ventas
•	Empleados - Personal del instituto
•	Indumentarias - Catálogo de productos
•	Talles - Talles disponibles
•	Stock - Control de inventario por producto y talle
•	Ventas - Registro de transacciones de venta
•	LineasDeVenta - Detalle de productos vendidos
•	Pagos - Información de pagos realizados
•	Facturas - Facturación de ventas
Módulo de Cuotas
•	Alumnos - Datos de estudiantes (DNI único)
•	Cuotas - Cuotas mensuales con 3 vencimientos
•	PagosCuotas - Registro de pagos de cuotas con recargos
---
🔧 Características Técnicas
Patrones Implementados
•	Repository Pattern: Abstracción de acceso a datos
•	MVP (Model-View-Presenter): Separación de lógica de presentación
•	Dependency Injection: Unity Container para IoC
•	DTOs: Transferencia optimizada de datos entre capas
Reglas de Negocio
•	Recargos por vencimiento: 5% acumulativo por cada fecha de vencimiento pasada
•	Control de stock: Validación automática de disponibilidad antes de venta
•	Facturación automática: Generación de facturas al confirmar pagos
•	Índices únicos: DNI de alumno y código de barras de cuota
---
📝 Créditos y Licencia
Desarrollado por: Estudiantes de la Universidad Tecnologica Nacional Facultad Regional Tucuman
Año: 2025
---
🐛 Troubleshooting
Error: "Cannot attach the file 'IvcDb.mdf' as database 'IvcDb'"
Solución: Elimina archivos .mdf y .ldf antiguos, luego ejecuta Update-Database nuevamente.
Error: "A connection was successfully established with the server, but then an error occurred during the login process"
Solución: Verifica que Encrypt=False;TrustServerCertificate=True; esté en tu cadena de conexión.
Error de Migración: "There is already an object named 'X' in the database"
Solución: Ejecuta Drop-Database y vuelve a crear:
Drop-Database -Confirm
Update-Database
¡Gracias por usar IVC-NET! 🎓✨
