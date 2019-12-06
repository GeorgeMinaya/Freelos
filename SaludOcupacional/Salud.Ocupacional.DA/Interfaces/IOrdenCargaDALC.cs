using ACL.MegaCentro.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace ACL.MegaCentro.DALC.Interfaces
{
    public interface IOrdenCargaDALC
    {
        bool ValidarCentro(int IdCentro, string CentroGuia);

        #region Interface 27
        void BorealEnviar(OrdenCargaBE objOrdenCargaBE);
        #endregion

        IEnumerable<OrdenCargaBE> ListarFinalizados(int IdCentro, string FechaInicio, string FechaFinal);
        OrdenCargaBE BuscarDiferencias(int IdOrdenCarga);
        object ConsultarPI(string CodigoCentro, string NumeroGuia);
        OrdenCargaBE CargoTransportista(int IdOrdenCarga);

        OrdenCargaBE ConsultarGuia_PI(string CodigoCentro, string NumeroGuia);
        OrdenCargaBE ConsultarGuia_SQL(int IdOrdenCarga);
        OrdenCargaBE Insertar(OrdenCargaBE objOrdenCargaBE);
        void TransportistaActualizar(OrdenCargaBE objOrdenCargaBE);
        int TransportistaIniciar(int IdUsuarioTransportista, string CodigoCentro, string NumeroGuia);
        int BuscarPorNumeroGuia(string NumeroGuia);
        bool TransportistaFinalizar(int IdUsuarioTransportista, int IdOrdenCarga);

        bool LiquidoIniciar(int IdUsuarioTransportista, int IdOrdenCarga);
        bool EnvaseIniciar(int IdUsuarioTransportista, int IdOrdenCarga);
        bool OtrosIniciar(int IdUsuarioTransportista, int IdOrdenCarga);
        bool CambioIniciar(int IdUsuarioTransportista, int IdOrdenCarga);
        bool NoPlanIniciar(int IdUsuarioTransportista, int IdOrdenCarga);

        IEnumerable<OrdenCargaBE> ListarTodosPendientes();
        IEnumerable<OrdenCargaBE> ListarPendientes(string centro);
        bool InspectorIniciar(int IdUsuarioSupervisor, int IdOrdenCarga);
        void InspectorActualizar(OrdenCargaBE objOrdenCargaBE);
        bool InspectorFinalizar(int IdUsuarioSupervisor, int IdOrdenCarga);
        bool ComercialEnviar(OrdenCargaBE objOrdenCargaBE);
        bool TransportistaCancelar(int IdOrdenCarga);
        int BuscarIdOrdenCarga(string CodigoCentro, string NumeroGuia);
        bool Eliminar(int IdOrdenCarga, int IdUsuarioRegistro);

        OrdenCargaBE ConsultarGuia_AS400(string NumeroGuia);
        IEnumerable<OrdenCargaBE.ReporteUtilizacion> ReporteUtilizacion(int IdCentro, DateTime FechaInicio, DateTime FechaFinal);
        IEnumerable<OrdenCargaBE.ReporteDevolucion> ReporteDevolucion(int IdCentro, string FechaInicio, string FechaFinal);
    }
}
