using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Buscar_Producto : Form
    {

        public Buscar_Producto()
        {
            InitializeComponent();
        }

        DataTable dtProductos = new DataTable();
        public string buscar = "";

        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Buscar_Producto_Load(object sender, EventArgs e)
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {

                    dtProductos = Utilerias.EjecutaComando("SELECT id_producto Código, nombre Nombre, precio Precio " +
                                                           "FROM   productos " +
                                                           "WHERE  status = 1 " +
                                                           "ORDER BY id_producto",
                                                           CommandType.Text,
                                                           openCon,
                                                           transaction);

                    dataGridViewProductos.DataSource = dtProductos;

                    dataGridViewProductos.AutoResizeColumns();

                    dataGridViewProductosAgregados.Columns.Add("codigo", "Código");
                    dataGridViewProductosAgregados.Columns.Add("nombre", "Nombre");
                    dataGridViewProductosAgregados.Columns.Add("precio", "Precio");

                    dataGridViewProductosAgregados.AutoResizeColumns();
                    dataGridViewProductosAgregados.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    textBoxBuscar.Focus();
                    textBoxBuscar.Text = buscar;
                }
                catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtProductos.Rows.Count > 0)
                {
                    dtProductos.DefaultView.RowFilter = $"Código LIKE '%{Utilerias.VerifyQuotes(textBoxBuscar.Text)}%' OR Nombre Like '%{Utilerias.VerifyQuotes(textBoxBuscar.Text)}%'";
                    dataGridViewProductos.AutoResizeColumns();
                    dataGridViewProductos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dataGridViewProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1 && !dataGridViewProductosAgregados.Rows.Cast<DataGridViewRow>().Any((dgvRow) => dgvRow.Cells[0].Value.ToString() == dataGridViewProductos.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                dataGridViewProductosAgregados.Rows.Add(dataGridViewProductos.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                                        dataGridViewProductos.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                                        dataGridViewProductos.Rows[e.RowIndex].Cells[2].Value.ToString());
                dataGridViewProductosAgregados.AutoResizeColumns();
                dataGridViewProductosAgregados.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void dataGridViewProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dataGridViewProductos.SelectedCells[0].RowIndex != -1)
            {
                if (!dataGridViewProductosAgregados.Rows.Cast<DataGridViewRow>().Any((dgvRow) => dgvRow.Cells[0].Value.ToString() == dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString()))
                {
                    dataGridViewProductosAgregados.Rows.Add(dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString(),
                                                            dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString(),
                                                            dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString());
                    dataGridViewProductosAgregados.AutoResizeColumns();
                    dataGridViewProductosAgregados.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        private void DataGridViewProductosAgregados_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex != -1)
                    {
                        dataGridViewProductosAgregados.CurrentCell = dataGridViewProductosAgregados.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        dataGridViewProductosAgregados.Rows[e.RowIndex].Selected = true;
                        dataGridViewProductosAgregados.Focus();

                        contextMenuStripTabla.Show(dataGridViewProductosAgregados, dataGridViewProductosAgregados.PointToClient(Cursor.Position));
                    }
                }
            }
            catch (ArgumentException) { }
        }

        private void ContextMenuStripTabla_Click(object sender, EventArgs e)
        {
            if (dataGridViewProductosAgregados.RowCount > 0 && contextMenuStripTabla.Items[0].Selected)
            {
                dataGridViewProductosAgregados.Rows.Remove(dataGridViewProductosAgregados.CurrentRow);
                if (dataGridViewProductosAgregados.RowCount > 2)
                    dataGridViewProductosAgregados.CurrentCell = dataGridViewProductosAgregados.Rows[dataGridViewProductosAgregados.RowCount - 1].Cells[0];
            }
        }

        private void textBoxBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Utilerias.CaracterValido(e.KeyChar);
        }

        private void Buscar_Producto_Shown(object sender, EventArgs e)
        {

            if (dataGridViewProductos.Rows.Count == 1)
            {
                dataGridViewProductosAgregados.Rows.Add(dataGridViewProductos.Rows[0].Cells[0].Value.ToString(),
                                                        dataGridViewProductos.Rows[0].Cells[1].Value.ToString(),
                                                        dataGridViewProductos.Rows[0].Cells[2].Value.ToString());

                buttonAceptar.PerformClick();
            }
        }
    }
}
