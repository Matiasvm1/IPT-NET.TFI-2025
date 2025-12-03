using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TFI.AccesoADatos;
using TFI.Dominio.Contratos;
using TFI.Vista.Vistas;
using Unity;

namespace TFI.Vista
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var contenedor = new UnityContainer();
            contenedor.RegisterType<IRepositorio, Repositorio>();
            var vista = contenedor.Resolve<Login>();
            Application.Run(vista);
        }
    }
}
