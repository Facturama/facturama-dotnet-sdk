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
            Console.WriteLine("----- Inicio del ejemplo Carta Porte 3.0-----");    
            

            try
            {
                //TestCartaPorte30(facturama);
                //TestCartaPorte30_Traslado(facturama);
                //TestCartaPorte30_autotransporte(facturama);
            }
            catch (FacturamaException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Model.Details != null)
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

        public void TestCartaPorte30(FacturamaApi facturama)
        {
            var cfdi = new Cfdi
            {
                NameId = "35",
                Currency = "MXN",
                Folio = "99999",
                Serie = "CCP", 
                CfdiType = CfdiType.Ingreso,
                PaymentForm = "03",
                PaymentMethod = "PUE",
                Date = null,
                ExpeditionPlace = "78000",
                PaymentConditions = "CARTA PORTE 3.0",
                Observations = "Elemento Observaciones solo visible en PDF",
                Items = new List<Item>(),
                Receiver = new Receiver
                {
                    CfdiUse = "S01",
                    Name = "ESCUELA KEMPER URGATE",
                    Rfc = "EKU9003173C9",
                    FiscalRegime = "601",
                    TaxZipCode = "42501"

                },
            };

            var item = new Item
            {
                ProductCode = "78101800",
                UnitCode = "E48",
                Unit = "Unidad de servicio",
                Description = "Transporte de carga por carretera",
                IdentificationNumber = "UT421511",
                Quantity = 1,
                Discount = 0,
                UnitPrice = 1500,
                Subtotal = 1500,
                TaxObject = "02",
                Taxes = new[] { new Tax
                {
                    Name = "IVA",
                    IsQuota = false,
                    IsRetention = false,

                    Rate = 0.16m,
                    Base = 1500,
                    Total = 240
                } }.ToList()
            };
            var retenciones = item.Taxes?.Where(t => t.IsRetention).Sum(t => t.Total) ?? 0;
            var traslados = item.Taxes?.Where(t => !t.IsRetention).Sum(t => t.Total) ?? 0;
            item.Total = item.Subtotal - (item.Discount ?? 0) + traslados - retenciones;
            cfdi.Items.Add(item);

            // Complemento Carta Porte            
            cfdi.Complement = new Complement
            {
                CartaPorte30 = new ComplementoCartaPorte30
                {
                    TranspInternac = TranspInternac.No,
                    TotalDistRec = 1.0M,
                    RegistroISTMO = RegistroISTMO.Sí,
                    UbicacionPoloOrigen = "01",
                    UbicacionPoloDestino = "01",
                    Ubicaciones = new[]
                    {
                        new Ubicacion
                        {
                            TipoUbicacion = TipoUbicacion.Origen,
                            IDUbicacion = "OR101010",
                            RFCRemitenteDestinatario = "EKU9003173C9",
                            NombreRemitenteDestinatario = "ESCUELA KEPLER URGATE",
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
                            IDUbicacion = "DE202020",
                            RFCRemitenteDestinatario = "EKU9003173C9",
                            NombreRemitenteDestinatario = "NombreRem2",
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
                        PesoBrutoTotal = 1.0M,
                        UnidadPeso = "XBX",
                        PesoNetoTotal = 1M,
                        NumTotalMercancias = 1,
                        LogisticaInversaRecoleccionDevolucion = "Sí",
                        Mercancia = new[]
                        {
                            new Mercancia
                            {
                                BienesTransp = "11121900",
                                Descripcion = "Accesorios de equipo de telefonía",
                                SectorCOFEPRIS = "01",
                                Cantidad = 1.0M,
                                ClaveUnidad = "XBX",
                                MaterialPeligroso = "No",
                                PesoEnKg = 1,
                                DenominacionGenericaProd = "DenominacionGenericaProd1",
                                DenominacionDistintivaProd = "DenominacionDistintivaProd1",
                                Fabricante = "Fabricante1",
                                FechaCaducidad = DateTime.Now.AddHours(-6),
                                LoteMedicamento = "LoteMedic1",
                                FormaFarmaceutica = "01",
                                CondicionesEspTransp = "01",
                                RegistroSanitarioFolioAutorizacion = "RegistroSanita1",
                                CantidadTransporta = new[]
                                {
                                    new CantidadTransporta
                                    {
                                        Cantidad = 1.0m,
                                        IDOrigen = "OR101010",
                                        IDDestino = "DE202020"
                                    }
                                }

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
                                PesoBrutoVehicular = "1"
                            },
                            Remolques = new[]
                            {
                                new Remolque
                                {
                                    SubTipoRem = "CTR001",
                                    Placa = "21132H"
                                }
                            }
                        }
                    },
                    FiguraTransporte = new[]
                    {
                        new TiposFigura
                        {
                            TipoFigura = "01",
                            RFCFigura = "XIA190128J61",
                            NombreFigura = "XENON INDUSTRIAL ARTICLES",
                            NumLicencia = "000001",
                        },
                        new TiposFigura
                        {
                            TipoFigura = "02",
                            RFCFigura = "XAMA620210DQ5",
                            NombreFigura = "Juan Jose Perez Lopez",
                            PartesTransporte = new []
                            {
                                new PartesTransporte
                                {
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



            var cfdiCreated = facturama.Cfdis.Create3(cfdi);
            Console.WriteLine($"Se genero la carta porte 3.0 con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");
            //facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);
            //facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);


            //if (facturama.Cfdis.SendByMail(cfdiCreated.Id, "correo@ejemplo.com"))
            // Console.WriteLine("Se envió correctamente la carta porte 3.0");


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

            Console.WriteLine("----- Fin del ejemplo WaybillComplementExample -----");

        }

        public void TestCartaPorte30_Traslado(FacturamaApi facturama)
        {
            var cfdi = new Cfdi
            {
                NameId = "35",
                Currency = "MXN",
                Folio = "99999",
                Serie = "CCP",
                CfdiType = CfdiType.Traslado,
                Date = null,
                ExpeditionPlace = "78000",
                Observations = "Elemento Observaciones solo visible en PDF",
                Items = new List<Item>(),
                Receiver = new Receiver
                {
                    CfdiUse = "S01",
                    Name = "ESCUELA KEMPER URGATE",
                    Rfc = "EKU9003173C9",
                    FiscalRegime = "601",
                    TaxZipCode = "42501"

                },
            };

            var item = new Item
            {
                ProductCode = "78101800",
                UnitCode = "E48",
                Unit = "Unidad de servicio",
                Description = "Transporte de carga por carretera",
                IdentificationNumber = "UT421511",
                Quantity = 1,
                UnitPrice = 0,
                Subtotal = 0M,
                TaxObject = "01",
                Total = 0M,
            };
            cfdi.Items.Add(item);

            // Complemento Carta Porte            
            cfdi.Complement = new Complement
            {
                CartaPorte30 = new ComplementoCartaPorte30
                {
                    TranspInternac = TranspInternac.No,
                    TotalDistRec = 1.0M,
                    RegistroISTMO = RegistroISTMO.Sí,
                    UbicacionPoloOrigen = "01",
                    UbicacionPoloDestino = "01",
                    Ubicaciones = new[]
                    {
                        new Ubicacion
                        {
                            TipoUbicacion = TipoUbicacion.Origen,
                            IDUbicacion = "OR101010",
                            RFCRemitenteDestinatario = "EKU9003173C9",
                            NombreRemitenteDestinatario = "ESCUELA KEPLER URGATE",
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
                            IDUbicacion = "DE202020",
                            RFCRemitenteDestinatario = "EKU9003173C9",
                            NombreRemitenteDestinatario = "NombreRem2",
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
                        PesoBrutoTotal = 1.0M,
                        UnidadPeso = "XBX",
                        PesoNetoTotal = 1M,
                        NumTotalMercancias = 1,
                        LogisticaInversaRecoleccionDevolucion = "Sí",
                        Mercancia = new[]
                        {
                            new Mercancia
                            {
                                BienesTransp = "11121900",
                                Descripcion = "Accesorios de equipo de telefonía",
                                SectorCOFEPRIS = "01",
                                Cantidad = 1.0M,
                                ClaveUnidad = "XBX",
                                MaterialPeligroso = "No",
                                PesoEnKg = 1,
                                DenominacionGenericaProd = "DenominacionGenericaProd1",
                                DenominacionDistintivaProd = "DenominacionDistintivaProd1",
                                Fabricante = "Fabricante1",
                                FechaCaducidad = DateTime.Now.AddHours(-6),
                                LoteMedicamento = "LoteMedic1",
                                FormaFarmaceutica = "01",
                                CondicionesEspTransp = "01",
                                RegistroSanitarioFolioAutorizacion = "RegistroSanita1",
                                CantidadTransporta = new[]
                                {
                                    new CantidadTransporta
                                    {
                                        Cantidad = 1.0m,
                                        IDOrigen = "OR101010",
                                        IDDestino = "DE202020"
                                    }
                                }

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
                                PesoBrutoVehicular = "1"
                            },
                            Remolques = new[]
                            {
                                new Remolque
                                {
                                    SubTipoRem = "CTR001",
                                    Placa = "21132H"
                                }
                            }
                        }
                    },
                    FiguraTransporte = new[]
                    {
                        new TiposFigura
                        {
                            TipoFigura = "01",
                            RFCFigura = "XIA190128J61",
                            NombreFigura = "NombreFigura1",
                            NumLicencia = "000001",
                        }
                    }
                }
            };



            var cfdiCreated = facturama.Cfdis.Create3(cfdi);
            Console.WriteLine($"Se genero la carta porte 3.0 con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");
            //facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);
            //facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);


            //if (facturama.Cfdis.SendByMail(cfdiCreated.Id, "correo@ejemplo.com"))
            // Console.WriteLine("Se envió correctamente la carta porte 3.0");


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

            Console.WriteLine("----- Fin del ejemplo WaybillComplementExample -----");

        }

        public void TestCartaPorte30_autotransporte(FacturamaApi facturama)
        {
            var cfdi = new Cfdi
            {
                NameId = "35",
                Currency = "MXN",
                Folio = "99999",
                Serie = "CCP",
                CfdiType = CfdiType.Ingreso,
                PaymentForm = "03",
                PaymentMethod = "PUE",
                Date = null,
                ExpeditionPlace = "78000",
                PaymentConditions = "CARTA PORTE 3.0",
                Observations = "Elemento Observaciones solo visible en PDF",
                Items = new List<Item>(),
                Receiver = new Receiver
                {
                    CfdiUse = "S01",
                    Name = "ESCUELA KEMPER URGATE",
                    Rfc = "EKU9003173C9",
                    FiscalRegime = "601",
                    TaxZipCode = "42501"

                },
            };

            var item = new Item
            {
                ProductCode = "78101800",
                UnitCode = "E48",
                Unit = "Unidad de servicio",
                Description = "Transporte de carga por carretera",
                IdentificationNumber = "UT421511",
                Quantity = 1,
                Discount = 0,
                UnitPrice = 1500,
                Subtotal = 1500,
                TaxObject = "02",
                Taxes = new[] { new Tax
                {
                    Name = "IVA",
                    IsQuota = false,
                    IsRetention = false,

                    Rate = 0.16m,
                    Base = 1500,
                    Total = 240
                } }.ToList()
            };
            var retenciones = item.Taxes?.Where(t => t.IsRetention).Sum(t => t.Total) ?? 0;
            var traslados = item.Taxes?.Where(t => !t.IsRetention).Sum(t => t.Total) ?? 0;
            item.Total = item.Subtotal - (item.Discount ?? 0) + traslados - retenciones;
            cfdi.Items.Add(item);

            // Complemento Carta Porte            
            cfdi.Complement = new Complement
            {
                CartaPorte30 = new ComplementoCartaPorte30
                {
                    TranspInternac = TranspInternac.No,
                    TotalDistRec = 1.0M,
                    RegistroISTMO = null,
                    UbicacionPoloOrigen = null,
                    UbicacionPoloDestino = null,
                    Ubicaciones = new[]
                    {
                        new Ubicacion
                        {
                            TipoUbicacion = TipoUbicacion.Origen,
                            IDUbicacion = "OR101010",
                            RFCRemitenteDestinatario = "EKU9003173C9",
                            NombreRemitenteDestinatario = "ESCUELA KEPLER URGATE",
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
                            IDUbicacion = "DE202020",
                            RFCRemitenteDestinatario = "EKU9003173C9",
                            NombreRemitenteDestinatario = "NombreRem2",
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
                        PesoBrutoTotal = 10.0M,
                        UnidadPeso = "KGM",
                        PesoNetoTotal = 10.0M,
                        NumTotalMercancias = 1,
                        LogisticaInversaRecoleccionDevolucion = null,
                        CargoPorTasacion = 90.00M,
                        Mercancia = new[]
                        {
                            new Mercancia
                            {
                                BienesTransp = "44122000",
                                Descripcion = "Folder",
                                ClaveSTCC = null,
                                SectorCOFEPRIS = null,
                                Cantidad = 2.0M,
                                ClaveUnidad = "H87",
                                MaterialPeligroso = "No",
                                PesoEnKg = 1.00M,
                                ValorMercancia = 116.00M,
                                Moneda = "MXN",

                                DenominacionGenericaProd = null,
                                DenominacionDistintivaProd = null,
                                Fabricante = null,
                                FechaCaducidad = null,
                                LoteMedicamento = null,
                                FormaFarmaceutica = null,
                                CondicionesEspTransp = null,
                                RegistroSanitarioFolioAutorizacion = null,
                                CantidadTransporta = new[]
                                {
                                    new CantidadTransporta
                                    {
                                        Cantidad = 1.0m,
                                        IDOrigen = "OR101010",
                                        IDDestino = "DE202020"
                                    }
                                }

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
                                PesoBrutoVehicular = "1"
                            },
                            Remolques = new[]
                            {
                                new Remolque
                                {
                                    SubTipoRem = "CTR001",
                                    Placa = "21132H"
                                }
                            }
                        }
                    },
                    FiguraTransporte = new[]
                    {
                        new TiposFigura
                        {
                            TipoFigura = "01",
                            RFCFigura = "XIA190128J61",
                            NombreFigura = "XENON INDUSTRIAL ARTICLES",
                            NumLicencia = "000001",
                        },
                        new TiposFigura
                        {
                            TipoFigura = "02",
                            RFCFigura = "XAMA620210DQ5",
                            NombreFigura = "Juan Jose Perez Lopez",
                            PartesTransporte = new []
                            {
                                new PartesTransporte
                                {
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



            var cfdiCreated = facturama.Cfdis.Create3(cfdi);
            Console.WriteLine($"Se genero la carta porte 3.0 con el folio fiscal: {cfdiCreated.Complement.TaxStamp.Uuid}");
            //facturama.Cfdis.SavePdf($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.pdf", cfdiCreated.Id);
            //facturama.Cfdis.SaveXml($"factura{cfdiCreated.Complement.TaxStamp.Uuid}.xml", cfdiCreated.Id);


            //if (facturama.Cfdis.SendByMail(cfdiCreated.Id, "correo@ejemplo.com"))
            // Console.WriteLine("Se envió correctamente la carta porte 3.0");


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

            Console.WriteLine("----- Fin del ejemplo WaybillComplementExample -----");

        }
    }
}
