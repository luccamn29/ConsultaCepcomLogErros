using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ConsultaViaCep.ConsumoAPI
{
    public static class ExecutarApi
    {
        public static readonly HttpClient _client = new HttpClient();

        public static  T ConsultaVerboGet<T>(string url)
        {
            var response  = _client.GetAsync(url).Result;

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Ocorreu um erro na api: " + response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }

        public static T ConsultaVerboPost<T>(string url, object objetoEntrada)
        {
            string json = JsonConvert.SerializeObject(objetoEntrada);
            StringContent content = new StringContent(json, Encoding.UTF8,"application/json");
            var response = _client.PostAsync(url, content).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {

                throw new Exception("Ocorreu um erro na api: " + response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
