using FJP.CarStorage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.CarStorage.Repositories
{
    public interface ICarRepository : IDisposable
    {
        Task<List<Car>> GetList(int pagina, int quantidade);
        Task<Car> GetById(Guid id);
        Task<Car> GetByChassis(string chassi);
        Task<List<Car>> GetByModelAndBrand(string modelo, string marca);
        Task Insert(Car veiculo);
        Task Update(Car veiculo);
        Task Delete(Guid id);
        Task Sell(Car veiculo);
    }
}
