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
    public class CentroDALC : ICentroDALC
    {
        private MegaCentroDataContext model;

        public CentroDALC() {
            this.model = new MegaCentroDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }

        public IEnumerable<CentroBE> List()
        {
            var query = from c in model.Centros
                        where !c.Eliminado && c.Activo
                        orderby c.Nombre
                        select new CentroBE()
                        {
                            IdCentro = c.IdCentro,
                            Nombre = c.Nombre,
                            Codigo = c.Codigo,
                            Activo = c.Activo
                        };

            return query;
        }
    }
}
