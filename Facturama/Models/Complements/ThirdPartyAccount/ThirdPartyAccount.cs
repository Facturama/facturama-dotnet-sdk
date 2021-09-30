using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Facturama.Models.Complements.ThirdPartyAccount
{
	public class ThirdPartyAccount
    {
        [JsonProperty("Rfc")]
        public string Rfc { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ThirdTaxInformation")]
        public Address ThirdTaxInformation { get; set; }

        [JsonProperty("CustomsInformation")]
        public CustomsInformation CustomsInformation { get; set; }

        [JsonProperty("Parts")]
        public List<Part> Parts { get; set; }

        [JsonProperty("PropertyTaxNumber")]
        public string PropertyTaxNumber { get; set; }

        [JsonProperty("Taxes")]
        public List<ThirdPartyAccountTax> Taxes { get; set; }
    }
}
