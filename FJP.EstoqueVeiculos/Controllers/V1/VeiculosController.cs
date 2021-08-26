using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.EstoqueVeiculos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<object>>> Obter()
        {
            return Ok();
        }

        [HttpGet("{idVeiculo:guid}")]
        public async Task<ActionResult<object>> Obter(Guid idVeiculo)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<object>> InserirVeiculo(object veiculo)
        {
            return Ok();
        }

        [HttpPut("{idVeiculo:guid}")]
        public async Task<ActionResult> AtualizarVeiculo(Guid idVeiculo, object veiculo)
        {
            return Ok();
        }

        [HttpPatch("{idVeiculo:guid}/valorvenda/{valorVenda:decimal}")]
        public async Task<ActionResult> AtualizarVeiculo(Guid idVeiculo, decimal valorVenda)
        {
            return Ok();
        }

        [HttpDelete("{idVeiculo:guid}")]
        public async Task<ActionResult> ApagarVeiculo(Guid idVeiculo)
        {
            return Ok();
        }
    }
}
