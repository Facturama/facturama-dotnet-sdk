using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facturama;
using System.Linq;
using Facturama.Models.Request;
using Facturama.Models;
using Facturama.Models.Complements;
using System.Collections;
using Facturama.Models.Response.Catalogs.Cfdi;

namespace Tests
{
    /// <summary>
    /// Descripción resumida de PaymentComplement
    /// </summary>
    [TestClass]
    public class CDPaymentComplement
    {
        private readonly FacturamaApi _facturamaWeb;
		private readonly FacturamaApiMultiemisor _facturamaMultiEmisor;

        public CDPaymentComplement()
        {
            //Api
            _facturamaWeb = new FacturamaApi("pruebas", "pruebas2011");
            
			/*// Ejemplo de la creación de un complemento de pago
            SamplePaymentComplement(facturama);
            */

            //Api Multiemisor
            _facturamaMultiEmisor = new FacturamaApiMultiemisor("pruebas", "pruebas2011");


            // Ejemplo de la creación de un complemento de pago Multiemeisor
            //SampleCreatePaymentCfdiMultiemisor(facturamaMultiEmisor);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateSamplePaymentMultiemisorComplement()
        {
            var cfdi = SampleCreatePaymentCfdiMultiemisor(_facturamaMultiEmisor);
			Assert.IsNotNull(cfdi);
			Assert.IsTrue(Guid.TryParse(cfdi.Complement.TaxStamp.Uuid, out Guid uuid));
			Assert.AreEqual(cfdi.Status, "active");
			cfdi = _facturamaMultiEmisor.Cfdis.Remove(cfdi.Id);
			Assert.AreEqual(cfdi.Status, "canceled");
        }

        /**
         * Llenado del modelo de CFDI, de una forma general
         * - Se especifica: la moneda, método de pago, forma de pago, cliente, y lugar de expedición     
         */
        private static Cfdi CreateModelCfdiGeneral(FacturamaApi facturama)
        {
            var products = facturama.Products.List().Where(p => p.Taxes.Any()).ToList();

            var nameId = facturama.Catalogs.NameIds.First(); //Nombre en el pdf: "Factura"
            var currency = facturama.Catalogs.Currencies.First(m => m.Value == "MXN");
            var paymentMethod = facturama.Catalogs.PaymentMethods.First(p => p.Value == "PUE");
            var paymentForm = facturama.Catalogs.PaymentForms.First(p => p.Name == "Efectivo");
            var cliente = facturama.Clients.List().First(c => c.Rfc == "ESO1202108R2");

            var branchOffice = facturama.BranchOffices.List().First();

            var cfdi = new Cfdi
            {
                NameId = nameId.Value,
                CfdiType = CfdiType.Ingreso,
                PaymentForm = paymentForm.Value,
                PaymentMethod = paymentMethod.Value,
                Currency = currency.Value,
                Date = DateTime.Now,
                ExpeditionPlace = branchOffice.Address.ZipCode,
                //Items = new List<Item>(),
                Receiver = new Receiver
                {
                    CfdiUse = string.IsNullOrEmpty(cliente.CfdiUse) ? "P01": cliente.CfdiUse,
                    Name = cliente.Name,
                    Rfc = cliente.Rfc
                },
            };
            
            return cfdi;
        }
        private static Cfdi AddItemsToCfdi(FacturamaApi facturama,Cfdi cfdi) {

            // Lista de todos los productos
            List<Product> lstProducts = facturama.Products.List();
            Random random = new Random();
            var currency = facturama.Catalogs.Currencies.First(m => m.Value == "MXN");

            int nItems = random.Next(lstProducts.Count) % 10 + 1;
            int decimals = (int)currency.Decimals;


            // Lista de Items en el cfdi (los articulos a facturar)
            List<Item> lstItems = new List<Item>();

            // Creacion del CFDI 
            for (int i = lstProducts.Count - nItems; i < lstProducts.Count && i > 0; i++) {

                Product product = lstProducts.ElementAt(i);   // Un producto cualquiera
                int quantity = random.Next(1,5) + 1;   // una cantidad aleatoria de elementos de este producto
                Double discount = Decimal.ToDouble(product.Price % (product.Price == 0 ? 1 : random.Next(1,Decimal.ToInt32(product.Price))));

                // Redondeo del precio del producto, de acuerdo a la moneda
                Double numberOfDecimals = Math.Pow(10, decimals);
                Double subTotal = Math.Round((Decimal.ToDouble(product.Price) * quantity) * numberOfDecimals) / numberOfDecimals;


                // Llenado del item (que va en el cfdi)
                Item item = new Item
                {
                    ProductCode = product.CodeProdServ,
                    UnitCode = product.UnitCode,
                    Unit = product.Unit,
                    Description = product.Description,
                    IdentificationNumber = product.IdentificationNumber,
                    Quantity = quantity,
                    Discount = Convert.ToDecimal(Math.Round(discount * numberOfDecimals) / numberOfDecimals),
                    UnitPrice = Convert.ToDecimal(Math.Round(Decimal.ToDouble(product.Price) * numberOfDecimals) / numberOfDecimals),
                    Subtotal = Convert.ToDecimal(subTotal)

                };

                // ---- Llenado de los impuestos del item ----                                    
                item = AddTaxesToItem(item, product, numberOfDecimals);
                if(item.UnitPrice > 0m)
                lstItems.Add(item);
            }
            cfdi.Items=lstItems;
        
            return cfdi;
        }

        /**
         * Se agregan los impuestos al Item (uno de los items del cfdi)
         * Se agregan todos los impuestos del producto, en el caso de que no se tengan impuestos, se debe colocar un valor nulo
        */
        private static Item AddTaxesToItem(Item item, Product product, Double numberOfDecimals)
        {
            var lstProductTaxes = product.Taxes.Select(i => i).ToList();
            //List<Tax> lstProductTaxes = product.Taxes.Select(i=>i).ToList(); // impuestos del producto
            List<Facturama.Models.Request.Tax> lstTaxes = new List<Facturama.Models.Request.Tax>();              // Impuestos del item (del cfdi)

            Double baseAmount = Math.Round((Decimal.ToDouble(item.Subtotal) - Decimal.ToDouble(item.Discount)) * numberOfDecimals) / numberOfDecimals;

            for (int j = 0; j < lstProductTaxes.Count; j++)
            {

                Facturama.Models.Request.Tax pTax = lstProductTaxes.ElementAt(j);

                Facturama.Models.Request.Tax tax = new Facturama.Models.Request.Tax();

                tax.Name=pTax.Name;
                tax.IsQuota=pTax.IsQuota;
                tax.IsRetention=pTax.IsRetention;
                if (pTax.Rate<1)
                {
                    pTax.Rate=0;
                }

                Double rate = Decimal.ToDouble(pTax.Rate);
                Double rateRounded = (double)Math.Round(rate * 1000000) / 1000000;
                tax.Rate= Convert.ToDecimal(rateRounded);
                tax.Base= Convert.ToDecimal(Math.Round(decimal.ToDouble(item.Subtotal)* numberOfDecimals) / numberOfDecimals);
                tax.Total= Convert.ToDecimal(Math.Round((/*cambie el baseAmount*/decimal.ToDouble(tax.Base * pTax.Rate)) * numberOfDecimals) / numberOfDecimals);
                if(tax.Base>=0m)
                lstTaxes.Add(tax);
            }


            Double retentionsAmount = 0D;
            Double transfersAmount = 0D;

            // Asignación de los impuestos, en caso de que no se tengan, el campo va nulo
            if (lstTaxes.Count>0)
            {
                item.Taxes=lstTaxes;

                retentionsAmount = item.Taxes.Where(o => o.IsRetention).Sum(o=> Decimal.ToDouble(o.Total));
                transfersAmount = item.Taxes.Where(o=> !o.IsRetention).Sum(o=> Decimal.ToDouble(o.Total));
            }

            // Calculo del subtotal
            item.Total=(item.Subtotal - item.Discount + Convert.ToDecimal(transfersAmount) - Convert.ToDecimal(retentionsAmount));

            return item;

        }
        /**
        * Modelo "Complemento de pago"
        * - Se especifica: la moneda, método de pago, forma de pago, cliente, y lugar de expedición     
        */
        private static Cfdi CreateModelCfdiPaymentComplement(FacturamaApi facturama, Facturama.Models.Response.Cfdi cfdiInicial)
        {


            Cfdi cfdi = new Cfdi();

            // Lista del catálogo de nombres en el PDF
            var nameForPdf = facturama.Catalogs.NameIds.First(m => m.Value == "14"); // Nombre en el pdf: "Complemento de pago"
            cfdi.NameId = nameForPdf.Value;

            // Receptor de comprobante (se toma como cliente el mismo a quien se emitió el CFDI Inicial),            
            String clientRfc = cfdiInicial.Receiver.Rfc;
            Client client = facturama.Clients.List().Where(p => p.Rfc.Equals(clientRfc)).First();

            Receiver receiver = new Receiver
            {
                CfdiUse = client.CfdiUse,//"P01"
                Name = client.Name,
                Rfc = client.Rfc
            };
            cfdi.Receiver = receiver;

            // Lugar de expedición (es necesario por lo menos tener una sucursal)
            BranchOffice branchOffice = facturama.BranchOffices.List().First();
            cfdi.ExpeditionPlace = branchOffice.Address.ZipCode;

            // Fecha y hora de expecidión del comprobante
            //DateTime bindingDate;
            //DateTime.TryParse(cfdiBinding.Date, null, DateTimeStyles.RoundtripKind, out bindingDate);
            DateTime cfdiDate = DateTime.Now;
            cfdi.Date = cfdiDate;
            cfdi.CfdiType = CfdiType.Pago;
            // Complemento de pago ---
            Complement complement = new Complement();

            // Pueden representarse más de un pago en un solo CFDI
            List<Payment> lstPagos = new List<Payment>();
            Payment pago = new Payment();

            // Fecha y hora en que se registró el pago en el formato: "yyyy-MM-ddTHH:mm:ss" 
            // (la fecha del pago debe ser menor que la fecha en que se emite el CFDI)
            // Para este ejemplo, se considera que  el pago se realizó hace una hora   
            pago.Date = cfdiDate.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss");


            // Forma de pago (Efectivo, Tarjeta, etc)
            Facturama.Models.Response.Catalogs.CatalogViewModel paymentForm = facturama.Catalogs.PaymentForms.Where(p => p.Name.Equals("Efectivo")).First();
            pago.PaymentForm = paymentForm.Value;

            // Selección de la moneda del catálogo
            // La Moneda, puede ser diferente a la del documento inicial
            // (En el caso de que sea diferente, se debe colocar el tipo de cambio)
            List<CurrencyCatalog> lstCurrencies = facturama.Catalogs.Currencies.ToList();
            CurrencyCatalog currency = lstCurrencies.Where(p => p.Value.Equals("MXN")).First();
            pago.Currency = currency.Value;

            // Monto del pago
            // Este monto se puede distribuir entre los documentos relacionados al pago            
            pago.Amount = 100.00m;

            // Documentos relacionados con el pago
            // En este ejemplo, los datos se obtiene el cfdiInicial, pero puedes colocar solo los datos
            // aun sin tener el "Objeto" del cfdi Inicial, ya que los valores son del tipo "String"
            List<RelatedDocument> lstRelatedDocuments = new List<RelatedDocument>();
            RelatedDocument relatedDocument = new RelatedDocument {
                Uuid = cfdiInicial.Complement.TaxStamp.Uuid, // "27568D31-E579-442F-BA77-798CBF30BD7D"
                Serie="A",//cfdiInicial.Serie, // "EA"
                Folio=cfdiInicial.Folio, // 34853
                Currency=currency.Value,
                PaymentMethod="PUE", // En el complemento de pago tiene que ser PUE
                PartialityNumber=1,
                PreviousBalanceAmount=100.00m,
                AmountPaid=100.00m
            };
            lstRelatedDocuments.Add(relatedDocument);
            
            pago.RelatedDocuments=lstRelatedDocuments;
            
            lstPagos.Add(pago);
            
            complement.Payments=lstPagos;
            
            cfdi.Complement=complement;
            
       
            return cfdi;
                
        }

        /**
         * Ejemplo de creación de un CFDI "complemento de pago"
         * Referencia: https://apisandbox.facturama.mx/guias/api-web/cfdi/complemento-pago
         * 
         * En virtud de que el complemento de pago, requiere ser asociado a un CFDI con el campo "PaymentMethod" = "PPD"
         * En este ejemplo se incluye la creacón de este CFDI, para posteriormente realizar el  "Complemento de pago" = "PUE"     
        */
        private static Cfdi SamplePaymentComplement(FacturamaApi facturama)
        {

            try
            {
                Console.WriteLine("----- Inicio del ejemplo samplePaymentComplement -----");

				// Cfdi Incial (debe ser "PPD")
				// -------- Creacion del cfdi en su forma general (sin items / productos) asociados --------
				Cfdi cfdi = CreateModelCfdiGeneral(facturama);

                // -------- Agregar los items que lleva el cfdi ( para este ejemplo, se agregan con datos aleatorios) --------        
                cfdi = AddItemsToCfdi(facturama, cfdi);

                cfdi.PaymentMethod = "PPD";           // El método de pago del documento inicial debe ser "PPD"

                // Se manda timbrar mediante Facturama
                Facturama.Models.Response.Cfdi cfdiInicial = facturama.Cfdis.Create(cfdi);

                Console.WriteLine("Se creó exitosamente el cfdi Inicial (PPD) con el folio fiscal: " + cfdiInicial.Complement.TaxStamp.Uuid);

                // Descarga de los archivos del documento inicial
                String filePath = "factura" + cfdiInicial.Complement.TaxStamp.Uuid;
                facturama.Cfdis.SavePdf(filePath + ".pdf", cfdiInicial.Id);
                facturama.Cfdis.SaveXml(filePath + ".xml", cfdiInicial.Id);



				// Complemento de pago (debe ser "PUE")        
				// Y no lleva "Items" solo especifica el "Complemento"
				Cfdi paymentComplementModel = CreateModelCfdiPaymentComplement(facturama, cfdiInicial);


                // Se manda timbrar el complemento de pago mediante Facturama
                Facturama.Models.Response.Cfdi paymentComplement = facturama.Cfdis.Create(paymentComplementModel);

                Console.WriteLine("Se creó exitosamente el complemento de pago con el folio fiscal: " + cfdiInicial.Complement.TaxStamp.Uuid);


                // Descarga de los archivos del documento inicial
                String filePathPayment = "factura" + paymentComplement.Complement.TaxStamp.Uuid;
                facturama.Cfdis.SavePdf(filePath + ".pdf", paymentComplement.Id);
                facturama.Cfdis.SaveXml(filePath + ".xml", paymentComplement.Id);



                // Posibilidad de mandar  los cfdis por coreo ( el cfdiInical y complemento de pago)
                Console.WriteLine(facturama.Cfdis.SendByMail(cfdiInicial.Id, "chucho@facturama.mx"));
                Console.WriteLine(facturama.Cfdis.SendByMail(paymentComplement.Id, "chucho@facturama.mx"));


                Console.WriteLine("----- Fin del ejemplo de samplePaymentComplement -----");
				return cfdi;
            }
            catch (FacturamaException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var messageDetail in ex.Model.Details)
                {
                    Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
                }
				return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: ", ex.Message);
				return null;
            }

        }
        /*
         * Ejemplo de creación de un CFDI Multiemisor "complemento de pago"
         * 
         */
        private static Facturama.Models.Response.Cfdi SampleCreatePaymentCfdiMultiemisor(FacturamaApiMultiemisor facturama)
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
                    Rfc = "EKU9003173C9",
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
               
            
				return cfdiCreated;
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
			return null;
        }

    }
}
