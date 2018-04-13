using Facturama.Services;
using RestSharp;
using RestSharp.Authenticators;

namespace Facturama
{
    public class FacturamaApi
    {
        public FacturamaApi(string user, string password, bool isDevelopment = true)
        {
            var url = isDevelopment ? "https://apisandbox.facturama.mx/" : "https://api.facturama.mx/";
            
            var httpClient = new RestClient(url)
            {
                Authenticator = new HttpBasicAuthenticator(user, password)
            };

            Clients = new ClientService(httpClient);
            Cfdis = new CfdiService(httpClient);
            Products = new ProductService(httpClient);
            BranchOffices = new BranchOfficeService(httpClient);
            Profile = new ProfileService(httpClient);
            TaxEntities = new TaxEntityService(httpClient);
            Series = new SerieService(httpClient);
            Catalogs = new CatalogService(httpClient);
        }

        public ProductService Products { get; }
        public ClientService Clients { get; }
        public CfdiService Cfdis { get; }
        public BranchOfficeService BranchOffices { get; }
        public ProfileService Profile { get; }
        public TaxEntityService TaxEntities { get;  }
        public SerieService Series { get; }
        public CatalogService Catalogs { get; }

    }


}
