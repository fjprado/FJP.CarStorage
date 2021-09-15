using FJP.CarStorage.Entities;
using FJP.CarStorage.Exceptions;
using FJP.CarStorage.InputModel;
using FJP.CarStorage.Repositories;
using FJP.CarStorage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.CarStorage.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(
            ICarRepository carRepository
            )
        {
            _carRepository = carRepository;
        }

        public async Task Update(Guid id, CarInputModel car)
        {
            var entityCar = await _carRepository.GetById(id);

            if (entityCar == null)
                throw new CarDoesntExistsException();

            entityCar.Chassis = car.Chassis;
            entityCar.Model = car.Model;
            entityCar.Brand = car.Brand;
            entityCar.ManufactureYear = car.ManufactureYear;
            entityCar.Modelyear = car.ModelYear;
            entityCar.SalePrice = car.SalePrice;
            entityCar.SaleDate = car.SaleDate;
            entityCar.BuyPrice = car.BuyPrice;
            entityCar.BuyDate = car.BuyDate;

            await _carRepository.Update(entityCar);
        }

        public async Task Update(Guid id, decimal salePrice)
        {
            var entityCar = await _carRepository.GetById(id);

            if (entityCar == null)
                throw new CarDoesntExistsException();

            entityCar.SalePrice = salePrice;

            await _carRepository.Update(entityCar);
        }

        public async Task<CarViewModel> Insert(CarInputModel car)
        {
            var entityCar = await _carRepository.GetByChassis(car.Chassis);

            if (entityCar != null)
                throw new CarAlreadyExistsException();

            var carInsert = new Car
            {
                Id = Guid.NewGuid(),
                Chassis = car.Chassis,
                Model = car.Model,
                Brand = car.Brand,
                ManufactureYear = car.ManufactureYear,
                Modelyear = car.ModelYear,
                SalePrice = car.SalePrice,
                SaleDate = car.SaleDate,
                BuyPrice = car.BuyPrice,
                BuyDate = car.BuyDate
            };

            await _carRepository.Insert(carInsert);

            return new CarViewModel
            {
                Id = carInsert.Id,
                Chassis = car.Chassis,
                Model = car.Model,
                Brand = car.Brand,
                ManufactureYear = car.ManufactureYear,
                ModelYear = car.ModelYear,
                SalePrice = car.SalePrice,
                SaleDate = car.SaleDate,
                BuyPrice = car.BuyPrice,
                BuyDate = car.BuyDate
            };
        }

        public async Task<List<CarViewModel>> GetList(int page, int quantity)
        {
            var cars = await _carRepository.GetList(page, quantity);
            return cars.Select(car => new CarViewModel
            {
                Id = car.Id,
                Chassis = car.Chassis,
                Model = car.Model,
                Brand = car.Brand,
                ManufactureYear = car.ManufactureYear,
                ModelYear = car.Modelyear,
                SalePrice = car.SalePrice,
                SaleDate = car.SaleDate,
                BuyPrice = car.BuyPrice,
                BuyDate = car.BuyDate
            }).ToList();
        }

        public async Task<CarViewModel> GetById(Guid id)
        {
            var car = await _carRepository.GetById(id);

            if (car == null)
                return null;

            return new CarViewModel
            {
                Id = car.Id,
                Chassis = car.Chassis,
                Model = car.Model,
                Brand = car.Brand,
                ManufactureYear = car.ManufactureYear,
                ModelYear = car.Modelyear,
                SalePrice = car.SalePrice,
                SaleDate = car.SaleDate,
                BuyPrice = car.BuyPrice,
                BuyDate = car.BuyDate
            };
        }

        public async Task<CarViewModel> GetByChassis(string chassis)
        {
            var car = await _carRepository.GetByChassis(chassis);

            if (car == null)
                return null;

            return new CarViewModel
            {
                Id = car.Id,
                Chassis = car.Chassis,
                Model = car.Model,
                Brand = car.Brand,
                ManufactureYear = car.ManufactureYear,
                ModelYear = car.Modelyear,
                SalePrice = car.SalePrice,
                SaleDate = car.SaleDate,
                BuyPrice = car.BuyPrice,
                BuyDate = car.BuyDate
            };
        }

        public async Task Delete(Guid id)
        {
            var car = await _carRepository.GetById(id);

            if (car == null)
                throw new CarDoesntExistsException();

            await _carRepository.Delete(id);
        }

        public async Task Sell(string chassis, DateTime saleDate, decimal salePrice)
        {
            var entityCar = await _carRepository.GetByChassis(chassis);

            if (entityCar == null)
                throw new CarDoesntExistsException();

            entityCar.SalePrice = salePrice;
            entityCar.SaleDate = saleDate;

            await _carRepository.Sell(entityCar);
        }

        public void Dispose()
        {
            _carRepository.Dispose();
        }
    }
}
