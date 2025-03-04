using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturama;
using Facturama.Models.Complements.ForeignTrade;
using Facturama.Models.Request;

namespace MultiIssuerExamples
{
    /*
     * Ejemplo de creacion de CFDI con comercio exterior en API Multiemisor
     * 
     * */
    public class ForeignTradeExampleAPILite
    {
        private readonly FacturamaApiMultiemisor facturama;

        public ForeignTradeExampleAPILite(FacturamaApiMultiemisor facturama)
        {
            this.facturama = facturama;
        }

        public void Run()
        {
            try
            {
                TestForeignTrade(facturama);
            }
            catch (FacturamaException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Model.Details != null)
                {
                    foreach (var messageDetail in ex.Model.Details)
                    {
                        Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: ", ex.Message);

            }
            Console.WriteLine("----- Fin del ejemplo InvoiceExample -----");
        }

        public void TestForeignTrade(FacturamaApiMultiemisor facturama)
        {
            Console.WriteLine("----- Inicio del ejemplo CFDI con comercio exterior -----");

            var cfdi = new CfdiMulti
            {
                NameId = "26",
                CfdiType = CfdiType.Ingreso,
                Folio = "1",
                Serie = "FAC",
                Currency = "MXN",
                PaymentForm = "03",
                PaymentMethod = "PUE",
                OrderNumber = "123-A",
                ExpeditionPlace = "78000",
                Date = DateTime.Now,
                PaymentConditions = "Comercio Exterior",
                Observations = "Sample CFDI",
                Exportation = "02",

                Issuer = new Facturama.Models.Request.Issuer
                {
                    Rfc= "EKU9003173C9",
                    Name = "ESCUELA KEMPER URGATE",
                    FiscalRegime = "601"
                },
                Receiver = new Facturama.Models.Request.Receiver
                {
                    Rfc = "XEXX010101000",
                    CfdiUse = "S01",
                    Name = "Nombre cliente extrangero",
                    FiscalRegime = "616",
                    TaxZipCode = "78000",
                    TaxResidence = "USA",
                    TaxRegistrationNumber = "123456789"
                },
                Items = new List<Item>
                {
                    new Item
                    {
                        ProductCode = "41106300",
                        IdentificationNumber = "CX-000988",
                        Description = "Laptop",
                        Unit = "PZA",
                        UnitCode = "EA",
                        UnitPrice = 100,
                        Quantity = 1,
                        Discount = 0,
                        Subtotal = 100,
                        TaxObject = "02",
                        Taxes = new List<Tax>
                        {
                            new Tax
                            {
                                Total = 16,
                                Name = "IVA",
                                Base = 100,
                                Rate = 0.16M,
                                IsRetention = false
                            }
                        },
                        Total = 116
                    },
                    new Item
                    {
                        ProductCode = "41106300",
                        IdentificationNumber = "CX-000988",
                        Description = "Laptop",
                        Unit = "PZA",
                        UnitCode = "EA",
                        UnitPrice = 100,
                        Quantity = 1,
                        Discount = 0,
                        Subtotal = 100,
                        TaxObject = "02",
                        Taxes = new List<Tax>
                        {
                            new Tax
                            {
                                Total = 16,
                                Name = "IVA",
                                Base = 100,
                                Rate = 0.16M,
                                IsRetention = false
                            }
                        },
                        Total = 116
                    }
                }
            };
            cfdi.Complement = new Facturama.Models.Complement
            {
                ForeignTrade = new ForeignTrade
                {
                    Issuer = new Facturama.Models.Complements.ForeignTrade.Issuer
                    {
                        Address = new Address
                        {
                            Street = "Cañada de Gomez",
                            ExteriorNumber = "123",
                            InteriorNumber = "B",
                            Neighborhood = "0001",
                            Locality = "Guadalajara",
                            Municipality = "028",
                            State = "SLP",
                            Country = "MEX",
                            ZipCode = "78000"
                        }

                    },
                    Receiver = new Facturama.Models.Complements.ForeignTrade.Receiver
                    {
                        Address = new Address
                        {
                            Street = "Cañada de Gomez",
                            ExteriorNumber = "123",
                            InteriorNumber = "B",
                            Neighborhood = "0001",
                            Locality = "Guadalajara",
                            Municipality = "028",
                            State = "NT",
                            Country = "CAN",
                            ZipCode = "M3C 0C1"
                        }
                    },
                    Commodity = new List<Commodity>
                    {
                        new Commodity
                        {
                            SpecificDescriptions = new List<SpecificDescriptions>
                            {
                                new SpecificDescriptions
                                {
                                    Brand = "HP",
                                    Model = "Pavilion",
                                    SubModel = "TX2000",
                                    SerialNumber = "SN123456",
                                }
                            },
                            IdentificationNumber = "CX-000988",
                            TariffFraction = "7308900200",
                            CustomsQuantity = "1",
                            CustomsUnit = "01",
                            CustomsUnitValue = 10.60M,

                        }
                    },
                    RequestCode = "A1",
                    OriginCertificate = true,
                    Incoterm = "CFR",
                    ExchangeRateUSD = 20.5080M,
                    TotalUSD = 1.0M,
                    OriginCertificateNumber = "20001000000300022817",
                    ReliableExporterNumber = null,
                    Observations = "sample string 8"
                }
            };
            var cfdiCreated = facturama.Cfdis.Create3(cfdi);
            Console.WriteLine($"CFDI creado con éxito con ID: {cfdiCreated.Id}");

        }
    }
}
