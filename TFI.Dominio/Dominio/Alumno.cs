using System;
using System.Collections.Generic;

namespace TFI.Dominio
{
 /// <summary>
 /// Entidad Alumno - Representa un estudiante del instituto
    /// </summary>
    public class Alumno
    {
    public int Id { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
     public string Apellido { get; set; }
      public string Email { get; set; }
        public string Telefono { get; set; }
        
        // Propiedad calculada
        public string NombreCompleto => $"{Apellido}, {Nombre}";
        
        // Relación: Un alumno tiene muchas cuotas
        public List<Cuota> Cuotas { get; set; }

        public Alumno()
        {
    Cuotas = new List<Cuota>();
  }
    }
}
