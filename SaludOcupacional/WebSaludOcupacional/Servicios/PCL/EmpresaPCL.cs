using Salud.Ocupacional.BE;
using Salud.Ocupacional.BE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSaludOcupacional.DTO;
using WebSaludOcupacional.PCL.Helpers;

namespace WebSaludOcupacional.PCL
{
    public class EmpresaPCL
    {
        private static string url = Constants.URL + "Empresa/";

        /// <summary>
        /// Lista de los perfiles de usuario
        /// </summary>
        /// <returns>lista de perfiles</returns>
        public static async Task<ResultDTO<EmpresaBE.Response>> ListarEmpresas()
        {
            try
            {
                string get = url + "Listar";
                return await ResultPCL<EmpresaBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<EmpresaBE.Response>> BuscarEmpresa(int IdPerfil)
        {
            try
            {
                string get = url + string.Format("Buscar/{0}", IdPerfil);
                return await ResultPCL<EmpresaBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<ErrorBE>> RegistrarEmpresa(EmpresaBE.ResponseEmpresaBE oPerfil)
        {
            try
            {
                string post = url + "Registrar";
                return await ResultPCL<ErrorBE>.Post(post, oPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<ErrorBE>> ModificarEmpresa(EmpresaBE.ResponseEmpresaBE oPerfil)
        {
            try
            {
                string post = url + "Modificar";
                return await ResultPCL<ErrorBE>.Post(post, oPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

