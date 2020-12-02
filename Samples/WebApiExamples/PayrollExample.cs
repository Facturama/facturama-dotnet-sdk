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
    * Referencia: https://apisandbox.facturama.mx/guias/nominas/sueldo
    * 
    * En virtud de que el complemento de pago, requiere ser asociado a un CFDI con el campo "PaymentMethod" = "PPD"
    * En este ejemplo se incluye la creacón de este CFDI, para posteriormente realizar el  "Complemento de pago" = "PUE"     
    */
    class PayrollExample
	{
        private readonly FacturamaApi facturama;
        public PayrollExample(FacturamaApi facturama)
        {
             this.facturama = facturama;
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("----- Inicio del ejemplo PayrollExample -----");
               

                // Cfdi Incial (debe ser "PPD")
                // -------- Creacion del cfdi en su forma general (sin items / productos) asociados --------
                Cfdi payroll = CreatePayrollModel(facturama);                

                // Se manda timbrar mediante Facturama
                Facturama.Models.Response.Cfdi cfdiInicial = facturama.Cfdis.Create(payroll);

                Console.WriteLine("Se creó exitosamente el CFDI de Nómina con el folio fiscal: " + cfdiInicial.Complement.TaxStamp.Uuid);

                // Descarga de los archivos de la Nómina (en este caso se especifica que el tipo de comprobante es "Payroll")
                String filePath = "nomina" + cfdiInicial.Complement.TaxStamp.Uuid;
                facturama.Cfdis.SavePdf(filePath + ".pdf", cfdiInicial.Id, Facturama.Services.CfdiService.InvoiceType.Payroll);
                facturama.Cfdis.SaveXml(filePath + ".xml", cfdiInicial.Id, Facturama.Services.CfdiService.InvoiceType.Payroll);                                

                // Posibilidad de mandar las nóminas por correo
                Console.WriteLine(facturama.Cfdis.SendByMail(cfdiInicial.Id, "chucho@facturama.mx", null, Facturama.Services.CfdiService.InvoiceType.Payroll));
                

                Console.WriteLine("----- Fin del ejemplo de PayrollExample -----");
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

        /**
        * Llenado del modelo de CFDI para Nóminas        
        */
        private static Cfdi CreatePayrollModel(FacturamaApi facturama)
        {
            var products = facturama.Products.List().Where(p => p.Taxes.Any()).ToList();

            var nameId = "16"; // "Nomina" de acuerdo al catálogo de nombres https://apisandbox.facturama.mx/guias/catalogos/nombres-cfdi
            var currency = facturama.Catalogs.Currencies.First(m => m.Value == "MXN");
            var paymentMethod = facturama.Catalogs.PaymentMethods.First(p => p.Value == "PUE");
            var paymentForm = "99";  // 99 = por definir
            var cliente = facturama.Clients.List().First();                // Puedes seleccionar el cliente de tu catálogo de clientes

            var branchOffice = facturama.BranchOffices.List().First();


            var lstPerceptions = new List<PerceptionsDetail>();
            lstPerceptions.Add(new PerceptionsDetail
            {
                PerceptionType = "046",
                Code = "046",
                Description = "ASIMILIADOS A SALARIOS",
                TaxedAmount = 3621.18m,
                ExemptAmount = 0m
            });

            
            var lstDeductions = new List<DeductionsDetail>();
            lstDeductions.Add(new DeductionsDetail
            {
                DeduccionType = "002",
                Code = "002",
                Description = "ISR",
                Amount = 172.60M                
            });

            var lstOtherPayments = new List<OtherPayment>();
            lstOtherPayments.Add( new OtherPayment
			{
                EmploymentSubsidy = new EmploymentSubsidy
				{
                    Amount = 10
				},
                OtherPaymentType = "002",
                Code = "000",
                Description = "Subsidio",
                Amount = 10
            });


            var payroll = new Cfdi
            {
                NameId = nameId,
                CfdiType = CfdiType.Nomina,
                PaymentForm = paymentForm,
                PaymentMethod = "PUE",           // Pago en una exhibicion
                Currency = currency.Value,
                Date = null,
                ExpeditionPlace = "78220",                



                Receiver = new Receiver  // En este caso pudimos poner los datos de cliente tomado del catálogo, o colocar los datos de otra fuente
                {
                    CfdiUse = "P01",
                    Name = "ADRIANA GARCIA MORALES",
                    Rfc = "CACX7605101P8"
                },
                Complement = new Complement
                {
                    Payroll = new Payroll
                    {
                        Type = "O",                      // Tipo de nomina [O = ordinaria, E = exenta]
                        DailySalary = 0m,
                        BaseSalary = 0m,
                        PaymentDate = Convert.ToDateTime("2020-10-06"),
                        InitialPaymentDate = Convert.ToDateTime("2020-10-06"),
                        FinalPaymentDate = Convert.ToDateTime("2020-10-06"),
                        DaysPaid = 15,
                        Issuer = new PayrollIssuer
                        {
                            EmployerRegistration = "SampleRegistration"
                        },
                        Employee = new Employee
                        {
                            Curp = "GAMA800912MSPRRD05",
                            SocialSecurityNumber = "92919084431",
                            PositionRisk = "1",
                            ContractType = "01",
                            RegimeType = "02",
                            Unionized = false,
                            TypeOfJourney = "01",
                            EmployeeNumber = "006",
                            Department = null,
                            Position = null,
                            FrequencyPayment = "04",
                            FederalEntityKey = "SLP",
                            DailySalary = 50,
                            StartDateLaborRelations = Convert.ToDateTime("2019-10-06"),
                        },
                        Perceptions = new Perceptions
                        {
                            Details = lstPerceptions.ToArray()
                        },
                        Deductions = new Deductions
                        {
                            Details = lstDeductions.ToArray()
                        },
                        OtherPayments = lstOtherPayments.ToArray()
                    }
                    
				}
            };

            return payroll;
        }

       
    }
}
