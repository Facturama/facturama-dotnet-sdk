using Facturama.Models;
using Facturama.Models.Response.Catalogs;
using Facturama.Services.BaseService;
namespace Facturama.Services
{
    public class StoreService : HttpService<CatalogViewModel, CatalogViewModel>
    {
		public enum ProductType
		{
			Contabilidad, Api, Web
		}


		public StoreService(IHttpClient httpClient)
			: base(httpClient, "store")
		{

		}


        public Product[] Products(ProductType productType = ProductType.Web)
        {
            return base.Get<Product[]>(resourceId: $"products/{productType}");
        }        

    }
}
