using System;
using Facturama;
using Facturama.Models;
using Facturama.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias
{
    [TestClass]
    public class CRUDSerie
    {
        [TestMethod]
        public void SerieTest()
        {
            var facturama = new FacturamaApi("pruebas", "pruebas2011");
            var serie = (new Serie
            {
                IdBranchOffice = "G8H81UGPAcqAXVXxtfhGyw2",
                Name = "Trabajo",
                Description = "A Nice Place to Work",
                Folio = 100
            });

            serie = facturama.Series.Create(serie);
            Assert.IsNull(serie.IdBranchOffice);
            serie.Folio = 105;
            serie.IdBranchOffice = "G8H81UGPAcqAXVXxtfhGyw2";
            serie = facturama.Series.Retrieve(serie.IdBranchOffice, serie.Name);
            Assert.AreEqual(serie.Folio = 99, serie.Folio);
        }
    }
}
