using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public double Total { get { return LineaDeVentas.Sum(ldv => ldv.Subtotal); } }
        public List<LineaDeVenta> LineaDeVentas { get; set; }
        public Pago Pago { get; set; }

        public Venta()
        {
            this.FechaHora = DateTime.Now;
            this.LineaDeVentas = new List<LineaDeVenta>();
        }

        public void AgregarLineaDeVenta(Indumentaria indumentaria, int cantidad)
        {
            LineaDeVenta ldv = new LineaDeVenta(indumentaria, cantidad);
            this.LineaDeVentas.Add(ldv);
        }

        public double CalcularVuelto(double importe)
        {
            if(importe < Total)
            {
                throw new Exception("Importe insuficiente");
            }
            if(importe < 0)
            {
                throw new Exception("El importe no puede ser menor que 0");
            }
            return importe - Total;
        }
        public void GenerarPago()
        {
            Pago = new Pago(Total);
        }
    }
}
