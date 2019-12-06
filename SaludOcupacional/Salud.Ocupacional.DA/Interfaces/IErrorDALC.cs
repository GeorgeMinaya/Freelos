using ACL.MegaCentro.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACL.MegaCentro.DALC.Interfaces
{
    public interface IErrorDALC
    {
        void Registrar(int Tipo, string Mensaje, string Metodo, int IdUsuario, string NumeroGuia, int? IdOrdenCarga);
    }
}
