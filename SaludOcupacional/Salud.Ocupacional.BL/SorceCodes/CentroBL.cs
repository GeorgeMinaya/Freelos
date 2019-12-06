using ACL.MegaCentro.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACL.MegaCentro.BE;
using ACL.MegaCentro.DALC.Interfaces;
using ACL.MegaCentro.DALC.SourceCodes;
using Lindley.General.Communication;
using System.Data.SqlClient;

namespace ACL.MegaCentro.BL.SorceCodes
{
    public class CentroBL : ICentroBL
    {
        private ICentroDALC objCentroDALC;

        public CentroBL() {
            this.objCentroDALC = new CentroDALC();
        }

        public IEnumerable<CentroBE> List(ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                return objCentroDALC.List();
            }
            catch (SqlException exSql)
            {
                message = exSql.Message;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
    }
}
