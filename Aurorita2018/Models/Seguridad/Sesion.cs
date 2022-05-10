using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurorita2018.Models.Seguridad
{
    public class Sesion
    {

        public static string idUsuario
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session["Usuario"];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["Usuario"] = value;
            }
        }


        public static string usuarioNombre
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session["NombreUsuario"];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["NombreUsuario"] = value;
            }
        }
        public static string TipoUsuario
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session["TipoUsuario"];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["TipoUsuario"] = value;
            }
        }
    }
}
