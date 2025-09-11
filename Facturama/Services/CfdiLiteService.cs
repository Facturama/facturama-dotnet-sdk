using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Facturama.Models.Response;
using Facturama.Services.BaseService;

namespace Facturama.Services
{
    public class CfdiLiteService : HttpService<Models.Request.CfdiMulti, Cfdi>
    {
        /// <summary>
        /// Enumeración de los formatos de archivos disponibles para descarga
        /// </summary>
        public enum FileFormat
        {
            Xml, Pdf, Html
        }

        public enum CfdiStatus
        {
            All, Active, Cancel
        }

        public CfdiLiteService(IHttpClient httpClient) : 
            base(httpClient, "")
        {

        }

        public Cfdi Create3(Models.Request.CfdiMulti model)
        {
            return Post(model, "api-lite/3/cfdis");
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
        public CancelationStatusMulti Cancel(string id, string motive = "02", string uuidReplacement = null)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException(nameof(id));
            return base.Delete<CancelationStatusMulti>($"api-lite/cfdis/{id}?motive={motive}&uuidReplacement={uuidReplacement}");
        }

        public Cfdi Retrieve(string id)
        {
            return Get($"cfdi/{id}?type=issuedLite");
        }

        public CfdiSearchResults[] List(string keyword, CfdiStatus status = CfdiStatus.Active)
        {
            keyword = HttpUtility.UrlEncode(keyword);
            return this.Get<CfdiSearchResults[]>($"Cfdi?type=issuedLite&keyword={keyword}&status={status}");
        }

        public CfdiSearchResults[] List(int folioStart = -1, int folioEnd = -1,
            string rfc = null, string taxEntityName = null,
            string dateStart = "", string dateEnd = "",
            string idBranch = "", string serie = "",
            CfdiStatus status = CfdiStatus.Active)
        {
            return this.Get<CfdiSearchResults[]>($"Cfdi?type=issuedLite&status={status}&folioStart={folioStart}&folioEnd={folioEnd}&rfc={rfc}&taxEntityName={taxEntityName}&dateStart={dateStart}&dateEnd={dateEnd}&idBranch={idBranch}&serie={serie}");
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
            return base.Get<InvoiceFile>($"cfdi/{strFormat}/issuedLite/{id}");
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

		public bool SendByMail(string id, string email, string subject = null)
		{
            var result = base.Post<IDictionary<string, object>, object>($"Cfdi?cfdiType=issuedLite&cfdiId={id}&email={email}&subject={subject}", null);
            if (result != null && result.ContainsKey("success"))
			{
				return (bool)result["success"];
			}
			return false;
		}
	}
}
