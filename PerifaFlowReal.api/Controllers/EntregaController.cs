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
using PerifaFlowReal.Application.Interfaces.Repositories;
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
    public class EntregaController(IEntregaRepository entregaRepository, IRealizarEntregaUseCase realizarEntregaUseCase)
        : ControllerBase
    {
       
        


        // POST: api/Entrega
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entrega>> PostEntrega(EntregaRequest entregaRequest)
        {
            try
            {
                await realizarEntregaUseCase.RealizarEntrega(entregaRequest);

                return Ok(new
                {
                    message = "Entrega realizada com sucesso!"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Erro interno ao processar a entrega.",
                    details = ex.InnerException?.Message ?? ex.Message
                });
            }
            
           /* catch (Exception ex)
            {
                // Caso seja necessário logar
                // _logger.LogError(ex, "Erro ao realizar entrega");

                return StatusCode(500, new
                {
                    error = "Erro interno ao processar a entrega.",
                    details = ex.Message
                });
            }*/
        }
        
    }
}
