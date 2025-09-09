using Facturama.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Facturama.Services
{
    public class ProductService : CrudService<Product, Product>
    {
        public ProductService(IHttpClient httpClient) : base(httpClient, "product/")
        {
            
        }
        public Product[] List2(string keyword)
        {
            var response= this.HttpClient.Get<JsonResponse>($"{"products"}?{keyword}");
            var SerJson = JsonConvert.SerializeObject(response.Data);
            var modelView = JsonConvert.DeserializeObject<List<Product>>(SerJson);
            return modelView.ToArray();

        }

    }

    
}
