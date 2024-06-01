using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace P2U6_232310307
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTarea.Text))
            {
                listBox1.Items.Add(txtTarea.Text);
                txtTarea.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa una tarea válida.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una tarea para eliminar.");
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("No hay tareas para guardar. Por favor, ingrese una tarea.");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Guardar archivo de tareas";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            foreach (var item in listBox1.Items)
                            {
                                writer.WriteLine(item.ToString());
                            }
                        }
                        MessageBox.Show("Tareas guardadas exitosamente.");
                        listBox1.Items.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al guardar las tareas: " + ex.Message);
                    }
                }
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Abrir archivo de tareas";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        listBox1.Items.Clear();
                        using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                        {
                            string line;
                            bool hasTasks = false;
                            while ((line = reader.ReadLine()) != null)
                            {
                                listBox1.Items.Add(line);
                                hasTasks = true;
                            }

                            if (!hasTasks)
                            {
                                MessageBox.Show("No hay tareas guardadas en el archivo.");
                            }
                            else
                            {
                                MessageBox.Show("Tareas cargadas exitosamente.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al abrir las tareas: " + ex.Message);
                    }
                }
            }
        }   
    }
}
