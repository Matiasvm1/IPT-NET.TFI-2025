namespace TFI.Vista
{
    partial class MenuPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // MenuPrincipal
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 600);
            Name = "MenuPrincipal";
            Text = "IVC-NET - Menu Principal";
            FormClosed += MenuPrincipal_FormClosed;
            ResumeLayout(false);
        }

        // Ya no necesitamos estos controles, se crean programáticamente
    }
}

