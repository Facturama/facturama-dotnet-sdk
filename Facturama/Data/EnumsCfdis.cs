namespace Facturama.Data
{
    public enum FileFormat
    {
        Xml, Pdf, Html
    }

    public enum InvoiceType
    {
        Issued, Received, Payroll
    }

    public enum CfdiStatus
    {
        all, Active, Canceled
    }
    public enum Method
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
