using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Aurorita2018.Models;

namespace Aurorita2018.DAO
{
    public class ProcesosDAO
    {

        public List<DropDownList> ObtenerDropDown(string IdDrop)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            List<DropDownList> DropDownFallas = new List<DropDownList>();
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spc_ObtenerDropDownList";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_DROP", IdDrop);
                da.Fill(ds);
                DropDownList SeleccionaDrop = new DropDownList();
                SeleccionaDrop.IdDrop = -1;
                SeleccionaDrop.ValorDrop = "-- Selecciona --";
                DropDownFallas.Add(SeleccionaDrop);
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    DropDownList registro = new DropDownList();
                    registro.IdDrop = Convert.ToInt32(fila["IdDrop"].ToString());
                    registro.ValorDrop = fila["ValorDrop"].ToString();
                    DropDownFallas.Add(registro);
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
            return DropDownFallas;
        }
        public List<DropDownList> ObtenerDropDownString(string IdDrop)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            List<DropDownList> DropDownFallas = new List<DropDownList>();
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spc_ObtenerDropDownList";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_DROP", IdDrop);
                da.Fill(ds);
                DropDownList SeleccionaDrop = new DropDownList();
                SeleccionaDrop.IdDrop = -1;
                SeleccionaDrop.ValorDrop = "-- Selecciona --";
                DropDownFallas.Add(SeleccionaDrop);
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    DropDownList registro = new DropDownList();
                    registro.Value = fila["IdDrop"].ToString();
                    registro.ValorDrop = fila["ValorDrop"].ToString();
                    DropDownFallas.Add(registro);
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
            return DropDownFallas;
        }
        public void RegistroEvento(Evento DataEvento)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spi_RegistroEvento";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_ZONA", DataEvento.IdZona);
                cmd.Parameters.AddWithValue("@USUARIO", DataEvento.Autor);
                cmd.Parameters.AddWithValue("@DESCRIPCION_EVENTO", DataEvento.DescripcionEvento);
                cmd.Parameters.AddWithValue("@ACTIVIDAD_REALIZADA", DataEvento.ActividadRealizada);
                cmd.Parameters.AddWithValue("@EQUIPOS", DataEvento.EquiposUtilizados);
                cmd.Parameters.AddWithValue("@CANTIDAD", DataEvento.Cantidad);
                cmd.Parameters.AddWithValue("@ORIGEN", DataEvento.Origen);
                cmd.Parameters.AddWithValue("@STATUS", DataEvento.Status);
                cmd.Parameters.AddWithValue("@ADJUNTO", null);
                cmd.Parameters.AddWithValue("@TIEMPO_EMPLEADO", DataEvento.TiempoEmpleado);
                cmd.Parameters.AddWithValue("@ID_MANTENIMIENTO", DataEvento.IdMantenimiento);
                cmd.Parameters.AddWithValue("@ID_FALLA", DataEvento.IdFalla);
                cmd.Parameters.AddWithValue("@ADJUNTO2", null);
                cmd.Parameters.AddWithValue("@ADJUNTO3", null);
                cmd.Parameters.AddWithValue("@ADJUNTO4", null);
                cmd.Parameters.AddWithValue("@OBSERVACIONES", DataEvento.Observaciones);
                cmd.Parameters.AddWithValue("@FECHA_INICIO", DataEvento.FechaInicio);
                cmd.Parameters.AddWithValue("@FECHA_FIN", DataEvento.FechaFin);
                cmd.Parameters.AddWithValue("@MASTER_NUMBER", DataEvento.MasterNumber);

                da.Fill(ds);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                con.Close();
            }

        }
        public void CambioEstadoActividad(int idActividad)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spu_CambiaEstadoActividad";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", idActividad);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                con.Close();
            }
        }
        public int ObtenerIndiceEvento()
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            int MasterNumber = 0;
            try
            {
                cmd.Connection = con.Conex;
                cmd.CommandText = "spc_ObtenerMasterNumber";
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
                MasterNumber = Convert.ToInt32(ds.Tables[0].Rows[0]["MasterNumber"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                con.Close();
            }
            return MasterNumber;
        }
    }
}