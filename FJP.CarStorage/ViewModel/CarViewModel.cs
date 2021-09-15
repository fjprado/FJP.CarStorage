using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.CarStorage.ViewModel
{
    public class CarViewModel
    {
        public Guid Id { get; set; }
        public string Chassis { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int ManufactureYear { get; set; }
        public int ModelYear { get; set; }
        public decimal? SalePrice { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal BuyPrice { get; set; }
        public DateTime BuyDate { get; set; }
    }
}
