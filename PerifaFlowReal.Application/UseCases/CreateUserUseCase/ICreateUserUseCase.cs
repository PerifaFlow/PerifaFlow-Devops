using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Dtos.Response;
using PerifaFlowReal.Application.pagination;

namespace PerifaFlowReal.Application.UseCases.CreateUserUseCase;

public interface ICreateUserUseCase
{
    Task<UserResponse> Execute(UserRequest request);
    
    Task<PaginatedResult<UserSummary>> ExecuteAsync(
        PageRequest page,
        UserQuery? filter = null,
        CancellationToken ct = default
    );
}