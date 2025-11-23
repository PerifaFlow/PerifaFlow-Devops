using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Dtos.Response;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;

namespace PerifaFlowReal.Application.UseCases.CreateUserUseCase;

public class CreateUserUseCase(IUserRepository userRepository) : ICreateUserUseCase
{
    public async Task Execute(UserRequest request)
    {
        var existUser = userRepository.GetByEmailAsync(request.Email);
        
       // if(existUser != null)
         //   throw new Exception("User with this email already exists");

        await userRepository.AddAsync(request.ToDomain());
    }

  
}