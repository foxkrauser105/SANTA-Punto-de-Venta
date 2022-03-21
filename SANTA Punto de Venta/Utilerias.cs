using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANTA_Punto_de_Venta
{
    /// <summary>
    /// Clase que alberga funciones usadas frecuentemente.
    /// </summary>
    class Utilerias
    {
        /// <summary>
        /// Método que agrega caractér de escape para consultas a BD
        /// </summary>
        /// <param name="s">Cadena original y que incluye ya el caractér de escape donde lo requiera</param>
        /// <returns></returns>
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

        /// <summary>
        /// Método que actualiza información de una tabla, dependiendo del query y el rowfilter enviado
        /// </summary>
        /// <param name="query">Query para traer información de BD</param>
        /// <param name="rowFilter">El filtrado de la tabla</param>
        /// <returns></returns>
        public DataTable ejecutaComando(string query, CommandType type, SqlConnection openCon, SqlTransaction transaction, string rowFilter = "", params object [][] p)
        {
            DataTable dtInfo = new DataTable();

            SqlCommand command = new SqlCommand(query, openCon, transaction);
            command.CommandType = type;

            for(int i = 0; i < p.Length; i++)
            {
                SqlDbType sqldbtype = new SqlDbType();

                if (p[i][1].GetType().Equals(typeof(int))) sqldbtype = SqlDbType.Int;
                else if (p[i][1].GetType().Equals(typeof(string))) sqldbtype = SqlDbType.VarChar;
                else if (p[i][1].GetType().Equals(typeof(double))) sqldbtype = SqlDbType.Float;
                else if (p[i][1].GetType().Equals(typeof(float))) sqldbtype = SqlDbType.Float;
                else if (p[i][1].GetType().Equals(typeof(DateTime))) sqldbtype = SqlDbType.DateTime;
                else sqldbtype = SqlDbType.VarChar;

                command.Parameters.Add(p[i][0].ToString(), sqldbtype).Value = p[i][1];
            }

            SqlDataAdapter select = new SqlDataAdapter(command);

            dtInfo.Clear();
            select.Fill(dtInfo);

            dtInfo.DefaultView.RowFilter = rowFilter;

            return dtInfo;
        }
    }
}
