using FJP.CarStorage.InputModel;
using FJP.CarStorage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.CarStorage.Services
{
    public interface ICarService : IDisposable
    {
        Task<List<CarViewModel>> GetList(int pagina, int quantidade);
        Task<CarViewModel> GetById(Guid id);
        Task<CarViewModel> GetByChassis(string chassi);
        Task<CarViewModel> Insert(CarInputModel veiculo);
        Task Update(Guid id, CarInputModel veiculo);
        Task Update(Guid id, decimal valorVenda);
        Task Delete(Guid id);
        Task Sell(string chassi, DateTime dataVenda, decimal valorVenda);
    }
}
