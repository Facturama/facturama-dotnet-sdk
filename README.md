
# Facturama-SDK
Libreria para consumir la API Web de Facturama.

## Compatibilidad
* .Net Framework 4.5 ó superior

## Dependecias
* [Newtonsoft.Json](http://james.newtonking.com/json)
* [RestSharp](http://restsharp.org/)
* [System.ValueTuple](https://www.nuget.org/packages/System.ValueTuple/)

## Inicio Rapido

#### Instalación #####

Es recomendable utilizar [NuGet](http://docs.nuget.org) para instalar la librería. ó puedes hacer fork y modificar a tu conveniencia.
```net
Install-Package Facturama -Version 1.0.0.1
```

#### Configuración  #####

Al no recibir otro parametro aparte de usuario y contraseña la libreria esta en modo sandbox
```cs
var facturama = new FacturamaApi("usuario", "contraseña");
```
y especificando la propiedad isDevelopment en false esta en modo producción
```cs
var facturama = new FacturamaApi("usuario", "contraseña", isDevelopment: false);
```

## CFDI 3.3
Creacion de CFDI 3.3
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
Consulta tus facturas en cualquier momento mediante una palabra clave ó algun atributo en específico
```.cs
facturama.Cfdis.List("Expresion en Software");
facturama.Cfdis.List(rfc: "ESO1202108R2");
```
Envia tu CFDI por Email
```.cs
facturama.Cfdis.SendByMail(cfdiCreated.Id, "diego@facturama.com.mx")
```

## Otras Operaciones
* Consultar Perfil y Suscripción actual,
* Carga de Logo y Certificados Digitales
* CRUD de Productos, Clientes, Sucursales y Series.
