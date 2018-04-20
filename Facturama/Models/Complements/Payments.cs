using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturama.Models.Complements
{
    public class RelatedDocument
    {
        public string Uuid { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public string Currency { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string PaymentMethod { get; set; }
        public int PartialityNumber { get; set; }
        public decimal PreviousBalanceAmount { get; set; }
        public decimal AmountPaid { get; set; }
    }

    public class Tax
    {
        public decimal Total { get; set; }
        public string Name { get; set; }
        public decimal Base { get; set; }
        public decimal Rate { get; set; }
        public bool IsRetention { get; set; }
        public bool IsQuota { get; set; }
    }

    public class Payment
    {
        public List<RelatedDocument> RelatedDocuments { get; set; }
        public List<Tax> Taxes { get; set; }
        public string Date { get; set; }
        public string PaymentForm { get; set; }
        public string Currency { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public string OperationNumber { get; set; }
        public string RfcIssuerPayerAccount { get; set; }
        public string ForeignAccountNamePayer { get; set; }
        public string PayerAccount { get; set; }
        public string RfcReceiverBeneficiaryAccount { get; set; }
        public string BeneficiaryAccount { get; set; }
    }
    
}
