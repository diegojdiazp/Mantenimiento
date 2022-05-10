using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Aurorita2018.DAO
{
    public class Conexion
    {
        public SqlConnection Conex;
        public Conexion()
        {
            Conex = new SqlConnection();
            Conex.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Conexion"].ToString();
        }

        public void Open()
        {
            if (Conex.State == System.Data.ConnectionState.Closed)
                Conex.Open();
        }
        public void Close()
        {
            if (Conex.State == System.Data.ConnectionState.Open)
                Conex.Close();
        }

    }
}