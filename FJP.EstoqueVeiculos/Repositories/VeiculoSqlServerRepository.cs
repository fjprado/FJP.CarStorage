﻿using FJP.EstoqueVeiculos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.EstoqueVeiculos.Repositories
{
    public class VeiculoSqlServerRepository : IVeiculoRepository
    {
        private readonly SqlConnection sqlConnection;

        public VeiculoSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public Task Atualizar(Veiculo veiculo)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task Inserir(Veiculo veiculo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Veiculo>> Obter(int pagina, int quantidade)
        {
            var veiculos = new List<Veiculo>();

            var query = $@"SELECT *
                        FROM veiculos
                        ORDER BY id
                        OFFSET {(pagina - 1) * quantidade} ROWS 
                        FECTH NEXT {quantidade} ROWS ONLY";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                veiculos.Add(new Veiculo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Chassi = (string)sqlDataReader["Chassi"],
                    Modelo = (string)sqlDataReader["Modelo"],
                    Marca = (string)sqlDataReader["Marca"],
                    AnoFabricacao = (int)sqlDataReader["AnoFabricacao"],
                    AnoModelo = (int)sqlDataReader["AnoModelo"],
                    ValorVenda = (decimal)sqlDataReader["ValorVenda"],
                    DataVenda = (DateTime)sqlDataReader["DataVenda"],
                    ValorCompra = (decimal)sqlDataReader["ValorCompra"],
                    DataCompra = (DateTime)sqlDataReader["DataCompra"]
                });
            }

            await sqlConnection.CloseAsync();

            return veiculos;
        }

        public Task<Veiculo> Obter(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Veiculo>> Obter(string modelo, string marca)
        {
            throw new NotImplementedException();
        }

        public Task<Veiculo> ObterPorChassi(string chassi)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Vender(Veiculo veiculo)
        {
            throw new NotImplementedException();
        }
    }
}
