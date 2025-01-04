using SANTA_Punto_de_Venta.Vistas;
namespace SANTA_Punto_de_Venta.Vistas
{
    partial class Productos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxPrecio = new System.Windows.Forms.TextBox();
            this.textBoxCantidad = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menúToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.añadirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entradaDeProductoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desactivarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosEnCeroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosInactivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descuentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.admnistraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notasDeCréditoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxMarca = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCodigo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCategoria = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePickerFechaUltAct = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewProductos = new System.Windows.Forms.DataGridView();
            this.bgProductos = new System.ComponentModel.BackgroundWorker();
            this.lblCargaProductos = new System.Windows.Forms.Label();
            this.pnlProductos = new System.Windows.Forms.Panel();
            this.textBoxBuscar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.productosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sANTADataSetProductos = new SANTA_Punto_de_Venta.SANTADataSet();
            this.productosTableAdapter = new SANTA_Punto_de_Venta.SANTADataSetTableAdapters.productosTableAdapter();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).BeginInit();
            this.pnlProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANTADataSetProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(503, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Productos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(30, 385);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nombre:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(30, 421);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Precio:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(230, 421);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "Cantidad:";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(113, 382);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.ReadOnly = true;
            this.textBoxNombre.Size = new System.Drawing.Size(416, 27);
            this.textBoxNombre.TabIndex = 3;
            // 
            // textBoxPrecio
            // 
            this.textBoxPrecio.Location = new System.Drawing.Point(97, 418);
            this.textBoxPrecio.Name = "textBoxPrecio";
            this.textBoxPrecio.ReadOnly = true;
            this.textBoxPrecio.Size = new System.Drawing.Size(127, 27);
            this.textBoxPrecio.TabIndex = 5;
            // 
            // textBoxCantidad
            // 
            this.textBoxCantidad.Location = new System.Drawing.Point(327, 418);
            this.textBoxCantidad.Name = "textBoxCantidad";
            this.textBoxCantidad.ReadOnly = true;
            this.textBoxCantidad.Size = new System.Drawing.Size(130, 27);
            this.textBoxCantidad.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menúToolStripMenuItem,
            this.verToolStripMenuItem,
            this.admnistraciónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1087, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menúToolStripMenuItem
            // 
            this.menúToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.añadirToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.desactivarToolStripMenuItem});
            this.menúToolStripMenuItem.Name = "menúToolStripMenuItem";
            this.menúToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menúToolStripMenuItem.Text = "Menú";
            // 
            // añadirToolStripMenuItem
            // 
            this.añadirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productoToolStripMenuItem,
            this.entradaDeProductoToolStripMenuItem});
            this.añadirToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.add_icon;
            this.añadirToolStripMenuItem.Name = "añadirToolStripMenuItem";
            this.añadirToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.añadirToolStripMenuItem.Text = "Añadir...";
            // 
            // productoToolStripMenuItem
            // 
            this.productoToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.stat_icon;
            this.productoToolStripMenuItem.Name = "productoToolStripMenuItem";
            this.productoToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.productoToolStripMenuItem.Text = "Producto";
            this.productoToolStripMenuItem.Click += new System.EventHandler(this.productoToolStripMenuItem_Click);
            // 
            // entradaDeProductoToolStripMenuItem
            // 
            this.entradaDeProductoToolStripMenuItem.Enabled = false;
            this.entradaDeProductoToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.stat_icon__1_;
            this.entradaDeProductoToolStripMenuItem.Name = "entradaDeProductoToolStripMenuItem";
            this.entradaDeProductoToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.entradaDeProductoToolStripMenuItem.Text = "Entrada de producto";
            this.entradaDeProductoToolStripMenuItem.Click += new System.EventHandler(this.entradaDeProductoToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Enabled = false;
            this.editarToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Actions_edit_undo_icon;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.editarToolStripMenuItem.Text = "Editar nombre/precio";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // desactivarToolStripMenuItem
            // 
            this.desactivarToolStripMenuItem.Enabled = false;
            this.desactivarToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Actions_edit_delete_icon;
            this.desactivarToolStripMenuItem.Name = "desactivarToolStripMenuItem";
            this.desactivarToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.desactivarToolStripMenuItem.Text = "Desactivar Producto";
            this.desactivarToolStripMenuItem.Click += new System.EventHandler(this.desactivarToolStripMenuItem_Click);
            // 
            // verToolStripMenuItem
            // 
            this.verToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productosEnCeroToolStripMenuItem,
            this.productosInactivosToolStripMenuItem,
            this.descuentosToolStripMenuItem});
            this.verToolStripMenuItem.Name = "verToolStripMenuItem";
            this.verToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.verToolStripMenuItem.Text = "Ver";
            // 
            // productosEnCeroToolStripMenuItem
            // 
            this.productosEnCeroToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.market_icon;
            this.productosEnCeroToolStripMenuItem.Name = "productosEnCeroToolStripMenuItem";
            this.productosEnCeroToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.productosEnCeroToolStripMenuItem.Text = "Productos en cero";
            this.productosEnCeroToolStripMenuItem.Click += new System.EventHandler(this.productosEnCeroToolStripMenuItem_Click);
            // 
            // productosInactivosToolStripMenuItem
            // 
            this.productosInactivosToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Status_dialog_error_icon;
            this.productosInactivosToolStripMenuItem.Name = "productosInactivosToolStripMenuItem";
            this.productosInactivosToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.productosInactivosToolStripMenuItem.Text = "Productos inactivos";
            this.productosInactivosToolStripMenuItem.Click += new System.EventHandler(this.productosInactivosToolStripMenuItem_Click);
            // 
            // descuentosToolStripMenuItem
            // 
            this.descuentosToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Ecommerce_Discount_icon;
            this.descuentosToolStripMenuItem.Name = "descuentosToolStripMenuItem";
            this.descuentosToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.descuentosToolStripMenuItem.Text = "Descuentos";
            this.descuentosToolStripMenuItem.Click += new System.EventHandler(this.descuentosToolStripMenuItem_Click);
            // 
            // admnistraciónToolStripMenuItem
            // 
            this.admnistraciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuariosToolStripMenuItem,
            this.clientesToolStripMenuItem,
            this.notasDeCréditoToolStripMenuItem});
            this.admnistraciónToolStripMenuItem.Name = "admnistraciónToolStripMenuItem";
            this.admnistraciónToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.admnistraciónToolStripMenuItem.Text = "Administración";
            // 
            // usuariosToolStripMenuItem
            // 
            this.usuariosToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Admin_icon;
            this.usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            this.usuariosToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.usuariosToolStripMenuItem.Text = "Usuarios";
            this.usuariosToolStripMenuItem.Click += new System.EventHandler(this.usuariosToolStripMenuItem_Click);
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Users_icon__1_;
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // notasDeCréditoToolStripMenuItem
            // 
            this.notasDeCréditoToolStripMenuItem.Name = "notasDeCréditoToolStripMenuItem";
            this.notasDeCréditoToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.notasDeCréditoToolStripMenuItem.Text = "Notas de Crédito";
            this.notasDeCréditoToolStripMenuItem.Click += new System.EventHandler(this.notasDeCréditoToolStripMenuItem_Click);
            // 
            // textBoxMarca
            // 
            this.textBoxMarca.Location = new System.Drawing.Point(535, 418);
            this.textBoxMarca.Name = "textBoxMarca";
            this.textBoxMarca.ReadOnly = true;
            this.textBoxMarca.Size = new System.Drawing.Size(521, 27);
            this.textBoxMarca.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(463, 421);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 21);
            this.label6.TabIndex = 13;
            this.label6.Text = "Marca:";
            // 
            // textBoxCodigo
            // 
            this.textBoxCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCodigo.Location = new System.Drawing.Point(113, 346);
            this.textBoxCodigo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCodigo.Name = "textBoxCodigo";
            this.textBoxCodigo.ReadOnly = true;
            this.textBoxCodigo.Size = new System.Drawing.Size(416, 26);
            this.textBoxCodigo.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(35, 348);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 21);
            this.label7.TabIndex = 35;
            this.label7.Text = "Código:";
            // 
            // textBoxCategoria
            // 
            this.textBoxCategoria.Location = new System.Drawing.Point(653, 382);
            this.textBoxCategoria.Name = "textBoxCategoria";
            this.textBoxCategoria.ReadOnly = true;
            this.textBoxCategoria.Size = new System.Drawing.Size(403, 27);
            this.textBoxCategoria.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(552, 385);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 21);
            this.label8.TabIndex = 39;
            this.label8.Text = "Categoría:";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::SANTA_Punto_de_Venta.Properties.Resources.Files_2_icon;
            this.pictureBoxLogo.Location = new System.Drawing.Point(353, 42);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(135, 135);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 1;
            this.pictureBoxLogo.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(552, 348);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(229, 21);
            this.label9.TabIndex = 41;
            this.label9.Text = "Fecha Última Actualización:";
            // 
            // dateTimePickerFechaUltAct
            // 
            this.dateTimePickerFechaUltAct.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dateTimePickerFechaUltAct.CustomFormat = "dddd dd/MM/yyyy hh:mm:tt";
            this.dateTimePickerFechaUltAct.Enabled = false;
            this.dateTimePickerFechaUltAct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dateTimePickerFechaUltAct.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFechaUltAct.Location = new System.Drawing.Point(787, 347);
            this.dateTimePickerFechaUltAct.Name = "dateTimePickerFechaUltAct";
            this.dateTimePickerFechaUltAct.Size = new System.Drawing.Size(269, 23);
            this.dateTimePickerFechaUltAct.TabIndex = 53;
            this.dateTimePickerFechaUltAct.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // dataGridViewProductos
            // 
            this.dataGridViewProductos.AllowUserToAddRows = false;
            this.dataGridViewProductos.AllowUserToDeleteRows = false;
            this.dataGridViewProductos.AllowUserToResizeColumns = false;
            this.dataGridViewProductos.AllowUserToResizeRows = false;
            this.dataGridViewProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProductos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            this.dataGridViewProductos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewProductos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewProductos.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewProductos.EnableHeadersVisualStyles = false;
            this.dataGridViewProductos.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridViewProductos.Location = new System.Drawing.Point(9, 46);
            this.dataGridViewProductos.MultiSelect = false;
            this.dataGridViewProductos.Name = "dataGridViewProductos";
            this.dataGridViewProductos.ReadOnly = true;
            this.dataGridViewProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewProductos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProductos.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewProductos.RowHeadersVisible = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewProductos.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProductos.ShowEditingIcon = false;
            this.dataGridViewProductos.Size = new System.Drawing.Size(1070, 283);
            this.dataGridViewProductos.TabIndex = 54;
            this.dataGridViewProductos.TabStop = false;
            this.dataGridViewProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProductos_CellClick);
            this.dataGridViewProductos.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProductos_RowEnter);
            this.dataGridViewProductos.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewProductos_Scroll);
            // 
            // bgProductos
            // 
            this.bgProductos.WorkerSupportsCancellation = true;
            this.bgProductos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgProductos_DoWork);
            // 
            // lblCargaProductos
            // 
            this.lblCargaProductos.AutoSize = true;
            this.lblCargaProductos.Font = new System.Drawing.Font("Century Gothic", 48F);
            this.lblCargaProductos.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCargaProductos.Location = new System.Drawing.Point(32, 327);
            this.lblCargaProductos.Name = "lblCargaProductos";
            this.lblCargaProductos.Size = new System.Drawing.Size(1021, 78);
            this.lblCargaProductos.TabIndex = 0;
            this.lblCargaProductos.Text = "Cargando Productos... Espere...";
            // 
            // pnlProductos
            // 
            this.pnlProductos.Controls.Add(this.textBoxBuscar);
            this.pnlProductos.Controls.Add(this.label2);
            this.pnlProductos.Controls.Add(this.dataGridViewProductos);
            this.pnlProductos.Controls.Add(this.dateTimePickerFechaUltAct);
            this.pnlProductos.Controls.Add(this.label3);
            this.pnlProductos.Controls.Add(this.label9);
            this.pnlProductos.Controls.Add(this.label4);
            this.pnlProductos.Controls.Add(this.textBoxCategoria);
            this.pnlProductos.Controls.Add(this.label5);
            this.pnlProductos.Controls.Add(this.label8);
            this.pnlProductos.Controls.Add(this.textBoxNombre);
            this.pnlProductos.Controls.Add(this.textBoxCodigo);
            this.pnlProductos.Controls.Add(this.textBoxPrecio);
            this.pnlProductos.Controls.Add(this.label7);
            this.pnlProductos.Controls.Add(this.textBoxCantidad);
            this.pnlProductos.Controls.Add(this.textBoxMarca);
            this.pnlProductos.Controls.Add(this.label6);
            this.pnlProductos.Location = new System.Drawing.Point(0, 183);
            this.pnlProductos.Name = "pnlProductos";
            this.pnlProductos.Size = new System.Drawing.Size(1087, 459);
            this.pnlProductos.TabIndex = 1;
            this.pnlProductos.Visible = false;
            // 
            // textBoxBuscar
            // 
            this.textBoxBuscar.Location = new System.Drawing.Point(81, 13);
            this.textBoxBuscar.Name = "textBoxBuscar";
            this.textBoxBuscar.Size = new System.Drawing.Size(551, 27);
            this.textBoxBuscar.TabIndex = 1;
            this.textBoxBuscar.TextChanged += new System.EventHandler(this.textBoxBuscar_TextChanged);
            this.textBoxBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBuscar_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Buscar:";
            // 
            // productosBindingSource
            // 
            this.productosBindingSource.DataMember = "productos";
            this.productosBindingSource.DataSource = this.sANTADataSetProductos;
            // 
            // sANTADataSetProductos
            // 
            this.sANTADataSetProductos.DataSetName = "SANTADataSetProductos";
            this.sANTADataSetProductos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productosTableAdapter
            // 
            this.productosTableAdapter.ClearBeforeFill = true;
            // 
            // Productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(1087, 641);
            this.Controls.Add(this.lblCargaProductos);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlProductos);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Productos";
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.Productos_Load);
            this.Shown += new System.EventHandler(this.Productos_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).EndInit();
            this.pnlProductos.ResumeLayout(false);
            this.pnlProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANTADataSetProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxPrecio;
        private System.Windows.Forms.TextBox textBoxCantidad;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menúToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem añadirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desactivarToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxMarca;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem productoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entradaDeProductoToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCategoria;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem verToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosEnCeroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosInactivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descuentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem admnistraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notasDeCréditoToolStripMenuItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaUltAct;
        private System.Windows.Forms.DataGridView dataGridViewProductos;
        private System.ComponentModel.BackgroundWorker bgProductos;
        private SANTADataSet sANTADataSetProductos;
        private SANTADataSetTableAdapters.productosTableAdapter productosTableAdapter;
        private System.Windows.Forms.BindingSource productosBindingSource;
        private System.Windows.Forms.Label lblCargaProductos;
        private System.Windows.Forms.Panel pnlProductos;
        private System.Windows.Forms.TextBox textBoxBuscar;
        private System.Windows.Forms.Label label2;
    }
}