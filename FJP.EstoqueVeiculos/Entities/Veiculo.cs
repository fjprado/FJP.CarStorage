using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.EstoqueVeiculos.Entities
{
    public class Veiculo
    {
        public Guid Id { get; set; }
        public string Chassi { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public decimal? ValorVenda { get; set; }
        public DateTime? DataVenda { get; set; }
        public decimal ValorCompra { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
