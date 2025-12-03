using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI.Dominio
{
    public class Stock
    {
        public int Id { get; set; }
        public Indumentaria Indumentaria { get; set; }
        public int Cantidad { get; set; }
        public Talle Talle { get; set; }
        public int CantidadMaxima { get; set; }
        public int CantidadMinima { get; set; }

        public Stock()
        {
            this.CantidadMaxima = int.Parse(ConfigurationManager.AppSettings["CantidadMaximaDefecto"].ToString());
            this.CantidadMinima = int.Parse(ConfigurationManager.AppSettings["CantidadMinimaDefecto"].ToString());
        }

        public Stock(int cantidadMaxima, int cantidadMinima)
        {
            this.CantidadMaxima = cantidadMaxima;
            this.CantidadMinima = cantidadMinima;
        }


        public void AgregarCantidad(int cantidad)
        {
            if(cantidad + Cantidad > CantidadMaxima)
            {
                throw new Exception("El stock ingresado no puede superar la cantidad maxima");
            }
            this.Cantidad += cantidad;
        }
        public void DisminuirCantidad(int cantidad)
        {
            if (cantidad > CantidadMaxima)
            {
                throw new Exception("No se puede comprar mas de la cantidad maxima");
            }
            if (this.Cantidad - cantidad < 0)
            {
                throw new Exception("Stock insuficiente");
            }
            if( cantidad <= 0)
            {
                throw new Exception("La cantidad no puede ser menor o igual que 0");
            }
            this.Cantidad -= cantidad;
        }
    }
}
