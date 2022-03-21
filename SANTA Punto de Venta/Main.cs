using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    public partial class Main : Form
    {
        Ventas ventas;
        Requisición_Producto requisicion_Producto;

        DataTable dtNotificaciones = new DataTable();
        private int rowIndex = 0;
  
        public Main()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX", false);
        }

        /// <summary>
        /// Metodo que añade una forma al panel al hacer click en uno de los botones del menú principal
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

        private void Main_Load(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<Ventas>().FirstOrDefault();
            ventas = form ?? new Ventas();
            AddFormInPanel(ventas);
            ventas.Focus();
            ventas.textBoxCodigo.Focus();

            seleccionDB();

            //timerNotificaciones.Start();

        }

        private void buttonVenta_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<Ventas>().FirstOrDefault();
            ventas = form ?? new Ventas();
            AddFormInPanel(ventas);
            panelMove.Top = buttonVenta.Top;
            panelMove.Height = buttonVenta.Height;
            ventas.textBoxCodigo.Focus();
        }

        private void buttonProductos_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<Productos>().FirstOrDefault();
            Productos productos = form ?? new Productos();
            AddFormInPanel(productos);
            panelMove.Top = buttonProductos.Top;
            panelMove.Height = buttonProductos.Height;
        }

        private void buttonVentaDia_Click(object sender, EventArgs e)
        {           
            AddFormInPanel(new Venta_Dia());
            panelMove.Top = buttonVentaDia.Top;
            panelMove.Height = buttonVentaDia.Height;
        }

        private void buttonVentasHechas_Click(object sender, EventArgs e)
        {
            AddFormInPanel(new Ventas_Hechas());
            panelMove.Top = buttonVentasHechas.Top;
            panelMove.Height = buttonVentasHechas.Height;
        }

        private void buttonRequisicion_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<Requisición_Producto>().FirstOrDefault();
            requisicion_Producto = form ?? new Requisición_Producto();
            AddFormInPanel(requisicion_Producto);
            panelMove.Top = buttonRequisicion.Top;
            panelMove.Height = buttonRequisicion.Height;
        }

        private void buttonNotificaciones_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<Requisición_Producto>().FirstOrDefault();
            requisicion_Producto = form ?? new Requisición_Producto();
            AddFormInPanel(requisicion_Producto);
            panelMove.Top = buttonNotificaciones.Top;
            panelMove.Height = buttonNotificaciones.Height;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (ventas.dataGridViewVenta.RowCount > 0)
            {
                if (!panelPrincipal.Controls.Contains(ventas))
                {
                    buttonVenta.PerformClick();
                }
                
                if(MessageBox.Show("Aún tiene productos en venta. ¿Desea cerrar el programa?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            if (File.Exists(@"..\..\..\documentos\conexiontest.txt"))
               File.Delete(@"..\..\..\documentos\conexiontest.txt");
        }

        

        private void timerNotificaciones_Tick(object sender, EventArgs e)
        {
            //actualizaNotificaciones();
        }

        public void actualizaNotificaciones()
        {
            try
            {
                using (SqlConnection openCon = new SqlConnection(Properties.Settings.Default.SANTA_Connection))
                {
                    openCon.Open();
                    SqlTransaction transaction = openCon.BeginTransaction();
                    SqlDataAdapter select = new SqlDataAdapter(new SqlCommand("SELECT mensaje " +
                                                                              "FROM   notificaciones " +
                                                                              "WHERE  status <> 0 " +
                                                                              "ORDER BY prioridad desc, tipo", openCon, transaction));
                    dtNotificaciones.Clear();
                    select.Fill(dtNotificaciones);

                    dataGridViewNotificaciones.DataSource = dtNotificaciones;

                    if (dataGridViewNotificaciones.RowCount > 0)
                    {
                        if(rowIndex >= dataGridViewNotificaciones.RowCount)
                        {
                            rowIndex = dataGridViewNotificaciones.RowCount - 1;
                        }
                        
                        dataGridViewNotificaciones.CurrentCell = dataGridViewNotificaciones.Rows[rowIndex].Cells[0];
                        dataGridViewNotificaciones.Rows[rowIndex].Selected = true;

                    }
                    else
                    {
                        rowIndex = 0;
                    }
                }
            }
            catch (SqlException)
            {
                timerNotificaciones.Stop();
                MessageBox.Show("Ha ocurrido un problema. Verifica lo siguiente: \n\n- Verifica la conexión a la base de datos y prueba de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timerNotificaciones.Start();
            }

            catch (ArgumentOutOfRangeException)
            {
                timerNotificaciones.Stop();
                MessageBox.Show("Ha ocurrido un problema. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timerNotificaciones.Start();
            }
        }

        private void dataGridViewNotificaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = dataGridViewNotificaciones.SelectedCells[0].RowIndex;
        }

        private void buttonShowNot_Click(object sender, EventArgs e)
        {
            if(buttonShowNot.Text.Equals("Mostrar Notificaciones"))
            {
                buttonShowNot.Text = "Ocultar Notificaciones";
                this.Size = new Size(this.Width,850);
                this.CenterToScreen();
            }
            else
            {
                buttonShowNot.Text = "Mostrar Notificaciones";
                this.Size = new Size(this.Width, 720);
                this.CenterToScreen();
            }
        }

        public void seleccionDB()
        {
            ManagementObjectSearcher buscaProcesador = new ManagementObjectSearcher("SELECT Description FROM Win32_DisplayConfiguration");
            foreach (ManagementObject mo in buscaProcesador.Get())
            {
                foreach (PropertyData propiedades in mo.Properties)
                {
                    //Asi identifico si la instancia es de producción o de pruebas
                    //Mi PC de pruebas es 
                    if (propiedades.Value.ToString().Equals("NVIDIA GeForce GTX 1050"))
                    {

                        ConexionDBSeleccion a = new ConexionDBSeleccion();
                        a.ShowDialog();

                        try
                        {
                            if (!Directory.Exists(@"..\..\..\documentos")) { Directory.CreateDirectory(@"..\..\..\documentos"); }
                            if (!File.Exists(@"..\..\..\documentos\conexiontest.txt")) { using (var file = File.Create(@"..\..\..\documentos\conexiontest.txt")) { } }

                            using (StreamWriter sw = new StreamWriter(@"..\..\..\documentos\conexiontest.txt"))
                            {

                                sw.WriteLine(a.db == 0 ? "server = localhost\\SQLEXPRESS; database = SANTA; Trusted_Connection = True;" : "server = 192.168.100.7\\SQLEXPRESS; database = SANTA; uid = sa; pwd = 1234");

                                sw.Close();
                            }
                        }
                        catch (IOException)
                        {
                            MessageBox.Show("Ha ocurrido un error. El archivo 'conexiontest.txt' no puedo ser cargado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Dispose();
                        }
                    }
                }
            }
        }
    }
}
