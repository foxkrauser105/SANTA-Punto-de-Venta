using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Registro_Nota_Credito : Form
    {
        int accion;

        DataTable dtDetalle = new DataTable();

        public Registro_Nota_Credito(string nc, string nom, string nf, int accion)
        {
            InitializeComponent();
            textBoxNumCliente.Text = nc;
            textBoxNombre.Text = nom;
            textBoxNCFolio.Text = nf;
            this.accion = accion;

        }

        private void Registro_Nota_Credito_Load(object sender, EventArgs e)
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                string sql = "";

                if (accion == 0)
                {
                    sql = "SELECT r.id_producto Código, p.nombre Nombre, r.cantidad Cantidad, r.precio Precio, " +
                          "       r.importe Importe,    r.fechaSurtido \"Fecha de Surtido\" " +
                          "FROM   registro_notas_credito r, productos p " +
                          "WHERE  r.id_producto = p.id_producto " +
                          "AND    r.numcliente  = " + textBoxNumCliente.Text + " " +
                          "AND    r.ncfolio     = " + textBoxNCFolio.Text + " " +
                          "ORDER BY r.FechaSurtido DESC, r.id_producto";
                }
                else
                {
                    sql = "SELECT p.pago \"Pago No.\", p.importe \"Importe Pagado\", p.fecha \"Fecha de Pago\" " +
                          "FROM   pagos_notas_credito p " +
                          "WHERE  p.numcliente  = " + textBoxNumCliente.Text + " " +
                          "AND    p.ncfolio     = " + textBoxNCFolio.Text + " " +
                          "ORDER BY p.pago DESC";

                    labelTitulo.Text = "Pagos de Notas de Crédito";
                }

                SqlDataAdapter select = new SqlDataAdapter(new SqlCommand(sql, openCon, transaction));
                select.Fill(dtDetalle);

                dataGridViewProductos.DataSource = dtDetalle;

                dataGridViewProductos.AutoResizeColumns();
                dataGridViewProductos.Columns[dataGridViewProductos.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                if (dataGridViewProductos.RowCount > 0 && accion == 0)
                {
                    int conteo = 0;

                    for (int i = 0; i < dataGridViewProductos.ColumnCount; i++)
                    {
                        dataGridViewProductos.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    for (int i = 0; i < dataGridViewProductos.RowCount; i++)
                    {
                        try
                        {
                            SqlDataAdapter selecto = new SqlDataAdapter(new SqlCommand("SELECT IIF( " + dataGridViewProductos.Rows[i].Cells[2].Value.ToString() + " >= d.cantidadMinima AND d.status = 1, 'Si', 'No' ) descuento " +
                                                                                       "FROM  productos p " +
                                                                                       "LEFT OUTER JOIN descuentos d " +
                                                                                       "ON    p.id_producto = d.id_producto " +
                                                                                       "WHERE p.id_producto = '" + dataGridViewProductos.Rows[i].Cells[0].Value.ToString() + "'", openCon, transaction));

                            DataTable dtDescuento = new DataTable();
                            selecto.Fill(dtDescuento);

                            if (dtDescuento.Rows[0][0].ToString().Equals("Si"))
                            {
                                dataGridViewProductos.Rows[i].Cells[2].Style.ForeColor = Color.Green;
                                dataGridViewProductos.Rows[i].Cells[3].Style.ForeColor = Color.Green;
                                dataGridViewProductos.Rows[i].Cells[4].Style.ForeColor = Color.Green;
                            }
                        }
                        catch (SqlException)
                        {
                            conteo++;

                        }

                    }

                    if (conteo > 0)
                        MessageBox.Show("Ha ocurrido un problema, los descuentos no se pudieron mostrar pero se encuentran presentes", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                dateTimePickerPorFecha.Value = DateTime.Now.Date;
                dateTimePickerPorRango.Value = DateTime.Now.Date;

                calculaPrecio();
            }
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (checkedListBox.SelectedIndex == 0 && checkedListBox.GetItemChecked(checkedListBox.SelectedIndex))
            {
                checkedListBox.SetItemChecked(1, false);

                dateTimePickerPorFecha.Enabled = true;
                dateTimePickerPorRango.Enabled = false;

                if (accion == 0)
                {
                    dtDetalle.DefaultView.RowFilter = $"[Fecha de Surtido] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Surtido] <= '{dateTimePickerPorFecha.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                }
                else
                {
                    dtDetalle.DefaultView.RowFilter = $"[Fecha de Pago] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Pago] <= '{dateTimePickerPorFecha.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                }
            }
            else if (checkedListBox.SelectedIndex == 1 && checkedListBox.GetItemChecked(checkedListBox.SelectedIndex))
            {
                checkedListBox.SetItemChecked(0, false);

                dateTimePickerPorFecha.Enabled = true;
                dateTimePickerPorRango.Enabled = true;

                if (accion == 0)
                {
                    dtDetalle.DefaultView.RowFilter = $"[Fecha de Surtido] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Surtido] <= '{dateTimePickerPorRango.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                }
                else
                {
                    dtDetalle.DefaultView.RowFilter = $"[Fecha de Pago] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Pago] <= '{dateTimePickerPorRango.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                }
            }
            else if (checkedListBox.CheckedItems.Count == 0)
            {

                dateTimePickerPorFecha.Enabled = false;
                dateTimePickerPorRango.Enabled = false;

                dtDetalle.DefaultView.RowFilter = "";
            }

            calculaPrecio();
        }

        private void dateTimePickerPorFecha_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerPorFecha.Enabled)
            {
                if (checkedListBox.GetItemChecked(0))
                {
                    if (accion == 0)
                    {
                        dtDetalle.DefaultView.RowFilter = $"[Fecha de Surtido] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Surtido] <= '{dateTimePickerPorFecha.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                    }
                    else
                    {
                        dtDetalle.DefaultView.RowFilter = $"[Fecha de Pago] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Pago] <= '{dateTimePickerPorFecha.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                    }
                }
                else if (checkedListBox.GetItemChecked(1))
                {
                    // Si la fecha desde es mayor a hasta, las igualamos
                    if (dateTimePickerPorFecha.Value > dateTimePickerPorRango.Value)
                    {
                        dateTimePickerPorFecha.Value = dateTimePickerPorRango.Value;
                    }

                    if (accion == 0)
                    {
                        dtDetalle.DefaultView.RowFilter = $"[Fecha de Surtido] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Surtido] <= '{dateTimePickerPorRango.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                    }
                    else
                    {
                        dtDetalle.DefaultView.RowFilter = $"[Fecha de Pago] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Pago] <= '{dateTimePickerPorRango.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                    }
                }

                calculaPrecio();
            }
        }

        private void dateTimePickerPorRango_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerPorRango.Enabled)
            {
                if (dateTimePickerPorRango.Value < dateTimePickerPorFecha.Value)
                {
                    dateTimePickerPorRango.Value = dateTimePickerPorFecha.Value;
                }

                if (accion == 0)
                {
                    dtDetalle.DefaultView.RowFilter = $"[Fecha de Surtido] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Surtido] <= '{dateTimePickerPorRango.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                }
                else
                {
                    dtDetalle.DefaultView.RowFilter = $"[Fecha de Pago] >= '{dateTimePickerPorFecha.Value}' AND [Fecha de Pago] <= '{dateTimePickerPorRango.Value.AddHours(23).AddMinutes(59).AddSeconds(59)}'";
                }

                calculaPrecio();
            }
        }

        public void calculaPrecio()
        {
            decimal suma = 0.00M;
            for (int i = 0; i < dataGridViewProductos.RowCount; i++)
            {
                suma += decimal.Parse(dataGridViewProductos.Rows[i].Cells[accion == 0 ? 4 : 1].Value.ToString());
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
