using System;
using System.Collections.Generic;
using System.Text;
using Facturama.Models.Response.Catalogs;
using Facturama.Models.Response.Catalogs.Cfdi;
using Newtonsoft.Json;
using RestSharp;
using Facturama.Models.Charge;

namespace Facturama.Services
{
    public class ChargeService : HttpService<CatalogViewModel, CatalogViewModel>
    {

		public enum DocumentType
		{
			Receipt, Accuse
		}

		public ChargeService(RestClient httpClient) 
            : base(httpClient, "charges")
        {
            
        }

        public Charge Preview(Charge charge)
        {
            var request = new RestRequest(Method.POST) { Resource = $"{UriResource}/preview" };
            var response = Execute(request);
            return  JsonConvert.DeserializeObject<Charge>(response.Content);            
        }

		public Charge Create(Charge charge)
		{
			var request = new RestRequest(Method.POST) { Resource = $"{UriResource}" };
			var response = Execute(request);
			return JsonConvert.DeserializeObject<Charge>(response.Content);
		}

		public Charge[] List()
		{
			var request = new RestRequest(Method.GET) { Resource = $"{UriResource}" };
			var response = Execute(request);
			var modelView = JsonConvert.DeserializeObject<List<Charge>>(response.Content);
			return modelView.ToArray();			
		}

		public Charge Retrieve(string chargeId)
		{
			var request = new RestRequest(Method.GET) { Resource = $"{UriResource}/{chargeId}" };
			var response = Execute(request);
			return JsonConvert.DeserializeObject<Charge>(response.Content);			
		}

		public bool SendByMail(string chargeId, string email, DocumentType type = DocumentType.Receipt)
		{
			var documentType = (DocumentType.Receipt == type) ? "receipts" : "acuses";

			var request = new RestRequest(Method.POST) { Resource = $"{UriResource}/{documentType}?chargeId={chargeId}&email={email}" };
			var response = Execute(request);
			
			var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
			if (result != null && result.ContainsKey("success"))
			{
				return (bool)result["success"];
			}
			return false;

		}

	}
}
