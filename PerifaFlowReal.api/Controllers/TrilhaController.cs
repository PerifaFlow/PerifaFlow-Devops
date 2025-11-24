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
using PerifaFlowReal.Application.UseCases;
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
    public class TrilhaController : ControllerBase
    {
        private readonly ITrilhaRepository _trilhaRepository;
        private readonly ITrilhaUseCase _trilhaUseCase;
        private readonly IUpdateTrilhaUseCase _updateTrilhaUseCase;

        public TrilhaController(ITrilhaUseCase trilhaUseCase, ITrilhaRepository trilhaRepository, IUpdateTrilhaUseCase updateTrilhaUseCase)
        {
            _trilhaUseCase = trilhaUseCase;
            _trilhaRepository = trilhaRepository;
            _updateTrilhaUseCase = updateTrilhaUseCase;
        }
        
        
        [HttpGet("paged")]
        public Task<PaginatedResult<TrilhaSummary>> GetPage([FromQuery] PageRequest request,
            [FromQuery] TrilhaQuery trilhaQuery)
        {
            return _trilhaUseCase.GetPageAsync(request, trilhaQuery);
        }

        // GET: api/Trilha/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrilhaResponse>> GetTrilha(Guid id)
        {
            var trilhas = await _trilhaRepository.GetByIdAsync(id);
            if (trilhas == null) return NotFound();

            return new TrilhaResponse(trilhas.Id, trilhas.Titulo, trilhas.Descricao, trilhas.Missao);
        }

        // PUT: api/Trilha/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrilha(Guid id, TrilhaRequest trilhaRequest)
        {
            try
            {
                var updated = await _updateTrilhaUseCase.Execute(id, trilhaRequest);
                
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

        // POST: api/Trilha
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrilhaResponse>> PostTrilha(TrilhaRequest trilhaRequest)
        {
            try
            {
                var trilhaResponse = await _trilhaUseCase.CriarAsync(trilhaRequest);
                return CreatedAtAction(nameof(GetTrilha), new { id = trilhaResponse.Id }, trilhaResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro ao criar trilha" });
            }
        }

        // DELETE: api/Trilha/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrilha(Guid id)
        {
            var trilha = await _trilhaRepository.GetByIdAsync(id);
            if (trilha == null) return NotFound("Pátio não encontrado.");

            await _trilhaRepository.DeleteAsync(trilha);
            return NoContent();
        }
        
    }
}
