using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Facturama.Models.Response;
using Newtonsoft.Json;
using RestSharp;

namespace Facturama.Services
{
    public class CfdiService : CrudService<Models.Request.Cfdi, Models.Response.Cfdi>
    {
        public enum FileFormat
        {
            Xml, Pdf, Html
        }

        public enum InvoiceType
        {
            Issued, Received, Payroll
        }

        public enum CfdiStatus
        {
            All, Active, Cancel
        }

        public CfdiService(RestClient httpClient) : 
            base(httpClient, "")
        {
            
        }

        /// <summary>
        /// Creación CFDI3.3, Método obsoleto
        /// </summary>      
        [Obsolete(" El método 'Create' está OBSOLETO, por favor utiliza 'Create3'")]
        public override Models.Response.Cfdi Create(Models.Request.Cfdi model)
        {
            return Post(model, "2/cfdis");     // Método fuera de vigencia 
        }

        /// <summary>
        /// Creación CFDI4.0
        /// </summary>   
        public override Models.Response.Cfdi Create3(Models.Request.Cfdi model)
        {
            return Post(model, "3/cfdis");
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

            var request = new RestRequest(Method.DELETE) { Resource = $"{UriResource}cfdi/{id}?type=issued&motive={motive}&uuidReplacement={uuidReplacement}" };
            var response = Execute(request);
            return JsonConvert.DeserializeObject<Models.Response.CancelationStatus>(response.Content);
        }

		/// <summary>
		/// Cancelación CFDI3.3  vigencia 2017 y anteriores
		/// </summary>
		/// <param name="id">ID del CFDI</param>
		/// <returns>CFDI</returns>
		[Obsolete(" El método 'Remove' está OBSOLETO, por favor utiliza 'Cancel',  para la 'cancelación con aceptación' implementada en 2018.")]
		public override Models.Response.Cfdi Remove(string id)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException(nameof(id));

			var request = new RestRequest(Method.DELETE) { Resource = $"{UriResource}cfdi/{id}?type=issued" };
			var response = Execute(request);

			return Retrieve(id);			
		}



		public Cfdi Retrieve(string id, InvoiceType type = InvoiceType.Issued)
        {
            return Get($"cfdi/{id}?type={type}");
        }

        public CfdiSearchResults[] List(string keyword, CfdiStatus status = CfdiStatus.Active, InvoiceType type = InvoiceType.Issued)
        {
            keyword = HttpUtility.UrlEncode(keyword);
            var request = new RestRequest($"{UriResource}Cfdi?type={type}&keyword={keyword}&status={status}", Method.GET);
            request.AddHeader("Content-Type", "application/json");

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;
            var file = JsonConvert.DeserializeObject<CfdiSearchResults[]>(response.Content);
            return file;
        }
        
        public CfdiSearchResults[] List(int folioStart = -1, int folioEnd = -1, 
            string rfc = null, string taxEntityName = null, 
            string dateStart = "", string dateEnd = "",
            string idBranch = "", string serie = "",
            CfdiStatus status = CfdiStatus.Active, InvoiceType type = InvoiceType.Issued)
        {
            var request = new RestRequest($"{UriResource}Cfdi?type={type}&status={status}&folioStart={folioStart}&folioEnd={folioEnd}&rfc={rfc}&taxEntityName={taxEntityName}&dateStart={dateStart}&dateEnd={dateEnd}&idBranch={idBranch}&serie={serie}", Method.GET);
            request.AddHeader("Content-Type", "application/json");

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;
            var list = JsonConvert.DeserializeObject<CfdiSearchResults[]>(response.Content);
            return list;
        }

        public InvoiceFile GetFile(string id, FileFormat format, InvoiceType type = InvoiceType.Issued)
        {
			var strFormat = format.ToString().ToLower();
            var request = new RestRequest($"{UriResource}cfdi/{strFormat}/{type}/{id}", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            
            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;
            var file = JsonConvert.DeserializeObject<InvoiceFile>(response.Content);
            return file;
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
            var request = new RestRequest($"Cfdi?cfdiType={type}&cfdiId={id}&email={email}&subject={subject}", Method.POST);
            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;
            try { 
                var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
                if (result != null && result.ContainsKey("success"))
                {
                    return (bool)result["success"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al intentar enviar mensaje. Content: {response?.Content}", ex);
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
