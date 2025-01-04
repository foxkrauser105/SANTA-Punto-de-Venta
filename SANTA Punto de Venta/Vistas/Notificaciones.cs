using JR.Utils.GUI.Forms;
using SANTA_Punto_de_Venta.Vistas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Notificaciones : Form
    {

        #region Variables
        private readonly System.Timers.Timer _inputTimer = new System.Timers.Timer(500);
        private const string _tipoCambioProducto = "Cambio en producto";
        private bool _notificacionGuardada = false;
        #endregion

        #region Notificaciones
        public Notificaciones()
        {
            InitializeComponent();
            _inputTimer.Elapsed += InputTimer_Elapsed;
        }

        #region Trabajo en Segundo plano

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
        /// Verifica que los campos requeridos de guardado tengan datos.
        /// </summary>
        /// <returns><c>true</c> si algún campo requerido está vacio. De otra manera, <c>false</c>.</returns>
        private bool VerificarCamposVacios()
        {
            return string.IsNullOrEmpty(textBoxNoMensaje.Text) || comboBoxTipo.SelectedIndex < 0 || comboBoxPrioridad.SelectedIndex < 0 || 
                   string.IsNullOrEmpty(textBoxMensaje.Text) || (comboBoxTipo.SelectedItem.ToString() == _tipoCambioProducto && string.IsNullOrEmpty(textBoxProducto.Text));
        }

        /// <summary>
        /// Limpia los campos de guardado en la forma.
        /// </summary>
        private void LimpiarCamposDeGuardado()
        {
            textBoxNoMensaje.Text = textBoxMensaje.Text = textBoxProducto.Text = textBoxProductoDesc.Text = textBoxUsuario.Text = textBoxUsuarioDesc.Text = textBoxRecibe.Text = textBoxRecibeDesc.Text = string.Empty;
            comboBoxPrioridad.SelectedIndex = comboBoxTipo.SelectedIndex = -1;
            textBoxStatus.Text = "Activo";
            dateTimePickerFechaAlta.Value = DateTime.Now;
            dateTimePickerFechaTermino.Value = DateTime.Parse("01/01/3000");

            buttonGuardar.Enabled = buttonInactivar.Enabled = buttonTerminado.Enabled = true;
            _notificacionGuardada = false;
        }

        /// <summary>
        /// Detecta si el usuario realizó alguna modificación a algún campo de guardado antes de realizar acciones como Terminar o Inactivar la notificación.
        /// </summary>
        /// <returns></returns>
        private bool VerificaNotificacionGuardada(bool esGuardado = false)
        {
            if (!_notificacionGuardada && !esGuardado)
            {
                MessageBox.Show("Debe guardar las modificaciones en la notificación para realizar la acción deseada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (_notificacionGuardada && esGuardado)
            {
                MessageBox.Show("Debe realizar alguna modificación en la notificación para guardar los cambios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return _notificacionGuardada;
        }

        /// <summary>
        /// Obtiene los valores de los campos de búsqueda, para realizar una consulta SQL y refrescar la tabla de notificaciones.
        /// </summary>
        /// <returns>Un <see cref="Dictionary{string, object}"/> con llaves (parámetros) y valores de filtrado.</returns>
        private Dictionary<string, object> ObtenerParametrosDeBusqueda()
        {
            return new Dictionary<string, object>
            {
                { "@IncluirInactivosTerminados", this.chkIncluirInactTerm.Checked },
                { "@IdMensaje", this.textBoxBuscaNoMensaje.Text },
                { "@Tipo", this.comboBoxBuscaTipo.SelectedItem?.ToString() ?? string.Empty },
                { "@Prioridad", this.comboBoxBuscaPrioridad.SelectedItem?.ToString() ?? string.Empty },
                { "@Status", this.comboBoxBuscaStatus.SelectedItem?.ToString() ?? string.Empty },
                { "@FechaAlta", this.dateTimePickerBuscaFechaAlta.Value.Date },
                { "@FechaTermino", this.dateTimePickerBuscaFechaTermino.Value.Date }
            };
        }

        /// <summary>
        /// Obtiene los valores de los campos de guardado, para realizar una inserción o actualización de un registro SQL.
        /// </summary>
        /// <returns>Un <see cref="Dictionary{string, object}"/> con llaves (parámetros) y valores de guardado.</returns>
        private Dictionary<string, object> ObtenerParametrosDeGuardado()
        {
            return new Dictionary<string, object>
            {
                { "@IdMensaje", this.textBoxNoMensaje.Text },
                { "@Tipo", this.comboBoxTipo.SelectedItem?.ToString() ?? string.Empty },
                { "@Prioridad", this.comboBoxPrioridad.SelectedItem?.ToString() ?? string.Empty },
                { "@Status", this.textBoxStatus.Text },
                { "@Mensaje", this.textBoxMensaje.Text },
                { "@IdProducto", this.textBoxProducto.Text },
                { "@Usuclave", this.textBoxUsuario.Text },
                { "@UsuclaveRecibe", this.textBoxRecibe.Text }
            };
        }

        /// <summary>
        /// Gets the data from the DB asyncronously.
        /// </summary>
        /// <param name="parameters"><see cref="Dictionary{string, object}"/> with params and values to filter data.</param>
        /// <returns><see cref="DataTable"/> with results filtered from DB.</returns>
        private async Task<DataTable> LoadData(Dictionary<string, object> parameters)
        {
            return await Task.Run(() => this.LoadNotificationsFromDB(parameters));
        }

        /// <summary>
        /// Método que se ejecuta al iniciar Notificaciones. Carga las notificaciones en el <see cref="DataGridView"/> inferior e inicializa campos con valores default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Notificaciones_Load(object sender, System.EventArgs e)
        {
            await this.InicializarNotificaciones();
        }

        /// <summary>
        /// Método que inicializa la ventana Notificaciones de manera asíncrona. Carga el <see cref="DataGridView"/> inferior e inicializa campos por default.
        /// </summary>
        /// <returns></returns>
        private async Task InicializarNotificaciones()
        {
            this.dateTimePickerBuscaFechaAlta.ValueChanged -= new System.EventHandler(this.dateTimePickerBuscaFechaAlta_ValueChanged);
            this.dateTimePickerBuscaFechaTermino.ValueChanged -= new System.EventHandler(this.dateTimePickerBuscaFechaTermino_ValueChanged);
            this.comboBoxBuscaStatus.SelectedIndexChanged -= new System.EventHandler(this.comboBoxBuscaStatus_SelectedIndexChanged);
            this.textBoxNoMensaje.Validated -= new System.EventHandler(this.textBoxNoMensaje_Validated);

            if (this.textBoxNoMensaje.TextLength == 0)
            {
                this.ObtenerSiguienteNumeroNotificacion();
            }

            this.dateTimePickerFechaAlta.Value = DateTime.Now;
            this.dateTimePickerBuscaFechaAlta.Value = DateTime.Parse("01/01/3000");
            this.dateTimePickerBuscaFechaTermino.Value = DateTime.Parse("01/01/3000");
            this.textBoxStatus.Text = "Activo";

            if (this.comboBoxBuscaStatus.Items.Count > 0)
            {
                this.comboBoxBuscaStatus.SelectedItem = "Activo";
            }

            this.dataGridViewNotificaciones.DataSource = await this.LoadData(this.ObtenerParametrosDeBusqueda());
            this.dataGridViewNotificaciones.AutoResizeColumns();

            if (this.dataGridViewNotificaciones.ColumnCount > 0)
            {
                this.dataGridViewNotificaciones.Columns[this.dataGridViewNotificaciones.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            this.panelCargandoMensajes.Visible = false;
            this.groupBoxBuscar.Visible = true;

            this.dateTimePickerBuscaFechaAlta.ValueChanged += new System.EventHandler(this.dateTimePickerBuscaFechaAlta_ValueChanged);
            this.dateTimePickerBuscaFechaTermino.ValueChanged += new System.EventHandler(this.dateTimePickerBuscaFechaTermino_ValueChanged);
            this.comboBoxBuscaStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxBuscaStatus_SelectedIndexChanged);
        }

        /// <summary>
        /// Método que recarga el <see cref="DataGridView"/> de notificaciones inferior, con filtros de búsqueda, cuando un valor en algún campo es cambiado.
        /// </summary>
        /// <returns></returns>
        private async Task RecargarNotificaciones()
        {
            this.dataGridViewNotificaciones.DataSource = await this.LoadData(this.ObtenerParametrosDeBusqueda());
        }

        /// <summary>
        /// Gets notifications from DB. Filters data according to values in <paramref name="parameters"/>.
        /// </summary>
        /// <param name="parameters"><see cref="Dictionary{string, object}"/> with key/value pairs to filter data.</param>
        /// <returns><see cref="DataTable"/> with notifications from DB.</returns>
        private DataTable LoadNotificationsFromDB(Dictionary<string, object> parameters)
        {

            string sqlQuery = @"SELECT id_mensaje [No. Mensaje], tipo [Tipo], prioridad [Prioridad], 
                                       CASE status 
                                           WHEN 0 THEN 'Inactivo'
                                           WHEN 1 THEN 'Activo'
                                           WHEN 2 THEN 'Terminado'
                                           ELSE 'Desconocido'
                                       END AS [Estatus],
                                       mensaje [Mensaje],
                                       fecha_alta [Fecha de Alta], fecha_termino [Fecha de Término],
                                       id_producto [Código], usuclave [Usuario], usuclave_recibe [Recibe]
                                FROM notificaciones
                                WHERE (   (    @IncluirInactivosTerminados = 1
                                           AND status = CASE @Status
                                                            WHEN 'Inactivo' THEN 0
                                                            WHEN 'Activo' THEN 1
                                                            WHEN 'Terminado' THEN 2
                                                            ELSE status
                                                        END)
                                       OR (    @IncluirInactivosTerminados = 0
                                           AND status = 1))
                                AND (     @IdMensaje = ''
                                       OR (    @IdMensaje <> ''
                                           AND id_mensaje = @IdMensaje))
                                AND (     @Tipo = ''
                                       OR (    @Tipo <> ''
                                           AND tipo = @Tipo))
                                AND (     @Prioridad = ''
                                       OR (    @Prioridad <> ''
                                           AND prioridad = @Prioridad))
                                AND (     @FechaAlta = '01/01/3000'
                                       OR (    @FechaAlta <> ''
                                           AND CAST(fecha_alta AS DATE) = @FechaAlta))
                                AND (     @FechaTermino = '01/01/3000'
                                       OR (    @FechaTermino <> ''
                                           AND CAST(fecha_termino AS DATE) = @FechaTermino))
                                ORDER BY id_mensaje;";

            return Utilerias.GetResultsFromQuery(sqlQuery, parameters);

        }

        #region Acciones

        /// <summary>
        /// Método que obtiene el número de la siguiente notificación a insertar.
        /// </summary>
        private void ObtenerSiguienteNumeroNotificacion()
        {

            string sqlQuery = @"SELECT IIF(MAX(id_mensaje) IS NULL, 1, MAX(id_mensaje) + 1) id_mensaje
                                FROM notificaciones;";

            DataTable dt = Utilerias.GetResultsFromQuery(sqlQuery);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Error al intentar obtener el número de mensaje siguiente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.LimpiarCamposDeGuardado();
            textBoxNoMensaje.Text = dt.Rows[0]["id_mensaje"].ToString();
            this.textBoxNoMensaje.Validated -= new System.EventHandler(this.textBoxNoMensaje_Validated);
            return;
        }

        /// <summary>
        /// Actualiza una notificación con el estatus suministrado.
        /// </summary>
        /// <param name="status">El estatus al cual se quiere actualizar la notificación</param>
        /// <returns></returns>
        private async Task ActualizaNotificacion(int status)
        {
            string sqlQuery = @"UPDATE notificaciones
                                SET status = @Status,
                                    fecha_termino = GETDATE()
                                WHERE id_mensaje = @IdMensaje;";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "@IdMensaje", this.textBoxNoMensaje.Text },
                    { "@Status", status }
                };

            if (await Utilerias.ExecuteQueryAsync(sqlQuery, parameters))
            {
                MessageBox.Show($"Notificación actualizada con éxito", "Acción ejecutada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ObtenerSiguienteNumeroNotificacion();
                await this.RecargarNotificaciones();
            }
        }

        /// <summary>
        /// Método que valida el número de mensaje escrito por el usuario y muestra la información relacionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MuestraInformacionMensajeSeleccionado(object sender = null, EventArgs e = null)
        {
            if (this.textBoxNoMensaje.TextLength > 0)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "@IncluirInactivosTerminados", true },
                    { "@IdMensaje", textBoxNoMensaje.Text },
                    { "@Tipo", string.Empty },
                    { "@Prioridad", string.Empty },
                    { "@Status", string.Empty },
                    { "@FechaAlta", DateTime.Parse("01/01/3000") },
                    { "@FechaTermino", DateTime.Parse("01/01/3000") }
                };

                DataTable dt = this.LoadNotificationsFromDB(parameters);

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ObtenerSiguienteNumeroNotificacion();
                    return;
                }

                comboBoxTipo.SelectedItem = dt.Rows[0]["Tipo"].ToString();
                comboBoxPrioridad.SelectedItem = dt.Rows[0]["Prioridad"].ToString();
                textBoxStatus.Text = dt.Rows[0]["Estatus"].ToString();
                dateTimePickerFechaAlta.Value = (DateTime)dt.Rows[0]["Fecha de Alta"];
                textBoxMensaje.Text = dt.Rows[0]["Mensaje"].ToString();
                dateTimePickerFechaTermino.Value = dt.Rows[0]["Fecha de Término"] == DBNull.Value ? DateTime.Parse("01/01/3000") : (DateTime)dt.Rows[0]["Fecha de Término"];
                textBoxProducto.Text = dt.Rows[0]["Código"].ToString();
                textBoxUsuario.Text = dt.Rows[0]["Usuario"].ToString();
                textBoxRecibe.Text = dt.Rows[0]["Recibe"].ToString();
                this.textBoxProducto_Validated(sender, e);
                this.textBoxUsuario_Validated(sender, e);
                this.textBoxRecibe_Validated(sender, e);

                bool activarBotones = textBoxStatus.Text == "Activo";

                buttonGuardar.Enabled = buttonInactivar.Enabled = buttonTerminado.Enabled = activarBotones;

                _notificacionGuardada = true;
            }
            else
            {
                this.LimpiarCamposDeGuardado();
            }

            this.textBoxNoMensaje.Validated -= new System.EventHandler(this.textBoxNoMensaje_Validated);
        }

        /// <summary>
        /// Método que guarda la información en los campos de guardado como notificación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (VerificaNotificacionGuardada(true))
            {
                return;
            }

            if (this.VerificarCamposVacios())
            {
                MessageBox.Show($"Verificar que los campos superiores no estén vacios para poder guardar los datos.\nSi el tipo de notificación es '{_tipoCambioProducto}', verificar que el campo 'Producto' también sea llenado.", "Verificar campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("¿Desea guardar la notificación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlQuery = @"UPDATE notificaciones
                                    SET tipo = @Tipo,
                                        prioridad = @Prioridad,
                                        mensaje = @Mensaje,
                                        id_producto = @IdProducto,
                                        usuclave = @Usuclave,
                                        usuclave_recibe = @UsuclaveRecibe
                                    WHERE id_mensaje = @IdMensaje;

                                    IF @@ROWCOUNT = 0
                                    BEGIN
                                                                                 
                                        INSERT INTO notificaciones (tipo, prioridad, status, mensaje, fecha_alta, id_producto, usuclave, usuclave_recibe)
                                        VALUES (@Tipo, @Prioridad, 1, @Mensaje, GETDATE(), @IdProducto, @Usuclave, @UsuclaveRecibe);

                                    END";

                if (await Utilerias.ExecuteQueryAsync(sqlQuery, this.ObtenerParametrosDeGuardado()))
                {
                    MessageBox.Show($"Notificación guardada con éxito", "Acción ejecutada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ObtenerSiguienteNumeroNotificacion();
                    await this.RecargarNotificaciones();
                }
            }
        }

        /// <summary>
        /// Método que actualiza el estatus de la notificación a Terminado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonTerminado_Click(object sender, EventArgs e)
        {
            if (!this.VerificaNotificacionGuardada())
            {
                return;
            }

            if (MessageBox.Show("¿Desea actualizar la notificación y terminarla? Tenga en cuenta que una vez en estatus Terminado NO se puede volver a poner en estatus Activo.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await this.ActualizaNotificacion(2);
            }
        }

        /// <summary>
        /// Método que actualiza el estatus de la notificación a Inactivo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonInactivar_Click(object sender, EventArgs e)
        {
            if (!this.VerificaNotificacionGuardada())
            {
                return;
            }

            if (MessageBox.Show("¿Desea actualizar la notificación e inactivarla? Tenga en cuenta que una vez en estatus Inactivo NO se puede volver a poner en estatus Activo.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await this.ActualizaNotificacion(0);
            }
        }

        #endregion

        #region Eventos de Notificaciones

        /// <summary>
        /// Recarga las notificaciones, filtrando registros terminados o cancelados, dependiendo del valor del <see cref="CheckBox"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void chkIncluirInactTerm_CheckedChanged(object sender, System.EventArgs e)
        {
            this.comboBoxBuscaStatus.Enabled = this.chkIncluirInactTerm.Checked;

            this.comboBoxBuscaStatus.SelectedIndexChanged -= new System.EventHandler(this.comboBoxBuscaStatus_SelectedIndexChanged);
            this.comboBoxBuscaStatus.SelectedItem = !this.chkIncluirInactTerm.Checked ? "Activo" : string.Empty;
            this.comboBoxBuscaStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxBuscaStatus_SelectedIndexChanged);


            await this.RecargarNotificaciones();
        }

        /// <summary>
        /// Inhabilita la búsqueda de registros con fecha de término, y habilita la búsqueda de registros con fecha de alta. Recarga las notificaciones con filtrado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxFechaAlta_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxFechaAlta.Checked)
            {
                this.checkBoxFechaTermino.CheckedChanged -= new System.EventHandler(this.checkBoxFechaTermino_CheckedChanged);
                this.dateTimePickerBuscaFechaTermino.ValueChanged -= new System.EventHandler(this.dateTimePickerBuscaFechaTermino_ValueChanged);
                this.checkBoxFechaTermino.Checked = !this.checkBoxFechaAlta.Checked;

                this.dateTimePickerBuscaFechaTermino.Value = DateTime.Parse("01/01/3000");
                this.dateTimePickerBuscaFechaTermino.Enabled = !this.checkBoxFechaAlta.Checked;
                this.checkBoxFechaTermino.CheckedChanged += new System.EventHandler(this.checkBoxFechaTermino_CheckedChanged);
                this.dateTimePickerBuscaFechaTermino.ValueChanged += new System.EventHandler(this.dateTimePickerBuscaFechaTermino_ValueChanged);
            }

            this.dateTimePickerBuscaFechaAlta.Enabled = this.checkBoxFechaAlta.Checked;
            this.dateTimePickerBuscaFechaAlta.Value = this.checkBoxFechaAlta.Checked ? DateTime.Now : DateTime.Parse("01/01/3000");
        }

        /// <summary>
        /// Inhabilita la búsqueda de registros con fecha de alta, y habilita la búsqueda de registros con fecha de término. Recarga las notificaciones con filtrado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxFechaTermino_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxFechaTermino.Checked)
            {
                this.checkBoxFechaAlta.CheckedChanged -= new System.EventHandler(this.checkBoxFechaAlta_CheckedChanged);
                this.dateTimePickerBuscaFechaAlta.ValueChanged -= new System.EventHandler(this.dateTimePickerBuscaFechaAlta_ValueChanged);
                this.checkBoxFechaAlta.Checked = !this.checkBoxFechaTermino.Checked;

                this.dateTimePickerBuscaFechaAlta.Value = DateTime.Parse("01/01/3000");
                this.dateTimePickerBuscaFechaAlta.Enabled = !this.checkBoxFechaTermino.Checked;
                this.checkBoxFechaAlta.CheckedChanged += new System.EventHandler(this.checkBoxFechaAlta_CheckedChanged);
                this.dateTimePickerBuscaFechaAlta.ValueChanged += new System.EventHandler(this.dateTimePickerBuscaFechaAlta_ValueChanged);
            }

            this.dateTimePickerBuscaFechaTermino.Enabled = this.checkBoxFechaTermino.Checked;
            this.dateTimePickerBuscaFechaTermino.Value = this.checkBoxFechaTermino.Checked ? DateTime.Now : DateTime.Parse("01/01/3000");
        }

        /// <summary>
        /// Recarga las notificaciones con filtrado al escribir el número de mensaje deseado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void textBoxBuscaNoMensaje_TextChanged(object sender, EventArgs e)
        {
            _inputTimer.Stop();
            _inputTimer.Enabled = true;
            _inputTimer.Start();

            int delay = (int)_inputTimer.Interval;
            await Task.Delay(delay);

            if (_inputTimer.Enabled)
            {
                return;
            }

            await this.RecargarNotificaciones();
        }

        /// <summary>
        /// Recarga las notificaciones con filtrado al cambiar de tipo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void comboBoxBuscaTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            await this.RecargarNotificaciones();
        }

        /// <summary>
        /// Recarga las notificaciones con filtrado al cambiar de prioridad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void comboBoxBuscaPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            await this.RecargarNotificaciones();
        }

        /// <summary>
        /// Recarga las notificaciones con filtrado al cambiar de estatus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void comboBoxBuscaStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            await this.RecargarNotificaciones();
        }

        /// <summary>
        /// Recarga las notificaciones con filtrado al cambiar la fecha de alta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void dateTimePickerBuscaFechaAlta_ValueChanged(object sender, EventArgs e)
        {
            await this.RecargarNotificaciones();
        }

        /// <summary>
        /// Recarga las notificaciones con filtrado al cambiar la fecha de término.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void dateTimePickerBuscaFechaTermino_ValueChanged(object sender, EventArgs e)
        {
            await this.RecargarNotificaciones();
        }

        /// <summary>
        /// Método que valida el número de mensaje escrito por el usuario y muestra la información relacionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNoMensaje_Validated(object sender, EventArgs e)
        {
            this.MuestraInformacionMensajeSeleccionado(sender, e);
        }

        /// <summary>
        /// Método que activa el evento Validated del campo Número de Mensaje, para validar el dato en él.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNoMensaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.textBoxNoMensaje.Validated += new System.EventHandler(this.textBoxNoMensaje_Validated);
            e.Handled = Utilerias.CaracterEsNumero(e.KeyChar);
        }

        /// <summary>
        /// Método que envía el Número de mensaje del <see cref="DataGridView"/> al campo Número de mensaje y mostrar detalladamente la información de la notificación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewNotificaciones_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.textBoxNoMensaje.Validated += new System.EventHandler(this.textBoxNoMensaje_Validated);
            this.textBoxNoMensaje.Text = this.dataGridViewNotificaciones.Rows[e.RowIndex].Cells["No. Mensaje"].Value.ToString();
            this.textBoxNoMensaje_Validated(sender, e);
        }

        /// <summary>
        /// Método que muestra la lista de valores para el campo que lo invoca, al presionar F9.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Utilerias.CaracterEsNumero(e.KeyChar);
            _notificacionGuardada = !e.Handled;
        }

        /// <summary>
        /// Evento usado para abrir la lista de valores asociados al campo al presionar F9.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                Utilerias.ValidarDatos(this.textBoxProducto, this.textBoxProductoDesc, usuarioBusca: true);
                _notificacionGuardada = false;
            }

        }

        /// <summary>
        /// Método que valida la información escrita en el campo que lo invoca, y en caso de no tener un valor correcto, muestra una lista de valores correctos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxProducto_Validated(object sender, EventArgs e)
        {
            if (this.textBoxProducto.TextLength > 0)
            {
                Utilerias.ValidarDatos(this.textBoxProducto, this.textBoxProductoDesc);
            }
            else
            {
                this.textBoxProductoDesc.Text = string.Empty;
            }
        }

        /// <summary>
        /// Método que valida la información escrita en el campo que lo invoca, y en caso de no tener un valor correcto, muestra una lista de valores correctos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxUsuario_Validated(object sender, EventArgs e)
        {
            if (this.textBoxUsuario.TextLength > 0)
            {
                Utilerias.ValidarDatos(this.textBoxUsuario, this.textBoxUsuarioDesc);
            }
            else
            {
                this.textBoxUsuarioDesc.Text = string.Empty;
            }
        }

        /// <summary>
        /// Método que valida la información escrita en el campo que lo invoca, y en caso de no tener un valor correcto, muestra una lista de valores correctos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxRecibe_Validated(object sender, EventArgs e)
        {
            if (this.textBoxRecibe.TextLength > 0)
            {
                Utilerias.ValidarDatos(this.textBoxRecibe, this.textBoxRecibeDesc);
            }
            else
            {
                this.textBoxRecibeDesc.Text = string.Empty;
            }
        }

        /// <summary>
        /// Evento usado para abrir la lista de valores asociados al campo al presionar F9.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                Utilerias.ValidarDatos(this.textBoxUsuario, this.textBoxUsuarioDesc, usuarioBusca: true);
                _notificacionGuardada = false;
            }
        }

        /// <summary>
        /// Evento usado para abrir la lista de valores asociados al campo al presionar F9.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxRecibe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                Utilerias.ValidarDatos(this.textBoxRecibe, this.textBoxRecibeDesc, usuarioBusca: true);
                _notificacionGuardada = false;
            }
        }

        /// <summary>
        /// Evento usado para identificar si el valor del campo ha sido modificado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxUsuario_TextChanged(object sender, EventArgs e)
        {
            _notificacionGuardada = false;
        }

        /// <summary>
        /// Evento usado para identificar si el valor del campo ha sido modificado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxRecibe_TextChanged(object sender, EventArgs e)
        {
            _notificacionGuardada = false;
        }

        /// <summary>
        /// Evento que modifica la variable _notificacionGuardada al momento que el usuario escribe o cambia la opción del campo que lo implementa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _notificacionGuardada = false;
        }

        /// <summary>
        /// Evento que modifica la variable _notificacionGuardada al momento que el usuario escribe o cambia la opción del campo que lo implementa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            _notificacionGuardada = false;
        }

        /// <summary>
        /// Evento que modifica la variable _notificacionGuardada al momento que el usuario escribe o cambia la opción del campo que lo implementa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxMensaje_TextChanged(object sender, EventArgs e)
        {
            _notificacionGuardada = false;
        }

        #endregion

        #endregion

    }
}
