using System;
using System.Collections.Generic;
using System.Text;
using Facturama.Models;
using Facturama.Models.Response.Catalogs;
using Facturama.Models.Response.Catalogs.Cfdi;
using Newtonsoft.Json;
using RestSharp;

namespace Facturama.Services
{
    public class StoreService : HttpService<CatalogViewModel, CatalogViewModel>
    {
		public enum ProductType
		{
			Contabilidad, Api, Web
		}


		public StoreService(RestClient httpClient)
			: base(httpClient, "store")
		{

		}


        public Product[] Products(ProductType productType = ProductType.Web)
        {
            var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/products/{productType}" };
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<List<Product>>(response.Content);
            return modelView.ToArray();
        }        

    }
}
