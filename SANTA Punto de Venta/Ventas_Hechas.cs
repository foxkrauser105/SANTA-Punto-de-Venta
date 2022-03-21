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
    public partial class Ventas_Hechas : Form
    {
        public Ventas_Hechas()
        {
            InitializeComponent();
            dateTimePickerFecha.Value = DateTime.Now;
        }

        private void Ventas_Hechas_Load(object sender, EventArgs e)
        {
            loadAll();
        }

        public void loadAll()
        {
            using(SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {
                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT id_venta " +
                                                                              "FROM   venta " +
                                                                              "WHERE  fecha = '" + dateTimePickerFecha.Value.Date.ToString("yyyy-MM-dd") + "'", openCon, transaction));

                    DataTable dtVenta = new DataTable();
                    select.Fill(dtVenta);

                    comboBoxID.Items.Clear();

                    if(dtVenta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtVenta.Rows.Count; i++)
                        {
                            comboBoxID.Items.Add(dtVenta.Rows[i][0].ToString());
                        }

                        comboBoxID.SelectedIndex = 0;

                    }
                    else
                    {
                        int rows = dataGridViewVenta.RowCount;

                        for(int i = 0; i < rows; i++)
                        {
                            dataGridViewVenta.Rows.RemoveAt(0);
                        }

                        labelSuma.Text = "0.00";
                        productoUnitarioToolStripMenuItem.Enabled = false;
                        ordenCompletaToolStripMenuItem.Enabled = false;
                    }
                }
                catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n - Verifica la conexión a la base de datos y prueba de nuevo\n - Verifica que la tabla 'venta' no haya sido modificada y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dateTimePickerFecha_ValueChanged(object sender, EventArgs e)
        {
            loadAll();
        }

        private void comboBoxID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxID.Items.Count > 0)
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();

                    try
                    {
                        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT a.id_producto Código, IIF(b.numcliente IS NOT NULL, a.nombre + ' ' + CONVERT(VARCHAR,  b.numcliente) + ' - ' + CONVERT(VARCHAR, b.ncfolio), a.nombre) Nombre, " +
                                                                                  "       b.precio Precio, b.cantidad Cantidad,  CONVERT(DECIMAL(3,2), b.descuento) Importe " +
                                                                                  "FROM   productos a, " +
                                                                                  "       registro_ventas b, " +
                                                                                  "       venta c " +
                                                                                  "WHERE  a.id_producto = b.id_producto " +
                                                                                  "AND    b.id_venta    = c.id_venta " +
                                                                                  "AND    c.id_venta    = " + comboBoxID.Text, openCon, transaction));
                        DataTable dtVenta = new DataTable();
                        select.Fill(dtVenta);

                        dataGridViewVenta.DataSource = dtVenta;

                        if (dataGridViewVenta.RowCount > 0)
                        {
                            for (int i = 0; i < dataGridViewVenta.RowCount; i++)
                            {

                                if (dataGridViewVenta.Rows[i].Cells[4].Value.ToString().Equals("1.00"))
                                {
                                    dataGridViewVenta.Rows[i].Cells[2].Style.ForeColor = Color.Green;
                                    dataGridViewVenta.Rows[i].Cells[3].Style.ForeColor = Color.Green;
                                    dataGridViewVenta.Rows[i].Cells[4].Style.ForeColor = Color.Green;
                                }

                                dataGridViewVenta.Rows[i].Cells[4].Value = decimal.Round(decimal.Parse(dataGridViewVenta.Rows[i].Cells[2].Value.ToString()) * decimal.Parse(dataGridViewVenta.Rows[i].Cells[3].Value.ToString()), 2);

                                if (dataGridViewVenta.Rows[i].Cells[0].Value.ToString().Equals("NC01") || dataGridViewVenta.Rows[i].Cells[0].Value.ToString().Equals("NC02"))
                                {
                                    ordenCompletaToolStripMenuItem.Enabled = false;
                                }
                                else
                                {
                                    ordenCompletaToolStripMenuItem.Enabled = true;
                                }
                            }

                            dataGridViewVenta.AutoResizeColumns();
                            dataGridViewVenta.Columns[1].Width = 400;
                            dataGridViewVenta.Columns[dataGridViewVenta.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                            calculaPrecio();

                            if (dataGridViewVenta.RowCount > 1)
                                productoUnitarioToolStripMenuItem.Enabled = true;
                            else
                                productoUnitarioToolStripMenuItem.Enabled = false;
                        }
                        else
                        {
                            ordenCompletaToolStripMenuItem.Enabled    = false;
                            productoUnitarioToolStripMenuItem.Enabled = false;
                        }

                    }
                    catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n - Verifica la conexión a la base de datos y prueba de nuevo\n - Verifica que la tabla 'venta' no haya sido modificada y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void ordenCompletaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contraseña con = new Contraseña();
            if (con.DialogResult == DialogResult.No)
            {
                con.Dispose();
            }
            if (con.ShowDialog(this) == DialogResult.Yes && con.textBoxContraseña.Text.Equals("hancock7"))
            {
                if (MessageBox.Show("¿Está seguro/a de hacer devolución de la venta N° " + comboBoxID.Text + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();

                        try
                        {

                            for (int i = 0; i < dataGridViewVenta.RowCount; i++)
                            { 

                                using (SqlCommand queryUpdate = new SqlCommand("UPDATE productos " +
                                                                               "SET    cantidad    = cantidad + @cantidad " +
                                                                               "WHERE  id_producto = @id_producto", openCon, transaction))
                                {

                                    queryUpdate.Parameters.Add("@cantidad",    SqlDbType.Float).Value   = double.Parse(dataGridViewVenta.Rows[i].Cells[3].Value.ToString());
                                    queryUpdate.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = dataGridViewVenta.Rows[i].Cells[0].Value.ToString();

                                    queryUpdate.ExecuteNonQuery();

                                }
                            }

                            using (SqlCommand queryDelete = new SqlCommand("DELETE " +
                                                                           "FROM venta " +
                                                                           "WHERE id_venta = @id_venta", openCon, transaction))
                            {

                                queryDelete.Parameters.Add("@id_venta", SqlDbType.Int).Value = comboBoxID.Text;

                                queryDelete.ExecuteNonQuery();

                            }

                            transaction.Commit();
                            MessageBox.Show("La devolución se ha hecho de manera correcta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadAll();

                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n - Verifica la conexión a la base de datos y prueba de nuevo\n - Verifica que la tabla 'venta' no haya sido modificada y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }                       
                    }
                }                   
            }

            if(con.DialogResult == DialogResult.Yes && !con.textBoxContraseña.Text.Equals("hancock7"))
            {
                MessageBox.Show("Contraseña incorrecta. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void productoUnitarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contraseña con = new Contraseña();
            if (con.DialogResult == DialogResult.No)
            {
                con.Dispose();
            }
            if (con.ShowDialog(this) == DialogResult.Yes && con.textBoxContraseña.Text.Equals("hancock7"))
            {
                if (MessageBox.Show("¿Está seguro/a de devolver el producto '" + dataGridViewVenta.Rows[dataGridViewVenta.SelectedCells[0].RowIndex].Cells[1].Value.ToString() + "' de la venta N° " + comboBoxID.Text + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        try
                        {

                            using (SqlCommand queryUpdate = new SqlCommand("UPDATE productos " +
                                                                           "SET cantidad      = cantidad + @cantidad " +
                                                                           "WHERE id_producto = @id_producto", openCon, transaction))
                            {

                                queryUpdate.Parameters.Add("@cantidad",    SqlDbType.Float).Value   = double.Parse(dataGridViewVenta.Rows[dataGridViewVenta.SelectedCells[0].RowIndex].Cells[3].Value.ToString());
                                queryUpdate.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = dataGridViewVenta.Rows[dataGridViewVenta.SelectedCells[0].RowIndex].Cells[0].Value.ToString();

                                queryUpdate.ExecuteNonQuery();

                            }

                            using (SqlCommand queryUpdate = new SqlCommand("UPDATE venta " +
                                                                           "SET    venta    = venta - @venta " +
                                                                           "WHERE  id_venta = @id_venta", openCon, transaction))
                            {

                                queryUpdate.Parameters.Add("@venta",    SqlDbType.Decimal).Value = decimal.Parse(dataGridViewVenta.Rows[dataGridViewVenta.SelectedCells[0].RowIndex].Cells[4].Value.ToString());
                                queryUpdate.Parameters.Add("@id_venta", SqlDbType.VarChar).Value = comboBoxID.Text;

                                queryUpdate.ExecuteNonQuery();

                            }

                            SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT id_registro " +
                                                                                      "FROM   registro_ventas " +
                                                                                      "WHERE  id_venta    = " + comboBoxID.Text + " " +
                                                                                      "AND    id_producto = '" + dataGridViewVenta.Rows[dataGridViewVenta.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "'", openCon, transaction));
                            DataTable dtRegistro = new DataTable();
                            select.Fill(dtRegistro);

                            using (SqlCommand queryDelete = new SqlCommand("DELETE " +
                                                                           "FROM registro_ventas " +
                                                                           "WHERE id_registro = @id_registro", openCon, transaction))
                            {

                                queryDelete.Parameters.Add("@id_registro", SqlDbType.Int).Value = dtRegistro.Rows[0][0].ToString();

                                queryDelete.ExecuteNonQuery();

                            }

                            transaction.Commit();
                            MessageBox.Show("La devolución se ha hecho de manera correcta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            comboBoxID_SelectedIndexChanged(sender, e);

                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n - Verifica la conexión a la base de datos y prueba de nuevo\n - Verifica que la tabla 'venta' no haya sido modificada y prueba de nuevo ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }            
                    }
                }             
            }
            
            if(con.DialogResult == DialogResult.Yes && !con.textBoxContraseña.Text.Equals("hancock7"))
            {
                MessageBox.Show("Contraseña incorrecta. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void calculaPrecio()
        {
            decimal suma = 0.00M;
            for (int i = 0; i < dataGridViewVenta.RowCount; i++)
            {
                suma += decimal.Parse(dataGridViewVenta.Rows[i].Cells[4].Value.ToString());
            }

            //Redondeamos el precio final y mostramos label que lo indica
            //Necesitamos redondear de 0.00 hasta 0.24 a 0, de 0.25 a 0.74 a 0.50 y de 0.75 a .99 a 1.00
            //Si suma menos su mayor numero entero es mayor a 0, significa que tiene centavos y hay que verificar hacia donde se aplica redondeo
            //Sólo mostrar cuando el resultado sea diferente de un entero o 0.50, por que significa que si se aplica redondeo, los 50 centavos no aplican aqui
            //Nota: Se hizo suma de 0.00M a los números que son enteros, ya que ceiling y floor quitan los decimales...
            if (suma - Math.Floor(suma) != 0 && suma - Math.Floor(suma) != 0.50M)
            {
                labelRedondeo.Visible = true;

                if ((suma - Math.Floor(suma) > 0) && (suma - Math.Floor(suma) < 0.25M))
                {
                    suma = decimal.Round(Math.Floor(suma) + 0.00M, 2);
                }
                else if ((suma - Math.Floor(suma) >= 0.25M) && (suma - Math.Floor(suma) < 0.75M))
                {
                    suma = decimal.Round(Math.Floor(suma) + 0.50M, 2);
                }
                else
                {
                    suma = decimal.Round(Math.Ceiling(suma) + 0.00M, 2);
                }

            }
            else
            {
                labelRedondeo.Visible = false;
            }

            labelSuma.Text = (decimal.Round(suma, 2)).ToString(); 
        }
    }
}
