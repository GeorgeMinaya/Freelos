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
    public class EmpresaBL : IEmpresaBL
    {
        private IEmpresaDA objEmpresaDA;
        Result result;

        public EmpresaBL()
        {
            objEmpresaDA = new EmpresaDA();
            result = new Result();
            result.Code = Notification.OperationCode.Exito.GetHashCode();
            result.Message = Notification.Mensaje.Exito;
        }
        public Result Listar()
        {
            try
            {
                var data = objEmpresaDA.Listar_Empresa();
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
        public Result Buscar(int IdEmpresa)
        {
            try
            {
                var data = objEmpresaDA.BuscarEmpresa(IdEmpresa);
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
        public Result RegistrarEmpresa(EmpresaBE.ResponseEmpresaBE ObjRequestEmpresaBE)
        {
            try
            {
                var data = objEmpresaDA.RegistrarEmpresa(ObjRequestEmpresaBE);
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
        public Result ModificarEmpresa(EmpresaBE.ResponseEmpresaBE objEmpresaBE)
        {
            try
            {
                if (objEmpresaBE.Activo.Equals(false)) { 
                    var data = objEmpresaDA.EliminarEmpresa(objEmpresaBE);
                    result.Data = data;
                } else {
                    var data = objEmpresaDA.ModificarEmpresa(objEmpresaBE);
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
