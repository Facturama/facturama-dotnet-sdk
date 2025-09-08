using System.Collections.Generic;
using Facturama.Models;

namespace Facturama.Services
{
    public class SerieService : HttpService<Serie, Serie>
    {
        public SerieService(IHttpClient httpClient) : base(httpClient, "serie/")
        {
        }

        //---------------------------- metodo create add by kiva -----------------------// no sirve u.u
        public Serie Create(Serie modelo)
        {
            return this.HttpClient.Post<Serie, Serie>($"{modelo.IdBranchOffice}",modelo);
        }

        public List<Serie> List(string idBranchOffice)
        {
            return this.HttpClient.Get<List<Serie>>($"{idBranchOffice}");
        }

        public Serie Retrieve(string idBranchOffice, string serieName)
        {
            return this.HttpClient.Get<Serie>($"{idBranchOffice}/{serieName}");
        }

        public Serie Update(Serie model)
        {
            return this.HttpClient.Put<Serie,Serie>($"{model.IdBranchOffice}/{model.Name}",model);
        }

        public Serie Remove(string idBranchOffice, string serieName)
        {
            return this.HttpClient.Delete<Serie>($"{idBranchOffice}/{serieName}");
        }



    }
}
