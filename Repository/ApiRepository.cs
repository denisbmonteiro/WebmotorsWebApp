using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebmotorsWebApp.Models;

namespace WebmotorsWebApp.Repository
{
    public class ApiRepository
    {
        private readonly HttpClient _client;

        public ApiRepository()
        {
            _client = new HttpClient();

            _client.BaseAddress = new Uri("https://desafioonline.webmotors.com.br/api/OnlineChallenge/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<List<Marca>> GetMarcasAsync()
        {
            var response = await _client.GetAsync("Make");
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Marca>>(responseString);
            }

            return null;
        }

        public async Task<List<Modelo>> GetModelosAsync(string idMarca)
        {
            var response = await _client.GetAsync("Model?MakeID=" + idMarca);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Modelo>>(responseString);
            }

            return null;
        }

        public async Task<List<Versao>> GetVersoesAsync(string idModelo)
        {
            var response = await _client.GetAsync("Version?ModelID=" + idModelo);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Versao>>(responseString);
            }

            return null;
        }
    }
}