using Facturama.Services;
using Facturama.Services.BaseService;
using RestSharp.Authenticators;

namespace Facturama
{
    public class FacturamaApi
    {
        public FacturamaApi(string user, string password, string url)
            : this(new RestClientService(new RestSharp.RestClient(url)
            {
                Authenticator = new HttpBasicAuthenticator(user, password),
                UserAgent = user
            }))
        {
        }

        public FacturamaApi(string user, string password, bool isDevelopment = true)
			: this(user, password, isDevelopment 
				? "https://apisandbox.facturama.mx/" 
				: "https://api.facturama.mx/")
        {
            
		}

        public FacturamaApi(IHttpClient httpClient)
        {
            Clients = new CustomerService(httpClient);
            Cfdis = new CfdiService(httpClient);
            Retention = new RetentionService(httpClient);
            Products = new ProductService(httpClient);
            BranchOffices = new BranchOfficeService(httpClient);
            Profile = new ProfileService(httpClient);
            TaxEntities = new TaxEntityService(httpClient);
            Series = new SerieService(httpClient);
            Catalogs = new CatalogService(httpClient);
            Store = new StoreService(httpClient);
            Charges = new ChargeService(httpClient);
            Customer = new CustomerValidationService(httpClient);

        }

        public ProductService Products { get; }
        public CustomerService Clients { get; }
        public CfdiService Cfdis { get; }
        public BranchOfficeService BranchOffices { get; }
        public ProfileService Profile { get; }
        public TaxEntityService TaxEntities { get;  }
        public SerieService Series { get; }
        public CatalogService Catalogs { get; }
		public StoreService Store { get; }
		public ChargeService Charges { get; }
		public RetentionService Retention { get; }

        public CustomerValidationService Customer { get; }

    }


}
