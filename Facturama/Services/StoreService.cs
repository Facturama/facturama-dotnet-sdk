using System.Collections.Generic;
using Facturama.Models;
using Facturama.Models.Response.Catalogs;

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
            var response= this.HttpClient.Get<List<Product>>($"{UriResource}/products/{productType}");
            return response.ToArray();
        }        

    }
}
