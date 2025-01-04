using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Productos : Form
    {
        #region Variables

        private bool _actualizandoTabla = false, _cargaInicial = false;
        private int _rowIndex = 0, _currentCellIndex = 0, _scrollBarPositionindex = 0;
        private readonly System.Timers.Timer _inputTimer = new System.Timers.Timer(500);
        private readonly SANTADBContext _santaDBContext = new SANTADBContext();

        #endregion

        #region Producto
        public Productos()
        {
            InitializeComponent();
            _inputTimer.Elapsed += InputTimer_Elapsed;
        }

        #region Trabajo en segundo plano

        /// <summary>
        /// Background worker que checa cambios de productos cada segundos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgProductos_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bgProductos.CancellationPending)
            {
                Thread.Sleep(10000);

                if (_cargaInicial)
                {
                    BeginInvoke(new MethodInvoker(ActualizaProductos));
                }
            }

            e.Cancel = true;
        }

        /// <summary>
        /// Inicia el background worker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Productos_Shown(object sender, EventArgs e)
        {
            bgProductos.RunWorkerAsync();
        }

        /// <summary>
        /// Para el timer de búsqueda una vez que termina su tiempo de ejecución.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputTimer_Elapsed(object sender, EventArgs e)
        {
            _inputTimer.Stop();
            _inputTimer.Enabled = false;
        }

        #endregion

        /// <summary>
        /// Inicializa la ventana Producto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Productos_Load(object sender, EventArgs e)
        {
            await this.LoadProductos();
            await Task.Run(() => _cargaInicial = true);
        }

        /// <summary>
        /// Carga los productos en el <see cref="DataGridView"/> de Productos.
        /// </summary>
        /// <returns>Tarea para cargar <see cref="producto"/> en el <see cref="DataGridView"/> de Productos.</returns>
        private async Task LoadProductos()
        {

            dataGridViewProductos.SuspendLayout();
            dataGridViewProductos.DataSource = await Task.Run(async () => await this.LoadData());

            if (dataGridViewProductos.ColumnCount > 0)
            {
                dataGridViewProductos.Columns["Categoría"].Visible = false;
                dataGridViewProductos.AutoResizeColumns();
            }

            if (dataGridViewProductos.RowCount > 0)
            {
                this.SetValuesFromSelectedRow();               
            }

            dataGridViewProductos.ResumeLayout();
            lblCargaProductos.Visible = false;
            pnlProductos.Visible = true;
        }

        /// <summary>
        /// Obtiene el producto de la base de datos, conforme al filtro del campo Buscar, o todos en caso de ser vacío.
        /// </summary>
        /// <returns>Tarea con <see cref="DataTable"/> para llenar el <see cref="DataGridView"/> de Productos.</returns>
        private async Task<DataTable> LoadData()
        {
            string sqlQuery = @"SELECT id_producto [Código], 
                                       nombre [Nombre], 
                                       precio [Precio], 
                                       cantidad [Cantidad], 
                                       marca [Marca],
                                       categoria [Categoría],
                                       fechaultact [Fecha Última Actualización]
                                FROM productos
                                WHERE status = 1
                                AND (   @Filter = ''
                                     OR (    @Filter <> ''
                                         AND (   id_producto LIKE '%' + @Filter + '%'
                                              OR nombre LIKE '%' + @Filter + '%')));";

            string filter = string.Empty;

            if (!string.IsNullOrEmpty(textBoxBuscar.Text))
            {
                filter = Utilerias.VerifyQuotes(textBoxBuscar.Text).ToUpper();
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@Filter", filter }
            };

            return await Utilerias.GetResultsFromQueryAsync(sqlQuery, parameters);
        }

        /// <summary>
        /// Carga el <see cref="DataGridView"/> de Productos, y aplica validaciones al UI del mismo.
        /// </summary>
        /// <param name="buscando">Determina si el usuario está escribiendo en el campo Buscar (<c>true</c>), o el método es ejecutado por el background worker (<c>false</c>).</param>
        /// <returns></returns>
        private async Task ObtenBusqueda(bool buscando = false)
        {
            try
            {
                if (this.dataGridViewProductos.SelectedRows.Count > 0)
                {
                    this.SetCurrentCellIndex(this.dataGridViewProductos.CurrentCell.ColumnIndex);
                }

                dataGridViewProductos.SuspendLayout();
                dataGridViewProductos.DataSource = await Task.Run(async () => await this.LoadData());
                dataGridViewProductos.ResumeLayout();
                dataGridViewProductos.AutoResizeColumns();

                this.GetRowCellAndScrollIndex(buscando);
                this.SetValuesFromSelectedRow();

            }
            catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica que la base de datos no haya sido modificada en sus tablas y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// Método que se ejecuta cuando el backgroud worker termina su lapso. Espera a que el timer de búsqueda esté parado para realizar la búsqueda.
        /// </summary>
        private async void ActualizaProductos()
        {

            if (this._inputTimer.Enabled)
            {
                return;
            }

            this.dataGridViewProductos.Scroll -= dataGridViewProductos_Scroll;
            _actualizandoTabla = true;

            await ObtenBusqueda();

            this.dataGridViewProductos.Scroll += dataGridViewProductos_Scroll;
            _actualizandoTabla = false;
        }

        /// <summary>
        /// Evento que llena los campos al dar click en una celda de la tabla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.SetRowAndScrollBarIndex(e.RowIndex, this.dataGridViewProductos.FirstDisplayedScrollingRowIndex);
                this.SetCurrentCellIndex(e.ColumnIndex);
                this.SetValuesFromSelectedRow();
            }
        }

        /// <summary>
        /// Evento que llena los campos al cambiar de renglón de la tabla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewProductos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewProductos.SelectedRows.Count > 0 && !_actualizandoTabla)
            {
                this.SetRowAndScrollBarIndex(this.dataGridViewProductos.SelectedRows[0].Index, this.dataGridViewProductos.FirstDisplayedScrollingRowIndex);
                this.SetCurrentCellIndex(this.dataGridViewProductos.CurrentCell.ColumnIndex);
                this.SetValuesFromSelectedRow();
            }
        }

        /// <summary>
        /// Evento que aplica el valor del indice del primer renglón mostrado en el <see cref="DataGridView"/> de Productos, a la propiedad pertinente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewProductos_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                _scrollBarPositionindex = this.dataGridViewProductos.FirstDisplayedScrollingRowIndex;
            }
        }

        /// <summary>
        /// Evento que controla caracteres especiales que introduce el usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Utilerias.CaracterValido(e.KeyChar);
        }

        /// <summary>
        /// Método que se ejecuta cuando el usuario escribe sobre el campo Buscar. Espera a que el timer de búsqueda esté parado para realizar la búsqueda,
        /// lo que significa que el usuario aún no termina de escribir lo que está buscando, y obtiene la búsqueda una vez el timer se pare.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            _inputTimer.Stop();
            _inputTimer.Enabled = true;
            _inputTimer.Start();

            int delay = (int)_inputTimer.Interval;
            await Task.Delay(delay);

            if (_inputTimer.Enabled || _actualizandoTabla)
            {
                return;
            }

            await ObtenBusqueda(true);
        }

        /// <summary>
        /// Aplica el valor de las propiedades rowIndex y scrollBarPositionIndex al <see cref="DataGridView"/> de Productos, 
        /// una vez que se cargan datos al mismo, para evitar que el index del renglón seleccionado en la tabla se pierda.
        /// </summary>
        /// <param name="buscando">Determina si el valor de scrollBarPositionIndex se aplicará, o solo se calculará, 
        /// dependiendo de si el usario está relizando una búsqueda o el background worker está realizando la petición.</param>
        private void GetRowCellAndScrollIndex(bool buscando = false)
        {
            if (buscando)
            {
                this.SetRowAndScrollBarIndex(0, 0);
            }

            if (_rowIndex > -1 && this.dataGridViewProductos.Rows.Count > 0)
            {
                this.dataGridViewProductos.Rows[_rowIndex].Selected = true;
                this.dataGridViewProductos.CurrentCell = dataGridViewProductos.Rows[_rowIndex].Cells[_currentCellIndex];

                if (!buscando && _scrollBarPositionindex > -1)
                {
                    this.dataGridViewProductos.FirstDisplayedScrollingRowIndex = _scrollBarPositionindex;
                }
            }
            else
            {
                _scrollBarPositionindex = 0;
            }
        }

        /// <summary>
        /// Obtiene el valor de las propiedades rowIndex, y scrollBarPositionindex del <see cref="DataGridView"/> de Productos,
        /// cuando el usuario selecciona un renglón.
        /// </summary>
        /// <param name="rowIndex">El índice del renglón seleccionado.</param>
        /// <param name="scrollBarPositionindex">Elíndice del primer renglón mostrado en la tabla.</param>
        private void SetRowAndScrollBarIndex(int rowIndex, int scrollBarPositionindex)
        {
            this._rowIndex = rowIndex;
            this._scrollBarPositionindex = scrollBarPositionindex;
        }

        /// <summary>
        /// Obtiene el valor de la propiedad currentCellIndex del <see cref="DataGridView"/> de Productos,
        /// cuando el usuario selecciona una celda.
        /// </summary>
        /// <param name="currentCellIndex">El índice de la celda seleccionada.</param>
        private void SetCurrentCellIndex(int currentCellIndex)
        {
            this._currentCellIndex = currentCellIndex;
        }

        /// <summary>
        /// Aplica los valores del renglón seleccionado a los campos debajo del <see cref="DataGridView"/> de Productos.
        /// </summary>
        private void SetValuesFromSelectedRow()
        {
            if (dataGridViewProductos.RowCount > 0 && _rowIndex > -1)
            {
                this.textBoxCodigo.Text = dataGridViewProductos.Rows[_rowIndex].Cells["Código"].Value.ToString();
                this.textBoxNombre.Text = dataGridViewProductos.Rows[_rowIndex].Cells["Nombre"].Value.ToString();
                this.textBoxPrecio.Text = dataGridViewProductos.Rows[_rowIndex].Cells["Precio"].Value.ToString();
                this.textBoxCantidad.Text = dataGridViewProductos.Rows[_rowIndex].Cells["Cantidad"].Value.ToString();
                this.textBoxMarca.Text = dataGridViewProductos.Rows[_rowIndex].Cells["Marca"].Value.ToString();
                this.textBoxCategoria.Text = dataGridViewProductos.Rows[_rowIndex].Cells["Categoría"].Value.ToString();
                this.dateTimePickerFechaUltAct.Value = (DateTime)dataGridViewProductos.Rows[_rowIndex].Cells["Fecha Última Actualización"].Value;

                this.entradaDeProductoToolStripMenuItem.Enabled = true;
                this.editarToolStripMenuItem.Enabled = true;
                this.desactivarToolStripMenuItem.Enabled = true;
            }
            else
            {
                this._rowIndex = 0;
                this._scrollBarPositionindex = 0;

                this.textBoxCodigo.Text = "";
                this.textBoxNombre.Text = "";
                this.textBoxMarca.Text = "";
                this.textBoxCategoria.Text = "";
                this.textBoxCantidad.Text = "";
                this.textBoxPrecio.Text = "";
                this.dateTimePickerFechaUltAct.Value = DateTime.Parse("01/01/3000");

                this.entradaDeProductoToolStripMenuItem.Enabled = false;
                this.editarToolStripMenuItem.Enabled = false;
                this.desactivarToolStripMenuItem.Enabled = false;

            }
        }

        #region ToolStrip Menu

        /// <summary>
        /// Evento que inicia la ventana Añadir productos, y actualiza la tabla .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Añadir_Producto a = new Añadir_Producto(false);
            a.ShowDialog();
            if (a.añadido)
            {
                await ObtenBusqueda();

            }
        }

        /// <summary>
        /// Evento que abre la venta Editar producto, envía los datos neccesarios a editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string codigo = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells["Código"].Value.ToString();
            string nombre = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells["Nombre"].Value.ToString();
            double precio = double.Parse(dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells["Precio"].Value.ToString());
            double cantidad = double.Parse(dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells["Cantidad"].Value.ToString());
            string marca = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells["Marca"].Value.ToString();
            string categoria = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells["Categoría"].Value.ToString();

            Editar_Producto a = new Editar_Producto(codigo, nombre, marca, categoria, cantidad, precio);
            a.ShowDialog();
            if (a.editado)
            {
                await ObtenBusqueda();
            }
        }

        /// <summary>
        /// Evento que desactiva un producto de la base de datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contraseña con = new Contraseña();

            while (true)
            {
                con.ShowDialog(this);

                if (con.DialogResult == DialogResult.Cancel)
                {
                    con.Dispose();
                    break;

                }
                else if (con.DialogResult == DialogResult.Yes && con.textBoxContraseña.Text.Equals(Properties.Settings.Default.Pass))
                {
                    //Si la respuesta es si, desactivamos el producto de la base de datos
                    if (MessageBox.Show("¿Estás seguro de que quieres inactivar el producto '" + dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString() + "'?",
                        "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                        {
                            openCon.Open();
                            SqlTransaction transaction = openCon.BeginTransaction();
                            try
                            {

                                Utilerias.EjecutaComando(@"UPDATE productos 
                                                           SET    status      = 0 
                                                           WHERE  id_producto = @id_producto",
                                                           CommandType.Text,
                                                           openCon,
                                                           transaction,
                                                           "",
                                                           new object[] { "@id_producto", dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString() });

                                transaction.Commit();

                                await ObtenBusqueda();

                                MessageBox.Show("'" + textBoxNombre.Text + "' ha sido desactivado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            }
                            catch (SqlException sqlEx)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Ha habido un error. Verifica lo siguiente:\n\n- Verifica la conexión a la base de datos y prueba de nuevo\n- Verifica que el código de barras del producto a desactivar es correcto.\n\nError: " + sqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    break;

                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        /// <summary>
        /// Evento que abre la ventana Entrada de Producto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void entradaDeProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Añadir_Cantidad a = new Añadir_Cantidad();
            a.textBoxCodigo.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            a.textBoxNombre.Text = dataGridViewProductos.Rows[dataGridViewProductos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
            a.ShowDialog();
            if (a.añadido)
            {
                await ObtenBusqueda();
            }
        }

        /// <summary>
        /// Evento que abre la ventana Productos en Cero.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productosEnCeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Productos_Cero().ShowDialog();
        }

        /// <summary>
        /// Evento que checa si hay productos en estatus inactivo, y muestra la ventana Productos Inactivos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void productosInactivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {
                openCon.Open();
                SqlTransaction transaction = openCon.BeginTransaction();
                try
                {


                    DataTable dtProductos = Utilerias.EjecutaComando("SELECT COUNT(*) " +
                                                                           "FROM   productos " +
                                                                           "WHERE  status = 0",
                                                                           CommandType.Text,
                                                                           openCon,
                                                                           transaction);

                    if (int.Parse(dtProductos.Rows[0][0].ToString()) > 0)
                    {
                        Productos_Inactivos a = new Productos_Inactivos();
                        a.ShowDialog();
                        if (a.reactivado)
                        {
                            await ObtenBusqueda();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay productos inactivos, no es necesario acceder", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException) { MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        /// <summary>
        /// Evento que muestra la ventana Descuentos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void descuentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Accion_Productos("Descuento").ShowDialog();
        }

        /// <summary>
        /// Evento que abre la ventana Usuarios, al introducir la contraseña correcta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contraseña con = new Contraseña();
            con.ShowDialog(this);

            if (con.DialogResult == DialogResult.Cancel)
            {
                con.Dispose();
            }
            else if (con.DialogResult == DialogResult.Yes && con.textBoxContraseña.Text.Equals(Properties.Settings.Default.Pass))
            {
                new Usuarios(true).ShowDialog();
            }
            else
            {
                new Usuarios(false).ShowDialog();
            }
        }

        /// <summary>
        /// Evento que abre la ventana Clientes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acceso a = new Acceso();
            a.ShowDialog();
            if (a.enter)
            {
                new Clientes(a.usuclave).ShowDialog();
            }
        }

        /// <summary>
        /// Evento que abre la ventana Notas de Crédito.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notasDeCréditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Nota_Credito(0, null, 0).ShowDialog();
        }

        #endregion

        #endregion
    }
}
