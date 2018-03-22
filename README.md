# facturama-dotnet-sdk

Libreria para consumir la API Web de Facturama.

Ejemplo de uso
```cs
var cfdi = new Cfdi
{
    Serie = "R",
    Currency = "MXN",
    ExpeditionPlace = "78116",
    PaymentConditions = "CREDITO A SIETE DIAS",
    CfdiType = CfdiType.Ingreso,
    PaymentForm = "03",
    PaymentMethod = "PUE",
    Receiver = new Receiver
    {
        Rfc = "RSS2202108U5",
        Name = "RADIAL SOFTWARE SOLUTIONS",
        CfdiUse = "P01"
    },
    Items = new List<Item>
    {
        new Item
        {
            ProductCode = "10101504",
            IdentificationNumber = "EDL",
            Description = "Estudios de viabilidad",
            Unit = "NO APLICA",
            UnitCode = "MTS",
            UnitPrice = 50.00m,
            Quantity = 2.00m,
            Subtotal = 100.00m,
            Taxes = new List<Tax>
            {
                new Tax
                {

                    Total = 16.00m,
                    Name = "IVA",
                    Base = 100.00m,
                    Rate = 0.160000m,
                    IsRetention = false
                }
            },
            Total = 116.0m
        }
    }
};

var cfdiCreated = facturama.Cfdis.Create(cfdi);

facturama.Cfdis.SavePdf($"factura.pdf", cfdiCreated.Id);
facturama.Cfdis.SaveXml($"factura.xml", cfdiCreated.Id);
```
