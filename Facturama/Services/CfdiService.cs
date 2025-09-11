using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Facturama.Data;
using Facturama.Models.Response;
using Facturama.Services.BaseService;

namespace Facturama.Services
{
    public class CfdiService : HttpService<Models.Request.Cfdi, Cfdi>
    {

        public CfdiService(IHttpClient httpClient) : 
            base(httpClient, "")
        {
            
        }

        /// <summary>
        /// Creación CFDI4.0
        /// </summary>   
        public Cfdi Create3(Models.Request.Cfdi model)
        {
            return Post(model, "3/cfdis");
        }

        /// <summary>
        /// Creación CFDI4.0 Async
        /// </summary>   
        public async Task<Cfdi> Create3Async(Models.Request.Cfdi model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var result = await this.PostAsync<Cfdi, Models.Request.Cfdi>(model, "3/cfdis");
                return result;
            }
            catch
            {
                throw;
            }
           
        }

        /// <summary>
        /// Cancelacion con motivo (obligatorio a partir de 01 de enero de 2022)
        /// Nota: en el caso de que No se especifique el motivo de cancelación se considera el 02
        /// https://apisandbox.facturama.mx/guias/api-web/cfdi/cancelacion        
        /// </summary>
        /// <param name="id">ID del CFDI</param>
        /// <param name="motive">Clave del motivo de cancelación</param>
        /// <param name="uuidReplacement">UUID del comprobante que sustituye al cancelado</param>
        /// <returns>Estado de cancelación</returns>
        public CancelationStatus Cancel(string id, string motive = "02", string uuidReplacement = null)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            return base.Delete<Models.Response.CancelationStatus>($"cfdi/{id}?type=issued&motive={motive}&uuidReplacement={uuidReplacement}");
        }


		public Cfdi Retrieve(string id, InvoiceType type = InvoiceType.Issued)
        {
            return Get($"cfdi/{id}?type={type}");
        }

        public CfdiSearchResults[] List(string keyword, CfdiStatus status = CfdiStatus.Active, InvoiceType type = InvoiceType.Issued)
        {
            return base.Get<CfdiSearchResults[]>($"Cfdi?type={type}&keyword={keyword}&status={status}");
        }
        
        public CfdiSearchResults[] List(int folioStart = -1, int folioEnd = -1, 
            string rfc = null, string taxEntityName = null, 
            string dateStart = "", string dateEnd = "",
            string idBranch = "", string serie = "",
            CfdiStatus status = CfdiStatus.Active, InvoiceType type = InvoiceType.Issued)
        {
            return base.Get<CfdiSearchResults[]>($"Cfdi?type={type}&status={status}&folioStart={folioStart}&folioEnd={folioEnd}&rfc={HttpUtility.UrlEncode(rfc)}&taxEntityName={HttpUtility.UrlEncode(taxEntityName)}&dateStart={dateStart}&dateEnd={dateEnd}&idBranch={HttpUtility.UrlEncode(idBranch)}&serie={HttpUtility.UrlEncode(serie)}");
        }

        public InvoiceFile GetFile(string id, FileFormat format, InvoiceType type = InvoiceType.Issued)
        {
			var strFormat = format.ToString().ToLower();
            return base.Get<InvoiceFile>($"cfdi/{strFormat}/{type}/{id}");
        }

        public void SavePdf(string filePath, string id, InvoiceType type = InvoiceType.Issued)
        {
            var file = GetFile(id, FileFormat.Pdf, type);
            File.WriteAllBytes(filePath, Convert.FromBase64String(file.Content));
        }

        public void SaveXml(string filePath, string id, InvoiceType type = InvoiceType.Issued)
        {
            var file = GetFile(id, FileFormat.Xml, type);
            File.WriteAllBytes(filePath, Convert.FromBase64String(file.Content));
        }

        public void SaveHtml(string filePath, string id, InvoiceType type = InvoiceType.Issued)
        {
            var file = GetFile(id, FileFormat.Html, type);
            File.WriteAllBytes(filePath, Convert.FromBase64String(file.Content));
        }

        public bool SendByMail(string id, string email, string subject = null, InvoiceType type = InvoiceType.Issued)
        {
            return base.Post<bool, Models.Request.Cfdi>(null, $"Cfdi?cfdiType={type}&cfdiId={id}&email={email}&subject={subject}");
        }

        public Task<bool> SendByMailAsync(string id, string email, string subject = null, InvoiceType type = InvoiceType.Issued)
        {
            return base.PostAsync<bool, Models.Request.Cfdi>(null, $"Cfdi?cfdiType={type}&cfdiId={id}&email={email}&subject={subject}");
        }

        /// <summary>
        /// Carga de una factura recibida.
        /// Se coloca en la entidad fiscal  que realiza la llamada
        /// </summary>
        /// <param name="cfdiXMLBase64">Archivo XML en string de base64</param>
        public bool Upload(string cfdiXMLBase64)
		{
			var invFile = new InvoiceFile()
			{
				Content = cfdiXMLBase64,
				ContentEncoding = "base64",
				ContentType = "xml",
				ContentLength = cfdiXMLBase64.Length
			};

            return this.Put<bool, InvoiceFile>(invFile, "upload/cfdi");
        }		

	}
}
