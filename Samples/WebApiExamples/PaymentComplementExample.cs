using System;
using System.Collections.Generic;
using System.Linq;
using Facturama;
using Facturama.Models;
using Facturama.Models.Request;
using Facturama.Models.Response.Catalogs.Cfdi;

namespace WebApiExamples
{
    /**
    * Ejemplo de creación de un CFDI "complemento de pago"
    * Referencia: https://apisandbox.facturama.mx/guias/api-web/cfdi/complemento-pago
    * 
    * En virtud de que el complemento de pago, requiere ser asociado a un CFDI con el campo "PaymentMethod" = "PPD"
    * En este ejemplo se incluye la creacón de este CFDI, para posteriormente realizar el  "Complemento de pago" = "PUE"     
    */
    class PaymentComplementExample
	{
        private readonly FacturamaApi facturama;
        public PaymentComplementExample(FacturamaApi facturama)
        {
             this.facturama = facturama;
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("----- Inicio del ejemplo PaymentComplementExample -----");

                // Cfdi Incial (debe ser "PPD")
                // -------- Creacion del cfdi en su forma general (sin items / productos) asociados --------
                Facturama.Models.Request.Cfdi cfdi = CreateModelCfdiGeneral(facturama);

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
                Facturama.Models.Request.Cfdi paymentComplementModel = CreateModelCfdiPaymentComplement(facturama, cfdiInicial);


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


                Console.WriteLine("----- Fin del ejemplo de PaymentComplementExample -----");
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
        * Llenado del modelo de CFDI, de una forma general
        * - Se especifica: la moneda, método de pago, forma de pago, cliente, y lugar de expedición     
        */
        private static Facturama.Models.Request.Cfdi CreateModelCfdiGeneral(FacturamaApi facturama)
        {
            var products = facturama.Products.List().Where(p => p.Taxes.Any()).ToList();

            var nameId = facturama.Catalogs.NameIds.First(); //Nombre en el pdf: "Factura"
            var currency = facturama.Catalogs.Currencies.First(m => m.Value == "MXN");
            var paymentMethod = facturama.Catalogs.PaymentMethods.First(p => p.Value == "PUE");
            var paymentForm = facturama.Catalogs.PaymentForms.First(p => p.Name == "Efectivo");
            var cliente = facturama.Clients.List().First();

            var branchOffice = facturama.BranchOffices.List().First();

            var cfdi = new Cfdi
            {
                NameId = nameId.Value,
                CfdiType = CfdiType.Ingreso,
                PaymentForm = paymentForm.Value,
                PaymentMethod = paymentMethod.Value,
                Currency = currency.Value,
                Date = null,
                ExpeditionPlace = "83170",
                //Items = new List<Item>(),
                Receiver = new Receiver
                {
                    CfdiUse = "P01",
                    Name = "Público en general",
                    Rfc = "XAXX010101000"
                },
            };

            return cfdi;
        }


        private static Facturama.Models.Request.Cfdi AddItemsToCfdi(FacturamaApi facturama, Cfdi cfdi)
        {

            // Lista de todos los productos
            List<Product> lstProducts = facturama.Products.List();
            Random random = new Random();
            var currency = facturama.Catalogs.Currencies.First(m => m.Value == "MXN");

            int nItems = random.Next(lstProducts.Count) % 10 + 1;
            int decimals = (int)currency.Decimals;


            // Lista de Items en el cfdi (los articulos a facturar)
            List<Item> lstItems = new List<Item>();

            // Creacion del CFDI 
            for (int i = lstProducts.Count - nItems; i < lstProducts.Count && i > 0; i++)
            {

                Product product = lstProducts.ElementAt(i);   // Un producto cualquiera
                int quantity = random.Next(1, 5) + 1;   // una cantidad aleatoria de elementos de este producto
                Double discount = Decimal.ToDouble(product.Price % (product.Price == 0 ? 1 : random.Next(1, Decimal.ToInt32(product.Price))));

                // Redondeo del precio del producto, de acuerdo a la moneda
                Double numberOfDecimals = Math.Pow(10, decimals);
                Double subTotal = Math.Round((Decimal.ToDouble(product.Price) * quantity) * numberOfDecimals) / numberOfDecimals;


                // Llenado del item (que va en el cfdi)
                Item item = new Item
                {
                    ProductCode = product.CodeProdServ,
                    UnitCode = product.UnitCode,
                    Unit = product.Unit,
                    Description = $"Item{i}: product.Description",
                    IdentificationNumber = product.IdentificationNumber,
                    Quantity = quantity,
                    Discount = Convert.ToDecimal(Math.Round(discount * numberOfDecimals) / numberOfDecimals),
                    UnitPrice = Convert.ToDecimal(Math.Round(Decimal.ToDouble(product.Price) * numberOfDecimals) / numberOfDecimals),
                    Subtotal = Convert.ToDecimal(subTotal)

                };

                // ---- Llenado de los impuestos del item ----                                    
                item = AddTaxesToItem(item, product, numberOfDecimals);
                if (item.UnitPrice > 0m)
                    lstItems.Add(item);
            }
            cfdi.Items = lstItems;

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

                tax.Name = pTax.Name;
                tax.IsQuota = pTax.IsQuota;
                tax.IsRetention = pTax.IsRetention;
                if (pTax.Rate < 1)
                {
                    pTax.Rate = 0;
                }

                Double rate = Decimal.ToDouble(pTax.Rate);
                Double rateRounded = (double)Math.Round(rate * 1000000) / 1000000;
                tax.Rate = Convert.ToDecimal(rateRounded);
                tax.Base = Convert.ToDecimal(Math.Round(decimal.ToDouble(item.Subtotal) * numberOfDecimals) / numberOfDecimals);
                tax.Total = Convert.ToDecimal(Math.Round((/*cambie el baseAmount*/decimal.ToDouble(tax.Base * pTax.Rate)) * numberOfDecimals) / numberOfDecimals);
                if (tax.Base >= 0m)
                    lstTaxes.Add(tax);
            }


            Double retentionsAmount = 0D;
            Double transfersAmount = 0D;

            // Asignación de los impuestos, en caso de que no se tengan, el campo va nulo
            if (lstTaxes.Count > 0)
            {
                item.Taxes = lstTaxes;

                retentionsAmount = item.Taxes.Where(o => o.IsRetention).Sum(o => Decimal.ToDouble(o.Total));
                transfersAmount = item.Taxes.Where(o => !o.IsRetention).Sum(o => Decimal.ToDouble(o.Total));
            }

            // Calculo del subtotal
            item.Total = (item.Subtotal - item.Discount + Convert.ToDecimal(transfersAmount) - Convert.ToDecimal(retentionsAmount));

            return item;

        }


        /**
        * Modelo "Complemento de pago"
        * - Se especifica: la moneda, método de pago, forma de pago, cliente, y lugar de expedición     
        */
        private static Facturama.Models.Request.Cfdi CreateModelCfdiPaymentComplement(FacturamaApi facturama, Facturama.Models.Response.Cfdi cfdiInicial)
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
                CfdiUse = "P01",
                Name = client.Name,
                Rfc = client.Rfc
            };
            cfdi.Receiver = receiver;

            // Lugar de expedición (es necesario por lo menos tener una sucursal)
            BranchOffice branchOffice = facturama.BranchOffices.List().First();
            cfdi.ExpeditionPlace = "78240";

            // Fecha y hora de expecidión del comprobante
            //DateTime bindingDate;
            //DateTime.TryParse(cfdiBinding.Date, null, DateTimeStyles.RoundtripKind, out bindingDate);
            
            cfdi.Date = null; // Puedes especificar una fecha por ejemplo:  DateTime.Now
            cfdi.CfdiType = CfdiType.Pago;
            // Complemento de pago ---
            Complement complement = new Complement();

            // Pueden representarse más de un pago en un solo CFDI
            List<Facturama.Models.Complements.Payment> lstPagos = new List<Facturama.Models.Complements.Payment>();
            Facturama.Models.Complements.Payment pago = new Facturama.Models.Complements.Payment();

            // Fecha y hora en que se registró el pago en el formato: "yyyy-MM-ddTHH:mm:ss" 
            // (la fecha del pago debe ser menor que la fecha en que se emite el CFDI)
            // Para este ejemplo, se considera que  el pago se realizó hace una hora   
            pago.Date = DateTime.Now.AddHours(-6).ToString("yyyy-MM-dd HH:mm:ss");


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
            List<Facturama.Models.Complements.RelatedDocument> lstRelatedDocuments = new List<Facturama.Models.Complements.RelatedDocument>();
            Facturama.Models.Complements.RelatedDocument relatedDocument = new Facturama.Models.Complements.RelatedDocument
            {
                Uuid = cfdiInicial.Complement.TaxStamp.Uuid, // "27568D31-E579-442F-BA77-798CBF30BD7D"
                Serie = "A",//cfdiInicial.Serie, // "EA"
                Folio = cfdiInicial.Folio, // 34853
                Currency = currency.Value,
                PaymentMethod = "PUE", // En el complemento de pago tiene que ser PUE
                PartialityNumber = 1,
                PreviousBalanceAmount = 100.00m,
                AmountPaid = 100.00m
            };
            lstRelatedDocuments.Add(relatedDocument);

            pago.RelatedDocuments = lstRelatedDocuments;

            lstPagos.Add(pago);

            complement.Payments = lstPagos;

            cfdi.Complement = complement;


            return cfdi;

        }


    }
}
