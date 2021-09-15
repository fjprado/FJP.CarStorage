using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.CarStorage.InputModel
{
    public class CarInputModel
    {
        [Required]
        [StringLength(17, MinimumLength = 1, ErrorMessage = "Chassis must be unique and contains between 1 and 17 characters.")]
        public string Chassis { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Model must contains between 3 and 200 characters.")]
        public string Model { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Brand must contains between 3 e 50 characters.")]
        public string Brand { get; set; }
        [Required]
        [Range(1900, 2050, ErrorMessage = "Manufacture year must be between 1900 and 2050.")]
        public int ManufactureYear { get; set; }
        [Required]
        [Range(1900, 2050, ErrorMessage = "Manufacture year must be between 1900 and 2050.")]
        public int ModelYear { get; set; }
        [Range(1, 999999999999, ErrorMessage = "Sale price must be between R$ 1,00 and R$ 999.999.999.999,00.")]
        public decimal SalePrice { get; set; }
        [Range(typeof(DateTime), "1/2/2000", "31/12/2050", ErrorMessage = "Value from {0} must be between {1} and {2}.")]
        public DateTime SaleDate { get; set; }
        [Required]
        [Range(1, 999999999999, ErrorMessage = "Buy price must be between R$ 1,00 and R$ 999.999.999.999,00.")]
        public decimal BuyPrice { get; set; }
        [Required]
        [Range(typeof(DateTime), "1/2/2000", "31/12/2050", ErrorMessage = "Value from {0} must be between {1} and {2}.")]
        public DateTime BuyDate { get; set; }
    }

    public class CarSaleInputModel
    {
        [Required]
        [Range(1, 999999999999, ErrorMessage = "Sale price must be between R$ 1,00 and R$ 999.999.999.999,00.")]
        public decimal SalePrice { get; set; }
        [Required]
        [Range(typeof(DateTime), "1/2/2000", "31/12/2050", ErrorMessage = "Value from {0} must be between {1} and {2}.")]
        public DateTime SaleDate { get; set; }
    }
}
