using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI.Dominio;
using TFI.Dominio.Contratos;
using Microsoft.EntityFrameworkCore;

namespace TFI.AccesoADatos
{
    public class Repositorio : IRepositorio
    {
   private readonly IPTNetContext _context;
     
        public Repositorio(IPTNetContext context)
        {
   this._context = context;
      }

        public Repositorio() : this(new IPTNetContext())
        {
        }

      public Indumentaria BuscarIndumentaria(int codigo)
  {
 return this._context.Indumentarias
      .AsNoTracking()
 .FirstOrDefault(ind => ind.Codigo == codigo);
 }

        public Stock BuscarStock(Indumentaria indumentaria, int talleId)
      {
 return _context.Stocks
     .Include(s => s.Indumentaria)
        .Include(s => s.Talle)
                .Where(stock => stock.Indumentaria.Id == indumentaria.Id && stock.Talle.Id == talleId)
          .FirstOrDefault();
        }

public List<Indumentaria> GetIndumentarias()
        {
     return _context.Indumentarias
   .AsNoTracking()
  .ToList();
   }

public List<Talle> GetTalles()
        {
            return _context.Talles
  .AsNoTracking()
    .ToList();
    }

    public void GuardarVenta(Venta venta)
        {
    // ✅ SOLUCIÓN: Configurar correctamente las foreign keys manualmente
    // y manejar el estado de las entidades relacionadas
    
  foreach (var lineaVenta in venta.LineaDeVentas)
    {
    if (lineaVenta.Indumentaria != null)
 {
            // ✅ CRÍTICO: Obtener el Id de la Indumentaria ANTES de que EF intente rastrearla
            int indumentariaId = lineaVenta.Indumentaria.Id;
         
       // ✅ Buscar si hay una entidad Indumentaria ya siendo rastreada
  var trackedIndumentaria = _context.ChangeTracker.Entries<Indumentaria>()
                .FirstOrDefault(e => e.Entity.Id == indumentariaId);
    
         if (trackedIndumentaria != null)
        {
       // Desconectar la entidad rastreada para evitar conflictos
trackedIndumentaria.State = EntityState.Detached;
        }
   
            // ✅ Marcar la Indumentaria como "sin cambios" (ya existe en BD)
    _context.Entry(lineaVenta.Indumentaria).State = EntityState.Unchanged;
        }
    }
 
 // ✅ Marcar la Venta como nueva entidad a insertar
    _context.Ventas.Add(venta);
    
    try
    {
        _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
        // ✅ Proporcionar mensaje de error más detallado
        throw new Exception($"Error al guardar la venta: {ex.InnerException?.Message ?? ex.Message}", ex);
    }
        }

  public bool IniciarSesion(int legajo, string contraseña)
      {
    return _context.Empleados
     .AsNoTracking()
         .Where(e => e.Legajo == legajo && e.Contraseña == contraseña)
     .Any();
        }
    
 // ===== MÉTODOS PARA MÓDULO DE COBRO DE CUOTAS =====
        
  /// <summary>
     /// Busca una cuota por su código de barras
 /// </summary>
  public Cuota BuscarCuotaPorCodigoBarras(string codigoBarras)
{
 return _context.Cuotas
      .Include(c => c.Alumno)
       // ⚠️ Ya no incluimos PagoCuota porque eliminamos la navegación
       .AsNoTracking()
       .FirstOrDefault(c => c.CodigoBarras == codigoBarras);
  }

 /// <summary>
   /// Busca todas las cuotas de un alumno por su DNI
   /// </summary>
 public List<Cuota> BuscarCuotasPorDNI(int dni)
     {
   var alumno = BuscarAlumnoPorDNI(dni);
if (alumno == null)
   return new List<Cuota>();

     return _context.Cuotas
     .Include(c => c.Alumno)
     // ⚠️ Ya no incluimos PagoCuota porque eliminamos la navegación
  .AsNoTracking()
     .Where(c => c.AlumnoId == alumno.Id)
     .OrderBy(c => c.Anio)
 .ThenBy(c => c.Mes)
  .ToList();
        }

  /// <summary>
        /// Busca un alumno por su DNI
  /// </summary>
        public Alumno BuscarAlumnoPorDNI(int dni)
   {
     return _context.Alumnos
  .AsNoTracking()
      .FirstOrDefault(a => a.DNI == dni);
  }

        /// <summary>
 /// Guarda el pago de una cuota (transaccional)
   /// </summary>
        public void GuardarPagoCuota(Cuota cuota, PagoCuota pago)
 {
  try
{
  // ✅ IMPORTANTE: No usar Include aquí, trabajar con la entidad mínima
   var cuotaDb = _context.Cuotas
      .FirstOrDefault(c => c.Id == cuota.Id);

    if (cuotaDb == null)
     throw new Exception("La cuota no existe en la base de datos.");
         
      if (cuotaDb.Estado == EstadoCuota.Pagada)
    throw new Exception("La cuota ya fue pagada anteriormente.");

    // ✅ SOLUCIÓN: Primero crear el pago SIN asignarlo a la cuota
     pago.CuotaId = cuota.Id; // Asignar solo el ID, no la navegación
            pago.Cuota = null; // ⚠️ CRÍTICO: Evitar navegación circular
  
     _context.PagosCuotas.Add(pago);
 _context.SaveChanges(); // Guardar primero el pago para obtener su Id
  
    // ✅ Ahora actualizar la cuota con el ID del pago
    cuotaDb.Estado = EstadoCuota.Pagada;
            cuotaDb.PagoCuotaId = pago.Id; // Solo asignar el ID
     
     _context.SaveChanges(); // Guardar cambios de la cuota
        }
    catch (DbUpdateException ex)
         {
    throw new Exception($"Error al guardar el pago de la cuota: {ex.InnerException?.Message ?? ex.Message}", ex);
    }
  }

  /// <summary>
   /// Obtiene todas las cuotas pendientes o vencidas
  /// </summary>
        public List<Cuota> GetCuotasPendientes()
        {
    return _context.Cuotas
   .Include(c => c.Alumno)
      .AsNoTracking()
             .Where(c => c.Estado != EstadoCuota.Pagada)
          .OrderBy(c => c.PrimerVencimiento)
          .ToList();
      }

        // ===== MÉTODOS PARA ABM DE INDUMENTARIA =====

        /// <summary>
        /// Guarda una nueva indumentaria en la base de datos
        /// </summary>
      public void GuardarIndumentaria(Indumentaria indumentaria)
        {
            try
 {
      _context.Indumentarias.Add(indumentaria);
             _context.SaveChanges();
            }
   catch (DbUpdateException ex)
          {
      throw new Exception($"Error al guardar la indumentaria: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }

        /// <summary>
        /// Actualiza una indumentaria existente
        /// </summary>
 public void ActualizarIndumentaria(Indumentaria indumentaria)
   {
       try
  {
   var indumentariaDb = _context.Indumentarias.FirstOrDefault(i => i.Id == indumentaria.Id);
          
             if (indumentariaDb == null)
  throw new Exception("La indumentaria no existe en la base de datos.");

   indumentariaDb.Codigo = indumentaria.Codigo;
      indumentariaDb.Descripcion = indumentaria.Descripcion;
indumentariaDb.Precio = indumentaria.Precio;

     _context.SaveChanges();
            }
            catch (DbUpdateException ex)
   {
     throw new Exception($"Error al actualizar la indumentaria: {ex.InnerException?.Message ?? ex.Message}", ex);
      }
        }

        /// <summary>
        /// Elimina una indumentaria y sus stocks relacionados
        /// </summary>
        public void EliminarIndumentaria(int id)
        {
   try
  {
       var indumentaria = _context.Indumentarias
     .FirstOrDefault(i => i.Id == id);

        if (indumentaria == null)
     throw new Exception("La indumentaria no existe en la base de datos.");

     // Eliminar stocks relacionados
     var stocks = _context.Stocks.Where(s => s.Indumentaria.Id == id).ToList();
                _context.Stocks.RemoveRange(stocks);

         // Eliminar la indumentaria
       _context.Indumentarias.Remove(indumentaria);
       _context.SaveChanges();
            }
   catch (DbUpdateException ex)
            {
 throw new Exception($"Error al eliminar la indumentaria: {ex.InnerException?.Message ?? ex.Message}", ex);
  }
        }

        /// <summary>
 /// Busca una indumentaria por su ID
/// </summary>
public Indumentaria BuscarIndumentariaPorId(int id)
      {
return _context.Indumentarias
              .AsNoTracking()
 .FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// Verifica si existe un código de indumentaria (útil para validaciones de duplicados)
        /// </summary>
        public bool ExisteCodigoIndumentaria(int codigo, int? idExcluir = null)
     {
            if (idExcluir.HasValue)
     {
        return _context.Indumentarias
           .AsNoTracking()
    .Any(i => i.Codigo == codigo && i.Id != idExcluir.Value);
 }
          else
    {
return _context.Indumentarias
       .AsNoTracking()
               .Any(i => i.Codigo == codigo);
     }
      }

        /// <summary>
   /// Obtiene todos los stocks de una indumentaria específica
        /// </summary>
        public List<Stock> GetStocksPorIndumentaria(int indumentariaId)
        {
            return _context.Stocks
                .Include(s => s.Talle)
      .Include(s => s.Indumentaria)
           .AsNoTracking()
   .Where(s => s.Indumentaria.Id == indumentariaId)
           .OrderBy(s => s.Talle.Descripcion)
     .ToList();
        }

/// <summary>
        /// Crea registros de stock para todos los talles de una nueva indumentaria
        /// </summary>
        public void CrearStockParaIndumentaria(Indumentaria indumentaria, int stockMinimo = 5, int stockMaximo = 50)
     {
            try
      {
        var talles = GetTalles();
    
  foreach (var talle in talles)
    {
      var stock = new Stock(stockMaximo, stockMinimo)
         {
   Indumentaria = indumentaria,
                   Talle = talle,
      Cantidad = 0 // Stock inicial en 0
          };
      
        _context.Stocks.Add(stock);
                }
       
       _context.SaveChanges();
            }
   catch (DbUpdateException ex)
            {
         throw new Exception($"Error al crear stock para la indumentaria: {ex.InnerException?.Message ?? ex.Message}", ex);
        }
    }

        /// <summary>
        /// Actualiza un registro de stock
        /// </summary>
     public void ActualizarStock(Stock stock)
        {
            try
            {
     var stockDb = _context.Stocks.FirstOrDefault(s => s.Id == stock.Id);
      
       if (stockDb == null)
   throw new Exception("El stock no existe en la base de datos.");

   stockDb.Cantidad = stock.Cantidad;
    stockDb.CantidadMinima = stock.CantidadMinima;
    stockDb.CantidadMaxima = stock.CantidadMaxima;

  _context.SaveChanges();
 }
            catch (DbUpdateException ex)
      {
       throw new Exception($"Error al actualizar el stock: {ex.InnerException?.Message ?? ex.Message}", ex);
     }
  }

        /// <summary>
        /// Guarda un nuevo registro de stock
        /// </summary>
        public void GuardarStock(Stock stock)
 {
      try
     {
         _context.Stocks.Add(stock);
        _context.SaveChanges();
            }
  catch (DbUpdateException ex)
  {
    throw new Exception($"Error al guardar el stock: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
}
    }
}
