using System;
using Facturama.Models.Response;

namespace Facturama.Services
{
    public class CsdService : CrudService<Models.Request.Csd, Models.Response.Csd>
    {
        public CsdService(IHttpClient httpClient)
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
            return this.HttpClient.Delete<Csd>($"{UriResource}/{rfc}");
        }
    }
}
