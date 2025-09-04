using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Facturama.Models.Exception;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;


namespace Facturama.Services
{
    public class HttpClientService : IHttpClient
    {
        private readonly System.Net.Http.HttpClient httpClient;
        public HttpClientService(System.Net.Http.HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        [Obsolete("This method will be removed soon in favour of the proper async call")]
        public virtual RestRequestAsyncHandle ExecuteAsync(
           IRestRequest request,
           TaskCompletionSource<IRestResponse> taskCompletionSource)
        {
            throw new NotImplementedException("method not valid for HttpClient");
        }

        [Obsolete("This method will be removed soon in favour of the proper async call")]
        public virtual RestRequestAsyncHandle ExecuteAsync<TO>(
           IRestRequest request,
           TaskCompletionSource<IRestResponse<TO>> taskCompletionSource
        )
        {
            throw new NotImplementedException("method not valid for HttpClient");
        }
        public async Task<TO> PostAsync<TO, TI>(TI obj, string urlParams = "")
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            });
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await this.httpClient.PostAsync($"{this.httpClient.BaseAddress.AbsoluteUri}{urlParams}", content);
                if (response.StatusCode == HttpStatusCode.RequestTimeout || response.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    throw new TimeoutException($"La petición HTTP excedió el tiempo de espera configurado {response.StatusCode}.");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    try
                    {
                        var exception = JsonConvert.DeserializeObject<ModelException>(responseContent);
                        throw new FacturamaException(exception?.Message ?? "Bad request", exception);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Bad request. Content: {response?.Content}", ex);
                    }
                }
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TO>(responseContent);
            }
            catch (TaskCanceledException tex) when (!tex.CancellationToken.IsCancellationRequested)
            {
                throw new TimeoutException("La petición HTTP excedió el tiempo de espera configurado.", tex);
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Error HTTP en POST: {httpEx.Message}", httpEx);
            }
            catch (JsonException jsonEx)
            {
                throw new Exception($"Error al deserializar la respuesta: {jsonEx.Message}", jsonEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado en POST: {ex.Message}", ex);
            }
        }
    }
}
