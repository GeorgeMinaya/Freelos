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
    public class OcupacionBL : IOcupacionBL
    {
        private IOcupacionDA objOcupacionDA;
        Result result;

        public OcupacionBL()
        {
            objOcupacionDA = new OcupacionDA();
            result = new Result();
            result.Code = Notification.OperationCode.Exito.GetHashCode();
            result.Message = Notification.Mensaje.Exito;
        }
        public Result ListarLogin()
        {
            try
            {
                var data = objOcupacionDA.Listar_Ocupacion();
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
        public Result Buscar(int IdOcupacion)
        {
            try
            {
                var data = objOcupacionDA.BuscarOcupacion(IdOcupacion);
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
        public Result RegistrarOcupacion(OcupacionBE.ResponseOcupacionBE ObjRequestOcupacionBE)
        {
            try
            {
                var data = objOcupacionDA.RegistrarOcupacion(ObjRequestOcupacionBE);
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
        public Result ModificarOcupacion(OcupacionBE.ResponseOcupacionBE objOcupacionBE)
        {
            try
            {
                if (objOcupacionBE.Activo.Equals(false)) { 
                    var data = objOcupacionDA.EliminarOcupacion(objOcupacionBE);
                    result.Data = data;
                } else {
                    var data = objOcupacionDA.ModificarOcupacion(objOcupacionBE);
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
