using System;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class ConexionDBSeleccion : Form
    {
        public int db = 0; // 0 pruebas, 1 produccion
        public ConexionDBSeleccion()
        {
            InitializeComponent();
        }

        private void ConexionDBSeleccion_Load(object sender, EventArgs e)
        {
            comboBoxDB.SelectedIndex = 0;
        }

        private void comboBoxDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDB.SelectedIndex == 0)
            {
                db = 0;
            }
            else if (comboBoxDB.SelectedIndex == 1)
            {
                db = 1;
            }
        }

        private void buttonAccion_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
