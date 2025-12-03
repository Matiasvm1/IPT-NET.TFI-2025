using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI.Dominio;
using TFI.Dominio.Contratos;
using TFI.Dominio.Interfaces;

namespace TFI.Vista.Presentadores
{
    public class AdministrarIndumentariaPresentador
    {
   private readonly IRepositorio _repositorio;
 private IAdministrarIndumentariaVista _vista;
        private Indumentaria _indumentariaSeleccionada;

        public AdministrarIndumentariaPresentador(IRepositorio repositorio)
        {
  this._repositorio = repositorio;
        }

        public void SetVista(IAdministrarIndumentariaVista vista)
        {
  this._vista = vista;
        }

     /// <summary>
  /// Obtiene todos los talles disponibles
 /// </summary>
        public List<Talle> GetTalles()
     {
  return _repositorio.GetTalles();
        }

        /// <summary>
  /// Carga todas las indumentarias y las muestra en la vista
        /// </summary>
        public void CargarIndumentarias()
        {
   try
      {
       var indumentarias = _repositorio.GetIndumentarias();
          _vista.MostrarIndumentarias(indumentarias);
            }
catch (Exception ex)
     {
       _vista.MostrarError($"Error al cargar indumentarias: {ex.Message}");
            }
  }

        /// <summary>
  /// Busca indumentarias por código o descripción
      /// </summary>
        public void BuscarIndumentarias(string criterioBusqueda)
        {
     try
      {
     var todasLasIndumentarias = _repositorio.GetIndumentarias();
     
       if (string.IsNullOrWhiteSpace(criterioBusqueda))
        {
    _vista.MostrarIndumentarias(todasLasIndumentarias);
      return;
        }

         // Buscar por código (si es numérico) o por descripción
   var resultados = todasLasIndumentarias.Where(i =>
      i.Descripcion.ToLower().Contains(criterioBusqueda.ToLower()) ||
        (int.TryParse(criterioBusqueda, out int codigo) && i.Codigo == codigo)
         ).ToList();

    _vista.MostrarIndumentarias(resultados);

    if (resultados.Count == 0)
                {
   _vista.MostrarError("No se encontraron resultados para la búsqueda.");
       }
      }
        catch (Exception ex)
   {
         _vista.MostrarError($"Error al buscar indumentarias: {ex.Message}");
            }
        }

   /// <summary>
        /// Prepara el formulario para crear una nueva indumentaria
      /// </summary>
   public void NuevaIndumentaria()
        {
      _indumentariaSeleccionada = null;
  _vista.LimpiarFormulario();
}

 /// <summary>
      /// Guarda una nueva indumentaria o actualiza una existente
  /// </summary>
        public void GuardarIndumentaria(int codigo, string descripcion, double precio, 
            int stockMinimo, int stockMaximo, Dictionary<string, int> stocksPorTalle = null)
        {
  try
  {
   // Validaciones
         ValidarDatosIndumentaria(codigo, descripcion, precio, stockMinimo, stockMaximo);

            if (_indumentariaSeleccionada == null)
            {
       // ALTA - Crear nueva indumentaria
            if (_repositorio.ExisteCodigoIndumentaria(codigo))
        {
          _vista.MostrarError($"Ya existe una indumentaria con el código {codigo}.");
       return;
 }

   var nuevaIndumentaria = new Indumentaria
            {
    Codigo = codigo,
               Descripcion = descripcion,
                 Precio = precio
 };

      _repositorio.GuardarIndumentaria(nuevaIndumentaria);
             
           // Crear stocks para todos los talles
      var talles = _repositorio.GetTalles();
        foreach (var talle in talles)
    {
  int cantidadInicial = 0;
  
                // Si se proporcionaron cantidades por talle, usar esas
       if (stocksPorTalle != null && stocksPorTalle.ContainsKey(talle.Descripcion))
      {
   cantidadInicial = stocksPorTalle[talle.Descripcion];
          }

                      var stock = new Stock(stockMaximo, stockMinimo)
{
                Indumentaria = nuevaIndumentaria,
        Talle = talle,
   Cantidad = cantidadInicial
    };
       
           _repositorio.GuardarStock(stock);
   }

         _vista.MostrarExito($"Indumentaria '{descripcion}' creada exitosamente con stock inicial configurado.");
         }
  else
          {
                // MODIFICACIÓN - Actualizar existente
        if (_repositorio.ExisteCodigoIndumentaria(codigo, _indumentariaSeleccionada.Id))
     {
               _vista.MostrarError($"Ya existe otra indumentaria con el código {codigo}.");
            return;
        }

  _indumentariaSeleccionada.Codigo = codigo;
    _indumentariaSeleccionada.Descripcion = descripcion;
        _indumentariaSeleccionada.Precio = precio;

          _repositorio.ActualizarIndumentaria(_indumentariaSeleccionada);
           
       // Actualizar límites y cantidades de stock
             var stocks = _repositorio.GetStocksPorIndumentaria(_indumentariaSeleccionada.Id);
         foreach (var stock in stocks)
         {
         stock.CantidadMinima = stockMinimo;
                stock.CantidadMaxima = stockMaximo;
            
 // Si se proporcionaron cantidades, actualizarlas
          if (stocksPorTalle != null && stocksPorTalle.ContainsKey(stock.Talle.Descripcion))
      {
stock.Cantidad = stocksPorTalle[stock.Talle.Descripcion];
             }
          
          _repositorio.ActualizarStock(stock);
          }

    _vista.MostrarExito($"Indumentaria '{descripcion}' actualizada exitosamente.");
         }

     CargarIndumentarias();
        _vista.LimpiarFormulario();
     _indumentariaSeleccionada = null;
            }
catch (Exception ex)
            {
           _vista.MostrarError($"Error al guardar la indumentaria: {ex.Message}");
         }
    }

        /// <summary>
    /// Valida los datos de entrada para una indumentaria
        /// </summary>
        private void ValidarDatosIndumentaria(int codigo, string descripcion, double precio, int stockMinimo, int stockMaximo)
        {
   if (codigo <= 0)
                throw new Exception("El código debe ser un número mayor a 0.");

        if (string.IsNullOrWhiteSpace(descripcion))
    throw new Exception("La descripción es obligatoria.");

         if (descripcion.Length > 200)
                throw new Exception("La descripción no puede superar los 200 caracteres.");

   if (precio <= 0)
           throw new Exception("El precio debe ser mayor a 0.");

   if (stockMinimo < 0)
    throw new Exception("El stock mínimo no puede ser negativo.");

            if (stockMaximo <= 0)
throw new Exception("El stock máximo debe ser mayor a 0.");

            if (stockMinimo >= stockMaximo)
      throw new Exception("El stock mínimo debe ser menor al stock máximo.");
        }

        /// <summary>
      /// Carga una indumentaria para edición
        /// </summary>
        public void SeleccionarIndumentaria(int id)
        {
            try
         {
            _indumentariaSeleccionada = _repositorio.BuscarIndumentariaPorId(id);
    
             if (_indumentariaSeleccionada == null)
            {
        _vista.MostrarError("No se encontró la indumentaria seleccionada.");
         return;
            }

      _vista.CargarIndumentariaEnFormulario(_indumentariaSeleccionada);
       
             // Cargar también los stocks de esta indumentaria
        var stocks = _repositorio.GetStocksPorIndumentaria(id);
    _vista.MostrarStocks(stocks);
     }
            catch (Exception ex)
     {
         _vista.MostrarError($"Error al seleccionar la indumentaria: {ex.Message}");
      }
   }

        /// <summary>
        /// Elimina una indumentaria (baja)
        /// </summary>
        public void EliminarIndumentaria(int id)
        {
      try
    {
 var indumentaria = _repositorio.BuscarIndumentariaPorId(id);
         
      if (indumentaria == null)
         {
               _vista.MostrarError("No se encontró la indumentaria seleccionada.");
         return;
                }

      // Confirmar eliminación
     if (!_vista.ConfirmarEliminacion($"¿Está seguro de eliminar '{indumentaria.Descripcion}'?\n\nEsta acción eliminará también todos los stocks asociados y no se puede deshacer."))
                {
        return;
     }

     _repositorio.EliminarIndumentaria(id);
        _vista.MostrarExito($"Indumentaria '{indumentaria.Descripcion}' eliminada exitosamente.");
            
   CargarIndumentarias();
                _vista.LimpiarFormulario();
        _indumentariaSeleccionada = null;
     }
        catch (Exception ex)
            {
      _vista.MostrarError($"Error al eliminar la indumentaria: {ex.Message}");
       }
        }
    }
}
