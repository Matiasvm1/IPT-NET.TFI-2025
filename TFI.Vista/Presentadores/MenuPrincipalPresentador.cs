using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI.Dominio.Contratos;
using TFI.Dominio.Interfaces;
using TFI.Vista.Vistas;

namespace TFI.Vista.Presentadores
{
    public class MenuPrincipalPresentador
    {
        private IMenuPrincipalVista _vista;
        private IRepositorio _repositorio;
        public MenuPrincipalPresentador(IRepositorio repositorio)
        {
            this._repositorio = repositorio;
        }

        public void MostrarVentaIndumentaria()
        {
            VentaIndumentaria vista = new VentaIndumentaria(new VentaIndumentariaPresentador(_repositorio));
            vista.ShowDialog();
            vista.Dispose();
        }
        
        // Método para mostrar el módulo de Cobro de Cuotas
        public void MostrarCobroCuotas()
        {
            CobroCuotas vista = new CobroCuotas(new CobroCuotasPresentador(_repositorio));
            vista.ShowDialog();
            vista.Dispose();
        }
        
        // Método para mostrar el módulo de Administrar Indumentaria
        public void MostrarAdministrarIndumentaria()
        {
            AdministrarIndumentaria vista = new AdministrarIndumentaria(new AdministrarIndumentariaPresentador(_repositorio));
            vista.ShowDialog();
            vista.Dispose();
        }
        
        public void SetVista(IMenuPrincipalVista vista)
        {
            this._vista = vista;
        }
    }
}
