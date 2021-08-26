using FJP.EstoqueVeiculos.InputModel;
using FJP.EstoqueVeiculos.Services;
using FJP.EstoqueVeiculos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.EstoqueVeiculos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly Lazy<IVeiculoService> _veiculoService;

        public VeiculosController(
            Lazy<IVeiculoService> veiculoService
            )
        {
            _veiculoService = veiculoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var veiculos = await _veiculoService.Value.Obter(pagina, quantidade);

            if(veiculos.Count == 0)
                return NoContent();

            return Ok();
        }

        [HttpGet("{idVeiculo:guid}")]
        public async Task<ActionResult<VeiculoViewModel>> Obter([FromRoute] Guid idVeiculo)
        {
            var veiculo = await _veiculoService.Value.Obter(idVeiculo);

            if (veiculo == null)
                return NoContent();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<VeiculoViewModel>> InserirVeiculo([FromBody] VeiculoInputModel veiculoInputModel)
        {
            try
            {
                var veiculo = await _veiculoService.Value.Inserir(veiculoInputModel);

                return Ok(veiculo);
            }
            //catch (VeiculoJaCadastradoException ex)
            catch(Exception ex)
            {
                return UnprocessableEntity("Já existe um veículo com o mesmo chassi cadastrado!");
            }
            
        }

        [HttpPut("{idVeiculo:guid}")]
        public async Task<ActionResult> AtualizarVeiculo([FromRoute] Guid idVeiculo, [FromBody] VeiculoInputModel veiculoInputModel)
        {
            try
            {
                await _veiculoService.Value.Atualizar(idVeiculo, veiculoInputModel);
                return Ok();
            }
            // catch(VeiculoNaoCadastradoException ex)
            catch (Exception)
            {
                return NotFound("Não existe este veículo");
            }
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
