using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Facturama.Models;
using Facturama.Models.Request;
using Newtonsoft.Json;
using RestSharp;
using TaxEntity = Facturama.Models.Response.TaxEntity;

namespace Facturama.Services
{
    public class TaxEntityService : HttpService<Models.Request.TaxEntity, Models.Response.TaxEntity>
    {
        public TaxEntityService(RestClient httpClient) : base(httpClient, "taxentity/")
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
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception(response.ErrorMessage);

            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }

        public bool UploadCsd(Csd csd)
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
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception(response.ErrorMessage);

            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;

            return false;
        }
    }
}
