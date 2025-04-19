using MySqlConnector;
using System;

namespace Pokemon
{
    class Conexion
    {
        public static MySqlConnection GetConexion()
        {
            string servidor = "SERVER";

<<<<<<< HEAD
            string servidor = "SERVIDOR";

            string puerto = "PORT";

            string usuario = "USER ID";

            string password = "PASSWORD";

=======
            string puerto = "PORT";

            string usuario = "USER ID";

            string password = "PASSWORD";

>>>>>>> 97acb528955837417c5d71c1e4324b2b04da9cdf
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