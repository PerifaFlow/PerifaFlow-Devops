using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Dtos.Response;
using PerifaFlowReal.Application.Interfaces.Services.JWT;
using PerifaFlowReal.Application.UseCases.CreateUserUseCase;
using PerifaFlowReal.Application.UseCases.Login;
using Swashbuckle.AspNetCore.Annotations;
using static System.String;

namespace PerifaFlowReal.api.Controllers
{
    
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [SwaggerTag("Gerenciamento de Login")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICreateUserUseCase  _createUserUseCase;
        private readonly ILoginUseCase _loginUseCase;
        private readonly ITokenService _tokenService;

        public AuthController(
            ICreateUserUseCase createUserUseCase,
            ILoginUseCase loginUseCase,
            ITokenService tokenService)
        {
            _createUserUseCase = createUserUseCase;
            _loginUseCase = loginUseCase;
            _tokenService = tokenService;
        }
        
        [HttpPost("register")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Create new Employee", Description = "Creates a new Employee")]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRequest request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _createUserUseCase.Execute(request);
                return StatusCode((int)HttpStatusCode.Created); //201
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message }); //400
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); //500
            }
        }
        
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login Service", Description = "Get Token")]
        [SwaggerResponse(StatusCodes.Status200OK, "Token returned")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]

        public async Task<LoginResponse> Login([FromBody] LoginRequest loginDto)
        {
            return await _loginUseCase.Login(loginDto);
        }
    }
}
