using System;
using Facturama;
using Facturama.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
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
                IdBranchOffice = "yzBJZx7uYQiX8FhGN_WIOQ2",
                Name = "TR" + DateTime.Now.ToString("MMddmmss"),
                Description = "A Nice Place to Work",
                Folio = 100
            });

            serie = facturama.Series.Create(serie);
            Assert.IsNull(serie.IdBranchOffice);
            serie.IdBranchOffice = "yzBJZx7uYQiX8FhGN_WIOQ2";
            serie.Description = "Serie Editada"; //Solo la descripcion es editable
            facturama.Series.Update(serie);
            serie = facturama.Series.Retrieve(serie.IdBranchOffice, serie.Name);

            Assert.AreEqual("Serie Editada", serie.Description);

            Assert.AreEqual(serie.Folio = 99, serie.Folio);
        }
    }
}
