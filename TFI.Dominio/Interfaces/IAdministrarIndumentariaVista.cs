using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Dominio.Interfaces
{
    public interface IAdministrarIndumentariaVista
    {
        /// <summary>
        /// Muestra la lista de indumentarias en la grilla
      /// </summary>
        void MostrarIndumentarias(List<Indumentaria> indumentarias);
        
        /// <summary>
        /// Muestra los stocks de una indumentaria seleccionada
        /// </summary>
        void MostrarStocks(List<Stock> stocks);
    
        /// <summary>
   /// Muestra un mensaje de error al usuario
        /// </summary>
        void MostrarError(string mensaje);
     
        /// <summary>
      /// Muestra un mensaje de éxito al usuario
        /// </summary>
        void MostrarExito(string mensaje);
    
      /// <summary>
        /// Limpia el formulario para crear una nueva indumentaria
        /// </summary>
        void LimpiarFormulario();
        
        /// <summary>
        /// Carga los datos de una indumentaria en el formulario para edición
        /// </summary>
        void CargarIndumentariaEnFormulario(Indumentaria indumentaria);
        
        /// <summary>
        /// Solicita confirmación para eliminar una indumentaria
      /// </summary>
        bool ConfirmarEliminacion(string mensaje);
    }
}
