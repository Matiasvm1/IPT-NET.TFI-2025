using System;
using System.Drawing;
using System.Windows.Forms;
using TFI.Dominio.Interfaces;
using TFI.Vista.Presentadores;
using TFI.Vista.Styles;

namespace TFI.Vista
{
    public partial class MenuPrincipal : Form, IMenuPrincipalVista
    {
        private MenuPrincipalPresentador _presentador;
        
        public MenuPrincipal(MenuPrincipalPresentador presentador)
        {
         InitializeComponent();
            _presentador = presentador;
            
    // Configuración del formulario
   this.FormBorderStyle = FormBorderStyle.None;
     this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(900, 600);
            this.KeyPreview = true; // ✅ Habilitar captura de teclas
         
       // ✅ ESC para cerrar
       this.KeyDown += (s, e) =>
        {
                if (e.KeyCode == Keys.Escape)
      {
         var result = MessageBox.Show(
     "¿Está seguro que desea salir de la aplicación?",
        "Confirmar Salida",
   MessageBoxButtons.YesNo,
       MessageBoxIcon.Question
  );
 
            if (result == DialogResult.Yes)
           {
     Application.Exit();
 }
 }
     };
         
   // Aplicar estilos modernos
            ConfigurarDiseñoModerno();
     }

        private void ConfigurarDiseñoModerno()
        {
    // Aplicar estilo al formulario
  ModernStyles.ApplyFormStyle(this);
     
    // Header con degradado verde
       Panel headerPanel = new Panel
         {
Dock = DockStyle.Top,
  Height = 80,
          BackColor = ModernStyles.PrimaryGreen
            };
       
// Logo/Título en el header
     Label lblTitulo = new Label
 {
      Text = "🛍️ IPT-NET",
 Font = new Font("Segoe UI", 28F, FontStyle.Bold),
      ForeColor = Color.White,
       AutoSize = true,
           Location = new Point(30, 20)
  };
 headerPanel.Controls.Add(lblTitulo);
            
    // Subtítulo
    Label lblSubtitulo = new Label
     {
    Text = "Sistema de Gestión de Ventas",
          Font = new Font("Segoe UI", 11F, FontStyle.Regular),
   ForeColor = Color.FromArgb(230, 255, 230),
         AutoSize = true,
      Location = new Point(35, 52)
  };
    headerPanel.Controls.Add(lblSubtitulo);
          
       // Botón cerrar (X) moderno
       Button btnCerrar = new Button
            {
     Text = "✕",
       Size = new Size(40, 40),
     Location = new Point(this.Width - 50, 20),
         FlatStyle = FlatStyle.Flat,
     BackColor = Color.Transparent,
       ForeColor = Color.White,
        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
  Cursor = Cursors.Hand
   };
        btnCerrar.FlatAppearance.BorderSize = 0;
      btnCerrar.FlatAppearance.MouseOverBackColor = ModernStyles.DarkGreen;
      btnCerrar.Click += (s, e) => {
    var result = MessageBox.Show(
          "¿Está seguro que desea salir?",
  "Confirmar Salida",
         MessageBoxButtons.YesNo,
           MessageBoxIcon.Question
   );
      if (result == DialogResult.Yes)
           {
      this.Close();
 }
   };
    headerPanel.Controls.Add(btnCerrar);
        
 this.Controls.Add(headerPanel);
   
      // Panel central con las opciones
     Panel contentPanel = new Panel
 {
       Location = new Point(50, 120),
     Size = new Size(800, 420),
  BackColor = Color.Transparent
 };
  
       // Tarjeta de Venta de Indumentaria
       Panel cardVenta = CrearTarjetaOpcion(
        "🛒 Venta de Indumentaria",
          "Registrar nuevas ventas de productos",
     0,
        (s, e) => MostrarVentaIndumentaria()
   );
        contentPanel.Controls.Add(cardVenta);
  
   // ✅ NUEVO: Tarjeta de Cobro de Cuotas
    Panel cardCuotas = CrearTarjetaOpcion(
     "💳 Cobro de Cuotas",
        "Gestionar cobros de cuotas de alumnos",
    1,
      (s, e) => MostrarCobroCuotas()
  );
       contentPanel.Controls.Add(cardCuotas);
    
  // ✅ NUEVO: Tarjeta de Administrar Indumentaria
        Panel cardAdminIndumentaria = CrearTarjetaOpcion(
     "📦 Administrar Indumentaria",
            "ABM de productos - Alta, Baja y Modificación",
     2,
            (s, e) => MostrarAdministrarIndumentaria()
        );
        contentPanel.Controls.Add(cardAdminIndumentaria);
    
    this.Controls.Add(contentPanel);
            
            // Footer mejorado
            Panel footerPanel = new Panel
       {
                Dock = DockStyle.Bottom,
     Height = 50,
         BackColor = ModernStyles.SurfaceGray
   };
    
       Label lblFooter = new Label
{
     Text = "© 2025 IPT-NET | Versión 2.0 - .NET 8",
       Font = new Font("Segoe UI", 9F),
       ForeColor = ModernStyles.TextMedium,
         AutoSize = true,
         Location = new Point(30, 10)
  };
  footerPanel.Controls.Add(lblFooter);
     
      // ✅ Agregar hint de ESC
   Label lblEscHint = new Label
        {
     Text = "Presione ESC para salir",
                Font = new Font("Segoe UI", 9F),
      ForeColor = ModernStyles.TextLight,
         AutoSize = true,
Location = new Point(this.Width - 180, 10)
       };
            footerPanel.Controls.Add(lblEscHint);
    
            this.Controls.Add(footerPanel);
        }
        
        private Panel CrearTarjetaOpcion(string titulo, string descripcion, int posicionVertical, EventHandler clickHandler)
  {
      Panel card = new Panel
       {
  Location = new Point(0, posicionVertical * 140),
       Size = new Size(800, 120),
     BackColor = ModernStyles.CardBackground,
     Cursor = Cursors.Hand
       };
            
 // Borde y sombra
card.Paint += (s, e) =>
            {
       using (Pen pen = new Pen(ModernStyles.BorderLight, 1))
     {
       e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
      };
    
      // Icono/Barra lateral verde
            Panel leftBar = new Panel
  {
     Dock = DockStyle.Left,
       Width = 6,
     BackColor = ModernStyles.PrimaryGreen
            };
       card.Controls.Add(leftBar);
            
       // Contenido de la tarjeta
       Label lblTitulo = new Label
            {
         Text = titulo,
     Font = new Font("Segoe UI", 16F, FontStyle.Bold),
       ForeColor = ModernStyles.TextDark,
      AutoSize = true,
         Location = new Point(30, 25)
            };
        card.Controls.Add(lblTitulo);
    
  Label lblDescripcion = new Label
 {
                Text = descripcion,
   Font = new Font("Segoe UI", 10F),
      ForeColor = ModernStyles.TextMedium,
       AutoSize = true,
         Location = new Point(30, 55)
       };
     card.Controls.Add(lblDescripcion);

        // Flecha indicadora
            Label lblFlecha = new Label
       {
    Text = "→",
        Font = new Font("Segoe UI", 24F, FontStyle.Bold),
       ForeColor = ModernStyles.PrimaryGreen,
      AutoSize = true,
    Location = new Point(740, 40)
  };
card.Controls.Add(lblFlecha);
    
   // Efecto hover
       card.MouseEnter += (s, e) =>
  {
    card.BackColor = ModernStyles.AccentGreen;
  lblFlecha.ForeColor = ModernStyles.DarkGreen;
            };
  
       card.MouseLeave += (s, e) =>
{
         card.BackColor = ModernStyles.CardBackground;
       lblFlecha.ForeColor = ModernStyles.PrimaryGreen;
         };
            
  // Hacer que todos los controles disparen el click
card.Click += clickHandler;
   foreach (Control ctrl in card.Controls)
 {
         ctrl.Click += clickHandler;
      ctrl.MouseEnter += (s, e) => {
           card.BackColor = ModernStyles.AccentGreen;
      lblFlecha.ForeColor = ModernStyles.DarkGreen;
      };
         ctrl.MouseLeave += (s, e) => {
       card.BackColor = ModernStyles.CardBackground;
           lblFlecha.ForeColor = ModernStyles.PrimaryGreen;
       };
       }
       
     return card;
        }

   public void MostrarVentaIndumentaria()
 {
       _presentador.MostrarVentaIndumentaria();
     }

// ✅ NUEVO: Método para mostrar el módulo de Cobro de Cuotas
      public void MostrarCobroCuotas()
   {
      _presentador.MostrarCobroCuotas();
  }

    // ✅ NUEVO: Método para mostrar el módulo de Administrar Indumentaria
        public void MostrarAdministrarIndumentaria()
        {
 _presentador.MostrarAdministrarIndumentaria();
        }

 private void MenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
      Application.Exit();
      }

     private void btnVentaIndumentaria_Click(object sender, EventArgs e)
   {
       MostrarVentaIndumentaria();
   }
  }
}
