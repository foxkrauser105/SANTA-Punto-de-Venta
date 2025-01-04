using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Clientes : Form
    {
        DataTable dtClientes = new DataTable();
        string usuclave;

        //Variable que permitira saber el ultimo folio + 1 agregado a la base de datos
        int folioMax;

        public Clientes(string usuclave)
        {
            InitializeComponent();
            this.usuclave = usuclave;
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            loadAll();
        }

        public void loadAll()
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                limpiar();
                buttonAccion.Text = "Añadir";
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {
                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT CONVERT(VARCHAR(100), numCliente) Cliente, nombre + ' ' + aPaterno Nombre, calle Calle, " +
                                                                              "       colonia Colonia,                           telefono Teléfono " +
                                                                              "FROM   clientes", openCon, transaction));

                    dtClientes.Clear();
                    select.Fill(dtClientes);
                    dataGridViewClientes.DataSource = dtClientes;

                    if (dataGridViewClientes.RowCount > 0)
                    {
                        dtClientes.DefaultView.RowFilter = $"Cliente LIKE '%{textBoxBuscar.Text}%' OR Nombre LIKE '%{textBoxBuscar.Text}%'";
                        dataGridViewClientes.AutoResizeColumns();
                        dataGridViewClientes.Columns[dataGridViewClientes.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    SqlDataAdapter selectFolio = new SqlDataAdapter(new SqlCommand("SELECT IIF(MAX(numCliente) IS NULL, 1, MAX(numCliente) + 1) folioMax " +
                                                                                   "FROM   clientes", openCon, transaction));

                    DataTable dtFolio = new DataTable();
                    selectFolio.Fill(dtFolio);

                    folioMax = int.Parse(dtFolio.Rows[0][0].ToString());

                    textBoxCliente.Text = folioMax.ToString();

                }
                catch (SqlException)
                {
                    MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonAccion_Click(object sender, EventArgs e)
        {
            if (buttonAccion.Text.Equals("Añadir"))
            {
                if (textBoxNombre.TextLength == 0 || textBoxAPaterno.TextLength == 0 || textBoxCalle.TextLength == 0 || textBoxNumExt.TextLength == 0 || textBoxColonia.TextLength == 0)
                {
                    MessageBox.Show("Debe capturar todos los campos requeridos, excepto apellido materno o número interno", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (maskedTextBoxTelefono.TextLength < 10)
                {
                    MessageBox.Show("Debe capturar el número de teléfono con al menos 10 dígitos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("¿Está seguro de añadir al cliente a la base de datos?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                        {
                            openCon.Open();
                            SqlTransaction transaction = openCon.BeginTransaction();

                            try
                            {
                                SqlCommand queryAdd = new SqlCommand("INSERT INTO clientes (numCliente, nombre,  aPaterno, " +
                                                                     "                      aMaterno,   calle,   numeroExt, " +
                                                                     "                      numeroInt,  colonia, telefono, " +
                                                                     "                      usuclaveUltAct) " +
                                                                     "VALUES               (@numCliente, @nombre,  @aPaterno, " +
                                                                     "                      @aMaterno,   @calle,   @numeroExt, " +
                                                                     "                      @numeroInt,  @colonia, @telefono, " +
                                                                     "                      @usuclaveUltAct)", openCon, transaction);

                                queryAdd.Parameters.Add("@numCliente", SqlDbType.Int).Value = textBoxCliente.Text.Trim();
                                queryAdd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBoxNombre.Text.Trim();
                                queryAdd.Parameters.Add("@aPaterno", SqlDbType.VarChar).Value = textBoxAPaterno.Text.Trim();
                                queryAdd.Parameters.Add("@aMaterno", SqlDbType.VarChar).Value = textBoxAMaterno.Text.Trim();
                                queryAdd.Parameters.Add("@calle", SqlDbType.VarChar).Value = textBoxCalle.Text.Trim();
                                queryAdd.Parameters.Add("@numeroExt", SqlDbType.Int).Value = textBoxNumExt.Text.Trim();
                                queryAdd.Parameters.Add("@numeroInt", SqlDbType.Int).Value = (textBoxNumInt.TextLength > 0 ? int.Parse(textBoxNumInt.Text.Trim()) : 0);
                                queryAdd.Parameters.Add("@colonia", SqlDbType.VarChar).Value = textBoxColonia.Text.Trim();
                                queryAdd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = maskedTextBoxTelefono.Text.Trim();
                                queryAdd.Parameters.Add("@usuclaveUltAct", SqlDbType.VarChar).Value = usuclave.Trim();

                                queryAdd.ExecuteNonQuery();

                                transaction.Commit();

                                MessageBox.Show("Se añadió al cliente con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                textBoxCliente_Validated(sender, e);
                                loadAll();

                            }
                            catch (SqlException ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else if (buttonAccion.Text.Equals("Actualizar"))
            {
                if (textBoxNombre.TextLength == 0 || textBoxAPaterno.TextLength == 0 || textBoxCalle.TextLength == 0 || textBoxNumExt.TextLength == 0 || textBoxColonia.TextLength == 0)
                {
                    MessageBox.Show("Debe capturar todos los campos requeridos, excepto apellido materno o número interno", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (maskedTextBoxTelefono.TextLength < 10)
                {
                    MessageBox.Show("Debe capturar el número de teléfono con al menos 10 dígitos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("¿Está seguro de actualizar los datos del cliente?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                        {
                            openCon.Open();
                            SqlTransaction transaction = openCon.BeginTransaction();

                            try
                            {
                                SqlCommand queryUpdate = new SqlCommand("UPDATE clientes " +
                                                                        "SET    nombre         = @nombre, " +
                                                                        "       aPaterno       = @aPaterno, " +
                                                                        "       aMaterno       = @aMaterno, " +
                                                                        "       calle          = @calle, " +
                                                                        "       numeroExt      = @numeroExt, " +
                                                                        "       numeroInt      = @numeroInt, " +
                                                                        "       colonia        = @colonia, " +
                                                                        "       telefono       = @telefono, " +
                                                                        "       usuclaveUltAct = @usuclaveUltAct " +
                                                                        "WHERE  numCliente     = @numCliente", openCon, transaction);

                                queryUpdate.Parameters.Add("@numCliente", SqlDbType.Int).Value = textBoxCliente.Text.Trim();
                                queryUpdate.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBoxNombre.Text.Trim();
                                queryUpdate.Parameters.Add("@aPaterno", SqlDbType.VarChar).Value = textBoxAPaterno.Text.Trim();
                                queryUpdate.Parameters.Add("@aMaterno", SqlDbType.VarChar).Value = textBoxAMaterno.Text.Trim();
                                queryUpdate.Parameters.Add("@calle", SqlDbType.VarChar).Value = textBoxCalle.Text.Trim();
                                queryUpdate.Parameters.Add("@numeroExt", SqlDbType.Int).Value = textBoxNumExt.Text.Trim();
                                queryUpdate.Parameters.Add("@numeroInt", SqlDbType.Int).Value = (textBoxNumInt.TextLength > 0 ? int.Parse(textBoxNumInt.Text.Trim()) : 0);
                                queryUpdate.Parameters.Add("@colonia", SqlDbType.VarChar).Value = textBoxColonia.Text.Trim();
                                queryUpdate.Parameters.Add("@telefono", SqlDbType.VarChar).Value = maskedTextBoxTelefono.Text.Trim();
                                queryUpdate.Parameters.Add("@usuclaveUltAct", SqlDbType.VarChar).Value = usuclave.Trim();

                                queryUpdate.ExecuteNonQuery();

                                transaction.Commit();

                                MessageBox.Show("Se actualizó al cliente con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                textBoxCliente_Validated(sender, e);
                                loadAll();

                            }
                            catch (SqlException ex)
                            {
                                transaction.Rollback();
                                if (ex.Number == 2627)
                                {
                                    MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica el número de cliente, ya que existe un duplicado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void limpiar()
        {
            textBoxAMaterno.Text = textBoxAPaterno.Text = textBoxCalle.Text = textBoxColonia.Text = textBoxNombre.Text = textBoxNumExt.Text = textBoxNumInt.Text =
                textBoxUsuclaveUltAct.Text = maskedTextBoxTelefono.Text = "";
        }

        private void textBoxCliente_Validated(object sender, EventArgs e)
        {
            if (int.Parse(textBoxCliente.Text) < 1 || int.Parse(textBoxCliente.Text) > folioMax || int.Parse(textBoxCliente.Text) == folioMax)
            {
                textBoxCliente.Text = folioMax.ToString();
                limpiar();
                buttonAccion.Text = "Añadir";
            }
            else
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();

                    try
                    {
                        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT nombre,  aPaterno,  aMaterno, " +
                                                                                  "       calle,   numeroExt, numeroInt, " +
                                                                                  "       colonia, telefono,  usuclaveUltAct " +
                                                                                  "FROM   clientes " +
                                                                                  "WHERE  numCliente = " + textBoxCliente.Text, openCon, transaction));

                        DataTable dtCliente = new DataTable();
                        select.Fill(dtCliente);

                        textBoxNombre.Text = dtCliente.Rows[0][0].ToString();
                        textBoxAPaterno.Text = dtCliente.Rows[0][1].ToString();
                        textBoxAMaterno.Text = dtCliente.Rows[0][2].ToString();
                        textBoxCalle.Text = dtCliente.Rows[0][3].ToString();
                        textBoxNumExt.Text = dtCliente.Rows[0][4].ToString();
                        textBoxNumInt.Text = dtCliente.Rows[0][5].ToString();
                        textBoxColonia.Text = dtCliente.Rows[0][6].ToString();
                        maskedTextBoxTelefono.Text = dtCliente.Rows[0][7].ToString();
                        textBoxUsuclaveUltAct.Text = dtCliente.Rows[0][8].ToString();

                        buttonAccion.Text = "Actualizar";

                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiar();
                    }
                }
            }
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtClientes.DefaultView.RowFilter = $"Cliente LIKE '%{textBoxBuscar.Text}%' OR Nombre LIKE '%{textBoxBuscar.Text}%'";
                dataGridViewClientes.AutoResizeColumns();
                dataGridViewClientes.Columns[dataGridViewClientes.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (EvaluateException)
            {
                MessageBox.Show("Caracter no permitido para búsqueda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBuscar.Text = textBoxBuscar.Text.Substring(0, textBoxBuscar.TextLength - 1);
                textBoxBuscar.SelectionStart = textBoxBuscar.TextLength;
            }
        }

        private void dataGridViewClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewClientes.RowCount > 0)
            {
                textBoxCliente.Text = dataGridViewClientes.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxCliente_Validated(sender, e);
            }
        }

        private void dataGridViewClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dataGridViewClientes.RowCount > 0)
            {
                textBoxCliente.Text = dataGridViewClientes.Rows[dataGridViewClientes.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                textBoxCliente_Validated(sender, e);
            }
        }
    }
}
