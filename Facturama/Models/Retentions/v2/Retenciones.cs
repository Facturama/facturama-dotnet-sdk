using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions.v2
{
    public class Retenciones
    {
        public string FolioInt { get; set; }

        /// <summary>
        /// Atributo requerido para la expresión de la fecha y hora de
        /// expedición del documento de retención e información de
        /// pagos. Se expresa en la forma yyyy-mmddThh:mm:ssTZD-6, de acuerdo con la especificación ISO 8601.
        /// </summary>		
        public string FechaExp { get; set; }

        /// <summary>
        /// Atributo requerido para expresar la clave de la retención e
        /// información de pagos de acuerdo al catálogo publicado en internet por el SAT.
        /// </summary>		
        public string CveRetenc { get; set; }

        /// <summary>
        /// Atributo opcional que expresa la descripción de la
        /// retención e información de pagos en caso de que en el
        ///	atributo CveRetenc se haya elegido el valor para 'otro tipo de retenciones'.
        /// </summary>	
        public string DescRetenc { get; set; }

        /// <summary>
        /// Nodo requerido para expresar la información del contribuyente emisor del documento
        /// electrónico de retenciones e información de pagos.
        /// </summary>		
        public Emisor Emisor { get; set; }

        /// <summary>
        /// Nodo requerido para expresar la información del contribuyente receptor.
        /// </summary>

        public Receptor Receptor { get; set; }

        /// <summary>
        /// Nodo requerido para expresar el periodo que ampara el documento de retenciones e información de pagos.
        /// </summary>
        public Periodo Periodo { get; set; }

        /// <summary>
        /// Nodo requerido para expresar el total de las retenciones e información de pagos efectuados en el período que ampara el documento.
        /// </summary>	
        public Totales Totales { get; set; }

        /// <summary>
        /// Nodo opcional donde se incluirá el complemento Timbre Fiscal Digital de manera
        /// obligatoria y los nodos complementarios determinados por el SAT, de acuerdo a las
        /// disposiciones particulares a un sector o actividad específica.
        /// </summary>
        public Complemento Complemento { get; set; }

        /// <summary>
        /// Identificador unico de Facturama
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Cadena original de la retencion
        /// </summary>
        public string CadenaOriginal { get; set; }

        /// <summary>
        /// Indica si la retencion esta cancelada
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        /// Sello Digital del CFDI (solo lectura)
        /// </summary>
        public string Sello { get; set; }

        /// <summary>
        /// Atributo requerido para incorporar el código postal del lugar de
        /// expedición del comprobante que ampara retenciones e información de
        /// pagos.
        /// </summary>
        public string LugarExpRetenc { get; set; }
    }
}
