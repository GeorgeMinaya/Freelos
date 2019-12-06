using ACL.MegaCentro.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACL.MegaCentro.BL.Interfaces
{
    public interface IOrdenCargaBL
    {
        #region Interface 27
        bool BorealEnviar(OrdenCargaBE objOrdenCargaBE, ref int code, ref string message);
        #endregion
        #region Reporte de liquidacion
        IEnumerable<OrdenCargaBE> ListarFinalizados(int IdCentro, string FechaInicio, string FechaFinal, ref int code, ref string message);
        OrdenCargaBE BuscarDiferencias(int IdOrdenCarga, ref int code, ref string message);
        #endregion

        object ConsultarPI(string NumeroGuia, ref int code, ref string message);

        OrdenCargaBE ConsultarGuia(int IdCentroLogin, int IdUsuarioTransportista, string NumeroGuia, ref int code, ref string message);
        OrdenCargaBE ConsultarGuiaAS400(string NumeroGuia, ref int code, ref string message);
        
        #region Revisión por parte del inspector
        bool TransportistaCancelar(int IdOrdenCarga, ref int code, ref string message);
        #endregion

        #region reporte
        OrdenCargaBE CargoTransportista(int IdOrdenCarga, ref int code, ref string message);
        #endregion 

        bool EnvaseIniciar(int IdUsuarioTransportista, int IdOrdenCarga, ref int code, ref string message);
        bool OtrosIniciar(int IdUsuarioTransportista, int IdOrdenCarga, ref int code, ref string message);
        bool CambiosIniciar(int IdUsuarioTransportista, int IdOrdenCarga, ref int code, ref string message);
        bool NoPlanIniciar(int IdUsuarioTransportista, int IdOrdenCarga, ref int code, ref string message);
        bool TransportistaFinalizar(OrdenCargaBE objOrdenCargaBE, ref int code, ref string message);
        IEnumerable<OrdenCargaBE> ListarTodosPendientes(ref int code, ref string message);
        IEnumerable<OrdenCargaBE> ListarPendientes(string centro, ref int code, ref string message);
        OrdenCargaBE InspectorIniciar(int IdOrdenCarga, int IdUsuarioSupervisor, ref int code, ref string message);
        bool InspectorFinalizar(OrdenCargaBE objOrdenCargaBE, ref int code, ref string message);
        int? BuscarIdOrdenCarga(string NumeroGuia, ref int code, ref string message);
        bool DevolucionTotal(int IdCentroLogin, int IdUsuarioInspector, string NumeroGuia, ref int code, ref string message);
        bool Eliminar(int IdOrdenCarga, int IdUsuarioRegistro, ref int code, ref string message);
        IEnumerable<OrdenCargaBE.ReporteUtilizacion> ReporteUtilizacion(int IdCentro, DateTime FechaInicio, DateTime FechaFinal, ref int code, ref string message);
        IEnumerable<OrdenCargaBE.ReporteDevolucion> ReporteDevolucion(int IdCentro, string FechaInicio, string FechaFinal, ref int code, ref string message);
    }
}
