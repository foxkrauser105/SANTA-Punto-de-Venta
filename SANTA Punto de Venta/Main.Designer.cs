namespace SANTA_Punto_de_Venta
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panelImage = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonNotificaciones = new System.Windows.Forms.Button();
            this.panelMove = new System.Windows.Forms.Panel();
            this.buttonVentasHechas = new System.Windows.Forms.Button();
            this.buttonRequisicion = new System.Windows.Forms.Button();
            this.buttonVentaDia = new System.Windows.Forms.Button();
            this.buttonProductos = new System.Windows.Forms.Button();
            this.buttonVenta = new System.Windows.Forms.Button();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.dataGridViewNotificaciones = new System.Windows.Forms.DataGridView();
            this.timerNotificaciones = new System.Windows.Forms.Timer(this.components);
            this.buttonShowNot = new System.Windows.Forms.Button();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotificaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // panelImage
            // 
            this.panelImage.BackColor = System.Drawing.Color.LightGray;
            this.panelImage.Controls.Add(this.buttonShowNot);
            this.panelImage.Controls.Add(this.pictureBox1);
            this.panelImage.Location = new System.Drawing.Point(0, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(200, 153);
            this.panelImage.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SANTA_Punto_de_Venta.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(21, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.panelButtons.Controls.Add(this.buttonNotificaciones);
            this.panelButtons.Controls.Add(this.panelMove);
            this.panelButtons.Controls.Add(this.buttonVentasHechas);
            this.panelButtons.Controls.Add(this.buttonRequisicion);
            this.panelButtons.Controls.Add(this.buttonVentaDia);
            this.panelButtons.Controls.Add(this.buttonProductos);
            this.panelButtons.Controls.Add(this.buttonVenta);
            this.panelButtons.Location = new System.Drawing.Point(0, 153);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(200, 657);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonNotificaciones
            // 
            this.buttonNotificaciones.FlatAppearance.BorderSize = 0;
            this.buttonNotificaciones.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonNotificaciones.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonNotificaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNotificaciones.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNotificaciones.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonNotificaciones.Image = global::SANTA_Punto_de_Venta.Properties.Resources.messages_icon;
            this.buttonNotificaciones.Location = new System.Drawing.Point(0, 523);
            this.buttonNotificaciones.Name = "buttonNotificaciones";
            this.buttonNotificaciones.Size = new System.Drawing.Size(188, 130);
            this.buttonNotificaciones.TabIndex = 9;
            this.buttonNotificaciones.Text = "Centro de Notificaciones";
            this.buttonNotificaciones.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonNotificaciones.UseVisualStyleBackColor = true;
            this.buttonNotificaciones.Click += new System.EventHandler(this.buttonNotificaciones_Click);
            // 
            // panelMove
            // 
            this.panelMove.BackColor = System.Drawing.Color.Yellow;
            this.panelMove.Location = new System.Drawing.Point(188, 3);
            this.panelMove.Name = "panelMove";
            this.panelMove.Size = new System.Drawing.Size(12, 104);
            this.panelMove.TabIndex = 4;
            // 
            // buttonVentasHechas
            // 
            this.buttonVentasHechas.FlatAppearance.BorderSize = 0;
            this.buttonVentasHechas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonVentasHechas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonVentasHechas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonVentasHechas.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVentasHechas.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonVentasHechas.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Check_3_icon;
            this.buttonVentasHechas.Location = new System.Drawing.Point(0, 419);
            this.buttonVentasHechas.Name = "buttonVentasHechas";
            this.buttonVentasHechas.Size = new System.Drawing.Size(188, 104);
            this.buttonVentasHechas.TabIndex = 8;
            this.buttonVentasHechas.Text = "Ventas hechas";
            this.buttonVentasHechas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonVentasHechas.UseVisualStyleBackColor = true;
            this.buttonVentasHechas.Click += new System.EventHandler(this.buttonVentasHechas_Click);
            // 
            // buttonRequisicion
            // 
            this.buttonRequisicion.FlatAppearance.BorderSize = 0;
            this.buttonRequisicion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonRequisicion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonRequisicion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRequisicion.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRequisicion.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonRequisicion.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Arrow_reload_2_icon;
            this.buttonRequisicion.Location = new System.Drawing.Point(0, 315);
            this.buttonRequisicion.Name = "buttonRequisicion";
            this.buttonRequisicion.Size = new System.Drawing.Size(188, 104);
            this.buttonRequisicion.TabIndex = 7;
            this.buttonRequisicion.Text = "Requisición de productos";
            this.buttonRequisicion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRequisicion.UseVisualStyleBackColor = true;
            this.buttonRequisicion.Click += new System.EventHandler(this.buttonRequisicion_Click);
            // 
            // buttonVentaDia
            // 
            this.buttonVentaDia.FlatAppearance.BorderSize = 0;
            this.buttonVentaDia.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonVentaDia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonVentaDia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonVentaDia.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVentaDia.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonVentaDia.Image = global::SANTA_Punto_de_Venta.Properties.Resources.dollar_coin_icon;
            this.buttonVentaDia.Location = new System.Drawing.Point(0, 211);
            this.buttonVentaDia.Name = "buttonVentaDia";
            this.buttonVentaDia.Size = new System.Drawing.Size(188, 104);
            this.buttonVentaDia.TabIndex = 6;
            this.buttonVentaDia.Text = "Venta del día";
            this.buttonVentaDia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonVentaDia.UseVisualStyleBackColor = true;
            this.buttonVentaDia.Click += new System.EventHandler(this.buttonVentaDia_Click);
            // 
            // buttonProductos
            // 
            this.buttonProductos.FlatAppearance.BorderSize = 0;
            this.buttonProductos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonProductos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProductos.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonProductos.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonProductos.Image = global::SANTA_Punto_de_Venta.Properties.Resources.stat_icon;
            this.buttonProductos.Location = new System.Drawing.Point(0, 107);
            this.buttonProductos.Name = "buttonProductos";
            this.buttonProductos.Size = new System.Drawing.Size(188, 104);
            this.buttonProductos.TabIndex = 5;
            this.buttonProductos.Text = "Productos";
            this.buttonProductos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonProductos.UseVisualStyleBackColor = true;
            this.buttonProductos.Click += new System.EventHandler(this.buttonProductos_Click);
            // 
            // buttonVenta
            // 
            this.buttonVenta.FlatAppearance.BorderSize = 0;
            this.buttonVenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonVenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(130)))));
            this.buttonVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonVenta.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVenta.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonVenta.Image = global::SANTA_Punto_de_Venta.Properties.Resources.cart_icon;
            this.buttonVenta.Location = new System.Drawing.Point(0, 3);
            this.buttonVenta.Name = "buttonVenta";
            this.buttonVenta.Size = new System.Drawing.Size(188, 104);
            this.buttonVenta.TabIndex = 0;
            this.buttonVenta.Text = "Venta";
            this.buttonVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonVenta.UseVisualStyleBackColor = true;
            this.buttonVenta.Click += new System.EventHandler(this.buttonVenta_Click);
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.panelPrincipal.Location = new System.Drawing.Point(200, 0);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(1103, 680);
            this.panelPrincipal.TabIndex = 2;
            // 
            // dataGridViewNotificaciones
            // 
            this.dataGridViewNotificaciones.AllowUserToAddRows = false;
            this.dataGridViewNotificaciones.AllowUserToDeleteRows = false;
            this.dataGridViewNotificaciones.AllowUserToResizeColumns = false;
            this.dataGridViewNotificaciones.AllowUserToResizeRows = false;
            this.dataGridViewNotificaciones.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.dataGridViewNotificaciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewNotificaciones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewNotificaciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewNotificaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewNotificaciones.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewNotificaciones.EnableHeadersVisualStyles = false;
            this.dataGridViewNotificaciones.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridViewNotificaciones.Location = new System.Drawing.Point(203, 686);
            this.dataGridViewNotificaciones.MultiSelect = false;
            this.dataGridViewNotificaciones.Name = "dataGridViewNotificaciones";
            this.dataGridViewNotificaciones.ReadOnly = true;
            this.dataGridViewNotificaciones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewNotificaciones.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewNotificaciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewNotificaciones.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewNotificaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewNotificaciones.ShowEditingIcon = false;
            this.dataGridViewNotificaciones.Size = new System.Drawing.Size(1100, 120);
            this.dataGridViewNotificaciones.TabIndex = 55;
            this.dataGridViewNotificaciones.TabStop = false;
            this.dataGridViewNotificaciones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewNotificaciones_CellContentClick);
            // 
            // timerNotificaciones
            // 
            this.timerNotificaciones.Interval = 10000;
            this.timerNotificaciones.Tick += new System.EventHandler(this.timerNotificaciones_Tick);
            // 
            // buttonShowNot
            // 
            this.buttonShowNot.Location = new System.Drawing.Point(21, 127);
            this.buttonShowNot.Name = "buttonShowNot";
            this.buttonShowNot.Size = new System.Drawing.Size(155, 23);
            this.buttonShowNot.TabIndex = 1;
            this.buttonShowNot.Text = "Mostrar Notificaciones";
            this.buttonShowNot.UseVisualStyleBackColor = true;
            this.buttonShowNot.Visible = false;
            this.buttonShowNot.Click += new System.EventHandler(this.buttonShowNot_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(1304, 681);
            this.Controls.Add(this.dataGridViewNotificaciones);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SANTA Punto de Venta CiberStore";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotificaciones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelMove;
        private System.Windows.Forms.Button buttonVentasHechas;
        private System.Windows.Forms.Button buttonRequisicion;
        private System.Windows.Forms.Button buttonVentaDia;
        private System.Windows.Forms.Button buttonProductos;
        private System.Windows.Forms.Button buttonVenta;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonNotificaciones;
        private System.Windows.Forms.DataGridView dataGridViewNotificaciones;
        private System.Windows.Forms.Timer timerNotificaciones;
        private System.Windows.Forms.Button buttonShowNot;
    }
}

