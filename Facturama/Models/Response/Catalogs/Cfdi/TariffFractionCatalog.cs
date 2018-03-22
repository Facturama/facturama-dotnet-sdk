namespace Facturama.Models.Response.Catalogs.Cfdi
{
    public class TariffFractionCatalog : CatalogViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Crtierion { get; set; }
        public string CustomUnit { get; set; }
    }
}