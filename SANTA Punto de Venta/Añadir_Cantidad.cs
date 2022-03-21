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
    public partial class Añadir_Cantidad : Form
    {
        public bool añadido = false;
        public Añadir_Cantidad()
        {
            InitializeComponent();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buttonAñadir_Click(object sender, EventArgs e)
        {
            if (textBoxCantidad.Text.Equals(""))
            {
                MessageBox.Show("La cantidad a añadir no puede ser nula. Verifique", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(double.Parse(textBoxCantidad.Text) <= 0)
            {
                MessageBox.Show("La cantidad a añadir no puede ser 0 o menor. Verifique", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("¿Estás seguro/a de añadir " + double.Parse(textBoxCantidad.Text) + " pieza(s)/kilo(s) al producto '" + textBoxNombre.Text + "'?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();

                        try
                        {
                            string update = "UPDATE productos " +
                                            "SET    cantidad    = cantidad + @cantidad, " +
                                            "       fechaultact = @fechaultact " +
                                            "WHERE  id_producto = @id_producto";

                            using (SqlCommand queryUpdate = new SqlCommand(update, openCon, transaction))
                            {

                                queryUpdate.Parameters.Add("@cantidad", SqlDbType.Float).Value       = textBoxCantidad.Text;
                                queryUpdate.Parameters.Add("@fechaultact", SqlDbType.DateTime).Value = DateTime.Now;
                                queryUpdate.Parameters.Add("@id_producto", SqlDbType.VarChar).Value  = textBoxCodigo.Text;

                                queryUpdate.ExecuteNonQuery();

                                transaction.Commit();
                                MessageBox.Show("Cantidad añadida con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                añadido = true;
                                Dispose();
                            }
                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente:\n\n- Verifica la conexión a la base de datos y prueba de nuevo\n- Verifica que no hayas ingresado una letra o doble punto e intenta de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
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

            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonAñadir_Click(sender, e);
            }
        }
    }
}
