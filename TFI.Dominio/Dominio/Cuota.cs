using System;

namespace TFI.Dominio
{
    /// <summary>
    /// Entidad Cuota - Representa una cuota mensual de un alumno
    /// Incluye lógica de cálculo de intereses por vencimientos (regla de negocio del PDF)
    /// </summary>
    public class Cuota
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public double MontoOriginal { get; set; }
    
        // 3 fechas de vencimiento según PDF (5% de recargo acumulativo por cada vencimiento)
        public DateTime PrimerVencimiento { get; set; }
        public DateTime SegundoVencimiento { get; set; }
        public DateTime TercerVencimiento { get; set; }
        
        public EstadoCuota Estado { get; set; }
        
   // Relación: Una cuota pertenece a un alumno
   public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
        
    // Relación: Una cuota puede tener un pago asociado (cuando se paga)
        public int? PagoCuotaId { get; set; }
        // IMPORTANTE: Esta propiedad se ignora en EF Core para evitar FK circular
      // public PagoCuota PagoCuota { get; set; }

        // Propiedades calculadas (NO se persisten en BD)
        public string PeriodoDescripcion => $"{GetNombreMes(Mes)}/{Anio}";
        
        public Cuota()
     {
    Estado = EstadoCuota.Pendiente;
        }

        /// <summary>
        /// Calcula el monto a pagar según la fecha actual y los vencimientos
        /// Regla de negocio del PDF: 5% de recargo por cada vencimiento pasado (acumulativo)
    /// </summary>
        public double CalcularMontoAPagar()
   {
 DateTime fechaActual = DateTime.Now;
         double monto = MontoOriginal;
            
          // Si pasó el primer vencimiento: +5%
            if (fechaActual > PrimerVencimiento)
          {
       monto += MontoOriginal * 0.05;
       }
      
  // Si pasó el segundo vencimiento: +5% adicional (total 10%)
            if (fechaActual > SegundoVencimiento)
            {
     monto += MontoOriginal * 0.05;
 }
            
 // Si pasó el tercer vencimiento: +5% adicional (total 15%)
  if (fechaActual > TercerVencimiento)
     {
        monto += MontoOriginal * 0.05;
            }
            
            return Math.Round(monto, 2);
     }

  /// <summary>
        /// Actualiza el estado de la cuota según la fecha actual
        /// </summary>
  public void ActualizarEstado()
     {
            if (Estado == EstadoCuota.Pagada)
     return; // Si ya está pagada, no cambiar

      DateTime fechaActual = DateTime.Now;
 
      if (fechaActual > TercerVencimiento)
            {
    Estado = EstadoCuota.Vencida;
            }
          else if (fechaActual <= PrimerVencimiento)
       {
        Estado = EstadoCuota.Pendiente;
            }
            else
 {
         Estado = EstadoCuota.Vencida;
            }
      }

/// <summary>
        /// Marca la cuota como pagada
   /// </summary>
    public void MarcarComoPagada(PagoCuota pago)
  {
  Estado = EstadoCuota.Pagada;
        PagoCuotaId = pago.Id;
            // ? NO asignar la propiedad de navegación PagoCuota aquí
    // Esto lo manejará EF Core automáticamente
        }

        private string GetNombreMes(int mes)
        {
      string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
     "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            return mes >= 1 && mes <= 12 ? meses[mes] : "Inválido";
    }
    }
}
