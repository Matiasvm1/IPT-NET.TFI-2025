using System;

namespace TFI.Vista.DTOs
{
    /// <summary>
    /// DTO para mostrar cuotas en el DataGridView
    /// Similar a LineaDeVentaDTO del módulo de Venta de Indumentaria
    /// </summary>
    public class CuotaDTO
    {
        public int Id { get; set; }
 public string Periodo { get; set; }
   public string CodigoBarras { get; set; }
        public double MontoOriginal { get; set; }
        public DateTime PrimerVencimiento { get; set; }
  public string Estado { get; set; }
    public double Recargo { get; set; }
        public double MontoAPagar { get; set; }
  
  // Para marcar si está seleccionada en el grid
   public bool Seleccionada { get; set; }
    }
}
