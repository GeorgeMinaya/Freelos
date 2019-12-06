using WebSaludOcupacional.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebSaludOcupacional.PCL
{
    public class ResultPCL<T>
    {
        public static async Task<ResultDTO<T>> GetResult(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                var msg = await client.GetAsync(new Uri(url));

                if (msg.IsSuccessStatusCode)
                {
                    using (var stream = await msg.Content.ReadAsStreamAsync())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var str = await reader.ReadToEndAsync();
                            return JsonConvert.DeserializeObject<ResultDTO<T>>(str);
                        }
                    }
                }
                else
                    throw new HttpRequestException("Error en la conexión al servicio, favor de revisar su señal de Internet.");
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error en la conexión al servicio, favor de revisar su señal de Internet.");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }            
        }

        public static async Task<ResultDTO<T>> Post(string url, object objeto)
        {
            try
            {
                HttpClient client = new HttpClient();

                var serializedItemToCreate = JsonConvert.SerializeObject(objeto);
                var msg = await client.PostAsync(url, new StringContent(serializedItemToCreate, Encoding.UTF8, "application/json"));
                using (var stream = await msg.Content.ReadAsStreamAsync())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var str = await reader.ReadToEndAsync();
                        return JsonConvert.DeserializeObject<ResultDTO<T>>(str);
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error en la conexión al servicio, favor de revisar su señal de Internet.");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }        
    }
}
