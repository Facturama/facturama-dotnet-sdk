﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Facturama.Models.Response;
using Facturama.Models.Retentions;
using Newtonsoft.Json;
using RestSharp;

namespace Facturama.Services
{
    public class RetentionService : CrudService<Models.Retentions.Retenciones, Models.Retentions.Retenciones>
    {
        public enum FileFormat
        {
            Xml, Pdf, Html
        }

        public enum CfdiStatus
        {
            All, Active, Cancel
        }

        public RetentionService(RestClient httpClient) : 
            base(httpClient, "")
        {
            
        }

        public override Models.Retentions.Retenciones CreateRet(Models.Retentions.Retenciones model)
        {
            return Post(model, "/retenciones");      
        }
        
        public override Models.Retentions.Retenciones CreateRet2(Models.Retentions.Retenciones model)
        {
            return Post(model, "2/retenciones");
        }

        /// <summary>
        /// Cancelacion con motivo (obligatorio a partir de 01 de enero de 2022)
        /// Nota: en el caso de que No se especifique el motivo de cancelación se considera el 02
        /// https://apisandbox.facturama.mx/guias/retenciones/cancelacion
        /// </summary>
        /// <param name="id">ID del CFDI</param>
        /// <param name="motive">Clave del motivo de cancelación</param>

        /// <returns>Estado de cancelación</returns>
        public Models.Response.CancelationStatus Cancel(string id, string motive = "02", string uuidReplacement = null)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var request = new RestRequest(Method.DELETE) { Resource = $"api/retenciones/{id}?type=issued&motive={motive}&uuidReplacement={uuidReplacement}" };
            var response = Execute(request);
            return JsonConvert.DeserializeObject<Models.Response.CancelationStatus>(response.Content);
        }


        public override Retenciones Retrieve(string id)
        {
            return Get($"retenciones/{id}");
        }

        public CfdiSearchResults[] List(string keyword, CfdiStatus status = CfdiStatus.Active)
        {
            keyword = HttpUtility.UrlEncode(keyword);
            var request = new RestRequest($"Retenciones?keyword={keyword}&status={status}", Method.GET);
            request.AddHeader("Content-Type", "application/json");

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

            var response = taskCompletionSource.Task.Result;
            var file = JsonConvert.DeserializeObject<CfdiSearchResults[]>(response.Content);
            return file;
        }
        public InvoiceFile GetFile(string id, FileFormat format)
        {
			var strFormat = format.ToString().ToLower();
            var request = new RestRequest($"retenciones/{id}/{strFormat}", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            
            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            HttpClient.ExecuteAsync(request, restResponse => taskCompletionSource.SetResult(restResponse));

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

        public bool SendByMail(string id, string email)
        {   
            var request = new RestRequest($"retenciones/envia?id={id}&email={email}", Method.POST);
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
