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
    public partial class AdministrarIndumentaria : Form, IAdministrarIndumentariaVista
    {
        private readonly AdministrarIndumentariaPresentador _presentador;
 private bool _modoEdicion = false;

        // Controles del formulario
        private DataGridView dgvIndumentarias;
    private DataGridView dgvStocks;
        private TextBox txtCodigo;
   private TextBox txtDescripcion;
        private TextBox txtPrecio;
        private NumericUpDown numStockMinimo;
     private NumericUpDown numStockMaximo;
      private TextBox txtBuscar;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnCancelar;
        private Button btnEliminar;
        private Button btnModificar;
     private Button btnBuscar;
  private Label lblTitulo;
      private Label lblModo;
        private Panel panelFormulario;
        private Panel panelListado;

        public AdministrarIndumentaria(AdministrarIndumentariaPresentador presentador)
        {
            InitializeComponent();
    this._presentador = presentador;
            this.FormBorderStyle = FormBorderStyle.None;
    this.KeyPreview = true;
      presentador.SetVista(this);

            // ESC para volver
     this.KeyDown += (s, e) =>
            {
      if (e.KeyCode == Keys.Escape)
                {
         if (panelFormulario.Visible)
  {
     // Si el formulario está visible, cancelar edición
            BtnCancelar_Click(this, EventArgs.Empty);
        }
 else
   {
     // Si no, cerrar la ventana
            this.Close();
       }
           }
        };

    ConfigurarEstilosModernos();
     _presentador.CargarIndumentarias();
        }

        private void ConfigurarEstilosModernos()
        {
         // Configurar formulario
            ModernStyles.ApplyFormStyle(this);
            this.Size = new Size(1400, 850);
    this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "IPT-NET - Administrar Indumentaria";
            this.BackColor = ModernStyles.BackgroundLight;

        // Limpiar y recrear layout moderno
            this.Controls.Clear();
  CrearLayoutModerno();
  }

    private void CrearLayoutModerno()
        {
  // ===== PANEL SUPERIOR - TÍTULO =====
            Panel panelTitulo = new Panel
       {
             Location = new Point(20, 20),
                Size = new Size(1340, 80),
     BackColor = ModernStyles.CardBackground
     };
         ModernStyles.ApplyCardPanelStyle(panelTitulo);

  lblTitulo = new Label
        {
         Text = "ADMINISTRAR INDUMENTARIA",
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

            // ===== PANEL LISTADO =====
            panelListado = new Panel
            {
      Location = new Point(20, 120),
 Size = new Size(1340, 680),
      BackColor = ModernStyles.CardBackground
        };
            ModernStyles.ApplyCardPanelStyle(panelListado);

          Label lblListado = new Label
            {
       Text = "LISTADO DE PRODUCTOS",
            Font = new Font("Segoe UI", 18F, FontStyle.Bold),
      ForeColor = ModernStyles.TextDark,
     AutoSize = true,
       Location = new Point(25, 20)
         };
       panelListado.Controls.Add(lblListado);

     // Búsqueda
         Label lblBuscar = new Label
      {
     Text = "Buscar por código o descripción:",
        Font = new Font("Segoe UI", 10F),
     ForeColor = ModernStyles.TextMedium,
  AutoSize = true,
             Location = new Point(25, 65)
            };
            panelListado.Controls.Add(lblBuscar);

      txtBuscar = new TextBox
            {
   Location = new Point(25, 90),
       Size = new Size(1000, 30),
        Font = new Font("Segoe UI", 12F),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtBuscar.KeyDown += (s, e) =>
        {
          if (e.KeyCode == Keys.Enter)
           {
         BtnBuscar_Click(this, EventArgs.Empty);
        e.Handled = true;
      e.SuppressKeyPress = true;
    }
            };
      panelListado.Controls.Add(txtBuscar);

    btnBuscar = new Button
       {
    Text = "BUSCAR",
           Location = new Point(1040, 85),
    Size = new Size(270, 40),
    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
     Cursor = Cursors.Hand
 };
          ModernStyles.ApplyPrimaryButtonStyle(btnBuscar);
      btnBuscar.Click += BtnBuscar_Click;
         panelListado.Controls.Add(btnBuscar);

            // DataGrid de indumentarias
 dgvIndumentarias = new DataGridView
 {
                Location = new Point(25, 145),
   Size = new Size(1290, 440),
              ReadOnly = true,
    AllowUserToAddRows = false,
      AllowUserToDeleteRows = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
  AutoGenerateColumns = false
            };
        ModernStyles.ApplyDataGridViewStyle(dgvIndumentarias);

         // Configurar columnas
            dgvIndumentarias.Columns.Add(new DataGridViewTextBoxColumn
        {
         Name = "Id",
     DataPropertyName = "Id",
         HeaderText = "ID",
        Width = 80,
                Visible = false
      });

   dgvIndumentarias.Columns.Add(new DataGridViewTextBoxColumn
     {
         Name = "Codigo",
     DataPropertyName = "Codigo",
          HeaderText = "Código",
    Width = 120,
        DefaultCellStyle = new DataGridViewCellStyle { 
        Alignment = DataGridViewContentAlignment.MiddleCenter,
         Font = new Font("Segoe UI", 11F, FontStyle.Bold)
 }
  });

            dgvIndumentarias.Columns.Add(new DataGridViewTextBoxColumn
      {
      Name = "Descripcion",
        DataPropertyName = "Descripcion",
              HeaderText = "Descripción",
    Width = 700
});

       dgvIndumentarias.Columns.Add(new DataGridViewTextBoxColumn
        {
      Name = "Precio",
            DataPropertyName = "Precio",
      HeaderText = "Precio Unitario",
      Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle
         {
    Format = "C2",
               Alignment = DataGridViewContentAlignment.MiddleRight,
   Font = new Font("Segoe UI", 11F, FontStyle.Bold)
      }
            });

            dgvIndumentarias.SelectionChanged += DgvIndumentarias_SelectionChanged;
            panelListado.Controls.Add(dgvIndumentarias);

         // Botones de acción - SIN EMOJIS Y MÁS ANCHOS
        btnNuevo = new Button
            {
     Text = "NUEVO PRODUCTO",
  Location = new Point(25, 600),
   Size = new Size(350, 60),
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
   Cursor = Cursors.Hand
            };
        ModernStyles.ApplyPrimaryButtonStyle(btnNuevo);
          btnNuevo.Click += BtnNuevo_Click;
            panelListado.Controls.Add(btnNuevo);

     btnModificar = new Button
     {
   Text = "MODIFICAR",
       Location = new Point(395, 600),
   Size = new Size(350, 60),
          Font = new Font("Segoe UI", 13F, FontStyle.Bold),
     Cursor = Cursors.Hand
            };
            ModernStyles.ApplySecondaryButtonStyle(btnModificar);
            btnModificar.Click += BtnModificar_Click;
     panelListado.Controls.Add(btnModificar);

          btnEliminar = new Button
            {
                Text = "ELIMINAR",
        Location = new Point(765, 600),
           Size = new Size(350, 60),
   Font = new Font("Segoe UI", 13F, FontStyle.Bold),
          Cursor = Cursors.Hand,
         BackColor = Color.FromArgb(220, 53, 69),
         ForeColor = Color.White
         };
   btnEliminar.FlatStyle = FlatStyle.Flat;
         btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.MouseEnter += (s, e) => btnEliminar.BackColor = Color.FromArgb(200, 35, 51);
          btnEliminar.MouseLeave += (s, e) => btnEliminar.BackColor = Color.FromArgb(220, 53, 69);
    btnEliminar.Click += BtnEliminar_Click;
            panelListado.Controls.Add(btnEliminar);

    this.Controls.Add(panelListado);

            // ===== PANEL FORMULARIO =====
      panelFormulario = new Panel
    {
          Location = new Point(20, 120),
                Size = new Size(1340, 680),
       BackColor = ModernStyles.CardBackground,
        Visible = false
            };
            ModernStyles.ApplyCardPanelStyle(panelFormulario);

     lblModo = new Label
 {
        Text = "DATOS DEL PRODUCTO",
          Font = new Font("Segoe UI", 20F, FontStyle.Bold),
   ForeColor = ModernStyles.TextDark,
                AutoSize = true,
           Location = new Point(25, 20)
      };
    panelFormulario.Controls.Add(lblModo);

     // COLUMNA IZQUIERDA - Datos Básicos
      Label lblDatosBasicos = new Label
    {
    Text = "DATOS BASICOS",
          Font = new Font("Segoe UI", 14F, FontStyle.Bold),
    ForeColor = ModernStyles.PrimaryGreen,
         AutoSize = true,
     Location = new Point(25, 75)
        };
        panelFormulario.Controls.Add(lblDatosBasicos);

        // Código
    Label lblCodigo = new Label
   {
         Text = "Código *",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
  ForeColor = ModernStyles.TextDark,
     AutoSize = true,
    Location = new Point(25, 115)
     };
         panelFormulario.Controls.Add(lblCodigo);

  txtCodigo = new TextBox
        {
     Location = new Point(25, 145),
Size = new Size(250, 35),
    Font = new Font("Segoe UI", 12F),
        BorderStyle = BorderStyle.FixedSingle
            };
            panelFormulario.Controls.Add(txtCodigo);

   // Descripción
    Label lblDescripcion = new Label
       {
         Text = "Descripción *",
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
        ForeColor = ModernStyles.TextDark,
             AutoSize = true,
                Location = new Point(295, 115)
   };
   panelFormulario.Controls.Add(lblDescripcion);

   txtDescripcion = new TextBox
  {
                Location = new Point(295, 145),
     Size = new Size(600, 35),
      Font = new Font("Segoe UI", 12F),
         BorderStyle = BorderStyle.FixedSingle,
    MaxLength = 200
            };
         panelFormulario.Controls.Add(txtDescripcion);

            // Precio
      Label lblPrecio = new Label
  {
 Text = "Precio *",
    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
  ForeColor = ModernStyles.TextDark,
      AutoSize = true,
            Location = new Point(915, 115)
        };
            panelFormulario.Controls.Add(lblPrecio);

            txtPrecio = new TextBox
     {
    Location = new Point(915, 145),
       Size = new Size(200, 35),
    Font = new Font("Segoe UI", 12F),
       BorderStyle = BorderStyle.FixedSingle
 };
         panelFormulario.Controls.Add(txtPrecio);

            // SECCIÓN DE STOCK
            Label lblStockConfig = new Label
  {
            Text = "CONFIGURACION DE STOCK",
 Font = new Font("Segoe UI", 14F, FontStyle.Bold),
  ForeColor = ModernStyles.PrimaryGreen,
     AutoSize = true,
                Location = new Point(25, 205)
            };
            panelFormulario.Controls.Add(lblStockConfig);

    // Stock Mínimo
            Label lblStockMin = new Label
            {
 Text = "Stock Mínimo (para todos los talles)",
      Font = new Font("Segoe UI", 10F),
        ForeColor = ModernStyles.TextDark,
                AutoSize = true,
         Location = new Point(25, 245)
      };
   panelFormulario.Controls.Add(lblStockMin);

     numStockMinimo = new NumericUpDown
            {
  Location = new Point(25, 270),
                Size = new Size(200, 35),
     Font = new Font("Segoe UI", 12F),
                Minimum = 0,
       Maximum = 1000,
      Value = 5
   };
   panelFormulario.Controls.Add(numStockMinimo);

     // Stock Máximo
            Label lblStockMax = new Label
     {
        Text = "Stock Máximo (para todos los talles)",
    Font = new Font("Segoe UI", 10F),
ForeColor = ModernStyles.TextDark,
       AutoSize = true,
    Location = new Point(245, 245)
          };
      panelFormulario.Controls.Add(lblStockMax);

     numStockMaximo = new NumericUpDown
          {
    Location = new Point(245, 270),
        Size = new Size(200, 35),
        Font = new Font("Segoe UI", 12F),
      Minimum = 1,
          Maximum = 1000,
    Value = 50
            };
          panelFormulario.Controls.Add(numStockMaximo);

    // DataGrid de stocks por talle
      Label lblStocks = new Label
    {
                Text = "STOCK POR TALLE (hacer doble clic en Cantidad para editar)",
         Font = new Font("Segoe UI", 12F, FontStyle.Bold),
  ForeColor = ModernStyles.TextDark,
             AutoSize = true,
   Location = new Point(25, 330)
 };
     panelFormulario.Controls.Add(lblStocks);

    dgvStocks = new DataGridView
        {
         Location = new Point(25, 365),
                Size = new Size(1290, 220),
             AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        SelectionMode = DataGridViewSelectionMode.CellSelect,
   AutoGenerateColumns = false,
       EditMode = DataGridViewEditMode.EditOnEnter
       };
            ModernStyles.ApplyDataGridViewStyle(dgvStocks);

       dgvStocks.Columns.Add(new DataGridViewTextBoxColumn
  {
    Name = "Id",
    DataPropertyName = "Id",
        HeaderText = "ID",
                Visible = false
});

    dgvStocks.Columns.Add(new DataGridViewTextBoxColumn
            {
         Name = "Talle",
     DataPropertyName = "Talle",
         HeaderText = "Talle",
       Width = 150,
      ReadOnly = true,
       DefaultCellStyle = new DataGridViewCellStyle { 
          Font = new Font("Segoe UI", 12F, FontStyle.Bold),
   Alignment = DataGridViewContentAlignment.MiddleCenter
    }
       });

       dgvStocks.Columns.Add(new DataGridViewTextBoxColumn
    {
 Name = "Cantidad",
                DataPropertyName = "Cantidad",
      HeaderText = "Cantidad Actual",
        Width = 250,
     ReadOnly = false,
        DefaultCellStyle = new DataGridViewCellStyle { 
     Alignment = DataGridViewContentAlignment.MiddleCenter,
          Font = new Font("Segoe UI", 12F, FontStyle.Bold),
  BackColor = Color.FromArgb(255, 255, 200)
      }
     });

dgvStocks.Columns.Add(new DataGridViewTextBoxColumn
            {
           Name = "CantidadMinima",
   DataPropertyName = "CantidadMinima",
     HeaderText = "Stock Mínimo",
      Width = 200,
     ReadOnly = true,
          DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
  });

            dgvStocks.Columns.Add(new DataGridViewTextBoxColumn
     {
 Name = "CantidadMaxima",
                DataPropertyName = "CantidadMaxima",
    HeaderText = "Stock Máximo",
      Width = 200,
     ReadOnly = true,
DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
 });

       dgvStocks.Columns.Add(new DataGridViewTextBoxColumn
            {
        Name = "Estado",
     DataPropertyName = "Estado",
           HeaderText = "Estado",
      Width = 200,
     ReadOnly = true
  });

        // Validación al editar cantidad
     dgvStocks.CellValidating += (s, e) =>
      {
                if (dgvStocks.Columns[e.ColumnIndex].Name == "Cantidad")
                {
if (!int.TryParse(e.FormattedValue.ToString(), out int cantidad) || cantidad < 0)
      {
  e.Cancel = true;
         MessageBox.Show("La cantidad debe ser un número mayor o igual a 0.", 
        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         }
              }
  };

   panelFormulario.Controls.Add(dgvStocks);

      // Botones de acción del formulario - SIN EMOJIS Y MÁS ANCHOS
btnGuardar = new Button
    {
         Text = "GUARDAR CAMBIOS",
  Location = new Point(25, 605),
           Size = new Size(480, 60),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            Cursor = Cursors.Hand
         };
            ModernStyles.ApplyPrimaryButtonStyle(btnGuardar);
       btnGuardar.Click += BtnGuardar_Click;
   panelFormulario.Controls.Add(btnGuardar);

            btnCancelar = new Button
            {
    Text = "CANCELAR",
            Location = new Point(525, 605),
         Size = new Size(480, 60),
    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
         Cursor = Cursors.Hand
    };
 ModernStyles.ApplySecondaryButtonStyle(btnCancelar);
            btnCancelar.Click += BtnCancelar_Click;
            panelFormulario.Controls.Add(btnCancelar);

            this.Controls.Add(panelFormulario);
  }

      // ===== IMPLEMENTACIÓN DE LA INTERFAZ =====

        public void MostrarIndumentarias(List<Indumentaria> indumentarias)
        {
    var indumentariasDTO = indumentarias.Select(i => new IndumentariaDTO
      {
          Id = i.Id,
            Codigo = i.Codigo,
 Descripcion = i.Descripcion,
      Precio = i.Precio,
                StockTotal = 0 // Ya no lo mostramos en la grilla
  }).ToList();

            dgvIndumentarias.DataSource = null;
       dgvIndumentarias.DataSource = indumentariasDTO;
        }

        public void MostrarStocks(List<Stock> stocks)
        {
            var stocksDTO = stocks.Select(s => new StockDTO
     {
           Id = s.Id,
      Talle = s.Talle?.Descripcion ?? "N/A",
        Cantidad = s.Cantidad,
      CantidadMinima = s.CantidadMinima,
      CantidadMaxima = s.CantidadMaxima
            }).ToList();

   dgvStocks.DataSource = null;
   dgvStocks.DataSource = stocksDTO;
      }

        public void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void MostrarExito(string mensaje)
  {
            MessageBox.Show(mensaje, "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LimpiarFormulario()
        {
      txtCodigo.Clear();
  txtDescripcion.Clear();
            txtPrecio.Clear();
  numStockMinimo.Value = 5;
      numStockMaximo.Value = 50;
 dgvStocks.DataSource = null;
       _modoEdicion = false;
  txtCodigo.Enabled = true;
        }

        public void CargarIndumentariaEnFormulario(Indumentaria indumentaria)
        {
      txtCodigo.Text = indumentaria.Codigo.ToString();
  txtDescripcion.Text = indumentaria.Descripcion;
    txtPrecio.Text = indumentaria.Precio.ToString("F2");

            _modoEdicion = true;
            lblModo.Text = "MODIFICAR PRODUCTO";
  txtCodigo.Enabled = false;
        }

        public bool ConfirmarEliminacion(string mensaje)
 {
     return MessageBox.Show(mensaje, "Confirmar Eliminación",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

      // ===== EVENTOS =====

        private void BtnNuevo_Click(object sender, EventArgs e)
      {
      _presentador.NuevaIndumentaria();
       
            // Cargar talles vacíos para que el usuario pueda ingresar cantidades
var talles = _presentador.GetType().GetMethod("GetTalles")?.Invoke(_presentador, null) as List<Talle>;
  if (talles != null)
        {
    var stocksVacios = talles.Select(t => new StockDTO
    {
             Id = 0,
         Talle = t.Descripcion,
  Cantidad = 0,
         CantidadMinima = (int)numStockMinimo.Value,
         CantidadMaxima = (int)numStockMaximo.Value
        }).ToList();

      dgvStocks.DataSource = stocksVacios;
    }

       lblModo.Text = "NUEVO PRODUCTO";
   MostrarFormulario();
            txtCodigo.Focus();
      }

        private void BtnModificar_Click(object sender, EventArgs e)
      {
     if (dgvIndumentarias.SelectedRows.Count == 0)
        {
        MostrarError("Debe seleccionar un producto para modificar.");
                return;
       }

   var indumentariaDTO = (IndumentariaDTO)dgvIndumentarias.SelectedRows[0].DataBoundItem;
            _presentador.SeleccionarIndumentaria(indumentariaDTO.Id);
            MostrarFormulario();
         txtDescripcion.Focus();
        }

   private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
    return;

    if (!int.TryParse(txtCodigo.Text, out int codigo))
     {
                MostrarError("El código debe ser un número válido.");
         return;
            }

      if (!double.TryParse(txtPrecio.Text, out double precio))
  {
                MostrarError("El precio debe ser un número válido.");
                return;
      }

        // Obtener cantidades por talle desde el DataGrid
       var stocksPorTalle = new Dictionary<string, int>();
    if (dgvStocks.DataSource is List<StockDTO> stocksDTO)
      {
        foreach (var stock in stocksDTO)
        {
   stocksPorTalle[stock.Talle] = stock.Cantidad;
      }
 }

 _presentador.GuardarIndumentaria(
      codigo,
            txtDescripcion.Text.Trim(),
                precio,
           (int)numStockMinimo.Value,
     (int)numStockMaximo.Value,
         stocksPorTalle
            );

     OcultarFormulario();
        }

        private bool ValidarCampos()
        {
          if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
    MostrarError("El código es obligatorio.");
          txtCodigo.Focus();
    return false;
     }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
    MostrarError("La descripción es obligatoria.");
       txtDescripcion.Focus();
      return false;
   }

         if (string.IsNullOrWhiteSpace(txtPrecio.Text))
       {
              MostrarError("El precio es obligatorio.");
     txtPrecio.Focus();
     return false;
     }

         if (numStockMinimo.Value >= numStockMaximo.Value)
          {
         MostrarError("El stock mínimo debe ser menor al stock máximo.");
       numStockMinimo.Focus();
    return false;
    }

 return true;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
         OcultarFormulario();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
      if (dgvIndumentarias.SelectedRows.Count == 0)
            {
       MostrarError("Debe seleccionar una indumentaria para eliminar.");
  return;
        }

    var indumentariaDTO = (IndumentariaDTO)dgvIndumentarias.SelectedRows[0].DataBoundItem;
  _presentador.EliminarIndumentaria(indumentariaDTO.Id);
}

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            _presentador.BuscarIndumentarias(txtBuscar.Text.Trim());
     }

        private void DgvIndumentarias_SelectionChanged(object sender, EventArgs e)
        {
       // Ya no cargamos automáticamente, solo al hacer clic en "Modificar"
    }

        private void MostrarFormulario()
        {
        panelListado.Visible = false;
            panelFormulario.Visible = true;
        }

        private void OcultarFormulario()
      {
            panelFormulario.Visible = false;
      panelListado.Visible = true;
        }
    }
}
