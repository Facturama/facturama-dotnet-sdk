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
        public CatalogService(IHttpClient httpClient)
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
        public PostalCodeCatalog[] PostalCodes(string keyword)
        {
            var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/PostalCodes?keyword={keyword}" };
            var response = Execute(request);
            var modelView = JsonConvert.DeserializeObject<List<PostalCodeCatalog>>(response.Content);
            return modelView.ToArray();
        }

        public CatalogViewModel[] NameIds
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/NameIds" };
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

        public CfdiTypesCatalog[] CfdiTypes
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/CfdiTypes" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<CfdiTypesCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }
        public RelationTypesCatalog[] RelationTypes
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/RelationTypes" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<RelationTypesCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }

        public CountriesCatalog[] Countries
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/Countries" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<CountriesCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }
        //PayRoll Catalog
        public PayRollCatalog[] Banks
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/banks" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }
        public PayRollCatalog[] OriginSources
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/originsources" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }
        public PayRollCatalog[] paymentfrequencies
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/paymentfrequencies" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }
        public PayRollCatalog[] ContractTypes
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/contracttypes" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();

            }
        }

        public PayRollCatalog[] ExtraHours
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/extrahours" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] Incapacities
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/incapacities" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] TypesOfJourne
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/typesofjourne" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }
        public PayRollCatalog[] OtherPayment
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/otherpayment" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] Perceptions
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/perceptions" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }
        public PayRollCatalog[] RegimenTypes
        {
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/regimentypes" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }
        public PayRollCatalog[] PositionRisks
        { 
            get
            {
                var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/positionrisks" };
                var response = Execute(request);
                var modelView = JsonConvert.DeserializeObject<List<PayRollCatalog>>(response.Content);
                return modelView.ToArray();
            }
        }
        

    }

}

