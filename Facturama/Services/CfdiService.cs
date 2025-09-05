using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Facturama.Data;
using Facturama.Models;
using Facturama.Models.Request;
using Facturama.Models.Response;
using Newtonsoft.Json;

namespace Facturama.Services
{
    public class CfdiService : CrudService<Models.Request.Cfdi, Models.Response.Cfdi>
    {

        public CfdiService(IHttpClient httpClient) : 
            base(httpClient, "")
        {
            
        }
        /// <summary>
        /// Creación CFDI4.0
        /// </summary>   
        public override Models.Response.Cfdi Create3(Models.Request.Cfdi model)
        {
            return this.HttpClient.Post<Models.Response.Cfdi, Models.Request.Cfdi>("3/cfdis", model);
        }

        /// <summary>
        /// Creación CFDI4.0 Async
        /// </summary>   
        public async Task<Models.Response.Cfdi> Create3Async(Models.Request.Cfdi model)
        {
            return await this.HttpClient.PostAsync<Models.Response.Cfdi, Models.Request.Cfdi>("3/cfdis",model);
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
        public Models.Response.CancelationStatus Cancel(string id, string motive = "02", string uuidReplacement = null)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            return this.HttpClient.Delete<Models.Response.CancelationStatus>($"{UriResource}cfdi/{id}?type=issued&motive={motive}&uuidReplacement={uuidReplacement}");
        }

		public Facturama.Models.Response.Cfdi Retrieve(string id, InvoiceType type = InvoiceType.Issued)
        {
            return this.HttpClient.Get<Facturama.Models.Response.Cfdi>($"cfdi/{id}?type={type}");
        }

        public CfdiSearchResults[] List(string keyword, CfdiStatus status = CfdiStatus.Active, InvoiceType type = InvoiceType.Issued)
        {
            keyword = HttpUtility.UrlEncode(keyword);
            HttpRequestOptions option = new HttpRequestOptions()
            {
                ContentType = "application/json",
            };

            return this.HttpClient.Get<CfdiSearchResults[]>($"{UriResource}Cfdi?type={type}&keyword={keyword}&status={status}", option);
        }
        
        public CfdiSearchResults[] List(int folioStart = -1, int folioEnd = -1, 
            string rfc = null, string taxEntityName = null, 
            string dateStart = "", string dateEnd = "",
            string idBranch = "", string serie = "",
            CfdiStatus status = CfdiStatus.Active, InvoiceType type = InvoiceType.Issued)
        {
            HttpRequestOptions option = new HttpRequestOptions()
            {
                ContentType = "application/json",
            };

            return this.HttpClient.Get<CfdiSearchResults[]>($"{UriResource}Cfdi?type={type}&status={status}&folioStart={folioStart}&folioEnd={folioEnd}&rfc={rfc}&taxEntityName={taxEntityName}&dateStart={dateStart}&dateEnd={dateEnd}&idBranch={idBranch}&serie={serie}", option);
        }

        public InvoiceFile GetFile(string id, FileFormat format, InvoiceType type = InvoiceType.Issued)
        {
			var strFormat = format.ToString().ToLower();
            HttpRequestOptions option = new HttpRequestOptions()
            {
                ContentType = "application/json",
            };

            return this.HttpClient.Get<InvoiceFile>($"{UriResource}cfdi/{strFormat}/{type}/{id}", option);
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
            var result = this.HttpClient.Post<ResponseMailViewModel, Models.Request.Cfdi>($"Cfdi?cfdiType={type}&cfdiId={id}&email={email}&subject={subject}", null);
            if (result != null && result.success)
            {
                return result.success;
            }
            return false;
        }

        public async Task<bool> SendByMailAsync(string id, string email, string subject = null, InvoiceType type = InvoiceType.Issued)
        {
            var result = await this.HttpClient.PostAsync<ResponseMailViewModel,Models.Request.Cfdi>($"Cfdi?cfdiType={type}&cfdiId={id}&email={email}&subject={subject}",null);
            if (result != null && result.success)
            {
                return result.success;
            }
            return false;
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
			

			var request = new RestRequest(Method.POST) { Resource = $"{UriResource}/upload/cfdi" };
			request.AddHeader("Content-Type", "application/json");
			var json = JsonConvert.SerializeObject(invFile, Formatting.None, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() }
			});
			request.AddParameter("application/json", json, ParameterType.RequestBody);
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
