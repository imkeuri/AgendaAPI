using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AgendaFrontEnd.Services
{
    public class AgendaService<T> : IAgendaService<T>
    {


        private readonly HttpClient _httpClient;

        public AgendaService(HttpClient httpClient)
        {

            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");
            _httpClient = httpClient;
        }



        public async Task<T> DeleteAsync(string requestUri, int Id)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri + Id);



                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;

                if (responseStatusCode.ToString() == "OK")
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return await Task.FromResult(JsonConvert.DeserializeObject<T>(responseBody));
                }
                else
                    return default;


            }
            catch (Exception)
            {

                return default;
            }

        }

        public async Task<List<T>> GetAllAsync(string requestUri)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;

                if (responseStatusCode.ToString() == "OK")
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return await Task.FromResult(JsonConvert.DeserializeObject<List<T>>(responseBody));
                }
                else
                    return null;
            }
            catch (Exception)
            {

                return default;

            }

        }

        public async Task<T> GetByIdAsync(string requestUri, int Id)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + Id);



                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                return await Task.FromResult(JsonConvert.DeserializeObject<T>(responseBody));
            }
            catch (Exception)
            {

                return default;
            }

        }

        public async Task<T> PostAsync(string requestUri, T obj)
        {
            try
            {
                string serializedUser = JsonConvert.SerializeObject(obj);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(serializedUser)
                };

                requestMessage.Content.Headers.ContentType
                    = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                var returnedObj = JsonConvert.DeserializeObject<T>(responseBody);

                return await Task.FromResult(returnedObj);
            }
            catch (Exception)
            {

                return default;
            }

        }

        public async Task<T> PutAsync(string requestUri, int Id, T obj)
        {
            try
            {
                string serializedUser = JsonConvert.SerializeObject(obj);

                var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri + Id)
                {
                    Content = new StringContent(serializedUser)
                };

                requestMessage.Content.Headers.ContentType
                    = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                var returnedObj = JsonConvert.DeserializeObject<T>(responseBody);

                return await Task.FromResult(returnedObj);
            }
            catch (Exception)
            {

                return default;
            }

        }


        public async Task<T> GetAsync(string requestUri)
        {

            try
            {



                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;

                if (responseStatusCode.ToString() == "OK")
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return await Task.FromResult(JsonConvert.DeserializeObject<T>(responseBody));
                }
                else
                    return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

    }

}
