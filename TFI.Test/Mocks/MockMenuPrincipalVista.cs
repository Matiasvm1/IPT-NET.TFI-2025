using TFI.Dominio.Interfaces;

namespace TFI.Test.Mocks
{
    public class MockMenuPrincipalVista : IMenuPrincipalVista
    {
        public bool VentaIndumentariaMostrada { get; private set; }
        public bool CobroCuotasMostrada { get; private set; }
        public bool AdministrarIndumentariaMostrada { get; private set; }

        public void MostrarVentaIndumentaria()
        {
            VentaIndumentariaMostrada = true;
        }

        public void MostrarCobroCuotas()
        {
            CobroCuotasMostrada = true;
        }

        public void MostrarAdministrarIndumentaria()
        {
            AdministrarIndumentariaMostrada = true;
        }
    }
}
