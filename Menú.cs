using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pokemon
{
    public partial class Menú : Form
    {

        string connectionString = "Server = localhost; Database = PokeRhapsody; Uid = root; Pwd=110818;";

        public Menú()
        {
            InitializeComponent();
            MostrarExpansiones();
        }

        private void MostrarExpansiones()
        {
            try
            {
                // Crear la conexión
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();  // Abrir la conexión

                    // Consulta SQL para obtener los datos de la tabla
                    string qry = "SELECT Nombre FROM expansiones"; // Ajusta esto a tu consulta
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(qry, conexion);

                    // Crear un DataTable para almacenar los resultados
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Limpiar el ComboBox antes de llenarlo
                    cbxExpansion.Items.Clear();

                    // Llenar el ComboBox con los datos de la columna
                    foreach (DataRow row in dataTable.Rows)
                    {
                        cbxExpansion.Items.Add(row["Nombre"].ToString()); // Ajusta el nombre de la columna
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar las expansiones : " + ex.Message);
            }
        }

        // Evento SelectedIndexChanged del ComboBox
        private void cbxExpansion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Comprobamos si se ha seleccionado una opción (puedes comprobar el valor de la selección)
            if (cbxExpansion.SelectedItem.ToString() == "Promos")
            {
                // Crear e inicializar el formulario secundario
                Promos Promos = new Promos();

                // Mostrar Promos 
                Promos.Show();

                // Esconde Menu, hasta que se cierre Promos
                this.Hide();

            }
            // Comprobamos si se ha seleccionado una opción (puedes comprobar el valor de la selección)
            else if (cbxExpansion.SelectedItem.ToString() == "Genes Formidables")
            {
                // Crear e inicializar el formulario secundario
                GenesFormidables GenesFormidables = new GenesFormidables();

                // Esconde Menu, asta que se cierre Genes
                this.Hide();

                // Mostrar Genes
                GenesFormidables.Show();

            }
            /* Comprobamos si se ha seleccionado una opción (puedes comprobar el valor de la selección)
        else if (cbxExpansion.SelectedItem.ToString() == "La Isla Singular")
        {
            // Crear e inicializar el formulario secundario
            GenesFormidables genesFormidables = new GenesFormidables();

            // Mostrar el formulario
            genesFormidables.Show();

        }
            // Comprobamos si se ha seleccionado una opción (puedes comprobar el valor de la selección)
        else if (cbxExpansion.SelectedItem.ToString() == "Pugna Espacio Temporal")
        {
            // Crear e inicializar el formulario secundario
            GenesFormidables genesFormidables = new GenesFormidables();

            // Mostrar el formulario
            genesFormidables.Show();

        }
            // Comprobamos si se ha seleccionado una opción (puedes comprobar el valor de la selección)
        else if (cbxExpansion.SelectedItem.ToString() == "Luz Triunfal")
        {
            // Crear e inicializar el formulario secundario
            GenesFormidables genesFormidables = new GenesFormidables();

            // Mostrar el formulario
            genesFormidables.Show();

        }*/

            else
            {
                MessageBox.Show("No se encontro la expansión");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Estas seguro de salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (r == DialogResult.Yes)
            {
                //this.Close();
                Application.Exit();
            }
            else
            {
                cbxExpansion.Focus();
            }
        }
    }
}
