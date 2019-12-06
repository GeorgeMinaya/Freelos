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
    public class MarcaModeloBL : IMarcaModeloBL
    {
        private IMarcaModeloDA objMarcaModeloDA;
        Result result;

        public MarcaModeloBL()
        {
            objMarcaModeloDA = new MarcaModeloDA();
            result = new Result();
            result.Code = Notification.OperationCode.Exito.GetHashCode();
            result.Message = Notification.Mensaje.Exito;
        }
        public Result ListarLogin()
        {
            try
            {
                var data = objMarcaModeloDA.Listar_MarcaModelo();
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
        public Result Buscar(int IdMarcaModelo)
        {
            try
            {
                var data = objMarcaModeloDA.BuscarMarcaModelo(IdMarcaModelo);
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
        public Result RegistrarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE ObjRequestMarcaModeloBE)
        {
            try
            {
                var data = objMarcaModeloDA.RegistrarMarcaModelo(ObjRequestMarcaModeloBE);
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
        public Result ModificarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE objMarcaModeloBE)
        {
            try
            {
                if (objMarcaModeloBE.Activo.Equals(false)) { 
                    var data = objMarcaModeloDA.EliminarMarcaModelo(objMarcaModeloBE);
                    result.Data = data;
                } else {
                    var data = objMarcaModeloDA.ModificarMarcaModelo(objMarcaModeloBE);
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
