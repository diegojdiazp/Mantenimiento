using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Aurorita2018.Models;
using System.Data.Sql;
using System.Data;

namespace Aurorita2018.DAO
{
    public class LoginDAO
    {
        public CuentaUsuario LoginUser(string usuario, string pasword)
        {
            CuentaUsuario cuentaUsuario = new CuentaUsuario();
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spc_Login";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USUARIO", usuario);
                cmd.Parameters.AddWithValue("@PASSWORD", pasword);
                da.Fill(ds);

                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    
                    cuentaUsuario.Usuario = fila["Usuario"].ToString();
                    cuentaUsuario.Nombre = fila["Nombre"].ToString();
                    cuentaUsuario.TipoUsuario = fila["TipoUsuario"].ToString();
                }
            }
            catch (Exception ex)
            {
                cuentaUsuario = null;
                throw new Exception("No es posible Ingresar al Sistema. Detalles: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return cuentaUsuario;
        }




    }
}