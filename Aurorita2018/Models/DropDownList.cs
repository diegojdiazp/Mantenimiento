using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurorita2018.Models
{
    public class DropDownList
    {
        public int IdDrop { get; set; }
        public string ValorDrop { get; set; }

        public string Value { get; set; }

        public DropDownList()
        {
            IdDrop = -1;
            ValorDrop = "Selecciona";
        }

    }

}