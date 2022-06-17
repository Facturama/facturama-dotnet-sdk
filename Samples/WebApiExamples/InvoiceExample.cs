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
            

            try
            {
                //TestCFDI33(facturama);
                TestCFDI40(facturama);
                //TestCFDI40FacturaGlobal(facturama);
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

        public void TestCFDI33(FacturamaApi facturama)
        {
            Console.WriteLine("----- Inicio del ejemplo CFDI 3.3 -----");

            var cfdi33 = new Cfdi
            {
                NameId = "1",
                CfdiType = CfdiType.Ingreso,
                PaymentForm = "01",
                PaymentMethod = "PUE",
                ExpeditionPlace = "78140",
                Currency = "MXN",
                Date = null,                                    // Al especificar null, Facturama asigna la fecha y hora actual, de acuerdo al "ExpeditionPlace"
                Receiver = new Receiver
                {
                    Rfc = "CACX7605101P8",
                    Name = "XOCHILT CASAS CHAVEZ",
                    CfdiUse = "G03",           
                    /*
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
                    }*/
                },
            
                Items = new List<Item>
                {
                    new Item
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
                        Total=116.00m,
                        
                    }
                }
  
             };
            var cfdiCreated = facturama.Cfdis.Create(cfdi33);
            Console.WriteLine($"Se creo exitosamente el CFDI 3.3 con ID: {cfdiCreated.Id} y folío fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");

        
            //Descargar PDF y XML
            //facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);
            //facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);


            //var list = facturama.Cfdis.List("Expresion en Software");
            //Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");
            //list = facturama.Cfdis.List(rfc: "EWE1709045U0"); //RFC receptor en especifico
            //Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");


            //Enviar CFDI por correo
            /*
            if (facturama.Cfdis.SendByMail(cfdiCreated.Id, "ejemplo@facturama.mx"))
            {
                Console.WriteLine("Se envió correctamente el CFDI");
            }                
            */

            /*
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
            */

        }


        public void TestCFDI40(FacturamaApi facturama)
        {
            Console.WriteLine("----- Inicio del ejemplo CFDI 4.0 -----");


            var cfdi = new Cfdi
            {
                NameId = "1",
                CfdiType = CfdiType.Ingreso,
                PaymentForm = "01",
                PaymentMethod = "PUE",
                ExpeditionPlace = "78140",
                Currency = "MXN",
                Date = null,
                Receiver = new Receiver
                {
                    Rfc = "URE180429TM6",
                    Name = "UNIVERSIDAD ROBOTICA ESPAÑOLA",
                    CfdiUse = "G03",
                    FiscalRegime = "601",
                    TaxZipCode = "65000",
                    /*
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
                    }*/
                },
                Items = new List<Item>
                {
                    new Item
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
                        Total=116.00m,

                    }
                }

            };
            var cfdiCreated = facturama.Cfdis.Create3(cfdi); // Probar CFDI 4.0
            Console.WriteLine($"Se creo exitosamente el CFDI 4.0 con ID: {cfdiCreated.Id} y folío fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");

                //Descargar PDF y XML
                //facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);
                //facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);


            //var list = facturama.Cfdis.List("Expresion en Software");
            //Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");
            //list = facturama.Cfdis.List(rfc: "EWE1709045U0"); //RFC receptor en especifico
            //Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");


            //Enviar CFDI por correo
            /*
            if (facturama.Cfdis.SendByMail(cfdiCreated.Id, "rafael@facturama.mx"))
            {
                Console.WriteLine("Se envió correctamente el CFDI");
            }                
            */

            /*
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
            */

        }

        public void TestCFDI40FacturaGlobal(FacturamaApi facturama)
        {
            Console.WriteLine("----- Inicio del ejemplo CFDI 4.0 -----");

            var cfdi = new Cfdi
            {
                NameId = "1",
                CfdiType = CfdiType.Ingreso,
                PaymentForm = "01",
                PaymentMethod = "PUE",
                ExpeditionPlace = "78140",
                Currency = "MXN",
                Date = null,

                Exportation = "01",

                GlobalInformation = new GlobalInformation
                {
                    Periodicity = "04",
                    Months = "04",
                    Year = "2022",
                },

                Receiver = new Receiver
                {
                    Rfc = "XAXX010101000",
                    Name = "PUBLICO EN GENERAL",
                    CfdiUse = "S01",
                    FiscalRegime = "616",
                    TaxZipCode = "78140",
                    /*
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
                    }*/
                },

                Items = new List<Item>
                {
                    new Item
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
                        Total=116.00m,

                    }
                }
            };

            var cfdiCreated = facturama.Cfdis.Create3(cfdi); // Probar CFDI 4.0 Factura Global
            Console.WriteLine($"Se creo exitosamente el CFDI 4.0 Factura Global con ID: {cfdiCreated.Id} y folío fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");

            //Descargar PDF y XML
            //facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);
            //facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);


            //var list = facturama.Cfdis.List("Expresion en Software");
            //Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");
            //list = facturama.Cfdis.List(rfc: "EWE1709045U0"); //RFC receptor en especifico
            //Console.WriteLine($"Se encontraron: {list.Length} elementos en la busqueda");


            //Enviar CFDI por correo
            /*
            if (facturama.Cfdis.SendByMail(cfdiCreated.Id, "rafael@facturama.mx"))
            {
                Console.WriteLine("Se envió correctamente el CFDI");
            }                
            */

            /*
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
            */

        }
    }
}
