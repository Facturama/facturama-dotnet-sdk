using Facturama.Models.Response;
using Facturama.Models;
using RestSharp;


namespace Facturama.Services
{
    public class CustomersService : HttpService<Models.Request.CustumerValidate, Models.Response.CustumerValidate>
    {
        public CustomersService(RestClient httpClient) : base(httpClient, "api/customers/validate")
        {
        }

        public CustumerValidate Validate(Models.Request.CustumerValidate model)
        {
            return Post(model, "");
        }

    }
}
