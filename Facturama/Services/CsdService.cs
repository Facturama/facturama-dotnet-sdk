using System;
using Facturama.Models.Response;
using Newtonsoft.Json;
using RestSharp;

namespace Facturama.Services
{
    public class CsdService : CrudService<Models.Request.Csd, Models.Response.Csd>
    {
        public CsdService(RestClient httpClient)
            : base(httpClient, "api-lite/csds/")
        {

        }

        public Csd Update(Models.Request.Csd model)
        {
            return base.Update(model, model.Rfc);
        }

        public override Csd Remove(string rfc)
        {
            if (String.IsNullOrEmpty(rfc))
                throw new ArgumentNullException(nameof(rfc));

            var request = new RestRequest(Method.DELETE) { Resource = $"{UriResource}/{rfc}" };
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<Csd>(response.Content);
            return modelView;
        }
    }
}
