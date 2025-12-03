using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFI.Test.Mocks;
using TFI.Vista.Presentadores;

namespace TFI.Test
{
    [TestClass]
public class TestLoginPresentador
    {
        private MockRepositorio _repositorio;
  private MockLoginVista _vista;
        private LoginPresentador _presentador;

        [TestInitialize]
   public void Setup()
     {
        _repositorio = new MockRepositorio();
        _vista = new MockLoginVista();
_presentador = new LoginPresentador(_repositorio);
   _presentador.SetVista(_vista);
  }

 [TestMethod]
public void IngresarDatosCredencialesCorrectas()
      {
   _repositorio.CredencialesValidas = true;

  var resultado = _presentador.IngresarDatos(12345, "password123");

       Assert.IsTrue(resultado);
 }

        [TestMethod]
        public void IngresarDatosCredencialesIncorrectas()
        {
  _repositorio.CredencialesValidas = false;

      var resultado = _presentador.IngresarDatos(12345, "wrongpassword");

   Assert.IsFalse(resultado);
   Assert.AreEqual("Usuario incorrecto", _vista.UltimoMensajeError);
        }

        [TestMethod]
     public void IngresarDatosLegajoCero()
 {
            _repositorio.CredencialesValidas = false;

      var resultado = _presentador.IngresarDatos(0, "password");

   Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void IngresarDatosLegajoNegativo()
        {
  _repositorio.CredencialesValidas = false;

     var resultado = _presentador.IngresarDatos(-1, "password");

    Assert.IsFalse(resultado);
        }
    }
}
