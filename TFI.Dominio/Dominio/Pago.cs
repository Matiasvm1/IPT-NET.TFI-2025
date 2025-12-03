using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Dominio
{
    public class Pago
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public double Total { get; set; }
        public Factura Factura { get; set; }

        // ⭐ Constructor sin parámetros para Entity Framework Core
        public Pago()
        {
            // EF Core usa este constructor para hidratar entidades desde la BD
        }

        public Pago(double total)
        {
            this.FechaHora = DateTime.Now;
            this.Total = total;
            GenerarFactura();
        }

        public void GenerarFactura()
        {
            this.Factura = new Factura(Total); 
        }
    }
}
