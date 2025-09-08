using System.Collections.Generic;
using Facturama.Models.Charge;
using Facturama.Models.Response.Catalogs;

namespace Facturama.Services
{
    public class ChargeService : HttpService<CatalogViewModel, CatalogViewModel>
    {

		public enum DocumentType
		{
			Receipt, Accuse
		}

		public ChargeService(IHttpClient httpClient) 
            : base(httpClient, "charges")
        {
            
        }

        public Charge Preview(Charge charge)
        {
            return this.HttpClient.Post<Charge, object>($"{UriResource}/preview",null);         
        }

		public Charge Create(Charge charge)
		{
            return this.HttpClient.Post<Charge, object>($"{UriResource}", null);
		}

		public Charge[] List()
		{
            var response= this.HttpClient.Get<List<Charge>>($"{UriResource}");
			return response.ToArray();			
		}

		public Charge Retrieve(string chargeId)
		{
            return this.HttpClient.Get<Charge>($"{UriResource}/{chargeId}");	
		}

		public bool SendByMail(string chargeId, string email, DocumentType type = DocumentType.Receipt)
		{
			var documentType = (DocumentType.Receipt == type) ? "receipts" : "acuses";
            var response=this.HttpClient.Post<IDictionary<string, object>, object>($"{UriResource}/{documentType}?chargeId={chargeId}&email={email}",null);

			if (response != null && response.ContainsKey("success"))
			{
				return (bool)response["success"];
			}
			return false;

		}

	}
}
