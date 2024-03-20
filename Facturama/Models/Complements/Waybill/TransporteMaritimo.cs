

namespace Facturama.Models.Complements.Waybill
{
    public class TransporteMaritimo
	{
        public string PermSCT { get; set; }
        public string NumPermisoSCT { get; set; }
        public string NombreAseg { get; set; }
        public string NumPolizaSeguro { get; set; }
        public string TipoEmbarcacion { get; set; }
        
        public string Matricula { get; set; }
        
        public string NumeroOMI { get; set; }
        public int AnioEmbarcacion { get; set; }
        public string NombreEmbarc { get; set; }
        
        public string NacionalidadEmbarc { get; set; }
        
        public decimal UnidadesDeArqBruto { get; set; }
       
        public string TipoCarga { get; set; }
       
        public string NumCertITC { get; set; }
        public decimal Eslora { get; set; }
        public decimal Manga { get; set; }
        public decimal Calado { get; set; }

        public decimal Puntal { get; set; }
        public string LineaNaviera { get; set; }
       
        public string NombreAgenteNaviero { get; set; }
        
        public string NumAutorizacionNaviero { get; set; }
        public string NumViaje { get; set; }
        public string NumConocEmbarc { get; set; }

        public string PermisoTempNavegacion { get; set; }

        public Contenedor[] Contenedor { get; set; }

        public RemolquesCCP[] RemolquesCCP { get; set; }

    }
}
