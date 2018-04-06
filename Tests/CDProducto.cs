using System;
using Facturama;
using Facturama.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tax = Facturama.Models.Request.Tax;

namespace Tests
{
    [TestClass]
    public class CDProducto
    {
        [TestMethod]
        public void CreaYBorraProducto()
        {
            var facturama = new FacturamaApi("pruebas", "pruebas2011");
            var productOs = facturama.Clients.List();
            var productOsAntes = productOs.Count;

            var unit = facturama.Catalogs.Units("servicio")[0];
            var prod = facturama.Catalogs.ProductsOrServices("desarrollo")[0];

            var product = facturama.Products.Create(new Product
            {
                Unit = "Servicio",
                UnitCode = unit.Value,
                IdentificationNumber = "WEB003",
                Name = "Sitio Web CMS",
                Description = "Desarrollo e implementación de sitio web empleando un CMS",
                Price = 6500.0m,
                CodeProdServ = prod.Value,
                CuentaPredial = "123",
                Taxes = new[]
                {
                    new Tax
                    {
                        Name = "IVA",
                        Rate = 0.16m,
                        IsRetention = false,
                    },
                    new Tax
                    {
                        Name = "ISR",
                        IsRetention = true,
                        Total = 0.1m
                    },
                    new Tax
                    {
                        Name = "IVA",
                        IsRetention = true,
                        Total = 0.106667m
                    }
                }
            });

            try
            {
                
                Assert.IsNotNull(product.Id);

                //<-------------------update and retrive----------------------------------->>
                product.CuentaPredial = "420";
                product = facturama.Products.Update(product, product.Id);
                Assert.AreEqual(product.CuentaPredial = "420", product.CuentaPredial);
                product = facturama.Products.Retrieve(product.Id);
                Assert.IsNotNull(product.Id);
                
                facturama.Products.Remove(product.Id);
                productOs = facturama.Clients.List();
                var productOsFinales = productOs.Count;

                Assert.AreEqual(productOsFinales, productOsAntes);
                
            }
            catch (FacturamaException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var messageDetail in ex.Model.Details)
                {
                    Console.WriteLine($"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}");
                }
            }
        }
    }
}
