using ACL.Community.Exceptions;
using ACL.Community.Response;
using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Helpers;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.DA.Interfaces;
using Salud.Ocupacional.DA.SourceCodes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.SorceCodes
{
    public class ArchivoBL : IArchivoBL
    {
        readonly IArchivoDA ObjArchivoDA;
        readonly Result result;
        public ArchivoBL()
        {
            ObjArchivoDA = new ArchivoDA();
            result = new Result
            {
                Code = Notification.OperationCode.Exito.GetHashCode(),
                Message = Notification.Mensaje.Exito
            };
        }

        public Result DatosRegistrar()
        {
            try
            {
                var data = ObjArchivoDA.DatosRegistrar();

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

        public Result RegistrarArchivo(ArchivoBE.ResponseArchivoBE archivoBE)
        {
            try
            {
                var carpetaDestinoFiles = $"{ Directory.GetCurrentDirectory() }/{ ConstantesHelper.SERVIDOR_RUTA_FILES }/";
                var nombre = $"{ archivoBE.IdContract }-{ DateTime.Now.ToString("ddMMyyyyHHmmss") }.csv";                
                var rutaFinalFiles = Path.Combine(carpetaDestinoFiles, nombre);

                if (!Directory.Exists(carpetaDestinoFiles))
                    Directory.CreateDirectory(carpetaDestinoFiles);

                byte[] bytesFiles = Convert.FromBase64String(archivoBE.ArchivoBase64);

                File.WriteAllBytes(rutaFinalFiles, bytesFiles);

                archivoBE.UrlArchivo = $"{ ConstantesHelper.IP_SERVIDOR }{ ConstantesHelper.SERVIDOR_RUTA_FILES }/{ nombre }";

                var data = ObjArchivoDA.RegistrarArchivo(archivoBE);

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
    }
}
