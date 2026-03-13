using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Complements.NotariosPublicos
{
    public  class NotariosPublicos
    {
        public string Version { get; set; }
        public DatosOperacion DatosOperacion { get; set; }

        public DatosNotario DatosNotario { get; set; }

        public List<DescInmuebles> DescInmuebles { get; set; }

        public DatosEnajenante DatosEnajenante { get; set; }

        public DatosAdquiriente DatosAdquiriente { get; set; }
    }

    public class DatosOperacion
    {
        public string NumInstrumentoNotarial { get; set; }
        public string FechaInstNotarial { get; set; }
        public string MontoOperacion { get; set; }
        public string Subtotal { get; set; }
        public string IVA { get; set; }
    }
    public class DatosNotario
    {
        public string CURP { get; set; }
        public string NumNotaria { get; set; }
        public string EntidadFederativa { get; set; }
        public string Adscripcion { get; set; }
    }
    public class DescInmuebles
    {
        public string TipoInmueble { get; set; }
        public string Calle { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }
    }

    public class DatosEnajenante
    {
        public string CoproSocConyugalE { get; set; }
        public Item Item { get; set; }

        

    }
    public class DatosAdquiriente
    {
        public string CoproSocConyugalE { get; set; }
        public Item Item { get; set; }
        
    }

    public class Item
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }

        public List<DatosEnajenanteCopSC> DatosEnajenanteCopSC { get; set; }

    }
    public class DatosEnajenanteCopSC
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string Porcentaje { get; set; }

    }
}
