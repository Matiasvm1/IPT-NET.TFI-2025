using System;
using System.Drawing;
using System.Windows.Forms;
using TFI.Dominio.Interfaces;
using TFI.Vista.Presentadores;
using TFI.Vista.Styles;

namespace TFI.Vista.Vistas
{
    public partial class Login : Form, ILoginVista
    {
        private readonly LoginPresentador _presentador;
  
      public Login(LoginPresentador presentador)
        {
         InitializeComponent();
      this._presentador = presentador;
      _presentador.SetVista(this);
 
         // ✅ DISEÑO MODERNO ACTIVADO
     ConfigurarDiseñoModerno();
        }

   private void ConfigurarDiseñoModerno()
        {
    // Configuración del formulario
      this.FormBorderStyle = FormBorderStyle.None;
    this.StartPosition = FormStartPosition.CenterScreen;
        this.Size = new Size(500, 620);
     this.BackColor = ModernStyles.BackgroundLight;
   this.KeyPreview = true; // ✅ Habilitar captura de teclas
     
        // ✅ ESC para cerrar
        this.KeyDown += (s, e) =>
   {
   if (e.KeyCode == Keys.Escape)
        {
  Application.Exit();
          }
};
     
 // Limpiar controles existentes del Designer
        this.Controls.Clear();
  
        // Panel principal como tarjeta
            Panel cardPanel = new Panel
{
  Location = new Point(50, 40),
        Size = new Size(400, 540),
     BackColor = ModernStyles.CardBackground
      };
        ModernStyles.ApplyCardPanelStyle(cardPanel);
   
     // Título
      Label lblTitulo = new Label
            {
      Text = "IPT-NET",
    Font = new Font("Segoe UI", 32F, FontStyle.Bold),
          ForeColor = ModernStyles.TextDark,
  AutoSize = true,
  Location = new Point(120, 50)
        };
         cardPanel.Controls.Add(lblTitulo);

         // Subtítulo
      Label lblSubtitulo = new Label
   {
       Text = "Iniciar Sesión",
        Font = new Font("Segoe UI", 14F),
     ForeColor = ModernStyles.TextMedium,
    AutoSize = true,
         Location = new Point(140, 100)
          };
     cardPanel.Controls.Add(lblSubtitulo);
  
         // Label Legajo
     Label lblLegajoLabel = new Label
 {
  Text = "Legajo",
     Font = new Font("Segoe UI", 11F, FontStyle.Bold),
         ForeColor = ModernStyles.TextDark,
        AutoSize = true,
      Location = new Point(50, 170)
 };
 cardPanel.Controls.Add(lblLegajoLabel);
  
            // TextBox Legajo
  txtLegajo.Location = new Point(50, 200);
txtLegajo.Size = new Size(300, 40);
         txtLegajo.Font = new Font("Segoe UI", 12F);
   txtLegajo.BorderStyle = BorderStyle.FixedSingle;
          txtLegajo.BackColor = Color.White;
            txtLegajo.ForeColor = ModernStyles.TextDark;
      txtLegajo.TabIndex = 0;
    
        // Label Contraseña
Label lblContraseñaLabel = new Label
  {
    Text = "Contraseña",
    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
   ForeColor = ModernStyles.TextDark,
   AutoSize = true,
        Location = new Point(50, 270)
       };
       cardPanel.Controls.Add(lblContraseñaLabel);
   
     // TextBox Contraseña
         txtContraseña.Location = new Point(50, 300);
       txtContraseña.Size = new Size(300, 40);
 txtContraseña.Font = new Font("Segoe UI", 12F);
     txtContraseña.BorderStyle = BorderStyle.FixedSingle;
       txtContraseña.BackColor = Color.White;
     txtContraseña.ForeColor = ModernStyles.TextDark;
    txtContraseña.PasswordChar = '●';
 txtContraseña.TabIndex = 1;
 
            // Botón Ingresar
 btnIngresar.Text = "INGRESAR";
    btnIngresar.Location = new Point(50, 380);
            btnIngresar.Size = new Size(300, 55);
     btnIngresar.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
 btnIngresar.Cursor = Cursors.Hand;
            btnIngresar.TabIndex = 2;
    ModernStyles.ApplyPrimaryButtonStyle(btnIngresar);
       
       // ✅ Label ESC para salir
      Label lblEscHint = new Label
    {
    Text = "Presione ESC para salir",
    Font = new Font("Segoe UI", 9F),
       ForeColor = ModernStyles.TextLight,
        AutoSize = true,
           Location = new Point(130, 480)
         };
      cardPanel.Controls.Add(lblEscHint);
       
// Mover controles a la tarjeta
            cardPanel.Controls.Add(txtLegajo);
    cardPanel.Controls.Add(txtContraseña);
   cardPanel.Controls.Add(btnIngresar);
     
  this.Controls.Add(cardPanel);
   
            // Permitir Enter para login
 txtContraseña.KeyDown += (s, e) =>
            {
        if (e.KeyCode == Keys.Enter)
       {
    btnIngresar_Click(this, EventArgs.Empty);
  e.Handled = true;
        e.SuppressKeyPress = true;
   }
 };
  
     txtLegajo.KeyDown += (s, e) =>
        {
     if (e.KeyCode == Keys.Enter)
   {
     txtContraseña.Focus();
   e.Handled = true;
     e.SuppressKeyPress = true;
            }
      };
 }

 public void Mostrar()
        {
   this.Visible = true;
    txtLegajo.Focus();
    }

        public void MostrarError(string mensaje)
     {
 MessageBox.Show(
         mensaje,
      "⚠️ Error de Autenticación",
MessageBoxButtons.OK,
MessageBoxIcon.Warning
    );
 
        txtContraseña.Clear();
txtLegajo.Focus();
txtLegajo.SelectAll();
        }

  public void Ocultar()
        {
     this.Visible = false;
        }

    private void btnIngresar_Click(object sender, EventArgs e)
     {
        // Validaciones
          if (string.IsNullOrWhiteSpace(txtLegajo.Text))
       {
         MostrarError("Por favor, ingrese su legajo.");
      txtLegajo.Focus();
    return;
    }

     if (string.IsNullOrWhiteSpace(txtContraseña.Text))
    {
       MostrarError("Por favor, ingrese su contraseña.");
         txtContraseña.Focus();
         return;
    }
       
   if (!int.TryParse(txtLegajo.Text, out int legajo))
       {
       MostrarError("El legajo debe ser un número válido.");
   txtLegajo.Focus();
 txtLegajo.SelectAll();
      return;
      }
 
   // Intentar login
       _presentador.IngresarDatos(legajo, txtContraseña.Text);
  }
    }
}
