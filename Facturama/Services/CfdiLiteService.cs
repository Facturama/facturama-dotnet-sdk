using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using System.Web;
using Facturama.Models.Response;

namespace Facturama.Services
{
    public class CfdiLiteService : CrudService<Models.Request.CfdiMulti, Cfdi>
    {
        private enum FileFormat
        {
            Xml, Pdf, Html
        }

        public enum CfdiStatus
        {
            All, Active, Cancel
        }

        public CfdiLiteService(RestClient httpClient) : 
            base(httpClient, "")
        {

        }

        public override Cfdi Create(Models.Request.CfdiMulti model)
        {
            return Post(model, "api-lite/2/cfdis");
        }

		/// <summary>
		/// Cancelacion con aceptación de CFDI 3.3 vigencia 2018 y posteriores
		/// </summary>
		/// <param name="id">ID del CFDI</param>
		/// <returns>Estado de cancelación</returns>
		public Models.Response.CancelationStatusMulti Cancel(string id)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException(nameof(id));

			var request = new RestRequest(Method.DELETE) { Resource = $"{UriResource}api-lite/cfdis/{id}" };
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
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;
            var file = JsonConvert.DeserializeObject<CfdiSearchResults[]>(response.Content);
            return file;
        }

        public CfdiSearchResults[] List(int folioStart = -1, int folioEnd = -1,
            string rfc = null, string taxEntityName = null,
            string dateStart = "", string dateEnd = "",
            string idBranch = "", string serie = "",
            CfdiStatus status = CfdiStatus.Active)
        {
            var request = new RestRequest($"{UriResource}Cfdi?type=issuedLite&status={status}&folioStart={folioStart}&folioEnd={folioEnd}&rfc={rfc}&taxEntityName={taxEntityName}&dateStart={dateStart}&dateEnd={dateEnd}&idBranch={idBranch}&serie={serie}", Method.GET);
            request.AddHeader("Content-Type", "application/json");

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;
            var list = JsonConvert.DeserializeObject<CfdiSearchResults[]>(response.Content);
            return list;
        }

        private InvoiceFile GetFile(string id, FileFormat format)
        {
			var strFormat = format.ToString().ToLower();

			var request = new RestRequest($"{UriResource}cfdi/{strFormat}/issuedLite/{id}", Method.GET);
            request.AddHeader("Content-Type", "application/json");

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;
            var file = JsonConvert.DeserializeObject<InvoiceFile>(response.Content);
            return file;
        }

        //public void SavePdf(string filePath, string id)
        //{
        //    var file = GetFile(id, FileFormat.Pdf);
        //    File.WriteAllBytes(filePath, Convert.FromBase64String(file.Content));
        //}

        public void SaveXml(string filePath, string id)
        {
            var file = GetFile(id, FileFormat.Xml);
            File.WriteAllBytes(filePath, Convert.FromBase64String(file.Content));
        }

        //public void SaveHtml(string filePath, string id)
        //{
        //    var file = GetFile(id, FileFormat.Html);
        //    File.WriteAllBytes(filePath, Convert.FromBase64String(file.Content));
        //}

        //public bool SendByMail(string id, string email, string subject = null)
        //{
        //    var request = new RestRequest($"Cfdi?cfdiType=issuedLite&cfdiId={id}&email={email}&subject={subject}", Method.POST);
        //    var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
        //    HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

        //    var response = taskCompletionSource.Task.Result;
        //    var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        //    if (result != null && result.ContainsKey("success"))
        //    {
        //        return (bool)result["success"];
        //    }
        //    return false;
        //}
    }
}
