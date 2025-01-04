namespace SANTA_Punto_de_Venta.Vistas
{
    partial class ConexionDBSeleccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConexionDBSeleccion));
            this.label = new System.Windows.Forms.Label();
            this.comboBoxDB = new System.Windows.Forms.ComboBox();
            this.buttonAccion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label.ForeColor = System.Drawing.SystemColors.Control;
            this.label.Location = new System.Drawing.Point(30, 25);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(254, 21);
            this.label.TabIndex = 0;
            this.label.Text = "Seleccione DB para conectarse";
            // 
            // comboBoxDB
            // 
            this.comboBoxDB.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.comboBoxDB.FormattingEnabled = true;
            this.comboBoxDB.Items.AddRange(new object[] {
            "Pruebas",
            "Producción"});
            this.comboBoxDB.Location = new System.Drawing.Point(63, 70);
            this.comboBoxDB.Name = "comboBoxDB";
            this.comboBoxDB.Size = new System.Drawing.Size(192, 25);
            this.comboBoxDB.TabIndex = 1;
            this.comboBoxDB.SelectedIndexChanged += new System.EventHandler(this.comboBoxDB_SelectedIndexChanged);
            // 
            // buttonAccion
            // 
            this.buttonAccion.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.buttonAccion.Location = new System.Drawing.Point(189, 110);
            this.buttonAccion.Name = "buttonAccion";
            this.buttonAccion.Size = new System.Drawing.Size(109, 28);
            this.buttonAccion.TabIndex = 126;
            this.buttonAccion.Text = "Aceptar";
            this.buttonAccion.UseVisualStyleBackColor = true;
            this.buttonAccion.Click += new System.EventHandler(this.buttonAccion_Click);
            // 
            // ConexionDBSeleccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(310, 150);
            this.Controls.Add(this.buttonAccion);
            this.Controls.Add(this.comboBoxDB);
            this.Controls.Add(this.label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConexionDBSeleccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione Conexión";
            this.Load += new System.EventHandler(this.ConexionDBSeleccion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ComboBox comboBoxDB;
        private System.Windows.Forms.Button buttonAccion;
    }
}