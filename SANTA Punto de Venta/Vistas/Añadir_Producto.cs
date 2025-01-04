using JR.Utils.GUI.Forms;
using System;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Añadir_Producto : Form
    {
        public bool añadido = false;

        bool anadidoVenta = false;
        public Añadir_Producto(bool a)
        {
            InitializeComponent();
            anadidoVenta = a;
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

        /// <summary>
        /// Evento que añade un producto a la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAñadir_Click(object sender, EventArgs e)
        {
            if (!textBoxCantidad.Text.Equals(".") && !textBoxPrecio.Text.Equals("."))
            {
                //Si hay un campo vacio no hace nada
                if (textBoxNombre.Text.Equals("") || textBoxMarca.Text.Equals("") || textBoxPrecio.Text.Equals("") || textBoxCantidad.Text.Equals("") || textBoxCodigo.Text.Equals(""))
                {
                    MessageBox.Show("Verifica los campos. No dejes ninguno sin llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var db = new SANTADBContext())
                {

                    if (db.productos.AsNoTracking().SingleOrDefault(b => b.id_producto == textBoxCodigo.Text.Trim()) != null)
                    {
                        MessageBox.Show("El producto ya existe, favor de verificar el código de barras", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (MessageBox.Show("¿Estás seguro de que quieres añadir '" + textBoxNombre.Text + "' a la base de datos?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        try
                        {

                            db.productos.Add(new producto
                            {
                                id_producto = textBoxCodigo.Text.Trim(),
                                nombre = textBoxNombre.Text.Trim(),
                                marca = textBoxMarca.Text.Trim(),
                                categoria = comboBoxCategoria.Text.Trim(),
                                cantidad = double.Parse(textBoxCantidad.Text),
                                precio = decimal.Round(decimal.Parse(textBoxPrecio.Text), 2)
                            });


                            db.SaveChanges();

                            MessageBox.Show("'" + textBoxNombre.Text.Trim() + "' ha sido añadido correctamente a la base de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (anadidoVenta)
                            {
                                Dispose();
                                return;
                            }

                            cleanAll();
                            textBoxCodigo.Focus();

                        }
                        catch (DbEntityValidationException dbEVEx)
                        {
                            string errMessage = "";

                            foreach (var eve in dbEVEx.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);

                                errMessage += "Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:\n\n";

                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);

                                    errMessage += "- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage + "\n";

                                }
                            }

                            FlexibleMessageBox.Show("Error: " + errMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }


                //var prod = new SANTADBObject().productos.AsNoTracking().SingleOrDefault(b => b.id_producto == textBoxCodigo.Text.Trim());

                //prod.cantidad = 100;



                ////Si el elemento ya existe no hace nada
                //using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                //{
                //    openCon.Open();
                //    SqlTransaction transaction = openCon.BeginTransaction();
                //
                //    try
                //    {
                //
                //        SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT COUNT(id_producto) " +
                //                                                                  "FROM   productos " +
                //                                                                  "WHERE  id_producto = '" + textBoxCodigo.Text.ToUpper().Trim() + "'", openCon, transaction));
                //
                //        DataTable dtProductos = new DataTable();
                //        select.Fill(dtProductos);
                //
                //        if (int.Parse(dtProductos.Rows[0][0].ToString()) > 0)
                //        {
                //            MessageBox.Show("El producto ya existe, favor de verificar el código de barras", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //        //Si no existe procedemos a darlo de alta
                //        else
                //        {
                //            //Si se escoge si, se da de alta el producto
                //            if (MessageBox.Show("¿Estás seguro de que quieres añadir '" + textBoxNombre.Text + "' a la base de datos?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //            {
                //
                //                using (SqlCommand queryAdd = new SqlCommand("INSERT INTO productos (id_producto,  nombre,   marca, " +
                //                                                            "                       categoria,   cantidad,  precio) " +
                //                                                            "VALUES                (@id_producto, @nombre,  @marca, " +
                //                                                            "                       @categoria,  @cantidad, @precio)", openCon, transaction))
                //                {
                //                    queryAdd.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = textBoxCodigo.Text.ToUpper().Trim();
                //                    queryAdd.Parameters.Add("@nombre",      SqlDbType.VarChar).Value = textBoxNombre.Text.Trim();
                //                    queryAdd.Parameters.Add("@marca",       SqlDbType.VarChar).Value = textBoxMarca.Text.Trim();
                //                    queryAdd.Parameters.Add("@categoria",   SqlDbType.VarChar).Value = comboBoxCategoria.Text;
                //                    queryAdd.Parameters.Add("@cantidad",    SqlDbType.Float).Value   = textBoxCantidad.Text;
                //                    queryAdd.Parameters.Add("@precio",      SqlDbType.Decimal).Value = decimal.Round(decimal.Parse(textBoxPrecio.Text), 2);
                //
                //                    queryAdd.ExecuteNonQuery();
                //
                //                    transaction.Commit();
                //
                //                    MessageBox.Show("'" + textBoxNombre.Text + "' ha sido añadido correctamente a la base de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                    cleanAll();
                //                    textBoxCodigo.Focus();
                //                    añadido = true;
                //
                //                    if (anadidoVenta)
                //                    {
                //                        Dispose();
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    catch (SqlException)
                //    {
                //        transaction.Rollback();
                //        MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente: \n\n- Verifica si la base de datos está en linea\n- Verifica que la información en los campos sea correcta (Ejem. letras en los precios)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    } 
                //}

            }
        }

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

            if (e.KeyChar == (char)Keys.Enter && textBoxCantidad.TextLength > 0 && !textBoxCantidad.Text.Equals("."))
            {
                buttonAñadir_Click(sender, e);
            }
        }

        //Método de limpia los campos para añadir otro producto
        public void cleanAll()
        {
            textBoxCantidad.Text = textBoxCodigo.Text = textBoxMarca.Text = textBoxNombre.Text = textBoxPrecio.Text = "";
            comboBoxCategoria.SelectedIndex = 0;
        }

        /// <summary>
        /// Evento que inicializa los campos al inicializarse la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Añadir_Producto_Load(object sender, EventArgs e)
        {
            cargarCombo();
            textBoxCodigo.Focus();
            if (comboBoxCategoria.Items.Count > 0)
                comboBoxCategoria.SelectedIndex = 0;
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

                    //Verifying if line already exists on comboBox, if not, add line
                    if (!comboBoxCategoria.Items.Contains(s))
                    {
                        comboBoxCategoria.Items.Add(s);
                    }
                }
                //Close file
                sr.Close();
                comboBoxCategoria.SelectedIndex = 0;
            }
            catch (IOException) { MessageBox.Show("Ha ocurrido un error. El archivo 'categoria.txt' no ha podido ser cargado. Primero añada una categoria al lado de Categoría para crear el archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void comboBoxCategoria_Click(object sender, EventArgs e)
        {
            cargarCombo();
        }

        private void buttonAñadirCategoria_Click(object sender, EventArgs e)
        {
            new Añadir_Categoria().ShowDialog();
        }
    }
}
