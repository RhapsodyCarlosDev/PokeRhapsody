using MySqlConnector;
using System;

namespace Pokemon
{
    class Conexion
    {
        public static MySqlConnection GetConexion()
        {
            string servidor = "SERVER";

            string puerto = "PORT";

            string usuario = "USER ID";

            string password = "PASSWORD";

            string db = "DATABASE";

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
