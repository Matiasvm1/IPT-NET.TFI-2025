using TFI.Dominio.Interfaces;

namespace TFI.Test.Mocks
{
    public class MockLoginVista : ILoginVista
    {
        public string UltimoMensajeError { get; private set; }
      public bool FueOcultada { get; private set; }
      public bool FueMostrada { get; private set; }

        public void MostrarError(string mensaje)
        {
       UltimoMensajeError = mensaje;
        }

        public void Ocultar()
    {
FueOcultada = true;
        }

        public void Mostrar()
        {
     FueMostrada = true;
        }
    }
}
