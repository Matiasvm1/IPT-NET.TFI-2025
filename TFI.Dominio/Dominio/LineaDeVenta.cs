using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Dominio
{
    public class LineaDeVenta
    {
        public int Id { get; set; }
        public Indumentaria Indumentaria { get; set; }
        public int CodigoIndumentaria { get { return Indumentaria?.Id ?? 0; } }
        public string DescripcionIndumentaria { get { return Indumentaria?.Descripcion ?? string.Empty; } }
        public double PrecioIndumentaria { get { return Indumentaria?.Precio ?? 0; } }
        public int Cantidad { get; set; }
        public double Subtotal { get { return PrecioIndumentaria * Cantidad; } }

        // ⭐ Constructor sin parámetros para Entity Framework Core
        public LineaDeVenta()
        {
            // EF Core usa este constructor para hidratar entidades desde la BD
        }

        // Constructor con parámetros para uso en código
        public LineaDeVenta(Indumentaria indumentaria, int cantidad)
        {
            this.Indumentaria = indumentaria;
            this.Cantidad = cantidad;
        }
    }
}
