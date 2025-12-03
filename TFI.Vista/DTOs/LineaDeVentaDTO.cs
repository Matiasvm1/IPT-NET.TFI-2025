using System;

namespace TFI.Vista.DTOs
{
    /// <summary>
    /// DTO para mostrar líneas de venta en el DataGridView
    /// Evita problemas de binding con propiedades de navegación de Entity Framework
    /// </summary>
    public class LineaDeVentaDTO
    {
        public string DescripcionIndumentaria { get; set; }
        public int Cantidad { get; set; }
        public double PrecioIndumentaria { get; set; }
 public double Subtotal { get; set; }
   
        public LineaDeVentaDTO()
        {
        }
        
        public LineaDeVentaDTO(string descripcion, int cantidad, double precio)
        {
    DescripcionIndumentaria = descripcion;
 Cantidad = cantidad;
            PrecioIndumentaria = precio;
          Subtotal = precio * cantidad;
        }
    }
}
