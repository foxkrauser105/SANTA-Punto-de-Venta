namespace SANTA_Punto_de_Venta
{
    partial class Ventas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCodigo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewVenta = new System.Windows.Forms.DataGridView();
            this.contextMenuStripTabla = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCompra = new System.Windows.Forms.Button();
            this.buttonAñadir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSuma = new System.Windows.Forms.Label();
            this.textBoxCantidad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPago = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelResta = new System.Windows.Forms.Label();
            this.labelRestante = new System.Windows.Forms.Label();
            this.labelPago = new System.Windows.Forms.Label();
            this.labelPagado = new System.Windows.Forms.Label();
            this.buttonLinea = new System.Windows.Forms.Button();
            this.buttonActualizarNota = new System.Windows.Forms.Button();
            this.buttonAgregarNota = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelRedondeo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVenta)).BeginInit();
            this.contextMenuStripTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(532, 69);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 58);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ventas";
            // 
            // textBoxCodigo
            // 
            this.textBoxCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCodigo.Location = new System.Drawing.Point(387, 497);
            this.textBoxCodigo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCodigo.Name = "textBoxCodigo";
            this.textBoxCodigo.Size = new System.Drawing.Size(259, 26);
            this.textBoxCodigo.TabIndex = 2;
            this.textBoxCodigo.TextChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            this.textBoxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCodigo_KeyDown);
            this.textBoxCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCodigo_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(314, 502);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 21);
            this.label7.TabIndex = 37;
            this.label7.Text = "Código:";
            // 
            // dataGridViewVenta
            // 
            this.dataGridViewVenta.AllowUserToAddRows = false;
            this.dataGridViewVenta.AllowUserToOrderColumns = true;
            this.dataGridViewVenta.AllowUserToResizeColumns = false;
            this.dataGridViewVenta.AllowUserToResizeRows = false;
            this.dataGridViewVenta.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.dataGridViewVenta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewVenta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewVenta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewVenta.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewVenta.EnableHeadersVisualStyles = false;
            this.dataGridViewVenta.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridViewVenta.Location = new System.Drawing.Point(120, 189);
            this.dataGridViewVenta.MultiSelect = false;
            this.dataGridViewVenta.Name = "dataGridViewVenta";
            this.dataGridViewVenta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewVenta.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewVenta.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewVenta.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewVenta.ShowEditingIcon = false;
            this.dataGridViewVenta.Size = new System.Drawing.Size(844, 295);
            this.dataGridViewVenta.TabIndex = 41;
            this.dataGridViewVenta.TabStop = false;
            this.dataGridViewVenta.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVenta_CellEndEdit);
            this.dataGridViewVenta.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewVenta_CellMouseDown);
            this.dataGridViewVenta.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewVenta_RowsAdded);
            this.dataGridViewVenta.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridViewVenta_RowsRemoved);
            // 
            // contextMenuStripTabla
            // 
            this.contextMenuStripTabla.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStripTabla.Name = "contextMenuStripTabla";
            this.contextMenuStripTabla.Size = new System.Drawing.Size(218, 26);
            this.contextMenuStripTabla.Click += new System.EventHandler(this.contextMenuStripTabla_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.toolStripMenuItem1.Text = "Eliminar producto de venta";
            // 
            // buttonCompra
            // 
            this.buttonCompra.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCompra.Location = new System.Drawing.Point(495, 540);
            this.buttonCompra.Name = "buttonCompra";
            this.buttonCompra.Size = new System.Drawing.Size(170, 28);
            this.buttonCompra.TabIndex = 4;
            this.buttonCompra.Text = "Realizar compra";
            this.buttonCompra.UseVisualStyleBackColor = true;
            this.buttonCompra.Click += new System.EventHandler(this.buttonCompra_Click);
            // 
            // buttonAñadir
            // 
            this.buttonAñadir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonAñadir.Location = new System.Drawing.Point(652, 495);
            this.buttonAñadir.Name = "buttonAñadir";
            this.buttonAñadir.Size = new System.Drawing.Size(107, 28);
            this.buttonAñadir.TabIndex = 5;
            this.buttonAñadir.TabStop = false;
            this.buttonAñadir.Text = "Añadir";
            this.buttonAñadir.UseVisualStyleBackColor = true;
            this.buttonAñadir.Click += new System.EventHandler(this.buttonAñadir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(766, 490);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 33);
            this.label2.TabIndex = 44;
            this.label2.Text = "Total: $";
            // 
            // labelSuma
            // 
            this.labelSuma.AutoSize = true;
            this.labelSuma.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSuma.Location = new System.Drawing.Point(870, 491);
            this.labelSuma.Name = "labelSuma";
            this.labelSuma.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelSuma.Size = new System.Drawing.Size(67, 33);
            this.labelSuma.TabIndex = 45;
            this.labelSuma.Text = "0.00";
            // 
            // textBoxCantidad
            // 
            this.textBoxCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCantidad.Location = new System.Drawing.Point(208, 497);
            this.textBoxCantidad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCantidad.Name = "textBoxCantidad";
            this.textBoxCantidad.Size = new System.Drawing.Size(95, 26);
            this.textBoxCantidad.TabIndex = 1;
            this.textBoxCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCantidad_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(116, 499);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 21);
            this.label4.TabIndex = 47;
            this.label4.Text = "Cantidad:";
            // 
            // textBoxPago
            // 
            this.textBoxPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPago.Location = new System.Drawing.Point(387, 540);
            this.textBoxPago.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxPago.Name = "textBoxPago";
            this.textBoxPago.Size = new System.Drawing.Size(102, 26);
            this.textBoxPago.TabIndex = 3;
            this.textBoxPago.TextChanged += new System.EventHandler(this.textBoxPago_TextChanged);
            this.textBoxPago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPago_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(331, 542);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 21);
            this.label3.TabIndex = 49;
            this.label3.Text = "Pago:";
            // 
            // labelResta
            // 
            this.labelResta.AutoSize = true;
            this.labelResta.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResta.Location = new System.Drawing.Point(757, 567);
            this.labelResta.Name = "labelResta";
            this.labelResta.Size = new System.Drawing.Size(116, 33);
            this.labelResta.TabIndex = 50;
            this.labelResta.Text = "Resta: $";
            // 
            // labelRestante
            // 
            this.labelRestante.AutoSize = true;
            this.labelRestante.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRestante.Location = new System.Drawing.Point(870, 568);
            this.labelRestante.Name = "labelRestante";
            this.labelRestante.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelRestante.Size = new System.Drawing.Size(67, 33);
            this.labelRestante.TabIndex = 51;
            this.labelRestante.Text = "0.00";
            // 
            // labelPago
            // 
            this.labelPago.AutoSize = true;
            this.labelPago.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPago.Location = new System.Drawing.Point(759, 524);
            this.labelPago.Name = "labelPago";
            this.labelPago.Size = new System.Drawing.Size(114, 33);
            this.labelPago.TabIndex = 52;
            this.labelPago.Text = "Pagó: $";
            // 
            // labelPagado
            // 
            this.labelPagado.AutoSize = true;
            this.labelPagado.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPagado.Location = new System.Drawing.Point(870, 525);
            this.labelPagado.Name = "labelPagado";
            this.labelPagado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelPagado.Size = new System.Drawing.Size(67, 33);
            this.labelPagado.TabIndex = 53;
            this.labelPagado.Text = "0.00";
            // 
            // buttonLinea
            // 
            this.buttonLinea.Location = new System.Drawing.Point(764, 560);
            this.buttonLinea.Name = "buttonLinea";
            this.buttonLinea.Size = new System.Drawing.Size(200, 2);
            this.buttonLinea.TabIndex = 54;
            this.buttonLinea.TabStop = false;
            this.buttonLinea.Text = "button1";
            this.buttonLinea.UseVisualStyleBackColor = true;
            // 
            // buttonActualizarNota
            // 
            this.buttonActualizarNota.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonActualizarNota.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Actions_edit_redo_icon__1_;
            this.buttonActualizarNota.Location = new System.Drawing.Point(712, 538);
            this.buttonActualizarNota.Name = "buttonActualizarNota";
            this.buttonActualizarNota.Size = new System.Drawing.Size(35, 32);
            this.buttonActualizarNota.TabIndex = 6;
            this.buttonActualizarNota.UseVisualStyleBackColor = true;
            this.buttonActualizarNota.Click += new System.EventHandler(this.buttonActualizarNota_Click);
            // 
            // buttonAgregarNota
            // 
            this.buttonAgregarNota.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonAgregarNota.Image = global::SANTA_Punto_de_Venta.Properties.Resources.shop_cart_add_icon__1_;
            this.buttonAgregarNota.Location = new System.Drawing.Point(671, 538);
            this.buttonAgregarNota.Name = "buttonAgregarNota";
            this.buttonAgregarNota.Size = new System.Drawing.Size(35, 32);
            this.buttonAgregarNota.TabIndex = 5;
            this.buttonAgregarNota.UseVisualStyleBackColor = true;
            this.buttonAgregarNota.Click += new System.EventHandler(this.buttonAgregarNota_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::SANTA_Punto_de_Venta.Properties.Resources.cart_add_icon;
            this.pictureBoxLogo.Location = new System.Drawing.Point(387, 31);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(135, 135);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 3;
            this.pictureBoxLogo.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(671, 576);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 25);
            this.button1.TabIndex = 7;
            this.button1.Text = "Limpiar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelRedondeo
            // 
            this.labelRedondeo.AutoSize = true;
            this.labelRedondeo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRedondeo.Location = new System.Drawing.Point(967, 497);
            this.labelRedondeo.Name = "labelRedondeo";
            this.labelRedondeo.Size = new System.Drawing.Size(115, 21);
            this.labelRedondeo.TabIndex = 55;
            this.labelRedondeo.Text = "Por redondeo";
            this.labelRedondeo.Visible = false;
            // 
            // Ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(1087, 641);
            this.Controls.Add(this.labelRedondeo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonActualizarNota);
            this.Controls.Add(this.buttonAgregarNota);
            this.Controls.Add(this.buttonLinea);
            this.Controls.Add(this.labelPagado);
            this.Controls.Add(this.labelPago);
            this.Controls.Add(this.labelRestante);
            this.Controls.Add(this.labelResta);
            this.Controls.Add(this.textBoxPago);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxCantidad);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelSuma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonAñadir);
            this.Controls.Add(this.buttonCompra);
            this.Controls.Add(this.dataGridViewVenta);
            this.Controls.Add(this.textBoxCodigo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Ventas";
            this.Text = "Ventas";
            this.Load += new System.EventHandler(this.Ventas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVenta)).EndInit();
            this.contextMenuStripTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonCompra;
        private System.Windows.Forms.Button buttonAñadir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSuma;
        private System.Windows.Forms.TextBox textBoxCantidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTabla;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TextBox textBoxPago;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelResta;
        private System.Windows.Forms.Label labelRestante;
        private System.Windows.Forms.Label labelPago;
        private System.Windows.Forms.Label labelPagado;
        private System.Windows.Forms.Button buttonLinea;
        internal System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.Button buttonAgregarNota;
        private System.Windows.Forms.Button buttonActualizarNota;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelRedondeo;
        public System.Windows.Forms.DataGridView dataGridViewVenta;
    }
}