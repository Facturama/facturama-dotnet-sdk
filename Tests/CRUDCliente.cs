using Facturama;
using Facturama.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CRUDCliente
    {

        [TestMethod]
        public void CrudCliente()
        {
            var facturama = new FacturamaApi("pruebas", "pruebas2011");
            var clientes = facturama.Clients.List();
            var clientesBefore = clientes.Count;

            var cliente = facturama.Clients.Create(new Client
            {
                Address = new Address
                {
                    Country = "MEXICO",
                    ExteriorNumber = "1230",
                    InteriorNumber = "B",
                    Locality = "San Luis",
                    Municipality = "San Luis Potosí",
                    Neighborhood = "Lomas 4ta",
                    State = "San Luis Potosí",
                    Street = "Cañada de Gomez",
                    ZipCode = "78220"
                },
                CfdiUse = "P01",
                Email = "diego@facturama.com.mx",
                Rfc = "ESO1202108R2",
                Name = "Expresion en Software"
            });

            cliente = facturama.Clients.Retrieve(cliente.Id);
            cliente.Rfc = "XAXX010101000";
            facturama.Clients.Update(cliente, cliente.Id);
            cliente = facturama.Clients.Retrieve(cliente.Id);

            Assert.AreEqual(cliente.Rfc = "XAXX010101000", cliente.Rfc);
            facturama.Clients.Remove(cliente.Id);

            clientes = facturama.Clients.List();
            var clientesAfter = clientes.Count;

            Assert.AreEqual(clientesAfter, clientesBefore);
        }
    }
}
