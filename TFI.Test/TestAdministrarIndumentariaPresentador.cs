using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TFI.Dominio;
using TFI.Test.Mocks;
using TFI.Vista.Presentadores;

namespace TFI.Test
{
    [TestClass]
    public class TestAdministrarIndumentariaPresentador
    {
   private MockRepositorio _repositorio;
 private MockAdministrarIndumentariaVista _vista;
        private AdministrarIndumentariaPresentador _presentador;

        [TestInitialize]
        public void Setup()
  {
      _repositorio = new MockRepositorio();
   _vista = new MockAdministrarIndumentariaVista();
   _presentador = new AdministrarIndumentariaPresentador(_repositorio);
     _presentador.SetVista(_vista);
        }

   [TestMethod]
     public void GetTalles()
    {
      var talles = _presentador.GetTalles();

            Assert.IsNotNull(talles);
   Assert.IsTrue(talles.Count > 0);
  }

   [TestMethod]
        public void CargarIndumentariasExitosa()
  {
       var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
_repositorio.AgregarIndumentaria(indumentaria);

      _presentador.CargarIndumentarias();

    Assert.IsNotNull(_vista.IndumentariasMostradas);
     Assert.AreEqual(1, _vista.IndumentariasMostradas.Count);
   }

   [TestMethod]
        public void BuscarIndumentariasPorDescripcion()
     {
   var ind1 = new Indumentaria { Codigo = 100, Descripcion = "Remera Azul", Precio = 500 };
    var ind2 = new Indumentaria { Codigo = 101, Descripcion = "Pantalon Azul", Precio = 800 };
   var ind3 = new Indumentaria { Codigo = 102, Descripcion = "Remera Roja", Precio = 600 };
       
       _repositorio.AgregarIndumentaria(ind1);
            _repositorio.AgregarIndumentaria(ind2);
       _repositorio.AgregarIndumentaria(ind3);

     _presentador.BuscarIndumentarias("Azul");

       Assert.AreEqual(2, _vista.IndumentariasMostradas.Count);
  }

        [TestMethod]
        public void BuscarIndumentariasPorCodigo()
        {
       var ind1 = new Indumentaria { Codigo = 100, Descripcion = "Remera Azul", Precio = 500 };
var ind2 = new Indumentaria { Codigo = 101, Descripcion = "Pantalon Azul", Precio = 800 };
     
      _repositorio.AgregarIndumentaria(ind1);
       _repositorio.AgregarIndumentaria(ind2);

       _presentador.BuscarIndumentarias("100");

Assert.AreEqual(1, _vista.IndumentariasMostradas.Count);
   Assert.AreEqual(100, _vista.IndumentariasMostradas[0].Codigo);
  }

   [TestMethod]
        public void BuscarIndumentariasSinResultados()
      {
  var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
            _repositorio.AgregarIndumentaria(indumentaria);

  _presentador.BuscarIndumentarias("NoExiste");

       Assert.AreEqual(0, _vista.IndumentariasMostradas.Count);
   Assert.IsNotNull(_vista.UltimoMensajeError);
        }

   [TestMethod]
  public void BuscarIndumentariasCriterioVacio()
   {
   var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
      _repositorio.AgregarIndumentaria(indumentaria);

          _presentador.BuscarIndumentarias("");

     Assert.AreEqual(1, _vista.IndumentariasMostradas.Count);
        }

      [TestMethod]
   public void NuevaIndumentaria()
  {
     _presentador.NuevaIndumentaria();

   Assert.IsTrue(_vista.FueLimpiada);
        }

[TestMethod]
public void GuardarIndumentariaNuevaExitosa()
  {
     _presentador.GuardarIndumentaria(100, "Remera", 500, 10, 100, null);

       Assert.IsNotNull(_vista.UltimoMensajeExito);
   Assert.IsTrue(_vista.UltimoMensajeExito.Contains("creada exitosamente"));
Assert.IsTrue(_vista.FueLimpiada);
 }

   [TestMethod]
        public void GuardarIndumentariaNuevaCodigoExistente()
   {
     var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera Vieja", Precio = 500 };
   _repositorio.AgregarIndumentaria(indumentaria);

       _presentador.GuardarIndumentaria(100, "Remera Nueva", 600, 10, 100, null);

Assert.IsNotNull(_vista.UltimoMensajeError);
       Assert.IsTrue(_vista.UltimoMensajeError.Contains("Ya existe"));
        }

        [TestMethod]
        public void GuardarIndumentariaCodigoCero()
        {
   _presentador.GuardarIndumentaria(0, "Remera", 500, 10, 100, null);

   Assert.IsNotNull(_vista.UltimoMensajeError);
     Assert.IsTrue(_vista.UltimoMensajeError.Contains("código debe ser un número mayor a 0"));
        }

  [TestMethod]
        public void GuardarIndumentariaCodigoNegativo()
        {
_presentador.GuardarIndumentaria(-1, "Remera", 500, 10, 100, null);

       Assert.IsNotNull(_vista.UltimoMensajeError);
 }

        [TestMethod]
   public void GuardarIndumentariaDescripcionVacia()
    {
   _presentador.GuardarIndumentaria(100, "", 500, 10, 100, null);

       Assert.IsNotNull(_vista.UltimoMensajeError);
Assert.IsTrue(_vista.UltimoMensajeError.Contains("descripción es obligatoria"));
   }

        [TestMethod]
 public void GuardarIndumentariaDescripcionLarga()
        {
var descripcionLarga = new string('A', 201);
      _presentador.GuardarIndumentaria(100, descripcionLarga, 500, 10, 100, null);

   Assert.IsNotNull(_vista.UltimoMensajeError);
       Assert.IsTrue(_vista.UltimoMensajeError.Contains("no puede superar los 200 caracteres"));
    }

        [TestMethod]
        public void GuardarIndumentariaPrecioCero()
  {
_presentador.GuardarIndumentaria(100, "Remera", 0, 10, 100, null);

       Assert.IsNotNull(_vista.UltimoMensajeError);
    Assert.IsTrue(_vista.UltimoMensajeError.Contains("precio debe ser mayor a 0"));
  }

      [TestMethod]
   public void GuardarIndumentariaPrecioNegativo()
    {
       _presentador.GuardarIndumentaria(100, "Remera", -500, 10, 100, null);

   Assert.IsNotNull(_vista.UltimoMensajeError);
      }

[TestMethod]
        public void GuardarIndumentariaStockMinimoNegativo()
        {
      _presentador.GuardarIndumentaria(100, "Remera", 500, -1, 100, null);

   Assert.IsNotNull(_vista.UltimoMensajeError);
      Assert.IsTrue(_vista.UltimoMensajeError.Contains("stock mínimo no puede ser negativo"));
        }

     [TestMethod]
  public void GuardarIndumentariaStockMaximoCero()
  {
       _presentador.GuardarIndumentaria(100, "Remera", 500, 10, 0, null);

            Assert.IsNotNull(_vista.UltimoMensajeError);
    Assert.IsTrue(_vista.UltimoMensajeError.Contains("stock máximo debe ser mayor a 0"));
   }

        [TestMethod]
        public void GuardarIndumentariaStockMinimoMayorQueMaximo()
   {
   _presentador.GuardarIndumentaria(100, "Remera", 500, 100, 10, null);

     Assert.IsNotNull(_vista.UltimoMensajeError);
 Assert.IsTrue(_vista.UltimoMensajeError.Contains("stock mínimo debe ser menor al stock máximo"));
        }

  [TestMethod]
    public void GuardarIndumentariaConStocksPorTalle()
  {
    var stocksPorTalle = new Dictionary<string, int>
{
      { "S", 10 },
     { "M", 20 },
   { "L", 15 }
   };

   _presentador.GuardarIndumentaria(100, "Remera", 500, 10, 100, stocksPorTalle);

   Assert.IsNotNull(_vista.UltimoMensajeExito);
        }

   [TestMethod]
        public void SeleccionarIndumentariaExitosa()
    {
            var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
            _repositorio.AgregarIndumentaria(indumentaria);

   _presentador.SeleccionarIndumentaria(indumentaria.Id);

   Assert.IsNotNull(_vista.IndumentariaCargada);
     Assert.AreEqual("Remera", _vista.IndumentariaCargada.Descripcion);
        }

    [TestMethod]
        public void SeleccionarIndumentariaNoExistente()
        {
       _presentador.SeleccionarIndumentaria(999);

       Assert.IsNotNull(_vista.UltimoMensajeError);
    Assert.IsTrue(_vista.UltimoMensajeError.Contains("No se encontró"));
  }

[TestMethod]
  public void ModificarIndumentariaExitosa()
        {
  var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera Vieja", Precio = 500 };
          _repositorio.AgregarIndumentaria(indumentaria);

            _presentador.SeleccionarIndumentaria(indumentaria.Id);
   _presentador.GuardarIndumentaria(100, "Remera Nueva", 600, 10, 100, null);

   Assert.IsNotNull(_vista.UltimoMensajeExito);
     Assert.IsTrue(_vista.UltimoMensajeExito.Contains("actualizada exitosamente"));
   }

        [TestMethod]
      public void ModificarIndumentariaCodigoExistente()
        {
          var ind1 = new Indumentaria { Codigo = 100, Descripcion = "Remera 1", Precio = 500 };
     var ind2 = new Indumentaria { Codigo = 101, Descripcion = "Remera 2", Precio = 600 };
            _repositorio.AgregarIndumentaria(ind1);
          _repositorio.AgregarIndumentaria(ind2);

   _presentador.SeleccionarIndumentaria(ind2.Id);
         _presentador.GuardarIndumentaria(100, "Remera 2 Modificada", 700, 10, 100, null);

  Assert.IsNotNull(_vista.UltimoMensajeError);
  Assert.IsTrue(_vista.UltimoMensajeError.Contains("Ya existe otra indumentaria"));
        }

        [TestMethod]
     public void EliminarIndumentariaExitosa()
  {
var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
       _repositorio.AgregarIndumentaria(indumentaria);
       _vista.RespuestaConfirmacion = true;

            _presentador.EliminarIndumentaria(indumentaria.Id);

 Assert.IsNotNull(_vista.UltimoMensajeExito);
   Assert.IsTrue(_vista.UltimoMensajeExito.Contains("eliminada exitosamente"));
            Assert.IsTrue(_vista.FueLimpiada);
        }

     [TestMethod]
        public void EliminarIndumentariaCancelada()
        {
            var indumentaria = new Indumentaria { Codigo = 100, Descripcion = "Remera", Precio = 500 };
    _repositorio.AgregarIndumentaria(indumentaria);
       _vista.RespuestaConfirmacion = false;

   _presentador.EliminarIndumentaria(indumentaria.Id);

     Assert.IsNull(_vista.UltimoMensajeExito);
     Assert.AreEqual(1, _repositorio.GetIndumentarias().Count);
  }

  [TestMethod]
        public void EliminarIndumentariaNoExistente()
    {
  _vista.RespuestaConfirmacion = true;
       _presentador.EliminarIndumentaria(999);

     Assert.IsNotNull(_vista.UltimoMensajeError);
    }
    }
}
