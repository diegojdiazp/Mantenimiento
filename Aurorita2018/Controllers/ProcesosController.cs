using Aurorita2018.DAO;
using Aurorita2018.Models;
using Aurorita2018.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using EvoPdf;

namespace Aurorita2018.Controllers
{
    public class ProcesosController : Controller
    {
        public string OtVisible = string.Empty;
        private bool ValidaSession()
        {
            CuentaUsuario cuentaUser = new CuentaUsuario();
            cuentaUser.Usuario = Sesion.idUsuario;
            cuentaUser.Nombre = Sesion.usuarioNombre;
            cuentaUser.TipoUsuario = Sesion.TipoUsuario;
            ViewBag.UserAccount = cuentaUser;
            if (cuentaUser.Usuario != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public ActionResult RegistrarEvento(TareasUsuario item)
        {
            if (ValidaSession())
            {
                ViewBag.IdActividad = item.Indice;
                OtVisible = "disabled";
                ViewBag.DropFallas = ObtenerDropFallas();
                ViewBag.DropMantenimiento = ObtenerDropTipoMantenimiento();
                ViewBag.DropEstados = ObtenerDropEstados();
                ViewBag.DropZonas = ObtenerDropZonas();
                ViewBag.MasterNumber = ObtenerindiceRegistro();
                ViewBag.OtVisible = OtVisible;
                return View();
            }
            else
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", action = "Index" }));
            }
        }

        private int ObtenerindiceRegistro()
        {
            ProcesosDAO CapaDatos = new ProcesosDAO();
            int indiceRegistro = CapaDatos.ObtenerIndiceEvento();
            return indiceRegistro;
        }

        private List<DropDownList> ObtenerDropFallas()
        {
            ProcesosDAO CapaDatos = new ProcesosDAO();
            List<DropDownList> DropDownFallas = new List<DropDownList>();
            DropDownFallas = CapaDatos.ObtenerDropDown("Fallas");
            return DropDownFallas;
        }
        private List<DropDownList> ObtenerDropTipoMantenimiento()
        {
            ProcesosDAO CapaDatos = new ProcesosDAO();
            List<DropDownList> DropDownFallas = new List<DropDownList>();
            DropDownFallas = CapaDatos.ObtenerDropDown("Mantenimiento");
            return DropDownFallas;
        }
        private List<DropDownList> ObtenerDropEstados()
        {
            ProcesosDAO CapaDatos = new ProcesosDAO();
            List<DropDownList> DropDownEstados = new List<DropDownList>();
            DropDownEstados = CapaDatos.ObtenerDropDown("Estado");
            return DropDownEstados;
        }
        private List<DropDownList> ObtenerDropZonas()
        {
            ProcesosDAO CapaDatos = new ProcesosDAO();
            List<DropDownList> DropDownZonas = new List<DropDownList>();
            DropDownZonas = CapaDatos.ObtenerDropDown("Zonas");
            return DropDownZonas;
        }
        [HttpPost]
        public string CalcularTiempoEmpleado(string fechaInicio, string fechaFin)
        {
            string tiempoResultado = string.Empty;
            if (fechaFin != null && fechaInicio != null)
            {
                DateTime InicioEvento = Convert.ToDateTime(fechaInicio);
                DateTime FinEvento = Convert.ToDateTime(fechaFin);
                var TiempoEmpleado = FinEvento - InicioEvento;
                tiempoResultado = TiempoEmpleado.Days + "d " + TiempoEmpleado.Hours + "h  " + TiempoEmpleado.Minutes + "m ";
                if (TiempoEmpleado.TotalSeconds < 0)
                {
                    tiempoResultado = "sin_datos";
                }
            }
            else
            {
                tiempoResultado = "sin_datos";
            }

            return tiempoResultado;
        }
        [HttpPost]
        public JsonResult RegistroEvento(List<JSONArraySerialize> DataEvento)
        {
            if (ValidaSession())
            {
                try
                {
                    ProcesosDAO CapaDatos = new ProcesosDAO();
                    Evento Registro = new Evento();
                    IPHostEntry host;
                    string localIP = "";
                    int idActividad = 0;
                    host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress ip in host.AddressList)
                    {
                        if (ip.AddressFamily.ToString() == "InterNetwork")
                        {
                            localIP = ip.ToString();
                        }
                    }
                    Registro.Autor = DataEvento[0].Value;
                    Registro.FechaInicio = DataEvento[1].Value;
                    Registro.FechaFin = DataEvento[2].Value;
                    Registro.TiempoEmpleado = DataEvento[3].Value;
                    Registro.IdFalla = DataEvento[4].Value;
                    Registro.IdMantenimiento = DataEvento[5].Value;
                    Registro.Status = DataEvento[6].Value;
                    Registro.IdZona = Convert.ToInt32(DataEvento[7].Value);
                    Registro.DescripcionEvento = DataEvento[8].Value;
                    Registro.ActividadRealizada = DataEvento[9].Value;
                    Registro.EquiposUtilizados = DataEvento[10].Value;
                    Registro.Cantidad = DataEvento[11].Value;
                    Registro.Observaciones = DataEvento[12].Value;
                    Registro.MasterNumber = Convert.ToInt32(DataEvento[13].Value);
                    Registro.Origen = localIP;
                    idActividad = Convert.ToInt32(DataEvento[14].Value);
                    if (idActividad != 0 && Registro.Status == "2")
                    {
                        CapaDatos.CambioEstadoActividad(idActividad);
                    }
                    CapaDatos.RegistroEvento(Registro);

                    return Json("GuardadoOK");
                }
                catch
                {
                    return Json("error");
                }
            }
            else
            {
                return Json("sin_session");
            }
        }

        [HttpPost]
        public JsonResult ObtenerUsuarios()
        {
            ProcesosDAO CapaDatos = new ProcesosDAO();
            List<DropDownList> DropUsuarios = new List<DropDownList>();
            DropUsuarios = CapaDatos.ObtenerDropDownString("Usuarios");
            if (DropUsuarios != null)
            {
                return Json(DropUsuarios);
            }
            else
            {
                return Json("sin_datos");
            }

        }
        [HttpPost]
        public JsonResult DropZonas()
        {
            ProcesosDAO CapaDatos = new ProcesosDAO();
            List<DropDownList> DropDownZonas = new List<DropDownList>();
            DropDownZonas = CapaDatos.ObtenerDropDown("Zonas");
            if (DropDownZonas != null)
            {
                return Json(DropDownZonas);
            }
            else
            {
                return Json("sin_datos");
            }
        }

        [HttpPost]
        public FileResult GenerarPdfOT(FormCollection collection)
        {
            object model = null;
            ViewDataDictionary viewData = new ViewDataDictionary(model);

            // The string writer where to render the HTML code of the view 
            StringWriter stringWriter = new StringWriter();

            // Render the Index view in a HTML string 
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, "PrevisualizarOT", null);
            ViewContext viewContext = new ViewContext(
              ControllerContext,
              viewResult.View,
              viewData,
              new TempDataDictionary(),
              stringWriter
              );
            viewResult.View.Render(viewContext, stringWriter);

            // Get the view HTML string 
            string htmlToConvert = stringWriter.ToString();

            // Get the base URL 
            String currentPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
            String baseUrl = "http://localhost:50080";
            //String baseUrl = currentPageUrl.Substring(0, currentPageUrl.Length - "Convert_Current_Page/ConvertCurrentPageToPdf".Length);

            // Create a HTML to PDF converter object with default settings 
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
            
            // Convert the HTML string to a PDF document in a memory buffer 
            byte[] outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlToConvert, baseUrl);

            // Send the PDF file to browser 
            FileResult fileResult = new FileContentResult(outPdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "Convert_Current_Page.pdf";

            return fileResult;
        }
    }
}