using Facturama.Models;
using Facturama.Services.BaseService;

namespace Facturama.Services
{
    public class ProductService : CrudService<Product, Product>
    {
        public ProductService(IHttpClient httpClient) : base(httpClient, "product/")
        {
            
        }
        public Product[] List(string keyword)
        {
            return this.Get<Product[]>(resourceId: $"{"products"}?{keyword}");
        }

    }

    
}
