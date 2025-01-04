using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Lista_Valores : Form
    {
        #region Variables
        private string _filtroBusqueda;
        private string _valorBusqueda;
        private DataTable _valores;

        public string Valor { get; set; }

        public string Descripcion { get; set; }
        #endregion

        #region Lista_Valores

        /// <summary>
        /// Clase que permite mostrar una lista de valores válidos para el campo que lo implemente.
        /// </summary>
        /// <param name="filtroBusqueda"><see cref="string"/> con el nombre del campo que lo manda llamar.</param>
        /// <param name="valorBusqueda"><see cref="string"/> con el texto del campo que lo manda llamar, usado para un pre-filtro en la forma.</param>
        public Lista_Valores(string filtroBusqueda, string valorBusqueda)
        {
            InitializeComponent();
            _filtroBusqueda = filtroBusqueda.ToLower().Replace("textbox", string.Empty);
            _valorBusqueda = valorBusqueda;
            this.labelValor.Text = filtroBusqueda.Replace("textBox", string.Empty);
            this.labelValor.Location = new Point((this.Width / 2) - (this.labelValor.Width / 2) + (this.labelListaValores.Width / 4), this.labelValor.Location.Y);
        }

        /// <summary>
        /// Método que inicializa el <see cref="DataGridView"/> con 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lista_Valores_Load(object sender, EventArgs e)
        {
            string sqlQuery = string.Empty;

            switch (_filtroBusqueda)
            {
                case "producto":
                case "codigo":
                    sqlQuery = @"SELECT id_producto [Código], nombre [Nombre]
                                 FROM productos
                                 WHERE status = 1;";
                    break;
                case "usuario":
                case "recibe":
                    sqlQuery = @"SELECT usuclave [Usuario], nombre + ' ' + aPaterno + ' ' + aMaterno [Nombre]
                                 FROM usuarios
                                 WHERE status = 1;";
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(sqlQuery))
            {
                this._valores = Utilerias.GetResultsFromQuery(sqlQuery);
                this.dataGridViewValores.DataSource = this._valores;

                if (!string.IsNullOrEmpty(_valorBusqueda))
                {
                    this.textBoxBuscar.Text = _valorBusqueda;
                }

                if (this.dataGridViewValores.Rows.Count == 1)
                {
                    this.buttonAceptar.PerformClick();
                    return;
                }

                if (this.dataGridViewValores.ColumnCount > 0)
                {
                    this.dataGridViewValores.AutoResizeColumns();
                    this.dataGridViewValores.Columns[this.dataGridViewValores.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            else
            {
                this.Valor = _valorBusqueda;
                MessageBox.Show($"No se encontró un criterio de búsqueda para el filtro '{this.labelValor.Text}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        #region Eventos

        /// <summary>
        /// Evento que filtra los resultados en la tabla al momento de escribir en el campo Buscar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            string rowFilter = string.Empty;

            if (this.textBoxBuscar.TextLength > 0)
            {
                switch (_filtroBusqueda)
                {
                    case "producto":
                    case "codigo":
                        rowFilter = $"Código LIKE '{this.textBoxBuscar.Text}%' OR Nombre LIKE '%{this.textBoxBuscar.Text}%'";
                        break;
                    case "usuario":
                    case "recibe":
                        rowFilter = $"Usuario LIKE '{this.textBoxBuscar.Text}%' OR Nombre LIKE '%{this.textBoxBuscar.Text}%'";
                        break;
                    default:
                        break;
                }
            }

            this._valores.DefaultView.RowFilter = rowFilter;

            if (this.dataGridViewValores.Rows.Count > 0)
            {
                this.dataGridViewValores.AutoResizeColumns();
                this.dataGridViewValores.Columns[this.dataGridViewValores.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        /// <summary>
        /// Evento que realiza un click en el botón aceptar, al dar doble click sobre una celda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewValores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.buttonAceptar.PerformClick();
        }

        /// <summary>
        /// Evento que cierra la forma y devuelve valores vacios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Valor = string.Empty;
            this.Descripcion = string.Empty;
            this.Dispose();
        }

        /// <summary>
        /// Evento que cierra la forma y devuelve los valores del renglón seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewValores.SelectedRows.Count > 0)
            {
                this.Valor = this.dataGridViewValores.SelectedRows[0].Cells[0].Value.ToString();
                this.Descripcion = this.dataGridViewValores.SelectedRows[0].Cells[1].Value.ToString();
                this.Dispose();
            }
        }

        /// <summary>
        /// Evento que checa si el caractér escrito en el campo Buscar es un caractér de búsqueda válido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Utilerias.CaracterValido(e.KeyChar);
        }

        #endregion

        #endregion
    }
}
