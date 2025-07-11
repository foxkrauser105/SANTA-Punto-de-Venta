﻿using JR.Utils.GUI.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Ventas : Form
    {

        #region Variables
        private DataTable dt = new DataTable();
        public bool añadidoVenta = false;
        #endregion

        public Ventas()
        {
            InitializeComponent();
        }

        private void buttonAñadir_Click(object sender, EventArgs e)
        {
            añadirProducto();
        }

        public void loadAll()
        {
            dt.Columns.Add("Código", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(float));
            dt.Columns.Add("Importe", typeof(decimal));

            dataGridViewVenta.DataSource = dt;

            buttonCompra.Enabled = false;
            buttonAñadir.Enabled = false;
            labelResta.Visible = false;
            labelRestante.Visible = false;
            buttonLinea.Visible = false;
            labelPagado.Visible = false;
            labelPago.Visible = false;
            textBoxCodigo.Focus();

            buttonActualizarNota.Enabled = false;
            buttonAgregarNota.Enabled = false;

            dataGridViewVenta.AutoResizeColumns();
            dataGridViewVenta.Columns[1].Width = 400;
            dataGridViewVenta.Columns[dataGridViewVenta.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewVenta.Columns[0].ReadOnly = true;
            dataGridViewVenta.Columns[1].ReadOnly = true;
            dataGridViewVenta.Columns[2].ReadOnly = true;
            dataGridViewVenta.Columns[4].ReadOnly = true;

            textBoxCodigo.Focus();
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            loadAll();
        }

        private void dataGridViewVenta_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridViewVenta.AutoResizeColumns();
            dataGridViewVenta.Columns[1].Width = 400;
            dataGridViewVenta.Columns[dataGridViewVenta.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewVenta.Columns[0].ReadOnly = true;
            dataGridViewVenta.Columns[1].ReadOnly = true;
            dataGridViewVenta.Columns[2].ReadOnly = true;
            dataGridViewVenta.Columns[4].ReadOnly = true;

            if (dataGridViewVenta.RowCount == 0)
            {

                buttonActualizarNota.Enabled = false;
                buttonAgregarNota.Enabled = false;
            }
            else
            {

                buttonActualizarNota.Enabled = true;
                buttonAgregarNota.Enabled = true;

            }

        }

        private void dataGridViewVenta_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridViewVenta.AutoResizeColumns();
            dataGridViewVenta.Columns[1].Width = 400;
            dataGridViewVenta.Columns[dataGridViewVenta.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (dataGridViewVenta.RowCount == 0)
            {

                limpiar();

            }
            else
            {
                buttonActualizarNota.Enabled = true;
                buttonAgregarNota.Enabled = true;
            }

        }

        public void calculaPrecio()
        {
            decimal suma = 0.00M;
            /*for (int i = 0; i < dataGridViewVenta.RowCount; i++)
            {
                suma += decimal.Parse(dataGridViewVenta.Rows[i].Cells[4].Value.ToString());
            }*/

            suma = dataGridViewVenta.Rows.Cast<DataGridViewRow>().SelectMany((dgvRow) => dgvRow.Cells.Cast<DataGridViewCell>()).Where((dgvCell) => dgvCell.ColumnIndex == 4).Sum((dgvCell) => decimal.Parse(dgvCell.Value.ToString()));

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

            if (labelRestante.Visible)
            {
                decimal restante = Math.Abs(decimal.Round(decimal.Parse(labelSuma.Text), 2) - decimal.Round(decimal.Parse(labelPagado.Text), 2));

                /*if(decimal.Round(decimal.Parse(labelSuma.Text), 2) <= decimal.Round(decimal.Parse(labelPagado.Text), 2))
                {
                    labelResta.Text = "Sobra: $";
                }
                else
                {
                    labelResta.Text = "Resta: $";
                }*/

                labelResta.Text = (decimal.Round(decimal.Parse(labelSuma.Text), 2) <= decimal.Round(decimal.Parse(labelPagado.Text), 2) ? "Sobra: $" : "Resta: $");

                buttonCompra.Enabled = true;
                labelRestante.Text = (decimal.Round(restante, 2)).ToString();
            }

            //Activación del botón compra
            /*if ((textBoxPago.TextLength == 0 &&
                    decimal.Round(decimal.Parse(labelPagado.Text), 2) >= decimal.Round(decimal.Parse(labelSuma.Text), 2) &&
                    decimal.Round(decimal.Parse(labelPagado.Text), 2) > 0.00M)
                || (textBoxPago.TextLength > 0 && dataGridViewVenta.RowCount > 0))
            {
                buttonCompra.Enabled = true;
            }
            else
            {
                buttonCompra.Enabled = false;
            }*/

            buttonCompra.Enabled = ((textBoxPago.TextLength == 0 &&
                                        decimal.Round(decimal.Parse(labelPagado.Text), 2) >= decimal.Round(decimal.Parse(labelSuma.Text), 2) &&
                                        decimal.Round(decimal.Parse(labelPagado.Text), 2) > 0.00M)
                                    || (textBoxPago.TextLength > 0 && dataGridViewVenta.RowCount > 0));
        }

        private void dataGridViewVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (!double.TryParse(dataGridViewVenta.Rows[e.RowIndex].Cells[3].Value.ToString(), out _))
                {
                    dataGridViewVenta.Rows[e.RowIndex].Cells[3].Value = 1;
                }
                try
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();

                        DataTable dtVenta = Utilerias.EjecutaComando("SELECT p.id_producto, p.nombre, IIF( @iCantMinima >= d.cantidadMinima AND d.status = 1, d.precioDescuento, p.precio ) precio, " +
                                                                           "IIF( @iCantMinima >= d.cantidadMinima AND d.status = 1, 'Si', 'No' ) descuento " +
                                                                           "FROM  productos p " +
                                                                           "LEFT JOIN descuentos d " +
                                                                           "ON    p.id_producto = d.id_producto " +
                                                                           "WHERE p.id_producto = @iProducto",
                                                                           CommandType.Text,
                                                                           openCon,
                                                                           transaction,
                                                                           "",
                                                                           new object[] { "@iCantMinima", double.Parse(dataGridViewVenta.Rows[e.RowIndex].Cells[3].Value.ToString()) },
                                                                           new object[] { "@iProducto", dataGridViewVenta.Rows[e.RowIndex].Cells[0].Value.ToString() });

                        if (dtVenta.Rows.Count > 0)
                        {

                            dataGridViewVenta.Rows[e.RowIndex].Cells[2].Style.ForeColor = dtVenta.Rows[0][3].ToString().Equals("Si") ? Color.Green : Color.White;
                            dataGridViewVenta.Rows[e.RowIndex].Cells[3].Style.ForeColor = dtVenta.Rows[0][3].ToString().Equals("Si") ? Color.Green : Color.White;
                            dataGridViewVenta.Rows[e.RowIndex].Cells[4].Style.ForeColor = dtVenta.Rows[0][3].ToString().Equals("Si") ? Color.Green : Color.White;

                            dataGridViewVenta.Rows[e.RowIndex].Cells[2].Value = decimal.Round(decimal.Parse(dtVenta.Rows[0][2].ToString()), 2);
                            dataGridViewVenta.Rows[e.RowIndex].Cells[4].Value = decimal.Round(decimal.Parse(dtVenta.Rows[0][2].ToString()) * decimal.Parse(dataGridViewVenta.Rows[e.RowIndex].Cells[3].Value.ToString()), 2);

                            calculaPrecio();
                            textBoxCodigo.Focus();
                        }
                        else
                        {
                            dataGridViewVenta.Rows[dataGridViewVenta.SelectedCells[0].RowIndex].Cells[0].Value = "1";
                            calculaPrecio();
                            MessageBox.Show("Hubo un error al cambiar la cantidad del producto. Verifique que exista", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (SqlException) { MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente: \n\n - Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (FormatException) { MessageBox.Show("Verifica los datos insertados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void contextMenuStripTabla_Click(object sender, EventArgs e)
        {
            if (dataGridViewVenta.RowCount > 0 && contextMenuStripTabla.Items[0].Selected)
            {
                dataGridViewVenta.Rows.Remove(dataGridViewVenta.CurrentRow);

                calculaPrecio();
                if (dataGridViewVenta.RowCount > 2)
                    dataGridViewVenta.CurrentCell = dataGridViewVenta.Rows[dataGridViewVenta.RowCount - 1].Cells[0];
            }
        }

        private void dataGridViewVenta_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex != -1)
                    {
                        dataGridViewVenta.CurrentCell = dataGridViewVenta.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        dataGridViewVenta.Rows[e.RowIndex].Selected = true;
                        dataGridViewVenta.Focus();

                        contextMenuStripTabla.Show(dataGridViewVenta, dataGridViewVenta.PointToClient(Cursor.Position));
                    }
                }
            }
            catch (ArgumentException) { }
        }

        private void buttonCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Round(decimal.Parse(labelSuma.Text), 2) - decimal.Round(decimal.Parse(labelPagado.Text), 2) - (!textBoxPago.Text.Equals("") ? decimal.Round(decimal.Parse(textBoxPago.Text), 2) : 0.00M) > 0.00M)
                {
                    if (labelRestante.Visible)
                    {
                        labelPagado.Text = (decimal.Round(decimal.Parse(labelPagado.Text), 2) + decimal.Round(decimal.Parse(textBoxPago.Text), 2)).ToString();
                        labelRestante.Text = (decimal.Round(decimal.Parse(labelSuma.Text), 2) - decimal.Round(decimal.Parse(labelPagado.Text), 2)).ToString();
                    }
                    else
                    {
                        labelPagado.Text = (decimal.Round(decimal.Parse(textBoxPago.Text) + 0.00M, 2)).ToString();
                        labelPago.Visible = true;
                        labelPagado.Visible = true;
                        buttonLinea.Visible = true;
                        labelResta.Visible = true;
                        labelRestante.Visible = true;
                        labelRestante.Text = (decimal.Round(decimal.Parse(labelSuma.Text), 2) - decimal.Round(decimal.Parse(labelPagado.Text), 2)).ToString();
                    }

                    textBoxPago.Text = "";
                }
                else if (MessageBox.Show("¿Está seguro de realizar la compra?\n\n- Total a pagar: $" + labelSuma.Text + " pesos\n- Pagó: $" + ((!textBoxPago.Text.Equals("") ? decimal.Round(decimal.Parse(textBoxPago.Text), 2) : 0.00M) + decimal.Round(decimal.Parse(labelPagado.Text), 2)) + " pesos\n- Cambio: $" + ((!textBoxPago.Text.Equals("") ? decimal.Round(decimal.Parse(textBoxPago.Text), 2) : 0.00M) + decimal.Round(decimal.Parse(labelPagado.Text), 2) - decimal.Round(decimal.Parse(labelSuma.Text), 2)) + " pesos", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    procesoVenta();

                }
            }
            catch (FormatException) { MessageBox.Show("Verifica los datos insertados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void limpiar()
        {

            dt.Rows.Clear();

            labelSuma.Text = "0.00";
            buttonCompra.Enabled = false;
            textBoxPago.Text = "";
            textBoxCodigo.Text = "";
            textBoxCantidad.Text = "";

            labelRestante.Text = "Resta: $";

            labelPagado.Text = "0.00";
            labelRestante.Text = "0.00";
            labelPago.Visible = false;
            labelPagado.Visible = false;
            buttonLinea.Visible = false;
            labelResta.Visible = false;
            labelRestante.Visible = false;

            labelRedondeo.Visible = false;

            buttonActualizarNota.Enabled = false;
            buttonAgregarNota.Enabled = false;

            textBoxCodigo.Focus();
        }

        private void textBoxCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            e.Handled = !(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString().Equals("&") || char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar));

            /*if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }*/

            if (e.KeyChar == (char)Keys.Enter && textBoxCodigo.TextLength > 0)
            {
                buttonAñadir_Click(sender, e);
            }
        }

        private void textBoxPago_TextChanged(object sender, EventArgs e)
        {
            //Activación del botón compra

            buttonCompra.Enabled = (textBoxPago.TextLength == 0 &&
                                       decimal.Round(decimal.Parse(labelPagado.Text), 2) >= decimal.Round(decimal.Parse(labelSuma.Text), 2) &&
                                       decimal.Round(decimal.Parse(labelPagado.Text), 2) > 0.00M)
                                   || (textBoxPago.TextLength > 0 && dataGridViewVenta.RowCount > 0);

            /*if (   (textBoxPago.TextLength == 0 && 
                   decimal.Round(decimal.Parse(labelPagado.Text), 2) >= decimal.Round(decimal.Parse(labelSuma.Text), 2) && 
                   decimal.Round(decimal.Parse(labelPagado.Text), 2) > 0.00M)
               || (textBoxPago.TextLength > 0 && dataGridViewVenta.RowCount > 0))
            {
                buttonCompra.Enabled = true;
            }
            else
            {
                buttonCompra.Enabled = false;
            }*/
        }

        private void textBoxPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            e.Handled = !(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar.ToString().Equals(cc.NumberFormat.NumberDecimalSeparator) && textBoxPago.Text.Contains(".")));

            /*if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxPago.Text.Contains("."))
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
            }*/

            if (e.KeyChar == (char)Keys.Enter && dataGridViewVenta.RowCount > 0 && (textBoxPago.TextLength > 0 || decimal.Round(decimal.Parse(labelPagado.Text), 2) >= decimal.Round(decimal.Parse(labelSuma.Text), 2))) buttonCompra_Click(sender, e);
            /*{

                if(textBoxPago.TextLength > 0 || decimal.Round(decimal.Parse(labelPagado.Text), 2) >= decimal.Round(decimal.Parse(labelSuma.Text), 2))
                {
                    buttonCompra_Click(sender, e);
                }
            }*/
        }

        public void imprimeTicket(string idVenta)
        {
            Ticket ticket = new Ticket();

            string colonia = "", calle = "", telefono = "";
            ManagementObjectSearcher buscaProcesador = new ManagementObjectSearcher("SELECT Description FROM Win32_DisplayConfiguration");
            foreach (ManagementObject mo in buscaProcesador.Get())
            {
                foreach (PropertyData propiedades in mo.Properties)
                {
                    if (propiedades.Value.ToString().Equals("NVIDIA GeForce GTX 1050"))
                    {
                        calle = "Calle De la Educación 9901";
                        colonia = "Colonia Insurgentes";
                        telefono = "Tel. (614)-388-7015";
                    }
                    if (propiedades.Value.ToString().Equals("Intel(R) Q45/Q43 Express Chipset"))
                    {
                        calle = "Calle Eusebio Baez 600";
                        colonia = "Colonia Deportistas";
                        telefono = "Tel. (614)-388-6730";
                    }
                }
            }

            ticket.textoCentro("CiberStore");
            ticket.textoCentro(calle);
            ticket.textoCentro(colonia);
            ticket.textoCentro(telefono);
            ticket.textoCentro("Correo: ciberstore1@hotmail.com");
            ticket.textoCentro("Ticket # " + idVenta);
            ticket.textoIzquierda("");
            ticket.lineasGuia();
            ticket.lineasAsterisco();
            ticket.textoExtremos("Fecha: " + DateTime.Now.ToShortDateString(), "Hora: " + DateTime.Now.ToShortTimeString());
            ticket.lineasAsterisco();
            ticket.lineasAsterisco();
            ticket.Encabezado();
            ticket.lineasAsterisco();

            for (int i = 0; i < dataGridViewVenta.RowCount; i++)
            {
                ticket.agregarArticulos(dataGridViewVenta.Rows[i].Cells[1].Value.ToString(), decimal.Parse(dataGridViewVenta.Rows[i].Cells[2].Value.ToString()), int.Parse(dataGridViewVenta.Rows[i].Cells[3].Value.ToString()), decimal.Parse(dataGridViewVenta.Rows[i].Cells[4].Value.ToString()));
            }

            ticket.lineasIgual();
            ticket.agregarTotales("         Total.......$", decimal.Parse(labelSuma.Text));
            ticket.lineasGuia();
            ticket.textoIzquierda("");
            ticket.textoCentro("¡Gracias por su preferencia!");
            ticket.textoIzquierda("");
            ticket.lineasIgual();
            ticket.cortarTicket();
            ticket.imprimirTicket("Microsoft XPS Document Writer");
        }

        private void textBoxCodigo_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCodigo.TextLength == 0)
            {
                buttonAñadir.Enabled = false;
            }
            else
            {
                buttonAñadir.Enabled = true;
            }
        }

        private void textBoxCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            e.Handled = !(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || (textBoxCantidad.TextLength > 0 && e.KeyChar.ToString().Equals(cc.NumberFormat.NumberDecimalSeparator) && !textBoxCantidad.Text.Contains(".")));

            /*if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator) || e.KeyChar.ToString().Equals("&")
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxCantidad.Text.Contains("."))
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
            }*/
        }

        private void textBoxCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                Buscar_Producto a = new Buscar_Producto();
                a.buscar = textBoxCodigo.Text;
                a.ShowDialog();
                if (a.buttonAceptar.DialogResult == DialogResult.OK)
                {
                    for (int i = 0; i < a.dataGridViewProductosAgregados.RowCount; i++)
                    {
                        textBoxCodigo.Text = a.dataGridViewProductosAgregados.Rows[i].Cells[0].Value.ToString();
                        añadirProducto();
                    }
                }
            }
        }

        public void añadirProducto()
        {

            double cantSumar = 0;
            /*if (!textBoxCantidad.Text.Equals("") && double.Parse(textBoxCantidad.Text) > 0)
            {
                cantSumar = double.Parse(textBoxCantidad.Text);
            }*/

            //Como la consulta ya traeria por su cuenta el precio que se deba aplicar, con o sin descuento, debemos primero
            //verificar si en el datagrid ya esta el articulo a ingresar, sumamos la cantidad que exista, para poder hacer
            //la consulta de manera correcta, y aplicar el descuento

            /*for(int i = 0; i < dataGridViewVenta.RowCount; i++)
            {
                if (dataGridViewVenta.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(textBoxCodigo.Text.ToUpper()))
                {
                    cantSumar += double.Parse(dataGridViewVenta.Rows[i].Cells[3].Value.ToString());
                    break;
                }
            }*/

            DataGridViewRow dRow = dataGridViewVenta.Rows.Cast<DataGridViewRow>().Where((DataGridViewRow dgvRow) => dgvRow.Cells[0].Value.Equals(textBoxCodigo.Text))?.FirstOrDefault();

            cantSumar += double.Parse(!textBoxCantidad.Text.Equals("") ? textBoxCantidad.Text : "1") + double.Parse(dRow != null ? dRow.Cells[3].Value.ToString() : "0");

            try
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();

                    DataTable dtVenta = Utilerias.EjecutaComando("SELECT p.id_producto, p.nombre, IIF( @iCantMinima >= d.cantidadMinima AND d.status = 1, d.precioDescuento, p.precio ) precio, " +
                                                                       "IIF( @iCantMinima >= d.cantidadMinima AND d.status = 1, 'Si', 'No' ) descuento, " +
                                                                       "p.status " +
                                                                       "FROM  productos p " +
                                                                       "LEFT JOIN descuentos d " +
                                                                       "ON    p.id_producto = d.id_producto " +
                                                                       "WHERE p.id_producto = @iProducto",
                                                                       CommandType.Text,
                                                                       openCon,
                                                                       transaction,
                                                                       "",
                                                                       new object[] { "@iCantMinima", double.Parse(cantSumar.ToString()) },
                                                                       new object[] { "@iProducto", textBoxCodigo.Text });

                    if (dtVenta.Rows.Count > 0)
                    {
                        //bool enter = true;

                        //Si el producto esta desactivado, se informa al usuario
                        if (int.Parse(dtVenta.Rows[0][4].ToString()) == 0)
                        {
                            MessageBox.Show("El producto '" + dtVenta.Rows[0][1].ToString() + "' está en estatus inactivo. Verifica.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        /*for (int i = 0; i < dataGridViewVenta.RowCount; i++)
                        {
                            
                        }*/

                        //Si detectamos que el producto ya existe en el datagridview, solo actualizamos la cantidad, el importe, y si hay productos con descuento 
                        if (dRow != null)
                        {
                            dataGridViewVenta.Rows[dRow.Index].Cells[2].Value = decimal.Round(decimal.Parse(dtVenta.Rows[0][2].ToString()), 2);
                            dataGridViewVenta.Rows[dRow.Index].Cells[3].Value = cantSumar;
                            dataGridViewVenta.Rows[dRow.Index].Cells[4].Value = decimal.Round(decimal.Parse(dtVenta.Rows[0][2].ToString()) * decimal.Parse(cantSumar.ToString()), 2);

                            /*if (dtVenta.Rows[0][3].ToString().Equals("Si"))
                            {
                                dataGridViewVenta.Rows[i].Cells[2].Style.ForeColor = Color.Green;
                                dataGridViewVenta.Rows[i].Cells[3].Style.ForeColor = Color.Green;
                                dataGridViewVenta.Rows[i].Cells[4].Style.ForeColor = Color.Green;
                            }*/
                        }
                        else
                        {

                            //Agregamos el producto al datagridview en orden
                            //Codigo
                            //Nombre
                            //Precio
                            //Cantidad
                            //Importe

                            dt.Rows.Add(dtVenta.Rows[0][0].ToString(),
                                        dtVenta.Rows[0][1].ToString(),
                                        decimal.Round(decimal.Parse(dtVenta.Rows[0][2].ToString()), 2),
                                        cantSumar,
                                        decimal.Round(decimal.Parse(dtVenta.Rows[0][2].ToString()) * decimal.Parse(cantSumar.ToString()), 2)
                                        );

                            //dataGridViewVenta.DataSource = dt;
                            //calculaPrecio();

                            //Como agregamos el producto, siempre es al final, por lo que aplicamos el descuento a la fila final

                            /*if (dtVenta.Rows[0][3].ToString().Equals("Si"))
                            {
                                dataGridViewVenta.Rows[dataGridViewVenta.RowCount - 1].Cells[2].Style.ForeColor = Color.Green;
                                dataGridViewVenta.Rows[dataGridViewVenta.RowCount - 1].Cells[3].Style.ForeColor = Color.Green;
                                dataGridViewVenta.Rows[dataGridViewVenta.RowCount - 1].Cells[4].Style.ForeColor = Color.Green;
                            }*/

                        }

                        calculaPrecio();

                        if (dtVenta.Rows[0][3].ToString().Equals("Si"))
                        {
                            dataGridViewVenta.Rows[dRow != null ? dRow.Index : dataGridViewVenta.RowCount - 1].Cells[2].Style.ForeColor = Color.Green;
                            dataGridViewVenta.Rows[dRow != null ? dRow.Index : dataGridViewVenta.RowCount - 1].Cells[3].Style.ForeColor = Color.Green;
                            dataGridViewVenta.Rows[dRow != null ? dRow.Index : dataGridViewVenta.RowCount - 1].Cells[4].Style.ForeColor = Color.Green;
                        }

                        textBoxCodigo.Text = "";
                        textBoxCantidad.Text = "";

                    }
                    else
                    {
                        Accion a = new Accion();
                        a.ShowDialog();
                        if (a.DialogResult == DialogResult.Yes)
                        {
                            añadidoVenta = true;
                            Añadir_Producto apr = new Añadir_Producto(true);
                            apr.textBoxCodigo.Text = textBoxCodigo.Text;
                            apr.ShowDialog();
                            if (apr.añadido)
                            {
                                añadirProducto();
                                añadidoVenta = false;
                            }
                        }
                        else if (a.DialogResult == DialogResult.OK)
                        {
                            Accion_Productos ap = new Accion_Productos("Editar");
                            ap.textBoxCodigo.Text = textBoxCodigo.Text;
                            ap.ShowDialog();
                            if (ap.editado)
                            {
                                añadirProducto();
                            }
                        }
                    }
                }
            }
            catch (SqlException) { MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente: \n\n - Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (FormatException) { MessageBox.Show("Verifica los datos insertados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void procesoVenta()
        {

            string lista = "";
            string listaCorreo = "";

            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {

                    DataTable dtVenta = Utilerias.EjecutaComando("INSERT INTO venta (venta,  fecha) " +
                                                                       "OUTPUT INSERTED.id_venta " +
                                                                       "VALUES            (@venta, @fecha)",
                                                                       CommandType.Text,
                                                                       openCon,
                                                                       transaction,
                                                                       "",
                                                                       new object[] { "@venta", decimal.Parse(labelSuma.Text) },
                                                                       new object[] { "@fecha", DateTime.Now.Date });

                    for (int i = 0; i < dataGridViewVenta.RowCount; i++)
                    {

                        Utilerias.EjecutaComando("INSERT INTO registro_ventas (id_venta,  id_producto,  precio, " +
                                                       "                             cantidad,  descuento) " +
                                                       "VALUES                      (@id_venta, @id_producto, @precio, " +
                                                       "                             @cantidad, @descuento)",
                                                       CommandType.Text,
                                                       openCon,
                                                       transaction,
                                                       "",
                                                       new object[] { "@id_venta", int.Parse(dtVenta.Rows[0][0].ToString()) },
                                                       new object[] { "@id_producto", dataGridViewVenta.Rows[i].Cells[0].Value.ToString() },
                                                       new object[] { "@precio", decimal.Parse(dataGridViewVenta.Rows[i].Cells[2].Value.ToString()) },
                                                       new object[] { "@cantidad", float.Parse(dataGridViewVenta.Rows[i].Cells[3].Value.ToString()) },
                                                       new object[] { "@descuento", dataGridViewVenta.Rows[i].Cells[2].Style.ForeColor == Color.Green ? 1 : 0 });

                        DataTable dtCantidad = Utilerias.EjecutaComando("UPDATE productos " +
                                                                              "SET    cantidad    = Iif(cantidad - @cantidad <= 0, 0, cantidad - @cantidad) " +
                                                                              "OUTPUT INSERTED.cantidad " +
                                                                              "WHERE  id_producto = @id_producto",
                                                                              CommandType.Text,
                                                                              openCon,
                                                                              transaction,
                                                                              "",
                                                                              new object[] { "@cantidad", double.Parse(dataGridViewVenta.Rows[i].Cells[3].Value.ToString()) },
                                                                              new object[] { "@id_producto", dataGridViewVenta.Rows[i].Cells[0].Value.ToString() });

                        if (double.Parse(dtCantidad.Rows[0][0].ToString()) <= 0)
                        {

                            lista += "- " + dataGridViewVenta.Rows[i].Cells[1].Value.ToString() + "\n";
                            listaCorreo += "- " + dataGridViewVenta.Rows[i].Cells[1].Value.ToString() + "<br>";

                        }

                    }

                    transaction.Commit();

                    if (!lista.Equals(""))
                    {
                        FlexibleMessageBox.Show("La base de datos reporta que el/los producto(s):\n\n" + lista + "\ntiene(n) menor existencia que la deseada a comprar.\n En este caso se realizará la compra, pero favor de \nverificar la existencia real del/los producto(s)", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        if (Properties.Settings.Default.SendEmailNoProduct)
                        {
                            Thread tCorreo = new Thread(new ThreadStart(new Correos(listaCorreo, null).correoVenta));
                            tCorreo.Start();
                        }

                    }

                    //if(MessageBox.Show("¿Desea imprimir comprobante?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    imprimeTicket(idVenta);
                    //}

                    MessageBox.Show("La compra se realizó de manera correcta. ¡Gracias por su preferencia!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();

                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente:\n\n- Verifica si la base de datos está en linea\n- Verifica los datos a grabar. No se realizó venta " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonAgregarNota_Click(object sender, EventArgs e)
        {
            Nota_Credito a = new Nota_Credito(1, dataGridViewVenta, double.Parse(labelSuma.Text));
            a.ShowDialog();

            if (a.action)
            {
                limpiar();
            }
        }

        private void buttonActualizarNota_Click(object sender, EventArgs e)
        {
            Nota_Credito a = new Nota_Credito(2, dataGridViewVenta, double.Parse(labelSuma.Text));
            a.ShowDialog();

            if (a.action)
            {
                limpiar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridViewVenta.Rows.Count > 0 && MessageBox.Show("¿Está seguro de limpiar la venta actual?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                limpiar();
            }
        }
    }
}
