using MySqlConnector;
using System;

namespace Pokemon
{
    class Conexion
    {
        public static MySqlConnection GetConexion()
        {

            string servidor = "localhost";

            string puerto = "3306";

            string usuario = "root";

            string password = "110818";

            string db = "PokeRhapsody";

            string cadenaConexion = "server=" + servidor +
                                    "; port=" + puerto +
                                    "; user id=" + usuario +
                                    "; password=" + password +
                                    "; database=" + db;
            MySqlConnection conexion = new MySqlConnection(cadenaConexion);
            return conexion;

        }

        internal void Open()
        {
            throw new NotImplementedException();
        }
    }
}
