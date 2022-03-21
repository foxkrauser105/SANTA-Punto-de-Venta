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

namespace SANTA_Punto_de_Venta
{
    public partial class Nota_Credito : Form
    {
        //Variable que permitira saber si se consultará para pagar, agregará nota, o actualizará nota
        public int accion;

        //Monto a asignar a la nota, o actualizar a la nota
        public double monto;

        //Si se actualiza, se le pasa un parametro de tipo datatgridview, para ponerlo en el data gridview que permite ver los productos a añadir
        public DataGridView dgvAgregarProductos;

        //Permite controlar si se agregó/actualizó alguna nota de crédito
        public bool action = false;

        //Permite saber si se debe consultar un usuario o no
        private bool numcliente = false;

        DataTable dtNota = new DataTable();

        public Nota_Credito(int act, DataGridView data, double mon)
        {
            InitializeComponent();
            accion = act;
            dgvAgregarProductos = data;
            monto = mon;
        }

        private void Nota_Credito_Load(object sender, EventArgs e)
        {
            loadAll();
        }

        public void loadAll()
        {
            if(accion != 0)
            {
                labelMontoPagar.Visible          = false;
                textBoxMontoPagar.Visible        = false;
                                                 
                textBoxNCFolio.Enabled           = false;
                /*labelBusqueda.Visible            = false;
                textBoxBuscar.Visible            = false;*/
              
                dataGridViewProductos.DataSource = dgvAgregarProductos.DataSource;
                dataGridViewProductos.Visible    = true;

                if (dataGridViewProductos.RowCount > 0)
                {
                    dataGridViewProductos.AutoResizeColumns();
                    dataGridViewProductos.Columns[dataGridViewProductos.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                

                labelSuma.Text                   = monto.ToString();
                labelSuma.Visible                = true;
                labelAnadir.Visible              = true;

                if(dataGridViewProductos.RowCount > 0)
                {
                    int conteo=0;

                    for (int i = 0; i < dataGridViewProductos.ColumnCount; i++)
                    {
                        dataGridViewProductos.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    for(int i=0; i<dataGridViewProductos.RowCount; i++)
                    {
                        using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                        {
                            openCon.Open();
                            SqlTransaction transaction = openCon.BeginTransaction();
                            try
                            {
                                SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT IIF( " + dataGridViewProductos.Rows[i].Cells[3].Value.ToString() + " >= d.cantidadMinima AND d.status = 1, 'Si', 'No' ) descuento " +
                                                                                          "FROM  productos p " +
                                                                                          "LEFT OUTER JOIN descuentos d " +
                                                                                          "ON    p.id_producto = d.id_producto " +
                                                                                          "WHERE p.id_producto = '" + dataGridViewProductos.Rows[i].Cells[0].Value.ToString() + "'", openCon, transaction));

                                DataTable dtDescuento = new DataTable();
                                select.Fill(dtDescuento);

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
                    }

                    if(conteo>0)
                        MessageBox.Show("Ha ocurrido un problema, los descuentos no se pudieron mostrar pero si están presentes", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                //Si es 1, agregar nota de credito a cliente
                if (accion == 1)
                {
                    buttonAccion.Text                     = "Agregar";
                    dateTimePickerFechaCompromiso.Enabled = true;
                    textBoxStatus.Text                    = "Pendiente";
                    textBoxTotal.Text                     = "0";
                    textBoxMontoPagado.Text               = "0";
                }
                //Si es 2, actualizar productos en nota de credito en progreso del cliente
                else if(accion == 2)
                {
                    buttonAccion.Text                     = "Actualizar";
                    dateTimePickerFechaCompromiso.Enabled = false;
                }
            }
            else
            {
                buttonAccion.Text = "Aplicar";

                labelMontoPagar.Visible   = true;
                textBoxMontoPagar.Visible = true;

                textBoxNCFolio.Enabled    = true;
                /*labelBusqueda.Visible     = true;
                textBoxBuscar.Visible     = true;*/

                //dataGridViewProductos.DataSource = dgvAgregarProductos.DataSource;
                dataGridViewProductos.Visible = false;

                //dataGridViewProductos.AutoResizeColumns();
                //dataGridViewProductos.Columns[dataGridViewProductos.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //labelSuma.Text = monto.ToString();
                labelSuma.Visible   = false;
                labelAnadir.Visible = false;
            }
        }

        private void textBoxNumCliente_Validated(object sender, EventArgs e)
        {
            if (numcliente)
            {
                if (textBoxNumCliente.TextLength == 0)
                {
                    textBoxNombre.Text = "";

                    textBoxNCFolio.Text = "";
                    textBoxNCFolio_Validated(sender, e);

                    dtNota.Clear();
                    dataGridViewNotas.DataSource = dtNota;
                }
                else
                {

                    textBoxNCFolio.Text = "";
                    textBoxNCFolio_Validated(sender, e);

                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();

                        try
                        {
                            SqlDataAdapter selectCliente = new SqlDataAdapter(new SqlCommand("SELECT nombre + ' ' + aPaterno + ' ' + aMaterno Nombre " +
                                                                                             "FROM   clientes " +
                                                                                             "WHERE  numcliente = " + textBoxNumCliente.Text, openCon, transaction));

                            DataTable dtCliente = new DataTable();
                            selectCliente.Fill(dtCliente);

                            //Para cualquier consulta, requerimos verficar si existe el cliente, si no, mandamos alerta
                            if (dtCliente.Rows.Count == 0)
                            {
                                MessageBox.Show("No existe el cliente seleccionado. Asegúrese de que esté añadido a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBoxNumCliente.Focus();
                                textBoxNumCliente.Text = "";
                                textBoxNombre.Text = "";
                            }
                            else
                            {

                                textBoxNombre.Text = dtCliente.Rows[0][0].ToString();

                                //Si la acción a realizar es diferente a 0 (agregar o actualizar y no consultar), realizamos las acciones correspondientes
                                if (accion != 0)
                                {
                                    SqlDataAdapter selectFolio = new SqlDataAdapter(new SqlCommand("SELECT ncfolio, fechaCompromiso " +
                                                                                                   "FROM   notas_credito " +
                                                                                                   "WHERE  numcliente   = " + textBoxNumCliente.Text + " " +
                                                                                                   "AND    status   NOT IN ('CA', 'CO')", openCon, transaction));

                                    DataTable dtFolioProg = new DataTable();
                                    selectFolio.Fill(dtFolioProg);

                                    if (accion == 1)
                                    {
                                        //Si ya tiene una nota en progreso, se cambia la ventana a modo actualizar con la nota obtenida
                                        if (dtFolioProg.Rows.Count > 0 && ((DateTime)dtFolioProg.Rows[0][1]).Date >= DateTime.Now.Date)
                                        {
                                            MessageBox.Show("El cliente seleccionado ya tiene una nota de crédito en progreso, se utilizará dicha nota con el folio N° " + dtFolioProg.Rows[0][0].ToString(), "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            accion = 2;
                                            loadAll();

                                            // textBoxNCFolio.Enabled = true;
                                            textBoxNCFolio.Text = dtFolioProg.Rows[0][0].ToString();
                                            //textBoxNCFolio.Focus();
                                            textBoxNCFolio_Validated(sender, e);

                                            //buttonAccion.Focus();
                                            //textBoxNCFolio.Enabled = false;
                                            textBoxTotal.Focus();
                                        }
                                        //Si la nota esta vencida, no procede a generarse una nueva, hasta saldar la que ya esta en progreso
                                        else if (dtFolioProg.Rows.Count > 0 && ((DateTime)dtFolioProg.Rows[0][1]).Date < DateTime.Now.Date)
                                        {
                                            MessageBox.Show("El cliente seleccionado ya tiene una nota de crédito en progreso con fecha de compromiso vencida, y no se puede crear una nueva hasta que haya sido liquidada. La nota tiene el folio N° " + dtFolioProg.Rows[0][0].ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                            accion = 0;
                                            loadAll();

                                            // textBoxNCFolio.Enabled = true;
                                            textBoxNCFolio.Text = dtFolioProg.Rows[0][0].ToString();
                                            //textBoxNCFolio.Focus();
                                            textBoxNCFolio_Validated(sender, e);

                                            //buttonAccion.Focus();
                                            //textBoxNCFolio.Enabled = false;
                                            buttonAccion.Focus();
                                        }
                                        //Ponemos la siguiente nota del cliente
                                        else
                                        {
                                            SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT IIF(MAX(ncfolio) IS NULL, 1, MAX(ncfolio) + 1) folioMax " +
                                                                                                      "FROM   notas_credito " +
                                                                                                      "WHERE  numcliente = " + textBoxNumCliente.Text, openCon, transaction));
                                            DataTable dtFolio = new DataTable();
                                            select.Fill(dtFolio);

                                            textBoxNCFolio.Text = dtFolio.Rows[0][0].ToString();
                                            dateTimePickerFechaAlta.Value = DateTime.Now;
                                            dateTimePickerFechaCompromiso.Value = DateTime.Now.AddDays(1);
                                        }
                                    }
                                    else if (accion == 2)
                                    {

                                        //Si no se consigue nota, no hay notas en progreso, por lo que se cambia la ventana a modo agregar
                                        if (dtFolioProg.Rows.Count == 0)
                                        {
                                            MessageBox.Show("El cliente seleccionado no tiene notas de crédito en progreso, se creará una nueva", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            accion = 1;
                                            loadAll();

                                            textBoxNumCliente_Validated(sender, e);

                                            textBoxTotal.Focus();
                                        }
                                        //Si la nota esta vencida, no procede a actualizarse la misma, hasta saldarla
                                        else if (dtFolioProg.Rows.Count > 0 && ((DateTime)dtFolioProg.Rows[0][1]).Date < DateTime.Now.Date)
                                        {
                                            MessageBox.Show("El cliente seleccionado ya tiene una nota de crédito en progreso con fecha de compromiso vencida. Debe liquidarse primero para crear una nueva. La nota tiene el folio N° " + dtFolioProg.Rows[0][0].ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            //buttonAccion.Enabled = false;
                                            accion = 0;
                                            loadAll();

                                            // textBoxNCFolio.Enabled = true;
                                            textBoxNCFolio.Text = dtFolioProg.Rows[0][0].ToString();
                                            //textBoxNCFolio.Focus();
                                            textBoxNCFolio_Validated(sender, e);

                                            //buttonAccion.Focus();
                                            //textBoxNCFolio.Enabled = false;
                                            buttonAccion.Focus();

                                        }
                                        //Se pone la nota para actualizar
                                        else
                                        {
                                            //buttonAccion.Enabled   = true;
                                            textBoxNCFolio.Text = dtFolioProg.Rows[0][0].ToString();
                                            //textBoxNCFolio.Enabled = true;
                                            //textBoxNCFolio.Focus();
                                            textBoxNCFolio_Validated(sender, e);

                                            buttonAccion.Focus();
                                            //textBoxNCFolio.Enabled = false;

                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT CONVERT (VARCHAR, n.ncfolio) Folio, " +
                                                                                                  "       CASE status " +
                                                                                                  "       WHEN 'AU' THEN 'Autorizado' " +
                                                                                                  "       WHEN 'PC' THEN 'Pagado Parcial' " +
                                                                                                  "       WHEN 'CO' THEN 'Cobrada' " +
                                                                                                  "       WHEN 'CA' THEN 'Cancelada' " +
                                                                                                  "       END AS Status, " +
                                                                                                  "       n.fechaAlta \"Fecha Alta\", n.fechaCompromiso \"Fecha Compromiso\" " +
                                                                                                  "FROM   notas_credito n, clientes c " +
                                                                                                  "WHERE  c.numcliente = n.numcliente " +
                                                                                                  "AND    c.numcliente = " + textBoxNumCliente.Text + " " +
                                                                                                  "ORDER BY n.ncfolio DESC", openCon, transaction));

                                        dtNota.Clear();
                                        select.Fill(dtNota);

                                        dataGridViewNotas.DataSource = dtNota;

                                        /*if (!textBoxBuscar.Text.Equals("") && dtNota.Rows.Count > 0)
                                        {
                                            dtNota.DefaultView.RowFilter = $"Folio Like '%{textBoxBuscar.Text}%'";
                                        }*/

                                        dataGridViewNotas.AutoResizeColumns();
                                        dataGridViewNotas.Columns[dataGridViewNotas.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                    }
                                    catch (SqlException)
                                    {
                                        MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                numcliente = false;
            }
        }

        private void textBoxNCFolio_Validated(object sender, EventArgs e)
        {
            
            if(textBoxNCFolio.TextLength > 0)
            {
                if (textBoxNumCliente.TextLength == 0)
                {
                    MessageBox.Show("Primero, seleccione un cliente", "Selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxNCFolio.Text = "";
                    textBoxNumCliente.Focus();
                }
                else
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();



                        try
                        {
                            SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT CASE status " +
                                                                                      "       WHEN 'AU' THEN 'Autorizado' " +
                                                                                      "       WHEN 'PC' THEN 'Pagado Parcial' " +
                                                                                      "       WHEN 'CO' THEN 'Cobrada' " +
                                                                                      "       WHEN 'CA' THEN 'Cancelada' " +
                                                                                      "       END AS status, " +
                                                                                      "       fechaAlta,  fechaCompromiso,  monto, " +
                                                                                      "       montoPagado " +
                                                                                      "FROM   notas_credito " +
                                                                                      "WHERE  numcliente = " + textBoxNumCliente.Text + " " +
                                                                                      "AND    ncfolio = " + textBoxNCFolio.Text, openCon, transaction));

                            DataTable dtNota = new DataTable();
                            select.Fill(dtNota);

                            if(dtNota.Rows.Count > 0)
                            {
                                textBoxStatus.Text                  = dtNota.Rows[0][0].ToString();
                                dateTimePickerFechaAlta.Value       = (DateTime)dtNota.Rows[0][1];
                                dateTimePickerFechaCompromiso.Value = (DateTime)dtNota.Rows[0][2];
                                textBoxTotal.Text                   = dtNota.Rows[0][3].ToString();
                                textBoxMontoPagado.Text             = dtNota.Rows[0][4].ToString();

                                buttonDetalle.Enabled = true;

                                if(accion == 0)
                                {
                                    if (dtNota.Rows[0][0].ToString().Equals("Cobrada") || dtNota.Rows[0][0].ToString().Equals("Cancelada"))
                                    {
                                        buttonCancelar.Enabled = false;
                                        buttonAccion.Enabled = false;
                                    }
                                    else
                                    {
                                        if(dtNota.Rows[0][0].ToString().Equals("Pagado Parcial"))
                                        {
                                            buttonCancelar.Enabled = false;
                                        }
                                        else
                                        {
                                            buttonCancelar.Enabled = true;
                                        }
                                        
                                        buttonAccion.Enabled = true;
                                    }
                                } 
                            }
                            else
                            {
                                MessageBox.Show("La nota de crédito consultada no existe, favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                buttonAccion.Enabled                = false;
                                buttonCancelar.Enabled              = false;
                                buttonDetalle.Enabled               = false;

                                textBoxMontoPagado.Text             = "";
                                textBoxStatus.Text                  = "No existe";
                                textBoxTotal.Text                   = "";

                                textBoxNCFolio.Focus();

                                //dateTimePickerFechaAlta.Value       = DateTime.MinDate;
                                //dateTimePickerFechaCompromiso.Value = DateTime.MinDate;
                            }
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                textBoxStatus.Text = "Pendiente";
                textBoxTotal.Text = "";
                textBoxMontoPagado.Text = "";
                dateTimePickerFechaAlta.Value = DateTime.Parse("01/01/3000");
                dateTimePickerFechaCompromiso.Value = DateTime.Parse("01/01/3000");
            }
        }

        private void buttonAccion_Click(object sender, EventArgs e)
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {
                    if (textBoxNumCliente.TextLength == 0 || textBoxNCFolio.TextLength == 0)
                    {
                        MessageBox.Show("Debe llenar todos los campos requeridos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (buttonAccion.Text.Equals("Agregar"))
                        {
                            if (dateTimePickerFechaCompromiso.Value <= dateTimePickerFechaAlta.Value)
                            {
                                MessageBox.Show("La fecha compromiso debe ser mayor a la fecha de alta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                if(MessageBox.Show("¿Desea agregar la nota de crédito N° " + textBoxNCFolio.Text + " para el cliente " + textBoxNombre.Text + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    using (SqlCommand queryAdd = new SqlCommand("INSERT INTO notas_credito (numcliente, ncfolio,         status, " +
                                                                                "                           fechaAlta,  fechaCompromiso, monto, " +
                                                                                "                           montoPagado) " +
                                                                                "VALUES                    (@numcliente, @ncfolio,       @status, " +
                                                                                "                           @fechaAlta, @fechaCompromiso, @monto, " +
                                                                                "                           @montoPagado)", openCon , transaction ))
                                    {
                                        queryAdd.Parameters.Add("@numcliente",      SqlDbType.Int).Value      = textBoxNumCliente.Text;
                                        queryAdd.Parameters.Add("@ncfolio",         SqlDbType.Int).Value      = textBoxNCFolio.Text;
                                        queryAdd.Parameters.Add("@status",          SqlDbType.VarChar).Value  = "AU";
                                        queryAdd.Parameters.Add("@fechaAlta",       SqlDbType.DateTime).Value = dateTimePickerFechaAlta.Value;
                                        queryAdd.Parameters.Add("@fechaCompromiso", SqlDbType.Date).Value     = dateTimePickerFechaCompromiso.Value;
                                        queryAdd.Parameters.Add("@monto",           SqlDbType.Decimal).Value  = labelSuma.Text;
                                        queryAdd.Parameters.Add("@montoPagado",     SqlDbType.Decimal).Value  = 0;



                                        queryAdd.ExecuteNonQuery();
                                    }

                                    for (int i = 0; i < dataGridViewProductos.RowCount; i++)
                                    {
                                        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT IIF(MAX(detalle) IS NULL, 1, MAX(detalle) + 1) detalleMax " +
                                                                                                  "FROM   registro_notas_credito " +
                                                                                                  "WHERE  numcliente = " + textBoxNumCliente.Text + " " +
                                                                                                  "AND    ncfolio    = " + textBoxNCFolio.Text, openCon, transaction));

                                        DataTable dtDetalle = new DataTable();
                                        select.Fill(dtDetalle);

                                        using (SqlCommand queryAdd = new SqlCommand("INSERT INTO registro_notas_credito (numcliente,   ncfolio,       detalle, " +
                                                                                    "                                    id_producto,  cantidad,      precio, " +
                                                                                    "                                    importe,      fechaSurtido,  descuento) " +
                                                                                    "VALUES                             (@numcliente,  @ncfolio,      @detalle, " +
                                                                                    "                                    @id_producto, @cantidad,     @precio, " +
                                                                                    "                                    @importe,     @fechaSurtido, @descuento)", openCon, transaction))
                                        {
                                            queryAdd.Parameters.Add("@numcliente",   SqlDbType.Int).Value      = textBoxNumCliente.Text;
                                            queryAdd.Parameters.Add("@ncfolio",      SqlDbType.Int).Value      = textBoxNCFolio.Text;
                                            queryAdd.Parameters.Add("@detalle",      SqlDbType.Int).Value      = dtDetalle.Rows[0][0].ToString();
                                            queryAdd.Parameters.Add("@id_producto",  SqlDbType.VarChar).Value  = dataGridViewProductos.Rows[i].Cells[0].Value.ToString();
                                            queryAdd.Parameters.Add("@cantidad",     SqlDbType.Float).Value    = dataGridViewProductos.Rows[i].Cells[3].Value.ToString();
                                            queryAdd.Parameters.Add("@precio",       SqlDbType.Decimal).Value  = dataGridViewProductos.Rows[i].Cells[2].Value.ToString();
                                            queryAdd.Parameters.Add("@importe",      SqlDbType.Decimal).Value  = dataGridViewProductos.Rows[i].Cells[4].Value.ToString();
                                            queryAdd.Parameters.Add("@fechaSurtido", SqlDbType.DateTime).Value = DateTime.Now;

                                            if (dataGridViewProductos.Rows[i].Cells[2].Style.ForeColor == Color.Green)
                                            {
                                                queryAdd.Parameters.Add("@descuento", SqlDbType.Int).Value = 1;
                                            }

                                            else
                                            {
                                                queryAdd.Parameters.Add("@descuento", SqlDbType.Int).Value = 0;
                                            }


                                            queryAdd.ExecuteNonQuery();
                                        }

                                        using (SqlCommand queryAdd = new SqlCommand("UPDATE productos " +
                                                                                    "SET    cantidad    = cantidad - @cantidad " +
                                                                                    "WHERE  id_producto = @id_producto", openCon, transaction))
                                        {
                                            queryAdd.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = dataGridViewProductos.Rows[i].Cells[0].Value.ToString();
                                            queryAdd.Parameters.Add("@cantidad",    SqlDbType.Float).Value = dataGridViewProductos.Rows[i].Cells[3].Value.ToString();

                                            queryAdd.ExecuteNonQuery();
                                        }
                                    
                                    }

                                    transaction.Commit();
                                    MessageBox.Show("Nota agregada con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    action = true;
                                    Dispose();
                                }
                            }
                        }
                        else if (buttonAccion.Text.Equals("Actualizar"))
                        {
                            if (dateTimePickerFechaCompromiso.Value <= dateTimePickerFechaAlta.Value)
                            {
                                MessageBox.Show("La fecha compromiso debe ser mayor a la fecha de alta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                if (MessageBox.Show("¿Desea actualizar la nota de crédito N° " + textBoxNCFolio.Text + " para el cliente " + textBoxNombre.Text + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    using (SqlCommand queryAdd = new SqlCommand("UPDATE notas_credito " +
                                                                                "SET    monto = monto + @monto " +
                                                                                "WHERE  numcliente = @numcliente " +
                                                                                "AND    ncfolio = @ncfolio", openCon, transaction))
                                    {
                                        queryAdd.Parameters.Add("@numcliente", SqlDbType.Int).Value = textBoxNumCliente.Text;
                                        queryAdd.Parameters.Add("@ncfolio",    SqlDbType.Int).Value = textBoxNCFolio.Text;
                                        queryAdd.Parameters.Add("@monto",      SqlDbType.Decimal).Value = labelSuma.Text;


                                        queryAdd.ExecuteNonQuery();
                                    }

                                    for (int i = 0; i < dataGridViewProductos.RowCount; i++)
                                    {
                                        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT IIF(MAX(detalle) IS NULL, 1, MAX(detalle) + 1) detalleMax " +
                                                                                                  "FROM   registro_notas_credito " +
                                                                                                  "WHERE  numcliente = " + textBoxNumCliente.Text + " " +
                                                                                                  "AND    ncfolio = " + textBoxNCFolio.Text, openCon, transaction));

                                        DataTable dtDetalle = new DataTable();
                                        select.Fill(dtDetalle);

                                        using (SqlCommand queryAdd = new SqlCommand("INSERT INTO registro_notas_credito (numcliente,   ncfolio,       detalle, " +
                                                                                    "                                    id_producto,  cantidad,      precio, " +
                                                                                    "                                    importe,      fechaSurtido,  descuento) " +
                                                                                    "VALUES                             (@numcliente,  @ncfolio,      @detalle, " +
                                                                                    "                                    @id_producto, @cantidad,     @precio, " +
                                                                                    "                                    @importe,     @fechaSurtido, @descuento)", openCon, transaction))
                                        {
                                            queryAdd.Parameters.Add("@numcliente",   SqlDbType.Int).Value      = textBoxNumCliente.Text;
                                            queryAdd.Parameters.Add("@ncfolio",      SqlDbType.Int).Value      = textBoxNCFolio.Text;
                                            queryAdd.Parameters.Add("@detalle",      SqlDbType.Int).Value      = dtDetalle.Rows[0][0].ToString();
                                            queryAdd.Parameters.Add("@id_producto",  SqlDbType.VarChar).Value  = dataGridViewProductos.Rows[i].Cells[0].Value.ToString();
                                            queryAdd.Parameters.Add("@cantidad",     SqlDbType.Float).Value    = dataGridViewProductos.Rows[i].Cells[3].Value.ToString();
                                            queryAdd.Parameters.Add("@precio",       SqlDbType.Decimal).Value  = dataGridViewProductos.Rows[i].Cells[2].Value.ToString();
                                            queryAdd.Parameters.Add("@importe",      SqlDbType.Decimal).Value  = dataGridViewProductos.Rows[i].Cells[4].Value.ToString();
                                            queryAdd.Parameters.Add("@fechaSurtido", SqlDbType.DateTime).Value = DateTime.Now;

                                            if (dataGridViewProductos.Rows[i].Cells[2].Style.ForeColor == Color.Green)
                                            {
                                                queryAdd.Parameters.Add("@descuento", SqlDbType.Int).Value = 1;
                                            }

                                            else
                                            {
                                                queryAdd.Parameters.Add("@descuento", SqlDbType.Int).Value = 0;
                                            }


                                            queryAdd.ExecuteNonQuery();
                                        }

                                        using (SqlCommand queryAdd = new SqlCommand("UPDATE productos " +
                                                                                    "SET    cantidad    = cantidad - @cantidad " +
                                                                                    "WHERE  id_producto = @id_producto", openCon, transaction))
                                        {
                                            queryAdd.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = dataGridViewProductos.Rows[i].Cells[0].Value.ToString();
                                            queryAdd.Parameters.Add("@cantidad",    SqlDbType.Float).Value   = dataGridViewProductos.Rows[i].Cells[3].Value.ToString();

                                            queryAdd.ExecuteNonQuery();
                                        }

                                    }

                                    transaction.Commit();
                                    MessageBox.Show("Nota actualizada con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    action = true;
                                    Dispose();
                                }
                            }

                        }

                        else if (buttonAccion.Text.Equals("Aplicar"))
                        {
                            if (textBoxMontoPagar.TextLength == 0)
                            {
                                MessageBox.Show("Ingrese el monto a pagar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            else if (double.Parse(textBoxMontoPagar.Text) <= 0)
                            {
                                MessageBox.Show("El monto a pagar no puede ser cero o menor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            else //if (double.Parse(textBoxTotal.Text) - double.Parse(textBoxMontoPagado.Text) - double.Parse(textBoxMontoPagar.Text) > 0 || double.Parse(textBoxMontoPagado.Text) != 0)
                            {
                                // Para mejorar la lectura de código, creo la variable monto para expresar si el monto total, menos el ya pagado, menos el que se paga actualmente, cubre el gasto de la nota de credito o no
                                double monto = double.Parse(textBoxTotal.Text) - double.Parse(textBoxMontoPagado.Text) - double.Parse(textBoxMontoPagar.Text);

                                // Definimos el mensaje que se mostrará, dependiendo si se cubre el pago total de la nota o no
                                string mensaje = monto > 0 ? "¿Desea abonar a la nota de crédito N° " + textBoxNCFolio.Text + " para el cliente " + textBoxNombre.Text + "? \n\n - Monto: $" + textBoxTotal.Text +
                                                              "\n - Monto Pagado: $" + textBoxMontoPagado.Text + "\n - Monto a Pagar: $" + decimal.Round(decimal.Parse(textBoxMontoPagar.Text) + 0.00M, 2) + (monto >= 0 ? "" : "\n - Cambio: $" + decimal.Round(decimal.Parse(Math.Abs(double.Parse(textBoxTotal.Text) - double.Parse(textBoxMontoPagado.Text) - double.Parse(textBoxMontoPagar.Text)).ToString()) + 0.00M, 2))
                                                             :"¿Desea pagar la nota de crédito N° " + textBoxNCFolio.Text + " para el cliente " + textBoxNombre.Text + "? \n\n - Monto: $" + textBoxTotal.Text +
                                                              "\n - Monto Pagado: $" + textBoxMontoPagado.Text + "\n - Monto a Pagar: $" + decimal.Round(decimal.Parse(textBoxMontoPagar.Text) + 0.00M, 2) + (monto >= 0 ? "" : "\n - Cambio: $" + decimal.Round(decimal.Parse(Math.Abs(double.Parse(textBoxTotal.Text) - double.Parse(textBoxMontoPagado.Text) - double.Parse(textBoxMontoPagar.Text)).ToString()) + 0.00M, 2));
                                
                                if (MessageBox.Show(mensaje, "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                                    DataTable dtPagos = new DataTable();
                                    select.Fill(dtPagos);

                                    using (SqlCommand queryAdd = new SqlCommand("INSERT INTO registro_ventas (id_venta,  id_producto,  precio, " +
                                                                                "                             cantidad,  descuento,    numcliente, " +
                                                                                "                             ncfolio) " +
                                                                                "VALUES                      (@id_venta, @id_producto, @precio, " +
                                                                                "                             @cantidad, @descuento,   @numcliente, " +
                                                                                "                             @ncfolio)", openCon, transaction))
                                    {
                                        queryAdd.Parameters.Add("@id_venta",    SqlDbType.Int).Value     = dtPagos.Rows[0][0].ToString();
                                        queryAdd.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = (monto > 0 || double.Parse(textBoxMontoPagado.Text) != 0 ? "NC02" : "NC01");
                                        queryAdd.Parameters.Add("@precio",      SqlDbType.Decimal).Value = (monto > 0 ? double.Parse(textBoxMontoPagar.Text) : double.Parse(textBoxTotal.Text) - double.Parse(textBoxMontoPagado.Text));
                                        queryAdd.Parameters.Add("@cantidad",    SqlDbType.Float).Value   = 1;
                                        queryAdd.Parameters.Add("@descuento",   SqlDbType.Int).Value     = 0;
                                        queryAdd.Parameters.Add("@numcliente",  SqlDbType.Int).Value     = textBoxNumCliente.Text;
                                        queryAdd.Parameters.Add("@ncfolio",     SqlDbType.Int).Value     = textBoxNCFolio.Text;


                                        queryAdd.ExecuteNonQuery();

                                    }

                                    using (SqlCommand queryAdd = new SqlCommand("UPDATE notas_credito " +
                                                                                "SET    status      = @status, " +
                                                                                "       montoPagado = montoPagado + @montoPagado " +
                                                                                "WHERE  numcliente  = @numcliente " +
                                                                                "AND    ncfolio     = @ncfolio ", openCon, transaction))
                                    {
                                        queryAdd.Parameters.Add("@numcliente",  SqlDbType.Int).Value     = textBoxNumCliente.Text;
                                        queryAdd.Parameters.Add("@ncfolio",     SqlDbType.Int).Value     = textBoxNCFolio.Text;
                                        queryAdd.Parameters.Add("@status",      SqlDbType.VarChar).Value = (monto > 0 ? "PC" : "CO");
                                        queryAdd.Parameters.Add("@montoPagado", SqlDbType.Decimal).Value = (monto > 0 ? double.Parse(textBoxMontoPagar.Text) : double.Parse(textBoxTotal.Text) - double.Parse(textBoxMontoPagado.Text));



                                        queryAdd.ExecuteNonQuery();
                                    }

                                    select = new SqlDataAdapter(new SqlCommand("SELECT IIF(MAX(pago) IS NULL, 1, MAX(pago) + 1) pagoMax " +
                                                                               "FROM   pagos_notas_credito " +
                                                                               "WHERE  numcliente = " + textBoxNumCliente.Text + " " +
                                                                               "AND    ncfolio    = " + textBoxNCFolio.Text, openCon, transaction));

                                    DataTable dtPagoxCliente = new DataTable();
                                    select.Fill(dtPagoxCliente);

                                    using (SqlCommand queryAdd = new SqlCommand("INSERT INTO pagos_notas_credito (numcliente,  ncfolio,  pago,  importe) " +
                                                                                "VALUES                          (@numcliente, @ncfolio, @pago, @importe)", openCon, transaction))
                                    {
                                        queryAdd.Parameters.Add("@numcliente", SqlDbType.Int).Value     = textBoxNumCliente.Text;
                                        queryAdd.Parameters.Add("@ncfolio",    SqlDbType.Int).Value     = textBoxNCFolio.Text;
                                        queryAdd.Parameters.Add("@pago",       SqlDbType.Int).Value     = dtPagoxCliente.Rows[0][0].ToString();
                                        queryAdd.Parameters.Add("@importe",    SqlDbType.Decimal).Value = (monto > 0 ? double.Parse(textBoxMontoPagar.Text) : double.Parse(textBoxTotal.Text) - double.Parse(textBoxMontoPagado.Text));



                                        queryAdd.ExecuteNonQuery();
                                    }

                                    transaction.Commit();
                                    MessageBox.Show("Pago realizado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    textBoxMontoPagar.Text = "";
                                    loadAll();
                                    string nota = textBoxNCFolio.Text;
                                    numcliente = true;
                                    textBoxNumCliente_Validated(sender, e);
                                    textBoxNCFolio.Text = nota;
                                    textBoxNCFolio_Validated(sender, e);
                                }
                            }
                        }                          
                    }
                }

                catch (SqlException)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                }
            }
        }

        private void buttonDetalle_Click(object sender, EventArgs e)
        {
            if (textBoxNCFolio.TextLength > 0 && textBoxNumCliente.TextLength > 0)
            {
                new Registro_Nota_Credito(textBoxNumCliente.Text, textBoxNombre.Text, textBoxNCFolio.Text, 0).ShowDialog();
            }
        }

        /*private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtNota.Rows.Count > 0)
                {
                    dtNota.DefaultView.RowFilter = $"Folio Like '%{textBoxBuscar.Text}%'";
                    dataGridViewNotas.AutoResizeColumns();
                    dataGridViewNotas.Columns[dataGridViewNotas.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

            }
            catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo "  +ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (EvaluateException)
            {
                MessageBox.Show("Caractér no permitido para búsqueda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBuscar.Text = textBoxBuscar.Text.Substring(0, textBoxBuscar.TextLength - 1);
                textBoxBuscar.SelectionStart = textBoxBuscar.TextLength;
            }
        }*/

        private void textBoxMontoPagar_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxMontoPagar.Text.Contains("."))
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

        private void dataGridViewNotas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridViewNotas.RowCount > 0)
            {
                textBoxNCFolio.Text = dataGridViewNotas.Rows[e.RowIndex].Cells[0].Value.ToString();
                //textBoxNumCliente_Validated(sender, e);
                textBoxNCFolio_Validated(sender, e);
            }           
        }

        private void dataGridViewNotas_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridViewNotas.RowCount > 0 && e.KeyCode == Keys.Enter)
            {
                textBoxNCFolio.Text = dataGridViewNotas.Rows[dataGridViewNotas.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                //textBoxNumCliente_Validated(sender, e);
                textBoxNCFolio_Validated(sender, e);
            }
        }

        private void buttonPagos_Click(object sender, EventArgs e)
        {
            if (textBoxNCFolio.TextLength > 0 && textBoxNumCliente.TextLength > 0)
            {
                new Registro_Nota_Credito(textBoxNumCliente.Text, textBoxNombre.Text, textBoxNCFolio.Text, 1).ShowDialog();
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {

        }

        private void textBoxNumCliente_TextChanged(object sender, EventArgs e)
        {
            numcliente = true;
        }
    }
}
