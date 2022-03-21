using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Productos : Form
    {

        public Productos()
        {
            InitializeComponent();
        }

        DataTable dtProductos = new DataTable();
        bool actualizandoTabla = false;
        int rowIndex = 0;

        private void Productos_Load(object sender, EventArgs e)
        {
            loadAll();
        }

        /// <summary>
        /// Evento que inicia la ventana Añadir productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Añadir_Producto a = new Añadir_Producto(false);
            a.ShowDialog();
            if (a.añadido)
            {
                loadAll();
            }  
        }

        /// <summary>
        /// Método que inicia la ventana y carga la tabla
        /// </summary>
        public void loadAll()
        {

            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection)) 
            {

                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {

                    dtProductos = new Utilerias().ejecutaComando("SELECT id_producto Código,  nombre Nombre,  precio Precio, " +
                                                                 "       cantidad Cantidad,   marca Marca,    categoria Categoría," +
                                                                 "       fechaultact \"Fecha Última Actualización\" " +
                                                                 "FROM   productos " +
                                                                 "WHERE  status <> 0 " +
                                                                 "ORDER BY id_producto",
                                                                 CommandType.Text,
                                                                 openCon,
                                                                 transaction,
                                                                 $"Código LIKE '%{new Utilerias().verifyQuotes(textBoxBuscar.Text)}%' OR Nombre Like '%{new Utilerias().verifyQuotes(textBoxBuscar.Text)}%'");

                    dataGridViewProductos.DataSource = dtProductos;


                    if (dataGridViewProductos.RowCount > 0)
                    {

                        dataGridViewProductos.CurrentCell = dataGridViewProductos.Rows[rowIndex].Cells[0];
                        dataGridViewProductos.Rows[rowIndex].Selected = true;

                        textBoxCodigo.Text = dataGridViewProductos.Rows[rowIndex].Cells[0].Value.ToString();
                        textBoxNombre.Text = dataGridViewProductos.Rows[rowIndex].Cells[1].Value.ToString();
                        textBoxPrecio.Text = dataGridViewProductos.Rows[rowIndex].Cells[2].Value.ToString();
                        textBoxCantidad.Text = dataGridViewProductos.Rows[rowIndex].Cells[3].Value.ToString();
                        textBoxMarca.Text = dataGridViewProductos.Rows[rowIndex].Cells[4].Value.ToString();
                        textBoxCategoria.Text = dataGridViewProductos.Rows[rowIndex].Cells[5].Value.ToString();
                        dateTimePickerFechaUltAct.Value = (DateTime)dataGridViewProductos.Rows[rowIndex].Cells[6].Value;

                        entradaDeProductoToolStripMenuItem.Enabled = true;
                        editarToolStripMenuItem.Enabled = true;
                        eliminarToolStripMenuItem.Enabled = true;

                        dataGridViewProductos.AutoResizeColumns();
                        //dataGridViewProductos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                        for (int i = 0; i < dataGridViewProductos.ColumnCount; i++)
                        {
                            dataGridViewProductos.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }

                    }
                    else
                    {
                        textBoxCodigo.Text = "";
                        textBoxNombre.Text = "";
                        textBoxMarca.Text = "";
                        textBoxCategoria.Text = "";
                        textBoxCantidad.Text = "";
                        textBoxPrecio.Text = "";
                        dateTimePickerFechaUltAct.Value = DateTime.Parse("01/01/3000");

                        entradaDeProductoToolStripMenuItem.Enabled = false;
                        editarToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                    }
                }
                catch (SqlException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (ArgumentOutOfRangeException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        

        /// <summary>
        /// Evento que abre la venta Editar producto, envía los datos neccesarios a editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string codigo = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            string nombre = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
            double precio = double.Parse(dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString());
            double cantidad = double.Parse(dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[3].Value.ToString());
            string marca = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
            string categoria = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
            
            Editar_Producto a = new Editar_Producto(codigo, nombre, marca, categoria, cantidad, precio);
            a.ShowDialog();
            if (a.editado)
            {
                loadAll();
            }  
        }

        /// <summary>
        /// Evento que elimina un producto de la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contraseña con = new Contraseña();
            if (con.DialogResult == DialogResult.No)
            {
                con.Dispose();
            }
            if (con.ShowDialog(this) == DialogResult.Yes && con.textBoxContraseña.Text.Equals("hancock7"))
            {
                //Si la respuesta es si, desactivamos el producto de la base de datos
                if (MessageBox.Show("¿Estás seguro de que quieres inactivar el producto '" + dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString() + "'?",
                    "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        try
                        {

                            new Utilerias().ejecutaComando("UPDATE productos " +
                                                           "SET    status      = 0 " +
                                                           "WHERE  id_producto = @id_producto",
                                                           CommandType.Text,
                                                           openCon,
                                                           transaction,
                                                           "",
                                                           new object[] { "@id_producto", dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString()});

                            transaction.Commit();

                            MessageBox.Show("'" + textBoxNombre.Text + "' ha sido desactivado correctamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Esto es porque si se desactiva un producto, y no se actualiza la cantidad de productos, truena porque quiere ir al index que es mayor
                            // a la cantidad de productos.
                            if (rowIndex > dataGridViewProductos.RowCount)
                            {
                                rowIndex = dataGridViewProductos.RowCount <= 1 ? 0 : dataGridViewProductos.RowCount - 1;
                            }

                            loadAll();

                        }
                        catch (SqlException sqlEx)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha habido un error. Verifica lo siguiente:\n\n- Verifica la conexión a la base de datos y prueba de nuevo\n- Verifica que el código de barras del producto a desactivar es correcto.\n\nError: " + sqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            if (con.DialogResult == DialogResult.Yes && !con.textBoxContraseña.Text.Equals("hancock7"))
            {
                MessageBox.Show("Contraseña incorrecta. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            obtenBusqueda();
        }

        /// <summary>
        /// Evento que llena los campos al dar click en una celda de la tabla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewProductos.RowCount > 0 && !actualizandoTabla)
            {

                textBoxCodigo.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                textBoxPrecio.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                textBoxCantidad.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                textBoxMarca.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                textBoxCategoria.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
                dateTimePickerFechaUltAct.Value = (DateTime)dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[6].Value;

                rowIndex = dataGridViewProductos.SelectedCells[0].RowIndex;
            }
        }

        /// <summary>
        /// Evento que llena los campos al dar click en una celda de la tabla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewProductos.RowCount > 0 && !actualizandoTabla)
            {
                textBoxCodigo.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                textBoxPrecio.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                textBoxCantidad.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                textBoxMarca.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                textBoxCategoria.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
                dateTimePickerFechaUltAct.Value = (DateTime)dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[6].Value;

                rowIndex = dataGridViewProductos.SelectedCells[0].RowIndex;
            }
        }

        private void dataGridViewProductos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewProductos.RowCount > 0 && !actualizandoTabla)
                {
                    textBoxCodigo.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                    textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                    textBoxPrecio.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                    textBoxCantidad.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                    textBoxMarca.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                    textBoxCategoria.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
                    dateTimePickerFechaUltAct.Value = (DateTime)dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[6].Value;

                    rowIndex = dataGridViewProductos.SelectedCells[0].RowIndex;

                }
            }
            catch (ArgumentOutOfRangeException) { }
            catch (NullReferenceException) { }
        }

        private void entradaDeProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Añadir_Cantidad a = new Añadir_Cantidad();
            a.textBoxCodigo.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            a.textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
            a.ShowDialog();
            if (a.añadido)
            {
                loadAll();
            }
        }

        private void productosEnCeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Productos_Cero().ShowDialog();
        }

        private void textBoxBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar)|| e.KeyChar.ToString().Equals("'") || e.KeyChar.ToString().Equals("&") || e.KeyChar.ToString().Equals("%"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void productosInactivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();
                try
                {


                    DataTable dtProductos = new Utilerias().ejecutaComando("SELECT COUNT(*) " +
                                                                           "FROM   productos " +
                                                                           "WHERE  status = 0",
                                                                           CommandType.Text,
                                                                           openCon,
                                                                           transaction);

                    if (int.Parse(dtProductos.Rows[0][0].ToString()) > 0)
                    {
                        Productos_Inactivos a = new Productos_Inactivos();
                        a.ShowDialog();
                        if (a.reactivado)
                        {
                            loadAll();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay productos inactivos, no es necesario acceder", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void descuentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Accion_Productos("Descuento").ShowDialog();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contraseña con = new Contraseña();
            if (con.DialogResult == DialogResult.No)
            {
                con.Dispose();
            }
            else if (con.ShowDialog(this) == DialogResult.Yes && con.textBoxContraseña.Text.Equals("hancock7"))
            {
                new Usuarios(true).ShowDialog();
            }
            else if (con.DialogResult == DialogResult.Yes && !con.textBoxContraseña.Text.Equals("hancock7"))
            {
                new Usuarios(false).ShowDialog();
            } 
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acceso a = new Acceso();
            a.ShowDialog();
            if (a.enter)
            {
                new Clientes(a.usuclave).ShowDialog();
            }
        }

        private void notasDeCréditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Nota_Credito(0, null, 0).ShowDialog();
        }

        public void obtenBusqueda()
        {
            try
            {
                dtProductos.DefaultView.RowFilter = $"Código LIKE '%{new Utilerias().verifyQuotes(textBoxBuscar.Text)}%' OR Nombre Like '%{new Utilerias().verifyQuotes(textBoxBuscar.Text)}%'";

                if (dataGridViewProductos.RowCount > 0)
                {
                    dataGridViewProductos.CurrentCell = dataGridViewProductos.Rows[rowIndex].Cells[0];
                    dataGridViewProductos.Rows[rowIndex].Selected = true;

                    textBoxCodigo.Text = dataGridViewProductos.Rows[rowIndex].Cells[0].Value.ToString();
                    textBoxNombre.Text = dataGridViewProductos.Rows[rowIndex].Cells[1].Value.ToString();
                    textBoxPrecio.Text = dataGridViewProductos.Rows[rowIndex].Cells[2].Value.ToString();
                    textBoxCantidad.Text = dataGridViewProductos.Rows[rowIndex].Cells[3].Value.ToString();
                    textBoxMarca.Text = dataGridViewProductos.Rows[rowIndex].Cells[4].Value.ToString();
                    textBoxCategoria.Text = dataGridViewProductos.Rows[rowIndex].Cells[5].Value.ToString();
                    dateTimePickerFechaUltAct.Value = (DateTime)dataGridViewProductos.Rows[rowIndex].Cells[6].Value;

                    dataGridViewProductos.AutoResizeColumns();
                    //dataGridViewProductos.Columns[dataGridViewProductos.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    entradaDeProductoToolStripMenuItem.Enabled = true;
                    editarToolStripMenuItem.Enabled = true;
                    eliminarToolStripMenuItem.Enabled = true;
                }
                else
                {
                    rowIndex = 0;

                    textBoxCodigo.Text = "";
                    textBoxNombre.Text = "";
                    textBoxMarca.Text = "";
                    textBoxCategoria.Text = "";
                    textBoxCantidad.Text = "";
                    textBoxPrecio.Text = "";
                    dateTimePickerFechaUltAct.Value = DateTime.Parse("01/01/3000");

                    entradaDeProductoToolStripMenuItem.Enabled = false;
                    editarToolStripMenuItem.Enabled = false;
                    eliminarToolStripMenuItem.Enabled = false;

                }

            }
            catch (SqlException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void actualizaProductos()
        {
            //El codigo siguiente actualiza la tabla de productos cada 10 segundos, de manera que si hay cambios, y aun no se refresca la tabla, se muestren dichos cambios
            actualizandoTabla = true;

            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {

                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {

                    dtProductos = new Utilerias().ejecutaComando("SELECT id_producto Código,  nombre Nombre,  precio Precio, " +
                                                                 "       cantidad Cantidad,   marca Marca,    categoria Categoría, " +
                                                                 "       fechaultact \"Fecha Última Actualización\" " +
                                                                 "FROM   productos " +
                                                                 "WHERE  status <> 0 " +
                                                                 "ORDER BY id_producto",
                                                                 CommandType.Text,
                                                                 openCon,
                                                                 transaction,
                                                                 $"Código LIKE '%{new Utilerias().verifyQuotes(textBoxBuscar.Text)}%' OR Nombre Like '%{new Utilerias().verifyQuotes(textBoxBuscar.Text)}%'");

                    dataGridViewProductos.DataSource = dtProductos;

                    if (dataGridViewProductos.RowCount > 0)
                    {
                        if (rowIndex >= dataGridViewProductos.RowCount)
                        {
                            rowIndex = dataGridViewProductos.RowCount <= 1 ? 0 : dataGridViewProductos.RowCount - 1;
                        }

                        dataGridViewProductos.CurrentCell = dataGridViewProductos.Rows[rowIndex].Cells[0];
                        dataGridViewProductos.Rows[rowIndex].Selected = true;
                    }
                    else
                    {
                        rowIndex = 0;
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Ha ocurrido un problema. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            actualizandoTabla = false;
        }

        private void bgProductos_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(10000);
                BeginInvoke(new MethodInvoker(actualizaProductos));
            }
        }

        private void Productos_Shown(object sender, EventArgs e)
        {
            bgProductos.RunWorkerAsync();
        }
    }
}
