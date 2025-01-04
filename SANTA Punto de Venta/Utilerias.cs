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
    /// <summary>
    /// Clase que alberga funciones usadas frecuentemente.
    /// </summary>
    static class Utilerias
    {
        /// <summary>
        /// Método que agrega caractér de escape para consultas a BD
        /// </summary>
        /// <param name="s">Cadena original y que incluye ya el caractér de escape donde lo requiera</param>
        /// <returns></returns>
        public static string VerifyQuotes(string s)
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

        /// <summary>
        /// Método que ejecuta un query en la base de datos y puede, o no, devolver una tabla, dependiendo del tipo de query.
        /// </summary>
        /// <param name="query">Query para traer información de BD</param>
        /// <param name="rowFilter">El filtrado de la tabla</param>
        /// <returns>Tabla con datos en caso de realizar un SELECT, de otra manera, <c>null</c>.</returns>
        public static DataTable EjecutaComando(string query, CommandType type, SqlConnection openCon, SqlTransaction transaction, string rowFilter = "", params object[][] p)
        {
            DataTable dtInfo = new DataTable();

            SqlCommand command = new SqlCommand(query, openCon, transaction)
            {
                CommandType = type
            };

            for (int i = 0; i < p.Length; i++)
            {
                SqlDbType sqldbtype = new SqlDbType();

                if (p[i][1].GetType().Equals(typeof(int))) sqldbtype = SqlDbType.Int;
                else if (p[i][1].GetType().Equals(typeof(string))) sqldbtype = SqlDbType.VarChar;
                else if (p[i][1].GetType().Equals(typeof(double))) sqldbtype = SqlDbType.Float;
                else if (p[i][1].GetType().Equals(typeof(float))) sqldbtype = SqlDbType.Float;
                else if (p[i][1].GetType().Equals(typeof(decimal))) sqldbtype = SqlDbType.Decimal;
                else if (p[i][1].GetType().Equals(typeof(DateTime))) sqldbtype = SqlDbType.DateTime;
                else if (p[i][1].GetType().Equals(typeof(bool))) sqldbtype = SqlDbType.Bit;
                else sqldbtype = SqlDbType.VarChar;

                command.Parameters.Add(p[i][0].ToString(), sqldbtype).Value = p[i][1];
            }

            SqlDataAdapter select = new SqlDataAdapter(command);

            dtInfo.Clear();
            select.Fill(dtInfo);

            dtInfo.DefaultView.RowFilter = rowFilter;

            return dtInfo;
        }

        private static DataTable EjecutaComando(string query, CommandType type, SqlConnection openCon, SqlTransaction transaction, Dictionary<string, object> p = null)
        {
            DataTable dtInfo = new DataTable();

            SqlCommand command = new SqlCommand(query, openCon, transaction)
            {
                CommandType = type
            };

            if (p != null && p.Count > 0)
            {
                foreach (KeyValuePair<string, object> parameter in p)
                {
                    SqlDbType sqldbtype = new SqlDbType();

                    if (parameter.Value.GetType().Equals(typeof(int))) sqldbtype = SqlDbType.Int;
                    else if (parameter.Value.GetType().Equals(typeof(string))) sqldbtype = SqlDbType.VarChar;
                    else if (parameter.Value.GetType().Equals(typeof(double))) sqldbtype = SqlDbType.Float;
                    else if (parameter.Value.GetType().Equals(typeof(float))) sqldbtype = SqlDbType.Float;
                    else if (parameter.Value.GetType().Equals(typeof(decimal))) sqldbtype = SqlDbType.Decimal;
                    else if (parameter.Value.GetType().Equals(typeof(DateTime))) sqldbtype = SqlDbType.DateTime;
                    else if (parameter.Value.GetType().Equals(typeof(bool))) sqldbtype = SqlDbType.Bit;
                    else sqldbtype = SqlDbType.VarChar;

                    command.Parameters.Add(parameter.Key, sqldbtype).Value = parameter.Value;
                }
            }

            SqlDataAdapter select = new SqlDataAdapter(command);

            dtInfo.Clear();
            select.Fill(dtInfo);

            return dtInfo;
        }

        /// <summary>
        /// Método que ejecuta un query en la base de datos y puede, o no, devolver una tabla, dependiendo del tipo de query, de manera asíncrona.
        /// </summary>
        /// <param name="query">Query para traer información de BD</param>
        /// <returns>Tabla con datos en caso de realizar un SELECT, de otra manera, <c>null</c>.</returns>
        private async static Task<DataTable> EjecutaComandoAsync(string query, CommandType type, SqlConnection openCon, SqlTransaction transaction, Dictionary<string, object> p = null)
        {
            DataTable dtInfo = new DataTable();

            SqlCommand command = new SqlCommand(query, openCon, transaction)
            {
                CommandType = type
            };

            if (p != null && p.Count > 0)
            {
                foreach (KeyValuePair<string, object> parameter in p)
                {
                    SqlDbType sqldbtype = new SqlDbType();

                    if (parameter.Value.GetType().Equals(typeof(int))) sqldbtype = SqlDbType.Int;
                    else if (parameter.Value.GetType().Equals(typeof(string))) sqldbtype = SqlDbType.VarChar;
                    else if (parameter.Value.GetType().Equals(typeof(double))) sqldbtype = SqlDbType.Float;
                    else if (parameter.Value.GetType().Equals(typeof(float))) sqldbtype = SqlDbType.Float;
                    else if (parameter.Value.GetType().Equals(typeof(decimal))) sqldbtype = SqlDbType.Decimal;
                    else if (parameter.Value.GetType().Equals(typeof(DateTime))) sqldbtype = SqlDbType.DateTime;
                    else if (parameter.Value.GetType().Equals(typeof(bool))) sqldbtype = SqlDbType.Bit;
                    else sqldbtype = SqlDbType.VarChar;

                    command.Parameters.Add(parameter.Key, sqldbtype).Value = parameter.Value;
                }
            }

            SqlDataAdapter select = new SqlDataAdapter(command);

            dtInfo.Clear();
            await Task.Run(() => select.Fill(dtInfo));

            return dtInfo;
        }

        /// <summary>
        /// Executes provided <paramref name="sqlQuery"/> in DB, with provided <paramref name="parameters"/>, if any.
        /// </summary>
        /// <param name="sqlQuery"><see cref="string"/> with SQL execution statement.</param>
        /// <param name="parameters"><see cref="Dictionary{string, object}"/> with key/value pairs to save data. Can be <c>null</c>.</param>
        public static bool ExecuteQuery(string sqlQuery, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {

                sqlCon.Open();
                SqlTransaction transaction = sqlCon.BeginTransaction();

                try
                {
                    EjecutaComando(sqlQuery,
                                   CommandType.Text,
                                   sqlCon,
                                   transaction,
                                   parameters);

                    transaction.Commit();

                    return true;
                }
                catch (SqlException sqlEx)
                {
                    transaction.Rollback();
                    FlexibleMessageBox.Show($"Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo.\n\nMás información:\n{sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return false;
        }

        /// <summary>
        /// Executes provided <paramref name="sqlQuery"/> in DB, with provided <paramref name="parameters"/>, if any, in an async way.
        /// </summary>
        /// <param name="sqlQuery"><see cref="string"/> with SQL execution statement.</param>
        /// <param name="parameters"><see cref="Dictionary{string, object}"/> with key/value pairs to save data. Can be <c>null</c>.</param>
        public async static Task<bool> ExecuteQueryAsync(string sqlQuery, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {

                await sqlCon.OpenAsync();
                SqlTransaction transaction = sqlCon.BeginTransaction();

                try
                {
                    await EjecutaComandoAsync(sqlQuery,
                                              CommandType.Text,
                                              sqlCon,
                                              transaction,
                                              parameters);

                    transaction.Commit();

                    return true;
                }
                catch (SqlException sqlEx)
                {
                    transaction.Rollback();
                    FlexibleMessageBox.Show($"Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo.\n\nMás información:\n{sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return false;
        }

        /// <summary>
        /// Get results from <paramref name="sqlQuery"/> in DB, with provided <paramref name="parameters"/>, if any.
        /// </summary>
        /// <param name="sqlQuery"><see cref="string"/> with SQL SELECT statement.</param>
        /// <param name="parameters"><see cref="Dictionary{string, object}"/> with key/value pairs to save data. Can be <c>null</c>.</param>
        public static DataTable GetResultsFromQuery(string sqlQuery, Dictionary<string, object> parameters = null)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {

                sqlCon.Open();
                SqlTransaction transaction = sqlCon.BeginTransaction();

                try
                {
                    dt = EjecutaComando(sqlQuery,
                                        CommandType.Text,
                                        sqlCon,
                                        transaction,
                                        parameters);

                }
                catch (SqlException sqlEx) { FlexibleMessageBox.Show($"Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo.\n\nMás información:\n{sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }

            return dt;
        }

        /// <summary>
        /// Get results from <paramref name="sqlQuery"/> in DB, with provided <paramref name="parameters"/>, if any, in an async way.
        /// </summary>
        /// <param name="sqlQuery"><see cref="string"/> with SQL SELECT statement.</param>
        /// <param name="parameters"><see cref="Dictionary{string, object}"/> with key/value pairs to save data. Can be <c>null</c>.</param>
        public async static Task<DataTable> GetResultsFromQueryAsync(string sqlQuery, Dictionary<string, object> parameters = null)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
            {

                await sqlCon.OpenAsync();
                SqlTransaction transaction = sqlCon.BeginTransaction();

                try
                {
                    dt = await EjecutaComandoAsync(sqlQuery,
                                                   CommandType.Text,
                                                   sqlCon,
                                                   transaction,
                                                   parameters);

                }
                catch (SqlException sqlEx) { FlexibleMessageBox.Show($"Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo.\n\nMás información:\n{sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }

            return dt;
        }

        /// <summary>
        /// Valida los datos suministrados en los campos que lo manden llamar en su validación o KeyPress.
        /// </summary>
        /// <param name="textBox">El campo con el valor único.</param>
        /// <param name="textBoxDesc">El campo con la descripción del valor único.</param>
        public static void ValidarDatos(TextBox textBox, TextBox textBoxDesc = null, bool usuarioBusca = false)
        {
            Dictionary<string, string> valores = MostrarListaValores(textBox.Name, !usuarioBusca ? textBox.Text : string.Empty);
            foreach (KeyValuePair<string, string> keyValuePair in valores)
            {
                textBox.Text = keyValuePair.Key;
                
                if (textBoxDesc != null)
                {
                    textBoxDesc.Text = keyValuePair.Value;
                }  
            }
        }

        /// <summary>
        /// Muestra una lista de valores válidos para el campo que manda llamar la función.
        /// </summary>
        /// <param name="valorBusqueda"><see cref="string"/> con el nombre del campo que invoca la función. Es importante para que la ventana sepa qué valores mostrar.</param>
        /// <param name="filtroBusqueda"><see cref="string"/> con el valor del campo que invoca la función. Es usado para filtrar de manera previa la tabla hacia el usuario.</param>
        /// <returns></returns>
        private static Dictionary<string, string> MostrarListaValores(string filtroBusqueda, string valorBusqueda)
        {
            Lista_Valores lov = new Lista_Valores(filtroBusqueda, valorBusqueda);
            lov.ShowDialog();
            return new Dictionary<string, string>()
            {
                { lov?.Valor ?? string.Empty, lov?.Descripcion ?? string.Empty }
            };
        }

        /// <summary>
        /// Determina si <paramref name="keyChar"/> es un caractér de búsqueda válido.
        /// </summary>
        /// <param name="keyChar">El caractér a checar.</param>
        /// <returns><c>true</c> si el caractér no es válido, de otra manera, <c>false</c>. Esto es para los eventos KeyPress, para la propiedad Handled.</returns>
        public static bool CaracterValido(char keyChar)
        {
            return !(char.IsNumber(keyChar) || char.IsControl(keyChar) || char.IsLetter(keyChar) || char.IsSeparator(keyChar) || keyChar.ToString().Equals("'") || keyChar.ToString().Equals("&") || keyChar.ToString().Equals("%"));
        }

        /// <summary>
        /// Determina si <paramref name="keyChar"/> es un caractér numérico.
        /// </summary>
        /// <param name="keyChar">El caractér a checar.</param>
        /// <returns><c>true</c> si el caractér no es numérico, de otra manera, <c>false</c>. Esto es para los eventos KeyPress, para la propiedad Handled.</returns>
        public static bool CaracterEsNumero(char keyChar)
        {
            return !(char.IsNumber(keyChar) || char.IsControl(keyChar));
        }
    }
}
