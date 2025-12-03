using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI.Dominio;
using TFI.Dominio.Contratos;
using TFI.Dominio.Interfaces;

namespace TFI.Vista.Presentadores
{
    public class VentaIndumentariaPresentador
    {
        private IRepositorio _repositorio;
        private IVentaIndumentariaVista _vista;
        private Venta Venta;
        private Indumentaria _indumentaria;

        public VentaIndumentariaPresentador(IRepositorio repositorio)
        {
            this._repositorio = repositorio;
        }

        public void SetVista(IVentaIndumentariaVista vista)
        {
            this._vista = vista;
        }
        public List<Talle> GetTalles()
        {
            return _repositorio.GetTalles();
        }

        public List<Indumentaria> GetIndumentarias()
        {
            return _repositorio.GetIndumentarias();
        }

        public Venta CrearNuevaVenta()
        {
            return this.Venta = new Venta(); 
        }
        public void IngresarIndumentaria(int codigo)
        {
            _indumentaria = this._repositorio.BuscarIndumentaria(codigo);
            if (_indumentaria == null) {
                _vista.MostrarError("Indumentaria inexistente");
            }
            else
            {
                _vista.MostrarIndumentaria(_indumentaria);
            } 
        }
        public void RegistrarLineaDeVenta(Indumentaria indumentaria, int cantidad, int talleId)
        {
            try
            {
                var stock = _repositorio.BuscarStock(indumentaria, talleId);
         
                // ✅ VALIDACIÓN: Verificar que existe stock para este producto y talle
                if (stock == null)
                {
                    throw new Exception($"No hay stock disponible para {indumentaria.Descripcion} en el talle seleccionado.");
                }
 
                stock.DisminuirCantidad(cantidad);
                Venta.AgregarLineaDeVenta(indumentaria, cantidad);
                _vista.ActualizarVista(Venta);
            }
            catch (Exception e)
            {
                _vista.MostrarError(e.Message);
            }
        }
        public double IngresarImporte(double importe)
        {
            double vuelto = 0;
            try
            {
            vuelto = Venta.CalcularVuelto(importe);
            }
            catch (Exception e)
            {
                _vista.MostrarError(e.Message);
            }
            return vuelto;
        }
        public void ConfirmarVenta()
        {
            try
            {
                ValidarVenta(Venta);
                Venta.GenerarPago();
                _repositorio.GuardarVenta(Venta);
                _vista.MostrarExito("Venta Realizada con Exito");
                _vista.LimpiarVentana();
            }catch(Exception e)
            {
                _vista.MostrarError(e.Message);
            }
        }
        public void ValidarVenta(Venta venta)
        {
            if(venta.LineaDeVentas.Count == 0)
            {
                throw new Exception("Debe haber al menos un producto cargado en la venta");
            }
        }
        public void EliminarLineaDeVenta(int fila)
        {
            this.Venta.LineaDeVentas.RemoveAt(fila);
             _vista.ActualizarVista(Venta);
        }
    }
}
