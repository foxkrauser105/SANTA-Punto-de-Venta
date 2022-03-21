using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Editar_Producto : Form
    {
        string codigo, nombre, marca, categoria;
        double cantidad, precio;
        public bool editado = false;

        /// <summary>
        /// Evento que evita escribir letras en el campo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Evento que evita escribir letras en el campo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                if (e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator && textBoxPrecio.Text.Contains("."))
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

        /// <summary>
        /// Evento que cierra la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void comboBoxCategoria_Click(object sender, EventArgs e)
        {
            cargarCombo();
        }


        /// <summary>
        /// Evento que edita un producto de la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditar_Click(object sender, EventArgs e)
        {
            //Si los valores no cambian, no se hace nada
            if(codigo.ToUpper().Equals(textBoxCodigo.Text.ToUpper()) && nombre.ToUpper().Equals(textBoxNombre.Text.ToUpper()) && marca.ToUpper().Equals(textBoxMarca.Text.ToUpper())
                && categoria.ToUpper().Equals(comboBoxCategoria.Text.ToUpper()) && cantidad.ToString().ToUpper().Equals(textBoxCantidad.Text.ToUpper())
                && precio.ToString().ToUpper().Equals(textBoxPrecio.Text.ToUpper()))
            {
                MessageBox.Show("Los valores para '" + textBoxNombre.Text + "´ no han sido modificados. No se requiere hacer ningúna operación en la base de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//Si hay un campo vacio no se hace nada
            else if(textBoxNombre.Text.Equals("") || textBoxMarca.Text.Equals("") || textBoxPrecio.Text.Equals("") || textBoxCantidad.Text.Equals("") || textBoxCodigo.Text.Equals(""))
            {
                MessageBox.Show("Verifica los campos. No dejes ninguno sin llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();

                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT COUNT(id_producto) " +
                                                                              "FROM   productos " +
                                                                              "WHERE  id_producto = '" + textBoxCodigo.Text.ToUpper().Trim() + "'", openCon, transaction));

                    DataTable dtProductos = new DataTable();
                    select.Fill(dtProductos);

                    if (int.Parse(dtProductos.Rows[0][0].ToString()) > 0 && !textBoxCodigo.Text.ToUpper().Trim().Equals(codigo.ToUpper().Trim()))
                    {
                        MessageBox.Show("El producto ya existe, favor de verificar el código de barras", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

               else if (MessageBox.Show("¿Estás seguro de hacer los cambios deseados? \n\nCódigo: " + codigo + "\nNombre: " + nombre + "\nMarca: " + marca + "\nCategoría: " + categoria
                         + "\nCantidad: " + cantidad.ToString() + "\nPrecio: " + precio.ToString() + "\n\na\n\nCódigo: " + textBoxCodigo.Text + "\nNombre: " + textBoxNombre.Text
                         + "\nMarca: " + textBoxMarca.Text + "\nCategoría: " + comboBoxCategoria.Text + "\nCantidad: " + textBoxCantidad.Text + "\nPrecio: " + textBoxPrecio.Text, "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                         
                        try
                        {
                            using (SqlCommand queryEdit = new SqlCommand("UPDATE productos " +
                                                                         "SET    id_producto = @id_producto, " +
                                                                         "       nombre      = @nombre, " +
                                                                         "       marca       = @marca, " +
                                                                         "       categoria   = @categoria, " +
                                                                         "       cantidad    = @cantidad, " +
                                                                         "       precio      = @precio, " +
                                                                         "       fechaultact = @fechaultact " +
                                                                         "WHERE  id_producto = @id", openCon, transaction))
                            {

                                queryEdit.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = textBoxCodigo.Text.ToUpper().Trim();
                                queryEdit.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBoxNombre.Text.Trim();
                                queryEdit.Parameters.Add("@marca", SqlDbType.VarChar).Value = textBoxMarca.Text.Trim();
                                queryEdit.Parameters.Add("@categoria", SqlDbType.VarChar).Value = comboBoxCategoria.Text;
                                queryEdit.Parameters.Add("@cantidad", SqlDbType.Float).Value = textBoxCantidad.Text;
                                queryEdit.Parameters.Add("@precio", SqlDbType.Decimal).Value = decimal.Round(decimal.Parse(textBoxPrecio.Text), 2);
                                queryEdit.Parameters.Add("@fechaultact", SqlDbType.DateTime).Value = DateTime.Now;
                                queryEdit.Parameters.Add("@id", SqlDbType.VarChar).Value = codigo;

                                queryEdit.ExecuteNonQuery();

                                transaction.Commit();
                                MessageBox.Show("'" + textBoxNombre.Text.Trim() + "' ha sido editado correctamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                editado = true;
                                Dispose();
                            }
                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente: \n\n- Verifica que el código de barras introducido no sea igual al de otro producto\n- Verifica la conexión a la base de datos y prueba de nuevo\n\n- Verifica que la información en los campos sea correcta (Ejem. letras en el precio)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } 
                }
            }
        }
       
        /// <summary>
        /// Constructor que inicializa las variables que se le pasan de la venta padre
        /// </summary>
        /// <param name="cod">Código</param>
        /// <param name="nom">Nombre</param>
        /// <param name="mar">Marca</param>
        /// <param name="cat">Categoría</param>
        /// <param name="can">Cantidad</param>
        /// <param name="pre">Precio</param>
        public Editar_Producto(string cod, string nom, string mar, string cat, double can, double pre)
        {
            codigo = cod;
            nombre = nom;
            marca = mar;
            categoria = cat;
            cantidad = can;
            precio = pre;
            InitializeComponent();
        }

        /// <summary>
        /// Evento que carga los campos al iniciar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editar_Producto_Load(object sender, EventArgs e)
        {
            cargarCombo();
            textBoxCodigo.Text = codigo;
            textBoxNombre.Text = nombre;
            textBoxMarca.Text = marca;
            comboBoxCategoria.Text = categoria;
            textBoxCantidad.Text = cantidad.ToString();
            textBoxPrecio.Text = precio.ToString();

            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();

                try
                {
                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT fechaultact fecha " +
                                                                              "FROM   productos " +
                                                                              "WHERE  id_producto = '" + codigo + "'", openCon, transaction));

                    DataTable dtFecha = new DataTable();
                    select.Fill(dtFecha);

                    if (dtFecha.Rows.Count > 0 && !dtFecha.Rows[0][0].ToString().Equals(""))
                    {
                        dateTimePickerFechaUltAct.Value = (DateTime)dtFecha.Rows[0][0];
                    }

                }
                catch (SqlException)

                {
                    MessageBox.Show("Ha ocurrido un error. No se ha podido verificar la fecha de última actualización del producto. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IndexOutOfRangeException) { }
            }
        }

        public void cargarCombo()
        {
            try
            {
                StreamReader sr = new StreamReader(@"..\..\..\documentos\categoria.txt", System.Text.Encoding.UTF8, true);
                comboBoxCategoria.Items.Clear();
                //Read file until last row
                while (sr.Peek() != -1)
                {
                    //Read line i
                    string s = sr.ReadLine();

                    //If not empty, add to comboBox
                    //If blank, just continue
                    if (String.IsNullOrEmpty(s))
                    {
                        continue;
                    }

                    //Verifying if line already exixts on comboBox, if not, add line
                    if (!comboBoxCategoria.Items.Contains(s))
                    {
                        comboBoxCategoria.Items.Add(s);
                    }
                }
                //Close file
                sr.Close();

                if(comboBoxCategoria.Items.Count > 0)
                {
                    for(int i = 0; i < comboBoxCategoria.Items.Count; i++)
                    {
                        if (comboBoxCategoria.Items[i].ToString().Equals(categoria))
                        {
                            comboBoxCategoria.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (IOException) { MessageBox.Show("Ha ocurrido un error. El archivo 'categoria.txt' no ha podido ser cargado. Primero añada una categoria en la ventana Añadir Producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
