using System;
using System.Collections.Generic;
using System.Linq;
using Facturama;
using Facturama.Models;
using Facturama.Models.Request;
using Facturama.Models.Complements.EducationalInstitution;

namespace MultiIssuerExamples
{
    /**
    * Ejemplo de creación de un CFDI con el "Complemento de instituciones educativas"
    * Referencia: https://apisandbox.facturama.mx/guias/api-web/cfdi/complemento-instituciones
    * 
    * Este tipo de complemento es un "Complemento del concepto", por lo que puede existir uno por cada concepto
    */
    class EducationalInstitutionComplementExampleMultiemisor
    {
        private readonly FacturamaApiMultiemisor facturama;
        public EducationalInstitutionComplementExampleMultiemisor(FacturamaApiMultiemisor facturama)
        {
             this.facturama = facturama;
        }

        public void Run()
        {
            Console.WriteLine("----- Inicio del ejemplo EducationalInstitutionComplementExampleMultiemisor  -----");            

            var cfdi = new CfdiMulti
            {
                NameId = "1",                                   // Factura - Tomado del catálogo de nombres de Facturama https://apisandbox.facturama.mx/guias/catalogos/nombres-cfdi
                Folio = "1242",                                   
                CfdiType = CfdiType.Ingreso,
                LogoUrl = "https://facturama.mx/img/facturama_logo.png",     // El logo debe existir en una URL
                PaymentForm = "03",                             // Transferencia electrónica de fondos
                PaymentMethod = "PUE",                          // Pago en una exhibición
                Date = null,                                    // Al especificar null, Facturama asigna la fecha y hora actual, de acuerdo al "ExpeditionPlace"
                ExpeditionPlace = "78000",                      // Codigo postal del la sucursal desde donde se expide el CFDI, (este debe pertenecer al catálogo de sucursales)  https://apisandbox.facturama.mx/guias/perfil-fiscal#lugares-expedicion-series
                Items = new List<Item>(),
                Issuer = new Issuer
                {
                    Name  = "Escuela emisora del CFDI",
                    Rfc = "EKU9003173C9",
                    FiscalRegime = "601"
                },
                Receiver = new Receiver
                {
                    CfdiUse = "P01",
                    Name = "José Perez Leon",
                    Rfc = "JUFA7608212V6",
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

            // Complemento IEDU
            var itemComplement = new Facturama.Models.Complements.ItemComplement();
            itemComplement.EducationalInstitution = new EducationalInstitution
            {
                AutRvoe = "234",
                Curp = "POAJ890718HSPMLS05",
                EducationLevel = "Profesional técnico",
                StudentsName = "Emilio Perez Lopez",
                PaymentRfc = "XAMA620210DQ5"
            };

            var item = new Item
            {
                ProductCode = "86121503",                       // Reacionado con institucion educativa
                UnitCode = "E48",                               // Servicio
                Unit = "Unidad de servicio",
                Description = "Colegiatura del mes de marzo",
                IdentificationNumber = "1234",                  // Numero de identificación interno del producto (opcional)
                Quantity = 1,
                Discount = 0,
                UnitPrice = 2300,
                Subtotal = 2300,
                Taxes = new [] { new Tax
                {
                    Name = "IVA",
                    IsQuota = false,
                    IsRetention = false,

                    Rate = 0.16m,
                    Base = 2300,
                    Total = 368
                } }.ToList(),
                Complement = itemComplement                     // Complemento IEDU
            };
            var retenciones = item.Taxes?.Where(t => t.IsRetention).Sum(t => t.Total) ?? 0;
            var traslados = item.Taxes?.Where(t => !t.IsRetention).Sum(t => t.Total) ?? 0;
            item.Total = item.Subtotal - (item.Discount ?? 0) + traslados - retenciones;
            cfdi.Items.Add(item);

            try
            {
                var cfdiCreated = facturama.Cfdis.Create(cfdi);
                Console.WriteLine(
                    $"Se creó exitosamente el cfdi con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");
                facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);
                facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);
                

                if (facturama.Cfdis.SendByMail(cfdiCreated.Id, "chucho@facturama.mx"))
                    Console.WriteLine("Se envió correctamente el CFDI");
                

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

            Console.WriteLine("----- Fin del ejemplo EducationalInstitutionComplementExampleMultiemisor  -----");
        }


    }
}
