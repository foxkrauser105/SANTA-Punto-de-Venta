using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Productos_Inactivos : Form
    {

        public bool reactivado = false;

        public Productos_Inactivos()
        {
            InitializeComponent();
        }

        public string verifyQuotes(string s)
        {
            string sNew = "";
            for (int i = 0; i < s.Length; i++)
            {
                char insert = (char)(s[i]);
                if (insert.Equals('\''))
                {
                    sNew += "'";
                }
                sNew += insert.ToString();
            }
            return sNew;
        }

        public void loadAll()
        {
            textBoxBuscar.Text = "";
            try
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();
                    SqlDataAdapter select = new SqlDataAdapter("select id_producto Código, nombre Nombre, marca Marca, categoria Categoría, cantidad Cantidad, precio Precio from productos where status = 0 ORDER BY id_producto", openCon);
                    select.SelectCommand.Transaction = transaction;
                    DataSet ds = new DataSet("Check");
                    select.FillSchema(ds, SchemaType.Source, "Productos");
                    select.Fill(ds, "Productos");
                    DataTable dtProductos = ds.Tables["Productos"];

                    dataGridViewProductos.DataSource = dtProductos;

                    if (dataGridViewProductos.RowCount == 0)
                    {
                        Dispose();
                    }

                    if (dataGridViewProductos.RowCount > 0)
                    {
                        textBoxCodigo.Text = dataGridViewProductos.Rows[0].Cells[0].Value.ToString();
                        textBoxNombre.Text = dataGridViewProductos.Rows[0].Cells[1].Value.ToString();
                        textBoxMarca.Text = dataGridViewProductos.Rows[0].Cells[2].Value.ToString();
                        textBoxCategoria.Text = dataGridViewProductos.Rows[0].Cells[3].Value.ToString();
                        textBoxCantidad.Text = dataGridViewProductos.Rows[0].Cells[4].Value.ToString();
                        textBoxPrecio.Text = dataGridViewProductos.Rows[0].Cells[5].Value.ToString();

                        dataGridViewProductos.Columns[0].HeaderText = "Código";
                        dataGridViewProductos.Columns[1].HeaderText = "Nombre";
                        dataGridViewProductos.Columns[2].HeaderText = "Marca";
                        dataGridViewProductos.Columns[3].HeaderText = "Categoría";
                        dataGridViewProductos.Columns[4].HeaderText = "Cantidad";
                        dataGridViewProductos.Columns[5].HeaderText = "Precio";

                        dataGridViewProductos.AutoResizeColumns();
                        dataGridViewProductos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                        buttonReactivar.Enabled = true;
                    }
                    else
                    {
                        textBoxCodigo.Text = "";
                        textBoxNombre.Text = "";
                        textBoxMarca.Text = "";
                        textBoxCategoria.Text = "";
                        textBoxCantidad.Text = "";
                        textBoxPrecio.Text = "";

                        buttonReactivar.Enabled = false;

                    }
                }
            }
            catch (SqlException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Productos_Inactivos_Load(object sender, EventArgs e)
        {
            loadAll();
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    string buscar = textBoxBuscar.Text;
                    if (buscar.IndexOf('\'') > 0)
                    {
                        buscar = verifyQuotes(buscar);
                    }

                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();

                    SqlDataAdapter select = new SqlDataAdapter("select id_producto Código, nombre Nombre, marca Marca, categoria Categoría, cantidad Cantidad, precio Precio from productos WHERE (nombre like '%" + buscar + "%' or id_producto like '%" + buscar + "%') and status = 0 ORDER BY id_producto", openCon);
                    select.SelectCommand.Transaction = transaction;
                    DataSet ds = new DataSet("Check");
                    select.FillSchema(ds, SchemaType.Source, "Productos");
                    select.Fill(ds, "Productos");
                    DataTable dtProductos = ds.Tables["Productos"];

                    dataGridViewProductos.DataSource = dtProductos;

                    if (dataGridViewProductos.RowCount > 0)
                    {
                        textBoxCodigo.Text = dataGridViewProductos.Rows[0].Cells[0].Value.ToString();
                        textBoxNombre.Text = dataGridViewProductos.Rows[0].Cells[1].Value.ToString();
                        textBoxMarca.Text = dataGridViewProductos.Rows[0].Cells[2].Value.ToString();
                        textBoxCategoria.Text = dataGridViewProductos.Rows[0].Cells[3].Value.ToString();
                        textBoxCantidad.Text = dataGridViewProductos.Rows[0].Cells[4].Value.ToString();
                        textBoxPrecio.Text = dataGridViewProductos.Rows[0].Cells[5].Value.ToString();

                        dataGridViewProductos.Columns[0].HeaderText = "Código";
                        dataGridViewProductos.Columns[1].HeaderText = "Nombre";
                        dataGridViewProductos.Columns[2].HeaderText = "Marca";
                        dataGridViewProductos.Columns[3].HeaderText = "Categoría";
                        dataGridViewProductos.Columns[4].HeaderText = "Cantidad";
                        dataGridViewProductos.Columns[5].HeaderText = "Precio";

                        dataGridViewProductos.AutoResizeColumns();
                        dataGridViewProductos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                        buttonReactivar.Enabled = true;

                    }
                    else
                    {
                        textBoxCodigo.Text = "";
                        textBoxNombre.Text = "";
                        textBoxMarca.Text = "";
                        textBoxCategoria.Text = "";
                        textBoxCantidad.Text = "";
                        textBoxPrecio.Text = "";

                        buttonReactivar.Enabled = false;

                    }
                }
            }
            catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dataGridViewProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewProductos.RowCount > 0)
            {
                textBoxCodigo.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                textBoxMarca.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                textBoxCategoria.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                textBoxCantidad.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                textBoxPrecio.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
            }
        }

        private void dataGridViewProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewProductos.RowCount > 0)
            {
                textBoxCodigo.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                textBoxMarca.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                textBoxCategoria.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                textBoxCantidad.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                textBoxPrecio.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
            }
        }

        private void dataGridViewProductos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewProductos.RowCount > 0)
                {
                    textBoxCodigo.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                    textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                    textBoxMarca.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                    textBoxCategoria.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                    textBoxCantidad.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                    textBoxPrecio.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
                }
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void buttonReactivar_Click(object sender, EventArgs e)
        {

            Contraseña con = new Contraseña();

            while (true)
            {

                con.ShowDialog(this);

                if (con.DialogResult == DialogResult.Cancel)
                {
                    con.Dispose();
                    break;

                }
                else if (con.DialogResult == DialogResult.Yes && con.textBoxContraseña.Text.Equals(Properties.Settings.Default.Pass))
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

                                Utilerias.EjecutaComando("UPDATE productos " +
                                                               "SET    status      = 1 " +
                                                               "WHERE  id_producto = @id_producto",
                                                               CommandType.Text,
                                                               openCon,
                                                               transaction,
                                                               "",
                                                               new object[] { "@id_producto", dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString() });

                                transaction.Commit();
                                reactivado = true;

                                MessageBox.Show("'" + textBoxNombre.Text + "' ha sido reactivado correctamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                loadAll();


                            }
                            catch (SqlException sqlEx)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Ha habido un error. Verifica lo siguiente:\n\n- Verifica la conexión a la base de datos y prueba de nuevo\n- Verifica que el código de barras del producto a desactivar es correcto.\n\nError: " + sqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    break;
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar) || e.KeyChar.ToString().Equals("'"))
            {
                if (e.KeyChar.ToString().Equals("'") && textBoxBuscar.TextLength == 0)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
