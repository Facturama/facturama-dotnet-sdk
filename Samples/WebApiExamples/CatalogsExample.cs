using System;
using System.Collections.Generic;
using System.Linq;
using Facturama;
using Facturama.Models;
using Facturama.Models.Request;
using Facturama.Models.Complements.Payroll;
using PayrollIssuer = Facturama.Models.Complements.Payroll.Issuer;
using Newtonsoft.Json;

namespace WebApiExamples
{
    /**
    * Ejemplo de catálogos
    * Los catálogos son propios de la API Web (No existen para API Multiemisor)    
    * Referencia Productos: https://apisandbox.facturama.mx/guias/productos
    * Referencia Clientes: https://apisandbox.facturama.mx/guias/clientes    
    */
    class CatalogsExample
	{
        private readonly FacturamaApi facturama;
        public CatalogsExample(FacturamaApi facturama)
        {
             this.facturama = facturama;
        }

        /// <summary>
        /// Se recomienda usar el try - catch
        /// especialmente cuando se agrega o edita  el cliente o producto, ya que nos permite atrapar los errores en la estructura de los datos enviados
        /// </summary>
        public void Run()
        {
            try
            {
                Console.WriteLine("----- Inicio del ejemplo CatalogsExample -----");

                //CrudClientExample(facturama);
                //CrudProductExample(facturama);
                //CrudBranchOfficeExample(facturama);

            
                Console.WriteLine("----- Fin del ejemplo de CatalogsExample -----");
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


        /// <summary>
        /// CRUD  de ejemplo para clientes                
        /// </summary>
        /// <param name="facturama"></param>
        private void CrudClientExample(FacturamaApi facturama)
        {
            Console.WriteLine("----- CatalogsExample > CrudClientExample - Inicio -----");

            var clientes = facturama.Clients.List();            // Obtiene el listado de clientes
            var clientesBefore = clientes.Count;

            
            var cliente = facturama.Clients.Create(new Client  // Agrega un nuevo cliente
            {
                Id="",
                Email = "ejemplo@ejemplo.mx",
                EmailOp1=null,
                EmailOp2=null,
                Rfc = "IAÑL750210963",
                Name = "LUIS IAN ÑUZCO",
                CfdiUse = "G03",
                TaxResidence=null,
                FiscalRegime="605",
                TaxZipCode= "30230",
                Address = new Address
                {
                    Country = "MEXICO",
                    ExteriorNumber = "1230",
                    InteriorNumber = "B",
                    Locality = "San Luis",
                    Municipality = "San Luis Potosí",
                    Neighborhood = "Lomas Bonitas",
                    State = "San Luis Potosí",
                    Street = "Cañada de Lobos",
                    ZipCode = "78000"
                },
                
            });

            cliente = facturama.Clients.Retrieve(cliente.Id);   // Detalle de cliente
            cliente.Rfc = "IAÑL750210963";
            facturama.Clients.Update(cliente, cliente.Id);      // Actualizar datos
            cliente = facturama.Clients.Retrieve(cliente.Id);

            Console.WriteLine(cliente.Rfc == "IAÑL750210963" ? "Cliente Editado" : "Error al editar cliente");

            facturama.Clients.Remove(cliente.Id);               // Eliminar cliente

            clientes = facturama.Clients.List();
            var clientesAfter = clientes.Count;

            Console.WriteLine(clientesAfter == clientesBefore ? "Test Passed!" : "Test Failed!");            
            
            Console.WriteLine("----- CatalogsExample > CrudClientExample - Fin -----");
        }


        /// <summary>
        /// CRUD  de ejemplo para productos
        /// </summary>
        /// <param name="facturama"></param>
        private void CrudProductExample(FacturamaApi facturama)
        {
            Console.WriteLine("----- CatalogsExample > CrudProductExample - Inicio -----");

            var unit = facturama.Catalogs.Units("servicio")[0];
            var prod = facturama.Catalogs.ProductsOrServices("desarrollo")[0];
            var product = new Product
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
            };
            
            product = facturama.Products.Create(product);
            Console.WriteLine("Se creo exitosamente un producto con el id: " + product.Id);

            facturama.Products.Remove(product.Id);
            Console.WriteLine("Se elimino exitosamente un producto con el id: " + product.Id);            

            Console.WriteLine("----- CatalogsExample > CrudProductExample - Fin -----");
        }


        /// <summary>
        /// CRUD  de ejemplo para Lugar de Expedición
        /// </summary>
        /// <param name="facturama"></param>
        private void CrudBranchOfficeExample(FacturamaApi facturama)
        {
            Console.WriteLine("----- CatalogsExample > CrudBranchOffice - Inicio -----");


            var branchoffice = new BranchOffice
            {
                Name="Nueva Sucursal",
                Description="Descripcion de la sucursal",
                Address = new Address
                {
                    Street = "Av. del Sauce",
                    ExteriorNumber = "120",
                    InteriorNumber = "",
                    Neighborhood = "Las Flores",
                    ZipCode = "78116",
                    Locality = "",
                    Municipality = "San Luis Potosi",
                    State = "San Luis Potosi",
                    Country = "México"
                }

            };
            branchoffice= facturama.BranchOffices.Create(branchoffice);

            Console.WriteLine("Se creo exitosamente un producto con el id: " + branchoffice.Id);

            //facturama.Products.Remove(product.Id);
            //Console.WriteLine("Se elimino exitosamente un producto con el id: " + product.Id);

            Console.WriteLine("----- CatalogsExample > CrudProductExample - Fin -----");
        }



    }
}
