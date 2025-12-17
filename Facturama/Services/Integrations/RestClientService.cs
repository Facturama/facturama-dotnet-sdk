using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Facturama.Services.Integrations
{
    public class RestClientService : IHttpClient
    {
        private readonly RestClient HttpClient;
        public RestClientService(RestClient HttpClient) {
            this.HttpClient = HttpClient;

        }
        [Obsolete("This method will be removed soon in favour of the proper async call")]
        public virtual RestRequestAsyncHandle ExecuteAsync(
            IRestRequest request,
            TaskCompletionSource<IRestResponse> taskCompletionSource)
        {
            return HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));
        }

        [Obsolete("This method will be removed soon in favour of the proper async call")]
        public virtual RestRequestAsyncHandle ExecuteAsync<TO>(
           IRestRequest request,
           TaskCompletionSource<IRestResponse<TO>> taskCompletionSource
        )
        {
            return HttpClient.ExecuteAsync<TO>(request, (restResponse, r) => taskCompletionSource.SetResult(restResponse));
        }
        public async Task<TO> PostAsync<TO, TI>(TI obj, string urlParams = "")
        {
            throw new NotImplementedException("method not valid for RestClient");
        }
        public async Task<TO> SendAsync<TO, TI>(TI obj, string token, HttpMethod httpMethod, string urlParams = "")
        {
            throw new NotImplementedException("method not valid for RestClient");
        }
    }
}
