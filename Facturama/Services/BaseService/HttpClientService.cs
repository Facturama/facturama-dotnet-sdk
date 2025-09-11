namespace Facturama.Services.BaseService
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Facturama.Models.Exception;
    using Facturama.Models.Request;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class HttpClientService : IHttpClient
    {
        private readonly HttpClient httpClient;
        public HttpClientService(HttpClient HttpClient)
        {
            httpClient = HttpClient;
        }
        private void HandleBadRequest(string responseContent)
        {
            try
            {
                var exception = JsonConvert.DeserializeObject<ModelException>(responseContent);
                throw new FacturamaException(exception?.Message ?? "Bad request", exception);
            }
            catch (JsonException)
            {
                throw new Exception($"Bad Request: {responseContent}");
            }
        }
        private void HandleErrorResponse(HttpResponseMessage response, string responseContent)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new FacturamaException("Unauthorized");

                case HttpStatusCode.BadRequest:
                    HandleBadRequest(responseContent);
                    break;

                case HttpStatusCode.InternalServerError:
                    throw new Exception($"Internal Server Error: {responseContent}");

                case HttpStatusCode.MethodNotAllowed:
                    throw new Exception($"Method Not Allowed: {responseContent}");

                default:
                    throw new Exception($"HTTP Error {(int)response.StatusCode}: {responseContent}");
            }
        }
        private async Task<TO> HandleResponseAsync<TO>(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new TimeoutException($"Request timeout: {response.ReasonPhrase}");
            }
            try
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    HandleErrorResponse(response, responseContent);
                }
                return JsonConvert.DeserializeObject<TO>(responseContent);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to deserialize response: {response.ReasonPhrase}", ex);
            }
        }
        private StringContent CreateRequest<TI>(TI data, HttpRequestOptions options)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            foreach (var header in options?.Headers ?? new Dictionary<string, string>())
            {
                content.Headers.Add(header.Key, header.Value);
            }
            return content;
        }
        public string GetBaseUrl()
        {
            return "";
        }
        public TO Post<TO, TI>(string url, TI data, HttpRequestOptions options = null)
        {

            return PostAsync<TO, TI>(url, data, options)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        }
        public async Task<TO> PostAsync<TO, TI>(string url, TI data, HttpRequestOptions options = null)
        {
            var content = CreateRequest(data, options);
            try
            {
                var response = await httpClient.PostAsync($"{httpClient.BaseAddress.AbsoluteUri}{url}", content).ConfigureAwait(false);
                return await HandleResponseAsync<TO>(response).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TO Get<TO>(string url, HttpRequestOptions options = null)
        {
            return GetAsync<TO>(url, options)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        }
        public async Task<TO> GetAsync<TO>(string url, HttpRequestOptions options = null)
        {
            try
            {
                var response = await httpClient.GetAsync($"{httpClient.BaseAddress.AbsoluteUri}{url}").ConfigureAwait(false);
                return await HandleResponseAsync<TO>(response).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TO Put<TO, TI>(string url, TI data, HttpRequestOptions options = null)
        {
            return PutAsync<TO, TI>(url, data, options)
                 .ConfigureAwait(false)
                 .GetAwaiter()
                 .GetResult();
        }
        public async Task<TO> PutAsync<TO, TI>(string url, TI data, HttpRequestOptions options = null)
        {
            var content = CreateRequest(data, options);
            try
            {
                var response = await httpClient.PutAsync($"{httpClient.BaseAddress.AbsoluteUri}{url}", content).ConfigureAwait(false);
                return await HandleResponseAsync<TO>(response).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TO Delete<TO>(string url, HttpRequestOptions options = null)
        {
            return DeleteAsync<TO>(url, options)
                 .ConfigureAwait(false)
                 .GetAwaiter()
                 .GetResult();
        }
        public async Task<TO> DeleteAsync<TO>(string url, HttpRequestOptions options = null)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{httpClient.BaseAddress.AbsoluteUri}{url}").ConfigureAwait(false);
                return await HandleResponseAsync<TO>(response).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}