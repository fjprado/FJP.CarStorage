using FJP.CarStorage.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.CarStorage.Repositories
{
    public class CarSqlServerRepository : ICarRepository
    {
        private readonly SqlConnection sqlConnection;

        public CarSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task Update(Car car)
        {
            var query = $@"UPDATE cars
                        SET chassis = '{car.Chassis}',
                            model = '{car.Model}',
                            brand = '{car.Brand}',
                            manufactureyear = '{car.ManufactureYear}',
                            modelyear = '{car.Modelyear}',
                            saleprice = '{car.SalePrice.ToString().Replace(",", ".")}',
                            saledate = '{car.SaleDate}',
                            buyprice = '{car.BuyPrice.ToString().Replace(",", ".")}',
                            buydate = '{car.BuyDate}'
                        WHERE id = '{car.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Insert(Car car)
        {
            var query = $@"INSERT cars
                        VALUES ('{car.Id}', 
                                '{car.Chassis}',
                                '{car.Model}',
                                '{car.Brand}',
                                '{car.ManufactureYear}',
                                '{car.Modelyear}',
                                '{car.SalePrice.ToString().Replace(",", ".")}',
                                '{car.SaleDate}',
                                '{car.BuyPrice.ToString().Replace(",", ".")}',
                                '{car.BuyDate}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task<List<Car>> GetList(int pagina, int quantidade)
        {
            var cars = new List<Car>();

            var query = $@"SELECT *
                        FROM cars
                        ORDER BY id
                        OFFSET {(pagina - 1) * quantidade} ROWS 
                        FECTH NEXT {quantidade} ROWS ONLY";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                cars.Add(new Car
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Chassis = (string)sqlDataReader["Chassis"],
                    Model = (string)sqlDataReader["Model"],
                    Brand = (string)sqlDataReader["Brand"],
                    ManufactureYear = (int)sqlDataReader["ManufactureYear"],
                    Modelyear = (int)sqlDataReader["ModelYear"],
                    SalePrice = (decimal)sqlDataReader["SalePrice"],
                    SaleDate = (DateTime)sqlDataReader["SaleDate"],
                    BuyPrice = (decimal)sqlDataReader["BuyPrice"],
                    BuyDate = (DateTime)sqlDataReader["BuyDate"]
                });
            }

            await sqlConnection.CloseAsync();

            return cars;
        }

        public async Task<Car> GetById(Guid id)
        {
            Car car = null;

            var query = $@"SELECT *
                        FROM cars
                        WHERE id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                car = new Car
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Chassis = (string)sqlDataReader["Chassis"],
                    Model = (string)sqlDataReader["Model"],
                    Brand = (string)sqlDataReader["Brand"],
                    ManufactureYear = (int)sqlDataReader["ManufactureYear"],
                    Modelyear = (int)sqlDataReader["ModelYear"],
                    SalePrice = (decimal)sqlDataReader["SalePrice"],
                    SaleDate = (DateTime)sqlDataReader["SaleDate"],
                    BuyPrice = (decimal)sqlDataReader["BuyPrice"],
                    BuyDate = (DateTime)sqlDataReader["BuyDate"]
                };
            }

            await sqlConnection.CloseAsync();

            return car;
        }

        public async Task<List<Car>> GetByModelAndBrand(string model, string brand)
        {
            var cars = new List<Car>();

            var query = $@"SELECT *
                        FROM cars
                        WHERE model = '{model}'
                        AND brand = '{brand}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                cars.Add(new Car
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Chassis = (string)sqlDataReader["Chassis"],
                    Model = (string)sqlDataReader["Model"],
                    Brand = (string)sqlDataReader["Brand"],
                    ManufactureYear = (int)sqlDataReader["ManufactureYear"],
                    Modelyear = (int)sqlDataReader["ModelYear"],
                    SalePrice = (decimal)sqlDataReader["SalePrice"],
                    SaleDate = (DateTime)sqlDataReader["SaleDate"],
                    BuyPrice = (decimal)sqlDataReader["BuyPrice"],
                    BuyDate = (DateTime)sqlDataReader["BuyDate"]
                });
            }

            await sqlConnection.CloseAsync();

            return cars;
        }

        public async Task<Car> GetByChassis(string chassis)
        {
            Car car = null;

            var query = $@"SELECT *
                        FROM cars
                        WHERE chassis = '{chassis}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                car = new Car
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Chassis = (string)sqlDataReader["Chassis"],
                    Model = (string)sqlDataReader["Model"],
                    Brand = (string)sqlDataReader["Brand"],
                    ManufactureYear = (int)sqlDataReader["ManufactureYear"],
                    Modelyear = (int)sqlDataReader["ModelYear"],
                    SalePrice = (decimal)sqlDataReader["SalePrice"],
                    SaleDate = (DateTime)sqlDataReader["SaleDate"],
                    BuyPrice = (decimal)sqlDataReader["BuyPrice"],
                    BuyDate = (DateTime)sqlDataReader["BuyDate"]
                };
            }

            await sqlConnection.CloseAsync();

            return car;
        }

        public async Task Delete(Guid id)
        {
            var query = $@"DELETE FROM cars
                        WHERE id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Sell(Car car)
        {
            var query = $@"UPDATE cars
                        SET saleprice = '{car.SalePrice.ToString().Replace(",", ".")}',
                            saledate = '{car.SaleDate}'
                        WHERE id = '{car.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
