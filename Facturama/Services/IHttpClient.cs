using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Facturama.Services
{
    public interface IHttpClient
    {
        RestRequestAsyncHandle ExecuteAsync(
            IRestRequest request,TaskCompletionSource<IRestResponse> taskCompletionSource);
        RestRequestAsyncHandle ExecuteAsync<TO>(
            IRestRequest request, TaskCompletionSource<IRestResponse<TO>> taskCompletionSource);
        Task<TO> PostAsync<TO, TI>(TI obj, string urlParams = "");
    }
}
