namespace Facturama.Models.Response.Catalogs.Cfdi
{
    public class CurrencyCatalog : CatalogViewModel
    {
        public decimal Decimals { get; set; }
        public decimal PrecisionRate { get; set; }
    }

    public class ProductServicesCatalog : CatalogViewModel
    {
        public string IncludeIva { get; set; }
        public string IncludeIeps { get; set; }
        public string Complement { get; set; }
    }

    public class CfdiTypesCatalog : CatalogViewModel
    {
        public int NameId { get; set; }
    }

    public class UnitCatalog : CatalogViewModel
    {
        public string ShortName { get; set; }
    }
}