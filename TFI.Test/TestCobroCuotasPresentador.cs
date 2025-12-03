using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TFI.Dominio;
using TFI.Test.Mocks;
using TFI.Vista.Presentadores;

namespace TFI.Test
{
    [TestClass]
    public class TestCobroCuotasPresentador
    {
    private MockRepositorio _repositorio;
   private MockCobroCuotasVista _vista;
        private CobroCuotasPresentador _presentador;

     [TestInitialize]
  public void Setup()
        {
    _repositorio = new MockRepositorio();
       _vista = new MockCobroCuotasVista();
 _presentador = new CobroCuotasPresentador(_repositorio);
      _presentador.SetVista(_vista);
        }

      [TestMethod]
        public void BuscarCuotaPorCodigoBarrasExitosa()
    {
      var alumno = new Alumno { DNI = 12345678, Nombre = "Juan", Apellido = "Perez" };
        _repositorio.AgregarAlumno(alumno);

      var cuota = new Cuota
{
         CodigoBarras = "123456",
  Alumno = alumno,
      MontoOriginal = 1000,
            Estado = EstadoCuota.Pendiente,
     PrimerVencimiento = DateTime.Now.AddDays(10),
    SegundoVencimiento = DateTime.Now.AddDays(20),
    TercerVencimiento = DateTime.Now.AddDays(30)
            };
    _repositorio.AgregarCuota(cuota);

            _presentador.BuscarCuotaPorCodigoBarras("123456");

     Assert.IsNotNull(_vista.AlumnoMostrado);
   Assert.AreEqual("Juan", _vista.AlumnoMostrado.Nombre);
    Assert.IsNotNull(_vista.CuotasMostradas);
        }

[TestMethod]
        public void BuscarCuotaPorCodigoBarrasVacio()
  {
          _presentador.BuscarCuotaPorCodigoBarras("");

       Assert.IsNotNull(_vista.UltimoMensajeError);
      Assert.IsTrue(_vista.UltimoMensajeError.Contains("código de barras válido"));
        }

   [TestMethod]
        public void BuscarCuotaPorCodigoBarrasNoEncontrada()
  {
_presentador.BuscarCuotaPorCodigoBarras("999999");

       Assert.IsNotNull(_vista.UltimoMensajeError);
         Assert.IsTrue(_vista.UltimoMensajeError.Contains("No se encontró"));
        }

   [TestMethod]
   public void BuscarCuotaPorCodigoBarrasCuotaPagada()
        {
      var alumno = new Alumno { DNI = 12345678, Nombre = "Juan", Apellido = "Perez" };
        _repositorio.AgregarAlumno(alumno);

            var cuota = new Cuota
{
   CodigoBarras = "123456",
    Alumno = alumno,
   MontoOriginal = 1000,
            Estado = EstadoCuota.Pagada,
     PrimerVencimiento = DateTime.Now.AddDays(-30),
          SegundoVencimiento = DateTime.Now.AddDays(-20),
     TercerVencimiento = DateTime.Now.AddDays(-10)
         };
      _repositorio.AgregarCuota(cuota);

   _presentador.BuscarCuotaPorCodigoBarras("123456");

     Assert.IsNotNull(_vista.UltimoMensajeAdvertencia);
      Assert.IsTrue(_vista.UltimoMensajeAdvertencia.Contains("ya fue pagada"));
        }

 [TestMethod]
        public void BuscarCuotasPorDNIExitosa()
        {
      var alumno = new Alumno { DNI = 12345678, Nombre = "Juan", Apellido = "Perez" };
   _repositorio.AgregarAlumno(alumno);

    var cuota = new Cuota
  {
    CodigoBarras = "123456",
     Alumno = alumno,
      MontoOriginal = 1000,
 Estado = EstadoCuota.Pendiente,
    PrimerVencimiento = DateTime.Now.AddDays(10),
   SegundoVencimiento = DateTime.Now.AddDays(20),
     TercerVencimiento = DateTime.Now.AddDays(30)
     };
   _repositorio.AgregarCuota(cuota);

  _presentador.BuscarCuotasPorDNI(12345678);

        Assert.IsNotNull(_vista.AlumnoMostrado);
     Assert.AreEqual("Juan", _vista.AlumnoMostrado.Nombre);
        Assert.IsNotNull(_vista.CuotasMostradas);
        }

 [TestMethod]
   public void BuscarCuotasPorDNIInvalido()
        {
   _presentador.BuscarCuotasPorDNI(0);

 Assert.IsNotNull(_vista.UltimoMensajeError);
            Assert.IsTrue(_vista.UltimoMensajeError.Contains("DNI válido"));
        }

  [TestMethod]
        public void BuscarCuotasPorDNINegativo()
   {
   _presentador.BuscarCuotasPorDNI(-1);

     Assert.IsNotNull(_vista.UltimoMensajeError);
   }

  [TestMethod]
     public void BuscarCuotasPorDNIAlumnoNoEncontrado()
        {
       _presentador.BuscarCuotasPorDNI(99999999);

       Assert.IsNotNull(_vista.UltimoMensajeError);
       Assert.IsTrue(_vista.UltimoMensajeError.Contains("No se encontró ningún alumno"));
     }

  [TestMethod]
      public void BuscarCuotasPorDNISinCuotas()
        {
            var alumno = new Alumno { DNI = 12345678, Nombre = "Juan", Apellido = "Perez" };
   _repositorio.AgregarAlumno(alumno);

            _presentador.BuscarCuotasPorDNI(12345678);

     Assert.IsNotNull(_vista.UltimoMensajeAdvertencia);
          Assert.IsTrue(_vista.UltimoMensajeAdvertencia.Contains("no tiene cuotas registradas"));
  }

        [TestMethod]
        public void RegistrarPagoExitoso()
     {
   var alumno = new Alumno { DNI = 12345678, Nombre = "Juan", Apellido = "Perez" };
    _repositorio.AgregarAlumno(alumno);

       var cuota = new Cuota
  {
      CodigoBarras = "123456",
  Alumno = alumno,
    MontoOriginal = 1000,
   Estado = EstadoCuota.Pendiente,
   PrimerVencimiento = DateTime.Now.AddDays(10),
  SegundoVencimiento = DateTime.Now.AddDays(20),
         TercerVencimiento = DateTime.Now.AddDays(30)
       };
     _repositorio.AgregarCuota(cuota);

   var cuotasSeleccionadas = new List<Cuota> { cuota };
     _presentador.RegistrarPago(cuotasSeleccionadas, 1500, "Efectivo");

 Assert.IsNotNull(_vista.UltimoMensajeExito);
            Assert.IsTrue(_vista.UltimoMensajeExito.Contains("Pago registrado con éxito"));
   Assert.IsTrue(_vista.FueLimpiada);
    }

 [TestMethod]
  public void RegistrarPagoSinCuotasSeleccionadas()
        {
   _presentador.RegistrarPago(new List<Cuota>(), 1000, "Efectivo");

    Assert.IsNotNull(_vista.UltimoMensajeError);
     Assert.IsTrue(_vista.UltimoMensajeError.Contains("al menos una cuota"));
  }

 [TestMethod]
  public void RegistrarPagoCuotasYaPagadas()
     {
    var alumno = new Alumno { DNI = 12345678, Nombre = "Juan", Apellido = "Perez" };
   _repositorio.AgregarAlumno(alumno);

          var cuota = new Cuota
    {
     CodigoBarras = "123456",
 Alumno = alumno,
    MontoOriginal = 1000,
  Estado = EstadoCuota.Pagada,
       PrimerVencimiento = DateTime.Now.AddDays(-30),
     SegundoVencimiento = DateTime.Now.AddDays(-20),
      TercerVencimiento = DateTime.Now.AddDays(-10)
  };
 _repositorio.AgregarCuota(cuota);

       var cuotasSeleccionadas = new List<Cuota> { cuota };
_presentador.RegistrarPago(cuotasSeleccionadas, 1500, "Efectivo");

      Assert.IsNotNull(_vista.UltimoMensajeError);
     Assert.IsTrue(_vista.UltimoMensajeError.Contains("ya fueron pagadas"));
 }

      [TestMethod]
        public void RegistrarPagoImporteInsuficiente()
  {
    var alumno = new Alumno { DNI = 12345678, Nombre = "Juan", Apellido = "Perez" };
     _repositorio.AgregarAlumno(alumno);

     var cuota = new Cuota
            {
     CodigoBarras = "123456",
       Alumno = alumno,
       MontoOriginal = 1000,
       Estado = EstadoCuota.Pendiente,
    PrimerVencimiento = DateTime.Now.AddDays(10),
   SegundoVencimiento = DateTime.Now.AddDays(20),
         TercerVencimiento = DateTime.Now.AddDays(30)
  };
  _repositorio.AgregarCuota(cuota);

     var cuotasSeleccionadas = new List<Cuota> { cuota };
      _presentador.RegistrarPago(cuotasSeleccionadas, 500, "Efectivo");

 Assert.IsNotNull(_vista.UltimoMensajeError);
 Assert.IsTrue(_vista.UltimoMensajeError.Contains("menor al total a pagar"));
 }

        [TestMethod]
   public void CalcularTotalCuotasSeleccionadas()
        {
   var alumno = new Alumno { DNI = 12345678, Nombre = "Juan", Apellido = "Perez" };
          _repositorio.AgregarAlumno(alumno);

        var cuota1 = new Cuota
        {
 CodigoBarras = "123456",
     Alumno = alumno,
        MontoOriginal = 1000,
   Estado = EstadoCuota.Pendiente,
    PrimerVencimiento = DateTime.Now.AddDays(10),
   SegundoVencimiento = DateTime.Now.AddDays(20),
TercerVencimiento = DateTime.Now.AddDays(30)
     };

     var cuota2 = new Cuota
     {
   CodigoBarras = "789012",
       Alumno = alumno,
       MontoOriginal = 1000,
    Estado = EstadoCuota.Pendiente,
     PrimerVencimiento = DateTime.Now.AddDays(10),
     SegundoVencimiento = DateTime.Now.AddDays(20),
   TercerVencimiento = DateTime.Now.AddDays(30)
         };

var cuotas = new List<Cuota> { cuota1, cuota2 };
            var total = _presentador.CalcularTotalCuotasSeleccionadas(cuotas);

       Assert.AreEqual(2000, total);
   }

    [TestMethod]
   public void CalcularTotalCuotasVacio()
      {
         var total = _presentador.CalcularTotalCuotasSeleccionadas(new List<Cuota>());

   Assert.AreEqual(0, total);
        }

    [TestMethod]
        public void GetCuotasPendientes()
  {
       var alumno = new Alumno { DNI = 12345678, Nombre = "Juan", Apellido = "Perez" };
            _repositorio.AgregarAlumno(alumno);

   var cuotaPendiente = new Cuota
{
   CodigoBarras = "123456",
       Alumno = alumno,
   MontoOriginal = 1000,
      Estado = EstadoCuota.Pendiente,
    PrimerVencimiento = DateTime.Now.AddDays(10),
  SegundoVencimiento = DateTime.Now.AddDays(20),
         TercerVencimiento = DateTime.Now.AddDays(30)
    };

    var cuotaPagada = new Cuota
            {
          CodigoBarras = "789012",
  Alumno = alumno,
 MontoOriginal = 1000,
    Estado = EstadoCuota.Pagada,
    PrimerVencimiento = DateTime.Now.AddDays(-30),
     SegundoVencimiento = DateTime.Now.AddDays(-20),
      TercerVencimiento = DateTime.Now.AddDays(-10)
    };

            _repositorio.AgregarCuota(cuotaPendiente);
       _repositorio.AgregarCuota(cuotaPagada);

   var pendientes = _presentador.GetCuotasPendientes();

   Assert.AreEqual(1, pendientes.Count);
 Assert.AreEqual(EstadoCuota.Pendiente, pendientes[0].Estado);
        }
    }
}
