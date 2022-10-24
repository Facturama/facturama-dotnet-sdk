using Facturama.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;



namespace Facturama.Services
{
    public partial class ClientService : CrudService<Client, Client>
    {
        public ClientService(RestClient client) 
            : base(client, "client/")
        {

        }
        public Client[] List2(string keyword)
        {
            var request = new RestRequest(Method.GET) { Resource = $"{"clients"}?{keyword}" };
            var response = Execute(request);
            var DesJson = JsonConvert.DeserializeObject<JsonResponse>(response.Content);
            var SerJson = JsonConvert.SerializeObject(DesJson.Data);
            var modelView = JsonConvert.DeserializeObject<List<Client>>(SerJson);
            return modelView.ToArray();

        }
    }

    
}
