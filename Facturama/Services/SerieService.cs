using System.Collections.Generic;
using Facturama.Models;
using RestSharp;

namespace Facturama.Services
{
    public class SerieService : HttpService<Serie, Serie>
    {
        public SerieService(RestClient httpClient) : base(httpClient, "serie")
        {
        }

        public List<Serie> List(string idBranchOffice)
        {
            return GetList(idBranchOffice);
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
