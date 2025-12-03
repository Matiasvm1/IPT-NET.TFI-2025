using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Dominio.Contratos
{
    public interface IRepositorio
    {
        // Métodos existentes de Venta de Indumentaria
        bool IniciarSesion(int legajo, string contraseña);
        List<Talle> GetTalles();
        void GuardarVenta(Venta venta);
        Indumentaria BuscarIndumentaria(int codigo);
        Stock BuscarStock(Indumentaria indumentaria, int talleId);
        List<Indumentaria> GetIndumentarias();
        
        // ✅ NUEVO: Métodos para módulo de Cobro de Cuotas
        Cuota BuscarCuotaPorCodigoBarras(string codigoBarras);
        List<Cuota> BuscarCuotasPorDNI(int dni);
        Alumno BuscarAlumnoPorDNI(int dni);
        void GuardarPagoCuota(Cuota cuota, PagoCuota pago);
        List<Cuota> GetCuotasPendientes();
        
        // ✅ NUEVO: Métodos para ABM de Indumentaria
        void GuardarIndumentaria(Indumentaria indumentaria);
        void ActualizarIndumentaria(Indumentaria indumentaria);
        void EliminarIndumentaria(int id);
        Indumentaria BuscarIndumentariaPorId(int id);
        bool ExisteCodigoIndumentaria(int codigo, int? idExcluir = null);
  
   // Stock management
        List<Stock> GetStocksPorIndumentaria(int indumentariaId);
   void CrearStockParaIndumentaria(Indumentaria indumentaria, int stockMinimo = 5, int stockMaximo = 50);
        void ActualizarStock(Stock stock);
   void GuardarStock(Stock stock);
    }
}
