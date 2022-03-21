using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Requisición_Producto : Form
    {

        private DataTable dtProductos = new DataTable();

        private DataTable dtProductosReq = new DataTable();

        public Requisición_Producto()
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
            try
            {                
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();
                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT id_producto Código,  nombre Nombre,  marca Marca " +
                                                                              "FROM   productos " +
                                                                              "WHERE  status <> 0 " +
                                                                              "ORDER BY id_producto", openCon, transaction));
                    dtProductos.Clear();
                    select.Fill(dtProductos);

                    dataGridViewProductos.DataSource = dtProductos;

                    if (dataGridViewProductos.RowCount > 0)
                    {
                        dataGridViewProductos.AutoResizeColumns();
                        dataGridViewProductos.Columns[1].Width = 200;
                        dataGridViewProductos.Columns[dataGridViewProductos.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    recuperaRequisicion();

                    if (dataGridViewRequisicion.ColumnCount == 0)
                    {
                        dataGridViewRequisicion.Columns.Add("codigo", "Código");
                        dataGridViewRequisicion.Columns.Add("nombre", "Nombre");
                        dataGridViewRequisicion.Columns.Add("marca", "Marca");
                        dataGridViewRequisicion.Columns.Add("cantidad", "Cantidad");

                        dataGridViewRequisicion.Columns[0].Width = 100;
                        dataGridViewRequisicion.Columns[1].Width = 180;
                        dataGridViewRequisicion.Columns[2].Width = 100;
                        dataGridViewRequisicion.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                        dataGridViewRequisicion.Columns[0].ReadOnly = true;
                        dataGridViewRequisicion.Columns[1].ReadOnly = true;
                        dataGridViewRequisicion.Columns[2].ReadOnly = true;
                    }
                }
            }
            catch (SqlException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n - Verifica la conexión a la base de datos y prueba de nuevo " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void Requisición_Producto_Load(object sender, EventArgs e)
        {
            loadAll();
        }

        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dataGridViewRequisicion.RowCount; i++)
            {
                if (dataGridViewRequisicion.Rows[i].Cells[dataGridViewRequisicion.Columns.Count - 1].Value.ToString() == "")
                {
                    MessageBox.Show("Ingrese una cantidad para el producto '" + dataGridViewRequisicion.Rows[i].Cells[1].Value.ToString() + "'.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show("¿Desea enviar correo con la requisición de producto?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string ciber = "CiberStore";
                string header = "<tr><th bordercolor=#FFFFE0>Nombre</th><th bordercolor=#FFFFE0>Cantidad Existente (Pzas o kgs)</th>";
                string style = "<style type=\"text/css\"> table th {background-color: #879cff; text-align: center;} table td {background: #eeeeee; text-align: center;} </style>";
                string body = "";

                for (int i = 0; i < dataGridViewRequisicion.RowCount; i++)
                {
                    body += "<tr><td>" + dataGridViewRequisicion.Rows[i].Cells[1].Value.ToString() + "</td><td>" + dataGridViewRequisicion.Rows[i].Cells[dataGridViewRequisicion.Columns.Count - 1].Value.ToString() + "</td></tr>";
                }

                string bodyFull = "Productos a pedir para " + ciber + ": " + "<table border=1 cellpadding=0>" + header + body + "</table>" + style;

                int validacion = 0;

                Thread tCorreo = new Thread(delegate () { validacion = new Correos(bodyFull, null).correoRequisicion(); });
                tCorreo.Start();
                tCorreo.Join();

                if (validacion == 1)
                {
                    limpiar();
                }
            }           
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtProductos.Rows.Count > 0)
                {
                    dtProductos.DefaultView.RowFilter = $"Código LIKE '%{verifyQuotes(textBoxBuscar.Text)}%' OR Nombre Like '%{verifyQuotes(textBoxBuscar.Text)}%'";
                    dataGridViewProductos.AutoResizeColumns();
                    dataGridViewProductos.Columns[1].Width = 200;
                    dataGridViewProductos.Columns[dataGridViewProductos.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dataGridViewProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                bool enter = true;
                for (int i = 0; i < dataGridViewRequisicion.RowCount; i++)
                {
                    if (dataGridViewProductos.Rows[e.RowIndex].Cells[0].Value.ToString() == dataGridViewRequisicion.Rows[i].Cells[0].Value.ToString())
                    {
                        enter = false;
                    }
                }

                if (enter)
                {
                    dataGridViewRequisicion.Rows.Add(dataGridViewProductos.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                                     dataGridViewProductos.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                                     dataGridViewProductos.Rows[e.RowIndex].Cells[2].Value.ToString(),
                                                     "");

                    guardaRequisicion();
                }
            }
        }

        private void dataGridViewProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dataGridViewProductos.SelectedCells[0].RowIndex != -1)
            {
                bool enter = true;
                for (int i = 0; i < dataGridViewRequisicion.RowCount; i++)
                {
                    if (dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString() == dataGridViewRequisicion.Rows[i].Cells[0].Value.ToString())
                    {
                        enter = false;
                    }
                }

                if (enter)
                {
                    dataGridViewRequisicion.Rows.Add(dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString(),
                                                     dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString(),
                                                     dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString(),
                                                     "");

                    guardaRequisicion();
                }
            }
        }

        private void contextMenuStripTabla_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequisicion.RowCount > 0 && contextMenuStripTabla.Items[0].Selected)
            {
                dataGridViewRequisicion.Rows.Remove(dataGridViewRequisicion.CurrentRow);
                if (dataGridViewRequisicion.RowCount > 2)
                    dataGridViewRequisicion.CurrentCell = dataGridViewRequisicion.Rows[dataGridViewRequisicion.RowCount - 1].Cells[0];
            }
        }

        private void dataGridViewRequisicion_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex != -1)
                    {
                        dataGridViewRequisicion.CurrentCell = dataGridViewRequisicion.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        dataGridViewRequisicion.Rows[e.RowIndex].Selected = true;
                        dataGridViewRequisicion.Focus();

                        contextMenuStripTabla.Show(dataGridViewRequisicion, dataGridViewRequisicion.PointToClient(Cursor.Position));
                    }
                }
            }
            catch (ArgumentException) { }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Estás seguro/a de limpiar la tabla de requisición de productos?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {               
                limpiar();
            }
        }

        public void limpiar()
        {
            int rows = dataGridViewRequisicion.RowCount;

            for (int i = 0; i < rows; i++)
            {
                dataGridViewRequisicion.Rows.RemoveAt(0);
            }

            try
            {
                File.Delete(@"..\..\..\documentos\requisicion.txt");

                using (var file = File.Create(@"..\..\..\documentos\requisicion.txt")) { }
            }
            catch (IOException) { }

            textBoxBuscar.Text = "";

            loadAll();
        }

        /// <summary>
        /// Función que guarda la requisición de productos añadida, al insertar un nuevo row, para recuperarla una vez que se abra de nuevo el programa
        /// </summary>
        public void guardaRequisicion()
        {

            try
            {
                if (!Directory.Exists(@"..\..\..\documentos")) { Directory.CreateDirectory(@"..\..\..\documentos"); }
                if (!File.Exists(@"..\..\..\documentos\requisicion.txt")) { using (var file = File.Create(@"..\..\..\documentos\requisicion.txt")) { } }

                using (StreamWriter sw = new StreamWriter(@"..\..\..\documentos\requisicion.txt"))
                {

                    for (int i = 0; i < dataGridViewRequisicion.RowCount; i++)
                    {
                        sw.WriteLine(dataGridViewRequisicion.Rows[i].Cells[0].Value.ToString().Trim());
                    }

                    sw.Close();
                }
            }
            catch (IOException) { MessageBox.Show("Ha ocurrido un error. El archivo 'requisicion.txt' no puedo ser cargado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        /// <summary>
        /// Función que recupera la requisición de productos, cuando se cierra el programa
        /// </summary>
        public void recuperaRequisicion()
        {
            try
            {
                if (File.Exists(@"..\..\..\documentos\requisicion.txt") && new FileInfo(@"..\..\..\documentos\requisicion.txt").Length != 0)
                {
                    string productos = "";

                    using (StreamReader sr = new StreamReader(@"..\..\..\documentos\requisicion.txt"))
                    {

                        while (!sr.EndOfStream)
                        {
                            productos += "'" + sr.ReadLine() + "', ";
                        }

                        sr.Close();
                    }

                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT id_producto Código,  nombre Nombre,  marca Marca, null Cantidad " +
                                                                                  "FROM   productos " +
                                                                                  "WHERE  status <> 0 " +
                                                                                  "AND    id_producto IN (" + productos.Substring(0, productos.Length - 2) + ") " +
                                                                                  "ORDER BY id_producto", openCon, transaction));
                        dtProductosReq.Clear();
                        select.Fill(dtProductosReq);

                        if(dtProductosReq.Rows.Count > 0)
                        {
                            dataGridViewRequisicion.Columns.Add("codigo", "Código");
                            dataGridViewRequisicion.Columns.Add("nombre", "Nombre");
                            dataGridViewRequisicion.Columns.Add("marca", "Marca");
                            dataGridViewRequisicion.Columns.Add("cantidad", "Cantidad");

                            dataGridViewRequisicion.Columns[0].Width = 100;
                            dataGridViewRequisicion.Columns[1].Width = 180;
                            dataGridViewRequisicion.Columns[2].Width = 100;
                            dataGridViewRequisicion.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                            for (int i = 0; i < dtProductosReq.Rows.Count; i++)
                            {
                                dataGridViewRequisicion.Rows.Add(dtProductosReq.Rows[i][0].ToString(),
                                                                 dtProductosReq.Rows[i][1].ToString(),
                                                                 dtProductosReq.Rows[i][2].ToString(),
                                                                 "");
                            }

                            dataGridViewRequisicion.Columns[0].ReadOnly = true;
                            dataGridViewRequisicion.Columns[1].ReadOnly = true;
                            dataGridViewRequisicion.Columns[2].ReadOnly = true;
                        }
                    }

                }
            }
            catch (IOException){ MessageBox.Show("Ha ocurrido un error. El archivo 'requisicion.txt' no puedo ser cargado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
