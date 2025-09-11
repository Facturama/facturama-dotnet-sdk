using System.Collections.Generic;
using Facturama.Models;
using Facturama.Services.BaseService;

namespace Facturama.Services
{
    public class SerieService : HttpService<Serie, Serie>
    {
        public SerieService(IHttpClient httpClient) : base(httpClient, "serie/")
        {
        }

        public Serie Create(Serie modelo)
        {
            return Post(modelo, $"{modelo.IdBranchOffice}");
        }

        public List<Serie> List(string idBranchOffice)
        {
            return Get<List<Serie>>(idBranchOffice);
        }

        public Serie Retrieve(string idBranchOffice, string serieName)
        {
            return Get($"{idBranchOffice}/{serieName}");
        }

        public Serie Update(Serie model)
        {
            return Put(model, $"{model.IdBranchOffice}/{model.Name}");
        }

        public Serie Remove(string idBranchOffice, string serieName)
        {
            return Delete($"{idBranchOffice}/{serieName}");
        }
    }
}
