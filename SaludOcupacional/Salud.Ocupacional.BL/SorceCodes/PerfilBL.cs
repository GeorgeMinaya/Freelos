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
    public class PerfilBL : IPerfilBL
    {
        private IPerfilDA objPerfilDA;
        Result result;

        public PerfilBL()
        {
            objPerfilDA = new PerfilDA();
            result = new Result();
            result.Code = Notification.OperationCode.Exito.GetHashCode();
            result.Message = Notification.Mensaje.Exito;
        }
        public PerfilBE Buscar(int IdPerfil, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                return objPerfilDA.Buscar(IdPerfil);
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

        public Result Listar()
        {
            try
            {
                var data = objPerfilDA.Listar();
                if (data == null)
                    throw new CustomException("Credenciales del usuario son incorrectos. Revisar usuario y contraseña");

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

        public PerfilBE Registrar(PerfilBE objPerfilBE, ref int code, ref string message) {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                if (!objPerfilBE.IdPerfil.Equals(0)) {
                    var IdPerfil = objPerfilDA.Actualizar(objPerfilBE);
                    if (IdPerfil.Equals(0))
                        throw new ArgumentException("El perfil no pudo ser actualizado.");

                    return objPerfilBE;
                }
                else {
                    var IdPerfil = objPerfilDA.Registrar(objPerfilBE);
                    if (IdPerfil.Equals(0))
                        throw new ArgumentException("El perfil no pudo ser registrado");

                    objPerfilBE.IdPerfil = IdPerfil;
                    return objPerfilBE;
                }
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
