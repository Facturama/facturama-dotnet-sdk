using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Facturama.Models.Request;
using Newtonsoft.Json.Converters;

namespace Facturama.Services.BaseService
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
        private readonly IHttpClient httpClient;
        private readonly string uriResource;

        protected HttpService(IHttpClient httpClient, string uri)
        {
            this.httpClient = httpClient;
            uriResource = uri;
        }

        protected TO1 Get<TO1>(string resourceId)
        {
            return httpClient.Get<TO1>($"{uriResource}{resourceId}");
        }

        protected TO1 Post<TO1, TI1>(TI1 obj, string urlParams = "")
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return httpClient.Post<TO1, TI1>($"{uriResource}{urlParams}", obj);
        }

        protected TO1 Put<TO1, TI1>(TI1 obj, string id)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return httpClient.Put<TO1, TI1>($"{uriResource}{id}", obj);
        }

        protected TO1 Delete<TO1>(string resourceId)
        {
            if (string.IsNullOrEmpty(resourceId))
                throw new ArgumentNullException(nameof(resourceId));
            var options = new HttpRequestOptions()
            {
                QueryParams = new Dictionary<string, string>() { { "id", resourceId } }
            };
            return httpClient.Delete<TO1>($"{uriResource}", options);
        }

        protected TO Put(TI obj, string id)
        {
            return Put<TO, TI>(obj, id);
        }

        protected TO Get(string resourceId)
        {
            return Get<TO>(resourceId);
        }

        protected TO Post(TI obj, string urlParams = "")
        {
            return Post<TO, TI>(obj, urlParams);
        }

        protected TO Delete(string resourceId)
        {
            return Delete<TO>(resourceId);
        }


        protected Task<TO1> GetAsync<TO1>(string resourceId)
        {
            return httpClient.GetAsync<TO1>($"{uriResource}{resourceId}");
        }

        protected Task<TO1> PostAsync<TO1, TI1>(TI1 obj, string urlParams = "")
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return httpClient.PostAsync<TO1, TI1>($"{uriResource}{urlParams}", obj);
        }

        protected Task<TO1> PutAsync<TO1, TI1>(TI1 obj, string id)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return httpClient.PutAsync<TO1, TI1>($"{uriResource}{id}", obj);
        }

        protected Task<TO1> DeleteAsync<TO1>(string resourceId)
        {
            if (string.IsNullOrEmpty(resourceId))
                throw new ArgumentNullException(nameof(resourceId));
            var options = new HttpRequestOptions()
            {
                QueryParams = new Dictionary<string, string>() { { "id", resourceId } }
            };
            return httpClient.DeleteAsync<TO1>($"{uriResource}", options);
        }

        protected Task<TO> PutAsync(TI obj, string id)
        {
            return PutAsync<TO, TI>(obj, id);
        }

        protected Task<TO> GetAsync(string resourceId)
        {
            return GetAsync<TO>(resourceId);
        }

        protected Task<TO> PostAsync(TI obj, string urlParams = "")
        {
            return PostAsync<TO, TI>(obj, urlParams);
        }

        protected Task<TO> DeleteAsync(string resourceId)
        {
            return DeleteAsync<TO>(resourceId);
        }
    }
}