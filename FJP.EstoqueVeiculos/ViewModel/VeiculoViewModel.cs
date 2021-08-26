using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.EstoqueVeiculos.ViewModel
{
    public class VeiculoViewModel
    {
        public Guid Id { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCompra { get; set; }
    }
}
