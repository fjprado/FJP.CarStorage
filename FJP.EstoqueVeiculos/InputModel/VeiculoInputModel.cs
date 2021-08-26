using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.EstoqueVeiculos.InputModel
{
    public class VeiculoInputModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O modelo do veículo deve conter entre 3 e 200 caracteres")]
        public string Modelo { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "A marca do veículo deve conter entre 3 e 50 caracteres")]
        public string Marca { get; set; }
        [Required]
        [Range(1900, 2050, ErrorMessage = "O ano de fabricação do veículo deve ser entre 1900 e 2050")]
        public int AnoFabricacao { get; set; }
        [Required]
        [Range(1900, 2050, ErrorMessage = "O ano de modelo do veículo deve ser entre 1900 e 2050")]
        public int AnoModelo { get; set; }
        [Required]
        [Range(1, 999999999999, ErrorMessage = "O valor de venda do veículo deve ser entre R$ 1,00 e R$ 999.999.999.999,00")]
        public decimal ValorVenda { get; set; }
        [Required]
        [Range(1, 999999999999, ErrorMessage = "O valor de compra do veículo deve ser entre R$ 1,00 e R$ 999.999.999.999,00")]
        public decimal ValorCompra { get; set; }
    }
}
