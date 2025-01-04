namespace SANTA_Punto_de_Venta
{
    partial class Notificaciones
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.labelMontoPagado = new System.Windows.Forms.Label();
            this.textBoxProducto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMensaje = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.dateTimePickerFechaAlta = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxNoMensaje = new System.Windows.Forms.TextBox();
            this.dateTimePickerBuscaFechaTermino = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxExtras = new System.Windows.Forms.GroupBox();
            this.dateTimePickerFechaTermino = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxRecibeDesc = new System.Windows.Forms.TextBox();
            this.textBoxRecibe = new System.Windows.Forms.TextBox();
            this.textBoxUsuarioDesc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxUsuario = new System.Windows.Forms.TextBox();
            this.textBoxProductoDesc = new System.Windows.Forms.TextBox();
            this.groupBoxBuscar = new System.Windows.Forms.GroupBox();
            this.chkIncluirInactTerm = new System.Windows.Forms.CheckBox();
            this.lblInclInactTerm = new System.Windows.Forms.Label();
            this.checkBoxFechaTermino = new System.Windows.Forms.CheckBox();
            this.checkBoxFechaAlta = new System.Windows.Forms.CheckBox();
            this.dataGridViewNotificaciones = new System.Windows.Forms.DataGridView();
            this.dateTimePickerBuscaFechaAlta = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxBuscaStatus = new System.Windows.Forms.ComboBox();
            this.comboBoxBuscaPrioridad = new System.Windows.Forms.ComboBox();
            this.comboBoxBuscaTipo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxBuscaNoMensaje = new System.Windows.Forms.TextBox();
            this.lblCargaMensajes = new System.Windows.Forms.Label();
            this.buttonTerminado = new System.Windows.Forms.Button();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.comboBoxTipo = new System.Windows.Forms.ComboBox();
            this.comboBoxPrioridad = new System.Windows.Forms.ComboBox();
            this.panelCargandoMensajes = new System.Windows.Forms.Panel();
            this.buttonInactivar = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.groupBoxExtras.SuspendLayout();
            this.groupBoxBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotificaciones)).BeginInit();
            this.panelCargandoMensajes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(346, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(603, 58);
            this.label1.TabIndex = 2;
            this.label1.Text = "Centro de Notificaciones";
            // 
            // labelMontoPagado
            // 
            this.labelMontoPagado.AutoSize = true;
            this.labelMontoPagado.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMontoPagado.ForeColor = System.Drawing.SystemColors.Control;
            this.labelMontoPagado.Location = new System.Drawing.Point(394, 26);
            this.labelMontoPagado.Name = "labelMontoPagado";
            this.labelMontoPagado.Size = new System.Drawing.Size(86, 21);
            this.labelMontoPagado.TabIndex = 120;
            this.labelMontoPagado.Text = "Producto:";
            // 
            // textBoxProducto
            // 
            this.textBoxProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProducto.Location = new System.Drawing.Point(486, 23);
            this.textBoxProducto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxProducto.Name = "textBoxProducto";
            this.textBoxProducto.Size = new System.Drawing.Size(144, 24);
            this.textBoxProducto.TabIndex = 5;
            this.textBoxProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxProducto_KeyDown);
            this.textBoxProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxProducto_KeyPress);
            this.textBoxProducto.Validated += new System.EventHandler(this.textBoxProducto_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(39, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 21);
            this.label6.TabIndex = 118;
            this.label6.Text = "Mensaje:";
            // 
            // textBoxMensaje
            // 
            this.textBoxMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.textBoxMensaje.Location = new System.Drawing.Point(157, 208);
            this.textBoxMensaje.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxMensaje.MaxLength = 1000;
            this.textBoxMensaje.Multiline = true;
            this.textBoxMensaje.Name = "textBoxMensaje";
            this.textBoxMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMensaje.Size = new System.Drawing.Size(891, 72);
            this.textBoxMensaje.TabIndex = 4;
            this.textBoxMensaje.TextChanged += new System.EventHandler(this.textBoxMensaje_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(547, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 21);
            this.label4.TabIndex = 116;
            this.label4.Text = "Prioridad:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(297, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 21);
            this.label5.TabIndex = 113;
            this.label5.Text = "Tipo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(838, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 21);
            this.label7.TabIndex = 112;
            this.label7.Text = "Estatus:";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Enabled = false;
            this.textBoxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStatus.Location = new System.Drawing.Point(908, 182);
            this.textBoxStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(140, 24);
            this.textBoxStatus.TabIndex = 108;
            this.textBoxStatus.TabStop = false;
            // 
            // dateTimePickerFechaAlta
            // 
            this.dateTimePickerFechaAlta.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dateTimePickerFechaAlta.CustomFormat = "dddd dd/MM/yyyy hh:mm:tt";
            this.dateTimePickerFechaAlta.Enabled = false;
            this.dateTimePickerFechaAlta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dateTimePickerFechaAlta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFechaAlta.Location = new System.Drawing.Point(808, 157);
            this.dateTimePickerFechaAlta.Name = "dateTimePickerFechaAlta";
            this.dateTimePickerFechaAlta.Size = new System.Drawing.Size(240, 23);
            this.dateTimePickerFechaAlta.TabIndex = 111;
            this.dateTimePickerFechaAlta.TabStop = false;
            this.dateTimePickerFechaAlta.Value = new System.DateTime(3000, 1, 1, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(677, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 21);
            this.label8.TabIndex = 110;
            this.label8.Text = "Fecha de Alta:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(39, 183);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 21);
            this.label9.TabIndex = 109;
            this.label9.Text = "No. Mensaje:";
            // 
            // textBoxNoMensaje
            // 
            this.textBoxNoMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNoMensaje.Location = new System.Drawing.Point(157, 182);
            this.textBoxNoMensaje.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxNoMensaje.Name = "textBoxNoMensaje";
            this.textBoxNoMensaje.Size = new System.Drawing.Size(130, 24);
            this.textBoxNoMensaje.TabIndex = 1;
            this.textBoxNoMensaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNoMensaje_KeyPress);
            this.textBoxNoMensaje.Validated += new System.EventHandler(this.textBoxNoMensaje_Validated);
            // 
            // dateTimePickerBuscaFechaTermino
            // 
            this.dateTimePickerBuscaFechaTermino.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dateTimePickerBuscaFechaTermino.CustomFormat = "dddd dd/MM/yyyy";
            this.dateTimePickerBuscaFechaTermino.Enabled = false;
            this.dateTimePickerBuscaFechaTermino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerBuscaFechaTermino.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerBuscaFechaTermino.Location = new System.Drawing.Point(779, 49);
            this.dateTimePickerBuscaFechaTermino.Name = "dateTimePickerBuscaFechaTermino";
            this.dateTimePickerBuscaFechaTermino.Size = new System.Drawing.Size(238, 20);
            this.dateTimePickerBuscaFechaTermino.TabIndex = 16;
            this.dateTimePickerBuscaFechaTermino.TabStop = false;
            this.dateTimePickerBuscaFechaTermino.Value = new System.DateTime(3000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerBuscaFechaTermino.ValueChanged += new System.EventHandler(this.dateTimePickerBuscaFechaTermino_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(656, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 122;
            this.label3.Text = "Fecha de Término:";
            // 
            // groupBoxExtras
            // 
            this.groupBoxExtras.Controls.Add(this.dateTimePickerFechaTermino);
            this.groupBoxExtras.Controls.Add(this.label15);
            this.groupBoxExtras.Controls.Add(this.textBoxRecibeDesc);
            this.groupBoxExtras.Controls.Add(this.textBoxRecibe);
            this.groupBoxExtras.Controls.Add(this.textBoxUsuarioDesc);
            this.groupBoxExtras.Controls.Add(this.label10);
            this.groupBoxExtras.Controls.Add(this.textBoxUsuario);
            this.groupBoxExtras.Controls.Add(this.textBoxProductoDesc);
            this.groupBoxExtras.Controls.Add(this.labelMontoPagado);
            this.groupBoxExtras.Controls.Add(this.textBoxProducto);
            this.groupBoxExtras.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.groupBoxExtras.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBoxExtras.Location = new System.Drawing.Point(31, 311);
            this.groupBoxExtras.Name = "groupBoxExtras";
            this.groupBoxExtras.Size = new System.Drawing.Size(1030, 87);
            this.groupBoxExtras.TabIndex = 124;
            this.groupBoxExtras.TabStop = false;
            this.groupBoxExtras.Text = "Extras";
            // 
            // dateTimePickerFechaTermino
            // 
            this.dateTimePickerFechaTermino.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dateTimePickerFechaTermino.CustomFormat = "dddd dd/MM/yyyy hh:mm:tt";
            this.dateTimePickerFechaTermino.Enabled = false;
            this.dateTimePickerFechaTermino.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dateTimePickerFechaTermino.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFechaTermino.Location = new System.Drawing.Point(145, 23);
            this.dateTimePickerFechaTermino.Name = "dateTimePickerFechaTermino";
            this.dateTimePickerFechaTermino.Size = new System.Drawing.Size(238, 23);
            this.dateTimePickerFechaTermino.TabIndex = 130;
            this.dateTimePickerFechaTermino.TabStop = false;
            this.dateTimePickerFechaTermino.Value = new System.DateTime(3000, 1, 1, 0, 0, 0, 0);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.Control;
            this.label15.Location = new System.Drawing.Point(11, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(126, 21);
            this.label15.TabIndex = 129;
            this.label15.Text = "Fecha de Baja:";
            // 
            // textBoxRecibeDesc
            // 
            this.textBoxRecibeDesc.Enabled = false;
            this.textBoxRecibeDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRecibeDesc.Location = new System.Drawing.Point(698, 49);
            this.textBoxRecibeDesc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxRecibeDesc.Name = "textBoxRecibeDesc";
            this.textBoxRecibeDesc.ReadOnly = true;
            this.textBoxRecibeDesc.Size = new System.Drawing.Size(319, 24);
            this.textBoxRecibeDesc.TabIndex = 128;
            this.textBoxRecibeDesc.TabStop = false;
            // 
            // textBoxRecibe
            // 
            this.textBoxRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRecibe.Location = new System.Drawing.Point(576, 49);
            this.textBoxRecibe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxRecibe.Name = "textBoxRecibe";
            this.textBoxRecibe.Size = new System.Drawing.Size(120, 24);
            this.textBoxRecibe.TabIndex = 7;
            this.textBoxRecibe.TextChanged += new System.EventHandler(this.textBoxRecibe_TextChanged);
            this.textBoxRecibe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRecibe_KeyDown);
            this.textBoxRecibe.Validated += new System.EventHandler(this.textBoxRecibe_Validated);
            // 
            // textBoxUsuarioDesc
            // 
            this.textBoxUsuarioDesc.Enabled = false;
            this.textBoxUsuarioDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUsuarioDesc.Location = new System.Drawing.Point(267, 49);
            this.textBoxUsuarioDesc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxUsuarioDesc.Name = "textBoxUsuarioDesc";
            this.textBoxUsuarioDesc.ReadOnly = true;
            this.textBoxUsuarioDesc.Size = new System.Drawing.Size(297, 24);
            this.textBoxUsuarioDesc.TabIndex = 126;
            this.textBoxUsuarioDesc.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Control;
            this.label10.Location = new System.Drawing.Point(11, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 21);
            this.label10.TabIndex = 125;
            this.label10.Text = "Usuario/Recibe:";
            // 
            // textBoxUsuario
            // 
            this.textBoxUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUsuario.Location = new System.Drawing.Point(145, 49);
            this.textBoxUsuario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxUsuario.Name = "textBoxUsuario";
            this.textBoxUsuario.Size = new System.Drawing.Size(120, 24);
            this.textBoxUsuario.TabIndex = 6;
            this.textBoxUsuario.TextChanged += new System.EventHandler(this.textBoxUsuario_TextChanged);
            this.textBoxUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUsuario_KeyDown);
            this.textBoxUsuario.Validated += new System.EventHandler(this.textBoxUsuario_Validated);
            // 
            // textBoxProductoDesc
            // 
            this.textBoxProductoDesc.Enabled = false;
            this.textBoxProductoDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProductoDesc.Location = new System.Drawing.Point(632, 23);
            this.textBoxProductoDesc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxProductoDesc.Name = "textBoxProductoDesc";
            this.textBoxProductoDesc.ReadOnly = true;
            this.textBoxProductoDesc.Size = new System.Drawing.Size(385, 24);
            this.textBoxProductoDesc.TabIndex = 123;
            this.textBoxProductoDesc.TabStop = false;
            // 
            // groupBoxBuscar
            // 
            this.groupBoxBuscar.Controls.Add(this.chkIncluirInactTerm);
            this.groupBoxBuscar.Controls.Add(this.lblInclInactTerm);
            this.groupBoxBuscar.Controls.Add(this.checkBoxFechaTermino);
            this.groupBoxBuscar.Controls.Add(this.checkBoxFechaAlta);
            this.groupBoxBuscar.Controls.Add(this.dataGridViewNotificaciones);
            this.groupBoxBuscar.Controls.Add(this.dateTimePickerBuscaFechaAlta);
            this.groupBoxBuscar.Controls.Add(this.label14);
            this.groupBoxBuscar.Controls.Add(this.comboBoxBuscaStatus);
            this.groupBoxBuscar.Controls.Add(this.comboBoxBuscaPrioridad);
            this.groupBoxBuscar.Controls.Add(this.comboBoxBuscaTipo);
            this.groupBoxBuscar.Controls.Add(this.label2);
            this.groupBoxBuscar.Controls.Add(this.label11);
            this.groupBoxBuscar.Controls.Add(this.label12);
            this.groupBoxBuscar.Controls.Add(this.dateTimePickerBuscaFechaTermino);
            this.groupBoxBuscar.Controls.Add(this.label13);
            this.groupBoxBuscar.Controls.Add(this.label3);
            this.groupBoxBuscar.Controls.Add(this.textBoxBuscaNoMensaje);
            this.groupBoxBuscar.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.groupBoxBuscar.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBoxBuscar.Location = new System.Drawing.Point(31, 404);
            this.groupBoxBuscar.Name = "groupBoxBuscar";
            this.groupBoxBuscar.Size = new System.Drawing.Size(1030, 225);
            this.groupBoxBuscar.TabIndex = 124;
            this.groupBoxBuscar.TabStop = false;
            this.groupBoxBuscar.Text = "Búsqueda Avanzada";
            this.groupBoxBuscar.Visible = false;
            // 
            // chkIncluirInactTerm
            // 
            this.chkIncluirInactTerm.AutoSize = true;
            this.chkIncluirInactTerm.Location = new System.Drawing.Point(12, 51);
            this.chkIncluirInactTerm.Name = "chkIncluirInactTerm";
            this.chkIncluirInactTerm.Size = new System.Drawing.Size(15, 14);
            this.chkIncluirInactTerm.TabIndex = 12;
            this.chkIncluirInactTerm.UseVisualStyleBackColor = true;
            this.chkIncluirInactTerm.CheckedChanged += new System.EventHandler(this.chkIncluirInactTerm_CheckedChanged);
            // 
            // lblInclInactTerm
            // 
            this.lblInclInactTerm.AutoSize = true;
            this.lblInclInactTerm.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lblInclInactTerm.ForeColor = System.Drawing.SystemColors.Control;
            this.lblInclInactTerm.Location = new System.Drawing.Point(33, 49);
            this.lblInclInactTerm.Name = "lblInclInactTerm";
            this.lblInclInactTerm.Size = new System.Drawing.Size(180, 17);
            this.lblInclInactTerm.TabIndex = 143;
            this.lblInclInactTerm.Text = "Incluir Inactivos y Terminados";
            // 
            // checkBoxFechaTermino
            // 
            this.checkBoxFechaTermino.AutoSize = true;
            this.checkBoxFechaTermino.Location = new System.Drawing.Point(635, 51);
            this.checkBoxFechaTermino.Name = "checkBoxFechaTermino";
            this.checkBoxFechaTermino.Size = new System.Drawing.Size(15, 14);
            this.checkBoxFechaTermino.TabIndex = 15;
            this.checkBoxFechaTermino.UseVisualStyleBackColor = true;
            this.checkBoxFechaTermino.CheckedChanged += new System.EventHandler(this.checkBoxFechaTermino_CheckedChanged);
            // 
            // checkBoxFechaAlta
            // 
            this.checkBoxFechaAlta.AutoSize = true;
            this.checkBoxFechaAlta.Location = new System.Drawing.Point(253, 51);
            this.checkBoxFechaAlta.Name = "checkBoxFechaAlta";
            this.checkBoxFechaAlta.Size = new System.Drawing.Size(15, 14);
            this.checkBoxFechaAlta.TabIndex = 13;
            this.checkBoxFechaAlta.UseVisualStyleBackColor = true;
            this.checkBoxFechaAlta.CheckedChanged += new System.EventHandler(this.checkBoxFechaAlta_CheckedChanged);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewNotificaciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewNotificaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewNotificaciones.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewNotificaciones.EnableHeadersVisualStyles = false;
            this.dataGridViewNotificaciones.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridViewNotificaciones.Location = new System.Drawing.Point(14, 73);
            this.dataGridViewNotificaciones.MultiSelect = false;
            this.dataGridViewNotificaciones.Name = "dataGridViewNotificaciones";
            this.dataGridViewNotificaciones.ReadOnly = true;
            this.dataGridViewNotificaciones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewNotificaciones.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewNotificaciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewNotificaciones.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewNotificaciones.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewNotificaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewNotificaciones.ShowEditingIcon = false;
            this.dataGridViewNotificaciones.Size = new System.Drawing.Size(1003, 146);
            this.dataGridViewNotificaciones.TabIndex = 140;
            this.dataGridViewNotificaciones.TabStop = false;
            this.dataGridViewNotificaciones.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewNotificaciones_CellMouseDoubleClick);
            // 
            // dateTimePickerBuscaFechaAlta
            // 
            this.dateTimePickerBuscaFechaAlta.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dateTimePickerBuscaFechaAlta.CustomFormat = "dddd dd/MM/yyyy";
            this.dateTimePickerBuscaFechaAlta.Enabled = false;
            this.dateTimePickerBuscaFechaAlta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerBuscaFechaAlta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerBuscaFechaAlta.Location = new System.Drawing.Point(375, 49);
            this.dateTimePickerBuscaFechaAlta.Name = "dateTimePickerBuscaFechaAlta";
            this.dateTimePickerBuscaFechaAlta.Size = new System.Drawing.Size(238, 20);
            this.dateTimePickerBuscaFechaAlta.TabIndex = 14;
            this.dateTimePickerBuscaFechaAlta.TabStop = false;
            this.dateTimePickerBuscaFechaAlta.Value = new System.DateTime(3000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerBuscaFechaAlta.ValueChanged += new System.EventHandler(this.dateTimePickerBuscaFechaAlta_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.label14.ForeColor = System.Drawing.SystemColors.Control;
            this.label14.Location = new System.Drawing.Point(274, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 17);
            this.label14.TabIndex = 139;
            this.label14.Text = "Fecha de Alta:";
            // 
            // comboBoxBuscaStatus
            // 
            this.comboBoxBuscaStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuscaStatus.Enabled = false;
            this.comboBoxBuscaStatus.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.comboBoxBuscaStatus.FormattingEnabled = true;
            this.comboBoxBuscaStatus.Items.AddRange(new object[] {
            "",
            "Activo",
            "Inactivo",
            "Terminado"});
            this.comboBoxBuscaStatus.Location = new System.Drawing.Point(824, 21);
            this.comboBoxBuscaStatus.Name = "comboBoxBuscaStatus";
            this.comboBoxBuscaStatus.Size = new System.Drawing.Size(192, 24);
            this.comboBoxBuscaStatus.TabIndex = 11;
            this.comboBoxBuscaStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxBuscaStatus_SelectedIndexChanged);
            // 
            // comboBoxBuscaPrioridad
            // 
            this.comboBoxBuscaPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuscaPrioridad.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.comboBoxBuscaPrioridad.FormattingEnabled = true;
            this.comboBoxBuscaPrioridad.Items.AddRange(new object[] {
            "",
            "Normal",
            "Baja",
            "Alta"});
            this.comboBoxBuscaPrioridad.Location = new System.Drawing.Point(573, 21);
            this.comboBoxBuscaPrioridad.Name = "comboBoxBuscaPrioridad";
            this.comboBoxBuscaPrioridad.Size = new System.Drawing.Size(192, 24);
            this.comboBoxBuscaPrioridad.TabIndex = 10;
            this.comboBoxBuscaPrioridad.SelectedIndexChanged += new System.EventHandler(this.comboBoxBuscaPrioridad_SelectedIndexChanged);
            // 
            // comboBoxBuscaTipo
            // 
            this.comboBoxBuscaTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuscaTipo.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.comboBoxBuscaTipo.FormattingEnabled = true;
            this.comboBoxBuscaTipo.Items.AddRange(new object[] {
            "",
            "Informativo",
            "Cambio en producto",
            "Aviso",
            "Encomienda",
            "Urgente"});
            this.comboBoxBuscaTipo.Location = new System.Drawing.Point(302, 21);
            this.comboBoxBuscaTipo.Name = "comboBoxBuscaTipo";
            this.comboBoxBuscaTipo.Size = new System.Drawing.Size(192, 24);
            this.comboBoxBuscaTipo.TabIndex = 9;
            this.comboBoxBuscaTipo.SelectedIndexChanged += new System.EventHandler(this.comboBoxBuscaTipo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(503, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 134;
            this.label2.Text = "Prioridad:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(261, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 17);
            this.label11.TabIndex = 133;
            this.label11.Text = "Tipo:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.label12.ForeColor = System.Drawing.SystemColors.Control;
            this.label12.Location = new System.Drawing.Point(771, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 17);
            this.label12.TabIndex = 132;
            this.label12.Text = "Estatus:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.label13.ForeColor = System.Drawing.SystemColors.Control;
            this.label13.Location = new System.Drawing.Point(9, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 17);
            this.label13.TabIndex = 131;
            this.label13.Text = "No. Mensaje:";
            // 
            // textBoxBuscaNoMensaje
            // 
            this.textBoxBuscaNoMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxBuscaNoMensaje.Location = new System.Drawing.Point(111, 23);
            this.textBoxBuscaNoMensaje.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxBuscaNoMensaje.Name = "textBoxBuscaNoMensaje";
            this.textBoxBuscaNoMensaje.Size = new System.Drawing.Size(144, 21);
            this.textBoxBuscaNoMensaje.TabIndex = 8;
            this.textBoxBuscaNoMensaje.TextChanged += new System.EventHandler(this.textBoxBuscaNoMensaje_TextChanged);
            // 
            // lblCargaMensajes
            // 
            this.lblCargaMensajes.AutoSize = true;
            this.lblCargaMensajes.Font = new System.Drawing.Font("Century Gothic", 48F);
            this.lblCargaMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCargaMensajes.Location = new System.Drawing.Point(36, 11);
            this.lblCargaMensajes.Name = "lblCargaMensajes";
            this.lblCargaMensajes.Size = new System.Drawing.Size(999, 78);
            this.lblCargaMensajes.TabIndex = 1;
            this.lblCargaMensajes.Text = "Cargando Mensajes... Espere...";
            // 
            // buttonTerminado
            // 
            this.buttonTerminado.Enabled = false;
            this.buttonTerminado.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.buttonTerminado.Location = new System.Drawing.Point(860, 285);
            this.buttonTerminado.Name = "buttonTerminado";
            this.buttonTerminado.Size = new System.Drawing.Size(187, 28);
            this.buttonTerminado.TabIndex = 126;
            this.buttonTerminado.TabStop = false;
            this.buttonTerminado.Text = "Marcar como Terminado";
            this.buttonTerminado.UseVisualStyleBackColor = true;
            this.buttonTerminado.Click += new System.EventHandler(this.buttonTerminado_Click);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.buttonGuardar.Location = new System.Drawing.Point(745, 285);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(109, 28);
            this.buttonGuardar.TabIndex = 125;
            this.buttonGuardar.TabStop = false;
            this.buttonGuardar.Text = "Guardar";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // comboBoxTipo
            // 
            this.comboBoxTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipo.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.comboBoxTipo.FormattingEnabled = true;
            this.comboBoxTipo.ItemHeight = 17;
            this.comboBoxTipo.Items.AddRange(new object[] {
            "Informativo",
            "Cambio en producto",
            "Aviso",
            "Encomienda",
            "Urgente"});
            this.comboBoxTipo.Location = new System.Drawing.Point(349, 181);
            this.comboBoxTipo.Name = "comboBoxTipo";
            this.comboBoxTipo.Size = new System.Drawing.Size(192, 25);
            this.comboBoxTipo.TabIndex = 2;
            this.comboBoxTipo.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipo_SelectedIndexChanged);
            // 
            // comboBoxPrioridad
            // 
            this.comboBoxPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrioridad.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.comboBoxPrioridad.FormattingEnabled = true;
            this.comboBoxPrioridad.Items.AddRange(new object[] {
            "Normal",
            "Baja",
            "Alta"});
            this.comboBoxPrioridad.Location = new System.Drawing.Point(635, 181);
            this.comboBoxPrioridad.Name = "comboBoxPrioridad";
            this.comboBoxPrioridad.Size = new System.Drawing.Size(192, 25);
            this.comboBoxPrioridad.TabIndex = 3;
            this.comboBoxPrioridad.SelectedIndexChanged += new System.EventHandler(this.comboBoxPrioridad_SelectedIndexChanged);
            // 
            // panelCargandoMensajes
            // 
            this.panelCargandoMensajes.Controls.Add(this.lblCargaMensajes);
            this.panelCargandoMensajes.Location = new System.Drawing.Point(12, 429);
            this.panelCargandoMensajes.Name = "panelCargandoMensajes";
            this.panelCargandoMensajes.Size = new System.Drawing.Size(1063, 100);
            this.panelCargandoMensajes.TabIndex = 129;
            // 
            // buttonInactivar
            // 
            this.buttonInactivar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.buttonInactivar.Location = new System.Drawing.Point(42, 233);
            this.buttonInactivar.Name = "buttonInactivar";
            this.buttonInactivar.Size = new System.Drawing.Size(109, 47);
            this.buttonInactivar.TabIndex = 130;
            this.buttonInactivar.TabStop = false;
            this.buttonInactivar.Text = "Inactivar Mensaje";
            this.buttonInactivar.UseVisualStyleBackColor = true;
            this.buttonInactivar.Click += new System.EventHandler(this.buttonInactivar_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Files_2_icon;
            this.pictureBoxLogo.Location = new System.Drawing.Point(192, 18);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(135, 135);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 3;
            this.pictureBoxLogo.TabStop = false;
            // 
            // Notificaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(1087, 641);
            this.Controls.Add(this.buttonInactivar);
            this.Controls.Add(this.panelCargandoMensajes);
            this.Controls.Add(this.comboBoxPrioridad);
            this.Controls.Add(this.comboBoxTipo);
            this.Controls.Add(this.buttonTerminado);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxMensaje);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.dateTimePickerFechaAlta);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxNoMensaje);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxExtras);
            this.Controls.Add(this.groupBoxBuscar);
            this.Name = "Notificaciones";
            this.Text = "Notificaciones";
            this.Load += new System.EventHandler(this.Notificaciones_Load);
            this.groupBoxExtras.ResumeLayout(false);
            this.groupBoxExtras.PerformLayout();
            this.groupBoxBuscar.ResumeLayout(false);
            this.groupBoxBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotificaciones)).EndInit();
            this.panelCargandoMensajes.ResumeLayout(false);
            this.panelCargandoMensajes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMontoPagado;
        public System.Windows.Forms.TextBox textBoxProducto;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxMensaje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaAlta;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox textBoxNoMensaje;
        private System.Windows.Forms.DateTimePicker dateTimePickerBuscaFechaTermino;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxExtras;
        public System.Windows.Forms.TextBox textBoxRecibeDesc;
        public System.Windows.Forms.TextBox textBoxRecibe;
        public System.Windows.Forms.TextBox textBoxUsuarioDesc;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox textBoxUsuario;
        public System.Windows.Forms.TextBox textBoxProductoDesc;
        private System.Windows.Forms.GroupBox groupBoxBuscar;
        private System.Windows.Forms.ComboBox comboBoxBuscaPrioridad;
        private System.Windows.Forms.ComboBox comboBoxBuscaTipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox textBoxBuscaNoMensaje;
        private System.Windows.Forms.Button buttonTerminado;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.ComboBox comboBoxTipo;
        private System.Windows.Forms.ComboBox comboBoxPrioridad;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaTermino;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dateTimePickerBuscaFechaAlta;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxBuscaStatus;
        private System.Windows.Forms.CheckBox checkBoxFechaTermino;
        private System.Windows.Forms.CheckBox checkBoxFechaAlta;
        private System.Windows.Forms.DataGridView dataGridViewNotificaciones;
        private System.Windows.Forms.CheckBox chkIncluirInactTerm;
        private System.Windows.Forms.Label lblInclInactTerm;
        private System.Windows.Forms.Label lblCargaMensajes;
        private System.Windows.Forms.Panel panelCargandoMensajes;
        private System.Windows.Forms.Button buttonInactivar;
    }
}