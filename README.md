# facturama-dotnet-sdk
Libreria para consumir la API Web de Facturama.

## Perfil
Obtiene información de la cuenta
```cs
facturama.Profile.Get();
```
## Clientes
Crear clients 
```cs
var cliente = new Client
{
    Address = new Address
    {
        Country = "MEXICO",
        ExteriorNumber = "1230",
        InteriorNumber = "B",
        Locality = "San Luis",
        Municipality = "San Luis Potosí",
        Neighborhood = "Lomas 4ta",
        State = "San Luis Potosí",
        Street = "Cañada de Gomez",
        ZipCode = "78220"
    },
    CfdiUse = "P01",
    Email = "diego@facturama.com.mx",
    Rfc = "ESO1202108R2",
    Name = "Expresion en Software"
}
facturama.Clients.Create(cliente)
```
Obtener un cliente ó varios
```.cs
facturama.Clients.Retrieve(cliente.Id);
facturama.Clients.List();
```
Actualizarlo
```.cs
facturama.Clients.Update(cliente, cliente.Id);
```
Eliminarlo
```.cs
facturama.Clients.Remove(cliente.Id);
```
## Productos
Puede consultar su catalogo de productos y efectuar las siguientes operaciones CRUD
```cs
var product = new Product
{
    Unit = "Servicio",
    UnitCode = unit.Value,
    IdentificationNumber = "WEB003",
    Name = "Sitio Web CMS",
    Description = "Desarrollo e implementación de sitio web empleando un CMS",
    Price = 6500.0m,
    CodeProdServ = prod.Value,
    CuentaPredial = "123",
    Taxes = new[]
    {
        new Tax
        {
            Name = "IVA",
            Rate = 0.16m,
            IsRetention = false,
        },
        new Tax
        {
            Name = "ISR",
            IsRetention = true,
            Total = 0.1m
        },
        new Tax
        {
            Name = "IVA",
            IsRetention = true,
            Total = 0.106667m
        }
    }
};
facturama.Products.Create(product);
```
Obtener un producto ó varios
```.cs
facturama.Products.Retrieve(product.Id);
facturama.Products.List();
```
Actualizarlo
```.cs
facturama.Products.Update(product, product.Id);
```
Eliminarlo
```.cs
facturama.Products.Remove(product.Id);
```
## CFDI 3.3
Creacion de CFDI 3.3,  y Descarga
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
```
Cancelación
```.cs
facturama.Cfdis.Remove(cfdiCreated.Id);
```
Descarga en el formato deseado xml, html ó pdf
```.cs
facturama.Cfdis.SavePdf($"factura.pdf", cfdiCreated.Id);
facturama.Cfdis.SaveXml($"factura.xml", cfdiCreated.Id);
```
