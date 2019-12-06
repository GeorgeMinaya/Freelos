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
    public class MantenimientoPCL
    {
        private static string urlU = Constants.URL + "Usuario/";
        private static string urlD = Constants.URL + "Doctor/";

        #region Usuario
        public static async Task<ResultDTO<Response>> ListarUsuarios()
        {
            try
            {
                string get = urlU + "Listar";
                return await ResultPCL<Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<ResultDTO<Response>> GetUsuarioById(int IdUsuario)
        {
            try
            {
                string get = urlU + string.Format("Buscar/{0}", IdUsuario.ToString());
                return await ResultPCL<Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<ResultDTO<ErrorBE>> ModificarUsuario(ResponseLoginBE usuario)
        {
            try
            {
                string post = urlU + "Modificar";
                return await ResultPCL<ErrorBE>.Post(post, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        public static async Task<ResultDTO<ErrorBE>> RegistrarUsuario(ResponseLoginBE usuario)
        {
            try
            {
                string post = urlU + "Registrar";
                return await ResultPCL<ErrorBE>.Post(post, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Doctor
        public static async Task<ResultDTO<DoctorBE.Response>> ListarDoctores()
        {
            try
            {
                string get = urlD + "Listar";
                return await ResultPCL<DoctorBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<ResultDTO<DoctorBE.Response>> GetDoctorById(int IdUsuario)
        {
            try
            {
                string get = urlD + string.Format("Buscar/{0}", IdUsuario.ToString());
                return await ResultPCL<DoctorBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<ResultDTO<ErrorBE>> ModificarDoctor(DoctorBE.ResponseDoctoreBE usuario)
        {
            try
            {
                string post = urlD + "Modificar";
                return await ResultPCL<ErrorBE>.Post(post, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<ResultDTO<ErrorBE>> RegistrarDoctor(DoctorBE.ResponseDoctoreBE usuario)
        {
            try
            {
                string post = urlD + "Registrar";
                return await ResultPCL<ErrorBE>.Post(post, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
