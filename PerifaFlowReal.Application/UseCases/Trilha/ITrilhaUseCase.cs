using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Dtos.Response;
using PerifaFlowReal.Application.pagination;

namespace PerifaFlowReal.Application.UseCases;

public interface ITrilhaUseCase
{
    Task<TrilhaResponse> CriarAsync(TrilhaRequest request);
    
    Task<PaginatedResult<TrilhaSummary>> GetPageAsync(
        PageRequest page, 
        TrilhaQuery? filter = null, 
        CancellationToken ct = default
    );
}