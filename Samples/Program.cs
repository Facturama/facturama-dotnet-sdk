using System;
using System.Collections.Generic;
using System.Linq;
using Facturama;
using Facturama.Models;
using Facturama.Models.Request;
using Facturama.Models.Response;
using Cfdi = Facturama.Models.Request.Cfdi;
using Item = Facturama.Models.Request.Item;
using Receiver = Facturama.Models.Request.Receiver;
using Tax = Facturama.Models.Request.Tax;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var facturama = new FacturamaApi("pruebas", "pruebas2011");

            TestCrudClient(facturama);
            //TestValidationsClient(facturama);
            //TestCreateProduct(facturama);
            //TestCreateCfdi(facturama);
        }

        private static void TestValidationsClient(FacturamaApi facturama)
        {
            try
            {
                var cliente = facturama.Clients.Create(new Client
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
                        ZipCode = "7822015"
                    },
                    CfdiUse = "P0001",
                    Email = "diego@@facturama.com.mx",
                    Rfc = "ESO1202108R245",
                    Name = "Expresion en Software"
                });
            }
            catch (FacturamaException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var messageDetail in ex.Model.Details)
                {
                    Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
                }
            }
            Console.ReadKey();
        }

        private static void TestCrudClient(FacturamaApi facturama)
        {
            var clientes = facturama.Clients.List();
            var clientesBefore = clientes.Count;

            var cliente = facturama.Clients.Create(new Client
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
            });

            cliente = facturama.Clients.Retrieve(cliente.Id);
            cliente.Rfc = "XAXX010101000";
            facturama.Clients.Update(cliente, cliente.Id);
            cliente = facturama.Clients.Retrieve(cliente.Id);

            Console.WriteLine(cliente.Rfc == "XAXX010101000" ? "Cliente Editado" : "Error al editar cliente");

            facturama.Clients.Remove(cliente.Id);

            clientes = facturama.Clients.List();
            var clientesAfter = clientes.Count;

            Console.WriteLine(clientesAfter == clientesBefore ? "Test Passed!" : "Test Failed!");
        }

        private static void TestCreateProduct(FacturamaApi facturama)
        {
            var unit = facturama.Catalogs.Units("servicio")[0];
            var prod = facturama.Catalogs.ProductsOrServices("desarrollo")[0];
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

            try
            {
                product = facturama.Products.Create(product);
                Console.WriteLine("Se creo exitosamente un producto con el id: " + product.Id);

                facturama.Products.Remove(product.Id);
                Console.WriteLine("Se elimino exitosamente un producto con el id: " + product.Id);
            }
            catch (FacturamaException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var messageDetail in ex.Model.Details)
                {
                    Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
                }
            }
        }

        private static void TestCreateCfdi(FacturamaApi facturama)
        {
            var products = facturama.Products.List().Where(p => p.Taxes.Any()).ToList();

            var nameId = facturama.Catalogs.NameIds.ElementAt(1); //Nombre en el pdf: "Factura"
            var currency = facturama.Catalogs.Currencies.First(m => m.Value == "MXN");
            var paymentMethod = facturama.Catalogs.PaymentMethods.First(p => p.Name == "Pago en una sola exhibición");
            var paymentForm = facturama.Catalogs.PaymentForms.First(p => p.Name == "Efectivo");
            var cliente = facturama.Clients.List().First(c => c.Rfc == "XAXX010101000");

            var branchOffice = facturama.BranchOffices.List().First();
            var random = new Random();
            var nitems = random.Next(1, products.Count) % 10; // Cantidad de items para la factura
            var decimals = (int)currency.Decimals;

            var cfdi = new Cfdi
            {
                NameId = nameId.Value,
                CfdiType = CfdiType.Ingreso,
                PaymentForm = paymentForm.Value,
                PaymentMethod = paymentMethod.Value,
                Currency = currency.Value,
                Date = DateTime.Now,
                ExpeditionPlace = branchOffice.Address.ZipCode,
                Items = new List<Item>(),
                Receiver = new Receiver
                {
                    CfdiUse = cliente.CfdiUse,
                    Name = cliente.Name,
                    Rfc = cliente.Rfc
                },
            };
            for (var i = products.Count - nitems; i < products.Count && i > 0; i++)
            {
                var product = products[i];
                var quantity = random.Next(1, 5); //Una cantidad aleatoria
                var discount = product.Price % (product.Price == 0 ? 1 : random.Next(0, (int)product.Price)); //Un descuento aleatorio
                var subtotal = Math.Round(product.Price * quantity, decimals);

                var item = new Item
                {
                    ProductCode = product.CodeProdServ,
                    UnitCode = product.UnitCode,
                    Unit = product.Unit,
                    Description = product.Description,
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
                list = facturama.Cfdis.List(rfc: "ESO1202108R2"); //Atributo en especifico
                Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");

                facturama.Cfdis.Remove(cfdiCreated.Id);
                Console.WriteLine(
                    $"Se eliminó exitosamente el cfdi con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");

            }
            catch (FacturamaException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var messageDetail in ex.Model.Details)
                {
                    Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: ", ex.Message);
            }
            Console.ReadKey();

        }
    }
}