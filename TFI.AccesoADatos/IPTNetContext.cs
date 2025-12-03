using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using TFI.Dominio;

namespace TFI.AccesoADatos
{
    /// <summary>
    /// Contexto de Entity Framework Core 8 para la base de datos IvcDb
    /// Migrado desde Entity Framework 6.5.1
    /// </summary>
    public class IPTNetContext : DbContext
    {
 public IPTNetContext() : base()
        {
        }

        public IPTNetContext(DbContextOptions<IPTNetContext> options) : base(options)
     {
    }

   // DbSets - Colecciones de entidades
        public DbSet<Empleado> Empleados { get; set; }
  public DbSet<Venta> Ventas { get; set; }
  public DbSet<LineaDeVenta> LineasDeVenta { get; set; }
     public DbSet<Factura> Facturas { get; set; }
        public DbSet<Stock> Stocks { get; set; }
  public DbSet<Talle> Talles { get; set; }
      public DbSet<Indumentaria> Indumentarias { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        
   // NUEVO: DbSets para módulo de Cobro de Cuotas
  public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<Cuota> Cuotas { get; set; }
        public DbSet<PagoCuota> PagosCuotas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
         if (!optionsBuilder.IsConfigured)
          {
    // Leer la cadena de conexión desde App.config
     string connectionString = GetConnectionString();
       optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);

   // Configuración de nombres de tablas (igual que en EF6)
     modelBuilder.Entity<Venta>().ToTable("Ventas");
    modelBuilder.Entity<LineaDeVenta>().ToTable("LineasDeVenta");
 modelBuilder.Entity<Empleado>().ToTable("Empleados");
    modelBuilder.Entity<Factura>().ToTable("Facturas");
            modelBuilder.Entity<Stock>().ToTable("Stock");
     modelBuilder.Entity<Talle>().ToTable("Talles");
  modelBuilder.Entity<Pago>().ToTable("Pagos");
     modelBuilder.Entity<Indumentaria>().ToTable("Indumentarias");
     
        // NUEVO: Configuración de tablas para módulo de Cobro de Cuotas
  modelBuilder.Entity<Alumno>().ToTable("Alumnos");
 modelBuilder.Entity<Cuota>().ToTable("Cuotas");
  modelBuilder.Entity<PagoCuota>().ToTable("PagosCuotas");

            // Configuraciones adicionales (claves primarias, relaciones, etc.)
   ConfigureEmployees(modelBuilder);
   ConfigureIndumentarias(modelBuilder);
    ConfigureTalles(modelBuilder);
  ConfigureStock(modelBuilder);
    ConfigureVentas(modelBuilder);
   ConfigureLineasDeVenta(modelBuilder);
    ConfigurePagos(modelBuilder);
            ConfigureFacturas(modelBuilder);
   
            // NUEVO: Configuraciones para módulo de Cobro de Cuotas
  ConfigureAlumnos(modelBuilder);
  ConfigureCuotas(modelBuilder);
  ConfigurePagosCuotas(modelBuilder);
     }

    private void ConfigureEmployees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(entity =>
       {
           entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
       entity.Property(e => e.Legajo).IsRequired();
      });
        }

        private void ConfigureIndumentarias(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Indumentaria>(entity =>
     {
         entity.HasKey(i => i.Id);
         entity.Property(i => i.Id).ValueGeneratedOnAdd();
        entity.Property(i => i.Codigo).IsRequired();
              entity.Property(i => i.Precio).IsRequired();
 });
    }

        private void ConfigureTalles(ModelBuilder modelBuilder)
      {
    modelBuilder.Entity<Talle>(entity =>
  {
 entity.HasKey(t => t.Id);
        entity.Property(t => t.Id).ValueGeneratedOnAdd();
            });
        }

        private void ConfigureStock(ModelBuilder modelBuilder)
{
      modelBuilder.Entity<Stock>(entity =>
{
       entity.HasKey(s => s.Id);
         entity.Property(s => s.Id).ValueGeneratedOnAdd();
   entity.Property(s => s.Cantidad).IsRequired().HasDefaultValue(0);
          entity.Property(s => s.CantidadMaxima).IsRequired().HasDefaultValue(100);
      entity.Property(s => s.CantidadMinima).IsRequired().HasDefaultValue(10);

         // Relaciones
  entity.HasOne(s => s.Indumentaria)
    .WithMany()
   .HasForeignKey("Indumentaria_Id")
    .OnDelete(DeleteBehavior.Restrict);

          entity.HasOne(s => s.Talle)
  .WithMany()
            .HasForeignKey("Talle_Id")
.OnDelete(DeleteBehavior.Restrict);
   });
        }

        private void ConfigureVentas(ModelBuilder modelBuilder)
      {
   modelBuilder.Entity<Venta>(entity =>
{
entity.HasKey(v => v.Id);
  entity.Property(v => v.Id).ValueGeneratedOnAdd();
   entity.Property(v => v.FechaHora).IsRequired();

     // Relación con LineasDeVenta
 entity.HasMany(v => v.LineaDeVentas)
                .WithOne()
         .HasForeignKey("Venta_Id")
      .OnDelete(DeleteBehavior.Cascade);

     // ? CORREGIDO: Relación con Pago (1 a 1, Venta es la principal)
     entity.HasOne(v => v.Pago)
  .WithOne()
       .HasForeignKey<Pago>("Venta_Id")
    .OnDelete(DeleteBehavior.Cascade) // Cambiar a Cascade para que se elimine el pago si se elimina la venta
     .IsRequired(false); // El pago es opcional hasta que se confirma la venta
      });
        }

    private void ConfigureLineasDeVenta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LineaDeVenta>(entity =>
            {
                // Clave primaria
     entity.HasKey(l => l.Id);
        entity.Property(l => l.Id).ValueGeneratedOnAdd();
         entity.Property(l => l.Cantidad).IsRequired();

            // ? IGNORAR PROPIEDADES CALCULADAS (no mapean a columnas de BD)
      entity.Ignore(l => l.CodigoIndumentaria);
    entity.Ignore(l => l.DescripcionIndumentaria);
    entity.Ignore(l => l.PrecioIndumentaria);
 entity.Ignore(l => l.Subtotal);

                // Relación con Indumentaria
     entity.HasOne(l => l.Indumentaria)
          .WithMany()
 .HasForeignKey("Indumentaria_Id")
.OnDelete(DeleteBehavior.Restrict);

      // La relación con Venta se define en ConfigureVentas
 });
        }

      private void ConfigurePagos(ModelBuilder modelBuilder)
        {
 modelBuilder.Entity<Pago>(entity =>
            {
       entity.HasKey(p => p.Id);
      entity.Property(p => p.Id).ValueGeneratedOnAdd();
     entity.Property(p => p.FechaHora).IsRequired();
     entity.Property(p => p.Total).IsRequired();

           // Relación con Factura
        entity.HasOne(p => p.Factura)
  .WithOne()
    .HasForeignKey<Factura>("Pago_Id")
      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureFacturas(ModelBuilder modelBuilder)
        {
   modelBuilder.Entity<Factura>(entity =>
 {
    entity.HasKey(f => f.Id);
  entity.Property(f => f.Id).ValueGeneratedOnAdd();
  entity.Property(f => f.FechaHora).IsRequired();
      entity.Property(f => f.Total).IsRequired();
            
     // ? La relación con Pago se define en ConfigurePagos
         // No necesitamos definirla aquí para evitar duplicación
      });
   }

            /// <summary>
/// Lee la cadena de conexión del archivo App.config
      /// Compatible con proyectos .NET 8 usando ConfigurationManager
/// </summary>
   private string GetConnectionString()
   {
       try
            {
     // Intenta leer desde App.config usando System.Configuration
   // Necesitas agregar referencia al paquete System.Configuration.ConfigurationManager
          var connectionString = System.Configuration.ConfigurationManager
       .ConnectionStrings["IvcDb"]?.ConnectionString;

         if (!string.IsNullOrEmpty(connectionString))
     {
   return connectionString;
  }
  }
     catch
  {
        // Si falla, usa cadena de conexión por defecto
        }

       // Cadena de conexión por defecto (fallback)
     return @"Data Source=.;Initial Catalog=IvcDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
        }
        
    // ===== CONFIGURACIONES PARA MÓDULO DE COBRO DE CUOTAS =====
  
  private void ConfigureAlumnos(ModelBuilder modelBuilder)
   {
    modelBuilder.Entity<Alumno>(entity =>
    {
    entity.HasKey(a => a.Id);
    entity.Property(a => a.Id).ValueGeneratedOnAdd();
       entity.Property(a => a.DNI).IsRequired();
     entity.Property(a => a.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Apellido).IsRequired().HasMaxLength(100);
    entity.Property(a => a.Email).HasMaxLength(150);
     entity.Property(a => a.Telefono).HasMaxLength(20);
    
     // Índice único para DNI
    entity.HasIndex(a => a.DNI).IsUnique();
   
       // Ignorar propiedades calculadas
  entity.Ignore(a => a.NombreCompleto);
  
      // Relación: Un alumno tiene muchas cuotas
         entity.HasMany(a => a.Cuotas)
       .WithOne(c => c.Alumno)
   .HasForeignKey(c => c.AlumnoId)
   .OnDelete(DeleteBehavior.Restrict);
     });
        }

  private void ConfigureCuotas(ModelBuilder modelBuilder)
  {
     modelBuilder.Entity<Cuota>(entity =>
   {
      entity.HasKey(c => c.Id);
   entity.Property(c => c.Id).ValueGeneratedOnAdd();
  entity.Property(c => c.CodigoBarras).IsRequired().HasMaxLength(50);
    entity.Property(c => c.Mes).IsRequired();
 entity.Property(c => c.Anio).IsRequired();
    entity.Property(c => c.MontoOriginal).IsRequired();
  entity.Property(c => c.PrimerVencimiento).IsRequired();
     entity.Property(c => c.SegundoVencimiento).IsRequired();
         entity.Property(c => c.TercerVencimiento).IsRequired();
    entity.Property(c => c.Estado)
    .IsRequired()
     .HasConversion<int>(); // Guardar enum como int
     
     // Índice único para código de barras
   entity.HasIndex(c => c.CodigoBarras).IsUnique();
  
       // Ignorar propiedades calculadas
     entity.Ignore(c => c.PeriodoDescripcion);
  
  // La relación con Alumno se define en ConfigureAlumnos
  // La propiedad PagoCuotaId será solo un campo nullable sin navegación
  });
   }

private void ConfigurePagosCuotas(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<PagoCuota>(entity =>
   {
    entity.HasKey(p => p.Id);
    entity.Property(p => p.Id).ValueGeneratedOnAdd();
 entity.Property(p => p.FechaPago).IsRequired();
          entity.Property(p => p.MontoAbonado).IsRequired();
       entity.Property(p => p.Recargo).IsRequired().HasDefaultValue(0);
    entity.Property(p => p.MedioPago).HasMaxLength(50).HasDefaultValue("Efectivo");
  entity.Property(p => p.Observaciones).HasMaxLength(500);
  
         // SOLUCIÓN: Relación unidireccional solo desde PagoCuota hacia Cuota
   entity.HasOne(p => p.Cuota)
                .WithMany() // ?? Sin navegación desde Cuota
       .HasForeignKey(p => p.CuotaId)
       .OnDelete(DeleteBehavior.Restrict)
         .IsRequired();
     });
  }
    }
}
