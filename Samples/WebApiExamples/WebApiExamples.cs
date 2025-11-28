using System;
using System.Collections.Generic;
using System.Linq;
using Facturama;
using Facturama.Models;
using Facturama.Models.Complements.ForeignTrade;
using Facturama.Models.Request;

namespace WebApiExamples
{
    public class WebApiExamples
    {
        public static void RunExamples()
        {
            // Los datos de usuario y contraseña serán los mismos con los que creaste tu cuenta en Facturama,
            // Si aún no tienes cuenta en Facturama te recomiendo el crear una en ambiente de sandbox (pruebas)
            // https://dev.facturama.mx/api/registro

            var facturama = new FacturamaApi("tu_usuario", "tu_contraseña");

            //new CatalogsExample(facturama).Run();                           // CRUD  de clientes y productos
            new InvoiceExample(facturama).Run();                            // Creación de factura, descarga de XML y PDF, envío por correo
            //new PaymentComplementExample(facturama).Run();                  // Complemento de pago
            //new PayrollExample(facturama).Run();                            // Nómina
            //new EducationalInstitutionComplementExample(facturama).Run();   // Complemento IEDU - Instituciones educativas
            //new WaybillComplementExample(facturama).Run();                  // Complemento Carta Porte 2.0
            //new ForeignTradeExample(facturama).Run();
            // Además puedes editar el logo y agregar series a las sucursales como en los siguientes ejemplos            
            //TestLogo(facturama);
            //TestSerie(facturama);
            

            Console.ReadKey();
        }



        /// <summary>
        /// Solo aplica para "API Web"   https://apisandbox.facturama.mx/guias#api-modalidades
        /// Ejempo de actualización del logo (aplicable para el "perfil fiscal")
        /// Puedes obtener el mismo resultado modificando directamente el logo en  tu perfil fiscal
        /// https://apisandbox.facturama.mx/guias/perfil-fiscal
        /// </summary>
        /// <param name="facturama"></param>
      

        /// <summary>
        /// Solo aplica para "API Web"   https://apisandbox.facturama.mx/guias#api-modalidades
        /// Ejempo de actualización del logo (aplicable para el "perfil fiscal")
        /// Puedes obtener el mismo resultado agregando la serie mediante el perfil fiscal
        /// https://apisandbox.facturama.mx/guias/perfil-fiscal#lugares-expedicion-series
        /// </summary>
        /// <param name="facturama"></param>
        private static void TestSerie(FacturamaApi facturama)
        {
            var serie = new Serie
            {
                IdBranchOffice = "yzBJZx7uYQiX8FhGN_WIOQ2",
                Name = "TR" + DateTime.Now.ToString("MMddmmss"),
                Description = "A Nice Place to Work",
                Folio = 100
            };
            try
            {
                serie = facturama.Series.Create(serie);
                Console.WriteLine($"Se creo exitosamente la serie {serie.Name} en la sucursal {serie.IdBranchOffice}." );
                serie.IdBranchOffice = "yzBJZx7uYQiX8FhGN_WIOQ2";
                serie.Description = "Serie Editada"; //Solo la descripcion es editable
                facturama.Series.Update(serie);
                serie = facturama.Series.Retrieve(serie.IdBranchOffice, serie.Name);
                Console.WriteLine($"Se actualizó la descripcion de la serie: {serie.Name} descripcion: {serie.Description}");

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
