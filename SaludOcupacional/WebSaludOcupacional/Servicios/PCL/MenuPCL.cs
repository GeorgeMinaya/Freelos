using WebSaludOcupacional.DTO;
using WebSaludOcupacional.PCL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salud.Ocupacional.BE;
using Salud.Ocupacional.BE.Model;

namespace WebSaludOcupacional.PCL
{
    public class MenuPCL
    {
        private static string url = Constants.URL + "Menu/";

        public static async Task<ResultDTO<MenuBE.Response>> ListarMenus()
        {
            try
            {
                string get = url + "Listar";
                return await ResultPCL<MenuBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<ResultDTO<MenuTypeBE.Response>> ListarTypeMenus()
        {
            try
            {
                string get = url + "ListarType";
                return await ResultPCL<MenuTypeBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static async Task<ResultDTO<MenuBE.Response>> GetMenuById(int IdUsuario)
        {
            try
            {
                string get = url + string.Format("Buscar/{0}", IdUsuario.ToString());
                return await ResultPCL<MenuBE.Response>.GetResult(get);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<ResultDTO<ErrorBE>> ModificarMenu(MenuBE.ResponseMenuBE usuario)
        {
            try
            {
                string post = url + "Modificar";
                return await ResultPCL<ErrorBE>.Post(post, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<ResultDTO<ErrorBE>> RegistrarMenu(MenuBE.ResponseMenuBE usuario)
        {
            try
            {
                string post = url + "Registrar";
                return await ResultPCL<ErrorBE>.Post(post, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
