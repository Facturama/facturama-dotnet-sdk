using Facturama.Models.Response;

namespace Facturama.Services
{
    public class CustomersService : HttpService<Models.Request.CustumerValidate, Models.Response.CustumerValidate>
    {
        public CustomersService(IHttpClient httpClient) : base(httpClient, "api/customers/validate")
        {
        }

        public CustumerValidate Validate(Models.Request.CustumerValidate model)
        {
            return Post(model, "");
        }

    }
}
