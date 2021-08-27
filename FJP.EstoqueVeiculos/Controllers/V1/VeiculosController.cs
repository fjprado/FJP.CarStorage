using FJP.EstoqueVeiculos.Exceptions;
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

        /// <summary>
        /// Buscar todos os veículos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os veículos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de veículos</response>
        /// <response code="204">Caso não haja veículos</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var veiculos = await _veiculoService.Value.Obter(pagina, quantidade);

            if(veiculos.Count == 0)
                return NoContent();

            return Ok();
        }

        /// <summary>
        /// Buscar um veiculo pelo seu Chassi
        /// </summary>
        /// <param name="chassi">Chassi do veiculo buscado</param>
        /// <response code="200">Retorna o veiculo filtrado</response>
        /// <response code="204">Caso não haja veiculo com este id</response> 
        [HttpGet("{chassi:string}")]
        public async Task<ActionResult<VeiculoViewModel>> Obter([FromRoute] string chassi)
        {
            var veiculo = await _veiculoService.Value.ObterPorChassi(chassi);

            if (veiculo == null)
                return NoContent();

            return Ok();
        }

        /// <summary>
        /// Inserir um veiculo no catálogo
        /// </summary>
        /// <param name="veiculoInputModel">Dados do veiculo a ser inserido</param>
        /// <response code="200">Caso o veiculo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um veiculo com mesmo chassi cadastrado</response>
        [HttpPost]
        public async Task<ActionResult<VeiculoViewModel>> InserirVeiculo([FromBody] VeiculoInputModel veiculoInputModel)
        {
            try
            {
                var veiculo = await _veiculoService.Value.Inserir(veiculoInputModel);

                return Ok(veiculo);
            }
            catch (VeiculoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um veículo com o mesmo chassi cadastrado!");
            }
            
        }

        /// <summary>
        /// Atualizar um veiculo no catálogo
        /// </summary>
        /// /// <param name="idVeiculo">Id do veiculo a ser atualizado</param>
        /// <param name="veiculoInputModel">Novos dados para atualizar o veiculo indicado</param>
        /// <response code="200">Caso o veiculo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um veiculo com este Id</response>  
        [HttpPut("{idVeiculo:guid}")]
        public async Task<ActionResult> AtualizarVeiculo([FromRoute] Guid idVeiculo, [FromBody] VeiculoInputModel veiculoInputModel)
        {
            try
            {
                await _veiculoService.Value.Atualizar(idVeiculo, veiculoInputModel);
                return Ok();
            }
            catch(VeiculoNaoCadastradoException ex)
            {
                return NotFound("Não existe este veículo");
            }
        }

        /// <summary>
        /// Atualizar o valor de venda de um veiculo
        /// </summary>
        /// /// <param name="idVeiculo">Id do veiculo a ser atualizado</param>
        /// <param name="valorVenda">Novo valor de venda do veiculo</param>
        /// <response code="200">Caso o valor de venda seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um veiculo com este Id</response> 
        [HttpPatch("{idVeiculo:guid}/valorvenda/{valorVenda:decimal}")]
        public async Task<ActionResult> AtualizarVeiculo([FromRoute] Guid idVeiculo, [FromRoute] decimal valorVenda)
        {
            try
            {
                await _veiculoService.Value.Atualizar(idVeiculo, valorVenda);
                return Ok();
            }
            catch(VeiculoNaoCadastradoException ex)
            {
                return NotFound("Não existe este veículo");
            }
        }

        /// <summary>
        /// Excluir um veiculo
        /// </summary>
        /// /// <param name="idVeiculo">Id do veiculo a ser excluído</param>
        /// <response code="200">Caso o seja excluído com sucesso</response>
        /// <response code="404">Caso não exista um veiculo com este Id</response>
        [HttpDelete("{idVeiculo:guid}")]
        public async Task<ActionResult> ApagarVeiculo([FromRoute] Guid idVeiculo)
        {
            try
            {
                await _veiculoService.Value.Remover(idVeiculo);
                return Ok();
            }
            catch(VeiculoNaoCadastradoException ex)
            {
                return NotFound("Não existe este veículo");
            }
        }

        /// <summary>
        /// Vender um veiculo
        /// </summary>
        /// /// <param name="chassi">Chassi do veiculo a ser vendido</param>
        /// <param name="dataVenda">Data da venda do veiculo</param>
        /// <param name="valorVenda">Valor de Venda do veiculo</param>
        /// <response code="200">Caso o veículo seja vendido com sucesso</response>
        /// <response code="404">Caso não exista um veiculo com este chassi</response> 
        [HttpPut("{chassi:string}")]
        public async Task<ActionResult> VenderVeiculo([FromRoute] string chassi, [FromBody] DateTime dataVenda, [FromBody] decimal valorVenda)
        {
            try
            {
                await _veiculoService.Value.Vender(chassi, dataVenda, valorVenda);
                return Ok();
            }
            catch(VeiculoNaoCadastradoException ex)
            {
                return NotFound("Não existe este veículo");
            }
        }
    }
}
