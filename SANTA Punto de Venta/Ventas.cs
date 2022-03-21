using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Net;
using System.Threading;
using JR.Utils.GUI.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Ventas : Form
    {

        public Ventas()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        public bool añadidoVenta = false;
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

            //dataGridViewVenta.Columns.Add("codigo", "Código");
            //dataGridViewVenta.Columns.Add("nombre", "Nombre");
            //dataGridViewVenta.Columns.Add("precio", "Precio");
            //dataGridViewVenta.Columns.Add("cantidad", "Cantidad");
            //dataGridViewVenta.Columns.Add("importe", "Importe");

            //dataGridViewVenta.AutoResizeColumns();
            //dataGridViewVenta.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewVenta.Columns[0].ReadOnly = true;
            //dataGridViewVenta.Columns[1].ReadOnly = true;
            //dataGridViewVenta.Columns[2].ReadOnly = true;
            //dataGridViewVenta.Columns[4].ReadOnly = true;

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

            if (labelRestante.Visible)
            {
                decimal restante = decimal.Round(decimal.Parse(labelSuma.Text), 2) - decimal.Round(decimal.Parse(labelPagado.Text), 2);
                restante = Math.Abs(restante);

                if(decimal.Round(decimal.Parse(labelSuma.Text), 2) <= decimal.Round(decimal.Parse(labelPagado.Text), 2))
                {
                    labelResta.Text = "Sobra: $";
                }
                else
                {
                    labelResta.Text = "Resta: $";
                }

                buttonCompra.Enabled = true;
                labelRestante.Text = (decimal.Round(restante, 2)).ToString();
            }

            //Activación del botón compra
            if ((textBoxPago.TextLength == 0 &&
                    decimal.Round(decimal.Parse(labelPagado.Text), 2) >= decimal.Round(decimal.Parse(labelSuma.Text), 2) &&
                    decimal.Round(decimal.Parse(labelPagado.Text), 2) > 0.00M)
                || (textBoxPago.TextLength > 0 && dataGridViewVenta.RowCount > 0))
            {
                buttonCompra.Enabled = true;
            }
            else
            {
                buttonCompra.Enabled = false;
            }
        }

        private void dataGridViewVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                double ejem = 0;
                if (!double.TryParse(dataGridViewVenta.Rows[e.RowIndex].Cells[3].Value.ToString(), out ejem))
                {
                    dataGridViewVenta.Rows[e.RowIndex].Cells[3].Value = 1;
                }
                try
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        SqlDataAdapter select = new SqlDataAdapter("SELECT p.id_producto, p.nombre, IIF( " + dataGridViewVenta.Rows[e.RowIndex].Cells[3].Value.ToString() + " >= d.cantidadMinima AND d.status = 1, d.precioDescuento, p.precio ) precio, " +
                                                                    "IIF( " + dataGridViewVenta.Rows[e.RowIndex].Cells[3].Value.ToString() + " >= d.cantidadMinima AND d.status = 1, 'Si', 'No' ) descuento " +
                                                                    "FROM  productos p " +
                                                                    "LEFT OUTER JOIN descuentos d " +
                                                                    "ON    p.id_producto = d.id_producto " +
                                                                    "WHERE p.id_producto = '" + dataGridViewVenta.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", openCon);
                        select.SelectCommand.Transaction = transaction;

                        DataTable dtVenta = new DataTable();
                        select.Fill(dtVenta); 
                         
                        if (dtVenta.Rows.Count > 0)
                        {

                            if (dtVenta.Rows[0][3].ToString().Equals("Si"))
                            {
                                dataGridViewVenta.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Green;
                                dataGridViewVenta.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Green;
                                dataGridViewVenta.Rows[e.RowIndex].Cells[4].Style.ForeColor = Color.Green;
                            }
                            else
                            {
                                dataGridViewVenta.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.White;
                                dataGridViewVenta.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.White;
                                dataGridViewVenta.Rows[e.RowIndex].Cells[4].Style.ForeColor = Color.White;
                            }

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
            if(dataGridViewVenta.RowCount > 0 && contextMenuStripTabla.Items[0].Selected)
            {
                dataGridViewVenta.Rows.Remove(dataGridViewVenta.CurrentRow);

                calculaPrecio();
                if(dataGridViewVenta.RowCount > 2)
                 dataGridViewVenta.CurrentCell = dataGridViewVenta.Rows[dataGridViewVenta.RowCount - 1].Cells[0];
            }           
        }

        private void dataGridViewVenta_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if(e.RowIndex != -1)
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
                        labelPagado.Text   = (decimal.Round(decimal.Parse(labelPagado.Text), 2) + decimal.Round(decimal.Parse(textBoxPago.Text), 2)).ToString();
                        labelRestante.Text = (decimal.Round(decimal.Parse(labelSuma.Text), 2) - decimal.Round(decimal.Parse(labelPagado.Text), 2)).ToString();
                    }
                    else
                    {
                        labelPagado.Text      = (decimal.Round(decimal.Parse(textBoxPago.Text) + 0.00M, 2)).ToString();
                        labelPago.Visible     = true;
                        labelPagado.Visible   = true;
                        buttonLinea.Visible   = true;
                        labelResta.Visible    = true;
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
            int rows = dataGridViewVenta.RowCount;
            for (int i = 0; i < rows; i++)
            {
                dataGridViewVenta.Rows.RemoveAt(0);
            }
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
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter && textBoxCodigo.TextLength > 0)
            {
                buttonAñadir_Click(sender, e);
            }
        }

        private void textBoxPago_TextChanged(object sender, EventArgs e)
        {
            //Activación del botón compra
            if(   (textBoxPago.TextLength == 0 && 
                   decimal.Round(decimal.Parse(labelPagado.Text), 2) >= decimal.Round(decimal.Parse(labelSuma.Text), 2) && 
                   decimal.Round(decimal.Parse(labelPagado.Text), 2) > 0.00M)
               || (textBoxPago.TextLength > 0 && dataGridViewVenta.RowCount > 0))
            {
                buttonCompra.Enabled = true;
            }
            else
            {
                buttonCompra.Enabled = false;
            }
        }

        private void textBoxPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
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
            }

            if (e.KeyChar == (char)Keys.Enter && dataGridViewVenta.RowCount > 0)
            {
                if(textBoxPago.TextLength > 0 || decimal.Round(decimal.Parse(labelPagado.Text), 2) >= decimal.Round(decimal.Parse(labelSuma.Text), 2))
                {
                    buttonCompra_Click(sender, e);
                }
            }
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

            for(int i = 0; i < dataGridViewVenta.RowCount; i++)
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
            if(textBoxCodigo.TextLength == 0)
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
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
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
            }
        }

        private void textBoxCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            { 
                Buscar_Producto a = new Buscar_Producto();
                a.buscar = textBoxCodigo.Text;
                a.ShowDialog();
                if(a.buttonAceptar.DialogResult == DialogResult.OK)
                {
                    for(int i = 0; i < a.dataGridViewProductosAgregados.RowCount; i++)
                    {
                        textBoxCodigo.Text = a.dataGridViewProductosAgregados.Rows[i].Cells[0].Value.ToString();
                        añadirProducto();
                    }
                }
            }
        }

        public void añadirProducto()
        {

            double cantSumar = 1;
            if (!textBoxCantidad.Text.Equals(""))
            {
                cantSumar = double.Parse(textBoxCantidad.Text);
            }

            //Como la consulta ya traeria por su cuenta el precio que se deba aplicar, con o sin descuento, debemos primero
            //verificar si en el datagrid ya esta el articulo a ingresar, sumamos la cantidad que exista, para poder hacer
            //la consulta de manera correcta, y aplicar el descuento

            for(int i = 0; i < dataGridViewVenta.RowCount; i++)
            {
                if (dataGridViewVenta.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(textBoxCodigo.Text.ToUpper()))
                {
                    cantSumar += double.Parse(dataGridViewVenta.Rows[i].Cells[3].Value.ToString());
                }
            }

            double cantidad = cantSumar;

            try
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();

                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT p.id_producto, p.nombre, IIF( " + cantidad.ToString() + " >= d.cantidadMinima AND d.status = 1, d.precioDescuento, p.precio ) precio, " +
                                                                              "IIF( " + cantidad.ToString() + " >= d.cantidadMinima AND d.status = 1, 'Si', 'No' ) descuento, p.status " +
                                                                              "FROM  productos p " +
                                                                              "LEFT OUTER JOIN descuentos d " +
                                                                              "ON    p.id_producto = d.id_producto " +
                                                                              "WHERE p.id_producto = '" + textBoxCodigo.Text + "'", openCon, transaction));

                    DataTable dtVenta = new DataTable();
                    select.Fill(dtVenta);


                    if (dtVenta.Rows.Count > 0)
                    {
                        bool enter = true;

                        //Si el producto esta desactivado, se informa al usuario
                        if (int.Parse(dtVenta.Rows[0][4].ToString()) == 0)
                        {
                            MessageBox.Show("El producto '" + dtVenta.Rows[0][1].ToString() + "' está en estatus inactivo. Verifica.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            for (int i = 0; i < dataGridViewVenta.RowCount; i++)
                            {
                                //Si detectamos que el producto ya existe en el datagridview, solo actualizamos la cantidad, el importe, y si hay productos con descuento 
                                if (dataGridViewVenta.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(textBoxCodigo.Text.ToUpper()))
                                {
                                    dataGridViewVenta.Rows[i].Cells[2].Value = decimal.Round(decimal.Parse(dtVenta.Rows[0][2].ToString()), 2);
                                    dataGridViewVenta.Rows[i].Cells[3].Value = cantidad;
                                    dataGridViewVenta.Rows[i].Cells[4].Value = decimal.Round(decimal.Parse(dtVenta.Rows[0][2].ToString()) * decimal.Parse(cantidad.ToString()), 2);
                                    calculaPrecio();
                                    enter = false;

                                    if (dtVenta.Rows[0][3].ToString().Equals("Si"))
                                    {
                                        dataGridViewVenta.Rows[i].Cells[2].Style.ForeColor = Color.Green;
                                        dataGridViewVenta.Rows[i].Cells[3].Style.ForeColor = Color.Green;
                                        dataGridViewVenta.Rows[i].Cells[4].Style.ForeColor = Color.Green;
                                    }
                                }
                            }
                            if (enter)
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
                                            cantidad,
                                            decimal.Round(decimal.Parse(dtVenta.Rows[0][2].ToString()) * decimal.Parse(cantidad.ToString()), 2)
                                            );

                                dataGridViewVenta.DataSource = dt;
                                calculaPrecio();

                                //Como agregamos el producto, siempre es al final, por lo que aplicamos el descuento a la fila final

                                if (dtVenta.Rows[0][3].ToString().Equals("Si"))
                                {
                                    dataGridViewVenta.Rows[dataGridViewVenta.RowCount - 1].Cells[2].Style.ForeColor = Color.Green;
                                    dataGridViewVenta.Rows[dataGridViewVenta.RowCount - 1].Cells[3].Style.ForeColor = Color.Green;
                                    dataGridViewVenta.Rows[dataGridViewVenta.RowCount - 1].Cells[4].Style.ForeColor = Color.Green;
                                }

                                for (int i = 0; i < dataGridViewVenta.ColumnCount; i++)
                                {
                                    dataGridViewVenta.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                                }
                            }

                            textBoxCodigo.Text = "";
                            textBoxCantidad.Text = "";
                        }
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

                    using (SqlCommand queryAdd = new SqlCommand("INSERT INTO venta (venta,  fecha) " +
                                                                "VALUES            (@venta, @fecha)", openCon, transaction))
                    {
                        
                        queryAdd.Parameters.Add("@venta", SqlDbType.Decimal).Value = labelSuma.Text;
                        queryAdd.Parameters.Add("@fecha", SqlDbType.Date).Value = DateTime.Now;

                        queryAdd.ExecuteNonQuery();

                    }

                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT MAX(id_venta) " +
                                                                              "FROM   venta", openCon, transaction));

                    DataTable dtVenta = new DataTable();
                    select.Fill(dtVenta);

                    for (int i = 0; i < dataGridViewVenta.RowCount; i++)
                    {
                        using (SqlCommand queryAdd = new SqlCommand("INSERT INTO registro_ventas (id_venta,  id_producto,  precio, " +
                                                                    "                             cantidad,  descuento) " +
                                                                    "VALUES                      (@id_venta, @id_producto, @precio, " +
                                                                    "                             @cantidad, @descuento)", openCon, transaction))
                        {
                            
                            queryAdd.Parameters.Add("@id_venta",    SqlDbType.Int).Value     = dtVenta.Rows[0][0].ToString();
                            queryAdd.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = dataGridViewVenta.Rows[i].Cells[0].Value.ToString();
                            queryAdd.Parameters.Add("@precio",      SqlDbType.Decimal).Value = dataGridViewVenta.Rows[i].Cells[2].Value.ToString();   
                            queryAdd.Parameters.Add("@cantidad",    SqlDbType.Float).Value   = dataGridViewVenta.Rows[i].Cells[3].Value.ToString(); 

                            if (dataGridViewVenta.Rows[i].Cells[2].Style.ForeColor == Color.Green)
                            {
                                queryAdd.Parameters.Add("@descuento", SqlDbType.Float).Value = 1;
                            }
                            else
                            {
                                queryAdd.Parameters.Add("@descuento", SqlDbType.Float).Value = 0;
                            }

                            queryAdd.ExecuteNonQuery();
                        }

                        using (SqlCommand queryUpdate = new SqlCommand("UPDATE productos " +
                                                                       "SET    cantidad    = @cantidad " +
                                                                       "WHERE  id_producto = @id_producto", openCon, transaction))
                        {
                            
                            SqlDataAdapter selectCant = new SqlDataAdapter(new SqlCommand("SELECT cantidad " +
                                                                                          "FROM   productos " +
                                                                                          "WHERE  id_producto = '" + dataGridViewVenta.Rows[i].Cells[0].Value.ToString() + "'", openCon, transaction));

                            DataTable dtCantidad = new DataTable();
                            selectCant.Fill(dtCantidad);

                            double cantNueva = double.Parse(dtCantidad.Rows[0][0].ToString()) - double.Parse(dataGridViewVenta.Rows[i].Cells[3].Value.ToString());

                            if (cantNueva < 0)
                            {
                                cantNueva = 0;

                                lista += "- " + dataGridViewVenta.Rows[i].Cells[1].Value.ToString() + "\n";
                                listaCorreo += "- " + dataGridViewVenta.Rows[i].Cells[1].Value.ToString() + "<br>";

                            }

                            queryUpdate.Parameters.Add("@cantidad",    SqlDbType.Float).Value   = cantNueva;
                            queryUpdate.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = dataGridViewVenta.Rows[i].Cells[0].Value.ToString();

                            queryUpdate.ExecuteNonQuery();
                        }

                    }

                    transaction.Commit();

                    if (!lista.Equals(""))
                    {
                        Thread tCorreo = new Thread(new ThreadStart(new Correos(listaCorreo, null).correoVenta));
                        tCorreo.Start();

                        FlexibleMessageBox.Show("La base de datos reporta que el/los producto(s):\n\n" + lista + "\ntiene(n) menor existencia que la deseada a comprar.\n En este caso se realizará la compra, pero favor de \nverificar la existencia real del/los producto(s)", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    //if(MessageBox.Show("¿Desea imprimir comprobante?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    imprimeTicket(idVenta);
                    //}

                    MessageBox.Show("La compra se realizó de manera correcta. ¡Gracias por su preferencia!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();

                }
                catch (SqlException)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente:\n\n- Verifica si la base de datos está en linea\n- Verifica los datos a grabar. No se realizó venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                 
                }
            }
        }

        private void buttonAgregarNota_Click(object sender, EventArgs e)
        {            
            Nota_Credito a = new Nota_Credito(1, dataGridViewVenta, double.Parse(labelSuma.Text));
            a.ShowDialog();

            if (a.action){
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
            if(MessageBox.Show("¿Está seguro de limpiar la venta actual?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                limpiar();
            }
        }
    }
}
