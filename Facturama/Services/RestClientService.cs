using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Facturama.Services
{
    public class RestClientService : IHttpClient
    {
        private readonly RestSharp.RestClient HttpClient;
        public RestClientService(RestSharp.RestClient HttpClient) {
            this.HttpClient = HttpClient;

        }
        [Obsolete("This method will be removed soon in favour of the proper async call")]
        public virtual RestRequestAsyncHandle ExecuteAsync(
            IRestRequest request,
            TaskCompletionSource<IRestResponse> taskCompletionSource)
        {
            return this.HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));
        }

        [Obsolete("This method will be removed soon in favour of the proper async call")]
        public virtual RestRequestAsyncHandle ExecuteAsync<TO>(
           IRestRequest request,
           TaskCompletionSource<IRestResponse<TO>> taskCompletionSource
        )
        {
            return  HttpClient.ExecuteAsync<TO>(request, (restResponse,r)=> taskCompletionSource.SetResult(restResponse));
        }
        public async Task<TO> PostAsync<TO, TI>(TI obj, string urlParams = "") 
        { 
            throw new NotImplementedException("method not valid for RestClient") ;
        }
    }
}
