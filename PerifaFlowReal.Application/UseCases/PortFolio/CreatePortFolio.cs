using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Dtos.Response;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;

namespace PerifaFlowReal.Application.UseCases.PortFolio;

public class CreatePortFolio(IPortfolioRepository portfolioRepository): ICreatePortfolioUseCase
{
    public async Task<PortfolioResponse> CreatePortfolio(PortfolioRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Titulo))
            throw new ArgumentException("Título é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Url))
            throw new ArgumentException("URL é obrigatória.");

        if (request.UserId == Guid.Empty)
            throw new ArgumentException("UserId inválido.");

        // 2. Criar entidade
        var portfolio = new Portfolio
        (
            request.Titulo,
            request.Url, 
            request.UserId
        );

        // 3. Persistir
        await portfolioRepository.AddAsync(portfolio);

        // 4. Mapear para response
        return new PortfolioResponse
        {
            Id = portfolio.Id,
            Titulo = portfolio.Titulo,
            Url = portfolio.Url,
            UserId = portfolio.UserID,
            Entregas =  new List<Entrega>() 
                        
            
        };
    }

    public Task<PaginatedResult<PortfolioSummary>> GetPageAsync(PageRequest page, PortfolioQuery? filter = null, CancellationToken ct = default)
    {
        return portfolioRepository.GetPageAsync(page, filter, ct);
    }
}