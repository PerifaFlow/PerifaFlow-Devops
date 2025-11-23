using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Dtos.Response;
using PerifaFlowReal.Application.pagination;

namespace PerifaFlowReal.Application.UseCases.PortFolio;

public interface ICreatePortfolioUseCase
{
    Task<PortfolioResponse>  CreatePortfolio(PortfolioRequest request);
    
    Task<PaginatedResult<PortfolioSummary>> GetPageAsync(
        PageRequest page, 
        PortfolioQuery? filter = null, 
        CancellationToken ct = default
    );
}