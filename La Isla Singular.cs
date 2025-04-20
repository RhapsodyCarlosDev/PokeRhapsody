using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class LaIslaSingular: Form
    {
        public LaIslaSingular()
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
                string nsde = txtNSDE.Text;
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
                    debilidad != String.Empty && rareza != String.Empty && fase != String.Empty && nsde != String.Empty &&
                    español >= 0 && ingles >= 0 && frances >= 0 && aleman >= 0 && italiano >= 0 && portugues >= 0 && japones >= 0 &&
                    koreano >= 0 && chino >= 0)
                {
                    // Verificar si el código ya existe en la base de datos
                    string checkSql = "SELECT COUNT(*) FROM la_isla_singular WHERE Codigo = @Codigo";
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
                        string sql = "INSERT INTO la_isla_singular (Codigo, Nombre, Tipo, PS, Habilidad, Ataque1, NEA1, Ataque2, NEA2, Ataque3, " +
                                     "NEA3, Retirada, Debilidad, Rareza, Fase, NSDE, ESP, ING, FRA, ALE, ITA, POR, JAP, KOR, CHI) " +
                                     "VALUES (@Codigo, @Nombre, @Tipo, @PS, @Habilidad, @Ataque1, @NEA1, @Ataque2, @NEA2, @Ataque3, " +
                                     "@NEA3, @Retirada, @Debilidad, @Rareza, @Fase, @NSDE, @Español, @Ingles, @Frances, @Aleman, @Italiano, " +
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
                        comando.Parameters.AddWithValue("@NSDE", nsde);
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

            string sql1 = "SELECT * FROM la_isla_singular WHERE Codigo LIKE '" + codigo + "'";
            string sql2 = "SELECT * FROM la_isla_singular WHERE Nombre LIKE '" + nombre + "'";

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
                            txtNSDE.Text = reader.GetString(16);
                            txtEspañol.Text = reader.GetString(17);
                            txtIngles.Text = reader.GetString(18);
                            txtFrances.Text = reader.GetString(19);
                            txtAleman.Text = reader.GetString(20);
                            txtItaliano.Text = reader.GetString(21);
                            txtPortugues.Text = reader.GetString(22);
                            txtJapones.Text = reader.GetString(23);
                            txtKoreano.Text = reader.GetString(24);
                            txtChino.Text = reader.GetString(25);
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
                                txtNSDE.Text = reader2.GetString(16);
                                txtEspañol.Text = reader2.GetString(17);
                                txtIngles.Text = reader2.GetString(18);
                                txtFrances.Text = reader2.GetString(19);
                                txtAleman.Text = reader2.GetString(20);
                                txtItaliano.Text = reader2.GetString(21);
                                txtPortugues.Text = reader2.GetString(22);
                                txtJapones.Text = reader2.GetString(23);
                                txtKoreano.Text = reader2.GetString(24);
                                txtChino.Text = reader2.GetString(25);

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
            string nsde = txtNSDE.Text;

            // Validación de los campos numéricos
            int español = ValidarCampoNumerico(txtEspañol.Text);
            int ingles = ValidarCampoNumerico(txtIngles.Text);
            int frances = ValidarCampoNumerico(txtFrances.Text);
            int aleman = ValidarCampoNumerico(txtAleman.Text);
            int italiano = ValidarCampoNumerico(txtItaliano.Text);
            int portugues = ValidarCampoNumerico(txtPortugues.Text);
            int japones = ValidarCampoNumerico(txtJapones.Text);
            int koreano = ValidarCampoNumerico(txtKoreano.Text);
            int chino = ValidarCampoNumerico(txtChino.Text);

            // Validamos que los campos obligatorios no estén vacíos
            if (string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Campos vacíos\nCompleta todos los campos");
                return; // Salimos si falta algún campo obligatorio
            }

            string sql = "UPDATE la_isla_singular SET Codigo = '" + codigo + "', Nombre = '" + nombre + "', Tipo = '" + tipo + "'," +
                " PS = '" + ps + "', Habilidad = '" + habilidad + "', Ataque1 = '" + ataque1 + "', NEA1 = '" + nea1 + "'," +
                " Ataque2 = '" + ataque2 + "', NEA2 = '" + nea2 + "', Ataque3 = '" + ataque3 + "', NEA3 = '" + nea3 + "'," +
                " Retirada = '" + retirada + "', Debilidad = '" + debilidad + "', Rareza = '" + rareza + "', Fase = '" + fase + "'," +
                " NSDE = '" + nsde + "', ESP = '" + español + "', ING = '" + ingles + "', FRA = '" + frances + "'," +
                " ALE = '" + aleman + "', ITA = '" + italiano + "', POR = '" + portugues + "', JAP = '" + japones + "'," +
                " KOR = '" + koreano + "', CHI = '" + chino + "' WHERE id = '" + id + "'";

            MySqlConnection conexion = Conexion.GetConexion();
            conexion.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Modificado");
                Limpiar(); // Limpiar los campos después de la modificación
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

        private int ValidarCampoNumerico(string campo)
        {
            int valor;
            // Si el campo está vacío o no es un número, asignamos 0
            if (string.IsNullOrEmpty(campo) || !int.TryParse(campo, out valor))
            {
                valor = 0;
            }
            return valor;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;  // Código que puede eliminarse directamente
            string nombre = txtNombre.Text;  // Nombre para el cual verificamos duplicados

            // Conexión a la base de datos
            MySqlConnection conexion = Conexion.GetConexion();
            conexion.Open();

            try
            {
                // Si se proporciona el Código, eliminamos directamente ese registro
                if (!string.IsNullOrEmpty(codigo))
                {
                    // Preguntamos al usuario si está seguro de eliminar los registros con el Nombre especificado
                    DialogResult eliminar = MessageBox.Show($"¿Estás seguro de eliminar la carta {codigo} {nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (eliminar == DialogResult.Yes)
                    {
                        string sqlEliminarCodigo = "DELETE FROM la_isla_singular WHERE Codigo = @codigo";
                        MySqlCommand comandoEliminarCodigo = new MySqlCommand(sqlEliminarCodigo, conexion);
                        comandoEliminarCodigo.Parameters.AddWithValue("@codigo", codigo);
                        int registrosEliminadosCodigo = comandoEliminarCodigo.ExecuteNonQuery();

                        if (registrosEliminadosCodigo == 1)
                        {
                            MessageBox.Show($"Se eliminó la carta {codigo} {nombre}.");
                        }
                        else
                        {
                            MessageBox.Show($"No se pudo eliminar la carta {codigo} {nombre}.");
                        }

                        // Limpiar los campos después de la eliminación
                        Limpiar();
                    }
                    else
                    {
                        txtCodigo.Focus(); // Si no se confirma la eliminación, se mantiene el enfoque en el campo de Nombre
                    }

                }
                // Si solo se proporciona el Nombre, verificamos cuántos registros existen con ese nombre
                else if (!string.IsNullOrEmpty(nombre))
                {
                    // Contamos cuántos registros existen con el mismo Nombre
                    string sqlContarNombre = "SELECT COUNT(*) FROM la_isla_singular WHERE Nombre = @nombre";
                    MySqlCommand comandoContarNombre = new MySqlCommand(sqlContarNombre, conexion);
                    comandoContarNombre.Parameters.AddWithValue("@nombre", nombre);
                    int contadorNombre = Convert.ToInt32(comandoContarNombre.ExecuteScalar());

                    // Si hay más de un registro con el mismo nombre, mostramos el mensaje
                    if (contadorNombre > 1)
                    {
                        MessageBox.Show($"No se pudo eliminar la carta {nombre}. Existen {contadorNombre} cartas con el nombre de {nombre}." +
                            $"\nFavor de ingresar el nombre y buscar para obtener el codigo de las cartas con ese nombre");
                    }
                    else
                    {
                        // Preguntamos al usuario si está seguro de eliminar los registros con el Nombre especificado
                        DialogResult eliminar = MessageBox.Show($"¿Estás seguro de eliminar la carta {nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (eliminar == DialogResult.Yes)
                        {
                            string sqlEliminarNombre = "DELETE FROM la_isla_singular WHERE Nombre = @nombre";
                            MySqlCommand comandoEliminarNombre = new MySqlCommand(sqlEliminarNombre, conexion);
                            comandoEliminarNombre.Parameters.AddWithValue("@nombre", nombre);
                            int registrosEliminadosNombre = comandoEliminarNombre.ExecuteNonQuery();

                            if (registrosEliminadosNombre > 0)
                            {
                                MessageBox.Show($"Se eliminó la carta {codigo} {nombre}.");
                            }
                            else
                            {
                                MessageBox.Show($"No se pudo eliminar la carta {codigo} {nombre}.");
                            }

                            // Limpiar los campos después de la eliminación
                            Limpiar();
                        }
                        else
                        {
                            txtNombre.Focus(); // Si no se confirma la eliminación, se mantiene el enfoque en el campo de Nombre
                        }
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
            txtNSDE.Text = string.Empty;
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