using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

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

        private void cbxExpansion_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbxExpansion.SelectedItem.ToString() == "Promos")
            {

                Promos Promos = new Promos();
                Promos.Show();
                this.Hide();

            }
            
            else if (cbxExpansion.SelectedItem.ToString() == "Genes Formidables")
            { 

                GenesFormidables GenesFormidables = new GenesFormidables();
                this.Hide();
                GenesFormidables.Show();

            }
            
            else if (cbxExpansion.SelectedItem.ToString() == "La Isla Singular")
            {
                
                LaIslaSingular laislasingular = new LaIslaSingular();
                this.Hide();
                laislasingular.Show();

            }

            else
            {
                MessageBox.Show("No se encontro la expansión");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.Show();
            this.Hide();
        }

        private void btnCartas_Click(object sender, EventArgs e)
        {
            Total total = new Total();
            total.Show();
            this.Hide();
        }
    }
}
