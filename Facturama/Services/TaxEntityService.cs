using Facturama.Models.Request;
using Facturama.Services.BaseService;
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
            return Put<bool, Image>(img, $"UploadLogo");
        }

        public bool UploadCsd(Csd csd)
        {
            return Put<bool, Csd>(csd, $"UploadCsd");
        }
    }
}
