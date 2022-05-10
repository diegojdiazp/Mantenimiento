using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Aurorita2018.Models;
using System.Data.SqlClient;

namespace Aurorita2018.DAO
{
    public class MenuInicioDAO
    {
        public List<TareasUsuario> TareasUsuario(string idUsuario, string tipoUsuario)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            List<TareasUsuario> listaTareas = new List<TareasUsuario>();
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "ObtenerTareasUsuario";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USUARIO", idUsuario);
                cmd.Parameters.AddWithValue("@TIPO_USUARIO", tipoUsuario);
                da.Fill(ds);
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    TareasUsuario tarea = new TareasUsuario();
                    tarea.Fecha = Convert.ToDateTime(fila["Fecha"].ToString());
                    tarea.Estado = fila["Estado"].ToString();
                    tarea.Actividad = fila["Actividad"].ToString();
                    tarea.NombreZona = fila["NombreZona"].ToString();
                    tarea.Indice = Convert.ToInt32(fila["Indice"].ToString());
                    listaTareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return listaTareas;
        }

        public List<EstadisticaUsuario> EstadisticaFallasRegistradas(string idUsuario)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            List<EstadisticaUsuario> FallasRegistradas = new List<EstadisticaUsuario>();
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spc_EstadisticaFallasRegistradas";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USUARIO", idUsuario);
                da.Fill(ds);
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    EstadisticaUsuario registro = new EstadisticaUsuario();
                    registro.Cantidad = Convert.ToInt32(fila["CantFalla"].ToString());
                    registro.Descripcion = fila["DescripcionFalla"].ToString();
                    //registro.Estado = fila["Status"].ToString();
                    FallasRegistradas.Add(registro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                con.Close();
            }
            return FallasRegistradas;
        }
        public List<EstadisticaUsuario> EstadisticaTiposMantenimientosUsuario(string idUsuario)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            List<EstadisticaUsuario> FallasRegistradas = new List<EstadisticaUsuario>();
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spc_EstadisticaTiposMantenimiento";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USUARIO", idUsuario);
                da.Fill(ds);
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    EstadisticaUsuario registro = new EstadisticaUsuario();
                    registro.Cantidad = Convert.ToInt32(fila["CANTIDAD"].ToString());
                    registro.Descripcion = fila["DescripcionMantenimiento"].ToString();
                    FallasRegistradas.Add(registro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return FallasRegistradas;
        }
        public List<EstadisticaUsuario> EstadisticasEventosMensuales(string idUsuario)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            List<EstadisticaUsuario> EventosMensuales = new List<EstadisticaUsuario>();
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spc_EstadisticaMensualEventos";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USUARIO", idUsuario);
                da.Fill(ds);
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    EstadisticaUsuario registro = new EstadisticaUsuario();
                    registro.Cantidad = Convert.ToInt32(fila["Cantidad"].ToString());
                    registro.Descripcion = fila["Descripcion"].ToString();
                    EventosMensuales.Add(registro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return EventosMensuales;
        }
        public List<EstadisticaUsuario> EstatidisticaEventosEstadosPorUsuario(string idUsuario)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            List<EstadisticaUsuario> EventosMensuales = new List<EstadisticaUsuario>();
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spc_EstadosEventosPorUsuario";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USUARIO", idUsuario);
                da.Fill(ds);
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    EstadisticaUsuario registro = new EstadisticaUsuario();
                    registro.Cantidad = Convert.ToInt32(fila["Cantidad"].ToString());
                    registro.Estado = fila["status"].ToString();
                    EventosMensuales.Add(registro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return EventosMensuales;
        }
        public List<EstadisticaUsuario> EstadisticaResgistroAnualUsuario(string idUsuario)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            List<EstadisticaUsuario> EventosMensuales = new List<EstadisticaUsuario>();
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spc_EstadisticaAnualUsuario";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USUARIO", idUsuario);
                da.Fill(ds);
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    EstadisticaUsuario registro = new EstadisticaUsuario();
                    registro.Cantidad = Convert.ToInt32(fila["CANT"].ToString());
                    registro.Descripcion = fila["NOMBREMES"].ToString();
                    EventosMensuales.Add(registro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return EventosMensuales;
        }


    }
}