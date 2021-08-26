using FJP.EstoqueVeiculos.Entities;
using FJP.EstoqueVeiculos.Exceptions;
using FJP.EstoqueVeiculos.InputModel;
using FJP.EstoqueVeiculos.Repositories;
using FJP.EstoqueVeiculos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.EstoqueVeiculos.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly Lazy<IVeiculoRepository> _veiculoRepository;

        public VeiculoService(
            Lazy<IVeiculoRepository> veiculoRepository
            )
        {
            _veiculoRepository = veiculoRepository;
        }

        public async Task Atualizar(Guid id, VeiculoInputModel veiculo)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Guid id, decimal valorVenda)
        {
            throw new NotImplementedException();
        }

        public async Task<VeiculoViewModel> Inserir(VeiculoInputModel veiculo)
        {
            var entidadeVeiculo = await _veiculoRepository.Value.ObterPorChassi(veiculo.Chassi);

            if (entidadeVeiculo != null)
                throw new VeiculoJaCadastradoException();

            var veiculoInsert = new Veiculo
            {
                Id = Guid.NewGuid(),
                Chassi = veiculo.Chassi,
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                AnoFabricacao = veiculo.AnoFabricacao,
                AnoModelo = veiculo.AnoModelo,
                ValorVenda = veiculo.ValorVenda,
                DataVenda = veiculo.DataVenda,
                ValorCompra = veiculo.ValorCompra,
                DataCompra = veiculo.DataCompra
            };

            await _veiculoRepository.Value.Inserir(veiculoInsert);

            return new VeiculoViewModel
            {
                Id = veiculoInsert.Id,
                Chassi = veiculo.Chassi,
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                AnoFabricacao = veiculo.AnoFabricacao,
                AnoModelo = veiculo.AnoModelo,
                ValorVenda = veiculo.ValorVenda,
                DataVenda = veiculo.DataVenda,
                ValorCompra = veiculo.ValorCompra,
                DataCompra = veiculo.DataCompra
            };
        }

        public async Task<List<VeiculoViewModel>> Obter(int pagina, int quantidade)
        {
            var veiculos = await _veiculoRepository.Value.Obter(pagina, quantidade);
            return veiculos.Select(veiculo => new VeiculoViewModel
            {
                Id = veiculo.Id,
                Chassi = veiculo.Chassi,
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                AnoFabricacao = veiculo.AnoFabricacao,
                AnoModelo = veiculo.AnoModelo,
                ValorVenda = veiculo.ValorVenda,
                DataVenda = veiculo.DataVenda,
                ValorCompra = veiculo.ValorCompra,
                DataCompra = veiculo.DataCompra
            }).ToList();
        }

        public async Task<VeiculoViewModel> Obter(Guid id)
        {
            var veiculo = await _veiculoRepository.Value.Obter(id);

            if (veiculo == null)
                return null;

            return new VeiculoViewModel
            {
                Id = veiculo.Id,
                Chassi = veiculo.Chassi,
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                AnoFabricacao = veiculo.AnoFabricacao,
                AnoModelo = veiculo.AnoModelo,
                ValorVenda = veiculo.ValorVenda,
                DataVenda = veiculo.DataVenda,
                ValorCompra = veiculo.ValorCompra,
                DataCompra = veiculo.DataCompra
            };
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Vender(Guid id, VeiculoInputModel veiculo)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _veiculoRepository.Value.Dispose();
        }
    }
}
