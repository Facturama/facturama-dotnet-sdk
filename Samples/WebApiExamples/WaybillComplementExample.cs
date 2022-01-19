using System;
using System.Collections.Generic;
using System.Linq;
using Facturama;
using Facturama.Models;
using Facturama.Models.Complements.Waybill;
using Facturama.Models.Request;

namespace WebApiExamples
{
    /**
    * Ejemplo de creación de un CFDI con el "Complemento Carta Porte 2.0"
    * Referencia: https://apisandbox.facturama.mx/guias/complementos/complemento-carta-porte
    * 
    * Este tipo de complemento es un "Complemento del comprobante", por lo que puede existir uno por cada comprobante
    */
    class WaybillComplementExample
    {
        private readonly FacturamaApi facturama;
        public WaybillComplementExample(FacturamaApi facturama)
        {
             this.facturama = facturama;
        }

        public void Run()
        {
            Console.WriteLine("----- Inicio del ejemplo WaybillComplementExample -----");            

            var cfdi = new Cfdi
            {
                NameId = "1",                                   // Factura
                CfdiType = CfdiType.Ingreso,
                PaymentForm = "03",                             // Transferencia electrónica de fondos
                PaymentMethod = "PUE",                          // Pago en una exhibición
                Date = null,                                    // Al especificar null, Facturama asigna la fecha y hora actual, de acuerdo al "ExpeditionPlace"
                ExpeditionPlace = "78000",                      // Codigo postal del la sucursal desde donde se expide el CFDI, (este debe pertenecer al catálogo de sucursales)  https://apisandbox.facturama.mx/guias/perfil-fiscal#lugares-expedicion-series
                Items = new List<Item>(),
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

            var item = new Item
            {
                ProductCode = "78101703",                       // Reacionado con institucion educativa
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
                } }.ToList()                
            };
            var retenciones = item.Taxes?.Where(t => t.IsRetention).Sum(t => t.Total) ?? 0;
            var traslados = item.Taxes?.Where(t => !t.IsRetention).Sum(t => t.Total) ?? 0;
            item.Total = item.Subtotal - (item.Discount ?? 0) + traslados - retenciones;
            cfdi.Items.Add(item);

            // Complemento Carta Porte            
            cfdi.Complement = new Complement
            {
                CartaPorte20 = new ComplementoCartaPorte20
                {
                    TranspInternac = TranspInternac.No,
                    Ubicaciones = new[] { new Ubicacion
                        {
                            TipoUbicacion = TipoUbicacion.Origen,
                            RFCRemitenteDestinatario = "EKU9003173C9",
                            FechaHoraSalidaLlegada = DateTime.Now.AddHours(-6),
                            DistanciaRecorrida = 1,
                            Domicilio = new Domicilio
                            {
                                Calle = "Puebla No.1",
                                CodigoPostal = "28239",
                                Colonia = "0342",
                                Estado = "COL",
                                Municipio = "007",
                                Localidad = "02",
                                Pais = "MEX",
                            }

                         },
                         new Ubicacion
                        {
                            TipoUbicacion = TipoUbicacion.Destino,
                            RFCRemitenteDestinatario = "RIFO990729M66",
                            FechaHoraSalidaLlegada = DateTime.Now.AddHours(+6),
                            DistanciaRecorrida = 100,
                            Domicilio = new Domicilio
                            {
                                Calle = "Morelos No.1",
                                CodigoPostal = "28219",
                                Colonia = "0575",
                                Estado = "COL",
                                Municipio = "007",
                                Localidad = "02",
                                Pais = "MEX",
                            }

                         }
                    },
                    Mercancias = new Mercancias
					{
                        UnidadPeso = "KGM",
                        Mercancia = new [] {
                            new Mercancia
							{
                                Cantidad = 1,
                                BienesTransp = "10101500",
                                Descripcion = "Animales vivos de granja",
                                ClaveUnidad = "KGM",
                                PesoEnKg = 120,
                            }
						},
                        Autotransporte = new Autotransporte
						{
                            PermSCT = "TPAF01",
                            NumPermisoSCT = "123abc",
                            Seguros = new Seguros
							{
                                AseguraRespCivil = "Seguros SA",
                                PolizaRespCivil = "123123"
                            },
                            IdentificacionVehicular = new IdentificacionVehicular
							{
                                AnioModeloVM = 1990,
                                ConfigVehicular = "C2R3",
                                PlacaVM = "XXX000",
                            },
                            Remolques = new []
							{
                                new Remolque
								{
                                    SubTipoRem = "CTR001",
                                    Placa = "21132H"
                                }
							}
                        }
					},
                    FiguraTransporte = new[] { new TiposFigura {
                            TipoFigura = "01",
                            RFCFigura = "MISC491214B86",
                            NombreFigura = "Juan",
                            NumLicencia = "000001",
                        },
                        new TiposFigura
						{
                            TipoFigura = "02",
                            RFCFigura = "XAMA620210DQ5",
                            NombreFigura = "Juan Jose Perez Lopez",
                            PartesTransporte = new [] { new PartesTransporte {
                                    ParteTransporte = "PT01"
                                }
                            },
                            Domicilio = new Domicilio
							{
                                Calle = "Morelos No.1",
                                CodigoPostal = "28219",
                                Colonia = "0575",
                                Estado = "COL",
                                Municipio = "007",
                                Localidad = "02",
                                Pais = "MEX",
                            }
                        }
                    }




                }
            };
                   

            try
            {
                var cfdiCreated = facturama.Cfdis.Create(cfdi);
                Console.WriteLine(
                    $"Se creó ex la carta porte 2.0 con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");
                facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);
                facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);
                

                if (facturama.Cfdis.SendByMail(cfdiCreated.Id, "chucho@facturama.mx"))
                    Console.WriteLine("Se envió correctamente la carta porte 2.0");
                

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

            Console.WriteLine("----- Fin del ejemplo WaybillComplementExample -----");
        }


    }
}
