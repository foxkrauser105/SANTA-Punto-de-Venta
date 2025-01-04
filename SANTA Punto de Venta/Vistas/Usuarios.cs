using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Usuarios : Form
    {
        bool admin;
        DataTable dtUsuarios = new DataTable();
        public Usuarios(bool admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            loadAll();
            textBoxUsuclave.Focus();
            buttonAltaBaja.Enabled = false;
            buttonPass.Enabled = false;
        }

        public void loadAll()
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {
                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT usuclave Clave, nombre + ' ' + aPaterno + ' ' + aMaterno Nombre, " +
                                                                              "       fechaAlta \"Fecha Alta\",   IIF(status = 1, 'Activo', 'Inactivo') Estatus " +
                                                                              "FROM   usuarios", openCon, transaction));
                    dtUsuarios.Clear();
                    select.Fill(dtUsuarios);

                    dataGridViewUsuarios.DataSource = dtUsuarios;

                    dtUsuarios.DefaultView.RowFilter = $"Clave LIKE '%{textBoxBuscar.Text}%' OR Nombre Like '%{textBoxBuscar.Text}%'";
                    dataGridViewUsuarios.AutoResizeColumns();
                    dataGridViewUsuarios.Columns[dataGridViewUsuarios.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    if (dataGridViewUsuarios.RowCount > 0)
                    {

                        for (int i = 0; i < dataGridViewUsuarios.RowCount; i++)
                        {
                            if (dataGridViewUsuarios.Rows[i].Cells[dataGridViewUsuarios.ColumnCount - 1].Value.ToString().Equals("Activo"))
                            {
                                dataGridViewUsuarios.Rows[i].Cells[dataGridViewUsuarios.ColumnCount - 1].Style.ForeColor = Color.Green;
                            }
                            else
                            {
                                dataGridViewUsuarios.Rows[i].Cells[dataGridViewUsuarios.ColumnCount - 1].Style.ForeColor = Color.Red;
                            }
                        }

                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (!admin)
            {
                textBoxAMaterno.Enabled = false;
                textBoxAPaterno.Enabled = false;
                textBoxNombre.Enabled = false;
                maskedTextBoxTelefono.Enabled = false;
                buttonAccion.Enabled = false;
            }
        }

        private void textBoxUsuclave_Validated(object sender, EventArgs e)
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {
                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT usuclave Clave, nombre,   aPaterno,      aMaterno, " +
                                                                              "       fechaAlta FechaAlta,      IIF(status = 1, 'Activo', 'Inactivo') Estatus, " +
                                                                              "       fechaUltAct fechaUltima,  telefono " +
                                                                              "FROM   usuarios " +
                                                                              "WHERE  usuclave = '" + textBoxUsuclave.Text + "'", openCon, transaction));

                    DataTable dtUsuarioEsp = new DataTable();
                    select.Fill(dtUsuarioEsp);

                    if (dtUsuarioEsp.Rows.Count > 0)
                    {
                        textBoxNombre.Text = dtUsuarioEsp.Rows[0][1].ToString();
                        textBoxAPaterno.Text = dtUsuarioEsp.Rows[0][2].ToString();
                        textBoxAMaterno.Text = dtUsuarioEsp.Rows[0][3].ToString();
                        dateTimePickerFechaAlta.Value = (DateTime)dtUsuarioEsp.Rows[0][4];
                        textBoxStatus.Text = dtUsuarioEsp.Rows[0][5].ToString();
                        dateTimePickerFechaUltAct.Value = (DateTime)dtUsuarioEsp.Rows[0][6];
                        maskedTextBoxTelefono.Text = dtUsuarioEsp.Rows[0][7].ToString();

                        buttonAccion.Text = "Actualizar";

                        if (admin)
                            buttonAltaBaja.Enabled = true;

                        buttonPass.Enabled = true;

                        if (dtUsuarioEsp.Rows[0][5].ToString().Equals("Activo"))
                        {
                            buttonAltaBaja.Image = Image.FromFile(@"..\..\Resources\Actions-window-close-icon.png");
                            textBoxStatus.ForeColor = Color.Green;
                        }
                        else
                        {
                            buttonAltaBaja.Image = Image.FromFile(@"..\..\Resources\add.png");
                            textBoxStatus.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        textBoxNombre.Text = "";
                        textBoxAPaterno.Text = "";
                        textBoxAMaterno.Text = "";
                        dateTimePickerFechaAlta.Value = DateTime.Now;
                        textBoxStatus.Text = "Activo";
                        dateTimePickerFechaUltAct.Value = DateTime.Now;
                        maskedTextBoxTelefono.Text = "";

                        buttonAccion.Text = "Añadir";

                        buttonAltaBaja.Enabled = false;
                        buttonPass.Enabled = false;

                        buttonAltaBaja.Image = Image.FromFile(@"..\..\Resources\Actions-window-close-icon.png");
                        textBoxStatus.ForeColor = Color.Green;

                    }

                }
                catch (SqlException)
                {
                    MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonAccion_Click(object sender, EventArgs e)
        {
            if (buttonAccion.Text.Equals("Añadir") && admin)
            {

                if (textBoxNombre.Text.Equals("") || textBoxAPaterno.Text.Equals("") || textBoxNombre.Text.Equals(""))
                {
                    MessageBox.Show("Debe capturar todos los campos requeridos, excepto apellido materno en caso de no aplicar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (maskedTextBoxTelefono.TextLength < 10)
                {
                    MessageBox.Show("Debe capturar el número de teléfono con al menos 10 dígitos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();

                        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT COUNT(*) " +
                                                                                  "FROM   usuarios " +
                                                                                  "WHERE  usuclave = '" + textBoxUsuclave.Text + "'", openCon, transaction));

                        DataTable dtConteo = new DataTable();
                        select.Fill(dtConteo);

                        if (int.Parse(dtConteo.Rows[0][0].ToString()) > 0)
                        {
                            MessageBox.Show("El usuario ya existe. Favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (MessageBox.Show("¿Está seguro de añadir al usuario '" + textBoxUsuclave.Text + "' a la base de datos?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                try
                                {
                                    string add = "INSERT INTO usuarios (usuclave,        nombre,        aPaterno, " +
                                                  "                     aMaterno,        telefono)" +
                                                  "VALUES              (TRIM(@usuclave), TRIM(@nombre), TRIM(@aPaterno), " +
                                                  "                     TRIM(@aMaterno), @telefono)";

                                    using (SqlCommand queryAdd = new SqlCommand(add, openCon, transaction))
                                    {
                                        queryAdd.Parameters.Add("@usuclave", SqlDbType.VarChar).Value = textBoxUsuclave.Text;
                                        queryAdd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBoxNombre.Text;
                                        queryAdd.Parameters.Add("@aPaterno", SqlDbType.VarChar).Value = textBoxAPaterno.Text;
                                        queryAdd.Parameters.Add("@aMaterno", SqlDbType.VarChar).Value = textBoxAMaterno.Text;
                                        queryAdd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = maskedTextBoxTelefono.Text;

                                        queryAdd.ExecuteNonQuery();

                                        transaction.Commit();

                                        MessageBox.Show("Usuario agregado con éxito, se procederá a asignar contraseña", "Ïnformación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        new Asignacion_Contraseña(true, textBoxUsuclave.Text).ShowDialog();


                                        loadAll();

                                        textBoxUsuclave_Validated(sender, e);

                                    }
                                }
                                catch (SqlException ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            else if (buttonAccion.Text.Equals("Actualizar") && admin)
            {
                if (textBoxNombre.Text.Equals("") || textBoxAPaterno.Text.Equals("") || textBoxNombre.Text.Equals(""))
                {
                    MessageBox.Show("Debe capturar todos los campos requeridos, excepto apellido materno en caso de no aplicar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (maskedTextBoxTelefono.TextLength < 10)
                {
                    MessageBox.Show("Debe capturar el número de teléfono con al menos 10 dígitos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();

                        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT COUNT(*) " +
                                                                                  "FROM   usuarios " +
                                                                                  "WHERE  usuclave = '" + textBoxUsuclave.Text + "'", openCon, transaction));

                        DataTable dtConteo = new DataTable();
                        select.Fill(dtConteo);

                        if (int.Parse(dtConteo.Rows[0][0].ToString()) == 0)
                        {
                            MessageBox.Show("El usuario no existe. Favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (MessageBox.Show("¿Está seguro de modificar la información personal del usuario '" + textBoxUsuclave.Text + "'?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                try
                                {
                                    string update = "UPDATE usuarios " +
                                                    "SET    nombre      = TRIM(@nombre), " +
                                                    "       aPaterno    = TRIM(@aPaterno), " +
                                                    "       aMaterno    = TRIM(@aMaterno), " +
                                                    "       telefono    = @telefono, " +
                                                    "       fechaUltAct = getDate() " +
                                                    "WHERE  usuclave    = @usuclave";

                                    using (SqlCommand queryUpdate = new SqlCommand(update, openCon, transaction))
                                    {
                                        queryUpdate.Parameters.Add("@usuclave", SqlDbType.VarChar).Value = textBoxUsuclave.Text;
                                        queryUpdate.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBoxNombre.Text;
                                        queryUpdate.Parameters.Add("@aPaterno", SqlDbType.VarChar).Value = textBoxAPaterno.Text;
                                        queryUpdate.Parameters.Add("@aMaterno", SqlDbType.VarChar).Value = textBoxAMaterno.Text;
                                        queryUpdate.Parameters.Add("@telefono", SqlDbType.VarChar).Value = maskedTextBoxTelefono.Text;

                                        queryUpdate.ExecuteNonQuery();

                                        transaction.Commit();

                                        MessageBox.Show("Usuario actualizado con éxito", "Ïnformación", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                        loadAll();

                                        textBoxUsuclave_Validated(sender, e);

                                    }
                                }
                                catch (SqlException ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonPass_Click(object sender, EventArgs e)
        {
            new Asignacion_Contraseña(admin, textBoxUsuclave.Text).ShowDialog();
        }

        private void buttonAltaBaja_Click(object sender, EventArgs e)
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                SqlDataAdapter selectSt = new SqlDataAdapter(new SqlCommand("SELECT status " +
                                                                            "FROM   usuarios " +
                                                                            "WHERE  usuclave = '" + textBoxUsuclave.Text + "'", openCon, transaction));

                DataTable dtStatus = new DataTable();
                selectSt.Fill(dtStatus);

                if (int.Parse(dtStatus.Rows[0][0].ToString()) == 1)
                {
                    if (MessageBox.Show("¿Está seguro de dar de baja al usuario '" + textBoxUsuclave.Text + "'?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SqlCommand queryUpdate = new SqlCommand("UPDATE usuarios " +
                                                                    "SET    status   = 0 " +
                                                                    "WHERE  usuclave = @usuclave", openCon, transaction);

                            queryUpdate.Parameters.Add("@usuclave", SqlDbType.VarChar).Value = textBoxUsuclave.Text;

                            queryUpdate.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Usuario dado de baja con éxito", "Ïnformación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            loadAll();

                            textBoxUsuclave_Validated(sender, e);

                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("¿Está seguro de dar de alta nuevamente al usuario '" + textBoxUsuclave.Text + "'?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SqlCommand queryUpdate = new SqlCommand("UPDATE usuarios " +
                                                                    "SET    status   = 1 " +
                                                                    "WHERE  usuclave = @usuclave", openCon, transaction);

                            queryUpdate.Parameters.Add("@usuclave", SqlDbType.VarChar).Value = textBoxUsuclave.Text;

                            queryUpdate.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Usuario dado de alta nuevamente con éxito", "Ïnformación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            loadAll();

                            textBoxUsuclave_Validated(sender, e);

                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void dataGridViewUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewUsuarios.RowCount > 0)
            {
                textBoxUsuclave.Text = dataGridViewUsuarios.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxUsuclave_Validated(sender, e);
            }
        }

        private void dataGridViewUsuarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dataGridViewUsuarios.RowCount > 0)
            {
                textBoxUsuclave.Text = dataGridViewUsuarios.Rows[dataGridViewUsuarios.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                textBoxUsuclave_Validated(sender, e);
            }
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtUsuarios.DefaultView.RowFilter = $"Clave LIKE '%{textBoxBuscar.Text}%' OR Nombre LIKE '%{textBoxBuscar.Text}%'";

                dataGridViewUsuarios.AutoResizeColumns();
                dataGridViewUsuarios.Columns[dataGridViewUsuarios.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                if (dataGridViewUsuarios.RowCount > 0)
                {

                    for (int i = 0; i < dataGridViewUsuarios.RowCount; i++)
                    {
                        if (dataGridViewUsuarios.Rows[i].Cells[dataGridViewUsuarios.ColumnCount - 1].Value.ToString().Equals("Activo"))
                        {
                            dataGridViewUsuarios.Rows[i].Cells[dataGridViewUsuarios.ColumnCount - 1].Style.ForeColor = Color.Green;
                        }
                        else
                        {
                            dataGridViewUsuarios.Rows[i].Cells[dataGridViewUsuarios.ColumnCount - 1].Style.ForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (EvaluateException)
            {
                MessageBox.Show("Caracter no permitido para búsqueda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBuscar.Text = textBoxBuscar.Text.Substring(0, textBoxBuscar.TextLength - 1);
                textBoxBuscar.SelectionStart = textBoxBuscar.TextLength;
            }
        }
    }
}
