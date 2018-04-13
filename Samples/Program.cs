using System;
using System.Collections.Generic;
using System.Linq;
using Facturama;
using Facturama.Models;
using Facturama.Models.Request;
using Cfdi = Facturama.Models.Request.Cfdi;
using Csd = Facturama.Models.Request.Csd;
using Item = Facturama.Models.Request.Item;
using Receiver = Facturama.Models.Request.Receiver;
using Tax = Facturama.Models.Request.Tax;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            EjemplosMultiEmisor.RunExamples();
            EjemplosWebApi.RunExamples();
        }
    }
}