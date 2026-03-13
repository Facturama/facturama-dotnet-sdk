using Facturama;
using Facturama.Models.Complements.NotariosPublicos;
using Facturama.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiExamples
{
    public class NotariosPublicosExample
    {
        private readonly FacturamaApi facturama;

        public NotariosPublicosExample(FacturamaApi facturama)
        {
            this.facturama = facturama;
        }

        public void Run()
        {
            try
            {
                TestNotariosPublicos(facturama);
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

        public void TestNotariosPublicos(FacturamaApi facturama)
        {
            Console.WriteLine("----- Inicio del ejemplo Notarios Publicos -----");

            var cfdi = new Cfdi
            {
                NameId = "1",
                CfdiType = CfdiType.Ingreso,
                PaymentForm = "01",
                PaymentMethod = "PUE",
                ExpeditionPlace = "78000",
                Currency = "MXN",
                Date = null,
                Receiver = new Receiver
                {
                    Rfc = "URE180429TM6",
                    Name = "UNIVERSIDAD ROBOTICA ESPAÑOLA",
                    CfdiUse = "G03",
                    FiscalRegime = "601",
                    TaxZipCode = "86991",
                },
                Items = new List<Facturama.Models.Request.Item>
                {
                    new Facturama.Models.Request.Item
                    {
                        ProductCode = "10101504",
                        UnitCode = "MTS",
                        Unit = "NO APLICA",
                        Description = "Estudios de laboratorio",
                        IdentificationNumber = null,
                        Quantity = 2.0m,
                        Discount = 0.0m,
                        UnitPrice = 50.0m,
                        Subtotal = 100.0m,
                        TaxObject="02",
                        Taxes=new List<Tax>
                        {
                            new Tax
                            {
                                Name = "IVA",
                                Rate = 0.16m,
                                Total = 16.0m,
                                Base = 100.00m,
                                IsRetention = false
                            }

                        },
                        Total=116.00m
                    }
                }

            };
            cfdi.Complement = new Facturama.Models.Complement()
            {
                NotariosPublicos = new NotariosPublicos
                {
                    Version = "1.0",
                    DatosOperacion = new DatosOperacion
                    {
                        NumInstrumentoNotarial = "123456",
                        FechaInstNotarial = "14/05/2022",
                        MontoOperacion = "100.00",
                        Subtotal = "100.00",
                        IVA = "1"
                    },
                    DatosNotario = new DatosNotario
                    {
                        CURP = "XEXX010101HNEXXXA4",
                        NumNotaria = "12",
                        EntidadFederativa = "24",
                        Adscripcion = "",
                    },
                    DescInmuebles = new List<DescInmuebles>
                    {
                        new DescInmuebles
                        {
                            TipoInmueble = "01",
                            Calle = "Calle Falsa 123",
                            Municipio = "Springfield",
                            Estado = "24",
                            Pais = "MEX",
                            CodigoPostal = "78000"
                        }
                    },
                    DatosEnajenante = new DatosEnajenante
                    {
                        CoproSocConyugalE = "No",
                        Item = new Facturama.Models.Complements.NotariosPublicos.Item
                        {
                            Nombre = "Juan",
                            ApellidoPaterno = "Perez",
                            RFC = "KICR630120NX3",
                            CURP = "XEXX010101HNEXXXA4"
                        }
                    },
                    DatosAdquiriente = new DatosAdquiriente
                    {
                        CoproSocConyugalE = "No",
                        Item = new Facturama.Models.Complements.NotariosPublicos.Item
                        {
                            Nombre = "Maria",
                            ApellidoPaterno = "Lopez",
                            RFC = "LOPM8001019Q8",
                            CURP = "XEXX010101HNEXXXA4"
                        }
                    }
                }

            };

            var request = facturama.Cfdis.Create3(cfdi);


            Console.WriteLine($"Total de notarios publicos: {request.Id}");

            Console.WriteLine("----- Fin del ejemplo Notarios Publicos -----");
        }
    }
}
