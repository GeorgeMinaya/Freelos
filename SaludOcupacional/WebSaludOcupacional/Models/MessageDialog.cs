using Salud.Ocupacional.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSaludOcupacional.Models
{
    public class MessageDialog
    {
        public string   Title     { get; set; }
        public string   Message   { get; set; }
        public int      Estado    { get; set; }
    }
}