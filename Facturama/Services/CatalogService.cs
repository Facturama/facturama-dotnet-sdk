using System.Collections.Generic;
using Facturama.Models.Response.Catalogs;
using Facturama.Models.Response.Catalogs.Cfdi;
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
            var url = $"{UriResource}/Units?keyword={keyword}";
            var modelView = HttpClient.Get<List<ProductOrServiceCatalog>>(url);
            return modelView.ToArray();
        }

        public ProductOrServiceCatalog[] ProductsOrServices(string keyword)
        {
            var url = $"{UriResource}/ProductsOrServices?keyword={keyword}";
            var modelView = HttpClient.Get<List<ProductOrServiceCatalog>>(url);
            return modelView.ToArray();
        }

        public PostalCodeCatalog[] PostalCodes(string keyword)
        {
            var url = $"{UriResource}/PostalCodes?keyword={keyword}";
            var modelView = HttpClient.Get<List<PostalCodeCatalog>>(url);
            return modelView.ToArray();
        }

        public CatalogViewModel[] NameIds
        {
            get
            {
                var url = $"{UriResource}/NameIds";
                var modelView = HttpClient.Get<List<CatalogViewModel>>(url);
                return modelView.ToArray();
            }
        }

        public CurrencyCatalog[] Currencies
        {
            get
            {
                var url = $"{UriResource}/Currencies";
                var modelView = HttpClient.Get<List<CurrencyCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public CatalogViewModel[] PaymentForms
        {
            get
            {
                var url = $"{UriResource}/paymentforms";
                var modelView = HttpClient.Get<List<CatalogViewModel>>(url);
                return modelView.ToArray();
            }
        }

        public CatalogViewModel[] PaymentMethods
        {
            get
            {
                var url = $"{UriResource}/paymentmethods";
                var modelView = HttpClient.Get<List<CatalogViewModel>>(url);
                return modelView.ToArray();
            }
        }

        public UseCfdiCatalog[] CfdiUses(string rfc)
        {
            var url = $"{UriResource}/CfdiUses?keyword={rfc}";
            var modelView = HttpClient.Get<List<UseCfdiCatalog>>(url);
            return modelView.ToArray();
        }

        public CatalogViewModel[] FiscalRegimens
        {
            get
            {
                var url = $"{UriResource}/FiscalRegimens";
                var modelView = HttpClient.Get<List<CatalogViewModel>>(url);
                return modelView.ToArray();
            }
        }

        public CfdiTypesCatalog[] CfdiTypes
        {
            get
            {
                var url = $"{UriResource}/CfdiTypes";
                var modelView = HttpClient.Get<List<CfdiTypesCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public RelationTypesCatalog[] RelationTypes
        {
            get
            {
                var url = $"{UriResource}/RelationTypes";
                var modelView = HttpClient.Get<List<RelationTypesCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public CountriesCatalog[] Countries
        {
            get
            {
                var url = $"{UriResource}/Countries";
                var modelView = HttpClient.Get<List<CountriesCatalog>>(url);
                return modelView.ToArray();
            }
        }

        // PayRoll Catalog
        public PayRollCatalog[] Banks
        {
            get
            {
                var url = $"{UriResource}/banks";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] OriginSources
        {
            get
            {
                var url = $"{UriResource}/originsources";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] paymentfrequencies
        {
            get
            {
                var url = $"{UriResource}/paymentfrequencies";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] ContractTypes
        {
            get
            {
                var url = $"{UriResource}/contracttypes";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] ExtraHours
        {
            get
            {
                var url = $"{UriResource}/extrahours";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] Incapacities
        {
            get
            {
                var url = $"{UriResource}/incapacities";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] TypesOfJourne
        {
            get
            {
                var url = $"{UriResource}/typesofjourne";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] OtherPayment
        {
            get
            {
                var url = $"{UriResource}/otherpayment";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] Perceptions
        {
            get
            {
                var url = $"{UriResource}/perceptions";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] RegimenTypes
        {
            get
            {
                var url = $"{UriResource}/regimentypes";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }

        public PayRollCatalog[] PositionRisks
        {
            get
            {
                var url = $"{UriResource}/positionrisks";
                var modelView = HttpClient.Get<List<PayRollCatalog>>(url);
                return modelView.ToArray();
            }
        }
    }

}

