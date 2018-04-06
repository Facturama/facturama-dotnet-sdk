using Facturama;
using Facturama.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CRUDSucursales
    {
        [TestMethod]
        public void BranchOffice()
        {
            var facturama = new FacturamaApi("pruebas", "pruebas2011");
            var Cont = facturama.BranchOffices.List();
            var ContUno = Cont.Count;

            var sucursal = facturama.BranchOffices.Create(new BranchOffice
            {
                Address = new Address
                {
                    Country = "MEXICO",
                    ExteriorNumber = "1230",
                    InteriorNumber = "B",
                    Locality = "Aguascalientes",
                    Municipality = "Aguascalientes",
                    Neighborhood = "ojo caliente",
                    State = "Aguascalientes",
                    Street = "Cañada de Gomez",
                    ZipCode = "78000"
                },
                Name = "La Ofi",
                Description = "un lugar bonito pa trabajar",
            });

            Assert.IsNotNull(sucursal.Id);
            sucursal.Name = "Rancho La Chingada";
            facturama.BranchOffices.Update(sucursal, sucursal.Id);
            sucursal = facturama.BranchOffices.Retrieve(sucursal.Id);
            Assert.AreEqual(sucursal.Name = "Rancho La Chingada", sucursal.Name);
            facturama.BranchOffices.Remove(sucursal.Id);
            var Conta = facturama.BranchOffices.List();
            var ContDos = Conta.Count;
            Assert.AreEqual(ContUno, ContDos);
        }
    }
}
