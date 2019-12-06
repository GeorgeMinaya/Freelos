using ACL.MegaCentro.DALC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACL.MegaCentro.BE;
using ACL.MegaCentro.DM;
using System.Configuration;

namespace ACL.MegaCentro.DALC.SourceCodes
{
    public class ErrorDALC : IErrorDALC
    {
        private MegaCentroDataContext model;

        public ErrorDALC() {
            this.model = new MegaCentroDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }

        public void Registrar(int Tipo, string Mensaje, string Metodo, int IdUsuario, string NumeroGuia, int? IdOrdenCarga)
        {
            Error error = new Error()
            {
                Fecha = DateTime.Now,
                Tipo = Tipo,
                Mensaje = Mensaje,
                Metodo = Metodo,
                IdUsuario = IdUsuario,
                NumeroGuia = NumeroGuia,
                IdOrdenCarga = IdOrdenCarga
            };

            model.Errors.InsertOnSubmit(error);
            model.SubmitChanges();
        }
    }
}
