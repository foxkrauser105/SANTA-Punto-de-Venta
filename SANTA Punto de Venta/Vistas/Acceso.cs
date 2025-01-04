using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Acceso : Form
    {
        //Esta variable permitira saber si el usuario y contraseña coiciden, si no, no entrará a la pantalla requerida por la forma padre
        public bool enter = false;
        public string usuclave;
        public Acceso()
        {
            InitializeComponent();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            enter = false;
            Dispose();
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            if (textBoxUsuario.TextLength == 0 || textBoxContrasena.TextLength == 0)
            {
                MessageBox.Show("Debe capturar todos los campos requeridos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();

                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"SELECT CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('Itendstonightkrystal05', pass)) pass, status 
                                                                 FROM   usuarios 
                                                                 WHERE  usuclave = @Usuario", openCon, transaction);

                        sqlCommand.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = textBoxUsuario.Text;

                        SqlDataAdapter select = new SqlDataAdapter(sqlCommand);
                        DataTable dtUsuario = new DataTable();
                        select.Fill(dtUsuario);

                        if (dtUsuario.Rows.Count == 0 || int.Parse(dtUsuario.Rows[0][1].ToString()) == 0)
                        {
                            MessageBox.Show("El usuario no existe o está dado de baja. Favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            transaction.Dispose();
                        }
                        else if (!dtUsuario.Rows[0][0].ToString().Equals(textBoxContrasena.Text))
                        {
                            MessageBox.Show("La contraseña no es correcta. Favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            enter = true;
                            usuclave = textBoxUsuario.Text;
                            Dispose();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ha ocurrido un error. No se pudo acceder a la base de datos. Verifica si está en línea y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBoxUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBoxContrasena.Focus();
            }
        }

        private void textBoxContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonAceptar.PerformClick();
            }
        }
    }
}
