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
        private enum FileFormat
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

        public override Models.Response.Cfdi Create(Models.Request.Cfdi model)
        {
            return Post(model, "2/cfdis");
        }

        public override Models.Response.Cfdi Remove(string id)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var request = new RestRequest(Method.DELETE) { Resource = $"{UriResource}cfdi/{id}?type=issued" };
            var response = Execute(request);
            return JsonConvert.DeserializeObject<Models.Response.Cfdi>(response.Content);
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

        private InvoiceFile GetFile(string id, FileFormat format, InvoiceType type = InvoiceType.Issued)
        {
            
            var request = new RestRequest($"{UriResource}cfdi/{format}/{type}/{id}", Method.GET);
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
            var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (result != null && result.ContainsKey("success"))
            {
                return (bool)result["success"];
            }
            return false;

        }
    }
}
