using System;
using System.Collections.Generic;
using System.Linq;
using Facturama;
using Facturama.Models;
using Facturama.Models.Complements;
using Facturama.Models.Request;
using Tax = Facturama.Models.Request.Tax;

namespace MultiIssuerExamples
{
    public class MultiIssuerExamples
    {
        public static void RunExamples()
        {
            var facturamaMultiEmisor = new FacturamaApiMultiemisor("pruebas", "pruebas2011");
            TestListCreateAndRemoveCsd(facturamaMultiEmisor);
            TestCreateCfdiMultiemisor(facturamaMultiEmisor);
            TestCreatePaymentCfdi(facturamaMultiEmisor);

            new EducationalInstitutionComplementExampleMultiemisor(facturamaMultiEmisor).Run();   // Complemento IEDU - Instituciones educativas

            Console.ReadKey();
        }

        private static void TestCreatePaymentCfdi(FacturamaApiMultiemisor facturama)
        {
            var nameId = facturama.Catalogs.NameIds[14]; //Nombre en el pdf: "Factura"
            var paymentForm = facturama.Catalogs.PaymentForms.First(p => p.Name == "Efectivo");
            var regimen = facturama.Catalogs.FiscalRegimens.First();


            var cfdi = new CfdiMulti
            {
                NameId = nameId.Value,
                CfdiType = CfdiType.Pago,
                Folio = "100",
                ExpeditionPlace = "78220",
                LogoUrl = "https://www.ejemplos.co/wp-content/uploads/2015/11/Logo-Chanel.jpg",

                Issuer = new Issuer
                {
                    Rfc = "EKU9003173C9",
                    Name = "Kemper Software SAPI de CV",
                    FiscalRegime = regimen.Value
                },
                Receiver = new Receiver
                {
                    Rfc = "EWE1709045U0",
                    Name = "Escuela Wilson Esquivel",
                    CfdiUse = "P01"
                },
                Complement = new Complement
                {
                    Payments = new List<Payment>
                    {
                        new Payment
                        {
                            Date = "2018-04-04T00:00:00.000Z",
                            PaymentForm = paymentForm.Value,
                            Currency = "MXN",
                            Amount = 1200.00m,
                            RelatedDocuments = new List<RelatedDocument> {
                                new RelatedDocument {
                                    Uuid = "F884C787-EEA6-4720-874D-B5048DB8F960",
                                    Folio = "100032007",
                                    Currency = "MXN",
                                    PaymentMethod = "PUE",
                                    PartialityNumber = 1,
                                    PreviousBalanceAmount = 1200.00m,
                                    AmountPaid = 1200.00m
                                }
                            }
                        }
                    }
                }
            };
            try
            {
                var cfdiCreated = facturama.Cfdis.Create(cfdi);
                Console.WriteLine(
                    $"Se creó exitosamente el cfdi con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");
                facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);
                Console.WriteLine($"Se guardo existosamente la factura con el UUID: {cfdiCreated.Complement.TaxStamp.Uuid}.");

	            Console.WriteLine($"Se intenta cancelar la factura con el UUID: {cfdiCreated.Complement.TaxStamp.Uuid}.");
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
                foreach (var messageDetail in ex.Model.Details)
                {
                    Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: ", ex.Message);
            }
        }

        private static void TestCreateCfdiMultiemisor(FacturamaApiMultiemisor facturama)
        {
            var nameId = facturama.Catalogs.NameIds.ElementAt(1); //Nombre en el pdf: "Factura"
            var currency = facturama.Catalogs.Currencies.First(m => m.Value == "MXN");
            var paymentMethod = facturama.Catalogs.PaymentMethods.First(p => p.Name == "Pago en una sola exhibición");
            var paymentForm = facturama.Catalogs.PaymentForms.First(p => p.Name == "Efectivo");
            var cfdiUse = facturama.Catalogs.CfdiUses("EKU9003173C9").First();
            var codeProdServ = facturama.Catalogs.ProductsOrServices("desarrollo").First();
            var unitCode = facturama.Catalogs.Units("pieza").First();
            var decimals = (int)currency.Decimals;
            var regimen = facturama.Catalogs.FiscalRegimens.First();

            var cfdi = new CfdiMulti
            {
                Folio = "10",
                NameId = nameId.Value,
                CfdiType = CfdiType.Ingreso,
                PaymentForm = paymentForm.Value,
                PaymentMethod = paymentMethod.Value,
                Currency = currency.Value,
                Date = null,
                ExpeditionPlace = "78180",
                LogoUrl = "https://www.ejemplos.co/wp-content/uploads/2015/11/Logo-Chanel.jpg",
                Items = new List<Item>(),
                Issuer = new Issuer
                {
                    FiscalRegime = regimen.Value,
                    Name = "Kemper Urgate",
                    Rfc = "EKU9003173C9"
                },
                Receiver = new Receiver
                {
                    CfdiUse = cfdiUse.Value,
                    Name = "Escuela Wilson Esquivel",
                    Rfc = "EWE1709045U0"
                },
            };

            var price = 100.00m;
            var quantity = 2m;
            var discount = 10m;
            var subtotal = Math.Round(price * quantity, decimals);

            var item = new Item
            {
                ProductCode = codeProdServ.Value,
                UnitCode = unitCode.Value,
                Unit = "Libra",
                Description = "Descripción del Producto",
                IdentificationNumber = "010101-56",
                Quantity = quantity,
                Discount = Math.Round(discount, decimals),
                UnitPrice = Math.Round(price, decimals),
                Subtotal = subtotal,
                Taxes = new List<Tax>
                {
                    new Tax
                    {
                        Name = "IVA",
                        IsQuota = false,
                        IsRetention = false,

                        Rate = 0.160000m,
                        Base = Math.Round(subtotal - discount, decimals),
                        Total = Math.Round((subtotal - discount) * 0.160000m, decimals)
                    }
                }

            };
            var retenciones = item.Taxes?.Where(t => t.IsRetention).Sum(t => t.Total) ?? 0;
            var traslados = item.Taxes?.Where(t => !t.IsRetention).Sum(t => t.Total) ?? 0;
            item.Total = item.Subtotal - item.Discount + traslados - retenciones;
            cfdi.Items.Add(item);

            try
            {
                var cfdiCreated = facturama.Cfdis.Create(cfdi);
                Console.WriteLine(
                    $"Se creó exitosamente el cfdi con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");
                facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);                
                facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);                

                var list = facturama.Cfdis.List("Emisor de Ejemplo");
                Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");
                list = facturama.Cfdis.List(rfc: "EWE1709045U0"); //RFC receptor en especifico
                Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");

                var cancelationStatus = facturama.Cfdis.Cancel(cfdiCreated.Id);

				if(cancelationStatus.Status == "canceled")
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
                foreach (var messageDetail in ex.Model.Details)
                {
                    Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: ", ex.Message);
            }
        }

        private static void TestListCreateAndRemoveCsd(FacturamaApiMultiemisor facturama)
        {
            // Archivo a Base64 convertido en http://jpillora.com/base64-encoder/
            var csds = facturama.Csds.List();
            Console.WriteLine($"Se encontraron {csds.Count} csd.");
            var csd = csds.FirstOrDefault(c => c.Rfc == "EKU9003173C9");
            if (csd != null)
            {
                facturama.Csds.Remove(csd.Rfc);
                Console.WriteLine($"Se eliminó el CSD relacionado con el RFC: {csd.Rfc}");
            }
            var csdRequest = new Csd
            {
                Rfc = "EKU9003173C9",
                Certificate = "MIIFuzCCA6OgAwIBAgIUMzAwMDEwMDAwMDA0MDAwMDI0MzQwDQYJKoZIhvcNAQELBQAwggErMQ8wDQYDVQQDDAZBQyBVQVQxLjAsBgNVBAoMJVNFUlZJQ0lPIERFIEFETUlOSVNUUkFDSU9OIFRSSUJVVEFSSUExGjAYBgNVBAsMEVNBVC1JRVMgQXV0aG9yaXR5MSgwJgYJKoZIhvcNAQkBFhlvc2Nhci5tYXJ0aW5lekBzYXQuZ29iLm14MR0wGwYDVQQJDBQzcmEgY2VycmFkYSBkZSBjYWRpejEOMAwGA1UEEQwFMDYzNzAxCzAJBgNVBAYTAk1YMRkwFwYDVQQIDBBDSVVEQUQgREUgTUVYSUNPMREwDwYDVQQHDAhDT1lPQUNBTjERMA8GA1UELRMIMi41LjQuNDUxJTAjBgkqhkiG9w0BCQITFnJlc3BvbnNhYmxlOiBBQ0RNQS1TQVQwHhcNMTkwNjE3MTk0NDE0WhcNMjMwNjE3MTk0NDE0WjCB4jEnMCUGA1UEAxMeRVNDVUVMQSBLRU1QRVIgVVJHQVRFIFNBIERFIENWMScwJQYDVQQpEx5FU0NVRUxBIEtFTVBFUiBVUkdBVEUgU0EgREUgQ1YxJzAlBgNVBAoTHkVTQ1VFTEEgS0VNUEVSIFVSR0FURSBTQSBERSBDVjElMCMGA1UELRMcRUtVOTAwMzE3M0M5IC8gWElRQjg5MTExNlFFNDEeMBwGA1UEBRMVIC8gWElRQjg5MTExNk1HUk1aUjA1MR4wHAYDVQQLExVFc2N1ZWxhIEtlbXBlciBVcmdhdGUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCN0peKpgfOL75iYRv1fqq+oVYsLPVUR/GibYmGKc9InHFy5lYF6OTYjnIIvmkOdRobbGlCUxORX/tLsl8Ya9gm6Yo7hHnODRBIDup3GISFzB/96R9K/MzYQOcscMIoBDARaycnLvy7FlMvO7/rlVnsSARxZRO8Kz8Zkksj2zpeYpjZIya/369+oGqQk1cTRkHo59JvJ4Tfbk/3iIyf4H/Ini9nBe9cYWo0MnKob7DDt/vsdi5tA8mMtA953LapNyCZIDCRQQlUGNgDqY9/8F5mUvVgkcczsIgGdvf9vMQPSf3jjCiKj7j6ucxl1+FwJWmbvgNmiaUR/0q4m2rm78lFAgMBAAGjHTAbMAwGA1UdEwEB/wQCMAAwCwYDVR0PBAQDAgbAMA0GCSqGSIb3DQEBCwUAA4ICAQBcpj1TjT4jiinIujIdAlFzE6kRwYJCnDG08zSp4kSnShjxADGEXH2chehKMV0FY7c4njA5eDGdA/G2OCTPvF5rpeCZP5Dw504RZkYDl2suRz+wa1sNBVpbnBJEK0fQcN3IftBwsgNFdFhUtCyw3lus1SSJbPxjLHS6FcZZ51YSeIfcNXOAuTqdimusaXq15GrSrCOkM6n2jfj2sMJYM2HXaXJ6rGTEgYmhYdwxWtil6RfZB+fGQ/H9I9WLnl4KTZUS6C9+NLHh4FPDhSk19fpS2S/56aqgFoGAkXAYt9Fy5ECaPcULIfJ1DEbsXKyRdCv3JY89+0MNkOdaDnsemS2o5Gl08zI4iYtt3L40gAZ60NPh31kVLnYNsmvfNxYyKp+AeJtDHyW9w7ftM0Hoi+BuRmcAQSKFV3pk8j51la+jrRBrAUv8blbRcQ5BiZUwJzHFEKIwTsRGoRyEx96sNnB03n6GTwjIGz92SmLdNl95r9rkvp+2m4S6q1lPuXaFg7DGBrXWC8iyqeWE2iobdwIIuXPTMVqQb12m1dAkJVRO5NdHnP/MpqOvOgLqoZBNHGyBg4Gqm4sCJHCxA1c8Elfa2RQTCk0tAzllL4vOnI1GHkGJn65xokGsaU4B4D36xh7eWrfj4/pgWHmtoDAYa8wzSwo2GVCZOs+mtEgOQB91/g==",
                PrivateKey = "MIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIAgEAAoIBAQACAggAMBQGCCqGSIb3DQMHBAgwggS8AgEAMASCBMh4EHl7aNSCaMDA1VlRoXCZ5UUmqErAbucRFLOMmsAaFNkyWR0dXIAh0CMjE6NpQIMZhQ0HH/4tHgmwh4kCawGjIwERoG6/IH3mCt7u19J5+m6gUEGOJdEMXj976E5lKCd/EG6t6lCq66GE3rgux/nFmeQZvsjLlzPyhe2j+X81LrGudITTjDdgLI0EdbdV9CUJwWbibzrVxjuAVShRh07XPL/DiEw3Wk2+kdy4cfWmMvh0U55p0RKZopNkWuVVSvr3ai7ZNCwHZWDVqkUDpwDDGdyt0kYQ7qoKanIxv/A9wv6ekq0LQ/yLlOcelkxQeb8Glu4RXe+krRvrASw1eBAQ3mvNKpngwF8vtlyoil41PjHUOKALMJtNpywckRRYOk4703ylWIzTfdBlrZ6VmDBjdC5723G1HAx3R/x+o+08++RNiFaN06Ly5QbZZvjnealDfSKz1VKRHWeXggaW87rl4n0SOOWnvabKs4ZWRXTS0dhWK+KD/yYYQypTslDSXQrmyMkpc1Zcb4p9RTjodXxGCWdsR5i5+Ro/RiJvxWwwaO3YW6eaSavV0ROqANQ+A+GizMlxsVjl6G5Ooh6ORdA7jTNWmK44Icgyz6QFNh+J3NibxVK2GZxsQRi+N3HXeKYtq5SDXARA0BsaJQzYfDotA9LFgmFKg9jVhtcc1V3rtpaJ5sab8tdBTPPyN/XT8fA0GxlIX+hjLd3E9wB7qzNR6PZ84UKDxhCGWrLuIoSzuCbr+TD9UCJprsfTu8kr8Pur4rrxm7Zu1MsJRR9U5Ut+O9FZfw4SqGykyTGGh0v1gDG8esKpTW5MKNk9dRwDNHEmIF6tE6NeXDlzovf8VW6z9JA6AVUkgiFjDvLUY5MgyTqPB9RJNMSAZBzrkZgXyHlmFz2rvPqQGFbAtukjeRNS+nkVayLqfQnqpgthBvsgDUgFn03z0U2Svb094Q5XHMeQ4KM/nMWTEUC+8cybYhwVklJU7FBl9nzs66wkMZpViIrVWwSB2k9R1r/ZQcmeL+LR+WwgCtRs4It1rNVkxXwYHjsFM2Ce46TWhbVMF/h7Ap4lOTS15EHC8RvIBBcR2w1iJ+3pXiMeihArTELVnQsS31X3kxbBp3dGvLvW7PxDlwwdUQOXnMoimUCI/h0uPdSRULPAQHgSp9+TwqI0Uswb7cEiXnN8PySN5Tk109CYJjKqCxtuXu+oOeQV2I/0knQLd2zol+yIzNLj5a/HvyN+kOhIGi6TrFThuiVbbtnTtRM1CzKtFGuw5lYrwskkkvenoSLNY0N85QCU8ugjc3Bw4JZ9jNrDUaJ1Vb5/+1GQx/q/Dbxnl+FK6wMLjXy5JdFDeQyjBEBqndQxrs9cM5xBnl6AYs2Xymydafm2qK0cEDzwOPMpVcKU8sXS/AHvtgsn+rjMzW0wrQblWE0Ht/74GgfCj4diCDtzxQ0ggi6yJD+yhLZtVVqmKS3Gwnj9RxPLNfpgzPP01eYyBBi/W0RWTzcTb8iMxWX52MTU0oX9//4I7CAPXn0ZhpWAAIvUmkfjwfEModH7iwwaNtZFlT2rlzeshbP++UCEtqbwvveDRhmr5sMYkl+duEOca5156fcRy4tQ8Y3moNcKFKzHGMenShEIHz+W5KE=",
                PrivateKeyPassword = "12345678a"
            };
	        try
	        {
		        facturama.Csds.Create(csdRequest);
		        Console.WriteLine($"Se guardo el CSD relacionado con el RFC: {csdRequest.Rfc}");

		        csdRequest.Rfc = "EKU9003173C9";
		        facturama.Csds.Update(csdRequest);
		        Console.WriteLine($"Se actualizó el CSD relacionado con el RFC: {csdRequest.Rfc}");

	        }
	        catch (FacturamaException ex)
	        {
		        Console.WriteLine(ex.Message);
		        foreach (var messageDetail in ex.Model.Details)
		        {
			        Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
		        }
			}
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
