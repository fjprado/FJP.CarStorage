using FJP.EstoqueVeiculos.Entities;
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

        public async Task Atualizar(Veiculo veiculo)
        {
            var query = $@"UPDATE veiculos
                        SET chassi = '{veiculo.Chassi}',
                            modelo = '{veiculo.Modelo}',
                            marca = '{veiculo.Marca}',
                            anofabricacao = '{veiculo.AnoFabricacao}',
                            anomodelo = '{veiculo.AnoModelo}',
                            valorvenda = '{veiculo.ValorVenda.ToString().Replace(",", ".")}',
                            datavenda = '{veiculo.DataVenda}',
                            valorcompra = '{veiculo.ValorCompra.ToString().Replace(",", ".")}',
                            datavenda = '{veiculo.DataCompra}'
                        WHERE id = '{veiculo.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Inserir(Veiculo veiculo)
        {
            var query = $@"INSERT veiculos
                        VALUES ('{veiculo.Id}', 
                                '{veiculo.Chassi}',
                                '{veiculo.Modelo}',
                                '{veiculo.Marca}',
                                '{veiculo.AnoFabricacao}',
                                '{veiculo.AnoModelo}',
                                '{veiculo.ValorVenda.ToString().Replace(",", ".")}',
                                '{veiculo.DataVenda}',
                                '{veiculo.ValorCompra.ToString().Replace(",", ".")}',
                                '{veiculo.DataCompra}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
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

        public async Task<Veiculo> Obter(Guid id)
        {
            Veiculo veiculo = null;

            var query = $@"SELECT *
                        FROM veiculos
                        WHERE id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                veiculo = new Veiculo
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
                };
            }

            await sqlConnection.CloseAsync();

            return veiculo;
        }

        public async Task<List<Veiculo>> Obter(string modelo, string marca)
        {
            var veiculos = new List<Veiculo>();

            var query = $@"SELECT *
                        FROM veiculos
                        WHERE modelo = '{modelo}'
                        AND marca = '{marca}'";

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

        public async Task<Veiculo> ObterPorChassi(string chassi)
        {
            Veiculo veiculo = null;

            var query = $@"SELECT *
                        FROM veiculos
                        WHERE chassi = '{chassi}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                veiculo = new Veiculo
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
                };
            }

            await sqlConnection.CloseAsync();

            return veiculo;
        }

        public async Task Remover(Guid id)
        {
            var query = $@"DELETE FROM veiculos
                        WHERE id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Vender(Veiculo veiculo)
        {
            var query = $@"UPDATE veiculos
                        SET valorvenda = '{veiculo.ValorVenda.ToString().Replace(",", ".")}',
                            datavenda = '{veiculo.DataVenda}'
                        WHERE id = '{veiculo.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
