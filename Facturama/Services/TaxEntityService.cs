using System.Net;
using Facturama.Models;
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
            return Put(model,"");
        }

        public bool UploadImage(Image img)
        {
            var response=this.HttpClient.Put<ImageResponse, Image>($"{UriResource}UploadLogo", img);
            if (response.Message == "Se cargo exitosamente el logo")
            {
                return true;
            }
            return false;
        }

        public bool UploadCsd(Csd csd)
        {
            return this.HttpClient.Put<bool, Csd>($"{UriResource}UploadCsd", csd);
        }
    }
}
