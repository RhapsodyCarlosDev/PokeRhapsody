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
            string connectionString = "Server = SERVER; Database = DATABASE; Uid = USER ID; Pwd=PASSWORD;";

            string querytotal = "SELECT COALESCE ((SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) FROM promos), 0) +" +
                                  "COALESCE ((SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) FROM pokerhapsody.genes_formidables WHERE NOMBRE != 'Ambar Viejo'), 0) +" +
                                  "COALESCE ((SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) FROM la_isla_singular), 0) +" +
                                  "COALESCE ((SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) FROM pugna_espacio_temporal), 0) +" +
                                  "COALESCE ((SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) FROM luz_triunfal), 0) +" +
                                  "COALESCE ((SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) FROM festival_brillante), 0) AS Total";

            string querypromos = "SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) AS Total FROM promos";
            string querygenes = "SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) AS Total FROM pokerhapsody.genes_formidables WHERE NOMBRE != 'Ambar Viejo'";
            string queryisla = "SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) AS Total FROM la_isla_singular";
            string querypugna = "SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) AS Total FROM pugna_espacio_temporal";
            string queryluz = "SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) AS Total FROM luz_triunfal";
            string queryfestival = "SELECT SUM(ESP + ING + FRA + ALE + ITA + POR + JAP + KOR + CHI) AS Total FROM festival_brillante";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    MySqlCommand cmdtotal = new MySqlCommand(querytotal, conexion);
                    object resulttotal = cmdtotal.ExecuteScalar();
                    int total = 0;

                    if (resulttotal != null && resulttotal != DBNull.Value)
                    {
                        total = Convert.ToInt16(resulttotal);
                    }

                    lblTotal.Text = total.ToString();

                    MySqlCommand cmdpromos = new MySqlCommand(querypromos, conexion);
                    object resultpromos = cmdpromos.ExecuteScalar();
                    int totalpromos = 0;

                    if (resultpromos != null && resultpromos != DBNull.Value)
                    {
                        totalpromos = Convert.ToInt16(resultpromos);
                    }

                    lblTotalP.Text = totalpromos.ToString();

                    MySqlCommand cmdgenes = new MySqlCommand(querygenes, conexion);
                    object resultgenes = cmdgenes.ExecuteScalar();
                    int totalgenes = 0;

                    if (resultgenes != null && resultgenes != DBNull.Value)
                    {
                        totalgenes = Convert.ToInt16(resultgenes);
                    }

                    lblTotalG.Text = totalgenes.ToString();

                    MySqlCommand cmdisla = new MySqlCommand(queryisla, conexion);
                    object resultisla = cmdisla.ExecuteScalar();
                    int totalisla = 0;

                    if (resultisla != null && resultisla != DBNull.Value)
                    {
                        totalisla = Convert.ToInt16(resultisla);
                    }

                    lblTotalL.Text = totalisla.ToString();

                    MySqlCommand cmdpugna = new MySqlCommand(querypugna, conexion);
                    object resultpugna = cmdpugna.ExecuteScalar();
                    int totalpugna = 0;

                    if (resultpugna != null && resultpugna != DBNull.Value)
                    {
                        totalpugna = Convert.ToInt16(resultpugna);
                    }

                    lblTotalPET.Text = totalpugna.ToString();

                    MySqlCommand cmdluz = new MySqlCommand(queryluz, conexion);
                    object resultluz = cmdpugna.ExecuteScalar();
                    int totalluz = 0;

                    if (resultluz != null && resultluz != DBNull.Value)
                    {
                        totalluz = Convert.ToInt16(resultluz);
                    }

                    lblTotalLT.Text = totalluz.ToString();

                    MySqlCommand cmdfestival = new MySqlCommand(queryfestival, conexion);
                    object resultfestival = cmdfestival.ExecuteScalar();
                    int totalfestival = 0;

                    if (resultfestival != null && resultfestival != DBNull.Value)
                    {
                        totalfestival = Convert.ToInt16(resultfestival);
                    }

                    lblTotalFB.Text = totalfestival.ToString();

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