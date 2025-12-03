using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Dominio
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public double Total { get; set; }

        // ⭐ Constructor sin parámetros para Entity Framework Core
        public Factura()
        {
            // EF Core usa este constructor para hidratar entidades desde la BD
        }

        public Factura (double total)
        {
            this.FechaHora = DateTime.Now;
            this.Total = total;
        }
    }
}
