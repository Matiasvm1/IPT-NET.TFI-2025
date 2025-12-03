using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFI.Dominio;
using TFI.Dominio.Interfaces;
using TFI.Vista.Presentadores;
using TFI.Vista.Styles;
using TFI.Vista.DTOs;

namespace TFI.Vista.Vistas
{
    public partial class VentaIndumentaria : Form, IVentaIndumentariaVista
    {
        private VentaIndumentariaPresentador _presentador;
 private Venta _venta;
        private Indumentaria _indumentaria;
        
        public VentaIndumentaria(VentaIndumentariaPresentador presentador)
{
            InitializeComponent();
   this._presentador = presentador;
         this.FormBorderStyle = FormBorderStyle.None;
   this.KeyPreview = true; // ✅ Habilitar captura de teclas
            presentador.SetVista(this);
       
        // ✅ ESC para volver
this.KeyDown += (s, e) =>
{
   if (e.KeyCode == Keys.Escape)
         {
      this.Close();
     }
        };
            
     // ✅ Aplicar estilos modernos mejorados
       ConfigurarEstilosModernos();
      
        InicializarVista();
        }

        private void ConfigurarEstilosModernos()
   {
       // Configurar formulario
     ModernStyles.ApplyFormStyle(this);
  this.Size = new Size(1400, 850);
      this.StartPosition = FormStartPosition.CenterScreen;
     this.Text = "IPT-NET - Venta de Indumentaria";
        this.BackColor = ModernStyles.BackgroundLight;
      
     // Limpiar y recrear layout moderno
    this.Controls.Clear();
   CrearLayoutModerno();
      }

        private void CrearLayoutModerno()
        {
       // ===== IMPORTANTE: NO limpiar controles, solo ocultarlos =====
  // Los BindingSources del Designer deben permanecer
      foreach (Control ctrl in this.Controls)
    {
if (!(ctrl is BindingSource))
        {
   ctrl.Visible = false;
    }
 }

   // ===== PANEL SUPERIOR - TÍTULO =====
     Panel panelTitulo = new Panel
 {
       Location = new Point(20, 20),
    Size = new Size(1340, 80),
   BackColor = ModernStyles.CardBackground
      };
    ModernStyles.ApplyCardPanelStyle(panelTitulo);

     Label lblTitulo = new Label
  {
    Text = "🛒 Venta de Indumentaria",
    Font = new Font("Segoe UI", 28F, FontStyle.Bold),
    ForeColor = ModernStyles.PrimaryGreen,
    AutoSize = true,
Location = new Point(30, 20)
  };
    panelTitulo.Controls.Add(lblTitulo);
  
        Label lblEscHint = new Label
 {
    Text = "ESC para volver",
     Font = new Font("Segoe UI", 10F),
     ForeColor = ModernStyles.TextLight,
     AutoSize = true,
   Location = new Point(1180, 30)
       };
  panelTitulo.Controls.Add(lblEscHint);
   
       this.Controls.Add(panelTitulo);

       // ===== PANEL IZQUIERDO - CATÁLOGO DE PRODUCTOS =====
 Panel panelIzquierdo = new Panel
 {
Location = new Point(20, 120),
    Size = new Size(650, 400),
   BackColor = ModernStyles.CardBackground
     };
       ModernStyles.ApplyCardPanelStyle(panelIzquierdo);
    
        // Título sección
Label lblCatalogo = new Label
     {
  Text = "📦 Catálogo de Productos",
  Font = new Font("Segoe UI", 18F, FontStyle.Bold),
    ForeColor = ModernStyles.TextDark,
         AutoSize = true,
    Location = new Point(25, 20)
        };
            panelIzquierdo.Controls.Add(lblCatalogo);

   // DataGrid de productos disponibles
       DataGridView dgvProductos = new DataGridView
   {
Location = new Point(25, 65),
       Size = new Size(600, 240),
   ReadOnly = true,
          AllowUserToAddRows = false,
  AllowUserToDeleteRows = false,
       SelectionMode = DataGridViewSelectionMode.FullRowSelect,
     MultiSelect = false,
     AutoGenerateColumns = false,
        TabIndex = 0
            };
    ModernStyles.ApplyDataGridViewStyle(dgvProductos);

  // Configurar columnas del catálogo
    dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
      {
    Name = "Codigo",
       DataPropertyName = "Codigo",
     HeaderText = "Código",
   Width = 80,
      DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
   });

   dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
 {
    Name = "Descripcion",
      DataPropertyName = "Descripcion",
  HeaderText = "Producto",
   AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
      });

       dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
  {
    Name = "Precio",
       DataPropertyName = "Precio",
   HeaderText = "Precio",
   Width = 120,
    DefaultCellStyle = new DataGridViewCellStyle
   {
      Format = "C2",
        Alignment = DataGridViewContentAlignment.MiddleRight
         }
      });

 // Evento de selección de producto
  dgvProductos.SelectionChanged += (s, e) =>
{
       if (dgvProductos.SelectedRows.Count > 0)
       {
     var producto = (Indumentaria)dgvProductos.SelectedRows[0].DataBoundItem;
    SeleccionarProducto(producto);
     }
  };

  panelIzquierdo.Controls.Add(dgvProductos);

  // Panel para selección de talle y cantidad
   Panel panelSeleccion = new Panel
         {
      Location = new Point(25, 315),
  Size = new Size(600, 70),
 BackColor = Color.FromArgb(245, 245, 245)
       };

   Label lblProductoSel = new Label
       {
   Text = "Producto Seleccionado:",
    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
    ForeColor = ModernStyles.TextDark,
Location = new Point(10, 10),
   AutoSize = true
       };
      panelSeleccion.Controls.Add(lblProductoSel);

         LblDescripcion.Location = new Point(10, 30);
        LblDescripcion.Size = new Size(300, 30);
  LblDescripcion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
  LblDescripcion.ForeColor = ModernStyles.InfoBlue;
 LblDescripcion.Text = "Seleccione un producto";
     LblDescripcion.BorderStyle = BorderStyle.None;
   LblDescripcion.BackColor = Color.Transparent;
  LblDescripcion.TextAlign = ContentAlignment.MiddleLeft;
   LblDescripcion.Visible = true;
        panelSeleccion.Controls.Add(LblDescripcion);

        Label lblPrecioLbl = new Label
  {
  Text = "Precio:",
    Font = new Font("Segoe UI", 9F),
    ForeColor = ModernStyles.TextMedium,
  Location = new Point(330, 10),
     AutoSize = true
   };
 panelSeleccion.Controls.Add(lblPrecioLbl);

  lblPrecio.Location = new Point(330, 25);
   lblPrecio.Size = new Size(120, 35);
      lblPrecio.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
  lblPrecio.ForeColor = ModernStyles.PrimaryGreen;
    lblPrecio.Text = "$ 0,00";
lblPrecio.BorderStyle = BorderStyle.None;
   lblPrecio.BackColor = Color.Transparent;
  lblPrecio.TextAlign = ContentAlignment.MiddleLeft;
    lblPrecio.Visible = true;
        panelSeleccion.Controls.Add(lblPrecio);

       Label lblTalleLbl = new Label
       {
          Text = "Talle:",
    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
       ForeColor = ModernStyles.TextDark,
   Location = new Point(470, 10),
          AutoSize = true
      };
 panelSeleccion.Controls.Add(lblTalleLbl);

     comboTalle.Location = new Point(470, 30);
   comboTalle.Size = new Size(120, 30);
  comboTalle.Font = new Font("Segoe UI", 11F);
   comboTalle.DropDownStyle = ComboBoxStyle.DropDownList;
 comboTalle.FlatStyle = FlatStyle.Flat;
 comboTalle.BackColor = Color.White;
   comboTalle.TabIndex = 1;
    comboTalle.Visible = true;
    panelSeleccion.Controls.Add(comboTalle);

  panelIzquierdo.Controls.Add(panelSeleccion);

 // Botón Agregar (grande y prominente) - ahora más abajo
    btnAgregarIndumentaria.Text = "➕ AGREGAR AL CARRITO";
   btnAgregarIndumentaria.Location = new Point(25, 395);
   btnAgregarIndumentaria.Size = new Size(300, 0);
     btnAgregarIndumentaria.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
 btnAgregarIndumentaria.Cursor = Cursors.Hand;
   btnAgregarIndumentaria.TabIndex = 3;
    btnAgregarIndumentaria.Visible = false;
      ModernStyles.ApplyPrimaryButtonStyle(btnAgregarIndumentaria);
     panelIzquierdo.Controls.Add(btnAgregarIndumentaria);

 this.Controls.Add(panelIzquierdo);

 // ===== PANEL CENTRAL - AGREGAR AL CARRITO =====
    Panel panelAgregar = new Panel
    {
        Location = new Point(690, 120),
       Size = new Size(320, 400),
        BackColor = ModernStyles.CardBackground
     };
        ModernStyles.ApplyCardPanelStyle(panelAgregar);

  Label lblAgregar = new Label
     {
     Text = "Agregar al Carrito",
       Font = new Font("Segoe UI", 18F, FontStyle.Bold),
    ForeColor = ModernStyles.TextDark,
   AutoSize = true,
 Location = new Point(25, 20)
    };
    panelAgregar.Controls.Add(lblAgregar);

 // Cantidad
   Label lblCantidadLabel = new Label
 {
    Text = "Cantidad",
    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
 ForeColor = ModernStyles.TextDark,
  AutoSize = true,
    Location = new Point(25, 80)
};
   panelAgregar.Controls.Add(lblCantidadLabel);

      TxtCantidad.Location = new Point(25, 115);
   TxtCantidad.Size = new Size(270, 50);
   TxtCantidad.Font = new Font("Segoe UI", 20F);
    TxtCantidad.BorderStyle = BorderStyle.FixedSingle;
  TxtCantidad.BackColor = Color.White;
  TxtCantidad.ForeColor = ModernStyles.TextDark;
TxtCantidad.Text = "1";
        TxtCantidad.TextAlign = HorizontalAlignment.Center;
 TxtCantidad.TabIndex = 2;
       TxtCantidad.Visible = true;
panelAgregar.Controls.Add(TxtCantidad);

      // Botones +/-
   Button btnMenos = new Button
  {
          Text = "➖",
   Location = new Point(25, 180),
    Size = new Size(60, 50),
       Font = new Font("Segoe UI", 16F),
    Cursor = Cursors.Hand
 };
      ModernStyles.ApplySecondaryButtonStyle(btnMenos);
 btnMenos.Click += (s, e) =>
     {
   if (int.TryParse(TxtCantidad.Text, out int cant) && cant > 1)
     {
         TxtCantidad.Text = (cant - 1).ToString();
}
      };
      panelAgregar.Controls.Add(btnMenos);

 Button btnMas = new Button
        {
       Text = "➕",
      Location = new Point(235, 180),
       Size = new Size(60, 50),
    Font = new Font("Segoe UI", 16F),
  Cursor = Cursors.Hand
   };
        ModernStyles.ApplyPrimaryButtonStyle(btnMas);
       btnMas.Click += (s, e) =>
   {
     if (int.TryParse(TxtCantidad.Text, out int cant))
  {
    TxtCantidad.Text = (cant + 1).ToString();
       }
         };
     panelAgregar.Controls.Add(btnMas);

        // Botón Agregar grande
  Button btnAgregar = new Button
      {
     Text = "🛒 AGREGAR\nAL CARRITO",
     Location = new Point(25, 250),
        Size = new Size(270, 130),
   Font = new Font("Segoe UI", 16F, FontStyle.Bold),
         Cursor = Cursors.Hand,
  TabIndex = 3
       };
  ModernStyles.ApplyPrimaryButtonStyle(btnAgregar);
 btnAgregar.Click += btnAgregarIndumentaria_Click;
  panelAgregar.Controls.Add(btnAgregar);

      this.Controls.Add(panelAgregar);

 // ===== PANEL DERECHO - CARRITO Y TOTAL =====
 Panel panelDerecho = new Panel
  {
 Location = new Point(1030, 120),
  Size = new Size(330, 400),
 BackColor = ModernStyles.CardBackground
    };
 ModernStyles.ApplyCardPanelStyle(panelDerecho);

 Label lblCarrito = new Label
 {
 Text = "🛒 Mi Carrito",
  Font = new Font("Segoe UI", 16F, FontStyle.Bold),
    ForeColor = ModernStyles.TextDark,
     AutoSize = true,
  Location = new Point(20, 15)
   };
  panelDerecho.Controls.Add(lblCarrito);

 // ✅ SOLUCIÓN DEFINITIVA: NO vincular al BindingSource del Designer
// Usar el DataGridView sin DataSource automático y controlarlo manualmente
dataGridLineaVenta.DataSource = null;
dataGridLineaVenta.AutoGenerateColumns = false;
dataGridLineaVenta.Columns.Clear();

// ✅ Configurar propiedades visuales del DataGridView
dataGridLineaVenta.Location = new Point(10, 55);
dataGridLineaVenta.Size = new Size(310, 200);
dataGridLineaVenta.TabIndex = 5;
dataGridLineaVenta.Visible = true;
dataGridLineaVenta.RowHeadersVisible = false;
dataGridLineaVenta.AllowUserToResizeRows = false;
dataGridLineaVenta.AllowUserToResizeColumns = false;
dataGridLineaVenta.ScrollBars = ScrollBars.Vertical;
dataGridLineaVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
dataGridLineaVenta.MultiSelect = false;
dataGridLineaVenta.ReadOnly = true;
dataGridLineaVenta.BackgroundColor = Color.White;
dataGridLineaVenta.BorderStyle = BorderStyle.FixedSingle;
dataGridLineaVenta.AllowUserToAddRows = false;

// ✅ Crear columnas manualmente
dataGridLineaVenta.Columns.Add(new DataGridViewTextBoxColumn
{
  Name = "DescripcionIndumentaria",
DataPropertyName = "DescripcionIndumentaria",
    HeaderText = "Producto",
    Width = 140,
MinimumWidth = 120,
    ReadOnly = true
});

dataGridLineaVenta.Columns.Add(new DataGridViewTextBoxColumn
{
    Name = "Cantidad",
 DataPropertyName = "Cantidad",
    HeaderText = "Cant",
  Width = 50,
    MinimumWidth = 45,
    ReadOnly = true,
    DefaultCellStyle = new DataGridViewCellStyle
    {
        Alignment = DataGridViewContentAlignment.MiddleCenter,
Font = new Font("Segoe UI", 10F, FontStyle.Bold)
    }
});

dataGridLineaVenta.Columns.Add(new DataGridViewTextBoxColumn
{
    Name = "PrecioIndumentaria",
    DataPropertyName = "PrecioIndumentaria",
    HeaderText = "Precio",
    Width = 60,
    MinimumWidth = 55,
    ReadOnly = true,
    DefaultCellStyle = new DataGridViewCellStyle
    {
    Format = "C0",
        Alignment = DataGridViewContentAlignment.MiddleRight
    }
});

dataGridLineaVenta.Columns.Add(new DataGridViewTextBoxColumn
{
Name = "Subtotal",
    DataPropertyName = "Subtotal",
    HeaderText = "Total",
    Width = 70,
    MinimumWidth = 65,
  ReadOnly = true,
    DefaultCellStyle = new DataGridViewCellStyle
    {
 Format = "C0",
    Alignment = DataGridViewContentAlignment.MiddleRight,
    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
        ForeColor = ModernStyles.PrimaryGreen
    }
});

// ✅ NO asignar DataSource aquí - se hará manualmente en ActualizarVista

ModernStyles.ApplyDataGridViewStyle(dataGridLineaVenta);
panelDerecho.Controls.Add(dataGridLineaVenta);

// Botón Eliminar - rediseñado
  Button btnEliminar = new Button
  {
      Text = "🗑️ Eliminar",
    Location = new Point(10, 265),
    Size = new Size(310, 40),
      Font = new Font("Segoe UI", 11F, FontStyle.Bold),
      Cursor = Cursors.Hand,
      TabIndex = 4,
      ForeColor = Color.White,
      BackColor = Color.FromArgb(220, 53, 69), // Rojo
      FlatStyle = FlatStyle.Flat
  };
  btnEliminar.FlatAppearance.BorderSize = 0;
  btnEliminar.Click += btnEliminarIndumentaria_Click;
  
  // Efecto hover
btnEliminar.MouseEnter += (s, e) => btnEliminar.BackColor = Color.FromArgb(200, 35, 51);
  btnEliminar.MouseLeave += (s, e) => btnEliminar.BackColor = Color.FromArgb(220, 53, 69);
  
  panelDerecho.Controls.Add(btnEliminar);
  
  // Ocultar el botón original
  btnEliminarIndumentaria.Visible = false;

     // Separador visual
     Panel separador = new Panel
   {
     Location = new Point(10, 315),
      Size = new Size(310, 2),
         BackColor = Color.FromArgb(220, 220, 220)
     };
     panelDerecho.Controls.Add(separador);

 // Total
    Label lblTotalLabel = new Label
{
    Text = "TOTAL A PAGAR:",
     Font = new Font("Segoe UI", 11F, FontStyle.Bold),
   ForeColor = ModernStyles.TextDark,
   AutoSize = true,
Location = new Point(10, 325)
   };
 panelDerecho.Controls.Add(lblTotalLabel);

 lblTotal.Location = new Point(10, 345);
     lblTotal.Size = new Size(310, 45);
  lblTotal.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
      lblTotal.ForeColor = ModernStyles.PrimaryGreen;
   lblTotal.TextAlign = ContentAlignment.MiddleCenter;
  lblTotal.BorderStyle = BorderStyle.None;
     lblTotal.BackColor = Color.Transparent;
lblTotal.Visible = true;
      panelDerecho.Controls.Add(lblTotal);

 this.Controls.Add(panelDerecho);

// ===== PANEL INFERIOR - PAGO Y ACCIONES =====
  Panel panelPago = new Panel
{
       Location = new Point(20, 540),
   Size = new Size(1340, 250),
    BackColor = ModernStyles.CardBackground
 };
   ModernStyles.ApplyCardPanelStyle(panelPago);

  Label lblPago = new Label
    {
          Text = "💳 Finalizar Pago",
   Font = new Font("Segoe UI", 18F, FontStyle.Bold),
   ForeColor = ModernStyles.TextDark,
     AutoSize = true,
     Location = new Point(25, 20)
      };
       panelPago.Controls.Add(lblPago);

    // Importe Pagado
   Label lblImporteLabel = new Label
   {
      Text = "Importe Pagado",
 Font = new Font("Segoe UI", 12F, FontStyle.Bold),
   ForeColor = ModernStyles.TextDark,
 AutoSize = true,
 Location = new Point(25, 75)
         };
          panelPago.Controls.Add(lblImporteLabel);

 txtImporte.Location = new Point(25, 105);
     txtImporte.Size = new Size(250, 50);
    txtImporte.Font = new Font("Segoe UI", 18F);
     txtImporte.BorderStyle = BorderStyle.FixedSingle;
 txtImporte.BackColor = Color.White;
    txtImporte.ForeColor = ModernStyles.TextDark;
   txtImporte.TextAlign = HorizontalAlignment.Right;
     txtImporte.TabIndex = 6;
        txtImporte.Visible = true;
  
    // ✅ CORREGIDO: Solo calcular vuelto sin validar mientras escribe
    txtImporte.TextChanged += (s, e) =>
    {
     if (double.TryParse(txtImporte.Text, out double importe) && importe > 0 && _venta != null)
  {
     // Calcular vuelto directamente sin validaciones ni excepciones
            double vuelto = importe - _venta.Total;
  
       // Mostrar vuelto (puede ser negativo si falta dinero)
          if (vuelto >= 0)
        {
   txtVuelto.Text = $"$ {vuelto:N2}";
         txtVuelto.ForeColor = ModernStyles.SuccessGreen;
      }
        else
            {
              // Si es negativo, mostrar cuánto falta en rojo pero sin error
    txtVuelto.Text = $"$ {Math.Abs(vuelto):N2} (falta)";
       txtVuelto.ForeColor = Color.FromArgb(220, 53, 69);
            }
    }
    else
    {
      txtVuelto.Text = "$ 0,00";
 txtVuelto.ForeColor = ModernStyles.InfoBlue;
    }
  };

    panelPago.Controls.Add(txtImporte);

 // Vuelto
     Label lblVueltoLabel = new Label
      {
       Text = "Vuelto",
         Font = new Font("Segoe UI", 12F, FontStyle.Bold),
   ForeColor = ModernStyles.TextDark,
    AutoSize = true,
    Location = new Point(305, 75)
    };
   panelPago.Controls.Add(lblVueltoLabel);

     txtVuelto.Location = new Point(305, 105);
    txtVuelto.Size = new Size(250, 50);
       txtVuelto.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        txtVuelto.ForeColor = ModernStyles.InfoBlue;
         txtVuelto.Text = "$ 0,00";
      txtVuelto.BorderStyle = BorderStyle.FixedSingle;
     txtVuelto.BackColor = Color.FromArgb(240, 248, 255);
  txtVuelto.TextAlign = ContentAlignment.MiddleRight;
       txtVuelto.Visible = true;
       panelPago.Controls.Add(txtVuelto);

       // Botón Concretar Venta
   btnConcretarVenta.Text = "✅ CONCRETAR VENTA";
         btnConcretarVenta.Location = new Point(595, 75);
     btnConcretarVenta.Size = new Size(350, 80);
     btnConcretarVenta.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
     btnConcretarVenta.Cursor = Cursors.Hand;
     btnConcretarVenta.TabIndex = 7;
 btnConcretarVenta.Visible = true;
    ModernStyles.ApplyPrimaryButtonStyle(btnConcretarVenta);
      panelPago.Controls.Add(btnConcretarVenta);

    // Botón Cancelar Venta
      btnRealizarVenta.Text = "❌ Cancelar Venta";
    btnRealizarVenta.Location = new Point(975, 75);
       btnRealizarVenta.Size = new Size(340, 80);
       btnRealizarVenta.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
     btnRealizarVenta.Cursor = Cursors.Hand;
   btnRealizarVenta.TabIndex = 8;
    btnRealizarVenta.Visible = true;
   ModernStyles.ApplySecondaryButtonStyle(btnRealizarVenta);
      panelPago.Controls.Add(btnRealizarVenta);

   // Instrucciones
      Label lblInstrucciones = new Label
  {
   Text = "💡 Seleccione un producto de la tabla, elija el talle y la cantidad, luego presione AGREGAR AL CARRITO",
    Font = new Font("Segoe UI", 10F),
      ForeColor = ModernStyles.TextLight,
    AutoSize = true,
 Location = new Point(25, 190)
    };
     panelPago.Controls.Add(lblInstrucciones);

    this.Controls.Add(panelPago);

 // Guardar referencia al DataGrid de productos
 this.Tag = dgvProductos;

 // Configurar eventos de navegación
 ConfigurarEventosTeclado();
        }

      private void SeleccionarProducto(Indumentaria producto)
        {
            _indumentaria = producto;
 LblDescripcion.Text = producto.Descripcion;
      lblPrecio.Text = $"$ {producto.Precio:N2}";
  TxtCantidad.Focus();
        }

   private void ConfigurarEventosTeclado()
      {
   // Los eventos básicos ya están configurados en el constructor
      // Enter en comboTalle va a TxtCantidad
            comboTalle.KeyDown += (s, e) =>
            {
           if (e.KeyCode == Keys.Enter)
       {
        TxtCantidad.Focus();
      e.Handled = true;
          e.SuppressKeyPress = true;
    }
     };

            // Enter en TxtCantidad agrega al carrito
        TxtCantidad.KeyDown += (s, e) =>
       {
      if (e.KeyCode == Keys.Enter)
         {
       btnAgregarIndumentaria_Click(this, EventArgs.Empty);
 e.Handled = true;
        e.SuppressKeyPress = true;
 }
  };

            // Enter en txtImporte calcula vuelto
            txtImporte.KeyDown += (s, e) =>
          {
         if (e.KeyCode == Keys.Enter)
       {
       txtImporte_Leave(this, EventArgs.Empty);
             btnConcretarVenta.Focus();
        e.Handled = true;
           e.SuppressKeyPress = true;
    }
            };
        }

        public void InicializarVista()
   {
            this._venta = _presentador.CrearNuevaVenta();
            
  // ✅ Configurar BindingSource de Venta
      this.bsVenta.DataSource = _venta;
     
     // ✅ NO usar lineaDeVentaBindingSource - controlamos el DataGridView manualmente
   // Inicializar con lista vacía
        dataGridLineaVenta.DataSource = new List<TFI.Vista.DTOs.LineaDeVentaDTO>();
    
    // ✅ Inicializar el lblTotal
  lblTotal.Text = "$ 0,00";
       
        ListarTalles(_presentador.GetTalles());
   ListarIndumentarias(_presentador.GetIndumentarias());
        }

     public void ListarTalles(List<Talle> talles)
     {
      this.bsTalle.DataSource = talles;
        }

    public void ListarIndumentarias(List<Indumentaria> indumentarias)
  {
    // Obtener el DataGrid de productos desde el Tag del formulario
   if (this.Tag is DataGridView dgvProductos)
      {
      dgvProductos.DataSource = indumentarias;
       }
    }

 public void MostrarError(string mensaje)
        {
    MessageBox.Show(mensaje, "⚠️ Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    public void MostrarIndumentaria(Indumentaria ind)
        {
   this._indumentaria = ind;
this.LblDescripcion.Text = ind.Descripcion;
  this.lblPrecio.Text = $"$ {ind.Precio:N2}";
     }

    private void TxtCodigo_Leave(object sender, EventArgs e)
     {
     if (!string.IsNullOrEmpty(TxtCodigo.Text))
    {
    if (int.TryParse(TxtCodigo.Text, out int codigo))
      {
    _presentador.IngresarIndumentaria(codigo);
      }
       else
     {
    MostrarError("El código debe ser un número válido.");
          }
            }
        }

        private void btnAgregarIndumentaria_Click(object sender, EventArgs e)
  {
            if (_indumentaria == null)
     {
     MostrarError("Primero debe buscar un producto.");
TxtCodigo.Focus();
                return;
            }
  
            if (comboTalle.SelectedItem == null)
            {
        MostrarError("Debe seleccionar un talle.");
 comboTalle.Focus();
                return;
            }
         
     if (!int.TryParse(TxtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
      MostrarError("La cantidad debe ser un número mayor a 0.");
            TxtCantidad.Focus();
   return;
    }
          
       var talle = (Talle)comboTalle.SelectedItem;
            _presentador.RegistrarLineaDeVenta(_indumentaria, cantidad, talle.Id);
     }

        private void txtImporte_Leave(object sender, EventArgs e)
{
   // ✅ CORREGIDO: Solo actualizar la visualización del vuelto, no validar aquí
   // La validación se hace al presionar "Concretar Venta"
   if (double.TryParse(txtImporte.Text, out double importe) && _venta != null)
  {
     double vuelto = importe - _venta.Total;
    
    if (vuelto >= 0)
    {
        txtVuelto.Text = $"$ {vuelto:N2}";
        txtVuelto.ForeColor = ModernStyles.SuccessGreen;
    }
    else
    {
        txtVuelto.Text = $"$ {Math.Abs(vuelto):N2} (falta)";
        txtVuelto.ForeColor = Color.FromArgb(220, 53, 69);
    }
    }
    else
   {
     txtVuelto.Text = "$ 0,00";
   txtVuelto.ForeColor = ModernStyles.InfoBlue;
         }
     }

  public void ActualizarVista(Venta venta)
        {
          this._venta = venta;
      this.bsVenta.DataSource = venta;
      
  // ✅ SOLUCIÓN CRÍTICA: Capturar los valores ANTES de que EF Core intente lazy loading
  // Acceder a las propiedades de Indumentaria directamente desde el objeto en memoria
  var lineasDTO = venta.LineaDeVentas.Select(ldv => new TFI.Vista.DTOs.LineaDeVentaDTO
          {
       // ✅ IMPORTANTE: Acceder a la propiedad del objeto Indumentaria que YA está en memoria
   // NO dejar que EF Core intente cargarla desde la base de datos
   DescripcionIndumentaria = ldv.Indumentaria?.Descripcion ?? "Sin descripción",
      Cantidad = ldv.Cantidad,
    PrecioIndumentaria = ldv.Indumentaria?.Precio ?? 0,
  Subtotal = (ldv.Indumentaria?.Precio ?? 0) * ldv.Cantidad
    }).ToList();
       
     // ✅ Asignar la lista de DTOs directamente al DataGridView
        dataGridLineaVenta.DataSource = null; // Limpiar primero
dataGridLineaVenta.DataSource = lineasDTO;
     
      // ✅ Configurar formato de columnas del DataGrid
ConfigurarColumnasDataGrid();

       // ✅ CRÍTICO: Actualizar manualmente el lblTotal porque no tiene binding automático
 lblTotal.Text = $"$ {venta.Total:N2}";

       LimpiarCampos();
   }

        private void ConfigurarColumnasDataGrid()
        {
   // ✅ Las columnas ya están correctamente configuradas en CrearLayoutModerno()
    // Solo aplicar estilos adicionales del grid
    
    dataGridLineaVenta.RowTemplate.Height = 35;
    dataGridLineaVenta.ColumnHeadersHeight = 40;
    dataGridLineaVenta.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 5, 5, 5);
    dataGridLineaVenta.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
 dataGridLineaVenta.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
    dataGridLineaVenta.ColumnHeadersDefaultCellStyle.BackColor = ModernStyles.PrimaryGreen;
    dataGridLineaVenta.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
    dataGridLineaVenta.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
    dataGridLineaVenta.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 240, 255);
    dataGridLineaVenta.DefaultCellStyle.SelectionForeColor = ModernStyles.TextDark;
    dataGridLineaVenta.EnableHeadersVisualStyles = false;
    dataGridLineaVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
    dataGridLineaVenta.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
    dataGridLineaVenta.GridColor = Color.FromArgb(222, 226, 230);
}

        private void LimpiarCampos()
        {
 TxtCodigo.Clear();
      lblPrecio.Text = "$ 0,00";
    LblDescripcion.Text = "-";
     TxtCantidad.Text = "1";
    if (comboTalle.Items.Count > 0)
              comboTalle.SelectedIndex = 0;
    _indumentaria = null;
          TxtCodigo.Focus();
        }

        private void TxtCantidad_Leave(object sender, EventArgs e)
{
        if (!int.TryParse(TxtCantidad.Text, out int cantidad) || cantidad <= 0)
       {
     MostrarError("La cantidad debe ser un número mayor a 0.");
         TxtCantidad.Text = "1";
            }
        }

        private void btnConcretarVenta_Click(object sender, EventArgs e)
     {
            if (_venta.LineaDeVentas.Count == 0)
     {
    MostrarError("No hay productos en el carrito.");
                TxtCodigo.Focus();
       return;
      }
       
            if (string.IsNullOrWhiteSpace(txtImporte.Text) || !double.TryParse(txtImporte.Text, out double importe))
   {
                MostrarError("Debe ingresar el importe pagado.");
           txtImporte.Focus();
      return;
            }
   
  if (importe < _venta.Total)
            {
 MostrarError($"El importe pagado ($ {importe:N2}) es menor al total ($ {_venta.Total:N2}).");
          txtImporte.Focus();
  txtImporte.SelectAll();
        return;
 }
            
            _presentador.ConfirmarVenta();
        }

 public void MostrarExito(string mensaje)
      {
     MessageBox.Show(mensaje, "✓ Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LimpiarVentana()
        {
    InicializarVista();
 txtVuelto.Text = "$ 0,00";
         TxtCantidad.Text = "1";
  txtImporte.Clear();
   }

        private void btnEliminarIndumentaria_Click(object sender, EventArgs e)
     {
         if (dataGridLineaVenta.SelectedRows.Count > 0)
   {
       var fila = dataGridLineaVenta.CurrentRow.Index;
            _presentador.EliminarLineaDeVenta(fila);
     }
      else
       {
          MostrarError("Debe seleccionar un producto para eliminar.");
   }
  }

        private void btnRealizarVenta_Click(object sender, EventArgs e)
        {
            if (_venta.LineaDeVentas.Count > 0)
       {
    var result = MessageBox.Show(
        "¿Está seguro de cancelar la venta actual? Se perderán todos los productos agregados.",
         "Confirmar Cancelación",
  MessageBoxButtons.YesNo,
           MessageBoxIcon.Question
      );
          
         if (result == DialogResult.Yes)
    {
       LimpiarVentana();
        MostrarExito("Venta cancelada.");
     }
          }
            else
 {
    this.Close(); // Volver al menú si no hay productos
        }
        }
    }
}
