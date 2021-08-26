using FJP.EstoqueVeiculos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.EstoqueVeiculos.Repositories
{
    public interface IVeiculoRepository : IDisposable
    {
        Task<List<Veiculo>> Obter(int pagina, int quantidade);
        Task<Veiculo> Obter(Guid id);
        Task<Veiculo> ObterPorChassi(string chassi);
        Task<List<Veiculo>> Obter(string modelo, string marca);
        Task Inserir(Veiculo veiculo);
        Task Atualizar(Veiculo veiculo);
        Task Remover(Guid id);
        Task Vender(Veiculo veiculo);
    }
}
