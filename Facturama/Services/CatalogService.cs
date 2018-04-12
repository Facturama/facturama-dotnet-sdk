using System;
using System.Collections.Generic;
using System.Text;
using Facturama.Models.Response.Catalogs;
using Facturama.Models.Response.Catalogs.Cfdi;
using Newtonsoft.Json;
using RestSharp;

namespace Facturama.Services
{
    public class CatalogService : HttpService<CatalogViewModel, CatalogViewModel>
    {
        public CatalogService(RestClient httpClient) 
            : base(httpClient, "catalogs")
        {
            
        }

        public ProductOrServiceCatalog[] Units(string keyword)
        {
            var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/Units?keyword={keyword}" };
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<List<ProductOrServiceCatalog>>(response.Content);
            return modelView.ToArray();
        }

        public ProductOrServiceCatalog[] ProductsOrServices(string keyword)
        {
            var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/ProductsOrServices?keyword={keyword}" };
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<List<ProductOrServiceCatalog>>(response.Content);
            return modelView.ToArray();
        }

        public CatalogViewModel[] NameIds
        {
            get
            {
                var request = new RestRequest(Method.GET) {Resource = $"{UriResource}/NameIds"};
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<CatalogViewModel>>(response.Content);
                return modelView.ToArray();
            }
        }

        public CurrencyCatalog[] Currencies
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/Currencies" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<CurrencyCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }

        public CatalogViewModel[] PaymentForms
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/paymentforms" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<CatalogViewModel>>(response.Content);
                return modelView.ToArray();
            }
        }

        public CatalogViewModel[] PaymentMethods
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/paymentmethods" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<CatalogViewModel>>(response.Content);
                return modelView.ToArray();
            }
        }

        public UseCfdiCatalog[] CfdiUses(string rfc)
        {
            var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/CfdiUses?keyword={rfc}" };
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<List<UseCfdiCatalog>>(response.Content);
            return modelView.ToArray();
        }

        public CatalogViewModel[] FiscalRegimens
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/FiscalRegimens" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<CatalogViewModel>>(response.Content);
                return modelView.ToArray();
            }
        }

    }
}
