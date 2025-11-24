using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Dtos.Response;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;

namespace PerifaFlowReal.Application.UseCases.CreateUserUseCase;

public class CreateUserUseCase(IUserRepository userRepository) : ICreateUserUseCase
{
    public async Task<UserResponse> Execute(UserRequest request)
    {
        var existUser = userRepository.GetByEmailAsync(request.Email);
        var user = new User(request.Username, request.Email, request.Password);
        
        //if(existUser != null)
          //  throw new Exception("User with this email already exists");

        await userRepository.AddAsync(request.ToDomain());
        
        return new UserResponse(
            user.Id,
            user.Username,
            user.Email,
            user.Password
        );
    }

    public Task<PaginatedResult<UserSummary>> ExecuteAsync(PageRequest page, UserQuery? filter = null, CancellationToken ct = default)
    {
        return userRepository.GetPageAsync(page, filter, ct);
    }
}