using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Vista.DTOs
{
    /// <summary>
    /// DTO para mostrar indumentarias en el DataGridView
    /// Evita problemas de lazy loading y optimiza la visualización
    /// </summary>
    public class IndumentariaDTO
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
   public string PrecioFormateado => $"$ {Precio:N2}";
        public int StockTotal { get; set; }
        public string EstadoStock
        {
            get
        {
         if (StockTotal == 0) return "Sin Stock";
        if (StockTotal < 20) return "Stock Bajo";
                return "Stock OK";
      }
        }
    }
    
    /// <summary>
    /// DTO para mostrar stocks por talle
    /// </summary>
    public class StockDTO
    {
        public int Id { get; set; }
        public string Talle { get; set; }
     public int Cantidad { get; set; }
        public int CantidadMinima { get; set; }
        public int CantidadMaxima { get; set; }
        public string Estado
        {
      get
            {
                if (Cantidad == 0) return "Agotado";
     if (Cantidad < CantidadMinima) return "Bajo";
    if (Cantidad > CantidadMaxima) return "Exceso";
return "Normal";
            }
}
    }
}
