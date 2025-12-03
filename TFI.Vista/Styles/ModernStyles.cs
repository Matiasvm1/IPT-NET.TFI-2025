using System.Drawing;
using System.Windows.Forms;

namespace TFI.Vista.Styles
{
    /// <summary>
    /// Clase estática que define la paleta de colores y estilos modernos para toda la aplicación
    /// Paleta: Fresh Green - Profesional y moderna
    /// </summary>
    public static class ModernStyles
    {
        // ?? PALETA DE COLORES PRINCIPAL
        public static readonly Color PrimaryGreen = ColorTranslator.FromHtml("#10B981");
public static readonly Color DarkGreen = ColorTranslator.FromHtml("#059669");
        public static readonly Color LightGreen = ColorTranslator.FromHtml("#34D399");
        public static readonly Color AccentGreen = ColorTranslator.FromHtml("#D1FAE5");
        
        // COLORES DE FONDO
        public static readonly Color BackgroundLight = ColorTranslator.FromHtml("#F9FAFB");
  public static readonly Color CardBackground = Color.White;
        public static readonly Color SurfaceGray = ColorTranslator.FromHtml("#F3F4F6");
        
        // COLORES DE TEXTO
        public static readonly Color TextDark = ColorTranslator.FromHtml("#1F2937");
        public static readonly Color TextMedium = ColorTranslator.FromHtml("#4B5563");
        public static readonly Color TextLight = ColorTranslator.FromHtml("#6B7280");
      
        // COLORES DE BORDES Y LÍNEAS
        public static readonly Color BorderLight = ColorTranslator.FromHtml("#E5E7EB");
     public static readonly Color BorderMedium = ColorTranslator.FromHtml("#D1D5DB");

   // COLORES DE ESTADO
        public static readonly Color SuccessGreen = ColorTranslator.FromHtml("#10B981");
        public static readonly Color WarningYellow = ColorTranslator.FromHtml("#F59E0B");
        public static readonly Color ErrorRed = ColorTranslator.FromHtml("#EF4444");
        public static readonly Color InfoBlue = ColorTranslator.FromHtml("#3B82F6");
     
        // ?? FUENTES
        public static readonly Font TitleFont = new Font("Segoe UI", 24F, FontStyle.Bold);
        public static readonly Font SubtitleFont = new Font("Segoe UI", 18F, FontStyle.Bold);
  public static readonly Font ButtonFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        public static readonly Font LabelFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font SmallFont = new Font("Segoe UI", 9F, FontStyle.Regular);
        
      // ?? APLICAR ESTILOS A CONTROLES
  
    /// <summary>
        /// Aplica estilo moderno a un botón primario (verde)
        /// </summary>
   public static void ApplyPrimaryButtonStyle(Button button)
        {
        button.BackColor = PrimaryGreen;
    button.ForeColor = Color.White;
          button.FlatStyle = FlatStyle.Flat;
button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = DarkGreen;
    button.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#047857");
   button.Font = ButtonFont;
            button.Cursor = Cursors.Hand;
 button.Padding = new Padding(20, 12, 20, 12);
        }
   
/// <summary>
    /// Aplica estilo moderno a un botón secundario (blanco con borde verde)
        /// </summary>
 public static void ApplySecondaryButtonStyle(Button button)
        {
            button.BackColor = Color.White;
  button.ForeColor = PrimaryGreen;
button.FlatStyle = FlatStyle.Flat;
     button.FlatAppearance.BorderSize = 2;
    button.FlatAppearance.BorderColor = PrimaryGreen;
   button.FlatAppearance.MouseOverBackColor = AccentGreen;
     button.Font = ButtonFont;
        button.Cursor = Cursors.Hand;
            button.Padding = new Padding(20, 12, 20, 12);
        }
        
    /// <summary>
        /// Aplica estilo moderno a un TextBox
        /// </summary>
     public static void ApplyTextBoxStyle(TextBox textBox)
        {
     textBox.BorderStyle = BorderStyle.FixedSingle;
   textBox.Font = LabelFont;
   textBox.BackColor = Color.White;
            textBox.ForeColor = TextDark;
        }
      
        /// <summary>
        /// Aplica estilo moderno a un Label de título
        /// </summary>
        public static void ApplyTitleLabelStyle(Label label)
        {
            label.Font = TitleFont;
 label.ForeColor = TextDark;
  label.BackColor = Color.Transparent;
        }
        
/// <summary>
        /// Aplica estilo moderno a un Label de subtítulo
        /// </summary>
  public static void ApplySubtitleLabelStyle(Label label)
        {
     label.Font = SubtitleFont;
    label.ForeColor = TextMedium;
       label.BackColor = Color.Transparent;
        }
        
      /// <summary>
     /// Aplica estilo moderno a un Label normal
        /// </summary>
        public static void ApplyLabelStyle(Label label)
        {
    label.Font = LabelFont;
            label.ForeColor = TextMedium;
       label.BackColor = Color.Transparent;
        }
        
        /// <summary>
        /// Aplica estilo moderno a un DataGridView
    /// </summary>
 public static void ApplyDataGridViewStyle(DataGridView grid)
        {
      // Colores generales
grid.BackgroundColor = CardBackground;
       grid.GridColor = BorderLight;
 grid.BorderStyle = BorderStyle.None;

     // Estilo de celdas
     grid.DefaultCellStyle.BackColor = Color.White;
         grid.DefaultCellStyle.ForeColor = TextDark;
        grid.DefaultCellStyle.Font = LabelFont;
            grid.DefaultCellStyle.SelectionBackColor = LightGreen;
            grid.DefaultCellStyle.SelectionForeColor = TextDark;
   grid.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
    
    // Estilo de encabezados
            grid.EnableHeadersVisualStyles = false;
      grid.ColumnHeadersDefaultCellStyle.BackColor = PrimaryGreen;
         grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
         grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(8);
 grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.ColumnHeadersHeight = 40;
      grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
       
            // Filas alternadas
            grid.AlternatingRowsDefaultCellStyle.BackColor = SurfaceGray;
            
     // Comportamiento
 grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
       grid.AllowUserToDeleteRows = false;
    grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
       grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
          grid.RowTemplate.Height = 36;
      
         // Eliminar bordes de celdas
          grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
     }
      
        /// <summary>
   /// Aplica estilo moderno a un Panel (como tarjeta)
        /// </summary>
  public static void ApplyCardPanelStyle(Panel panel)
  {
         panel.BackColor = CardBackground;
   panel.Padding = new Padding(20);
    
       // Simular sombra con borde
            panel.Paint += (sender, e) =>
   {
         var pnl = sender as Panel;
        if (pnl != null)
         {
       // Dibujar borde sutil
     using (Pen pen = new Pen(BorderLight, 1))
{
             e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
     }
    }
        };
        }
    
/// <summary>
      /// Aplica estilo moderno a un Form
  /// </summary>
 public static void ApplyFormStyle(Form form)
{
       form.BackColor = BackgroundLight;
            form.Font = LabelFont;
            form.ForeColor = TextDark;
  form.StartPosition = FormStartPosition.CenterScreen;
}
    }
}
