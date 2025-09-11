using Facturama.Services.BaseService;

namespace Facturama.Services
{
    public class CsdService : CrudService<Models.Request.Csd, Models.Response.Csd>
    {
        public CsdService(IHttpClient httpClient)
            : base(httpClient, "api-lite/csds/")
        {
        }
    }
}
