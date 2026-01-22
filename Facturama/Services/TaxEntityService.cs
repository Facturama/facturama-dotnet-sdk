using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Facturama.Models;
using Facturama.Models.Request;
using Facturama.Models.Response;
using Newtonsoft.Json;
using RestSharp;
using TaxEntity = Facturama.Models.Response.TaxEntity;

namespace Facturama.Services
{
    public class TaxEntityService : HttpService<Models.Request.TaxEntity, Models.Response.TaxEntity>
    {
        public TaxEntityService(IHttpClient httpClient) : base(httpClient, "taxentity/")
        {
        }

        public TaxEntity Retrieve()
        {
            return Get("");
        }

        public TaxEntity Update(Models.Request.TaxEntity model)
        {
            return Put(model, "");
        }

        public bool UploadImage(Image img)
        {
            var request = new RestRequest($"{UriResource}UploadLogo", Method.PUT);
            request.AddHeader("Content-Type", "application/json");

            var json = JsonConvert.SerializeObject(img,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, taskCompletionSource);

            var response = taskCompletionSource.Task.Result;

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception(response.ErrorMessage);

            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }

        public UploadCsdResponse UploadCsd(Models.Request.Csd csd)
        {
            var request = new RestRequest($"{UriResource}UploadCsd", Method.PUT);
            request.AddHeader("Content-Type", "application/json");

            var json = JsonConvert.SerializeObject(csd,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, taskCompletionSource);

            var response = taskCompletionSource.Task.Result;

            if (response.ErrorException != null)
                throw new Exception("Request execution failed.", response.ErrorException);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception(response.ErrorMessage ?? response.Content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new Exception($"BadRequest (400): {response.Content}");

            if (!response.IsSuccessful)
                throw new Exception($"Request failed with status {response.StatusCode}: {response.Content} - {response.StatusDescription}");

            if (string.IsNullOrEmpty(response.Content))
                throw new Exception($"Failed to deserialize response. Content: {response.Content}");

            return JsonConvert.DeserializeObject<UploadCsdResponse>(response.Content);
        }
    }
}
