using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurorita2018.Models
{
    public class Evento
    {
        public int IdZona { get; set; }
        public string Autor { get; set; }
        public string DescripcionEvento { get; set; }
        public string ActividadRealizada { get; set; }
        public string EquiposUtilizados { get; set; }
        public string Cantidad { get; set; }
        public string Status { get; set; }
        public string TiempoEmpleado { get; set; }
        public string IdMantenimiento { get; set; }
        public string IdFalla { get; set; }
        public string Observaciones { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Origen { get; set; }
        public string FechaOT { get; set; }
        //public string Adjunto2 { get; set; }
        //public string Adjunto3 { get; set; }
        //public string Adjunto4 { get; set; }
        public string Adjunto { get; set; }
        public int MasterNumber { get; set; }


    }
}