using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFI.Test.Mocks;
using TFI.Vista.Presentadores;

namespace TFI.Test
{
    [TestClass]
    public class TestMenuPrincipalPresentador
    {
        private MockRepositorio _repositorio;
   private MockMenuPrincipalVista _vista;
     private MenuPrincipalPresentador _presentador;

        [TestInitialize]
   public void Setup()
  {
      _repositorio = new MockRepositorio();
  _vista = new MockMenuPrincipalVista();
  _presentador = new MenuPrincipalPresentador(_repositorio);
      _presentador.SetVista(_vista);
        }

        [TestMethod]
        public void SetVista()
        {
  _presentador.SetVista(_vista);
 
   Assert.IsTrue(true);
        }
    }
}
