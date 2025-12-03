using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TFI.Dominio;
using TFI.Test.Mocks;
using TFI.Vista.Presentadores;

namespace TFI.Test
{
    [TestClass]
    public class TestVentaIndumentariaPresentador
    {
        private MockRepositorio _repositorio;
     private MockVentaIndumentariaVista _vista;
        private VentaIndumentariaPresentador _presentador;

        [TestInitialize]
      public void Setup()
        {
      _repositorio = new MockRepositorio();
            _vista = new MockVentaIndumentariaVista();
       _presentador = new VentaIndumentariaPresentador(_repositorio);
            _presentador.SetVista(_vista);
        }

        [TestMethod]
        public void CrearNuevaVenta()
        {
            var venta = _presentador.CrearNuevaVenta();

            Assert.IsNotNull(venta);
     Assert.AreEqual(0, venta.LineaDeVentas.Count);
 }

     [TestMethod]
        public void IngresarIndumentariaCodigoValido()
  {
        var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
      _repositorio.AgregarIndumentaria(indumentaria);

         _presentador.IngresarIndumentaria(100);

            Assert.IsNotNull(_vista.IndumentariaMostrada);
            Assert.AreEqual("Remera", _vista.IndumentariaMostrada.Descripcion);
        }

        [TestMethod]
        public void IngresarIndumentariaCodigoInvalido()
        {
   _presentador.IngresarIndumentaria(999);

            Assert.AreEqual("Indumentaria inexistente", _vista.UltimoMensajeError);
        }

        [TestMethod]
        public void RegistrarLineaDeVentaExitosa()
        {
            var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
      _repositorio.AgregarIndumentaria(indumentaria);
        
var talle = _repositorio.GetTalles()[0];
         var stock = new Stock(100, 10) { Indumentaria = indumentaria, Talle = talle, Cantidad = 50 };
     _repositorio.AgregarStock(stock);

   var venta = _presentador.CrearNuevaVenta();
            _presentador.RegistrarLineaDeVenta(indumentaria, 5, talle.Id);

            Assert.IsNotNull(_vista.VentaActualizada);
            Assert.AreEqual(1, venta.LineaDeVentas.Count);
        }

[TestMethod]
        public void RegistrarLineaDeVentaSinStockSuficiente()
        {
    var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
  _repositorio.AgregarIndumentaria(indumentaria);
       
            var talle = _repositorio.GetTalles()[0];
var stock = new Stock(100, 10) { Indumentaria = indumentaria, Talle = talle, Cantidad = 5 };
          _repositorio.AgregarStock(stock);

            var venta = _presentador.CrearNuevaVenta();
            _presentador.RegistrarLineaDeVenta(indumentaria, 10, talle.Id);

        Assert.IsNotNull(_vista.UltimoMensajeError);
    }

        [TestMethod]
        public void RegistrarLineaDeVentaStockNull()
  {
      var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
      _repositorio.AgregarIndumentaria(indumentaria);

   var venta = _presentador.CrearNuevaVenta();
          _presentador.RegistrarLineaDeVenta(indumentaria, 5, 99);

            Assert.IsNotNull(_vista.UltimoMensajeError);
  Assert.IsTrue(_vista.UltimoMensajeError.Contains("No hay stock disponible"));
 }

        [TestMethod]
    public void IngresarImporteSuficiente()
        {
            var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 300 };
            _repositorio.AgregarIndumentaria(indumentaria);

            var venta = _presentador.CrearNuevaVenta();
            venta.AgregarLineaDeVenta(indumentaria, 5);

            var vuelto = _presentador.IngresarImporte(2000);

            Assert.AreEqual(500, vuelto);
        }

        [TestMethod]
        public void IngresarImporteInsuficiente()
        {
     var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 300 };
            _repositorio.AgregarIndumentaria(indumentaria);

var venta = _presentador.CrearNuevaVenta();
            venta.AgregarLineaDeVenta(indumentaria, 5);

     var vuelto = _presentador.IngresarImporte(500);

     Assert.IsNotNull(_vista.UltimoMensajeError);
       Assert.AreEqual(0, vuelto);
        }

     [TestMethod]
        public void IngresarImporteNegativo()
        {
var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 300 };
          _repositorio.AgregarIndumentaria(indumentaria);

         var venta = _presentador.CrearNuevaVenta();
            venta.AgregarLineaDeVenta(indumentaria, 5);

            var vuelto = _presentador.IngresarImporte(-100);

 Assert.IsNotNull(_vista.UltimoMensajeError);
    }

        [TestMethod]
  public void ConfirmarVentaExitosa()
        {
 var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 300 };
     _repositorio.AgregarIndumentaria(indumentaria);

         var venta = _presentador.CrearNuevaVenta();
         venta.AgregarLineaDeVenta(indumentaria, 5);

          _presentador.ConfirmarVenta();

    Assert.AreEqual("Venta Realizada con Exito", _vista.UltimoMensajeExito);
            Assert.IsTrue(_vista.FueLimpiada);
        }

        [TestMethod]
      public void ValidarVentaSinLineas()
        {
        var venta = _presentador.CrearNuevaVenta();

       _presentador.ConfirmarVenta();

        Assert.IsNotNull(_vista.UltimoMensajeError);
     Assert.IsTrue(_vista.UltimoMensajeError.Contains("al menos un producto"));
        }

  [TestMethod]
      public void EliminarLineaDeVenta()
     {
   var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 300 };
            _repositorio.AgregarIndumentaria(indumentaria);

      var venta = _presentador.CrearNuevaVenta();
            venta.AgregarLineaDeVenta(indumentaria, 5);
         venta.AgregarLineaDeVenta(indumentaria, 3);

_presentador.EliminarLineaDeVenta(0);

     Assert.AreEqual(1, venta.LineaDeVentas.Count);
   Assert.IsNotNull(_vista.VentaActualizada);
        }

   [TestMethod]
        public void GetTalles()
        {
       var talles = _presentador.GetTalles();

       Assert.IsNotNull(talles);
    Assert.IsTrue(talles.Count > 0);
        }

        [TestMethod]
      public void GetIndumentarias()
     {
         var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 300 };
    _repositorio.AgregarIndumentaria(indumentaria);

       var indumentarias = _presentador.GetIndumentarias();

        Assert.IsNotNull(indumentarias);
       Assert.AreEqual(1, indumentarias.Count);
        }
    }
}
