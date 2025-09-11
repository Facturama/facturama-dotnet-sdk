using Facturama.Models;
using Facturama.Services.BaseService;

namespace Facturama.Services
{
    public partial class CustomerService : CrudService<Customer, Customer>
    {
        public CustomerService(IHttpClient client) 
            : base(client, "client/")
        {
        }

        public Customer[] List(string keyword)
        {
            return this.Get<Customer[]>(resourceId: $"{"clients"}?{keyword}");
        }
    }

    
}
