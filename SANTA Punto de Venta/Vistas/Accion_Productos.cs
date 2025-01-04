using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Accion_Productos : Form
    {

        string accion = "";
        string codigoVerificador = "";
        public bool editado = false;
        DataTable dtProductos = new DataTable();

        public Accion_Productos(string accion)
        {
            InitializeComponent();
            this.accion = accion;
        }

        public string verifyQuotes(string s)
        {
            string sNew = "";
            for (int i = 0; i < s.Length; i++)
            {
                char insert = (char)(s[i]);
                if (insert.Equals('\''))
                {
                    sNew += "'";
                }
                sNew += insert.ToString();
            }
            return sNew;
        }

        private void Accion_Productos_Load(object sender, EventArgs e)
        {
            if (accion.Equals("Editar"))
            {
                buttonAplicar.Visible = false;
                buttonStatus.Visible = false;

                textBoxCodigo.Enabled = false;
                textBoxNombre.Enabled = false;
                textBoxMarca.Enabled = false;
                textBoxCategoria.Enabled = false;
                textBoxPrecio.Enabled = false;
                textBoxCantidad.Enabled = false;

                labelPrincipal.Text = "Editar Código";
                labelPrincipal.Location = new Point(this.Width / 2 - labelPrincipal.Width / 2, labelPrincipal.Location.Y);
                pictureBoxLogo.Location = new Point(labelPrincipal.Location.X - pictureBoxLogo.Width - 7, pictureBoxLogo.Location.Y);

                labelCodigo.Text = "Código Nuevo:";
                labelCantidad.Text = "Cantidad:";
                labelPrecio.Text = "Precio:";

                labelPrecio.Location = new Point(labelCategoria.Location.X + labelCategoria.Width - labelPrecio.Width, labelPrecio.Location.Y);
                labelCantidad.Location = new Point(labelCategoria.Location.X + labelCategoria.Width - labelCantidad.Width, labelCantidad.Location.Y);

                loadEditar();
            }
            else if (accion.Equals("Descuento"))
            {
                buttonEditar.Visible = false;

                textBoxNombre.Enabled = false;
                textBoxMarca.Enabled = false;
                textBoxCategoria.Enabled = false;

                try
                {
                    pictureBoxLogo.Image = Image.FromFile(@"..\..\Resources\discount.png");
                }
                catch (IOException)
                {
                    MessageBox.Show("No se pudo cargar la imagen del logo principal de 'Descuentos'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                labelPrincipal.Text = "Descuentos";
                labelPrincipal.Location = new Point(this.Width / 2 - labelPrincipal.Width / 2, labelPrincipal.Location.Y);
                pictureBoxLogo.Location = new Point(labelPrincipal.Location.X - pictureBoxLogo.Width - 7, pictureBoxLogo.Location.Y);

                labelCodigo.Text = "Código:";
                labelCantidad.Text = "Cantidad Mínima:";
                labelPrecio.Text = "Precio Descuento:";

                labelCodigo.Location = new Point(labelNombre.Location.X + labelNombre.Width - labelCodigo.Width, labelCodigo.Location.Y);

                buttonAplicar.Text = "Aplicar";
                buttonStatus.Text = "Activar descuento";

                loadDescuento();
            }
        }

        public void loadEditar()
        {
            textBoxBuscar.Text = "";
            try
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();
                    SqlDataAdapter select = new SqlDataAdapter("SELECT id_producto Código,  nombre Nombre,     marca Marca, " +
                                                               "       categoria Categoría, cantidad Cantidad, precio Precio " +
                                                               "FROM   productos " +
                                                               "WHERE  status <> 0 " +
                                                               "ORDER BY id_producto", openCon);
                    select.SelectCommand.Transaction = transaction;
                    dtProductos.Clear();
                    select.Fill(dtProductos);


                    dataGridViewProductos.DataSource = dtProductos;


                    if (dataGridViewProductos.RowCount > 0)
                    {
                        textBoxNombre.Text = dataGridViewProductos.Rows[0].Cells[1].Value.ToString();
                        textBoxMarca.Text = dataGridViewProductos.Rows[0].Cells[2].Value.ToString();
                        textBoxCategoria.Text = dataGridViewProductos.Rows[0].Cells[3].Value.ToString();
                        textBoxCantidad.Text = dataGridViewProductos.Rows[0].Cells[4].Value.ToString();
                        textBoxPrecio.Text = dataGridViewProductos.Rows[0].Cells[5].Value.ToString();

                        dataGridViewProductos.AutoResizeColumns();

                    }
                    else
                    {
                        buttonEditar.Enabled = false;
                    }
                }
            }
            catch (SqlException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void loadDescuento()
        {
            textBoxBuscar.Text = "";
            try
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();
                    SqlDataAdapter select = new SqlDataAdapter("SELECT IIF(d.status = 1, 'Activo', 'Inactivo') Status, d.id_producto Código,  p.nombre Nombre, " +
                                                               "p.marca Marca,                                         p.categoria Categoría, d.cantidadMinima \"Cantidad Mínima\", " +
                                                               "d.precioDescuento \"Precio Descuento\" " +
                                                               "FROM   descuentos d, productos p " +
                                                               "WHERE  p.id_producto = d.id_producto " +
                                                               "ORDER BY d.id_producto", openCon);

                    select.SelectCommand.Transaction = transaction;
                    dtProductos.Clear();
                    select.Fill(dtProductos);

                    dataGridViewProductos.DataSource = dtProductos;


                    if (dataGridViewProductos.RowCount > 0)
                    {
                        textBoxCodigo.Text = dataGridViewProductos.Rows[0].Cells[1].Value.ToString();
                        textBoxNombre.Text = dataGridViewProductos.Rows[0].Cells[2].Value.ToString();
                        textBoxMarca.Text = dataGridViewProductos.Rows[0].Cells[3].Value.ToString();
                        textBoxCategoria.Text = dataGridViewProductos.Rows[0].Cells[4].Value.ToString();
                        textBoxCantidad.Text = dataGridViewProductos.Rows[0].Cells[5].Value.ToString();
                        textBoxPrecio.Text = dataGridViewProductos.Rows[0].Cells[6].Value.ToString();

                        dataGridViewProductos.AutoResizeColumns();

                        for (int i = 0; i < dataGridViewProductos.RowCount; i++)
                        {
                            if (dataGridViewProductos.Rows[i].Cells[0].Value.ToString().Equals("Activo"))
                            {
                                dataGridViewProductos.Rows[i].Cells[0].Style.ForeColor = Color.Green;
                            }
                            else if (dataGridViewProductos.Rows[i].Cells[0].Value.ToString().Equals("Inactivo"))
                            {
                                dataGridViewProductos.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            }
                        }

                        if (dataGridViewProductos.Rows[0].Cells[0].Value.ToString().Equals("Activo"))
                        {
                            buttonStatus.Text = "Desactivar descuento";
                        }
                        else if (dataGridViewProductos.Rows[0].Cells[0].Value.ToString().Equals("Inactivo"))
                        {
                            buttonStatus.Text = "Activar descuento";
                        }

                    }
                    else
                    {
                        buttonAplicar.Enabled = false;
                        buttonStatus.Enabled = false;
                    }
                }
            }
            catch (SqlException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (textBoxCodigo.Text.Equals(""))
            {
                MessageBox.Show("No dejes el campo Código sin llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Si la respuesta es si, se ejecuta el codigo de ejecución
                if (MessageBox.Show("¿Estás seguro de hacer los cambios deseados? \n\nCódigo: " + dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "\n\na\n\nCódigo: " + textBoxCodigo.Text, "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        try
                        {
                            string edit = "UPDATE productos " +
                                          "SET    id_producto = @id_producto " +
                                          "WHERE  id_producto = @id";
                            using (SqlCommand queryEdit = new SqlCommand(edit))
                            {
                                queryEdit.Connection = openCon;
                                queryEdit.Transaction = transaction;
                                queryEdit.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = textBoxCodigo.Text;
                                queryEdit.Parameters.Add("@id", SqlDbType.VarChar).Value = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();

                                queryEdit.ExecuteNonQuery();

                                transaction.Commit();
                                editado = true;
                                MessageBox.Show("'" + textBoxNombre.Text + "' ha sido editado correctamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            if (accion.Equals("Editar"))
            {
                try
                {
                    dtProductos.DefaultView.RowFilter = $"Código LIKE '%{verifyQuotes(textBoxBuscar.Text)}%' OR Nombre Like '%{verifyQuotes(textBoxBuscar.Text)}%'";
                    //using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    //{
                    //    openCon.Open();
                    //    SqlTransaction transaction = openCon.BeginTransaction();
                    //    SqlDataAdapter select = new SqlDataAdapter("select id_producto Código, nombre Nombre, marca Marca, categoria Categoría, cantidad Cantidad, precio Precio from productos where (id_producto like '%" + textBoxBuscar.Text + "%' or nombre like '%" + textBoxBuscar.Text + "%') and status <> 0 ORDER BY id_producto", openCon);
                    //    select.SelectCommand.Transaction = transaction;
                    //    DataSet ds = new DataSet("Check");
                    //    select.FillSchema(ds, SchemaType.Source, "Productos");
                    //    select.Fill(ds, "Productos");
                    //    DataTable dtProductos = ds.Tables["Productos"];

                    //    dataGridViewProductos.DataSource = dtProductos;


                    if (dataGridViewProductos.RowCount > 0)
                    {
                        textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                        textBoxMarca.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                        textBoxCategoria.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                        textBoxCantidad.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                        textBoxPrecio.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString();

                        dataGridViewProductos.AutoResizeColumns();

                        buttonEditar.Enabled = true;
                    }
                    else
                    {
                        textBoxNombre.Text = "";
                        textBoxMarca.Text = "";
                        textBoxCategoria.Text = "";
                        textBoxCantidad.Text = "";
                        textBoxPrecio.Text = "";

                        buttonEditar.Enabled = false;
                    }
                    //}
                }
                catch (SqlException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (accion.Equals("Descuento"))
            {
                try
                {
                    dtProductos.DefaultView.RowFilter = $"Código LIKE '%{textBoxBuscar.Text}%' OR Nombre Like '%{textBoxBuscar.Text}%'";
                    //using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    //{
                    //    openCon.Open();
                    //    SqlTransaction transaction = openCon.BeginTransaction();
                    //    SqlDataAdapter select = new SqlDataAdapter("select IIF(d.status = 1, 'Activo', 'Inactivo') Status, d.id_producto Código, p.nombre Nombre, p.marca Marca, p.categoria Categoría, d.cantidadMinima \"Cantidad Mínima\", d.precioDescuento \"Precio Descuento\" from descuentos d, productos  p where  p.id_producto = d.id_producto and (p.id_producto like '%" + textBoxBuscar.Text + "%' or p.nombre like '%" + textBoxBuscar.Text + "%') ORDER BY d.id_producto", openCon);
                    //    select.SelectCommand.Transaction = transaction;
                    //    DataSet ds = new DataSet("Check");
                    //    select.FillSchema(ds, SchemaType.Source, "Descuentos");
                    //    select.Fill(ds, "Descuentos");
                    //    DataTable dtProductos = ds.Tables["Descuentos"];

                    //    dataGridViewProductos.DataSource = dtProductos;

                    if (dataGridViewProductos.RowCount > 0)
                    {
                        textBoxCodigo.Text = dataGridViewProductos.Rows[0].Cells[1].Value.ToString();
                        textBoxNombre.Text = dataGridViewProductos.Rows[0].Cells[2].Value.ToString();
                        textBoxMarca.Text = dataGridViewProductos.Rows[0].Cells[3].Value.ToString();
                        textBoxCategoria.Text = dataGridViewProductos.Rows[0].Cells[4].Value.ToString();
                        textBoxCantidad.Text = dataGridViewProductos.Rows[0].Cells[5].Value.ToString();
                        textBoxPrecio.Text = dataGridViewProductos.Rows[0].Cells[6].Value.ToString();

                        dataGridViewProductos.AutoResizeColumns();

                        buttonAplicar.Enabled = true;
                        buttonStatus.Enabled = true;

                        for (int i = 0; i < dataGridViewProductos.RowCount; i++)
                        {
                            if (dataGridViewProductos.Rows[i].Cells[0].Value.ToString().Equals("Activo"))
                            {
                                dataGridViewProductos.Rows[i].Cells[0].Style.ForeColor = Color.Green;
                            }
                            else if (dataGridViewProductos.Rows[i].Cells[0].Value.ToString().Equals("Inactivo"))
                            {
                                dataGridViewProductos.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            }
                        }

                        if (dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString().Equals("Activo"))
                        {
                            buttonStatus.Text = "Desactivar descuento";
                        }
                        else if (dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString().Equals("Inactivo"))
                        {
                            buttonStatus.Text = "Activar descuento";
                        }

                    }
                    else
                    {
                        textBoxCodigo.Text = "";
                        textBoxNombre.Text = "";
                        textBoxMarca.Text = "";
                        textBoxCategoria.Text = "";
                        textBoxCantidad.Text = "";
                        textBoxPrecio.Text = "";

                        buttonAplicar.Enabled = false;
                        buttonStatus.Enabled = false;
                    }
                    //}
                }
                catch (SqlException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dataGridViewProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewProductos.RowCount > 0 && e.RowIndex != -1)
            {
                if (accion.Equals("Editar"))
                {
                    textBoxNombre.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBoxMarca.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBoxCategoria.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBoxCantidad.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBoxPrecio.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
                else if (accion.Equals("Descuento"))
                {
                    textBoxCodigo.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBoxNombre.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBoxMarca.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBoxCategoria.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBoxCantidad.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBoxPrecio.Text = dataGridViewProductos.Rows[e.RowIndex].Cells[6].Value.ToString();

                    if (dataGridViewProductos.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("Activo"))
                    {
                        buttonStatus.Text = "Desactivar descuento";
                    }
                    else if (dataGridViewProductos.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("Inactivo"))
                    {
                        buttonStatus.Text = "Activar descuento";
                    }

                    buttonAplicar.Text = "Aplicar";
                    buttonStatus.Enabled = true;
                }
            }
        }

        private void textBoxCodigo_Validated(object sender, EventArgs e)
        {
            if (accion.Equals("Descuento"))
            {
                if (!textBoxCodigo.Text.Equals(""))
                {
                    try
                    {
                        using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                        {
                            openCon.Open();
                            SqlTransaction transaction = openCon.BeginTransaction();
                            SqlCommand command = new SqlCommand(@"SELECT p.nombre,         p.marca,           p.categoria,
                                                                         d.cantidadMinima, d.precioDescuento, d.status
                                                                  FROM   productos p
                                                                  LEFT OUTER JOIN descuentos d
                                                                  ON     p.id_producto = d.id_producto
                                                                  WHERE  p.id_producto = @IdProducto
                                                                  AND    p.status <> 0", openCon, transaction);

                            command.Parameters.Add("@IdProducto", SqlDbType.VarChar).Value = textBoxCodigo.Text;

                            SqlDataAdapter select = new SqlDataAdapter(command);

                            DataTable dtProductos = new DataTable();
                            select.Fill(dtProductos);

                            if (dtProductos.Rows.Count > 0)
                            {
                                textBoxNombre.Text = dtProductos.Rows[0][0].ToString();
                                textBoxMarca.Text = dtProductos.Rows[0][1].ToString();
                                textBoxCategoria.Text = dtProductos.Rows[0][2].ToString();
                                textBoxCantidad.Text = dtProductos.Rows[0][3].ToString();
                                textBoxPrecio.Text = dtProductos.Rows[0][4].ToString();

                                if (dtProductos.Rows[0][3].ToString().Equals("") && dtProductos.Rows[0][4].ToString().Equals(""))
                                {
                                    buttonAplicar.Text = "Agregar";
                                    buttonAplicar.Enabled = true;
                                    buttonStatus.Enabled = false;
                                }
                                else
                                {
                                    buttonAplicar.Text = "Aplicar";
                                    if (int.Parse(dtProductos.Rows[0][5].ToString()) == 1)
                                    {
                                        buttonStatus.Text = "Desactivar descuento";
                                    }
                                    else
                                    {
                                        buttonStatus.Text = "Activar descuento";
                                    }
                                    buttonStatus.Enabled = true;
                                }

                                for (int i = 0; i < dataGridViewProductos.RowCount; i++)
                                {
                                    if (textBoxCodigo.Text.ToUpper().Equals(dataGridViewProductos.Rows[i].Cells[1].Value.ToString().ToUpper()))
                                    {
                                        dataGridViewProductos.CurrentCell = dataGridViewProductos.Rows[i].Cells[0];
                                        dataGridViewProductos.Rows[i].Selected = true;
                                        break;
                                    }
                                }


                                codigoVerificador = textBoxCodigo.Text;
                            }
                            else
                            {
                                MessageBox.Show("El producto que ingresaste no existe o está en status inactivo. Prueba a usar la lista de valores de Productos válidos usando F9.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBoxCodigo.Focus();
                                textBoxNombre.Text = "";
                                textBoxMarca.Text = "";
                                textBoxCategoria.Text = "";
                                textBoxCantidad.Text = "";
                                textBoxPrecio.Text = "";
                            }
                        }
                    }
                    catch (SqlException ex) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                {
                    textBoxNombre.Text = "";
                    textBoxMarca.Text = "";
                    textBoxCategoria.Text = "";
                    textBoxCantidad.Text = "";
                    textBoxPrecio.Text = "";
                }           
            }
        }

        private void buttonAplicar_Click(object sender, EventArgs e)
        {
            if (buttonAplicar.Text.Equals("Agregar"))
            {
                if (textBoxNombre.Text.Equals("") || textBoxCategoria.Text.Equals("") || textBoxMarca.Text.Equals("") || textBoxPrecio.Text.Equals("") || textBoxCantidad.Text.Equals("") || textBoxCodigo.Text.Equals(""))
                {
                    MessageBox.Show("Verifica los campos. No dejes ninguno sin llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!textBoxCodigo.Text.Equals(codigoVerificador) && !codigoVerificador.Equals(""))
                {
                    MessageBox.Show("El código no coincide con ningún producto, asegúrate de no haberlo modificado antes de agregar el descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        SqlCommand command = new SqlCommand(@"SELECT count(*)
                                                              FROM   descuentos
                                                              WHERE  id_producto = @IdProducto", openCon, transaction);

                        command.Parameters.Add("@IdProducto", SqlDbType.VarChar).Value = textBoxCodigo.Text;

                        SqlDataAdapter select = new SqlDataAdapter(command);
                        DataTable dtProductos = new DataTable();
                        select.Fill(dtProductos);


                        if (int.Parse(dtProductos.Rows[0][0].ToString()) > 0)
                        {
                            MessageBox.Show("El producto ingresado ya tiene un descuento asignado, verifique", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (MessageBox.Show($"¿Estás seguro de añadir el descuento al producto '{textBoxNombre.Text}'?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                string add = "INSERT INTO descuentos (id_producto,  cantidadMinima,  precioDescuento) " +
                                             "VALUES                 (@id_producto, @cantidadMinima, @precioDescuento)";
                                using (SqlCommand queryAdd = new SqlCommand(add))
                                {
                                    try
                                    {
                                        queryAdd.Connection = openCon;
                                        queryAdd.Transaction = transaction;
                                        queryAdd.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = textBoxCodigo.Text;
                                        queryAdd.Parameters.Add("@cantidadMinima", SqlDbType.Float).Value = textBoxCantidad.Text;
                                        queryAdd.Parameters.Add("@precioDescuento", SqlDbType.Float).Value = textBoxPrecio.Text;

                                        queryAdd.ExecuteNonQuery();

                                        transaction.Commit();

                                        MessageBox.Show($"Se ha asignado un descuento al producto '{textBoxNombre.Text}' correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        loadDescuento();
                                        textBoxCodigo.Focus();

                                    }
                                    catch (SqlException)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente: \n\n- Verifica si la base de datos está en linea\n- Verifica que la información en los campos sea correcta (Ejem. letras en los precios)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (buttonAplicar.Text.Equals("Aplicar"))
            {
                if (textBoxNombre.Text.Equals("") || textBoxCategoria.Text.Equals("") || textBoxMarca.Text.Equals("") || textBoxPrecio.Text.Equals("") || textBoxCantidad.Text.Equals("") || textBoxCodigo.Text.Equals(""))
                {
                    MessageBox.Show("Verifica los campos. No dejes ninguno sin llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString().Equals(textBoxCantidad.Text) && dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[6].Value.ToString().Equals(textBoxPrecio.Text))
                {
                    MessageBox.Show("El descuento y la cantidad en la que se aplica no han cambiado. No es necesario aplicar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        if (MessageBox.Show("¿Estás seguro de aplicar el descuento al producto '" + textBoxNombre.Text + "'?\n\n- Cantidad mínima anterior: " + dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[5].Value.ToString() + "\n- Precio descuento anterior: $" + dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[6].Value.ToString() +
                            "\n\na\n\n- Cantidad mínima nueva: " + textBoxCantidad.Text + "\n- Precio descuento nuevo: $" + textBoxPrecio.Text, "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string update = "UPDATE descuentos " +
                                            "SET    cantidadMinima  = @cantidadMinima, " +
                                            "       precioDescuento = @precioDescuento " +
                                            "WHERE  id_producto     = @id_producto";
                            using (SqlCommand queryAdd = new SqlCommand(update))
                            {
                                try
                                {
                                    queryAdd.Connection = openCon;
                                    queryAdd.Transaction = transaction;
                                    queryAdd.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = textBoxCodigo.Text;
                                    queryAdd.Parameters.Add("@cantidadMinima", SqlDbType.Float).Value = textBoxCantidad.Text;
                                    queryAdd.Parameters.Add("@precioDescuento", SqlDbType.Float).Value = textBoxPrecio.Text;

                                    queryAdd.ExecuteNonQuery();

                                    transaction.Commit();

                                    MessageBox.Show($"Se ha asignado un descuento al producto '{textBoxNombre.Text}' correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadDescuento();
                                    textBoxCodigo.Focus();

                                }
                                catch (SqlException)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente: \n\n- Verifica si la base de datos está en linea\n- Verifica que la información en los campos sea correcta (Ejem. letras en los precios)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void cleanAll()
        {
            textBoxCodigo.Text = "";
            textBoxNombre.Text = "";
            textBoxMarca.Text = "";
            textBoxCategoria.Text = "";
            textBoxCantidad.Text = "";
            textBoxPrecio.Text = "";
        }

        private void dataGridViewProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dataGridViewProductos.RowCount; i++)
            {
                if (dataGridViewProductos.Rows[i].Cells[0].Value.ToString().Equals("Activo"))
                {
                    dataGridViewProductos.Rows[i].Cells[0].Style.ForeColor = Color.Green;
                }
                else if (dataGridViewProductos.Rows[i].Cells[0].Value.ToString().Equals("Inactivo"))
                {
                    dataGridViewProductos.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                }
            }
        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            if (buttonStatus.Text.Equals("Desactivar descuento"))
            {
                if (MessageBox.Show($"¿Estás seguro de desactivar el producto '{textBoxNombre.Text}'? Esto hará que no se puedan aplicar descuentos al producto al momento de vender", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        try
                        {
                            string update = "UPDATE descuentos " +
                                            "SET    status      = 0 " +
                                            "WHERE  id_producto = @id_producto";

                            using (SqlCommand queryUpdate = new SqlCommand(update))
                            {
                                queryUpdate.Connection = openCon;
                                queryUpdate.Transaction = transaction;
                                queryUpdate.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = textBoxCodigo.Text;

                                queryUpdate.ExecuteNonQuery();

                                transaction.Commit();

                                MessageBox.Show($"El Status del producto '{textBoxNombre.Text}' ha sido actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadDescuento();
                            }
                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente: \n\n- Verifica si la base de datos está en linea\n- Verifica que la información en los campos sea correcta (Ejem. letras en los precios)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else if (buttonStatus.Text.Equals("Activar descuento"))
            {
                if (MessageBox.Show($"¿Estás seguro de activar el producto '{textBoxNombre.Text}'? Esto hará que se puedan aplicar descuentos al producto al momento de vender", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                    {
                        openCon.Open();
                        SqlTransaction transaction = openCon.BeginTransaction();
                        try
                        {
                            string update = "UPDATE descuentos " +
                                            "SET    status      = 1 " +
                                            "WHERE  id_producto = @id_producto";

                            using (SqlCommand queryUpdate = new SqlCommand(update))
                            {
                                queryUpdate.Connection = openCon;
                                queryUpdate.Transaction = transaction;
                                queryUpdate.Parameters.Add("@id_producto", SqlDbType.VarChar).Value = textBoxCodigo.Text;

                                queryUpdate.ExecuteNonQuery();

                                transaction.Commit();

                                MessageBox.Show($"El Status del producto '{textBoxNombre.Text}' ha sido actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadDescuento();
                            }
                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente: \n\n- Verifica si la base de datos está en linea\n- Verifica que la información en los campos sea correcta (Ejem. letras en los precios)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void textBoxCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (accion == "Descuento" && e.KeyCode == Keys.F9)
            {
                Utilerias.ValidarDatos(textBoxCodigo, usuarioBusca: true);
                this.textBoxCodigo_Validated(sender, e);
            }
        }
    }
}
