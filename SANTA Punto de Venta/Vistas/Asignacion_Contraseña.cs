using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Asignacion_Contraseña : Form
    {
        bool admin;
        string usuclave;
        public Asignacion_Contraseña(bool admin, string usuclave)
        {
            InitializeComponent();
            this.admin = admin;
            this.usuclave = usuclave;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Asignacion_Contraseña_Load(object sender, EventArgs e)
        {
            if (admin)
            {
                textBoxContAnt.Visible = false;
                labelContAnt.Visible = false;
                textBoxContNva.Focus();
            }
            else
            {
                textBoxContAnt.Focus();
            }
        }

        private void buttonAccion_Click(object sender, EventArgs e)
        {
            if (admin)
            {
                if (textBoxContNva.TextLength == 0 || textBoxVerifCon.TextLength == 0)
                {
                    MessageBox.Show("Debe capturar todos los campos requeridos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textBoxContNva.Text != textBoxVerifCon.Text)
                {
                    MessageBox.Show("Las contraseñas nuevas no coinciden entre sí. Favor de verificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();

                        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT status " +
                                                                                  "FROM   usuarios " +
                                                                                  "WHERE  usuclave = '" + usuclave + "'", openCon, transaction));

                        DataTable dtStatus = new DataTable();
                        select.Fill(dtStatus);

                        if (dtStatus.Rows.Count == 0 || int.Parse(dtStatus.Rows[0][0].ToString()) == 0)
                        {
                            MessageBox.Show("El usuario no existe o está dado de baja. Favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            transaction.Dispose();
                        }
                        else
                        {
                            try
                            {
                                string update = "UPDATE usuarios " +
                                                "SET    pass     = ENCRYPTBYPASSPHRASE('Itendstonightkrystal05', @pass) " +
                                                "WHERE  usuclave = @usuclave";

                                using (SqlCommand queryUpdate = new SqlCommand(update, openCon, transaction))
                                {
                                    queryUpdate.Parameters.Add("@pass", SqlDbType.VarChar).Value = textBoxContNva.Text;
                                    queryUpdate.Parameters.Add("@usuclave", SqlDbType.VarChar).Value = usuclave;

                                    queryUpdate.ExecuteNonQuery();

                                    transaction.Commit();

                                    MessageBox.Show("Contraseña actualizada con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Dispose();
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
            else
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();

                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('Itendstonightkrystal05', pass)) pass " +
                                                                              "FROM   usuarios " +
                                                                              "WHERE  usuclave = '" + usuclave + "'", openCon, transaction));

                    DataTable dtPass = new DataTable();
                    select.Fill(dtPass);

                    if (textBoxContNva.Text.Trim(' ').Equals("") || textBoxVerifCon.Text.Trim(' ').Equals("") || textBoxContAnt.Text.Trim(' ').Equals(""))
                    {
                        MessageBox.Show("Debe capturar todos los campos requeridos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (!dtPass.Rows[0][0].ToString().Equals(textBoxContAnt.Text))
                    {
                        MessageBox.Show("La contraseña anterior no es correcta. Favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxContAnt.Text.Equals(textBoxContNva.Text))
                    {
                        MessageBox.Show("La contraseña nueva no puede ser igual a la anterior. Favor de verificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (textBoxContNva.Text != textBoxVerifCon.Text)
                    {
                        MessageBox.Show("Las contraseñas nuevas no coinciden entre sí. Favor de verificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        SqlDataAdapter selectSt = new SqlDataAdapter(new SqlCommand("SELECT status " +
                                                                                    "FROM   usuarios " +
                                                                                    "WHERE  usuclave = '" + usuclave + "'", openCon, transaction));

                        DataTable dtStatus = new DataTable();
                        selectSt.Fill(dtStatus);

                        if (dtStatus.Rows.Count == 0 || int.Parse(dtStatus.Rows[0][0].ToString()) == 0)
                        {
                            MessageBox.Show("El usuario no existe o está dado de baja. Favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            transaction.Dispose();
                        }
                        else
                        {
                            try
                            {
                                string update = "UPDATE usuarios " +
                                                "SET    pass     = ENCRYPTBYPASSPHRASE('Itendstonightkrystal05', @pass) " +
                                                "WHERE  usuclave = @usuclave";

                                using (SqlCommand queryUpdate = new SqlCommand(update, openCon, transaction))
                                {
                                    queryUpdate.Parameters.Add("@pass", SqlDbType.VarChar).Value = textBoxContNva.Text;
                                    queryUpdate.Parameters.Add("@usuclave", SqlDbType.VarChar).Value = usuclave;

                                    queryUpdate.ExecuteNonQuery();

                                    transaction.Commit();

                                    MessageBox.Show("Contraseña actualizada con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Dispose();
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
}
