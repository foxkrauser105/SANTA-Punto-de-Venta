using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SANTA_Punto_de_Venta.Vistas;

namespace SANTA_Punto_de_Venta
{
    public partial class Main : Form
    {
        #region Variables

        private Ventas _ventas;
        private Requisición_Producto _requisicionProducto;
        private int _rowIndex = 0, _scrollBarPositionindex = 0, _currentCellIndex = 0;
        private bool _screensEnoughHeight = false;

        #endregion

        #region Main

        public Main()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX", false);
            _screensEnoughHeight = Screen.AllScreens.Any(s => s.Bounds.Height >= 850);
        }

        #region Trabajo en Segundo Plano

        /// <summary>
        /// Evento que inicia el background worker que actualiza las notificaciones del sistema.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Shown(object sender, EventArgs e)
        {
            if (this._screensEnoughHeight)
            {
                bwNotificaciones.RunWorkerAsync();
            }
            else
            {
                this.buttonShowNot.PerformClick();
                this.buttonShowNot.Visible = false;
            }
        }

        /// <summary>
        /// Actualiza el <see cref="DataGridView"/> de notificaciones del sistema cada 10 segundos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwNotificaciones_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!bwNotificaciones.CancellationPending)
            {
                Thread.Sleep(10000);
                BeginInvoke(new MethodInvoker(ActualizaNotificaciones));
            }

            e.Cancel = true;
        }

        #endregion

        /// <summary>
        /// Metodo que añade una forma al panel al hacer click en uno de los botones del menú principal.
        /// </summary>
        /// <param name="form"></param>
        private void AddFormInPanel(Form fh)
        {
            if (this.panelPrincipal.Controls.Count > 0)
                this.panelPrincipal.Controls.RemoveAt(0);
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            this.panelPrincipal.Controls.Add(fh);
            this.panelPrincipal.Tag = fh;
            fh.Show();
            panelPrincipal.Focus();

        }

        /// <summary>
        /// Obtiene las notificaciones del sistema y las muestra en el <see cref="DataGridView"/> inferior del sistema.
        /// </summary>
        /// <returns></returns>
        private async Task ObtenNotificaciones()
        {
            if (this.dataGridViewNotificaciones.SelectedRows.Count > 0)
            {
                this.SetCurrentCellIndex(this.dataGridViewNotificaciones.CurrentCell.ColumnIndex);
            }

            this.dataGridViewNotificaciones.SuspendLayout();
            this.dataGridViewNotificaciones.DataSource = await Task.Run( async () =>
            {
                string sqlQuery = @"SELECT [No.], ISNULL(Para, 'General') Para, Mensaje
                                    FROM (SELECT id_mensaje [No.], (SELECT nombre FROM usuarios WHERE usuclave = n.usuclave_recibe) [Para], n.mensaje [Mensaje]
                                          FROM   notificaciones n
                                          WHERE  status = 1) a
                                    ORDER BY [No.];";

                return await Utilerias.GetResultsFromQueryAsync(sqlQuery);
            });

            if (this.dataGridViewNotificaciones.ColumnCount > 0)
            {
                this.dataGridViewNotificaciones.AutoResizeColumns();
                this.dataGridViewNotificaciones.Columns[this.dataGridViewNotificaciones.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            this.GetRowCellAndScrollIndex();
            this.dataGridViewNotificaciones.ResumeLayout();
        }

        /// <summary>
        /// Actualiza las notificaciones del sistema, cuando el background worker lo manda llamar.
        /// </summary>
        public async void ActualizaNotificaciones()
        {
            this.dataGridViewNotificaciones.Scroll -= dataGridViewNotificaciones_Scroll;
            await this.ObtenNotificaciones();
            this.dataGridViewNotificaciones.Scroll -= dataGridViewNotificaciones_Scroll;
        }

        /// <summary>
        /// Aplica el valor de las propiedades rowIndex y scrollBarPositionIndex al <see cref="DataGridView"/> de Notificaciones, 
        /// una vez que se cargan datos al mismo, para evitar que el index del renglón seleccionado en la tabla se pierda.
        /// </summary>
        private void GetRowCellAndScrollIndex()
        {

            if (_rowIndex > -1 && this.dataGridViewNotificaciones.Rows.Count > 0)
            {
                this.dataGridViewNotificaciones.Rows[_rowIndex].Selected = true;
                this.dataGridViewNotificaciones.CurrentCell = dataGridViewNotificaciones.Rows[_rowIndex].Cells[_currentCellIndex];

                if (_scrollBarPositionindex > -1)
                {
                    this.dataGridViewNotificaciones.FirstDisplayedScrollingRowIndex = _scrollBarPositionindex;
                }
            }
            else
            {
                _scrollBarPositionindex = 0;
            }
        }

        /// <summary>
        /// Obtiene el valor de las propiedades rowIndex, y scrollBarPositionindex del <see cref="DataGridView"/> de Notificaciones,
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
        /// Obtiene el valor de la propiedad currentCellIndex del <see cref="DataGridView"/> de Notificaciones,
        /// cuando el usuario selecciona una celda.
        /// </summary>
        /// <param name="currentCellIndex">El índice de la celda seleccionada.</param>
        private void SetCurrentCellIndex(int currentCellIndex)
        {
            this._currentCellIndex = currentCellIndex;
        }

        #region Eventos

        /// <summary>
        /// Evento que carga la ventana principal del programa, ademas de las notificaciones del sistema.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Main_Load(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<Ventas>().FirstOrDefault();
            _ventas = form ?? new Ventas();
            AddFormInPanel(_ventas);
            _ventas.Focus();
            _ventas.textBoxCodigo.Focus();

            if (this._screensEnoughHeight)
            {
                await this.ObtenNotificaciones();
            }
        }

        /// <summary>
        /// Evento que carga la forma Venta en el panel principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonVenta_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<Ventas>().FirstOrDefault();
            _ventas = form ?? new Ventas();
            AddFormInPanel(_ventas);
            panelMove.Top = buttonVenta.Top;
            panelMove.Height = buttonVenta.Height;
            _ventas.textBoxCodigo.Focus();
        }

        /// <summary>
        /// Evento que carga la forma Productos en el panel principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonProductos_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<Productos>().FirstOrDefault();
            Productos productos = form ?? new Productos();
            AddFormInPanel(productos);
            panelMove.Top = buttonProductos.Top;
            panelMove.Height = buttonProductos.Height;
        }

        /// <summary>
        /// Evento que carga la forma Venta_Dia en el panel principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonVentaDia_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ventas_Dia());
            panelMove.Top = buttonVentaDia.Top;
            panelMove.Height = buttonVentaDia.Height;
        }

        /// <summary>
        /// Evento que carga la forma Ventas_Hechas en el panel principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonVentasHechas_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ventas_Hechas());
            panelMove.Top = buttonVentasHechas.Top;
            panelMove.Height = buttonVentasHechas.Height;
        }

        /// <summary>
        /// Evento que carga la forma Requisicion en el panel principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRequisicion_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<Requisición_Producto>().FirstOrDefault();
            _requisicionProducto = form ?? new Requisición_Producto();
            AddFormInPanel(_requisicionProducto);
            panelMove.Top = buttonRequisicion.Top;
            panelMove.Height = buttonRequisicion.Height;
        }

        /// <summary>
        /// Evento que carga la forma Notificaciones en el panel principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNotificaciones_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Notificaciones());
            panelMove.Top = buttonNotificaciones.Top;
            panelMove.Height = buttonNotificaciones.Height;
        }

        /// <summary>
        /// Evento que checa que la forma Venta no tenga artículos en proceso de venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (_ventas.dataGridViewVenta.RowCount > 0)
            {
                if (!panelPrincipal.Controls.Contains(_ventas))
                {
                    buttonVenta.PerformClick();
                }

                if (MessageBox.Show("Aún tiene productos en venta. ¿Desea cerrar el programa?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que actualiza los valores de rowIndex, currentCellIndex, y scrollBarPositionIndex, cuando el usuario ingresa a un nuevo renglón del <see cref="DataGridView"/> de Notificaciones.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewNotificaciones_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewNotificaciones.SelectedRows.Count > 0)
            {
                this.SetRowAndScrollBarIndex(this.dataGridViewNotificaciones.SelectedRows[0].Index, this.dataGridViewNotificaciones.FirstDisplayedScrollingRowIndex);
                this.SetCurrentCellIndex(this.dataGridViewNotificaciones.CurrentCell.ColumnIndex);
            }
        }

        /// <summary>
        /// Evento que actualiza el valor de scrollBarPositionIndex, cuando el usuario scrollea sobre el <see cref="DataGridView"/> de Notificaciones.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewNotificaciones_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                _scrollBarPositionindex = this.dataGridViewNotificaciones.FirstDisplayedScrollingRowIndex;
            }
        }

        /// <summary>
        /// Evento que abre la forma de Notificaciones, al hacer doble click sobre un renglón del <see cref="DataGridView"/> de Notificaciones, y muestra información detallada del mensaje seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewNotificaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!(this.panelPrincipal.Controls[0] is Notificaciones))
                {
                    this.buttonNotificaciones.PerformClick();
                }

                Notificaciones notificaciones = (Notificaciones)this.panelPrincipal.Controls[0];

                notificaciones.textBoxNoMensaje.Text = this.dataGridViewNotificaciones.Rows[_rowIndex].Cells["No."].Value.ToString();
                notificaciones.MuestraInformacionMensajeSeleccionado();
            }
        }

        /// <summary>
        /// Evento que actualiza los valores de rowIndex, currentCellIndex, y scrollBarPositionIndex, cuando el usuario da click en una celda de un renglón del <see cref="DataGridView"/> de Notificaciones.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewNotificaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.SetRowAndScrollBarIndex(e.RowIndex, this.dataGridViewNotificaciones.FirstDisplayedScrollingRowIndex);
                this.SetCurrentCellIndex(e.ColumnIndex);
            }
        }

        /// <summary>
        /// Evento que muestra o desaparece las notificaciones al usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowNot_Click(object sender, EventArgs e)
        {
            if (this.buttonShowNot.Text.Equals("Mostrar Notificaciones"))
            {
                this.buttonShowNot.Text = "Ocultar Notificaciones";
                this.Size = new Size(this.Width, 850);
                this.buttonNotificaciones.Visible = true;
                this.lblNotificaciones.Visible = true;
                this.dataGridViewNotificaciones.Visible = true;
                this.CenterToScreen();
            }
            else
            {
                this.buttonShowNot.Text = "Mostrar Notificaciones";
                this.buttonNotificaciones.Visible = false;
                this.lblNotificaciones.Visible = false;
                this.dataGridViewNotificaciones.Visible = false;
                this.Size = new Size(this.Width, 720);
                this.CenterToScreen();

                if (this.panelPrincipal.Controls[0] is Notificaciones)
                {
                    buttonVenta.PerformClick();
                }
            }
        }

        #endregion

        #endregion
    }
}
