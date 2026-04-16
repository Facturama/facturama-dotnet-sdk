using Facturama;
using Facturama.Models;
using Facturama.Models.Complements;
using Facturama.Models.Complements.HidroYPetro;
using Facturama.Models.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiExamples
{
    public class Hidrocarburos
    {

        private readonly FacturamaApi facturama;
        public Hidrocarburos(FacturamaApi facturama)
        {
            this.facturama = facturama; 
        }

        public void Run()
        {
            try
            {
                TestCfdiHidroYPetro(facturama);
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
        }

        public void TestCfdiHidroYPetro(FacturamaApi facturama)
        {
            var itemComplement = new ItemComplement
            {
                HidroYPetro = new HidroYPetro
                {
                    Version = "1.0",
                    TipoPermiso = "PER03",
                    NumeroPermiso = "PL/364/DIS/OM/2015",
                    ClaveHYP = "15101505",
                    SubProductoHYP = "SP22"
                }
            };
            var cfdi = new Cfdi
            {
                NameId = "1",
                CfdiType = CfdiType.Ingreso,
                PaymentForm = "01",
                PaymentMethod = "PUE",
                ExpeditionPlace = "78000",
                Currency = "MXN",
                Date = null,
                Observations = "Prueba desde SDK",
                Receiver = new Receiver
                {
                    Rfc = "URE180429TM6",
                    Name = "UNIVERSIDAD ROBOTICA ESPAÑOLA",
                    CfdiUse = "G03",
                    FiscalRegime = "601",
                    TaxZipCode = "86991"
                },
                Items = new List<Item>
                {
                    new Item
                    {
                        ProductCode = "15101505",
                        UnitCode = "LTR",
                        Unit = "NO APLICA",
                        Description = "Combustible",
                        IdentificationNumber = null,
                        Quantity = 1.0m,
                        Discount = 0.0m,
                        UnitPrice = 100.0m,
                        Subtotal = 100.0m,
                        TaxObject="02",
                        Taxes=new List<Facturama.Models.Request.Tax>
                        {
                            new Facturama.Models.Request.Tax
                            {
                                Name = "IVA",
                                Rate = 0.16m,
                                Total = 16.0m,
                                Base = 100.00m,
                                IsRetention = false
                            }

                        },
                        Total=116.00m,
                        Complement = itemComplement,


                    }
                }

            };
            


            Console.WriteLine(JsonConvert.SerializeObject(cfdi));
            var response = facturama.Cfdis.Create3(cfdi);
            Console.WriteLine(response.Id);



        }
    }
}
