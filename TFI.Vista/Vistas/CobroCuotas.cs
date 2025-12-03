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
    /// <summary>
    /// Vista para el módulo de Cobro de Cuotas
    /// Sigue el mismo patrón y estilo visual que VentaIndumentaria
    /// </summary>
public partial class CobroCuotas : Form, ICobroCuotasVista
    {
   private CobroCuotasPresentador _presentador;
        private Alumno _alumnoActual;
    private List<Cuota> _cuotasActuales;
        
 // Controles de la UI
        private TextBox txtCodigoBarras;
        private TextBox txtDNI;
        private Label lblNombreAlumno;
        private Label lblDNIAlumno;
   private DataGridView dgvCuotas;
    private Label lblTotal;
        private Label lblRecargo;
        private Label lblSubtotal;
        private TextBox txtImportePagado;
        private Label lblVuelto;
  private ComboBox cboMedioPago;
        private Button btnBuscarCodigo;
        private Button btnBuscarDNI;
        private Button btnCobrar;
 private Button btnCancelar;
    private CheckBox chkSoloPendientes;

        public CobroCuotas(CobroCuotasPresentador presentador)
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
                 this.Close();
           }
        };

            // Aplicar estilos modernos
            ConfigurarEstilosModernos();
    InicializarVista();
        }

     private void ConfigurarEstilosModernos()
        {
            // Configurar formulario
       ModernStyles.ApplyFormStyle(this);
            this.Size = new Size(1400, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
  this.Text = "IPT-NET - Cobro de Cuotas";
            this.BackColor = ModernStyles.BackgroundLight;
            
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

  Label lblTitulo = new Label
            {
           Text = "Cobro de Cuotas",  // Sin emoji
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

            // ===== PANEL IZQUIERDO - BÚSQUEDA =====
            Panel panelBusqueda = new Panel
   {
 Location = new Point(20, 120),
    Size = new Size(650, 300),
                BackColor = ModernStyles.CardBackground
  };
            ModernStyles.ApplyCardPanelStyle(panelBusqueda);
            
        Label lblBusqueda = new Label
  {
    Text = "Buscar Cuota",  // Sin emoji
            Font = new Font("Segoe UI", 18F, FontStyle.Bold),
      ForeColor = ModernStyles.TextDark,
         AutoSize = true,
  Location = new Point(25, 20)
          };
         panelBusqueda.Controls.Add(lblBusqueda);

            // Búsqueda por Código de Barras
    Label lblCodigoBarras = new Label
    {
        Text = "Código de Barras",
         Font = new Font("Segoe UI", 12F, FontStyle.Bold),
  ForeColor = ModernStyles.TextDark,
    AutoSize = true,
          Location = new Point(25, 70)
            };
panelBusqueda.Controls.Add(lblCodigoBarras);

        txtCodigoBarras = new TextBox
    {
      Location = new Point(25, 100),
        Size = new Size(420, 40),
   Font = new Font("Segoe UI", 14F),
         BorderStyle = BorderStyle.FixedSingle,
          BackColor = Color.White,
    TabIndex = 0
    };
         panelBusqueda.Controls.Add(txtCodigoBarras);

 btnBuscarCodigo = new Button
            {
         Text = "Buscar",
    Location = new Point(460, 100),
          Size = new Size(160, 50),  // Aumentado de 45 a 50
     Font = new Font("Segoe UI", 12F, FontStyle.Bold),  // Reducido de 13F a 12F
        Cursor = Cursors.Hand,
    TabIndex = 1,
         Padding = new Padding(0, 5, 0, 5)  // Padding interno para centrar texto
  };
            ModernStyles.ApplyPrimaryButtonStyle(btnBuscarCodigo);
            btnBuscarCodigo.Click += BtnBuscarCodigo_Click;
    panelBusqueda.Controls.Add(btnBuscarCodigo);

            // Separador
        Panel separador1 = new Panel
{
             Location = new Point(25, 160),
          Size = new Size(595, 2),
   BackColor = Color.FromArgb(220, 220, 220)
            };
     panelBusqueda.Controls.Add(separador1);

        // Búsqueda por DNI
            Label lblDNI = new Label
            {
  Text = "DNI del Alumno",
 Font = new Font("Segoe UI", 12F, FontStyle.Bold),
       ForeColor = ModernStyles.TextDark,
     AutoSize = true,
      Location = new Point(25, 180)
            };
    panelBusqueda.Controls.Add(lblDNI);

            txtDNI = new TextBox
      {
         Location = new Point(25, 210),
      Size = new Size(420, 40),
                Font = new Font("Segoe UI", 14F),
    BorderStyle = BorderStyle.FixedSingle,
       BackColor = Color.White,
                TabIndex = 2
 };
  panelBusqueda.Controls.Add(txtDNI);

       btnBuscarDNI = new Button
   {
     Text = "Buscar",
     Location = new Point(460, 210),
       Size = new Size(160, 50),  // Aumentado de 45 a 50
  Font = new Font("Segoe UI", 12F, FontStyle.Bold),  // Reducido de 13F a 12F
    Cursor = Cursors.Hand,
      TabIndex = 3,
  Padding = new Padding(0, 5, 0, 5)  // Padding interno para centrar texto
     };
            ModernStyles.ApplyPrimaryButtonStyle(btnBuscarDNI);
         btnBuscarDNI.Click += BtnBuscarDNI_Click;
    panelBusqueda.Controls.Add(btnBuscarDNI);

      this.Controls.Add(panelBusqueda);

        // ===== PANEL DERECHO SUPERIOR - DATOS DEL ALUMNO =====
    Panel panelAlumno = new Panel
        {
     Location = new Point(690, 120),
             Size = new Size(670, 300),
                BackColor = ModernStyles.CardBackground
   };
   ModernStyles.ApplyCardPanelStyle(panelAlumno);
      
         Label lblInfoAlumno = new Label
            {
              Text = "Datos del Alumno",  // Sin emoji
        Font = new Font("Segoe UI", 18F, FontStyle.Bold),
       ForeColor = ModernStyles.TextDark,
         AutoSize = true,
        Location = new Point(25, 20)
 };
       panelAlumno.Controls.Add(lblInfoAlumno);

       Label lblNombreLabel = new Label
            {
       Text = "Nombre:",
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
      ForeColor = ModernStyles.TextMedium,
      AutoSize = true,
         Location = new Point(25, 80)
      };
       panelAlumno.Controls.Add(lblNombreLabel);

            lblNombreAlumno = new Label
          {
       Text = "-",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
       ForeColor = ModernStyles.TextDark,
           AutoSize = false,
    Size = new Size(620, 40),
     Location = new Point(25, 105),
       TextAlign = ContentAlignment.MiddleLeft
   };
 panelAlumno.Controls.Add(lblNombreAlumno);

      Label lblDNILabel = new Label
            {
     Text = "DNI:",
   Font = new Font("Segoe UI", 11F, FontStyle.Bold),
     ForeColor = ModernStyles.TextMedium,
          AutoSize = true,
      Location = new Point(25, 160)
     };
    panelAlumno.Controls.Add(lblDNILabel);

   lblDNIAlumno = new Label
        {
      Text = "-",
         Font = new Font("Segoe UI", 16F, FontStyle.Bold),
         ForeColor = ModernStyles.InfoBlue,
    AutoSize = false,
           Size = new Size(620, 40),
     Location = new Point(25, 185),
     TextAlign = ContentAlignment.MiddleLeft
     };
       panelAlumno.Controls.Add(lblDNIAlumno);

            this.Controls.Add(panelAlumno);

   // ===== PANEL CENTRAL - CUOTAS =====
            Panel panelCuotas = new Panel
            {
       Location = new Point(20, 440),
      Size = new Size(900, 340),
      BackColor = ModernStyles.CardBackground
            };
   ModernStyles.ApplyCardPanelStyle(panelCuotas);
          
     Label lblCuotas = new Label
            {
                Text = "Cuotas del Alumno",  // Sin emoji
        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
      ForeColor = ModernStyles.TextDark,
     AutoSize = true,
         Location = new Point(20, 15)
            };
            panelCuotas.Controls.Add(lblCuotas);

   chkSoloPendientes = new CheckBox
      {
  Text = "Solo pendientes",
   Location = new Point(700, 20),
    Size = new Size(180, 25),
      Font = new Font("Segoe UI", 10F),
           Checked = true,
       TabIndex = 4
  };
   chkSoloPendientes.CheckedChanged += ChkSoloPendientes_CheckedChanged;
   panelCuotas.Controls.Add(chkSoloPendientes);

            // DataGridView para cuotas
        dgvCuotas = new DataGridView
    {
         Location = new Point(10, 55),
 Size = new Size(880, 275),
        ReadOnly = false,
        AllowUserToAddRows = false,
       AllowUserToDeleteRows = false,
     SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = true,
    AutoGenerateColumns = false,
 TabIndex = 5,
      RowHeadersVisible = false,
      AllowUserToResizeRows = false,
                ScrollBars = ScrollBars.Vertical,
   BackgroundColor = Color.White,
  BorderStyle = BorderStyle.FixedSingle
   };
      ModernStyles.ApplyDataGridViewStyle(dgvCuotas);

  // Configurar columnas
            dgvCuotas.Columns.Add(new DataGridViewCheckBoxColumn
    {
          Name = "Seleccionada",
     DataPropertyName = "Seleccionada",
     HeaderText = "✓",
        Width = 40,
     ReadOnly = false
  });

            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
    {
  Name = "Periodo",
       DataPropertyName = "Periodo",
  HeaderText = "Período",
  Width = 120,
                ReadOnly = true
            });

            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
   {
       Name = "CodigoBarras",
       DataPropertyName = "CodigoBarras",
              HeaderText = "Código Barras",
      Width = 150,
       ReadOnly = true
  });

    dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
   {
     Name = "MontoOriginal",
         DataPropertyName = "MontoOriginal",
          HeaderText = "Monto",
           Width = 100,
           ReadOnly = true,
           DefaultCellStyle = new DataGridViewCellStyle
        {
         Format = "C2",
   Alignment = DataGridViewContentAlignment.MiddleRight
        }
          });

   dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
       Name = "PrimerVencimiento",
     DataPropertyName = "PrimerVencimiento",
                HeaderText = "1º Venc.",
    Width = 100,
      ReadOnly = true,
    DefaultCellStyle = new DataGridViewCellStyle
        {
     Format = "dd/MM/yyyy",
    Alignment = DataGridViewContentAlignment.MiddleCenter
         }
            });

            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
        {
  Name = "Estado",
              DataPropertyName = "Estado",
       HeaderText = "Estado",
    Width = 100,
        ReadOnly = true,
      DefaultCellStyle = new DataGridViewCellStyle
                {
Alignment = DataGridViewContentAlignment.MiddleCenter
      }
         });

      dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
  {
        Name = "Recargo",
  DataPropertyName = "Recargo",
       HeaderText = "Recargo",
          Width = 90,
 ReadOnly = true,
    DefaultCellStyle = new DataGridViewCellStyle
    {
   Format = "C2",
          Alignment = DataGridViewContentAlignment.MiddleRight,
       ForeColor = Color.FromArgb(220, 53, 69)
    }
        });

            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MontoAPagar",
                DataPropertyName = "MontoAPagar",
  HeaderText = "Total",
       Width = 110,
         ReadOnly = true,
 DefaultCellStyle = new DataGridViewCellStyle
           {
   Format = "C2",
 Alignment = DataGridViewContentAlignment.MiddleRight,
          Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
        ForeColor = ModernStyles.PrimaryGreen
          }
          });

            dgvCuotas.CellValueChanged += DgvCuotas_CellValueChanged;
      dgvCuotas.CurrentCellDirtyStateChanged += DgvCuotas_CurrentCellDirtyStateChanged;

        panelCuotas.Controls.Add(dgvCuotas);
    this.Controls.Add(panelCuotas);

      // ===== PANEL DERECHO - TOTALES Y PAGO =====
            Panel panelPago = new Panel
            {
        Location = new Point(940, 440),
          Size = new Size(420, 340),
       BackColor = ModernStyles.CardBackground
      };
            ModernStyles.ApplyCardPanelStyle(panelPago);
            
            Label lblPago = new Label
     {
     Text = "Totales",  // Sin emoji
      Font = new Font("Segoe UI", 16F, FontStyle.Bold),
             ForeColor = ModernStyles.TextDark,
  AutoSize = true,
    Location = new Point(20, 15)
     };
    panelPago.Controls.Add(lblPago);

  // Subtotal
            Label lblSubtotalLabel = new Label
     {
      Text = "Subtotal:",
    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
           ForeColor = ModernStyles.TextMedium,
        AutoSize = true,
            Location = new Point(20, 60)
            };
    panelPago.Controls.Add(lblSubtotalLabel);

         lblSubtotal = new Label
    {
        Text = "$ 0,00",
       Font = new Font("Segoe UI", 14F, FontStyle.Bold),
      ForeColor = ModernStyles.TextDark,
    AutoSize = false,
     Size = new Size(380, 30),
  Location = new Point(20, 85),
        TextAlign = ContentAlignment.MiddleRight
      };
    panelPago.Controls.Add(lblSubtotal);

      // Recargo
            Label lblRecargoLabel = new Label
            {
     Text = "Recargo:",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
 ForeColor = ModernStyles.TextMedium,
           AutoSize = true,
                Location = new Point(20, 125)
         };
          panelPago.Controls.Add(lblRecargoLabel);

      lblRecargo = new Label
            {
     Text = "$ 0,00",
         Font = new Font("Segoe UI", 14F, FontStyle.Bold),
       ForeColor = Color.FromArgb(220, 53, 69),
   AutoSize = false,
    Size = new Size(380, 30),
         Location = new Point(20, 150),
                TextAlign = ContentAlignment.MiddleRight
            };
            panelPago.Controls.Add(lblRecargo);

            // Separador
     Panel separador2 = new Panel
            {
    Location = new Point(20, 190),
                Size = new Size(380, 2),
         BackColor = Color.FromArgb(220, 220, 220)
            };
       panelPago.Controls.Add(separador2);

     // Total
            Label lblTotalLabel = new Label
            {
    Text = "TOTAL A PAGAR:",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
   ForeColor = ModernStyles.TextDark,
      AutoSize = true,
         Location = new Point(20, 200)
    };
        panelPago.Controls.Add(lblTotalLabel);

            lblTotal = new Label
            {
              Text = "$ 0,00",
             Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = ModernStyles.PrimaryGreen,
   AutoSize = false,
       Size = new Size(380, 40),
                Location = new Point(20, 225),
          TextAlign = ContentAlignment.MiddleRight
 };
            panelPago.Controls.Add(lblTotal);

      // Medio de Pago
            Label lblMedioPago = new Label
         {
   Text = "Medio de Pago:",
           Font = new Font("Segoe UI", 10F, FontStyle.Bold),
 ForeColor = ModernStyles.TextDark,
 AutoSize = true,
 Location = new Point(20, 275)
            };
panelPago.Controls.Add(lblMedioPago);

        cboMedioPago = new ComboBox
            {
                Location = new Point(150, 272),
      Size = new Size(250, 30),
         Font = new Font("Segoe UI", 10F),
   DropDownStyle = ComboBoxStyle.DropDownList,
      TabIndex = 6
    };
        cboMedioPago.Items.AddRange(new object[] { "Efectivo", "Tarjeta Débito", "Tarjeta Crédito", "Transferencia" });
    cboMedioPago.SelectedIndex = 0;
     panelPago.Controls.Add(cboMedioPago);

      this.Controls.Add(panelPago);

            // ===== PANEL INFERIOR - PAGO =====
          Panel panelAcciones = new Panel
          {
       Location = new Point(20, 790),
       Size = new Size(1340, 55),  // Aumentado de 50 a 55 para dar más espacio
   BackColor = Color.Transparent
         };

   // Importe Pagado
  Label lblImportePagado = new Label
  {
     Text = "Importe Pagado: $",
       Font = new Font("Segoe UI", 12F, FontStyle.Bold),
          ForeColor = ModernStyles.TextDark,
 AutoSize = true,
    Location = new Point(0, 15)  // Cambiado de 12 a 15 para centrar mejor
  };
  panelAcciones.Controls.Add(lblImportePagado);

  txtImportePagado = new TextBox
    {
Location = new Point(175, 10),  // Cambiado de 8 a 10
  Size = new Size(200, 35),
   Font = new Font("Segoe UI", 14F),
        BorderStyle = BorderStyle.FixedSingle,
       BackColor = Color.White,
        TextAlign = HorizontalAlignment.Right,
        TabIndex = 7
};
 txtImportePagado.TextChanged += TxtImportePagado_TextChanged;
   panelAcciones.Controls.Add(txtImportePagado);

         // Vuelto
  Label lblVueltoLabel = new Label
      {
  Text = "Vuelto: ",
    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
       ForeColor = ModernStyles.TextDark,
    AutoSize = true,
 Location = new Point(405, 15)  // Cambiado de 12 a 15
    };
    panelAcciones.Controls.Add(lblVueltoLabel);

lblVuelto = new Label
     {
           Text = "$ 0,00",
    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
  ForeColor = ModernStyles.SuccessGreen,
   AutoSize = true,
           Location = new Point(475, 13)  // Cambiado de 10 a 13
     };
     panelAcciones.Controls.Add(lblVuelto);

       // Botón Cobrar
 btnCobrar = new Button
  {
   Text = "COBRAR",  
    Location = new Point(950, 0),  // Cambiado de 2 a 0
   Size = new Size(200, 50),  // Aumentado de 45 a 50
  Font = new Font("Segoe UI", 15F, FontStyle.Bold),  // Reducido de 16F a 15F
        Cursor = Cursors.Hand,
  TabIndex = 8,
    Padding = new Padding(0, 5, 0, 5)  // Padding interno
    };
    ModernStyles.ApplyPrimaryButtonStyle(btnCobrar);
    btnCobrar.Click += BtnCobrar_Click;
  panelAcciones.Controls.Add(btnCobrar);

 // Botón Cancelar
      btnCancelar = new Button
 {
          Text = "Cancelar",
    Location = new Point(1160, 0),  // Cambiado de 2 a 0
    Size = new Size(180, 50),  // Aumentado de 45 a 50
     Font = new Font("Segoe UI", 13F, FontStyle.Bold),  // Reducido de 14F a 13F
 Cursor = Cursors.Hand,
      TabIndex = 9,
    Padding = new Padding(0, 5, 0, 5)  // Padding interno
   };
         ModernStyles.ApplySecondaryButtonStyle(btnCancelar);
  btnCancelar.Click += (s, e) => LimpiarVista();
     panelAcciones.Controls.Add(btnCancelar);

       this.Controls.Add(panelAcciones);

   // Configurar eventos de navegación con Enter
    ConfigurarEventosTeclado();
        }

    private void ConfigurarEventosTeclado()
        {
 // Enter en txtCodigoBarras ejecuta búsqueda
     txtCodigoBarras.KeyDown += (s, e) =>
            {
          if (e.KeyCode == Keys.Enter)
      {
         BtnBuscarCodigo_Click(this, EventArgs.Empty);
     e.Handled = true;
   e.SuppressKeyPress = true;
  }
      };

            // Enter en txtDNI ejecuta búsqueda
 txtDNI.KeyDown += (s, e) =>
{
 if (e.KeyCode == Keys.Enter)
            {
        BtnBuscarDNI_Click(this, EventArgs.Empty);
      e.Handled = true;
             e.SuppressKeyPress = true;
      }
            };

          // Enter en txtImportePagado va al botón Cobrar
txtImportePagado.KeyDown += (s, e) =>
   {
    if (e.KeyCode == Keys.Enter)
  {
           btnCobrar.Focus();
     e.Handled = true;
             e.SuppressKeyPress = true;
  }
            };
        }

        private void InicializarVista()
     {
     _cuotasActuales = new List<Cuota>();
      LimpiarVista();
        }

 // ===== IMPLEMENTACIÓN DE ICobroCuotasVista =====

   public void MostrarAlumno(Alumno alumno)
        {
    _alumnoActual = alumno;
     lblNombreAlumno.Text = alumno.NombreCompleto;
       lblDNIAlumno.Text = alumno.DNI.ToString("N0");
        }

        public void MostrarCuotas(List<Cuota> cuotas)
    {
            _cuotasActuales = cuotas;
   
            // Filtrar según checkbox
var cuotasFiltradas = chkSoloPendientes.Checked 
        ? cuotas.Where(c => c.Estado != EstadoCuota.Pagada).ToList()
   : cuotas;

        // Convertir a DTO
    var cuotasDTO = cuotasFiltradas.Select(c => new CuotaDTO
       {
          Id = c.Id,
                Periodo = c.PeriodoDescripcion,
     CodigoBarras = c.CodigoBarras,
 MontoOriginal = c.MontoOriginal,
   PrimerVencimiento = c.PrimerVencimiento,
     Estado = c.Estado.ToString(),
      Recargo = c.CalcularMontoAPagar() - c.MontoOriginal,
                MontoAPagar = c.CalcularMontoAPagar(),
   Seleccionada = c.Estado != EstadoCuota.Pagada // Preseleccionar solo las pendientes
            }).ToList();

  dgvCuotas.DataSource = null;
      dgvCuotas.DataSource = cuotasDTO;
     
            // Colorear filas según estado
   foreach (DataGridViewRow row in dgvCuotas.Rows)
            {
     var dto = (CuotaDTO)row.DataBoundItem;
      if (dto.Estado == "Pagada")
     {
     row.DefaultCellStyle.BackColor = Color.FromArgb(220, 252, 231);
            row.DefaultCellStyle.ForeColor = Color.FromArgb(22, 163, 74);
  }
     else if (dto.Estado == "Vencida")
        {
   row.DefaultCellStyle.BackColor = Color.FromArgb(254, 226, 226);
           row.DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
    }
 }
        }

  public void CalcularTotales(List<Cuota> cuotas)
        {
     if (cuotas == null || cuotas.Count == 0)
            {
     lblSubtotal.Text = "$ 0,00";
       lblRecargo.Text = "$ 0,00";
     lblTotal.Text = "$ 0,00";
  return;
        }

            double subtotal = cuotas.Sum(c => c.MontoOriginal);
        double recargo = cuotas.Sum(c => c.CalcularMontoAPagar() - c.MontoOriginal);
  double total = cuotas.Sum(c => c.CalcularMontoAPagar());

  lblSubtotal.Text = $"$ {subtotal:N2}";
       lblRecargo.Text = $"$ {recargo:N2}";
  lblTotal.Text = $"$ {total:N2}";
        }

     public void MostrarError(string mensaje)
  {
     MessageBox.Show(mensaje, "⚠️ Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

     public void MostrarAdvertencia(string mensaje)
        {
     MessageBox.Show(mensaje, "ℹ️ Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

  public void MostrarExito(string mensaje)
    {
            MessageBox.Show(mensaje, "✓ Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

   public void LimpiarVista()
        {
            txtCodigoBarras.Clear();
            txtDNI.Clear();
            lblNombreAlumno.Text = "-";
            lblDNIAlumno.Text = "-";
            dgvCuotas.DataSource = null;
 lblSubtotal.Text = "$ 0,00";
    lblRecargo.Text = "$ 0,00";
            lblTotal.Text = "$ 0,00";
    txtImportePagado.Clear();
            lblVuelto.Text = "$ 0,00";
       cboMedioPago.SelectedIndex = 0;
          _alumnoActual = null;
 _cuotasActuales = new List<Cuota>();
  txtCodigoBarras.Focus();
     }

        // ===== EVENTOS =====

        private void BtnBuscarCodigo_Click(object sender, EventArgs e)
        {
      _presentador.BuscarCuotaPorCodigoBarras(txtCodigoBarras.Text.Trim());
        }

        private void BtnBuscarDNI_Click(object sender, EventArgs e)
     {
            if (int.TryParse(txtDNI.Text.Trim(), out int dni))
        {
    _presentador.BuscarCuotasPorDNI(dni);
   }
            else
    {
      MostrarError("El DNI debe ser un número válido.");
            }
        }

        private void ChkSoloPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (_cuotasActuales != null && _cuotasActuales.Count > 0)
            {
        MostrarCuotas(_cuotasActuales);
      ActualizarTotalesDesdeSeleccion();
            }
        }

        private void DgvCuotas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
 {
   if (dgvCuotas.IsCurrentCellDirty && dgvCuotas.CurrentCell is DataGridViewCheckBoxCell)
          {
           dgvCuotas.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvCuotas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) // Columna de checkbox
      {
              ActualizarTotalesDesdeSeleccion();
            }
        }

        private void ActualizarTotalesDesdeSeleccion()
        {
            var cuotasSeleccionadas = GetCuotasSeleccionadas();
         CalcularTotales(cuotasSeleccionadas);
        }

        private List<Cuota> GetCuotasSeleccionadas()
        {
  var cuotasSeleccionadas = new List<Cuota>();
       
 if (dgvCuotas.DataSource is List<CuotaDTO> dtos)
   {
        foreach (var dto in dtos.Where(d => d.Seleccionada))
    {
            var cuota = _cuotasActuales.FirstOrDefault(c => c.Id == dto.Id);
           if (cuota != null)
         {
         cuotasSeleccionadas.Add(cuota);
             }
                }
    }

   return cuotasSeleccionadas;
 }

        private void TxtImportePagado_TextChanged(object sender, EventArgs e)
        {
 if (double.TryParse(txtImportePagado.Text, out double importe) && importe > 0)
            {
    var cuotasSeleccionadas = GetCuotasSeleccionadas();
         double total = cuotasSeleccionadas.Sum(c => c.CalcularMontoAPagar());
          double vuelto = importe - total;

 if (vuelto >= 0)
        {
        lblVuelto.Text = $"$ {vuelto:N2}";
 lblVuelto.ForeColor = ModernStyles.SuccessGreen;
          }
      else
        {
   lblVuelto.Text = $"$ {Math.Abs(vuelto):N2} (falta)";
                    lblVuelto.ForeColor = Color.FromArgb(220, 53, 69);
                }
}
            else
     {
     lblVuelto.Text = "$ 0,00";
    lblVuelto.ForeColor = ModernStyles.InfoBlue;
 }
        }

 private void BtnCobrar_Click(object sender, EventArgs e)
        {
            var cuotasSeleccionadas = GetCuotasSeleccionadas();

            if (cuotasSeleccionadas.Count == 0)
    {
                MostrarError("Debe seleccionar al menos una cuota para cobrar.");
    return;
            }

            if (!double.TryParse(txtImportePagado.Text, out double importe) || importe <= 0)
      {
     MostrarError("Debe ingresar un importe válido.");
          txtImportePagado.Focus();
       return;
   }

   string medioPago = cboMedioPago.SelectedItem?.ToString() ?? "Efectivo";
   _presentador.RegistrarPago(cuotasSeleccionadas, importe, medioPago);
        }

        // Designer required
        private void InitializeComponent()
  {
            this.SuspendLayout();
this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 261);
       this.Name = "CobroCuotas";
            this.Text = "CobroCuotas";
      this.ResumeLayout(false);
        }
    }
}
