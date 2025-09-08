using Facturama.Models.Request;
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
           return this.HttpClient.Get<TaxEntity>($"");
        }

        public TaxEntity Update(Models.Request.TaxEntity model)
        {
            return this.HttpClient.Put<TaxEntity, Models.Request.TaxEntity>($"",model);
        }

        public bool UploadImage(Image img)
        {
            return this.HttpClient.Put<bool, Image>($"{UriResource}UploadLogo", img);
        }

        public bool UploadCsd(Csd csd)
        {
            return this.HttpClient.Put<bool, Csd>($"{UriResource}UploadCsd", csd);
        }
    }
}
