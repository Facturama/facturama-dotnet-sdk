using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions.v2
{
    public class Nacional
    {
        /// <summary>
        /// Atributo requerido para la clave del Registro Federal de
        /// Contribuyentes correspondiente al contribuyente receptor	del documento.
        /// </summary>		
        public string RfcRecep { get; set; }

        /// <summary>
        /// Atributo opcional para el nombre, denominación o razón social del contribuyente receptor del documento.
        /// </summary>

        public string NomDenRazSocR { get; set; }

        /// <summary>
        /// Atributo opcional para la Clave Única del Registro Poblacional del contribuyente receptor del documento.
        /// </summary>		
        public string CurpR { get; set; }

        /// <summary>
        /// Atributo requerido para registrar el código postal del domicilio fiscal del
        /// receptor del comprobante que ampara retenciones e información de
        /// pagos.
        /// </summary>
        public string DomicilioFiscalR { get; set; }
    }
}
