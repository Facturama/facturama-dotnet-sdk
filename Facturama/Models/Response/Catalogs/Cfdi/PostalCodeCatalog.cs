namespace Facturama.Models.Response.Catalogs.Cfdi
{
    public class PostalCodeCatalog : CatalogViewModel
    {
        public string StateCode { get; set; }
        public string MunicipalityCode { get; set; }
        public string LocationCode { get; set; }
    }
}