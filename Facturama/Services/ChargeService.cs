using System.Collections.Generic;
using Facturama.Models.Response.Catalogs;
using Facturama.Models.Charge;
using Facturama.Services.BaseService;

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
			return base.Post<Charge, Charge>(charge, "/preview");
        }

		public Charge Create(Charge charge)
		{
            return base.Post<Charge, Charge>(charge);
        }

		public Charge[] List()
		{
			return base.Get<Charge[]>("");
        }

		public Charge Retrieve(string chargeId)
		{
			return base.Get<Charge>($"/{chargeId}");
        }

		public bool SendByMail(string chargeId, string email, DocumentType type = DocumentType.Receipt)
		{
			var documentType = (DocumentType.Receipt == type) ? "receipts" : "acuses";
			var result = base.Post<IDictionary<string, object>, object>(null, $"/{chargeId}/{documentType}?email={email}");
			if (result != null && result.ContainsKey("success"))
			{
				return (bool)result["success"];
			}
			return false;
		}

	}
}
