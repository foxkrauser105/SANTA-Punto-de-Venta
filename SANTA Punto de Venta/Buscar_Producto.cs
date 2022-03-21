using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SANTA_Punto_de_Venta
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

        private void Buscar_Producto_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();

                    SqlDataAdapter select = new SqlDataAdapter("SELECT id_producto Código, nombre Nombre, precio Precio " +
                                                               "FROM   productos " +
                                                               "ORDER BY id_producto", openCon);
                    select.SelectCommand.Transaction = transaction;
                    select.Fill(dtProductos);

                    dataGridViewProductos.DataSource = dtProductos;

                    //if (dataGridViewProductos.RowCount > 0)
                    //{
                    //    dataGridViewProductos.Columns[0].HeaderText = "Código";
                    //    dataGridViewProductos.Columns[1].HeaderText = "Nombre";
                    //    dataGridViewProductos.Columns[2].HeaderText = "Precio";

                        
                    //}

                    //if(dataGridViewProductos.RowCount == 0)
                    //{
                    //    dataGridViewProductos.Columns.Add("codigo", "Código");
                    //    dataGridViewProductos.Columns.Add("nombre", "Nombre");
                    //    dataGridViewProductos.Columns.Add("precio", "Precio");
                    //}

                    /*if(dataGridViewProductos.RowCount > 0)
                    {
                        dataGridViewProductos.Columns[0].HeaderText = "Código";
                        dataGridViewProductos.Columns[1].HeaderText = "Nombre";
                        dataGridViewProductos.Columns[2].HeaderText = "Precio";

                        
                        //dataGridViewProductos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    }*/

                    dataGridViewProductos.AutoResizeColumns();

                    dataGridViewProductosAgregados.Columns.Add("codigo", "Código");
                    dataGridViewProductosAgregados.Columns.Add("nombre", "Nombre");
                    dataGridViewProductosAgregados.Columns.Add("precio", "Precio");

                    dataGridViewProductosAgregados.AutoResizeColumns();
                    dataGridViewProductosAgregados.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    textBoxBuscar.Focus();
                    textBoxBuscar.Text = buscar;
                }
            }
            catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(dtProductos.Rows.Count > 0)
                {
                    dtProductos.DefaultView.RowFilter = $"Código LIKE '%{verifyQuotes(textBoxBuscar.Text)}%' OR Nombre Like '%{verifyQuotes(textBoxBuscar.Text)}%'";
                    dataGridViewProductos.AutoResizeColumns();
                    dataGridViewProductos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dataGridViewProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                bool enter = true;
                for(int i = 0; i < dataGridViewProductosAgregados.RowCount; i++)
                {
                    if(dataGridViewProductos.Rows[e.RowIndex].Cells[0].Value.ToString() == dataGridViewProductosAgregados.Rows[i].Cells[0].Value.ToString())
                    {
                        enter = false;
                    }
                }
                if (enter)
                {                 
                    dataGridViewProductosAgregados.Rows.Add(dataGridViewProductos.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                                            dataGridViewProductos.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                                            dataGridViewProductos.Rows[e.RowIndex].Cells[2].Value.ToString());
                    dataGridViewProductosAgregados.AutoResizeColumns();
                    dataGridViewProductosAgregados.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }  
            }
        }

        private void dataGridViewProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dataGridViewProductos.SelectedCells[0].RowIndex != -1)
            {
                bool enter = true;
                for (int i = 0; i < dataGridViewProductosAgregados.RowCount; i++)
                {
                    if (dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString() == dataGridViewProductosAgregados.Rows[i].Cells[0].Value.ToString())
                    {
                        enter = false;
                    }
                }
                if (enter)
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
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar) || e.KeyChar.ToString().Equals("'") || e.KeyChar.ToString().Equals("&") || e.KeyChar.ToString().Equals("%"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
