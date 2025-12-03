using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Dominio.Interfaces
{
    public interface ILoginVista
    {
        void MostrarError(string mensaje);
        void Ocultar();
        void Mostrar();
    }
}
