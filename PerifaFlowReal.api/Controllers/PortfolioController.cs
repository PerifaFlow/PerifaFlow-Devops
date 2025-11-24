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
using PerifaFlowReal.Application.UseCases.PortFolio;
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
    public class PortfolioController : ControllerBase
    {
        private readonly ICreatePortfolioUseCase  _createPortfolioUseCase;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUpdatePortfolioUseCase _updatePortfolioUseCase;
        

        public PortfolioController(ICreatePortfolioUseCase createPortfolioUseCase, IPortfolioRepository portfolioRepository, IUpdatePortfolioUseCase updatePortfolioUseCase)
        {
            _createPortfolioUseCase = createPortfolioUseCase;
            _portfolioRepository = portfolioRepository;
            _updatePortfolioUseCase = updatePortfolioUseCase; 
        }

        // GET: api/Portfolio
        [HttpGet("paged")]
        public Task<PaginatedResult<PortfolioSummary>> GetPage([FromQuery] PageRequest request,
            [FromQuery] PortfolioQuery portfolioQuery)
        {
            return _createPortfolioUseCase.GetPageAsync(request, portfolioQuery);
        }

        // GET: api/Portfolio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioResponse>> GetPortfolio(Guid id)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            if (portfolio == null) return NotFound();

            return new PortfolioResponse(portfolio.Id, portfolio.UserID, portfolio.Titulo, portfolio.Url);
        }

        [HttpGet("getbyuser")]
        public async Task<ActionResult<PortfolioResponse>> ObterPorUsuario(Guid usuarioId)
        {
            var portfolio = await _portfolioRepository.ObterPorUsuarioAsync(usuarioId);
            if (!portfolio.Any()) return NotFound($"Nenhuma missão encontrada encontrada par essa trilha '{usuarioId}'");
            return Ok(portfolio.Select(p =>new PortfolioResponse(p.Id, p.UserID,p.Titulo,p.Url)));
        }
        
        // PUT: api/Portfolio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortfolio(Guid id, PortfolioRequest portfolioRequest)
        {
            try
            {
                var update = await _updatePortfolioUseCase.Execute(id, portfolioRequest);
                
                if  (update == null) return NotFound();
                
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

        // POST: api/Portfolio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PortfolioResponse>> PostPortfolio(PortfolioRequest portfolioRequest)
        {
            try
            {
                var portfolioResponse = await _createPortfolioUseCase.CreatePortfolio(portfolioRequest);
                return CreatedAtAction(nameof(GetPortfolio), new { id = portfolioResponse.Id }, portfolioResponse);
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
                return StatusCode(500, new { message = "Erro ao criar Portfolio" });
            }
        }

        // DELETE: api/Portfolio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolio(Guid id)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            if (portfolio == null) return NotFound("Portfolio not found");
            
            await _portfolioRepository.DeleteAsync(portfolio);

            return NoContent();
        }

       
    }
}
