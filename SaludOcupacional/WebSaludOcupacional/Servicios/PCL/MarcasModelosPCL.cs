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
    public class MarcasModelosPCL
    {
        private static string url = Constants.URL + "MarcaModelo/";

        /// <summary>
        /// Lista de los perfiles de usuario
        /// </summary>
        /// <returns>lista de perfiles</returns>
        public static async Task<ResultDTO<MarcaModeloBE.Response>> ListarMarcasModelos()
        {
            try
            {
                string get = url + "Listar";
                return await ResultPCL<MarcaModeloBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<MarcaModeloBE.Response>> BuscarMarcasModelos(int IdPerfil)
        {
            try
            {
                string get = url + string.Format("Buscar/{0}", IdPerfil);
                return await ResultPCL<MarcaModeloBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<ErrorBE>> RegistrarMarcasModelos(MarcaModeloBE.ResponseMarcaModeloBE oPerfil)
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

        public static async Task<ResultDTO<ErrorBE>> ModificarMarcasModelos(MarcaModeloBE.ResponseMarcaModeloBE oPerfil)
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

