using Facturama.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace Facturama.Services
{
    public class ProductService : CrudService<Product, Product>
    {
        public ProductService(RestClient httpClient) : base(httpClient, "product/")
        {
            
        }
        public Product[] List2(string keyword)
        {
            var request = new RestRequest(Method.GET) { Resource = $"{"products"}?{keyword}" };
            var response = Execute(request);
            var DesJson = JsonConvert.DeserializeObject<JsonResponse>(response.Content);
            var SerJson = JsonConvert.SerializeObject(DesJson.Data);
            var modelView = JsonConvert.DeserializeObject<List<Product>>(SerJson);
            return modelView.ToArray();

        }

    }

    
}
