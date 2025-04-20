using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Usuarios: Form
    {

        string connectionString = "Server = SERVER; Database = DATABASE; Uid = USER ID; Pwd=PASSWORD;";

        public Usuarios()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            Mostrar.Image = Image.FromFile("C:FILES");
        }

        private void btnIngresar_Click(object sender, System.EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    string query = "SELECT * FROM usuarios WHERE Usuarios = @Usuarios AND Passwords = @Passwords";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Usuarios", usuario);
                        cmd.Parameters.AddWithValue("@Passwords", password);

                        bool loginCorrecto = false;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            loginCorrecto = reader.HasRows;
                        }

                        if (loginCorrecto)
                        {
                            string auditQuery = "INSERT INTO login_audit (username, password) VALUES (@username, @password)";
                            using (MySqlCommand auditCmd = new MySqlCommand(auditQuery, conexion))
                            {
                                auditCmd.Parameters.AddWithValue("@username", usuario);
                                auditCmd.Parameters.AddWithValue("@password", password);
                                auditCmd.ExecuteNonQuery();
                            }

                            Menú menu = new Menú();
                            menu.Show();
                            this.Hide();
                        }
                        else
                        {
                            string failedQuery = "INSERT INTO login_failed (username, password) VALUES (@username, @password)";
                            using (MySqlCommand failedCmd = new MySqlCommand(failedQuery, conexion))
                            {
                                failedCmd.Parameters.AddWithValue("@username", usuario);
                                failedCmd.Parameters.AddWithValue("@password", password);
                                failedCmd.ExecuteNonQuery();
                            }

                            Limpiar();
                            MessageBox.Show("Usuario o Contraseña Incorrecta.");
                            txtUsuario.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de Conexión: " + ex.Message);
                }
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                string Usuario = txtUsuario.Text.Trim();
                string Password = txtPassword.Text.Trim();

                if (!string.IsNullOrEmpty(Usuario))
                {
                    string checkSql = "SELECT COUNT(*) FROM usuarios WHERE Usuarios = @Usuarios";

                    using (MySqlConnection conexion = new MySqlConnection(connectionString))
                    {
                        conexion.Open();

                        using (MySqlCommand checkCommand = new MySqlCommand(checkSql, conexion))
                        {
                            checkCommand.Parameters.AddWithValue("@Usuarios", Usuario);
                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                            if (count > 0)
                            {
                                MessageBox.Show("Error en el Usuario\nFavor de ingresar un Usuario y Contraseña Válida.");
                                Limpiar();
                                txtUsuario.Focus();
                                return;
                            }
                        }

                        string insertSql = "INSERT INTO usuarios (Usuarios, Passwords) VALUES (@Usuarios, @Passwords)";
                        using (MySqlCommand comando = new MySqlCommand(insertSql, conexion))
                        {
                            comando.Parameters.AddWithValue("@Usuarios", Usuario);
                            comando.Parameters.AddWithValue("@Passwords", Password);

                            comando.ExecuteNonQuery();
                            MessageBox.Show("Usuario guardado con éxito.");
                            Limpiar(); 
                            txtUsuario.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un Usuario y Contraseña.");
                    Limpiar();
                    txtUsuario.Focus();
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Usuario o Contraseña incorrecta: \n" + fex.Message);
                Limpiar();
            }
            catch (MySqlException mex)
            {
                MessageBox.Show("Error con la conexión: \n" + mex.Message);
                Limpiar();
            }
        }

        private void Limpiar()
        {
            txtUsuario.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Estas seguro de salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                txtUsuario.Focus();
            }
        }

        private void Mostrar_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            Mostrar.Image = Image.FromFile("C:\\Users\\carloz3\\source\\repos\\Practicas Personales\\Pokemon\\Pokemon\\ICO\\Invisible.ico");
        }

        private void Mostrar_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            Mostrar.Image = Image.FromFile("C:\\Users\\carloz3\\source\\repos\\Practicas Personales\\Pokemon\\Pokemon\\ICO\\Visible.ico");
        }
    }
}
