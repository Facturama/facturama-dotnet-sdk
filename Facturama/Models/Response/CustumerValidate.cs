using Newtonsoft.Json;


namespace Facturama.Models.Response
{
    public class CustumerValidate
    {

        [JsonProperty("ExistRfc")]
        public string ExistRfc { get; set; }


        [JsonProperty("MatchName")]
        public string MatchName { get; set; }



        [JsonProperty("MatchZipCode")]
        public string MatchZipCode { get; set; }



        [JsonProperty("MatchFiscalRegime")]
        public string MatchFiscalRegime { get; set; }

    }
}
