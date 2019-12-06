using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salud.Ocupacional.BE;
using ACL.Community;
using System.Data.SqlClient;
using System.Security.Cryptography;
using ACL.Community.Response;
using ACL.Community.Exceptions;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.DA.Interfaces;
using Salud.Ocupacional.DA.SourceCodes;

namespace Salud.Ocupacional.BL.SorceCodes
{
    public class CondicionBL : ICondicionBL
    {
        private ICondicionDA objCondicionDA;
        Result result;

        public CondicionBL()
        {
            objCondicionDA = new CondicionDA();
            result = new Result();
            result.Code = Notification.OperationCode.Exito.GetHashCode();
            result.Message = Notification.Mensaje.Exito;
        }
        public Result ListarLogin()
        {
            try
            {
                var data = objCondicionDA.Listar_Condicion();
                result.Data = data;
            }
            catch (SqlException sqlex)
            {
                result.Code = Notification.OperationCode.ErrorDataBase.GetHashCode(); ;
                result.Message = sqlex.Message;
            }
            catch (CustomException cusex)
            {
                result.Code = Notification.OperationCode.ErrorCustom.GetHashCode(); ;
                result.Message = cusex.Message;
            }
            catch (Exception ex)
            {
                result.Code = Notification.OperationCode.ErrorNotControl.GetHashCode(); ;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result Buscar(int IdCondicion)
        {
            try
            {
                var data = objCondicionDA.BuscarCondicion(IdCondicion);
                result.Data = data;
            }
            catch (SqlException sqlex)
            {
                result.Code = Notification.OperationCode.ErrorDataBase.GetHashCode(); ;
                result.Message = sqlex.Message;
            }
            catch (CustomException cusex)
            {
                result.Code = Notification.OperationCode.ErrorCustom.GetHashCode(); ;
                result.Message = cusex.Message;
            }
            catch (Exception ex)
            {
                result.Code = Notification.OperationCode.ErrorNotControl.GetHashCode(); ;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result RegistrarCondicion(CondicionBE.ResponseCondicionBE ObjRequestCondicionBE)
        {
            try
            {
                var data = objCondicionDA.RegistrarCondicion(ObjRequestCondicionBE);
                result.Data = data;
            }
            catch (SqlException sqlex)
            {
                result.Code = Notification.OperationCode.ErrorDataBase.GetHashCode(); ;
                result.Message = sqlex.Message;
            }
            catch (CustomException cusex)
            {
                result.Code = Notification.OperationCode.ErrorCustom.GetHashCode(); ;
                result.Message = cusex.Message;
            }
            catch (Exception ex)
            {
                result.Code = Notification.OperationCode.ErrorNotControl.GetHashCode(); ;
                result.Message = ex.Message;
            }
            return result;
        }
        public Result ModificarCondicion(CondicionBE.ResponseCondicionBE objCondicionBE)
        {
            try
            {
                if (objCondicionBE.Activo.Equals(false)) { 
                    var data = objCondicionDA.EliminarCondicion(objCondicionBE);
                    result.Data = data;
                } else {
                    var data = objCondicionDA.ModificarCondicion(objCondicionBE);
                    result.Data = data;
                }
            }
            catch (SqlException sqlex)
            {
                result.Code = Notification.OperationCode.ErrorDataBase.GetHashCode(); ;
                result.Message = sqlex.Message;
            }
            catch (CustomException cusex)
            {
                result.Code = Notification.OperationCode.ErrorCustom.GetHashCode(); ;
                result.Message = cusex.Message;
            }
            catch (Exception ex)
            {
                result.Code = Notification.OperationCode.ErrorNotControl.GetHashCode(); ;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
