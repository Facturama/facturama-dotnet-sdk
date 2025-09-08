using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Facturama.Data;
using Facturama.Models;
using Facturama.Models.Response;
using Facturama.Models.Retentions;

namespace Facturama.Services
{
    public class RetentionService : CrudService<Models.Retentions.Retenciones, Models.Retentions.Retenciones>
    {
        public RetentionService(IHttpClient httpClient) : 
            base(httpClient, "")
        {
            
        }

        public override Models.Retentions.Retenciones CreateRet(Models.Retentions.Retenciones model)
        {
            return this.HttpClient.Post<Models.Retentions.Retenciones,Models.Retentions.Retenciones>($"/retenciones",model);
        }
        
        public override Models.Retentions.Retenciones CreateRet2(Models.Retentions.Retenciones model)
        {
            return this.HttpClient.Post<Models.Retentions.Retenciones, Models.Retentions.Retenciones>($"2/retenciones", model);
        }

        /// <summary>
        /// Creación Ret 2.0 Async
        /// </summary>   
        public async Task<Models.Retentions.Retenciones> CreateRet2Async(Models.Retentions.Retenciones model)
        {
            return await this.HttpClient.PostAsync<Models.Retentions.Retenciones, Models.Retentions.Retenciones>("2/retenciones",model);  
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

            return this.HttpClient.Delete<Models.Response.CancelationStatus>($"api/retenciones/{id}?type=issued&motive={motive}&uuidReplacement={uuidReplacement}");
        }


        public override Retenciones Retrieve(string id)
        {
            return this.HttpClient.Get<Retenciones>($"retenciones/{id}");
        }

        public CfdiSearchResults[] List(string keyword, CfdiStatus status = CfdiStatus.Active)
        {
            keyword = HttpUtility.UrlEncode(keyword);
            return this.HttpClient.Get<CfdiSearchResults[]>($"Retenciones?keyword={keyword}&status={status}");
        }
        public InvoiceFile GetFile(string id, FileFormat format)
        {
			var strFormat = format.ToString().ToLower();
            return this.HttpClient.Get<InvoiceFile>($"retenciones/{id}/{strFormat}");
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
            var response= this.HttpClient.Post<IDictionary<string, object>,object>($"retenciones/envia?id={id}&email={email}",null);
            if (response != null && response.ContainsKey("success"))
            {
                return (bool)response["success"];
            }
            return false;

        }
        public async Task<bool> SendByMailAsync(string id, string email, string subject = null, InvoiceType type = InvoiceType.Issued)
        {
            var result = await this.HttpClient.PostAsync<ResponseMailViewModel,object>($"retenciones/envia?id={id}&email={email}",null);
            if (result != null && result.success)
            {
                return result.success;
            }
            return false;
            
        }


    }
}
