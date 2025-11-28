using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using System.Web;
using Facturama.Models.Response;
using Facturama.Data;

namespace Facturama.Services
{
    public class CfdiLiteService : CrudService<Models.Request.CfdiMulti, Cfdi>
    {
        public CfdiLiteService(IHttpClient httpClient) : 
            base(httpClient, "")
        {

        }

        [Obsolete(" El método 'Create' está OBSOLETO, por favor utiliza 'Create3'.")]
        public override Cfdi Create(Models.Request.CfdiMulti model)
        {
            return Post(model, "api-lite/2/cfdis");
        }
        public override Cfdi Create3(Models.Request.CfdiMulti model)
        {
            return Post(model, "api-lite/3/cfdis");
        }

        public override Cfdi Create4(Models.Request.CfdiMulti model)
        {
            return Post(model, "api-lite/4/cfdis");
        }

        /// <summary>
        /// Cancelacion con motivo (obligatorio a partir de 01 de enero de 2022)
        /// Nota: en el caso de que No se especifique el motivo de cancelación se considera el 02
        /// https://apisandbox.facturama.mx/guias/api-multi/cfdi/cancelacion
        /// </summary>
        /// <param name="id">ID del CFDI</param>
        /// <param name="motive">Clave del motivo de cancelación</param>
        /// <param name="uuidReplacement">UUID del comprobante que sustituye al cancelado</param>
        /// <returns>Estado de cancelación</returns>
        public Models.Response.CancelationStatusMulti Cancel(string id, string motive = "02", string uuidReplacement = null)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException(nameof(id));

			var request = new RestRequest(Method.DELETE) { Resource = $"{UriResource}api-lite/cfdis/{id}?motive={motive}&uuidReplacement={uuidReplacement}" };
			var response = Execute(request);
			return JsonConvert.DeserializeObject<Models.Response.CancelationStatusMulti>(response.Content);
		}

		/// <summary>
		/// Cancelación CFDI3.3  vigencia 2017 y anteriores
		/// </summary>
		/// <param name="id">ID del CFDI</param>
		/// <returns>CFDI</returns>
		[Obsolete(" El método 'Remove' está OBSOLETO, por favor utiliza 'Cancel',  para la 'cancelación con aceptación' implementada en 2018.")]
		public override Cfdi Remove(string id)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            
            var request = new RestRequest(Method.DELETE) { Resource = $"{UriResource}api-lite/cfdis/{id}" };
            var response = Execute(request);			

			return Retrieve(id);
		}

        public override Cfdi Retrieve(string id)
        {
            return Get($"cfdi/{id}?type=issuedLite");
        }

        public CfdiSearchResults[] List(string keyword, CfdiStatus status = CfdiStatus.Active)
        {
            keyword = HttpUtility.UrlEncode(keyword);
            var request = new RestRequest($"{UriResource}Cfdi?type=issuedLite&keyword={keyword}&status={status}", Method.GET);
            request.AddHeader("Content-Type", "application/json");

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, taskCompletionSource);

            var response = taskCompletionSource.Task.Result;
            var file = JsonConvert.DeserializeObject<CfdiSearchResults[]>(response.Content);
            return file;
        }

        public CfdiSearchResults[] List(
                    int? folioStart = null,
                    int? folioEnd = null,
                    string rfcReceiver = "",
                    string taxEntityName = "",
                    string dateStart = null,
                    string dateEnd = null,
                    string serie = "",
                    CfdiStatus status = CfdiStatus.all,
                    string rfcIssuer = "",
                    int page = 0)
        {
            var request = new RestRequest($"{UriResource}Cfdi?" +
                $"type=issuedLite&status={status}" +
                $"&folioStart={folioStart}" +
                $"&folioEnd={folioEnd}" +
                $"&rfc={rfcReceiver}" +
                $"&taxEntityName={taxEntityName}" +
                $"&dateStart={dateStart}" +
                $"&dateEnd={dateEnd}" +
                $"&serie={serie}" +
                $"&rfcIssuer={rfcIssuer}" +
                $"&page={page}", Method.GET);

            request.AddHeader("Content-Type", "application/json");

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, taskCompletionSource);

            var response = taskCompletionSource.Task.Result;
            var list = JsonConvert.DeserializeObject<CfdiSearchResults[]>(response.Content);
            return list;
        }

        /// <summary>
        /// Obtiene el Archivo de la factura en Base64. 
        /// </summary>
        /// <param name="id">Identificador del CFDI</param>
        /// <param name="format">Formato en que se desea obtener,  debe ser una instancia de la enumeración (Xml, Pdf, Html)</param>
        /// <returns>Objeto InvoiceFile el cual en el atributo 'Content' tiene el Base64 del archivo</returns>
        public InvoiceFile GetFile(string id, FileFormat format)
        {
			var strFormat = format.ToString().ToLower();

			var request = new RestRequest($"{UriResource}cfdi/{strFormat}/issuedLite/{id}", Method.GET);
            request.AddHeader("Content-Type", "application/json");

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, taskCompletionSource);

            var response = taskCompletionSource.Task.Result;
            var file = JsonConvert.DeserializeObject<InvoiceFile>(response.Content);
            return file;
        }

		public void SavePdf(string filePath, string id)
		{
			var file = GetFile(id, FileFormat.Pdf);
			File.WriteAllBytes(filePath, Convert.FromBase64String(file.Content));
		}

		public void SaveXml(string filePath, string id)
        {
            var file = GetFile(id, FileFormat.Xml);
            File.WriteAllBytes(filePath, Convert.FromBase64String(file.Content));
        }

		public void SaveHtml(string filePath, string id)
		{
			var file = GetFile(id, FileFormat.Html);
			File.WriteAllBytes(filePath, Convert.FromBase64String(file.Content));
		}

		public bool SendByMail(string id, string email, string subject = null, string comments = null, string issuerEmail = null )
		{
			var request = new RestRequest($"Cfdi?" +
                $"cfdiType=issuedLite" +
                $"&cfdiId={id}" +
                $"&email={email}" +
                $"&subject={subject}" +
                $"&comments={comments}" +
                $"&issuerEmail={issuerEmail}", Method.POST);
			var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
			HttpClient.ExecuteAsync(request, taskCompletionSource);

			var response = taskCompletionSource.Task.Result;
			var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
			if (result != null && result.ContainsKey("success"))
			{
				return (bool)result["success"];
			}
			return false;
		}
	}
}
