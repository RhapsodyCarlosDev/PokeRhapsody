using MySqlConnector;
using System;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Total: Form
    {
        public Total()
        {
            InitializeComponent();
            MostrarTotal();
        }
       
        private void MostrarTotal()
        {
            string connectionString = "Server=localhost;Database=pokerhapsody;Uid=root;Pwd=110818;";

            string query = "SELECT (SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) FROM promos) +" +
                                  "(SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) FROM pokerhapsody.genes_formidables WHERE NOMBRE != 'Ambar Viejo') +" +
                                  "(SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) FROM la_isla_singular) AS Total";

            string query2 = "SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) AS Total FROM promos";
            string query3 = "SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) AS Total FROM pokerhapsody.genes_formidables WHERE NOMBRE != 'Ambar Viejo'";
            string query4 = "SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) AS Total FROM la_isla_singular";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    object result = cmd.ExecuteScalar();
                    int total = 0;

                    if (result != null && result != DBNull.Value)
                    {
                        total = Convert.ToInt16(result);
                    }

                    lblTotal.Text = total.ToString();

                    MySqlCommand cmd2 = new MySqlCommand(query2, conexion);
                    object result2 = cmd2.ExecuteScalar();
                    int total2 = 0;

                    if (result2 != null && result2 != DBNull.Value)
                    {
                        total2 = Convert.ToInt16(result2);
                    }

                    lblTotalP.Text = total2.ToString();

                    MySqlCommand cmd3 = new MySqlCommand(query3, conexion);
                    object result3 = cmd3.ExecuteScalar();
                    int total3 = 0;

                    if (result3 != null && result3 != DBNull.Value)
                    {
                        total3 = Convert.ToInt16(result3);
                    }

                    lblTotalG.Text = total3.ToString();

                    MySqlCommand cmd4 = new MySqlCommand(query4, conexion);
                    object result4 = cmd4.ExecuteScalar();
                    int total4 = 0;

                    if (result4 != null && result4 != DBNull.Value)
                    {
                        total4 = Convert.ToInt16(result4);
                    }

                    lblTotalL.Text = total4.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    conexion.Close();
                }

                conexion.Close();
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Menú Menu = new Menú();

            this.Hide();

            Menu.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Estas seguro de salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

    }

}