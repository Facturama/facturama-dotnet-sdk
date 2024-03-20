using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Complements.Waybill
{
    public class TransporteFerroviario
	{
        public string TipoDeServicio { get; set; }
        public string TipoDeTrafico { get; set; }
        public string NombreAseg { get; set; }
        public string NumPolizaSeguro { get; set; }
        public string Concesionario { get; set; }
        public DerechosDePaso[] DerechosDePaso { get; set; }
        
        public Carro[] Carro { get; set; }
    }
    public class DerechosDePaso
    {
        public string TipoDerechoDePaso { get; set; }
        
        public string KilometrajePagado { get; set; }
    }
    public class Carro
    {
        public string TipoCarro { get; set; }
        
        public string MatriculaCarro { get; set; }
        
        public string GuiaCarro { get; set; }
        
        public decimal ToneladasNetasCarro { get; set; }

        public CarroContenedor[] Contenedor { get; set; }
    }
    public class CarroContenedor
    {
        public string TipoContenedor { get; set; }
        
        public decimal PesoContenedorVacio { get; set; }
        
        public decimal PesoNetoMercancia { get; set; }
    }
}
