using System.Collections.Generic;
using TFI.Dominio;
using TFI.Dominio.Interfaces;

namespace TFI.Test.Mocks
{
    public class MockCobroCuotasVista : ICobroCuotasVista
    {
        public Alumno AlumnoMostrado { get; private set; }
 public List<Cuota> CuotasMostradas { get; private set; }
        public List<Cuota> CuotasCalculadas { get; private set; }
        public string UltimoMensajeError { get; private set; }
     public string UltimoMensajeAdvertencia { get; private set; }
        public string UltimoMensajeExito { get; private set; }
      public bool FueLimpiada { get; private set; }

  public void MostrarAlumno(Alumno alumno)
        {
            AlumnoMostrado = alumno;
        }

        public void MostrarCuotas(List<Cuota> cuotas)
        {
      CuotasMostradas = cuotas;
     }

  public void CalcularTotales(List<Cuota> cuotas)
        {
 CuotasCalculadas = cuotas;
     }

      public void MostrarError(string mensaje)
        {
     UltimoMensajeError = mensaje;
     }

        public void MostrarAdvertencia(string mensaje)
        {
 UltimoMensajeAdvertencia = mensaje;
  }

    public void MostrarExito(string mensaje)
        {
          UltimoMensajeExito = mensaje;
        }

   public void LimpiarVista()
        {
   FueLimpiada = true;
        }
  }
}
