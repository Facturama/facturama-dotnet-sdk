using Facturama.Models.Response.Catalogs;
using Facturama.Models.Response.Catalogs.Cfdi;
using Facturama.Services.BaseService;

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
            return this.Get<ProductOrServiceCatalog[]>(resourceId: $"{"units"}?{keyword}");
        }

        public ProductOrServiceCatalog[] ProductsOrServices(string keyword)
        {
            return this.Get<ProductOrServiceCatalog[]>(resourceId: $"{"productsorservices"}?{keyword}");
        }

        public PostalCodeCatalog[] PostalCodes(string keyword)
        {
            return this.Get<PostalCodeCatalog[]>(resourceId: $"{"postalcodes"}?{keyword}");
        }

        public CatalogViewModel[] NameIds
        {
            get
            {
                return this.Get<CatalogViewModel[]>(resourceId: $"{"nameids"}");
            }
        }

        public CurrencyCatalog[] Currencies
        {
            get
            {
                return this.Get<CurrencyCatalog[]>(resourceId: $"{"currencies"}");
            }
        }

        public CatalogViewModel[] PaymentForms
        {
            get
            {
                return this.Get<CatalogViewModel[]>(resourceId: $"{"paymentforms"}");
            }
        }

        public CatalogViewModel[] PaymentMethods
        {
            get
            {
                return this.Get<CatalogViewModel[]>(resourceId: $"{"paymentmethods"}");
            }
        }

        public UseCfdiCatalog[] CfdiUses(string rfc)
        {
            return this.Get<UseCfdiCatalog[]>(resourceId: $"{"cfdiuses"}?rfc={rfc}");
        }

        public CatalogViewModel[] FiscalRegimens
        {
            get
            {
                return this.Get<CatalogViewModel[]>(resourceId: $"{"fiscalregimens"}");
            }
        }

        public CfdiTypesCatalog[] CfdiTypes
        {
            get
            {
                return this.Get<CfdiTypesCatalog[]>(resourceId: $"{"cfditypes"}");
            }
        }
        
        public RelationTypesCatalog[] RelationTypes
        {
            get
            {
                return this.Get<RelationTypesCatalog[]>(resourceId: $"{"relationtypes"}");
            }
        }

        public CountriesCatalog[] Countries
        {
            get
            {
                return this.Get<CountriesCatalog[]>(resourceId: $"{"countries"}");
            }
        }

        public PayRollCatalog[] Banks
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"banks"}");
            }
        }

        public PayRollCatalog[] OriginSources
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"originsources"}");
            }
        }

        public PayRollCatalog[] PaymentFrequencies
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"paymentfrequencies"}");
            }
        }

        public PayRollCatalog[] ContractTypes
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"contracttypes"}");

            }
        }

        public PayRollCatalog[] ExtraHours
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"extrahours"}");
            }
        }

        public PayRollCatalog[] Incapacities
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"incapacities"}");
            }
        }
        
        public PayRollCatalog[] TypesOfJourne
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"typesofjourne"}");
            }
        }
        
        public PayRollCatalog[] OtherPayment
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"otherpayment"}");
            }
        }

        public PayRollCatalog[] Perceptions
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"perceptions"}");
            }
        }
        
        public PayRollCatalog[] RegimenTypes
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"regimentypes"}");
            }
        }
        
        public PayRollCatalog[] PositionRisks
        {
            get
            {
                return this.Get<PayRollCatalog[]>(resourceId: $"{"positionrisks"}");
            }
        }
    }
}

