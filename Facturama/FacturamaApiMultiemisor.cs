using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturama.Services;
using RestSharp;
using RestSharp.Authenticators;

namespace Facturama
{
    public class FacturamaApiMultiemisor
    {
        public FacturamaApiMultiemisor(string user, string password, bool isDevelopment = true)
        {
            var url = isDevelopment ? "https://apisandbox.facturama.mx/" : "https://api.facturama.mx/";

            var httpClient = new RestClient(url)
            {
                Authenticator = new HttpBasicAuthenticator(user, password)
            };
            Catalogs = new CatalogService(httpClient);
            Csds = new CsdService(httpClient);
            Cfdis = new CfdiLiteService(httpClient);
        }


        public CatalogService Catalogs { get; }
        public CsdService Csds { get; }
        public CfdiLiteService Cfdis { get; }

    }
}
