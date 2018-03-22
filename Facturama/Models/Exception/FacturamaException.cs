using System;
using Facturama.Models.Exception;

namespace Facturama
{
    public class FacturamaException : Exception
    {
        public ModelException Model { get; }

        public FacturamaException(string message)
            : base(message)
        {

        }

        public FacturamaException(string message, ModelException model)
            : base(message)
        {
            Model = model;
        }
    }
}
