using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Productos_Cero : Form
    {
        private int rowIndex = 0;
        private DataTable dtProductos = new DataTable();

        public Productos_Cero()
        {
            InitializeComponent();
        }

        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Productos_Cero_Load(object sender, EventArgs e)
        {
            loadAll();
            timerProductos.Start();
        }

        private void dataGridViewProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewProductos.RowCount > 0)
            {
                Añadir_Cantidad a = new Añadir_Cantidad();
                a.textBoxCodigo.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                a.textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                a.ShowDialog();
                if (a.añadido)
                {
                    loadAll();
                }
            }
        }

        /// <summary>
        /// Método que obtiene los productos en 0, cada 10 segundos y al iniciar la forma
        /// </summary>
        public void loadAll()
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {
                    dataGridViewProductos.Columns.Clear();

                    dtProductos = new Utilerias().ejecutaComando("SELECT id_producto Código, marca Marca, nombre Nombre " +
                                                                 "FROM   productos " +
                                                                 "WHERE  cantidad = 0 " +
                                                                 "ORDER BY id_producto",
                                                                 CommandType.Text,
                                                                 openCon,
                                                                 transaction);

                    dataGridViewProductos.DataSource = dtProductos;

                    if (dataGridViewProductos.RowCount > 0)
                    {
                        if (dataGridViewProductos.RowCount > 0)
                        {
                            if (rowIndex >= dataGridViewProductos.RowCount)
                            {
                                rowIndex = dataGridViewProductos.RowCount - 1;
                            }

                            dataGridViewProductos.CurrentCell = dataGridViewProductos.Rows[rowIndex].Cells[0];
                            dataGridViewProductos.Rows[rowIndex].Selected = true;

                        }
                        else
                        {
                            rowIndex = 0;
                        }
                    }

                    if (dataGridViewProductos.Columns.Count == 0)
                    {
                        dataGridViewProductos.Columns.Add("codigo", "Código");
                        dataGridViewProductos.Columns.Add("marca", "Marca");
                        dataGridViewProductos.Columns.Add("nombre", "Nombre");
                    }

                    dataGridViewProductos.AutoResizeColumns();
                    dataGridViewProductos.Columns[dataGridViewProductos.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }
                catch (SqlException)
                {
                    timerProductos.Stop();
                    MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente:\n\n- Verifica si la base de datos está en linea y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    timerProductos.Start();
                }
            }
                
        }

        private void dataGridViewProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewProductos.RowCount > 0)
                {             
                    rowIndex = dataGridViewProductos.SelectedCells[0].RowIndex;
                }
            }
            catch (ArgumentOutOfRangeException) { }
            catch (NullReferenceException) { }
            
        }

        private void timerProductos_Tick(object sender, EventArgs e)
        {
            loadAll();
        }
    }
}
