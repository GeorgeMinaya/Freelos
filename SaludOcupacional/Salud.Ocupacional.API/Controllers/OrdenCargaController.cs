using ACL.MegaCentro.BE;
using ACL.MegaCentro.BL.Interfaces;
using ACL.MegaCentro.BL.SorceCodes;
using Lindley.General.Communication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACL.MegaCentro.API.Resources;
using System.Net.Sockets;

namespace ACL.MegaCentro.API.Controllers
{
    [RoutePrefix("api/OrdenCarga")]
    public class OrdenCargaController : ApiController
    {
        IOrdenCargaBL objOrdenCargaBL;
        public OrdenCargaController()
        {
            objOrdenCargaBL = new OrdenCargaBL();
        }

        [HttpPost]
        [Route("EnvaseIniciar")]
        public IHttpActionResult EnvaseIniciar([FromBody] OrdenCargaBE objOrdenCargaBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.EnvaseIniciar((int)objOrdenCargaBE.IdUsuarioTransportista,
                (int)objOrdenCargaBE.IdOrdenCarga, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("OtrosIniciar")]
        public IHttpActionResult OtrosIniciar([FromBody] OrdenCargaBE objOrdenCargaBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.OtrosIniciar((int)objOrdenCargaBE.IdUsuarioTransportista,
                (int)objOrdenCargaBE.IdOrdenCarga, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("CambiosIniciar")]
        public IHttpActionResult CambiosIniciar([FromBody] OrdenCargaBE objOrdenCargaBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.CambiosIniciar((int)objOrdenCargaBE.IdUsuarioTransportista,
                (int)objOrdenCargaBE.IdOrdenCarga, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("NoPlanIniciar")]
        public IHttpActionResult NoPlanIniciar([FromBody] OrdenCargaBE objOrdenCargaBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.NoPlanIniciar((int)objOrdenCargaBE.IdUsuarioTransportista,
                (int)objOrdenCargaBE.IdOrdenCarga, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("TransportistaFinalizar")]
        public IHttpActionResult TransportistaFinalizar([FromBody] OrdenCargaBE objOrdenCargaBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.TransportistaFinalizar(objOrdenCargaBE, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("ConsultarGuia/{IdCentroLogin}/{IdUsuarioTransportista}/{NumeroGuia}")]
        public IHttpActionResult ConsultarGuia(int IdCentroLogin, int IdUsuarioTransportista, string NumeroGuia)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.ConsultarGuia(IdCentroLogin, IdUsuarioTransportista, NumeroGuia.ToUpper() , ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("ConsultarGuiaAS400/{NumeroGuia}")]
        public IHttpActionResult ConsultarGuiaAS400(string NumeroGuia)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.ConsultarGuiaAS400(NumeroGuia.ToUpper(), ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("InspectorIniciar")]
        public IHttpActionResult InspectorIniciar([FromBody] OrdenCargaBE objOrdenCargaBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.InspectorIniciar((int)objOrdenCargaBE.IdOrdenCarga,
                (int)objOrdenCargaBE.IdUsuarioSupervisor, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("InspectorFinalizar")]
        public IHttpActionResult InspectorFinalizar([FromBody] OrdenCargaBE objOrdenCargaBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.InspectorFinalizar(objOrdenCargaBE, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("ListarPendientes")]
        public IHttpActionResult ListarTodosPendientes()
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.ListarTodosPendientes(ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("ListarPendientes/{CentroLogin}")]
        public IHttpActionResult ListarPendientes(string CentroLogin)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.ListarPendientes(CentroLogin, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("TransportistaCancelar")]
        public IHttpActionResult TransportistaCancelar([FromBody] int IdOrdenCarga)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.TransportistaCancelar(IdOrdenCarga,
                ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("BorealEnviar")]
        public IHttpActionResult BorealEnviar([FromBody] OrdenCargaBE objOrdenCargaBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.BorealEnviar(objOrdenCargaBE, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("ListarFinalizados/{IdCentro}/{FechaInicio}/{FechaFinal}")]
        public IHttpActionResult ListarFinalizados(int IdCentro, string FechaInicio, string FechaFinal)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.ListarFinalizados(IdCentro, FechaInicio, FechaFinal, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("BuscarDiferencias/{IdOrdenCarga}")]
        public IHttpActionResult BuscarDiferencias(int IdOrdenCarga)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.BuscarDiferencias(IdOrdenCarga, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("ImprimirCargo/{IdOrdenCarga}")]
        public IHttpActionResult ImprimirCargo(int idOrdenCarga)
        {
            int code = Notification.OperationCode.Exito.GetHashCode();
            string message = string.Empty;
            try
            {
                string reportingserver = ConexionSSRS.ResourceManager.GetString(ConfigurationManager.AppSettings["Enviroment"]);

                string strReportUser = "ucaservices";
                string strReportUserPW = "Lindley@123";
                string strReportUserDomain = "jrdomain";

                string sTargetURL = reportingserver +
                   "/ACL.MegaCentro.Report/rptCargoTransportista&rs:Command=Render&rs:format=PDF&IdOrdenCarga=" +
                   idOrdenCarga.ToString();

                HttpWebRequest req =
                      (HttpWebRequest)WebRequest.Create(sTargetURL);
                req.PreAuthenticate = true;
                req.Credentials = new System.Net.NetworkCredential(
                    strReportUser,
                    strReportUserPW,
                    strReportUserDomain);

                HttpWebResponse HttpWResp = (HttpWebResponse)req.GetResponse();

                Stream fStream = HttpWResp.GetResponseStream();

                byte[] fileBytes = ReadFully(fStream);

                HttpWResp.Close();

                return Ok(new Result() { Code = code, Data = fileBytes, Message = message });
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return Ok(new Result() { Code = code, Data = null, Message = message });
            }
        }

        [HttpPost]
        [Route("ReImprimirCargo")]
        public IHttpActionResult ReImprimirCargo(ReImpresoraDTO oReImpresoraDTO)
        {
            int code = Notification.OperationCode.Exito.GetHashCode();
            string message = string.Empty;
            try
            {
                string reportingserver = ConexionSSRS.ResourceManager.GetString(ConfigurationManager.AppSettings["Enviroment"]);

                string strReportUser = "ucaservices";
                string strReportUserPW = "Lindley@123";
                string strReportUserDomain = "jrdomain";

                string sTargetURL = reportingserver +
                   "/ACL.MegaCentro.Report/rptCargoTransportista&rs:Command=Render&rs:format=PDF&IdOrdenCarga=" +
                   oReImpresoraDTO.Id.ToString();

                HttpWebRequest req =
                      (HttpWebRequest)WebRequest.Create(sTargetURL);
                req.PreAuthenticate = true;
                req.Credentials = new System.Net.NetworkCredential(
                    strReportUser,
                    strReportUserPW,
                    strReportUserDomain);

                HttpWebResponse HttpWResp = (HttpWebResponse)req.GetResponse();

                Stream fStream = HttpWResp.GetResponseStream();

                byte[] fileBytes = ReadFully(fStream);

                HttpWResp.Close();

                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { NoDelay = true };

                /* IP de la Impresora de Sistemas */
                //string ip = "192.168.216.83";
                IPAddress direccion = IPAddress.Parse(oReImpresoraDTO.IP);
                IPEndPoint ipep = new IPEndPoint(direccion, 9100);
                
                clientSocket.Connect(ipep);
                
                clientSocket.Send(fileBytes);
                clientSocket.Close();

                return Ok(new Result() { Code = code, Data = "0", Message = message });
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return Ok(new Result() { Code = code, Data = null, Message = message });
            }
        }        

        [HttpGet]
        [Route("DescargarDocumentoCargo/{IdOrdenCarga}")]
        public HttpResponseMessage DescargarDocumentoCargo(int idOrdenCarga)
        {
            string reportingserver = ConexionSSRS.ResourceManager.GetString(ConfigurationManager.AppSettings["Enviroment"]);

            int code = Notification.OperationCode.Exito.GetHashCode();
            string message = string.Empty;
            try
            {


                string strReportUser = "ucaservices";
                string strReportUserPW = "Lindley@123";
                string strReportUserDomain = "jrdomain";

                string sTargetURL = reportingserver +
                   "/ACL.MegaCentro.Report/rptCargoTransportista&rs:Command=Render&rs:format=PDF&IdOrdenCarga=" +
                   idOrdenCarga.ToString();

                HttpWebRequest req =
                      (HttpWebRequest)WebRequest.Create(sTargetURL);
                req.PreAuthenticate = true;
                req.Credentials = new System.Net.NetworkCredential(
                    strReportUser,
                    strReportUserPW,
                    strReportUserDomain);

                HttpWebResponse HttpWResp = (HttpWebResponse)req.GetResponse();

                Stream fStream = HttpWResp.GetResponseStream();

                byte[] fileBytes = ReadFully(fStream);

                HttpWResp.Close();

                var report = ReportUtil.createReport(fileBytes);
                return report;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        [HttpGet]
        [Route("ConsultarPI/{NumeroGuia}")]
        public IHttpActionResult ConsultarPI(string NumeroGuia)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.ConsultarPI(NumeroGuia, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("CargoTransportista/{IdOrdenCarga}")]
        public IHttpActionResult CargoTransportista(int IdOrdenCarga)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.CargoTransportista(IdOrdenCarga, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("BuscarIdOrdenCarga/{NumeroGuia}")]
        public IHttpActionResult BuscarIdOrdenCarga(string NumeroGuia)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.BuscarIdOrdenCarga(NumeroGuia, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("DevolucionTotal/{IdCentroLogin}/{IdUsuarioInspector}/{NumeroGuia}")]
        public IHttpActionResult CargoTransportista(int IdCentroLogin, int IdUsuarioInspector, string NumeroGuia)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.DevolucionTotal(IdCentroLogin, IdUsuarioInspector, NumeroGuia, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("DescargarConsultarInspeccion/{FechaInicio}/{FechaFinal}/{IdCentro}")]
        public HttpResponseMessage DescargarConsultarInspeccion(string FechaInicio, string FechaFinal, int IdCentro)
        {
            int code = Notification.OperationCode.Exito.GetHashCode();
            string message = string.Empty;
            try
            {
                string reportingserver = ConexionSSRS.ResourceManager.GetString(ConfigurationManager.AppSettings["Enviroment"]);

                string strReportUser = "ucaservices";
                string strReportUserPW = "Lindley@123";
                string strReportUserDomain = "jrdomain";

                string sTargetURL = reportingserver +
                   "/ACL.MegaCentro.Report/rptConsultarInspeccion&rs:Command=Render&rs:format=Excel&FechaInicio=" +
                   FechaInicio + "&FechaFinal=" + FechaFinal + "&IdCentro=" + IdCentro.ToString();

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sTargetURL);
                req.PreAuthenticate = true;
                req.Credentials = new System.Net.NetworkCredential(
                    strReportUser,
                    strReportUserPW,
                    strReportUserDomain);

                HttpWebResponse HttpWResp = (HttpWebResponse)req.GetResponse();

                Stream fStream = HttpWResp.GetResponseStream();

                byte[] fileBytes = ReadFully(fStream);

                HttpWResp.Close();

                var report = ReportUtil.createReport(fileBytes);
                report.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                report.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "ReporteLiquidacion_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls"
                };
                return report;
                //return Ok(new Result() { Code = code, Data = fileBytes, Message = message }); IHttpActionResult
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                throw ex;
                //return Ok(new Result() { Code = code, Data = null, Message = message });
            }
        }

        [HttpGet]
        [Route("Eliminar/{IdOrdenCarga}/{IdUsuarioRegistro}")]
        public IHttpActionResult Eliminar(int IdOrdenCarga, int IdUsuarioRegistro)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.Eliminar(IdOrdenCarga, IdUsuarioRegistro, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("ReporteUtilizacion/{IdCentro}/{FechaInicio}/{FechaFinal}")]
        public IHttpActionResult ReporteUtilizacion(int IdCentro, string FechaInicio, string FechaFinal)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.ReporteUtilizacion(IdCentro, DateTime.Parse(FechaInicio), DateTime.Parse(FechaFinal), ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("ReporteUtilizacionExcel/{IdCentro}/{FechaInicio}/{FechaFinal}")]
        public HttpResponseMessage ReporteUtilizacionExcel(int IdCentro, string FechaInicio, string FechaFinal)
        {
            int code = Notification.OperationCode.Exito.GetHashCode();
            string message = string.Empty;
            try
            {
                string reportingserver = ConexionSSRS.ResourceManager.GetString(ConfigurationManager.AppSettings["Enviroment"]);

                string strReportUser = "ucaservices";
                string strReportUserPW = "Lindley@123";
                string strReportUserDomain = "jrdomain";

                string sTargetURL = reportingserver +
                   "/ACL.MegaCentro.Report/rptUtilizacion&rs:Command=Render&rs:format=Excel&FechaInicio=" +
                   FechaInicio + "&FechaFinal=" + FechaFinal + "&IdCentro=" + IdCentro.ToString();

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sTargetURL);
                req.PreAuthenticate = true;
                req.Credentials = new System.Net.NetworkCredential(
                    strReportUser,
                    strReportUserPW,
                    strReportUserDomain);

                HttpWebResponse HttpWResp = (HttpWebResponse)req.GetResponse();

                Stream fStream = HttpWResp.GetResponseStream();

                byte[] fileBytes = ReadFully(fStream);

                HttpWResp.Close();

                var report = ReportUtil.createReport(fileBytes);
                report.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                report.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "ReporteUtilizacion_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls"
                };
                return report;
                //return Ok(new Result() { Code = code, Data = fileBytes, Message = message }); IHttpActionResult
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                throw ex;
                //return Ok(new Result() { Code = code, Data = null, Message = message });
            }
        }

        [HttpGet]
        [Route("ReporteDevolucion/{IdCentro}/{FechaInicio}/{FechaFinal}")]
        public IHttpActionResult ReporteDevolucion(int IdCentro, string FechaInicio, string FechaFinal)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            objOrdenCargaBL = new OrdenCargaBL();
            var dataResult = objOrdenCargaBL.ReporteDevolucion(IdCentro, FechaInicio, FechaFinal, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }
    }
}
