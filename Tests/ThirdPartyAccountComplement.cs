using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturama;
using Facturama.Models;
using Facturama.Models.Complements;
using Facturama.Models.Complements.ThirdPartyAccount;
using Facturama.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tax = Facturama.Models.Request.Tax;

namespace Tests
{
	[TestClass]
	public class ThirdPartyAccountComplement
	{

		[TestMethod]
		public void Test()
		{
			var facturama = new FacturamaApi("pruebas", "pruebas2011");
			var cfdi = new Cfdi
			{
				Serie = "R",
				Currency = "MXN",
				ExpeditionPlace = "78116",
				PaymentConditions = "CREDITO A SIETE DIAS",
				CfdiType = CfdiType.Ingreso,
				PaymentForm = "03",
				PaymentMethod = "PUE",
				Receiver = new Receiver
				{
					Rfc = "EKU9003173C9",
					Name = "RADIAL SOFTWARE SOLUTIONS",
					CfdiUse = "P01"
				},
				Items = new List<Item>
				{
					new Item
					{
						ProductCode = "10101504",
						IdentificationNumber = "EDL",
						Description = "Estudios de viabilidad",
						Unit = "NO APLICA",
						UnitCode = "MTS",
						UnitPrice = 50.00m,
						Quantity = 2.00m,
						Subtotal = 100.00m,
						Taxes = new List<Tax>
						{
							new Tax
							{

								Total = 16.00m,
								Name = "IVA",
								Base = 100.00m,
								Rate = 0.160000m,
								IsRetention = false
							}
						},
						Total = 116.0m,
						Complement = new ItemComplement
						{
							ThirdPartyAccount = new ThirdPartyAccount
							{
								Rfc = "ESO1202108R2",
								Name = "Expresión en Software",
								Taxes = new List<ThirdPartyAccountTax>
								{
									new ThirdPartyAccountTax
									{
										Name = "IVA",
										Rate = 0.16m,
										Amount = 1000.00m
									}
								}
							}
						}
					}
				}
			};
			var cfdiCreated = facturama.Cfdis.Create(cfdi);

			Assert.IsTrue(Guid.TryParse(cfdiCreated.Complement.TaxStamp.Uuid, out _));

		}

	}
}
