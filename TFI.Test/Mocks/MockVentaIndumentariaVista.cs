using System.Collections.Generic;
using TFI.Dominio;
using TFI.Dominio.Interfaces;

namespace TFI.Test.Mocks
{
  public class MockVentaIndumentariaVista : IVentaIndumentariaVista
    {
        public List<Talle> TallesMostrados { get; private set; }
   public List<Indumentaria> IndumentariasMostradas { get; private set; }
  public string UltimoMensajeError { get; private set; }
        public Indumentaria IndumentariaMostrada { get; private set; }
  public Venta VentaActualizada { get; private set; }
        public string UltimoMensajeExito { get; private set; }
   public bool FueLimpiada { get; private set; }

        public void ListarTalles(List<Talle> talles)
        {
       TallesMostrados = talles;
  }

        public void ListarIndumentarias(List<Indumentaria> indumentarias)
  {
      IndumentariasMostradas = indumentarias;
     }

        public void MostrarError(string mensaje)
     {
            UltimoMensajeError = mensaje;
}

        public void MostrarIndumentaria(Indumentaria ind)
        {
     IndumentariaMostrada = ind;
        }

   public void ActualizarVista(Venta venta)
        {
    VentaActualizada = venta;
        }

 public void MostrarExito(string mensaje)
   {
            UltimoMensajeExito = mensaje;
    }

  public void LimpiarVentana()
        {
   FueLimpiada = true;
        }
    }
}
