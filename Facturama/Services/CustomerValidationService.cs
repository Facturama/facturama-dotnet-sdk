using Facturama.Models.Response;
using Facturama.Services.BaseService;


namespace Facturama.Services
{
    public class CustomerValidationService : HttpService<Models.Request.CustumerValidate, CustumerValidate>
    {
        public CustomerValidationService(IHttpClient httpClient) : base(httpClient, "api/customers/validate")
        {
        }

        public CustumerValidate Validate(Models.Request.CustumerValidate model)
        {
            return Post(model, "");
        }

    }
}
