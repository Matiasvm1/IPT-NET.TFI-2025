using System.Collections.Generic;
using TFI.Dominio;
using TFI.Dominio.Interfaces;

namespace TFI.Test.Mocks
{
    public class MockAdministrarIndumentariaVista : IAdministrarIndumentariaVista
{
   public List<Indumentaria> IndumentariasMostradas { get; private set; }
  public List<Stock> StocksMostrados { get; private set; }
        public string UltimoMensajeError { get; private set; }
        public string UltimoMensajeExito { get; private set; }
   public bool FueLimpiada { get; private set; }
        public Indumentaria IndumentariaCargada { get; private set; }
      public string UltimoMensajeConfirmacion { get; private set; }
      public bool RespuestaConfirmacion { get; set; } = true;

  public void MostrarIndumentarias(List<Indumentaria> indumentarias)
      {
     IndumentariasMostradas = indumentarias;
   }

   public void MostrarStocks(List<Stock> stocks)
        {
   StocksMostrados = stocks;
     }

        public void MostrarError(string mensaje)
  {
          UltimoMensajeError = mensaje;
  }

    public void MostrarExito(string mensaje)
        {
            UltimoMensajeExito = mensaje;
 }

    public void LimpiarFormulario()
   {
  FueLimpiada = true;
   }

     public void CargarIndumentariaEnFormulario(Indumentaria indumentaria)
    {
     IndumentariaCargada = indumentaria;
     }

        public bool ConfirmarEliminacion(string mensaje)
        {
UltimoMensajeConfirmacion = mensaje;
            return RespuestaConfirmacion;
        }
    }
}
