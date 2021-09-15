using FJP.CarStorage.Exceptions;
using FJP.CarStorage.InputModel;
using FJP.CarStorage.Services;
using FJP.CarStorage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FJP.CarStorage.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(
            ICarService carService
            )
        {
            _carService = carService;
        }

        /// <summary>
        /// Search for all car by pages
        /// </summary>
        /// <remarks>
        /// It doesn't possible recover cars without pagination
        /// </remarks>
        /// <param name="page">Designate which page is being consulted. Min 1</param>
        /// <param name="quantity">Designate how many data by page. Min 1 e max 50</param>
        /// <response code="200">Returns the cars list</response>
        /// <response code="204">If it doesn't have cars</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarViewModel>>> GetList([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var cars = await _carService.GetList(page, quantity);

            if(cars.Count == 0)
                return NoContent();

            return Ok();
        }

        /// <summary>
        /// Find a car by your chassi
        /// </summary>
        /// <param name="chassis">The chassis of the wanted car</param>
        /// <response code="200">Returns the filtered car</response>
        /// <response code="204">If it doesn't have car with this chassi</response> 
        [HttpGet("{chassis:string}")]
        public async Task<ActionResult<CarViewModel>> GetByChassis([FromRoute] string chassis)
        {
            var car = await _carService.GetByChassis(chassis);

            if (car == null)
                return NoContent();

            return Ok();
        }

        /// <summary>
        /// Insert a car on storage
        /// </summary>
        /// <param name="carInputModel">Car data to be inserted</param>
        /// <response code="200">If the car is sucessfully inserted</response>
        /// <response code="422">If already exists a car with the same chassis</response>
        [HttpPost]
        public async Task<ActionResult<CarViewModel>> Insert([FromBody] CarInputModel carInputModel)
        {
            try
            {
                var car = await _carService.Insert(carInputModel);

                return Ok(car);
            }
            catch (CarAlreadyExistsException ex)
            {
                return UnprocessableEntity("Already exists a car with the same chassis!");
            }
            
        }

        /// <summary>
        /// Update a car on storage
        /// </summary>
        /// /// <param name="idCar">Id from car to be updated</param>
        /// <param name="carInputModel">New data to update the appointed car</param>
        /// <response code="200">If the car is sucessfully updated</response>
        /// <response code="404">If doesn't exists a car with appointed Id</response>  
        [HttpPut("{idCar:guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid idCar, [FromBody] CarInputModel carInputModel)
        {
            try
            {
                await _carService.Update(idCar, carInputModel);
                return Ok();
            }
            catch(CarDoesntExistsException ex)
            {
                return NotFound("This car doesn't exists!");
            }
        }

        /// <summary>
        /// Update a car price
        /// </summary>
        /// /// <param name="idCar">Id from car to be updated</param>
        /// <param name="price">New price to update the appointed car</param>
        /// <response code="200">If the car price is sucessfully updated</response>
        /// <response code="404">If doesn't exists a car with appointed Id</response> 
        [HttpPatch("{idCar:guid}/price/{price:decimal}")]
        public async Task<ActionResult> UpdatePrice([FromRoute] Guid idCar, [FromRoute] decimal price)
        {
            try
            {
                await _carService.Update(idCar, price);
                return Ok();
            }
            catch(CarDoesntExistsException ex)
            {
                return NotFound("This car doesn't exists!");
            }
        }

        /// <summary>
        /// Remove a car
        /// </summary>
        /// /// <param name="idCar">Id from car to be removed</param>
        /// <response code="200">If the car is sucessfully removed</response>
        /// <response code="404">If doesn't exists a car with appointed Id</response>
        [HttpDelete("{idCar:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid idCar)
        {
            try
            {
                await _carService.Delete(idCar);
                return Ok();
            }
            catch(CarDoesntExistsException ex)
            {
                return NotFound("This car doesn't exists!");
            }
        }

        /// <summary>
        /// Sell a car
        /// </summary>
        /// /// <param name="chassis">The chassis of the car to be sold</param>
        /// <param name="carData">Designate date and sale price of car</param>
        /// <response code="200">If the car is sucessfully sold</response>
        /// <response code="404">If doesn't exists a car with appointed chassis</response> 
        [HttpPut("{chassis:string}")]
        public async Task<ActionResult> SellCar([FromRoute] string chassis, [FromBody] CarSaleInputModel carData)
        {
            try
            {
                await _carService.Sell(chassis, carData.SaleDate, carData.SalePrice);
                return Ok();
            }
            catch(CarDoesntExistsException ex)
            {
                return NotFound("This car doesn't exists!");
            }
        }
    }
}
