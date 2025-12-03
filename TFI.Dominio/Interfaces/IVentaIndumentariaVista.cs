using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Dominio.Interfaces
{
    public interface IVentaIndumentariaVista
    {
        void ListarTalles(List<Talle> talles);
        void ListarIndumentarias(List<Indumentaria> indumentarias);
        void MostrarError(string mensaje);
        void MostrarIndumentaria(Indumentaria ind);
        void ActualizarVista(Venta venta);
        void MostrarExito(string mensaje);
        void LimpiarVentana();
    }
}
