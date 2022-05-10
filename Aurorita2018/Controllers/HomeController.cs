using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Aurorita2018.Models;
using Aurorita2018.Models.Seguridad;
using Aurorita2018.DAO;


namespace Aurorita2018.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Inicio()
        {
            CuentaUsuario cuentaUser = new CuentaUsuario();
            cuentaUser.Usuario = Sesion.idUsuario;
            cuentaUser.Nombre = Sesion.usuarioNombre;
            cuentaUser.TipoUsuario = Sesion.TipoUsuario;

            if (cuentaUser.Usuario != null)
            {
                CuadroTareas();
                return View();
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", action = "Index" }));
            }
        }
        public ActionResult CuadroTareas()
        {
            CuentaUsuario cuentaUser = new CuentaUsuario();
            cuentaUser.Usuario = Sesion.idUsuario;
            cuentaUser.Nombre = Sesion.usuarioNombre;
            cuentaUser.TipoUsuario = Sesion.TipoUsuario;
            MenuInicioDAO CapaDatos = new MenuInicioDAO();
            List<TareasUsuario> listaTareasUsuario = CapaDatos.TareasUsuario(cuentaUser.Usuario, cuentaUser.TipoUsuario);
            if (listaTareasUsuario != null && listaTareasUsuario.Count > 0)
            {
                ViewBag.TareasUsuario = listaTareasUsuario;
            }
            else
            {
                ViewBag.TareasUsuario = null;
            }
            return View();
        }
        [HttpPost]
        public JsonResult FallasRegistradasUsuario(string usuario)
        {
            string respuesta = string.Empty;
            MenuInicioDAO CapaDatos = new MenuInicioDAO();
            List<EstadisticaUsuario> listaFallasRegistradas = new List<EstadisticaUsuario>();
            listaFallasRegistradas = CapaDatos.EstadisticaFallasRegistradas(usuario);
            if (listaFallasRegistradas != null && listaFallasRegistradas.Count != 0)
            {
                return Json(listaFallasRegistradas);
            }
            else
            {
                respuesta = "sin_datos";
                return Json(respuesta);
            }
        }
        public JsonResult TiposMantenimientosUsuario(string usuario)
        {
            string respuesta = string.Empty;
            MenuInicioDAO CapaDatos = new MenuInicioDAO();
            List<EstadisticaUsuario> listaFallasRegistradas = new List<EstadisticaUsuario>();
            listaFallasRegistradas = CapaDatos.EstadisticaTiposMantenimientosUsuario(usuario);
            if (listaFallasRegistradas != null && listaFallasRegistradas.Count != 0)
            {
                return Json(listaFallasRegistradas);
            }
            else
            {
                respuesta = "sin_datos";
                return Json(respuesta);
            }
        }
        [HttpPost]
        public JsonResult EstadisticaMensualEventos(string usuario)
        {
            string respuesta = string.Empty;
            MenuInicioDAO CapaDatos = new MenuInicioDAO();
            List<EstadisticaUsuario> ListaEventosMensuales = new List<EstadisticaUsuario>();
            ListaEventosMensuales = CapaDatos.EstadisticasEventosMensuales(usuario);
            if (ListaEventosMensuales != null && ListaEventosMensuales.Count > 0)
            {
                return Json(ListaEventosMensuales);
            }
            else
            {
                respuesta = "sin_datos";
                return Json(respuesta);
            }
        }
        [HttpPost]
        public JsonResult EstadisticaPendientesUsuario(string usuario)
        {
            string respuesta = string.Empty;
            MenuInicioDAO CapaDatos = new MenuInicioDAO();
            List<EstadisticaUsuario> ListaEventosMensuales = new List<EstadisticaUsuario>();
            ListaEventosMensuales = CapaDatos.EstatidisticaEventosEstadosPorUsuario(usuario);
            if (ListaEventosMensuales != null && ListaEventosMensuales.Count > 0)
            {
                return Json(ListaEventosMensuales.Where(l => l.Estado == "PENDIENTE"));
            }
            else
            {
                respuesta = "sin_datos";
                return Json(respuesta);
            }
        }
        [HttpPost]
        public JsonResult EstadoEnventosUsuario(string usuario)
        {
            string respuesta = string.Empty;
            MenuInicioDAO CapaDatos = new MenuInicioDAO();
            List<EstadisticaUsuario> ListaEventosMensuales = new List<EstadisticaUsuario>();
            ListaEventosMensuales = CapaDatos.EstatidisticaEventosEstadosPorUsuario(usuario);
            if (ListaEventosMensuales != null && ListaEventosMensuales.Count > 0)
            {
                return Json(ListaEventosMensuales);
            }
            else
            {
                respuesta = "sin_datos";
                return Json(respuesta);
            }
        }
        [HttpPost]
        public JsonResult EstadisticaCulminadoUsuario(string usuario)
        {
            string respuesta = string.Empty;
            MenuInicioDAO CapaDatos = new MenuInicioDAO();
            List<EstadisticaUsuario> ListaEventosMensuales = new List<EstadisticaUsuario>();
            ListaEventosMensuales = CapaDatos.EstatidisticaEventosEstadosPorUsuario(usuario);
            if (ListaEventosMensuales != null && ListaEventosMensuales.Count > 0)
            {
                return Json(ListaEventosMensuales.Where(l => l.Estado == "CULMINADO"));
            }
            else
            {
                respuesta = "sin_datos";
                return Json(respuesta);
            }
        }
        [HttpPost]
        public JsonResult EstadisticaAnualUsuario(string usuario)
        {
            string respuesta = string.Empty;
            MenuInicioDAO CapaDatos = new MenuInicioDAO();
            List<EstadisticaUsuario> EventosAnuales = new List<EstadisticaUsuario>();
            EventosAnuales = CapaDatos.EstadisticaResgistroAnualUsuario(usuario);
            if (EventosAnuales != null && EventosAnuales.Count > 0)
            {
                return Json(EventosAnuales);
            }
            else
            {
                respuesta = "sin_datos";
                return Json(respuesta);
            }
        }
        public ActionResult CerrarSesion()
        {
            Sesion.idUsuario = null;
            Sesion.usuarioNombre = null;
            Sesion.TipoUsuario = null;

            return RedirectToAction("Index", "login");
        }
        [HttpPost]
        public JsonResult ObtenerPlanificacionUsusario()
        {
            List<TareasUsuario> listaTareasUsuario = new List<TareasUsuario>();
            MenuInicioDAO CapaDatos = new MenuInicioDAO();
            CuentaUsuario user = new CuentaUsuario();
            user.Usuario = Sesion.idUsuario;
            user.TipoUsuario = Sesion.TipoUsuario;
            listaTareasUsuario = CapaDatos.TareasUsuario(user.Usuario, user.TipoUsuario);

            if (listaTareasUsuario != null && listaTareasUsuario.Count > 0)
            {
                return Json(listaTareasUsuario);
            }
            else
            {
                return Json("sin_datos");
            }
        }
    }
}