using System;
using System.Collections.Generic;
using TFI.Dominio;

namespace TFI.Dominio.Interfaces
{
    /// <summary>
    /// Contrato para la vista de Cobro de Cuotas
    /// Sigue la misma estructura que IVentaIndumentariaVista
    /// </summary>
    public interface ICobroCuotasVista
    {
      void MostrarAlumno(Alumno alumno);
        void MostrarCuotas(List<Cuota> cuotas);
        void CalcularTotales(List<Cuota> cuotas);
   void MostrarError(string mensaje);
void MostrarAdvertencia(string mensaje);
        void MostrarExito(string mensaje);
        void LimpiarVista();
    }
}
