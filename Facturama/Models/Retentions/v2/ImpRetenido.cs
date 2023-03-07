using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions.v2
{
    public class ImpRetenido
    {
        /// <summary>
        /// Atributo opcional para expresar la base del impuesto, que puede ser la diferencia entre los ingresos percibidos y las
        /// deducciones autorizadas.
        /// </summary>		
        public decimal? BaseRet { get; set; }

        /// <summary>
        /// Atributo opcional para señalar el tipo de impuesto retenido del periodo o ejercicio conforme al catálogo.
        /// </summary>
        public string Impuesto { get; set; }

        /// <summary>
        /// Atributo requerido para expresar el importe del impuesto retenido en el periodo o ejercicio.
        /// </summary>
        public decimal MontoRet { get; set; }

        /// <summary>
        /// Atributo requerido para precisar si el monto de la retención es considerado pago definitivo o pago provisional.
        /// </summary>		
        public string TipoPagoRet { get; set; }

    }
}
