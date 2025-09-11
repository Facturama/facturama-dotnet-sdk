using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Facturama.Data;
using Facturama.Models;
using Facturama.Models.Response;
using Facturama.Models.Retentions;
using Facturama.Services.BaseService;

namespace Facturama.Services
{
    public class RetentionService : HttpService<Retenciones, Retenciones>
    {
        public RetentionService(IHttpClient httpClient) : 
            base(httpClient, "")
        {
            
        }

        public Retenciones CreateRet(Retenciones model)
        {
            return Post(model, "/retenciones");      
        }
        
        public Retenciones CreateRet2(Retenciones model)
        {
            return Post(model, "2/retenciones");
        }

        /// <summary>
        /// Creación Ret 2.0 Async
        /// </summary>   
        public async Task<Retenciones> CreateRet2Async(Retenciones model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var result = await PostAsync(model, "2/retenciones");
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
        /// https://apisandbox.facturama.mx/guias/retenciones/cancelacion
        /// </summary>
        /// <param name="id">ID del CFDI</param>
        /// <param name="motive">Clave del motivo de cancelación</param>

        /// <returns>Estado de cancelación</returns>
        public CancelationStatus Cancel(string id, string motive = "02", string uuidReplacement = null)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            return base.Delete<CancelationStatus>($"retenciones/{id}?type=issued&motive={motive}&uuidReplacement={uuidReplacement}");
        }


        public Retenciones Retrieve(string id)
        {
            return Get($"retenciones/{id}");
        }

        public CfdiSearchResults[] List(string keyword, CfdiStatus status = CfdiStatus.Active)
        {
            return base.Get<CfdiSearchResults[]>($"Retenciones?keyword={keyword}&status={status}");
        }
        public InvoiceFile GetFile(string id, FileFormat format)
        {
            return base.Get<InvoiceFile>($"retenciones/{id}/{format}");
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
            var result = base.Post<IDictionary<string, object>, object>(null, $"retenciones/envia?id={id}&email={email}");
            if (result != null && result.ContainsKey("success"))
            {
                return (bool)result["success"];
            }
            return false;
        }
        public async Task<bool> SendByMailAsync(string id, string email, string subject = null, InvoiceType type = InvoiceType.Issued)
        {
            try
            {
                var result = await PostAsync<ResponseMailViewModel, Models.Request.Cfdi>(null, $"retenciones/envia?id={id}&email={email}");
                if (result != null && result.success)
                {
                    return result.success;
                }
                return false;
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al intentar enviar mensaje. Cfdi:{id} To: {email}", ex);
            }
        }


    }
}
