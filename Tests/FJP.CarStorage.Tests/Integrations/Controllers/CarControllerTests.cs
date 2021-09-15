using FJP.CarStorage.InputModel;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace FJP.CarStorage.Tests.Integrations.Controllers
{
    public class CarControllerTests
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;

        //In constructor must have WebApplicationFactory appointing to Startup from API (add reference)
        public CarControllerTests(WebApplicationFactory<Startup> factory)
        {
            this._factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public void Logar()
        {
            var veiculoInput = new CarInputModel()
            {
                Chassis = "abc123def456",
                Model = "Sandero",
                Brand = "Renault",
                ManufactureYear = 2017,
                ModelYear = 2017,
                BuyPrice = 21000,
                BuyDate = new DateTime(2018, 10, 01)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(veiculoInput));
            var httpClientRequest = _httpClient.PostAsync("api/v1/veiculos/InserirVeiculo", content).GetAwaiter().GetResult();

            //"The best assert (situation) must be..."
            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
        }
    }
}
