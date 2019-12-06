using Salud.Ocupacional.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSaludOcupacional.Models
{
    public class BaseModel
    {
        public IEnumerable<MenuBE>  Listmenu        { get; set; }
        public ResponseLoginBE      Usuario         { get; set; }
    }
}