using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI.Dominio.Contratos;
using TFI.Dominio.Interfaces;

namespace TFI.Vista.Presentadores
{
    public class LoginPresentador
    {
        private IRepositorio _repositorio;
        private ILoginVista _vista;

        public LoginPresentador(IRepositorio repositorio)
        {
            this._repositorio = repositorio;
        }

        public void SetVista(ILoginVista vista)
        {
            this._vista = vista;
        }

        public bool IngresarDatos(int legajo,string contraseña)
        {
            if (_repositorio.IniciarSesion(legajo, contraseña))
            {
                MostrarMenuPrincipal();
                return true;
            }
            else
            {
                _vista.MostrarError("Usuario incorrecto");
                return false;
            }
        }


        public void MostrarMenuPrincipal()
        {
            MenuPrincipal vista = new MenuPrincipal(new MenuPrincipalPresentador(this._repositorio));
            vista.Show();
            _vista.Ocultar(); 
        }
    }
}
