namespace SANTA_Punto_de_Venta
{
    partial class Ventas_Hechas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerFecha = new System.Windows.Forms.DateTimePicker();
            this.comboBoxID = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewVenta = new System.Windows.Forms.DataGridView();
            this.labelSuma = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.devolucionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordenCompletaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productoUnitarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelRedondeo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVenta)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(452, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 58);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ventas Hechas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(215, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 21);
            this.label4.TabIndex = 50;
            this.label4.Text = "Ventas del día:";
            // 
            // dateTimePickerFecha
            // 
            this.dateTimePickerFecha.Location = new System.Drawing.Point(347, 219);
            this.dateTimePickerFecha.Name = "dateTimePickerFecha";
            this.dateTimePickerFecha.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFecha.TabIndex = 1;
            this.dateTimePickerFecha.Value = new System.DateTime(2019, 3, 16, 0, 0, 0, 0);
            this.dateTimePickerFecha.ValueChanged += new System.EventHandler(this.dateTimePickerFecha_ValueChanged);
            // 
            // comboBoxID
            // 
            this.comboBoxID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxID.FormattingEnabled = true;
            this.comboBoxID.Location = new System.Drawing.Point(683, 219);
            this.comboBoxID.Name = "comboBoxID";
            this.comboBoxID.Size = new System.Drawing.Size(121, 21);
            this.comboBoxID.TabIndex = 2;
            this.comboBoxID.SelectedIndexChanged += new System.EventHandler(this.comboBoxID_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(592, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 21);
            this.label2.TabIndex = 52;
            this.label2.Text = "Venta N°:";
            // 
            // dataGridViewVenta
            // 
            this.dataGridViewVenta.AllowUserToAddRows = false;
            this.dataGridViewVenta.AllowUserToDeleteRows = false;
            this.dataGridViewVenta.AllowUserToResizeColumns = false;
            this.dataGridViewVenta.AllowUserToResizeRows = false;
            this.dataGridViewVenta.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.dataGridViewVenta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewVenta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewVenta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewVenta.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewVenta.EnableHeadersVisualStyles = false;
            this.dataGridViewVenta.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridViewVenta.Location = new System.Drawing.Point(125, 267);
            this.dataGridViewVenta.MultiSelect = false;
            this.dataGridViewVenta.Name = "dataGridViewVenta";
            this.dataGridViewVenta.ReadOnly = true;
            this.dataGridViewVenta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewVenta.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewVenta.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewVenta.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewVenta.ShowEditingIcon = false;
            this.dataGridViewVenta.Size = new System.Drawing.Size(844, 283);
            this.dataGridViewVenta.TabIndex = 53;
            this.dataGridViewVenta.TabStop = false;
            // 
            // labelSuma
            // 
            this.labelSuma.AutoSize = true;
            this.labelSuma.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSuma.Location = new System.Drawing.Point(846, 566);
            this.labelSuma.Name = "labelSuma";
            this.labelSuma.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelSuma.Size = new System.Drawing.Size(67, 33);
            this.labelSuma.TabIndex = 55;
            this.labelSuma.Text = "0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(744, 566);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 33);
            this.label3.TabIndex = 54;
            this.label3.Text = "Total: $";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.devolucionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1087, 24);
            this.menuStrip1.TabIndex = 56;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // devolucionesToolStripMenuItem
            // 
            this.devolucionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordenCompletaToolStripMenuItem,
            this.productoUnitarioToolStripMenuItem});
            this.devolucionesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.devolucionesToolStripMenuItem.Name = "devolucionesToolStripMenuItem";
            this.devolucionesToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.devolucionesToolStripMenuItem.Text = "Devoluciones";
            // 
            // ordenCompletaToolStripMenuItem
            // 
            this.ordenCompletaToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.market_icon;
            this.ordenCompletaToolStripMenuItem.Name = "ordenCompletaToolStripMenuItem";
            this.ordenCompletaToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.ordenCompletaToolStripMenuItem.Text = "Orden Completa";
            this.ordenCompletaToolStripMenuItem.Click += new System.EventHandler(this.ordenCompletaToolStripMenuItem_Click);
            // 
            // productoUnitarioToolStripMenuItem
            // 
            this.productoUnitarioToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.market_flow_icon;
            this.productoUnitarioToolStripMenuItem.Name = "productoUnitarioToolStripMenuItem";
            this.productoUnitarioToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.productoUnitarioToolStripMenuItem.Text = "Producto Unitario";
            this.productoUnitarioToolStripMenuItem.Click += new System.EventHandler(this.productoUnitarioToolStripMenuItem_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::SANTA_Punto_de_Venta.Properties.Resources.check_icon;
            this.pictureBoxLogo.Location = new System.Drawing.Point(307, 52);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(135, 135);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 5;
            this.pictureBoxLogo.TabStop = false;
            // 
            // labelRedondeo
            // 
            this.labelRedondeo.AutoSize = true;
            this.labelRedondeo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRedondeo.Location = new System.Drawing.Point(962, 574);
            this.labelRedondeo.Name = "labelRedondeo";
            this.labelRedondeo.Size = new System.Drawing.Size(115, 21);
            this.labelRedondeo.TabIndex = 57;
            this.labelRedondeo.Text = "Por redondeo";
            this.labelRedondeo.Visible = false;
            // 
            // Ventas_Hechas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(1087, 641);
            this.Controls.Add(this.labelRedondeo);
            this.Controls.Add(this.labelSuma);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewVenta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePickerFecha);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1103, 680);
            this.MinimumSize = new System.Drawing.Size(1103, 680);
            this.Name = "Ventas_Hechas";
            this.Text = "Ventas_Hechas";
            this.Load += new System.EventHandler(this.Ventas_Hechas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVenta)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerFecha;
        private System.Windows.Forms.ComboBox comboBoxID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewVenta;
        private System.Windows.Forms.Label labelSuma;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem devolucionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordenCompletaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productoUnitarioToolStripMenuItem;
        private System.Windows.Forms.Label labelRedondeo;
    }
}