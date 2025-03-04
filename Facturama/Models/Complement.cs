using System.Collections.Generic;
using Facturama.Models.Complements;
using Facturama.Models.Complements.Payroll;
using Facturama.Models.Complements.TaxLegends;
using Facturama.Models.Complements.Donation;
using Facturama.Models.Complements.Waybill;
using Facturama.Models.Response;
using Newtonsoft.Json;
using Facturama.Models.Complements.ForeignTrade;

namespace Facturama.Models
{
    /// <summary>
    /// Complementos del comprobante.
    /// Aplican para todo el comprobante
    /// </summary>
    public class Complement
    {
        [JsonProperty("TaxStamp")]
        public TaxStamp TaxStamp { get; set; }

        [JsonProperty("Payments")]
        public List<Payment> Payments { get; set; }
		
	    [JsonProperty("Payroll")]
	    public Payroll Payroll { get; set; }

        /// <summary>
        /// Complemento de carta porte 2.0
        /// </summary>
        public ComplementoCartaPorte20 CartaPorte20 { get; set; }

        /// <summary>
        /// Complemento de carta porte 3.0
        /// </summary>
        public ComplementoCartaPorte30 CartaPorte30 { get; set; }

        /// <summary>
        /// Complemento de carta porte 3.1
        /// </summary>
        public ComplementoCartaPorte31 CartaPorte31 { get; set; }

        /// <summary>
        /// Complemento leyendas fiscales
        /// </summary>
        public TaxLegends TaxLegends { get; set; }

        /// <summary>
        /// Complemento Donativos 
        /// </summary>
        public Donation Donation { get; set; }

        public ForeignTrade ForeignTrade { get; set; }
    }
}


