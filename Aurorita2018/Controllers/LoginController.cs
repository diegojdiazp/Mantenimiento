using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aurorita2018.Models;
using Aurorita2018.Models.Seguridad;
using Aurorita2018.DAO;
using System.Web.Routing;

namespace Aurorita2018.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            CuentaUsuario cuentaUsuario = new CuentaUsuario();
            cuentaUsuario = null;
            return View("Index", cuentaUsuario);
        }
        [HttpPost]
        public ActionResult Ingreso(string usuario, string password)
        {
            LoginDAO getLoging = new LoginDAO();
            CuentaUsuario CuentaUsuario = new CuentaUsuario();
            CuentaUsuario = getLoging.LoginUser(usuario, password);
            if (CuentaUsuario.Usuario != null || CuentaUsuario.Usuario == string.Empty)
            {
                Sesion.idUsuario = CuentaUsuario.Usuario;
                Sesion.usuarioNombre = CuentaUsuario.Nombre;
                Sesion.TipoUsuario = CuentaUsuario.TipoUsuario;
                return new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Home", action = "Inicio" }));
            }
            else
            {
                CuentaUsuario.Usuario = "Error";
                return View("Index", CuentaUsuario);
            }
        }
    }
}