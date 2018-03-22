using Facturama.Models;
using RestSharp;

namespace Facturama.Services
{
    public partial class ClientService : CrudService<Client, Client>
    {
        public ClientService(RestClient client) 
            : base(client, "client/")
        {

        }
    }

    
}
