using System;
using System.Collections.Generic;
using System.Linq;
using TFI.Dominio;
using TFI.Dominio.Contratos;

namespace TFI.Test.Mocks
{
    public class MockRepositorio : IRepositorio
    {
        private List<Indumentaria> _indumentarias = new List<Indumentaria>();
        private List<Stock> _stocks = new List<Stock>();
    private List<Venta> _ventas = new List<Venta>();
        private List<Talle> _talles = new List<Talle>();
     private List<Cuota> _cuotas = new List<Cuota>();
        private List<Alumno> _alumnos = new List<Alumno>();
   private List<PagoCuota> _pagosCuotas = new List<PagoCuota>();
        private int _nextId = 1;
        private int _nextStockId = 1;
        private int _nextCuotaId = 1;
        private int _nextAlumnoId = 1;

    public bool CredencialesValidas { get; set; } = false;

        public MockRepositorio()
     {
            InicializarTalles();
        }

      private void InicializarTalles()
        {
     _talles = new List<Talle>
   {
           new Talle { Id = 1, Descripcion = "S" },
      new Talle { Id = 2, Descripcion = "M" },
      new Talle { Id = 3, Descripcion = "L" },
           new Talle { Id = 4, Descripcion = "XL" }
            };
        }

        public bool IniciarSesion(int legajo, string contraseña)
        {
return CredencialesValidas;
        }

        public List<Talle> GetTalles()
{
 return _talles;
        }

        public List<Indumentaria> GetIndumentarias()
     {
   return _indumentarias;
        }

        public Indumentaria BuscarIndumentaria(int codigo)
        {
            return _indumentarias.FirstOrDefault(i => i.Codigo == codigo);
        }

     public Indumentaria BuscarIndumentariaPorId(int id)
     {
   return _indumentarias.FirstOrDefault(i => i.Id == id);
 }

        public Stock BuscarStock(Indumentaria indumentaria, int talleId)
   {
       return _stocks.FirstOrDefault(s => s.Indumentaria.Id == indumentaria.Id && s.Talle.Id == talleId);
   }

    public void GuardarVenta(Venta venta)
        {
            venta.Id = _nextId++;
        _ventas.Add(venta);
  }

      public bool ExisteCodigoIndumentaria(int codigo, int? indumentariaIdExcluir = null)
        {
            return _indumentarias.Any(i => i.Codigo == codigo && (!indumentariaIdExcluir.HasValue || i.Id != indumentariaIdExcluir.Value));
        }

   public void GuardarIndumentaria(Indumentaria indumentaria)
        {
     indumentaria.Id = _nextId++;
   _indumentarias.Add(indumentaria);
        }

        public void ActualizarIndumentaria(Indumentaria indumentaria)
        {
            var existente = _indumentarias.FirstOrDefault(i => i.Id == indumentaria.Id);
            if (existente != null)
            {
                existente.Codigo = indumentaria.Codigo;
                existente.Descripcion = indumentaria.Descripcion;
      existente.Precio = indumentaria.Precio;
    }
        }

        public void EliminarIndumentaria(int id)
        {
      var indumentaria = _indumentarias.FirstOrDefault(i => i.Id == id);
        if (indumentaria != null)
            {
    _indumentarias.Remove(indumentaria);
    _stocks.RemoveAll(s => s.Indumentaria.Id == id);
  }
        }

        public List<Stock> GetStocksPorIndumentaria(int indumentariaId)
        {
   return _stocks.Where(s => s.Indumentaria.Id == indumentariaId).ToList();
        }

        public void GuardarStock(Stock stock)
        {
     stock.Id = _nextStockId++;
   _stocks.Add(stock);
        }

        public void ActualizarStock(Stock stock)
        {
   var existente = _stocks.FirstOrDefault(s => s.Id == stock.Id);
      if (existente != null)
   {
    existente.Cantidad = stock.Cantidad;
           existente.CantidadMaxima = stock.CantidadMaxima;
      existente.CantidadMinima = stock.CantidadMinima;
      }
        }

        public void CrearStockParaIndumentaria(Indumentaria indumentaria, int stockMinimo = 5, int stockMaximo = 50)
{
          foreach (var talle in _talles)
            {
  var stock = new Stock(stockMaximo, stockMinimo)
         {
     Indumentaria = indumentaria,
              Talle = talle,
        Cantidad = 0
   };
     GuardarStock(stock);
            }
        }

        public Cuota BuscarCuotaPorCodigoBarras(string codigoBarras)
  {
            return _cuotas.FirstOrDefault(c => c.CodigoBarras == codigoBarras);
        }

        public Alumno BuscarAlumnoPorDNI(int dni)
     {
            return _alumnos.FirstOrDefault(a => a.DNI == dni);
        }

        public List<Cuota> BuscarCuotasPorDNI(int dni)
        {
       var alumno = BuscarAlumnoPorDNI(dni);
            if (alumno == null) return new List<Cuota>();
            return _cuotas.Where(c => c.Alumno.Id == alumno.Id).ToList();
   }

     public void GuardarPagoCuota(Cuota cuota, PagoCuota pago)
        {
      cuota.Estado = EstadoCuota.Pagada;
     _pagosCuotas.Add(pago);
     }

   public List<Cuota> GetCuotasPendientes()
        {
          return _cuotas.Where(c => c.Estado != EstadoCuota.Pagada).ToList();
     }

        // Métodos auxiliares para testing
        public void AgregarIndumentaria(Indumentaria indumentaria)
        {
            indumentaria.Id = _nextId++;
            _indumentarias.Add(indumentaria);
        }

     public void AgregarStock(Stock stock)
        {
      stock.Id = _nextStockId++;
            _stocks.Add(stock);
        }

        public void AgregarCuota(Cuota cuota)
        {
   cuota.Id = _nextCuotaId++;
            _cuotas.Add(cuota);
    }

      public void AgregarAlumno(Alumno alumno)
        {
   alumno.Id = _nextAlumnoId++;
 _alumnos.Add(alumno);
}
    }
}
