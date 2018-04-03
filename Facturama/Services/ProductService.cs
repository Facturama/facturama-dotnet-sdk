using Facturama.Models;
using RestSharp;

namespace Facturama.Services
{
    public class ProductService : CrudService<Product, Product>
    {
        public ProductService(RestClient httpClient) : base(httpClient, "product/")
        {
            
        }

    }

    
}
