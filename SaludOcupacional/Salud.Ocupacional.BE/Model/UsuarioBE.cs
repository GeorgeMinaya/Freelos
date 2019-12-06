using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class RequestLoginBE
    {
        public string       DNI                 { get; set; }
        public string       Name                { get; set; }
        public int          Type                { get; set; }
        public string       Password            { get; set; }
        public string       Mobile              { get; set; }
        public string       Email               { get; set; }
    }
    public class Response{
        public ResponseLoginBE response                     { get; set; }
        public IEnumerable<ResponseLoginBE> lresponse       { get; set; }
    }
    public class ResponseLoginBE
    {
        public int              IdUsuario       { get; set; }
        public string           DNI             { get; set; }
        public string           Name            { get; set; }
        public string           LastName1       { get; set; }
        public string           LastName2       { get; set; }
        public int              Type            { get; set; }
        public string           Password        { get; set; }
        public string           Mobile          { get; set; }
        public bool             Activo          { get; set; }
        public string           Email           { get; set; }
        public DateTime         RegisterDate    { get; set; }
        public DateTime         LastAccesDate   { get; set; }
        public List<MenuBE.ResponseMenuBE>     menuList        { get; set; }
        public List<PerfilBE>   perfilList      { get; set; }
    }
}
