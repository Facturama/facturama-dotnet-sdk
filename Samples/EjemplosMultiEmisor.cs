using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturama;
using Facturama.Models;
using Facturama.Models.Complements;
using Facturama.Models.Request;
using Tax = Facturama.Models.Request.Tax;

namespace Samples
{
    public class EjemplosMultiEmisor
    {
        public static void RunExamples()
        {
            var facturamaMultiEmisor = new FacturamaApiMultiemisor("pruebas", "pruebas2011");
            //TestListCreateAndRemoveCsd(facturamaMultiEmisor);
            //TestCreateCfdiMultiemisor(facturamaMultiEmisor);
            TestCreatePaymentCfdi(facturamaMultiEmisor);
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
                Issuer = new Issuer
                {
                    Rfc = "AAA010101AAA",
                    Name = "Expresion en Software SAPI de CV",
                    FiscalRegime = regimen.Value
                },
                Receiver = new Receiver
                {
                    Rfc = "JAR1106038RA",
                    Name = "SinDelantal Mexico",
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
        }

        private static void TestCreateCfdiMultiemisor(FacturamaApiMultiemisor facturama)
        {
            var nameId = facturama.Catalogs.NameIds.ElementAt(1); //Nombre en el pdf: "Factura"
            var currency = facturama.Catalogs.Currencies.First(m => m.Value == "MXN");
            var paymentMethod = facturama.Catalogs.PaymentMethods.First(p => p.Name == "Pago en una sola exhibición");
            var paymentForm = facturama.Catalogs.PaymentForms.First(p => p.Name == "Efectivo");
            var cfdiUse = facturama.Catalogs.CfdiUses("AAA010101AAA").First();
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
                Date = DateTime.Now,
                ExpeditionPlace = "78180",
                Items = new List<Item>(),
                Issuer = new Issuer
                {
                    FiscalRegime = regimen.Value,
                    Name = "Emisor de Ejemplo",
                    Rfc = "AAA010101AAA"
                },
                Receiver = new Receiver
                {
                    CfdiUse = cfdiUse.Value,
                    Name = "Receptor de Ejemplo",
                    Rfc = "ESO1202108R2"
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

                var list = facturama.Cfdis.List("Emisor de Ejemplo");
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
        }

        private static void TestListCreateAndRemoveCsd(FacturamaApiMultiemisor facturama)
        {
            // Archivo a Base64 convertido en http://jpillora.com/base64-encoder/
            var csds = facturama.Csds.List();
            Console.WriteLine($"Se encontraron {csds.Count} csd.");
            var csd = csds.FirstOrDefault(c => c.Rfc == "AAA010101AAA");
            if (csd != null)
            {
                facturama.Csds.Remove(csd.Rfc);
                Console.WriteLine($"Se eliminó el CSD relacionado con el RFC: {csd.Rfc}");
            }
            var csdRequest = new Csd
            {
                Rfc = "aaa010101aaa",
                Certificate = "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",
                PrivateKey = "MIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIAgEAAoIBAQACAggAMBQGCCqGSIb3DQMHBAgwggS+AgEAMASCBMh4EHl7aNSCaMDA1VlRoXCZ5UUmqErAbuck7ujDnmKxSaOGzJzn1hAlfBWJNtr1rgiCXRHB5/2qJ/CnTOkCcgutvs1xl3vxHgY1+N9I60iZUG+yjfEd+ungL4alXXMtKgZ8CkQXaeYIeQXFdyZ5jUU07Cy+LjMrIOAh1m/VnL6U/qW3dY+oJmII6gCG0SKcfCojeCpBVL2ispK2CBTpMDO4hd7vnbFhafl9/wUkAncmz5SHLjXPMKgmK7HvBiUSMRYFCjcNEBvMshI7E1//nG8pi0Xrmbq4MfT1B+SF8vbA39hCqKP32m+QFlOduHlaFSnW96UkMBT5hF1qImwU3HTbtKfAumo3BLzYJ9XP7Y6eVOFFSSsXudrAt94mH7CojUjazGHBsqagsUY85Q7Cz0TTvnnvWFNFAj/xbQm6nT1VL8FkdJm8hEb5YLaOqQZ8y1AEv8sCq/M51aHglexuzGFIIUTF+/XQGeYDBITlS6z2TryoHp8n1+6LpClL51WrIfaSxyMEtG2fmAHN82iNujOP6MBR7aMZ6dfxJctFRAaWlmi89wa5VhyeaoDzkx1roJznF3MLxVKROmYLDYk142IwRtTgWrex4Wnidpo4unrfL+uj6VwTUDk0cizaYvamRhlZ/LXdwyB1syb/GQGu94gSzB1zAzb5/IIbtyofK+/tVTv08OMpCqHfBye1QJQg+vxQHMkbhZH6sEgORSEjuidW13DTKi8xyryQsD5WccMh8WDxMuAVFUldrwWdGilFKg0G99S/XJWLwKB74Nv0v/Ygdp6/d9T0fFD3FXpb9RznErCgfVSMtrPv1svGg3QFwh+qmkzjh+NBwUrmmqEjNshji+9SB4fnJNYlKVvu4WAzMKliixUkRcCID1QYwLtiyWuwZDYxKTnk7Y1LXmRGqqhNbh4kdnTNUdkxEjqp+UVtBdaxswa6s4qrLbNeD7VN+1KJEMN6/zZ6+2Uj2KBMzaDA0zwAHMB1gyPkgX0v47e61iffaVAUQzDGYGbDERG2vT8234NSDdgqzOpsf7il2Pv+uF0oab+db62JiRvOEjNefXG5p8KRudYyaVO8N7iTdRRj/A/yDwjmSq9dDCZEZE0cD6BEaAgmjvqwF5IvGgJGnWYKhrOGBPv+VL6zGOXo/L9zenxYwKNTHzNYlvug/t4gXQmArroqA2YKBGpYb8/FY/q3t3k+u1bXWvNLOzWi81InvxFSCTu6l9GBCthyWwekWdoL6ssSzOmzPr/d2klSRST3ByJmAJzLGJFsj6AL01BaUVWERH0s+GmnSWOU8ZIQVGF7aOEWWbtD0vyjJRxQnxPxn+Tt3oT9Nob10QGwG/2tNZtZuhAMf1yt+cF8jl0hC/LI0FtMqmLAkxaEOiXHmFuKXbAjFxIjdIwgWsAZe1cTLzR44jIKwlB64jvh1LXmJ5jCszLd/fuCEB0XZUWLDRCZVb82MqcZl7U/gaFazSqm71NNafCDzWjWO4ukWN1lcTDJE05KgeRqoYIEcpU8jXy/CAEaoseA1bWDnfnLJk2axVXzmrtYnojyKjTjDz3In41Kjsx3nNOegqtH7O2gl9YBzJfgwZmF0ldk+udcotc0JwIXYWk7b5HmRgXWa+WvDHSwyLzMrbw=",
                PrivateKeyPassword = "12345678a"
            };
            try
            {
                facturama.Csds.Create(csdRequest);
                Console.WriteLine($"Se guardo el CSD relacionado con el RFC: {csdRequest.Rfc}");

                csdRequest.Rfc = "AAA010101AAA";
                facturama.Csds.Update(csdRequest);
                Console.WriteLine($"Se actualizó el CSD relacionado con el RFC: {csdRequest.Rfc}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
