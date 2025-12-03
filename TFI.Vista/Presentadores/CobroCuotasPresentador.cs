using System;
using System.Collections.Generic;
using System.Linq;
using TFI.Dominio;
using TFI.Dominio.Contratos;
using TFI.Dominio.Interfaces;

namespace TFI.Vista.Presentadores
{
  /// <summary>
    /// Presentador para el módulo de Cobro de Cuotas (patrón MVP)
    /// Sigue la misma estructura que VentaIndumentariaPresentador
    /// </summary>
    public class CobroCuotasPresentador
    {
        private IRepositorio _repositorio;
        private ICobroCuotasVista _vista;
        private Cuota _cuotaSeleccionada;
   private List<Cuota> _cuotasAlumno;

        public CobroCuotasPresentador(IRepositorio repositorio)
        {
 this._repositorio = repositorio;
            this._cuotasAlumno = new List<Cuota>();
        }

      public void SetVista(ICobroCuotasVista vista)
   {
            this._vista = vista;
        }

        /// <summary>
        /// Busca una cuota por código de barras
      /// </summary>
        public void BuscarCuotaPorCodigoBarras(string codigoBarras)
        {
         try
            {
    if (string.IsNullOrWhiteSpace(codigoBarras))
          {
        _vista.MostrarError("Debe ingresar un código de barras válido.");
       return;
          }

     var cuota = _repositorio.BuscarCuotaPorCodigoBarras(codigoBarras);
 
    if (cuota == null)
    {
     _vista.MostrarError("No se encontró ninguna cuota con ese código de barras.");
return;
   }

                // Actualizar estado antes de mostrar
        cuota.ActualizarEstado();

 // Si ya está pagada, avisar
      if (cuota.Estado == EstadoCuota.Pagada)
           {
    // ?? Ya no podemos acceder a PagoCuota.FechaPago porque eliminamos la navegación
            _vista.MostrarAdvertencia($"La cuota {cuota.PeriodoDescripcion} ya fue pagada anteriormente.");
  return;
       }

       _cuotaSeleccionada = cuota;
          _cuotasAlumno = new List<Cuota> { cuota };
      
  _vista.MostrarAlumno(cuota.Alumno);
          _vista.MostrarCuotas(_cuotasAlumno);
     _vista.CalcularTotales(_cuotasAlumno);
            }
            catch (Exception ex)
            {
                _vista.MostrarError($"Error al buscar la cuota: {ex.Message}");
       }
        }

        /// <summary>
      /// Busca todas las cuotas de un alumno por DNI
/// </summary>
        public void BuscarCuotasPorDNI(int dni)
{
       try
   {
   if (dni <= 0)
       {
          _vista.MostrarError("Debe ingresar un DNI válido.");
          return;
     }

  var alumno = _repositorio.BuscarAlumnoPorDNI(dni);
            
       if (alumno == null)
         {
                    _vista.MostrarError($"No se encontró ningún alumno con DNI {dni}.");
  return;
                }

   _cuotasAlumno = _repositorio.BuscarCuotasPorDNI(dni);
         
  if (_cuotasAlumno == null || _cuotasAlumno.Count == 0)
       {
            _vista.MostrarAdvertencia($"El alumno {alumno.NombreCompleto} no tiene cuotas registradas.");
          _vista.MostrarAlumno(alumno);
   return;
   }

 // Actualizar estados de todas las cuotas
          foreach (var cuota in _cuotasAlumno)
    {
             cuota.ActualizarEstado();
   }

           _vista.MostrarAlumno(alumno);
        _vista.MostrarCuotas(_cuotasAlumno);
  
         // Mostrar solo cuotas pendientes por defecto
       var cuotasPendientes = _cuotasAlumno.Where(c => c.Estado != EstadoCuota.Pagada).ToList();
   _vista.CalcularTotales(cuotasPendientes);
          }
      catch (Exception ex)
         {
  _vista.MostrarError($"Error al buscar las cuotas: {ex.Message}");
      }
  }

      /// <summary>
     /// Registra el pago de las cuotas seleccionadas
        /// </summary>
      public void RegistrarPago(List<Cuota> cuotasSeleccionadas, double importeAbonado, string medioPago)
        {
            try
            {
          if (cuotasSeleccionadas == null || cuotasSeleccionadas.Count == 0)
 {
      _vista.MostrarError("Debe seleccionar al menos una cuota para pagar.");
                  return;
          }

         // Validar que ninguna cuota esté pagada
       var cuotasPagadas = cuotasSeleccionadas.Where(c => c.Estado == EstadoCuota.Pagada).ToList();
 if (cuotasPagadas.Any())
      {
          _vista.MostrarError($"Algunas cuotas seleccionadas ya fueron pagadas: {string.Join(", ", cuotasPagadas.Select(c => c.PeriodoDescripcion))}");
  return;
    }

 // Calcular total a pagar
        double totalAPagar = cuotasSeleccionadas.Sum(c => c.CalcularMontoAPagar());

          if (importeAbonado < totalAPagar)
          {
              _vista.MostrarError($"El importe abonado (${importeAbonado:N2}) es menor al total a pagar (${totalAPagar:N2}).");
        return;
          }

        // Registrar el pago de cada cuota
   foreach (var cuota in cuotasSeleccionadas)
             {
     double montoCuota = cuota.CalcularMontoAPagar();
            double recargo = montoCuota - cuota.MontoOriginal;
   
         var pago = new PagoCuota(montoCuota, recargo, medioPago);
        _repositorio.GuardarPagoCuota(cuota, pago);
 }

      double vuelto = importeAbonado - totalAPagar;
      _vista.MostrarExito($"Pago registrado con éxito. Vuelto: ${vuelto:N2}");
                _vista.LimpiarVista();
      }
      catch (Exception ex)
  {
     _vista.MostrarError($"Error al registrar el pago: {ex.Message}");
            }
   }

        /// <summary>
        /// Obtiene todas las cuotas pendientes del sistema
        /// </summary>
        public List<Cuota> GetCuotasPendientes()
      {
     try
       {
             return _repositorio.GetCuotasPendientes();
   }
         catch (Exception ex)
        {
                _vista.MostrarError($"Error al obtener cuotas pendientes: {ex.Message}");
           return new List<Cuota>();
 }
        }

        /// <summary>
        /// Calcula el total de cuotas seleccionadas
        /// </summary>
        public double CalcularTotalCuotasSeleccionadas(List<Cuota> cuotas)
      {
            if (cuotas == null || cuotas.Count == 0)
      return 0;

            return cuotas.Sum(c => c.CalcularMontoAPagar());
        }
    }
}
