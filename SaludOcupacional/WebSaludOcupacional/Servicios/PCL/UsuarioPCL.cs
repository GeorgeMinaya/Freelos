using WebSaludOcupacional.DTO;
using WebSaludOcupacional.PCL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salud.Ocupacional.BE;

namespace WebSaludOcupacional.PCL
{
    public class UsuarioPCL
    {
        private static string url = Constants.URL + "Usuario/";

        public static async Task<ResultDTO<Response>> Login(RequestLoginBE requestLoginBE)
        {
            try
            {
                string post = url + "AutenticarUsuario";
                var result = await ResultPCL<Response>.Post(post, requestLoginBE);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<ResultDTO<int>> NotificaLogin(RequestLoginBE requestLoginBE)
        {
            try
            {
                string post = url + "NotificaUsuario";
                var result = await ResultPCL<int>.Post(post, requestLoginBE);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
