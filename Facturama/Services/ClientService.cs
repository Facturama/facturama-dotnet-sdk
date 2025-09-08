using Facturama.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Facturama.Services
{
    public partial class ClientService : CrudService<Client, Client>
    {
        public ClientService(IHttpClient client) 
            : base(client, "client/")
        {

        }
        public Client[] List2(string keyword)
        {
            var response= this.HttpClient.Get<JsonResponse>($"{"clients"}?{keyword}");
            var SerJson = JsonConvert.SerializeObject(response.Data);
            var modelView = JsonConvert.DeserializeObject<List<Client>>(SerJson);
            return modelView.ToArray();

        }
    }

    
}
