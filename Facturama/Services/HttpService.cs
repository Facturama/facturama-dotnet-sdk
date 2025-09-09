using System;
using System.Collections.Generic;
using Facturama.Models.Request;
using Newtonsoft.Json.Converters;

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
        protected readonly IHttpClient HttpClient;
        protected readonly string UriResource;

        protected HttpService(IHttpClient httpClient, string uri)
        {
            HttpClient = httpClient;
            UriResource = uri;
        }

        protected TO Get(string resourceId)
        {
            return this.HttpClient.Get<TO>($"{UriResource}{resourceId}");
        }

        protected List<TO> GetList(string resourceId = null)
        {
            return this.HttpClient.Get<List<TO>>($"{UriResource}{resourceId}");
        }

        protected TO Post(TI obj, string urlParams = "")
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return this.HttpClient.Post<TO,TI>($"{UriResource}{urlParams}", obj);
        }

        protected TO Put(TI obj, string id)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return this.HttpClient.Put<TO, TI>($"{UriResource}{id}", obj);
        }
        
        protected TO Delete(string resourceId)
        {
            if (String.IsNullOrEmpty(resourceId))
                throw new ArgumentNullException(nameof(resourceId));
            var options=new HttpRequestOptions()
            {
                QueryParams= new Dictionary<string, string>() { { "id", resourceId } }
            };
            return this.HttpClient.Delete<TO>($"{UriResource}", options);
        }
    }
}
