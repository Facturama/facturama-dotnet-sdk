using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Facturama.Models.Exception;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;

namespace Facturama.Services
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }

    public abstract class HttpService<TI, TO> where TO : new()
    {
        protected readonly RestClient HttpClient;
        protected readonly string UriResource;

        protected IRestResponse Execute(IRestRequest request)
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<TO>>();
            HttpClient.ExecuteAsync<TO>(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new FacturamaException("No esta autorizado para realizar esta petición, verifique su usuario y contraseña y que su suscripción se encuentre activa");
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var exception = JsonConvert.DeserializeObject<ModelException>(response.Content);
                throw new FacturamaException(exception.Message, exception);
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception(response.Content, response.ErrorException);
            }
            if (response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                throw new Exception(response.Content);
            }
            return response;
        }

        protected HttpService(RestClient httpClient, string uri)
        {
            HttpClient = httpClient;
            UriResource = uri;
        }

        protected TO Get(string resourceId)
        {
            var uri = $"{UriResource}{resourceId}";
            var request = new RestRequest(uri, Method.GET);
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<TO>(response.Content);
            return modelView;
        }

        protected List<TO> GetList(string resourceId = null)
        {
            var request = new RestRequest(Method.GET) { Resource = UriResource };
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<List<TO>>(response.Content);
            return modelView;
        }

        protected TO Post(TI obj, string urlParams = "")
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var request = new RestRequest($"{UriResource}{urlParams}", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var json = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<TO>(response.Content);
            return modelView;
        }

        protected TO Put(TI obj, string id)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            
            var request = new RestRequest(Method.PUT) { Resource = $"{UriResource}{id}" };
            request.AddHeader("Content-Type", "application/json");
            var json = JsonConvert.SerializeObject(obj,  Formatting.None, new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = new List<JsonConverter> { new StringEnumConverter() }
            });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<TO>(response.Content);
            return modelView;
        }
        
        protected TO Delete(string resourceId)
        {
            if (String.IsNullOrEmpty(resourceId))
                throw new ArgumentNullException(nameof(resourceId));
            
            var request = new RestRequest(Method.DELETE) { Resource = UriResource };
            request.AddParameter("id", resourceId);
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<TO>(response.Content);
            return modelView;
        }
    }
}
