using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Dtos.Response;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;
using PerifaFlowReal.Application.UseCases.CreateMissaoUseCase;
using PerifaFlowReal.Infastructure.Percistence.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace PerifaFlowReal.api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [SwaggerTag("Gerenciamento de Login")]
    [AllowAnonymous]
    [ApiController]
    public class MissaoController : ControllerBase
    {
        private readonly ICreateMissaoUseCase  _createMissaoUseCase;
        private readonly IMissaoRepository _missaoRepository;
        private readonly IUpdateMissaoUseCase _updateMissaoUseCase;

        public MissaoController(ICreateMissaoUseCase createMissaoUseCase, IUpdateMissaoUseCase updateMissaoUseCase ,IMissaoRepository missaoRepository)
        {
            _createMissaoUseCase = createMissaoUseCase;
            _missaoRepository = missaoRepository;
            _updateMissaoUseCase = updateMissaoUseCase;
        }

        // GET: api/Missao
        [HttpGet("paged")]
        public Task<PaginatedResult<MissaoSummary>> GetPage([FromQuery] PageRequest request,
            [FromQuery] MissaoQuery missaoQuery)
        {
            return _createMissaoUseCase.GetPageAsync(request, missaoQuery);
        }

        [HttpGet("listbyTrilha")]
        public async Task<ActionResult<MissaoResponse>> ListarPorTrilhaAsync(Guid trilhaId)
        {
            var missao = await _missaoRepository.ListarPorTrilhaAsync(trilhaId);
            if (!missao.Any()) return NotFound($"Nenhuma missão encontrada encontrada par essa trilha '{trilhaId}'");
            return Ok(missao.Select(m =>new MissaoResponse(m.Id, m.Titulo, m.Descricao, m.TrilhaId)));
        }
        
        // GET: api/Missao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MissaoResponse>> GetMissao(Guid id)
        {
            var missao = await _missaoRepository.GetByIdAsync(id);

            if (missao == null)
            {
                return NotFound();
            }

            return new MissaoResponse(missao.Id, missao.Titulo, missao.Descricao, missao.TrilhaId);
        }

        // PUT: api/Missao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMissao(Guid id, MissaoRequest missaoRequest)
        {
            try
            {
                var updated = await _updateMissaoUseCase.Execute(id, missaoRequest);
                
                if(updated == null)
                    return NotFound("missao not found");

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro ao atualizar o pátio." });
            }
        }

        // POST: api/Missao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MissaoResponse>> PostMissao(MissaoRequest missaoRequest)
        {
            try
            {
                var missaoResponse = await _createMissaoUseCase.Execute(missaoRequest);
                return CreatedAtAction(nameof(GetMissao), new { id = missaoResponse.Id }, missaoResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        // DELETE: api/Missao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMissao(Guid id)
        {
            var missao = await _missaoRepository.GetByIdAsync(id);
            if (missao == null) return NotFound("Missao not found");
            
            await _missaoRepository.DeleteAsync(missao);
            return NoContent();
        }

        
    }
}
