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
using PerifaFlowReal.Application.UseCases.CreateUserUseCase;
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
    public class UserController(ICreateUserUseCase createUserUseCase,IUpdateUserUseCase updateUserUseCase, IUserRepository userRepository)
        : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICreateUserUseCase  _createUserUseCase = createUserUseCase;

        // GET: api/User
        [HttpGet("paged")]
        public Task<PaginatedResult<UserSummary>> GetPage([FromQuery] PageRequest pageRequest,
            [FromQuery] UserQuery userQuery)
        {
            return createUserUseCase.ExecuteAsync(pageRequest, userQuery);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if(user == null) return NotFound("Usuario não encontrado");
            return new UserResponse(
                user.Id,
                user.Username,
                user.Email,
                user.Password
            );
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserRequest request)
        {
            try
            {
                string updatedBy = User?.Identity?.Name ?? "system";
                var updated = await updateUserUseCase.Execute(id, request);

                if (!updated) return NotFound("Usuário não encontrado");

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro ao atualizar Usuario" });
            }
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRequest request)
        {
            try
            {
                var userResponse = await createUserUseCase.Execute(request);
                return CreatedAtAction(nameof(GetUser), new { id = userResponse.Id }, userResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro a cadastrar Usuario" });
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null) return NotFound("Usuário não encontrado");
            
            await userRepository.DeleteAsync(user);
            return NoContent();
        }
    }
}
