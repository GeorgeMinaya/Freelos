using ACL.MegaCentro.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACL.MegaCentro.BE;
using Lindley.General.Communication;
using System.Data.SqlClient;
using ACL.MegaCentro.DALC.Interfaces;
using ACL.MegaCentro.DALC.SourceCodes;

namespace ACL.MegaCentro.BL.SorceCodes
{
    public class ImpresoraBL : IImpresoraBL
    {
        IImpresoraDALC objImpresoraDALC;

        public ImpresoraBL() {
            this.objImpresoraDALC = new ImpresoraDALC();
        }

        public IEnumerable<ImpresoraBE> Listar(int IdCentro, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                return objImpresoraDALC.Listar(IdCentro);
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

        public int? Registrar(ImpresoraBE objImpresoraBE, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                if (objImpresoraBE.IdImpresora.Equals(0))
                    return objImpresoraDALC.Registrar(objImpresoraBE);
                else
                    return objImpresoraDALC.Actualizar(objImpresoraBE);
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
