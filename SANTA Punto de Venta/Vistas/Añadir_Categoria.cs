using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta.Vistas
{
    public partial class Añadir_Categoria : Form
    {
        public Añadir_Categoria()
        {
            InitializeComponent();
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            if (!textBoxCategoria.Text.Equals(""))
            {
                try
                {
                    if (!Directory.Exists(@"..\..\..\documentos")) { Directory.CreateDirectory(@"..\..\..\documentos"); }
                    //If file doesn't exist, create the file
                    if (!File.Exists(@"..\..\..\documentos\categoria.txt")) { using (var file = File.Create(@"..\..\..\documentos\categoria.txt")) { } }

                    //Compare if line already exists in file
                    string[] lines = File.ReadAllLines(@"..\..\..\documentos\categoria.txt");
                    int cont = 0;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].ToUpper().Trim().Equals(textBoxCategoria.Text.ToUpper().Trim()))
                        {
                            cont++;
                        }
                    }

                    if (cont > 0)
                    {
                        //If exists show to user
                        MessageBox.Show("La categoría introducida ya existe. Favor de verificar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //If yes, add line
                        if (MessageBox.Show("¿Estás seguro de añadir '" + textBoxCategoria.Text.Trim() + "' como categoría?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //Adding a new Category
                            //Researching for lines in file to rewrite file
                            using (StreamWriter sw = new StreamWriter(@"..\..\..\documentos\categoria.txt"))
                            {
                                //Using a list to add old lines and the new one
                                List<string> list = new List<string>();
                                list.AddRange(lines);
                                list.Add(textBoxCategoria.Text.Trim());

                                //Write the file line per line
                                for (int i = 0; i < list.Count; i++)
                                {
                                    sw.WriteLine(list[i]);
                                }

                                //If OK, close file and show a message
                                sw.Close();
                                MessageBox.Show("La categoría '" + textBoxCategoria.Text.Trim() + "' ha sido añadida satisfactoriamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBoxCategoria.Text = "";
                            }
                        }
                    }
                }
                catch (IOException) { MessageBox.Show("Ha ocurrido un error. El archivo 'categoria.txt' no puedo ser cargado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }
            else
            {
                //If blank
                MessageBox.Show("Una categoría válida debe ser introducida. Favor de no dejar espacios en blanco", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
