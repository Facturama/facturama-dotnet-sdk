using System;
using System.Collections.Generic;
using System.Linq;
using Facturama;
using Facturama.Models;
using Facturama.Models.Request;
using Facturama.Models.Complements.Payroll;
using PayrollIssuer = Facturama.Models.Complements.Payroll.Issuer;

namespace WebApiExamples
{
    /**
    * Ejemplo de creación de un CFDI "complemento de pago"
    * Referencia: https://apisandbox.facturama.mx/guias/api-web/cfdi/factura
    * 
    * En virtud de que el complemento de pago, requiere ser asociado a un CFDI con el campo "PaymentMethod" = "PPD"
    * En este ejemplo se incluye la creacón de este CFDI, para posteriormente realizar el  "Complemento de pago" = "PUE"     
    */
    class InvoiceExample
	{
        private readonly FacturamaApi facturama;
        public InvoiceExample(FacturamaApi facturama)
        {
             this.facturama = facturama;
        }

        public void Run()
        {
            Console.WriteLine("----- Inicio del ejemplo InvoiceExample -----");

            var products = facturama.Products.List().Where(p => p.Taxes.Any()).ToList();

            var nameId = facturama.Catalogs.NameIds.ElementAt(0);   //Nombre en el pdf: "Factura"
            var currency = facturama.Catalogs.Currencies.First(m => m.Value == "MXN");
            var paymentMethod = facturama.Catalogs.PaymentMethods.First(p => p.Name == "Pago en una sola exhibición");
            var paymentForm = facturama.Catalogs.PaymentForms.First(p => p.Name == "Efectivo");
            var cliente = facturama.Clients.List().First(c => c.Rfc == "XAXX010101000");

            var branchOffice = facturama.BranchOffices.List().First();
            var random = new Random();
            var nitems = random.Next(1, products.Count) % 2 + 1; // Cantidad de items para la factura
            var decimals = (int)currency.Decimals;

            var cfdi = new Cfdi
            {
                NameId = nameId.Value,
                CfdiType = CfdiType.Ingreso,
                PaymentForm = paymentForm.Value,
                PaymentMethod = paymentMethod.Value,
                Currency = currency.Value,
                Date = null,                                    // Al especificar null, Facturama asigna la fecha y hora actual, de acuerdo al "ExpeditionPlace"
                ExpeditionPlace = "78240",
                Items = new List<Item>(),
                Receiver = new Receiver
                {
                    CfdiUse = "P01",
                    Name = cliente.Name,
                    Rfc = cliente.Rfc,
                    Address = new Address                       // El nodo Address es opcional (puedes colocarlo nulo o no colocarlo). En el caso de no colcoarlo, tomará la correspondiente al RFC en el catálogo de clientes
					{
                        Street = "Avenida de los pinos",
                        ExteriorNumber = "110",
                        InteriorNumber = "A",
                        Neighborhood = "Las villerías",
                        ZipCode = "78000",
                        Municipality = "San Luis Potosí",
                        State = "San Luis Potosí",
                        Country = "México"
					}
                },
            };
            for (var i = products.Count - nitems; i < products.Count && i > 0; i++)
            {
                var product = products[i];
                var quantity = random.Next(1, 5); //Una cantidad aleatoria
                var discount = product.Price % (product.Price == 0 ? 1 : random.Next(1, (int)product.Price)); //Un descuento aleatorio
                var subtotal = Math.Round(product.Price * quantity, decimals);

                var item = new Item
                {
                    ProductCode = product.CodeProdServ,
                    UnitCode = product.UnitCode,
                    Unit = product.Unit,
                    Description = string.IsNullOrEmpty(product.Description) ? "Producto de ejemplo" : product.Description,
                    IdentificationNumber = product.IdentificationNumber,
                    Quantity = quantity,
                    Discount = Math.Round(discount, decimals),
                    UnitPrice = Math.Round(product.Price, decimals),
                    Subtotal = subtotal,
                    Taxes = product.Taxes?.Select(
                        t =>
                        {
                            var baseAmount = Math.Round(subtotal - discount, decimals);
                            return new Tax
                            {
                                Name = t.Name,
                                IsQuota = t.IsQuota,
                                IsRetention = t.IsRetention,

                                Rate = Math.Round(t.Rate, 6),
                                Base = Math.Round(subtotal - discount, decimals),
                                Total = Math.Round(baseAmount * t.Rate, decimals)
                            };
                        }).ToList()
                };
                var retenciones = item.Taxes?.Where(t => t.IsRetention).Sum(t => t.Total) ?? 0;
                var traslados = item.Taxes?.Where(t => !t.IsRetention).Sum(t => t.Total) ?? 0;
                item.Total = item.Subtotal - item.Discount + traslados - retenciones;
                cfdi.Items.Add(item);
            }

            try
            {
                var cfdiCreated = facturama.Cfdis.Create(cfdi);
                Console.WriteLine(
                    $"Se creó exitosamente el cfdi con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");
                facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);
                facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);

                var list = facturama.Cfdis.List("Expresion en Software");
                Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");
                list = facturama.Cfdis.List(rfc: "EWE1709045U0"); //RFC receptor en especifico
                Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");

                if (facturama.Cfdis.SendByMail(cfdiCreated.Id, "chucho@facturama.mx"))
                {
                    Console.WriteLine("Se envió correctamente el CFDI");
                }                


                var cancelationStatus = facturama.Cfdis.Cancel(cfdiCreated.Id);
                if (cancelationStatus.Status == "canceled")
                {
                    Console.WriteLine($"Se canceló exitosamente el CFDI con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");
                }
                else if (cancelationStatus.Status == "pending")
                {
                    Console.WriteLine($"El CFDI está en proceso de cancelacion, require aprobacion por parte del receptor UUID: {cfdiCreated.Complement.TaxStamp.Uuid}");
                }
                else if (cancelationStatus.Status == "active")
                {
                    Console.WriteLine($"El CFDI no pudo ser cancelado, se deben revisar docuementos relacionados on cancelar directo en el SAT UUID: {cfdiCreated.Complement.TaxStamp.Uuid}");
                }
                else
                {
                    Console.WriteLine($"Estado de cancelacin del CFDI desconocido UUID: {cfdiCreated.Complement.TaxStamp.Uuid}");
                }

            }
            catch (FacturamaException ex)
            {
                Console.WriteLine(ex.Message);
                if(ex.Model.Details != null)
                    foreach (var messageDetail in ex.Model.Details)
                    {
                        Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: ", ex.Message);
            }

            Console.WriteLine("----- Fin del ejemplo InvoiceExample -----");
        }


    }
}
