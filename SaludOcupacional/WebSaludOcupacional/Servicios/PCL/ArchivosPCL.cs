
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
    public class ArchivosPCL
    {
        private static readonly string url = Constants.URL + "Archivo/";

        public static async Task<ResultDTO<ArchivoBE.ResponseArchivoBE>> DatosRegistrar()
        {
            try
            {
                string get = url + "DatosRegistrar";
                return await ResultPCL<ArchivoBE.ResponseArchivoBE>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista de los perfiles de usuario
        /// </summary>
        /// <returns>lista de perfiles</returns>
        public static async Task<ResultDTO<ArchivoBE.Response>> ListarArchivos()
        {
            try
            {
                string get = url + "Listar";
                return await ResultPCL<ArchivoBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<ArchivoBE.Response>> BuscarArchivo(int IdPerfil)
        {
            try
            {
                string get = url + string.Format("Buscar/{0}", IdPerfil);
                return await ResultPCL<ArchivoBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<int>> RegistrarArchivo(ArchivoBE.ResponseArchivoBE oPerfil)
        {
            try
            {
                string post = url + "RegistrarArchivo";
                return await ResultPCL<int>.Post(post, oPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ResultDTO<ErrorBE>> ModificarArchivo(ArchivoBE.ResponseArchivoBE oPerfil)
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

