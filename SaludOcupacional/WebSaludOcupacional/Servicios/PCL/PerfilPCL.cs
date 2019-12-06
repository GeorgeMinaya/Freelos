using Salud.Ocupacional.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSaludOcupacional.DTO;
using WebSaludOcupacional.PCL.Helpers;

namespace WebSaludOcupacional.PCL
{
    public class PerfilPCL
    {
        private static string url = Constants.URL + "Perfil/";

        /// <summary>
        /// Lista de los perfiles de usuario
        /// </summary>
        /// <returns>lista de perfiles</returns>
        public static async Task<ResultDTO<List<PerfilBE>>> ListarPerfiles()
        {
            try
            {
                string get = url + "Listar";
                return await ResultPCL<List<PerfilBE>>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<PerfilDTO>> RegistrarPerfil(PerfilDTO oPerfil)
        {
            try
            {
                string post = url + "Registrar";
                return await ResultPCL<PerfilDTO>.Post(post, oPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<PerfilDTO>> BuscarPerfil(int IdPerfil)
        {
            try
            {
                string get = url + string.Format("Buscar/{0}", IdPerfil);
                return await ResultPCL<PerfilDTO>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
