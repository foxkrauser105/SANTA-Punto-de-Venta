using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Venta_Dia : Form
    {
        public Venta_Dia()
        {
            InitializeComponent();
            dateTimePickerFecha.Value = DateTime.Now;
        }

        DataTable dtVenta = new DataTable();
        DataTable dtInicio = new DataTable();
        double venta = 0;


        public void loadAll()
        {
            buttonGuardar.Visible = true;
            buttonEnviar.Visible = false;
            venta = 0;
            limpiar();
            try
            {
                using(SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    SqlDataAdapter select = new SqlDataAdapter("SELECT * " +
                                                               "FROM   venta_dia " +
                                                               "WHERE  fecha = '" + dateTimePickerFecha.Value.Date.ToString("yyyy-MM-dd") + "'", openCon);                   
                    select.Fill(dtVenta);

                    SqlDataAdapter inicio = new SqlDataAdapter("SELECT IIF(SUM(a.venta) IS NOT NULL, SUM(a.venta), 0) Venta " +
                                                               "FROM   venta a " +
                                                               "WHERE  fecha = '" + dateTimePickerFecha.Value.Date.ToString("yyyy-MM-dd") + "'", openCon);
                    inicio.Fill(dtInicio);

                    if (dtVenta.Rows.Count > 0)
                    {
                        //MessageBox.Show("0");
                        textBoxInicio.Text = dtVenta.Rows[0][1].ToString();
                        textBoxMonedas.Text = dtVenta.Rows[0][2].ToString();
                        textBoxUsoMonedas.Text = dtVenta.Rows[0][3].ToString();
                        textBoxProveedores.Text = dtVenta.Rows[0][4].ToString();
                        textBoxGasto.Text = dtVenta.Rows[0][5].ToString();
                        textBoxQuedo.Text = dtVenta.Rows[0][6].ToString();
                        textBoxSaldoInicial.Text = dtVenta.Rows[0][7].ToString();
                        textBoxSaldoFinal.Text = dtVenta.Rows[0][8].ToString();
                        textBoxCaja.Text = dtVenta.Rows[0][9].ToString();
                        textBoxVentaSaldo.Text = dtVenta.Rows[0][10].ToString();

                        venta = double.Parse(dtInicio.Rows[0][0].ToString());

                        textBoxVentaAbarrote.Text = (venta - double.Parse(dtVenta.Rows[0][10].ToString())).ToString();
                        textBoxTotal.Text = dtVenta.Rows[0][12].ToString();
                        textBoxFinal.Text = dtVenta.Rows[0][13].ToString();

                        buttonGuardar.Visible = false;
                        buttonEnviar.Visible = true;
                    }
                    else
                    {
                        //MessageBox.Show("1");
                        textBoxVentaAbarrote.Text = dtInicio.Rows[0][0].ToString();

                    }
                }
            }
            catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo\n- Verifica que la tabla 'venta_dia' no haya sido modificada y prueba de nuevo ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Venta_Dia_Load(object sender, EventArgs e)
        {
            loadAll();
            textBoxQuedo.ReadOnly = true;
            textBoxVentaSaldo.ReadOnly = true;
            textBoxVentaAbarrote.ReadOnly = true;
            textBoxFinal.ReadOnly = true;
            textBoxTotal.ReadOnly = true;
        }

        private void dateTimePickerFecha_ValueChanged(object sender, EventArgs e)
        {
            loadAll();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if(textBoxCaja.Text == "" || textBoxFinal.Text == "" || textBoxGasto.Text == "" || textBoxInicio.Text == "" || textBoxMonedas.Text == "" || textBoxProveedores.Text == "" || textBoxQuedo.Text == "" || 
                textBoxSaldoFinal.Text == "" || textBoxSaldoInicial.Text == "" || textBoxTotal.Text == "" || textBoxUsoMonedas.Text == "" || textBoxVentaAbarrote.Text == "" || textBoxVentaSaldo.Text == "")
            {
                MessageBox.Show("Verifica los campos. No dejes ningún espacio en blanco", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {               
                if (MessageBox.Show("¿Deseas guardar la venta del día actual para el dia de hoy?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        try
                        {
                            string add = "INSERT INTO venta_dia (inicio, monedas, uso_monedas, proveedores, gasto, quedo, saldo_inicial, saldo_final, caja, venta_saldo, venta_abarrote, total, final, fecha) " +
                            "VALUES (@inicio, @monedas, @uso_monedas, @proveedores, @gasto, @quedo, @saldo_inicial, @saldo_final, @caja, @venta_saldo, @venta_abarrote, @total, @final, @fecha)";
                            using (SqlCommand queryAdd = new SqlCommand(add))
                            {
                                queryAdd.Connection = openCon;
                                queryAdd.Transaction = transaction;
                                queryAdd.Parameters.Add("@inicio", SqlDbType.Float).Value = textBoxInicio.Text;
                                queryAdd.Parameters.Add("@monedas", SqlDbType.Float).Value = textBoxMonedas.Text;
                                queryAdd.Parameters.Add("@uso_monedas", SqlDbType.Float).Value = textBoxUsoMonedas.Text;
                                queryAdd.Parameters.Add("@proveedores", SqlDbType.Float).Value = textBoxProveedores.Text;
                                queryAdd.Parameters.Add("@gasto", SqlDbType.Float).Value = textBoxGasto.Text;
                                queryAdd.Parameters.Add("@quedo", SqlDbType.Float).Value = textBoxQuedo.Text;
                                queryAdd.Parameters.Add("@saldo_inicial", SqlDbType.Float).Value = textBoxSaldoInicial.Text;
                                queryAdd.Parameters.Add("@saldo_final", SqlDbType.Float).Value = textBoxSaldoFinal.Text;
                                queryAdd.Parameters.Add("@caja", SqlDbType.Float).Value = textBoxCaja.Text;
                                queryAdd.Parameters.Add("@venta_saldo", SqlDbType.Float).Value = textBoxVentaSaldo.Text;
                                queryAdd.Parameters.Add("@venta_abarrote", SqlDbType.Float).Value = textBoxVentaAbarrote.Text;
                                queryAdd.Parameters.Add("@total", SqlDbType.Float).Value = textBoxTotal.Text;
                                queryAdd.Parameters.Add("@final", SqlDbType.Float).Value = textBoxFinal.Text;
                                queryAdd.Parameters.Add("@fecha", SqlDbType.Date).Value = DateTime.Now;

                                queryAdd.ExecuteNonQuery();

                                transaction.Commit();
                                MessageBox.Show("La venta del día ha sido guardada exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadAll();
                            }
                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo\n- Verifica que la tabla 'venta_dia' no haya sido modificada y prueba de nuevo\n-Verifica que no hayas escrito letras o dos puntos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        public void limpiar()
        {
            textBoxCaja.Text = textBoxFinal.Text = textBoxGasto.Text = textBoxInicio.Text = textBoxMonedas.Text = textBoxProveedores.Text = textBoxQuedo.Text = textBoxSaldoFinal.Text = textBoxSaldoInicial.Text
                = textBoxTotal.Text = textBoxUsoMonedas.Text = textBoxVentaAbarrote.Text = textBoxVentaSaldo.Text = "";
            dtVenta.Clear();
            dtInicio.Clear();
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            DataRow row = dtVenta.Rows[0];
            if (textBoxInicio.Text == row[1].ToString() && textBoxMonedas.Text == row[2].ToString() && textBoxUsoMonedas.Text == row[3].ToString() && textBoxProveedores.Text == row[4].ToString()
                && textBoxGasto.Text == row[5].ToString() && textBoxQuedo.Text == row[6].ToString() && textBoxSaldoInicial.Text == row[7].ToString() && textBoxSaldoFinal.Text == row[8].ToString()
                && textBoxCaja.Text == row[9].ToString() && textBoxVentaSaldo.Text == row[10].ToString() && textBoxVentaAbarrote.Text == row[11].ToString() && textBoxTotal.Text == row[12].ToString()
                && textBoxFinal.Text == row[13].ToString())
            {
                MessageBox.Show("Los valores de los campos no han sido modificados. No es necesario realizar una actualización", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("¿Deseas actualizar la venta del día actual para el dia '" + dateTimePickerFecha.Value.ToLongDateString() + "'?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        try
                        {
                            string update = "UPDATE venta_dia " +
                                            "SET    inicio         = @inicio, " +
                                            "       monedas        = @monedas, " +
                                            "       uso_monedas    = @uso_monedas, " +
                                            "       proveedores    = @proveedores, " +
                                            "       gasto          = @gasto, " +
                                            "       quedo          = @quedo, " +
                                            "       saldo_inicial  = @saldo_inicial, " +
                                            "       saldo_final    = @saldo_final, " +
                                            "       caja           = @caja, " +
                                            "       venta_saldo    = @venta_saldo, " +
                                            "       venta_abarrote = @venta_abarrote, " +
                                            "       total          = @total, " +
                                            "       final          = @final " +
                                            "WHERE  fecha          = @fecha";

                            using (SqlCommand queryUpdate = new SqlCommand(update))
                            {
                                queryUpdate.Connection = openCon;
                                queryUpdate.Transaction = transaction;
                                queryUpdate.Parameters.Add("@inicio", SqlDbType.Float).Value = textBoxInicio.Text;
                                queryUpdate.Parameters.Add("@monedas", SqlDbType.Float).Value = textBoxMonedas.Text;
                                queryUpdate.Parameters.Add("@uso_monedas", SqlDbType.Float).Value = textBoxUsoMonedas.Text;
                                queryUpdate.Parameters.Add("@proveedores", SqlDbType.Float).Value = textBoxProveedores.Text;
                                queryUpdate.Parameters.Add("@gasto", SqlDbType.Float).Value = textBoxGasto.Text;
                                queryUpdate.Parameters.Add("@quedo", SqlDbType.Float).Value = textBoxQuedo.Text;
                                queryUpdate.Parameters.Add("@saldo_inicial", SqlDbType.Float).Value = textBoxSaldoInicial.Text;
                                queryUpdate.Parameters.Add("@saldo_final", SqlDbType.Float).Value = textBoxSaldoFinal.Text;
                                queryUpdate.Parameters.Add("@caja", SqlDbType.Float).Value = textBoxCaja.Text;
                                queryUpdate.Parameters.Add("@venta_saldo", SqlDbType.Float).Value = textBoxVentaSaldo.Text;
                                queryUpdate.Parameters.Add("@venta_abarrote", SqlDbType.Float).Value = textBoxVentaAbarrote.Text;
                                queryUpdate.Parameters.Add("@total", SqlDbType.Float).Value = textBoxTotal.Text;
                                queryUpdate.Parameters.Add("@final", SqlDbType.Float).Value = textBoxFinal.Text;
                                queryUpdate.Parameters.Add("@fecha", SqlDbType.Date).Value = dateTimePickerFecha.Value.Date.ToString("yyyy-MM-dd");

                                queryUpdate.ExecuteNonQuery();

                                transaction.Commit();
                                MessageBox.Show("La venta del día ha sido actualizada exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadAll();
                            }
                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo\n- Verifica que la tabla 'venta_dia' no haya sido modificada y prueba de nuevo\n-Verifica que no hayas escrito letras o dos puntos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }                        
                    }
                }              
            }      
        }

        private void textBoxGasto_TextChanged(object sender, EventArgs e)
        {
            if(textBoxGasto.TextLength > 0 && textBoxProveedores.TextLength > 0)
            {
                textBoxQuedo.Text = (double.Parse(textBoxProveedores.Text) - double.Parse(textBoxGasto.Text)).ToString();           
            }
        }

        private void textBoxProveedores_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGasto.TextLength > 0 && textBoxProveedores.TextLength > 0)
            {
                textBoxQuedo.Text = (double.Parse(textBoxProveedores.Text) - double.Parse(textBoxGasto.Text)).ToString();               
            }
        }

        private void textBoxSaldoInicial_TextChanged(object sender, EventArgs e)
        {
            if(textBoxSaldoInicial.TextLength > 0 && textBoxSaldoFinal.TextLength > 0)
            {               
                textBoxVentaSaldo.Text = (double.Parse(textBoxSaldoInicial.Text) - double.Parse(textBoxSaldoFinal.Text)).ToString();               
            }
        }

        private void textBoxSaldoFinal_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSaldoInicial.TextLength > 0 && textBoxSaldoFinal.TextLength > 0)
            {
                textBoxVentaSaldo.Text = (double.Parse(textBoxSaldoInicial.Text) - double.Parse(textBoxSaldoFinal.Text)).ToString();
            }

            if (textBoxCaja.TextLength > 0 && textBoxMonedas.TextLength > 0 && textBoxSaldoFinal.TextLength > 0 && textBoxVentaAbarrote.TextLength > 0 && textBoxVentaSaldo.TextLength > 0)
            {
                textBoxFinal.Text = (double.Parse(textBoxMonedas.Text) + double.Parse(textBoxSaldoFinal.Text) + double.Parse(textBoxVentaSaldo.Text) + double.Parse(textBoxVentaAbarrote.Text) + double.Parse(textBoxCaja.Text)).ToString();
                textBoxTotal.Text = (double.Parse(textBoxMonedas.Text) + double.Parse(textBoxVentaSaldo.Text) + double.Parse(textBoxVentaAbarrote.Text) + double.Parse(textBoxCaja.Text)).ToString();
            }
        }

        private void textBoxVentaSaldo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxVentaAbarrote.Text = (venta - double.Parse(textBoxVentaSaldo.Text)).ToString();
            }
            catch (FormatException) { }
        }

        private void textBoxCaja_TextChanged(object sender, EventArgs e)
        {
            if(textBoxCaja.TextLength > 0 && textBoxMonedas.TextLength > 0 && textBoxSaldoFinal.TextLength > 0 && textBoxVentaAbarrote.TextLength > 0 && textBoxVentaSaldo.TextLength > 0)
            {
                textBoxFinal.Text = (double.Parse(textBoxMonedas.Text) + double.Parse(textBoxSaldoFinal.Text) + double.Parse(textBoxVentaSaldo.Text) + double.Parse(textBoxVentaAbarrote.Text) + double.Parse(textBoxCaja.Text)).ToString();
                textBoxTotal.Text = (double.Parse(textBoxMonedas.Text) + double.Parse(textBoxVentaSaldo.Text) + double.Parse(textBoxVentaAbarrote.Text) + double.Parse(textBoxCaja.Text)).ToString();
            }
        }

        private void textBoxMonedas_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCaja.TextLength > 0 && textBoxMonedas.TextLength > 0 && textBoxSaldoFinal.TextLength > 0 && textBoxVentaAbarrote.TextLength > 0 && textBoxVentaSaldo.TextLength > 0)
            {
                textBoxFinal.Text = (double.Parse(textBoxMonedas.Text) + double.Parse(textBoxSaldoFinal.Text) + double.Parse(textBoxVentaSaldo.Text) + double.Parse(textBoxVentaAbarrote.Text) + double.Parse(textBoxCaja.Text)).ToString();
                textBoxTotal.Text = (double.Parse(textBoxMonedas.Text) + double.Parse(textBoxVentaSaldo.Text) + double.Parse(textBoxVentaAbarrote.Text) + double.Parse(textBoxCaja.Text)).ToString();
            }
        }

        private void textBoxInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxInicio.Text.Contains("."))
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

        private void textBoxMonedas_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxMonedas.Text.Contains("."))
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

        private void textBoxUsoMonedas_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxUsoMonedas.Text.Contains("."))
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

        private void textBoxProveedores_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxProveedores.Text.Contains("."))
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

        private void textBoxGasto_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxGasto.Text.Contains("."))
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

        private void textBoxQuedo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxQuedo.Text.Contains("."))
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

        private void textBoxSaldoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxSaldoInicial.Text.Contains("."))
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

        private void textBoxSaldoFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxSaldoFinal.Text.Contains("."))
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

        private void textBoxCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxCaja.Text.Contains("."))
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

        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea enviar correo con la venta del día?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string ciber = "CiberStore";

                string header = "<tr><th bordercolor=#FFFFE0>Campo</th><th bordercolor=#FFFFE0>Valor</th>";
                string style = "<style type=\"text/css\"> th {background-color: #879cff } table {background: #eeeeee;} </style>";
                string body = "<tr><td>Inicio</td><td>" + textBoxInicio.Text + "</td></tr>" +
                    "<tr><td>Monedas</td><td>" + textBoxMonedas.Text + "</td></tr>" +
                    "<tr><td>Uso Monedas</td><td>" + textBoxUsoMonedas.Text + "</td></tr>" +
                    "<tr><td>Proveedores</td><td>" + textBoxProveedores.Text + "</td></tr>" +
                    "<tr><td>Gasto</td><td>" + textBoxGasto.Text + "</td></tr>" +
                    "<tr><td>Queda</td><td>" + textBoxQuedo.Text + "</td></tr>" +
                    "<tr><td>Saldo Inicial</td><td>" + textBoxSaldoInicial.Text + "</td></tr>" +
                    "<tr><td>Saldo Final</td><td>" + textBoxSaldoFinal.Text + "</td></tr>" +
                    "<tr><td>Caja</td><td>" + textBoxCaja.Text + "</td></tr>" +
                    "<tr><td>Venta Saldo</td><td>" + textBoxVentaSaldo.Text + "</td></tr>" +
                    "<tr><td>Venta Abarrote</td><td>" + textBoxVentaAbarrote.Text + "</td></tr>" +
                    "<tr><td>Total</td><td>" + textBoxTotal.Text + "</td></tr>" +
                    "<tr><td>Final</td><td>" + textBoxFinal.Text + "</td></tr>";

                string fecha = "Venta " + ciber + " ( " + dateTimePickerFecha.Value.Date.ToString("dd / MMM / yyy") + " )";
                string bodyFull = "Estatus de Venta del día de hoy para " + ciber + ": " + "<table border=1 cellpadding=0>" + header + body + "</table>" + style;

                Thread tCorreo = new Thread(new ThreadStart(new Correos(bodyFull, fecha).correoVentaDia));
                tCorreo.Start();

                MessageBox.Show("Correo enviado satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void Venta_Dia_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxFinal_KeyDown(sender, e);
        }

        private void textBoxFinal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (textBoxVentaAbarrote.Visible)
                {
                    textBoxVentaAbarrote.Visible = false;
                    textBoxVentaSaldo.Visible = false;
                    labelVentaAbarrote.Visible = false;
                    labelVentaSaldo.Visible = false;
                }
                else
                {
                    Contraseña con = new Contraseña();

                    if (con.DialogResult == DialogResult.No)
                    {
                        con.Dispose();
                    }
                    if (con.ShowDialog(this) == DialogResult.Yes && con.textBoxContraseña.Text.Equals("hancock7"))
                    {
                        textBoxVentaAbarrote.Visible = true;
                        textBoxVentaSaldo.Visible = true;
                        labelVentaAbarrote.Visible = true;
                        labelVentaSaldo.Visible = true;
                    }

                    if (con.DialogResult == DialogResult.Yes && !con.textBoxContraseña.Text.Equals("hancock7"))
                    {
                        MessageBox.Show("Contraseña incorrecta. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Venta_Dia_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)Keys.F9)
            {
                if (textBoxVentaAbarrote.Visible)
                {
                    textBoxVentaAbarrote.Visible = false;
                    textBoxVentaSaldo.Visible = false;
                    labelVentaAbarrote.Visible = false;
                    labelVentaSaldo.Visible = false;
                }
                else
                {
                    Contraseña con = new Contraseña();

                    if (con.DialogResult == DialogResult.No)
                    {
                        con.Dispose();
                    }
                    if (con.ShowDialog(this) == DialogResult.Yes && con.textBoxContraseña.Text.Equals("hancock7"))
                    {
                        textBoxVentaAbarrote.Visible = true;
                        textBoxVentaSaldo.Visible = true;
                        labelVentaAbarrote.Visible = true;
                        labelVentaSaldo.Visible = true;
                    }

                    if (con.DialogResult == DialogResult.Yes && !con.textBoxContraseña.Text.Equals("hancock7"))
                    {
                        MessageBox.Show("Contraseña incorrecta. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
