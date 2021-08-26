using FJP.EstoqueVeiculos.InputModel;
using FJP.EstoqueVeiculos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.EstoqueVeiculos.Services
{
    public interface IVeiculoService
    {
        Task<List<VeiculoViewModel>> Obter(int pagina, int quantidade);
        Task<VeiculoViewModel> Obter(Guid id);
        Task<VeiculoViewModel> Inserir(VeiculoInputModel veiculo);
        Task Atualizar(Guid id, VeiculoInputModel veiculo);
        Task Atualizar(Guid id, decimal valorVenda);
        Task Remover(Guid id);
        Task Vender(Guid id, VeiculoInputModel veiculo);
    }
}
