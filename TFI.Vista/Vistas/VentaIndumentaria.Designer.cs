namespace TFI.Vista.Vistas
{
    partial class VentaIndumentaria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboTalle = new System.Windows.Forms.ComboBox();
            this.bsTalle = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.TxtCantidad = new System.Windows.Forms.TextBox();
            this.dataGridLineaVenta = new System.Windows.Forms.DataGridView();
            this.btnAgregarIndumentaria = new System.Windows.Forms.Button();
            this.btnEliminarIndumentaria = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnConcretarVenta = new System.Windows.Forms.Button();
            this.btnRealizarVenta = new System.Windows.Forms.Button();
            this.LblDescripcion = new System.Windows.Forms.Label();
            this.txtVuelto = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.bsVenta = new System.Windows.Forms.BindingSource(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.lineaDeVentaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsTalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLineaVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineaDeVentaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingrese el codigo del producto";
            // 
            // TxtCodigo
            // 
            this.TxtCodigo.Location = new System.Drawing.Point(12, 39);
            this.TxtCodigo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TxtCodigo.Name = "TxtCodigo";
            this.TxtCodigo.Size = new System.Drawing.Size(411, 20);
            this.TxtCodigo.TabIndex = 1;
            this.TxtCodigo.Leave += new System.EventHandler(this.TxtCodigo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Descripcion :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Talle:";
            // 
            // comboTalle
            // 
            this.comboTalle.DataSource = this.bsTalle;
            this.comboTalle.DisplayMember = "Descripcion";
            this.comboTalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTalle.FormattingEnabled = true;
            this.comboTalle.Location = new System.Drawing.Point(80, 118);
            this.comboTalle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboTalle.Name = "comboTalle";
            this.comboTalle.Size = new System.Drawing.Size(92, 28);
            this.comboTalle.TabIndex = 4;
            this.comboTalle.ValueMember = "Id";
            // 
            // bsTalle
            // 
            this.bsTalle.DataSource = typeof(TFI.Dominio.Talle);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "Cantidad:";
            // 
            // TxtCantidad
            // 
            this.TxtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCantidad.Location = new System.Drawing.Point(98, 150);
            this.TxtCantidad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TxtCantidad.Name = "TxtCantidad";
            this.TxtCantidad.Size = new System.Drawing.Size(73, 26);
            this.TxtCantidad.TabIndex = 6;
            this.TxtCantidad.Leave += new System.EventHandler(this.TxtCantidad_Leave);
            // 
            // dataGridLineaVenta
            // 
            this.dataGridLineaVenta.AutoGenerateColumns = false;
            this.dataGridLineaVenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridLineaVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridLineaVenta.Location = new System.Drawing.Point(9, 182);
            this.dataGridLineaVenta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridLineaVenta.Name = "dataGridLineaVenta";
            this.dataGridLineaVenta.RowHeadersWidth = 51;
            this.dataGridLineaVenta.RowTemplate.Height = 24;
            this.dataGridLineaVenta.Size = new System.Drawing.Size(568, 128);
            this.dataGridLineaVenta.TabIndex = 7;
            // 
            // btnAgregarIndumentaria
            // 
            this.btnAgregarIndumentaria.Location = new System.Drawing.Point(275, 89);
            this.btnAgregarIndumentaria.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAgregarIndumentaria.Name = "btnAgregarIndumentaria";
            this.btnAgregarIndumentaria.Size = new System.Drawing.Size(64, 70);
            this.btnAgregarIndumentaria.TabIndex = 8;
            this.btnAgregarIndumentaria.Text = "Agregar";
            this.btnAgregarIndumentaria.UseVisualStyleBackColor = true;
            this.btnAgregarIndumentaria.Click += new System.EventHandler(this.btnAgregarIndumentaria_Click);
            // 
            // btnEliminarIndumentaria
            // 
            this.btnEliminarIndumentaria.Location = new System.Drawing.Point(358, 89);
            this.btnEliminarIndumentaria.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEliminarIndumentaria.Name = "btnEliminarIndumentaria";
            this.btnEliminarIndumentaria.Size = new System.Drawing.Size(64, 70);
            this.btnEliminarIndumentaria.TabIndex = 9;
            this.btnEliminarIndumentaria.Text = "Eliminar";
            this.btnEliminarIndumentaria.UseVisualStyleBackColor = true;
            this.btnEliminarIndumentaria.Click += new System.EventHandler(this.btnEliminarIndumentaria_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(392, 312);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 24);
            this.label5.TabIndex = 10;
            this.label5.Text = "TOTAL:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(392, 350);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 24);
            this.label7.TabIndex = 12;
            this.label7.Text = "IMPORTE";
            // 
            // txtImporte
            // 
            this.txtImporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporte.Location = new System.Drawing.Point(488, 346);
            this.txtImporte.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(73, 28);
            this.txtImporte.TabIndex = 13;
            this.txtImporte.Leave += new System.EventHandler(this.txtImporte_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(392, 378);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 24);
            this.label8.TabIndex = 14;
            this.label8.Text = "VUELTO : $";
            // 
            // btnConcretarVenta
            // 
            this.btnConcretarVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConcretarVenta.Location = new System.Drawing.Point(135, 414);
            this.btnConcretarVenta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConcretarVenta.Name = "btnConcretarVenta";
            this.btnConcretarVenta.Size = new System.Drawing.Size(167, 34);
            this.btnConcretarVenta.TabIndex = 15;
            this.btnConcretarVenta.Text = "Concretar Venta";
            this.btnConcretarVenta.UseVisualStyleBackColor = true;
            this.btnConcretarVenta.Click += new System.EventHandler(this.btnConcretarVenta_Click);
            // 
            // btnRealizarVenta
            // 
            this.btnRealizarVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarVenta.Location = new System.Drawing.Point(312, 414);
            this.btnRealizarVenta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRealizarVenta.Name = "btnRealizarVenta";
            this.btnRealizarVenta.Size = new System.Drawing.Size(156, 34);
            this.btnRealizarVenta.TabIndex = 16;
            this.btnRealizarVenta.Text = "Cancelar Venta";
            this.btnRealizarVenta.UseVisualStyleBackColor = true;
            this.btnRealizarVenta.Click += new System.EventHandler(this.btnRealizarVenta_Click);
            // 
            // LblDescripcion
            // 
            this.LblDescripcion.AutoSize = true;
            this.LblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDescripcion.Location = new System.Drawing.Point(128, 59);
            this.LblDescripcion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDescripcion.Name = "LblDescripcion";
            this.LblDescripcion.Size = new System.Drawing.Size(80, 24);
            this.LblDescripcion.TabIndex = 17;
            this.LblDescripcion.Text = "XXXXX";
            // 
            // txtVuelto
            // 
            this.txtVuelto.AutoSize = true;
            this.txtVuelto.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVuelto.Location = new System.Drawing.Point(501, 378);
            this.txtVuelto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtVuelto.Name = "txtVuelto";
            this.txtVuelto.Size = new System.Drawing.Size(20, 24);
            this.txtVuelto.TabIndex = 18;
            this.txtVuelto.Text = "0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVenta, "Total", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0", "C2"));
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(484, 312);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(75, 24);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "9999.99";
            // 
            // bsVenta
            // 
            this.bsVenta.DataSource = typeof(TFI.Dominio.Venta);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 24);
            this.label6.TabIndex = 20;
            this.label6.Text = "Precio: ";
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.Location = new System.Drawing.Point(76, 89);
            this.lblPrecio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(20, 24);
            this.lblPrecio.TabIndex = 21;
            this.lblPrecio.Text = "$";
            // 
            // lineaDeVentaBindingSource
            // 
            this.lineaDeVentaBindingSource.DataSource = typeof(TFI.Vista.DTOs.LineaDeVentaDTO);
            // 
            // VentaIndumentaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 449);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtVuelto);
            this.Controls.Add(this.LblDescripcion);
            this.Controls.Add(this.btnRealizarVenta);
            this.Controls.Add(this.btnConcretarVenta);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnEliminarIndumentaria);
            this.Controls.Add(this.btnAgregarIndumentaria);
            this.Controls.Add(this.dataGridLineaVenta);
            this.Controls.Add(this.TxtCantidad);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboTalle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtCodigo);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "VentaIndumentaria";
            this.Text = "Venta de Indumentaria";
            ((System.ComponentModel.ISupportInitialize)(this.bsTalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLineaVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineaDeVentaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboTalle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtCantidad;
        private System.Windows.Forms.DataGridView dataGridLineaVenta;
        private System.Windows.Forms.Button btnAgregarIndumentaria;
        private System.Windows.Forms.Button btnEliminarIndumentaria;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnConcretarVenta;
        private System.Windows.Forms.Button btnRealizarVenta;
        private System.Windows.Forms.Label LblDescripcion;
        private System.Windows.Forms.Label txtVuelto;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.BindingSource bsVenta;
        private System.Windows.Forms.BindingSource bsTalle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.BindingSource lineaDeVentaBindingSource;
    }
}