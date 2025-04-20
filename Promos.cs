using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Promos: Form
    {
        public Promos()
        {
            InitializeComponent();
        }
                
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text;
                string nombre = txtNombre.Text;
                string tipo = txtTipo.Text;
                string ps = txtPS.Text;
                string habilidad = txtHabilidad.Text;
                string ataque1 = txtAtaque1.Text;
                string nea1 = txtNEA1.Text;
                string ataque2 = txtAtaque2.Text;
                string nea2 = txtNEA2.Text;
                string ataque3 = txtAtaque3.Text;
                string nea3 = txtNEA3.Text;
                string retirada = txtRetirada.Text;
                string debilidad = txtDebilidad.Text;
                string rareza = txtRareza.Text;
                string fase = txtFase.Text;
                int español = int.Parse(txtEspañol.Text);
                int ingles = int.Parse(txtIngles.Text);
                int frances = int.Parse(txtFrances.Text);
                int aleman = int.Parse(txtAleman.Text);
                int italiano = int.Parse(txtItaliano.Text);
                int portugues = int.Parse(txtPortugues.Text);
                int japones = int.Parse(txtJapones.Text);
                int koreano = int.Parse(txtKoreano.Text);
                int chino = int.Parse(txtChino.Text);

                // Verificar si todos los campos están llenos y si los valores numéricos son válidos
                if (codigo != String.Empty && nombre != String.Empty && tipo != String.Empty && ps != String.Empty &&
                    habilidad != String.Empty && ataque1 != String.Empty && nea1 != String.Empty && ataque2 != String.Empty &&
                    nea2 != String.Empty && ataque3 != String.Empty && nea3 != String.Empty && retirada != String.Empty &&
                    debilidad != String.Empty && rareza != String.Empty && fase != String.Empty && español >= 0 && ingles >= 0 &&
                    frances >= 0 && aleman >= 0 && italiano >= 0 && portugues >= 0 && japones >= 0 && koreano >= 0 && chino >= 0)
                {
                    // Verificar si el código ya existe en la base de datos
                    string checkSql = "SELECT COUNT(*) FROM Promos WHERE Codigo = @Codigo";
                    MySqlConnection conexion = Conexion.GetConexion();
                    conexion.Open();

                    MySqlCommand checkCommand = new MySqlCommand(checkSql, conexion);
                    checkCommand.Parameters.AddWithValue("@Codigo", codigo);
                    int count = Convert.ToInt16(checkCommand.ExecuteScalar()); // Ejecutamos la consulta y obtenemos el conteo

                    if (count > 0) // Si el código ya existe
                    {
                        MessageBox.Show("Carta ya registrada o código incorrecto\nFavor de ingresar un código diferente.");
                    }
                    else
                    {
                        // Si el código no existe, realizamos la inserción
                        string sql = "INSERT INTO Promos (Codigo, Nombre, Tipo, PS, Habilidad, Ataque1, NEA1, Ataque2, NEA2, Ataque3, " +
                                     "NEA3, Retirada, Debilidad, Rareza, Fase, ESP, ING, FRA, ALE, ITA, POR, JAP, KOR, CHI) " +
                                     "VALUES (@Codigo, @Nombre, @Tipo, @PS, @Habilidad, @Ataque1, @NEA1, @Ataque2, @NEA2, @Ataque3, " +
                                     "@NEA3, @Retirada, @Debilidad, @Rareza, @Fase, @Español, @Ingles, @Frances, @Aleman, @Italiano, " +
                                     "@Portugues, @Japones, @Koreano, @Chino)";

                        MySqlCommand comando = new MySqlCommand(sql, conexion);
                        comando.Parameters.AddWithValue("@Codigo", codigo);
                        comando.Parameters.AddWithValue("@Nombre", nombre);
                        comando.Parameters.AddWithValue("@Tipo", tipo);
                        comando.Parameters.AddWithValue("@PS", ps);
                        comando.Parameters.AddWithValue("@Habilidad", habilidad);
                        comando.Parameters.AddWithValue("@Ataque1", ataque1);
                        comando.Parameters.AddWithValue("@NEA1", nea1);
                        comando.Parameters.AddWithValue("@Ataque2", ataque2);
                        comando.Parameters.AddWithValue("@NEA2", nea2);
                        comando.Parameters.AddWithValue("@Ataque3", ataque3);
                        comando.Parameters.AddWithValue("@NEA3", nea3);
                        comando.Parameters.AddWithValue("@Retirada", retirada);
                        comando.Parameters.AddWithValue("@Debilidad", debilidad);
                        comando.Parameters.AddWithValue("@Rareza", rareza);
                        comando.Parameters.AddWithValue("@Fase", fase);
                        comando.Parameters.AddWithValue("@Español", español);
                        comando.Parameters.AddWithValue("@Ingles", ingles);
                        comando.Parameters.AddWithValue("@Frances", frances);
                        comando.Parameters.AddWithValue("@Aleman", aleman);
                        comando.Parameters.AddWithValue("@Italiano", italiano);
                        comando.Parameters.AddWithValue("@Portugues", portugues);
                        comando.Parameters.AddWithValue("@Japones", japones);
                        comando.Parameters.AddWithValue("@Koreano", koreano);
                        comando.Parameters.AddWithValue("@Chino", chino);

                        try
                        {
                            comando.ExecuteNonQuery();
                            MessageBox.Show("Carta Guardada");
                            Limpiar();
                            txtCodigo.Focus();
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("Error al guardar \n" + ex.Message);
                            txtCodigo.Focus();
                        }
                    }

                    conexion.Close();
                }
                else
                {
                    MessageBox.Show("Se deben completar todos los campos vacíos correctamente.");
                    txtCodigo.Focus();
                }

            }
            catch (FormatException fex)
            {
                MessageBox.Show("Datos Incorrectos \n" + fex.Message);
                txtCodigo.Focus();
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            MySqlDataReader reader = null;
            MySqlDataReader reader2 = null;

            string sql1 = "SELECT * FROM promos WHERE Codigo LIKE '" + codigo + "'";
            string sql2 = "SELECT * FROM promos WHERE Nombre LIKE '" + nombre + "'";

            MySqlConnection conexioncodigo = Conexion.GetConexion();
            conexioncodigo.Open();

            MySqlConnection conexionnombre = Conexion.GetConexion();
            conexionnombre.Open();

            bool Validacion = false; // Variable para saber si encontramos al menos un registro
            int contadorNombre = 0; // Variable para contar cuántos registros con el mismo nombre se encontraron
            List<string> registrosconmismonombre = new List<string>(); // Lista para guardar los índices y datos de los registros encontrados

            try
            {
                MySqlCommand comandocodigo = new MySqlCommand(sql1, conexioncodigo);
                MySqlCommand comandonombre = new MySqlCommand(sql2, conexionnombre);

                reader = comandocodigo.ExecuteReader();
                reader2 = comandonombre.ExecuteReader();

                // Solo entramos si alguna de las condiciones de código o nombre no está vacía
                if (codigo != string.Empty || nombre != string.Empty)
                {

                    // Verificamos si se encontraron registros en la consulta de código
                    if (reader.HasRows)
                    {
                        Validacion = true;

                        while (reader.Read())
                        {
                            txtId.Text = reader.GetInt32(0).ToString();
                            txtCodigo.Text = reader.GetString(1);
                            txtNombre.Text = reader.GetString(2);
                            txtTipo.Text = reader.GetString(3);
                            txtPS.Text = reader.GetString(4);
                            txtHabilidad.Text = reader.GetString(5);
                            txtAtaque1.Text = reader.GetString(6);
                            txtNEA1.Text = reader.GetString(7);
                            txtAtaque2.Text = reader.GetString(8);
                            txtNEA2.Text = reader.GetString(9);
                            txtAtaque3.Text = reader.GetString(10);
                            txtNEA3.Text = reader.GetString(11);
                            txtRetirada.Text = reader.GetString(12);
                            txtDebilidad.Text = reader.GetString(13);
                            txtRareza.Text = reader.GetString(14);
                            txtFase.Text = reader.GetString(15);
                            txtEspañol.Text = reader.GetString(16);
                            txtIngles.Text = reader.GetString(17);
                            txtFrances.Text = reader.GetString(18);
                            txtAleman.Text = reader.GetString(19);
                            txtItaliano.Text = reader.GetString(20);
                            txtPortugues.Text = reader.GetString(21);
                            txtJapones.Text = reader.GetString(22);
                            txtKoreano.Text = reader.GetString(23);
                            txtChino.Text = reader.GetString(24);
                        }
                    }

                    // Verificamos si se encontraron registros en la consulta de nombre
                    if (reader2.HasRows)
                    {
                        Validacion = true; // Se encontraron registros
                        int index = 1; // Variable para llevar el seguimiento del índice de los registros encontrados

                        // Contamos cuántos registros con el mismo nombre se encontraron
                        while (reader2.Read())
                        {
                            contadorNombre++; // Incrementamos el contador por cada registro encontrado
                                              // Añadimos a la lista el índice y los datos del registro encontrado
                            registrosconmismonombre.Add($"Carta {index} : {reader2.GetString(1)} {reader2.GetString(2)}");

                            if (contadorNombre != 0)
                            {

                                // Aquí puedes rellenar los cuadros de texto si lo necesitas:
                                txtId.Text = reader2.GetInt32(0).ToString();
                                txtCodigo.Text = reader2.GetString(1);
                                txtNombre.Text = reader2.GetString(2);
                                txtTipo.Text = reader2.GetString(3);
                                txtPS.Text = reader2.GetString(4);
                                txtHabilidad.Text = reader2.GetString(5);
                                txtAtaque1.Text = reader2.GetString(6);
                                txtNEA1.Text = reader2.GetString(7);
                                txtAtaque2.Text = reader2.GetString(8);
                                txtNEA2.Text = reader2.GetString(9);
                                txtAtaque3.Text = reader2.GetString(10);
                                txtNEA3.Text = reader2.GetString(11);
                                txtRetirada.Text = reader2.GetString(12);
                                txtDebilidad.Text = reader2.GetString(13);
                                txtRareza.Text = reader2.GetString(14);
                                txtFase.Text = reader2.GetString(15);
                                txtEspañol.Text = reader2.GetString(16);
                                txtIngles.Text = reader2.GetString(17);
                                txtFrances.Text = reader2.GetString(18);
                                txtAleman.Text = reader2.GetString(19);
                                txtItaliano.Text = reader2.GetString(20);
                                txtPortugues.Text = reader2.GetString(21);
                                txtJapones.Text = reader2.GetString(22);
                                txtKoreano.Text = reader2.GetString(23);
                                txtChino.Text = reader2.GetString(24);

                                index++; // Incrementamos el índice para el siguiente registro

                            }
                        }

                        if (contadorNombre > 1)
                        {
                            Limpiar();
                            // Si se encontraron registros, mostramos un mensaje con los índices de los registros encontrados
                            MessageBox.Show($"Se encontraron {contadorNombre} cartas: {nombre}\n\n{string.Join("\n", registrosconmismonombre)}");
                        }
                    }

                    // Si no se encontraron registros en ninguna de las consultas
                    if (!Validacion)
                    {
                        MessageBox.Show("No se encontro la carta");
                    }
                }
                else
                {
                    MessageBox.Show("No se llenaron los campos\nFavor de buscar por código o por nombre");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar \n" + ex.Message);
            }
            finally
            {
                conexioncodigo.Close();
                conexionnombre.Close();
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string tipo = txtTipo.Text;
            string ps = txtPS.Text;
            string habilidad = txtHabilidad.Text;
            string ataque1 = txtAtaque1.Text;
            string nea1 = txtNEA1.Text;
            string ataque2 = txtAtaque2.Text;
            string nea2 = txtNEA2.Text;
            string ataque3 = txtAtaque3.Text;
            string nea3 = txtNEA3.Text;
            string retirada = txtRetirada.Text;
            string debilidad = txtDebilidad.Text;
            string rareza = txtRareza.Text;
            string fase = txtFase.Text;
            int español = int.Parse(txtEspañol.Text);
            int ingles = int.Parse(txtIngles.Text);
            int frances = int.Parse(txtFrances.Text);
            int aleman = int.Parse(txtAleman.Text);
            int italiano = int.Parse(txtItaliano.Text);
            int portugues = int.Parse(txtPortugues.Text);
            int japones = int.Parse(txtJapones.Text);
            int koreano = int.Parse(txtKoreano.Text);
            int chino = int.Parse(txtChino.Text);

            string sql = "UPDATE promos SET Codigo = '" + codigo + "', Nombre = '" + nombre + "', Tipo = '" + tipo + "'," +
                "PS = '" + ps + "', Habilidad = '" + habilidad + "', Ataque1 = '" + ataque1 + "', NEA1 = '" + nea1 + "'," +
                "Ataque2 = '" + ataque2 + "', NEA2 = '" + nea2 + "', Ataque3 = '" + ataque3 + "', NEA3 = '" + nea3 + "'," +
                "Retirada = '" + retirada + "', Debilidad = '" + debilidad + "', Rareza = '" + rareza + "', Fase = '" + fase + "'," +
                "ESP = '" + español + "', ING = '" + ingles + "', FRA = '" + frances + "', ALE = '" + aleman + "'," +
                "ITA = '" + italiano + "', POR = '" + portugues + "', JAP = '" + japones + "', KOR = '" + koreano + "'," +
                "CHI = '" + chino + "' WHERE id = '" + id + "'";

            MySqlConnection conexion = Conexion.GetConexion();
            conexion.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Modificado");
                Limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al modificar \n" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;

            // Creación de las consultas para eliminar por Código y Nombre
            string sqlcodigo = "DELETE FROM promos WHERE Codigo = '" + codigo + "'";
            string sqlnombre = "DELETE FROM promos WHERE Nombre = '" + nombre + "'";

            MySqlConnection conexion = Conexion.GetConexion();
            conexion.Open();

            try
            {
                // Verificamos si al menos uno de los campos (Codigo o Nombre) no está vacío
                if (!string.IsNullOrEmpty(codigo) || !string.IsNullOrEmpty(nombre))
                {
                    // Preguntamos al usuario si está seguro de eliminar los registros
                    DialogResult eliminar = MessageBox.Show("¿Estás seguro de eliminar esta carta?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (eliminar == DialogResult.Yes)
                    {
                        // Eliminar registros basados en el Código (si se proporcionó)
                        if (!string.IsNullOrEmpty(codigo))
                        {
                            MySqlCommand comandocodigo = new MySqlCommand(sqlcodigo, conexion);
                            int registroseliminadoscodigo = comandocodigo.ExecuteNonQuery();

                            if (registroseliminadoscodigo > 0)
                            {
                                MessageBox.Show($"Se elimino la carta {codigo} {nombre}.");
                            }
                            else
                            {
                                MessageBox.Show($"No se pudo eliminar la carta {codigo} {nombre}.");
                            }
                        }

                        // Eliminar registros basados en el Nombre (si se proporcionó)
                        if (!string.IsNullOrEmpty(nombre))
                        {
                            MySqlCommand comandonombre = new MySqlCommand(sqlnombre, conexion);
                            int registroseliminadosnombre = comandonombre.ExecuteNonQuery();

                            if (registroseliminadosnombre > 0)
                            {
                                MessageBox.Show($"Se elimino la carta {codigo} {nombre}.");
                            }
                            else
                            {
                                MessageBox.Show($"No se pudo eliminar la carta {codigo} {nombre}.");
                            }
                        }

                        // Limpiar los campos después de la eliminación
                        Limpiar();
                    }
                    else
                    {
                        txtCodigo.Focus(); // Si no se confirma la eliminación, se mantiene el enfoque en el campo de Código
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el Código o el Nombre de la carta para eliminar");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar \n" + ex.Message);
            }
            finally
            {
                conexion.Close(); // Cerramos la conexión
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void Limpiar()
        {
            txtId.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtTipo.Text = string.Empty;
            txtPS.Text = string.Empty;
            txtHabilidad.Text = string.Empty;
            txtAtaque1.Text = string.Empty;
            txtNEA1.Text = string.Empty;
            txtAtaque2.Text = string.Empty;
            txtNEA2.Text = string.Empty;
            txtAtaque3.Text = string.Empty;
            txtNEA3.Text = string.Empty;
            txtRetirada.Text = string.Empty;
            txtDebilidad.Text = string.Empty;
            txtRareza.Text = string.Empty;
            txtFase.Text = string.Empty;
            txtEspañol.Text = string.Empty;
            txtIngles.Text = string.Empty;
            txtFrances.Text = string.Empty;
            txtAleman.Text = string.Empty;
            txtItaliano.Text = string.Empty;
            txtPortugues.Text = string.Empty;
            txtJapones.Text = string.Empty;
            txtKoreano.Text = string.Empty;
            txtChino.Text = string.Empty;
        }
        private void btnESP_Click(object sender, EventArgs e)
        {
            string Imagen = txtCodigo.Text.ToString();
            
            Pokedex cartas = new Pokedex();

            cartas.MostrarImagen("C:FILES.JPEG");

            if (File.Exists("C:FILES.JPEG"))

            {
                cartas.Show();
            }
            else
            {
                MessageBox.Show("Carta no conseguida");
            }
        }
        private void btnING_Click(object sender, EventArgs e)
        {
            string Imagen = txtCodigo.Text.ToString();

            Pokedex cartas = new Pokedex();

            cartas.MostrarImagen("C:FILES.JPEG");

            if (File.Exists("C:FILES.JPEG"))

            {
                cartas.Show();
            }
            else
            {
                MessageBox.Show("Carta no conseguida");
            }
        }
        private void btnFRA_Click(object sender, EventArgs e)
        {
            string Imagen = txtCodigo.Text.ToString();

            Pokedex cartas = new Pokedex();

            cartas.MostrarImagen("C:FILES.JPEG");

            if (File.Exists("C:FILES.JPEG"))

            {
                cartas.Show();
            }
            else
            {
                MessageBox.Show("Carta no conseguida");
            }
        }
        private void btnALE_Click(object sender, EventArgs e)
        {
            string Imagen = txtCodigo.Text.ToString();

            Pokedex cartas = new Pokedex();

            cartas.MostrarImagen("C:FILES.JPEG");

            if (File.Exists("C:FILES.JPEG"))

            {
                cartas.Show();
            }
            else
            {
                MessageBox.Show("Carta no conseguida");
            }
        }
        private void btnITA_Click(object sender, EventArgs e)
        {
            string Imagen = txtCodigo.Text.ToString();

            Pokedex cartas = new Pokedex();

            cartas.MostrarImagen("C:FILES.JPEG");

            if (File.Exists("C:FILES.JPEG"))

            {
                cartas.Show();
            }
            else
            {
                MessageBox.Show("Carta no conseguida");
            }
        }
        private void btnPOR_Click(object sender, EventArgs e)
        {
            string Imagen = txtCodigo.Text.ToString();

            Pokedex cartas = new Pokedex();

            cartas.MostrarImagen("C:FILES.JPEG");

            if (File.Exists("C:FILES.JPEG"))

            {
                cartas.Show();
            }
            else
            {
                MessageBox.Show("Carta no conseguida");
            }
        }
        private void btnJAP_Click(object sender, EventArgs e)
        {
            string Imagen = txtCodigo.Text.ToString();

            Pokedex cartas = new Pokedex();

            cartas.MostrarImagen("C:FILES.JPEG");

            if (File.Exists("C:FILES.JPEG"))

            {
                cartas.Show();
            }
            else
            {
                MessageBox.Show("Carta no conseguida");
            }
        }
        private void btnKOR_Click(object sender, EventArgs e)
        {
            string Imagen = txtCodigo.Text.ToString();

            Pokedex cartas = new Pokedex();

            cartas.MostrarImagen("C:FILES.JPEG");

            if (File.Exists("C:FILES.JPEG"))

            {
                cartas.Show();
            }
            else
            {
                MessageBox.Show("Carta no conseguida");
            }
        }
        private void btnCHI_Click(object sender, EventArgs e)
        {
            string Imagen = txtCodigo.Text.ToString();

            Pokedex cartas = new Pokedex();

            cartas.MostrarImagen("C:FILES.JPEG");

            if (File.Exists("C:FILES.JPEG"))

            {
                cartas.Show();
            }
            else
            {
                MessageBox.Show("Carta no conseguida");
            }
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            // Crear e inicializar el formulario secundario
            Menú Menu = new Menú();

            // Esconde Promos, es como cerrar la ventana
            this.Hide();

            // Mostrar el Menu
            Menu.Show();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Estas seguro de salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (r == DialogResult.Yes)
            {
                Application.Exit();
                //this.Close();
            }
            else
            {
                txtCodigo.Focus();
            }
        }
    }
}