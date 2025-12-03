using System;

namespace TFI.Dominio
{
    /// <summary>
    /// Entidad PagoCuota - Representa el pago de una cuota
    /// Similar a la entidad Pago existente, pero específica para cuotas
    /// </summary>
    public class PagoCuota
 {
        public int Id { get; set; }
     public DateTime FechaPago { get; set; }
  public double MontoAbonado { get; set; }
  public string MedioPago { get; set; } // Efectivo, Tarjeta, Transferencia, etc.

        // Información adicional del pago
        public double Recargo { get; set; } // Recargo aplicado por vencimiento
     public string Observaciones { get; set; }
      
        // Relación: Un pago corresponde a una cuota
    public int CuotaId { get; set; }
        public Cuota Cuota { get; set; }

  // Constructor sin parámetros para EF Core
  public PagoCuota()
        {
   }

        public PagoCuota(double montoAbonado, double recargo, string medioPago = "Efectivo")
        {
          FechaPago = DateTime.Now;
       MontoAbonado = montoAbonado;
       Recargo = recargo;
          MedioPago = medioPago;
        }
    }
}
