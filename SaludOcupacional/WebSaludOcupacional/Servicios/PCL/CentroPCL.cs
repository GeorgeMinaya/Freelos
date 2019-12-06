using WebSaludOcupacional.DTO;
using WebSaludOcupacional.PCL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaludOcupacional.PCL
{
    public class CentroPCL
    {
        private static string url = Constants.URL + "Centro/"; 
        public static async Task<ResultDTO<List<CentroDTO>>> Listar()
        {
            string get = url + string.Format("List");
            return await ResultPCL<List<CentroDTO>>.GetResult(get);
        }
    }
}
