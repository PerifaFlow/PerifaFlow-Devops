using Microsoft.EntityFrameworkCore;
using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;
using PerifaFlowReal.Infastructure.Percistence.Context;

namespace PerifaFlowReal.Infastructure.Percistence.Repositories;

public class PortfolioRepository(PerifaFlowContext context) : Repository<Portfolio>(context), IPortfolioRepository
{
    private readonly PerifaFlowContext _context = context;
    
    public async Task<IEnumerable<Portfolio>> ObterPorUsuarioAsync(Guid usuarioId)
    {
        return await _context.Portfolio
            .Where(p => p.UserID == usuarioId)
            .ToListAsync();
    }

    public async Task<PaginatedResult<PortfolioSummary>> GetPageAsync(PageRequest page, PortfolioQuery? filter = null, CancellationToken ct = default)
    {
        page.EnsureValid();

        IQueryable<Portfolio> query = _context.Portfolio.AsNoTracking();
        if (filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(filter.Titulo))
            {
                var titulo = filter.Titulo.Trim();
                query = query.Where(p => p.Titulo.Contains(titulo));
            }

            if (filter.FromCreatedAtUc is not null)
                query = query.Where(p => p.CreatedAt >= filter.FromCreatedAtUc);
            query = filter.DescendingByCreatedAt
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt);
        }
        else
        {
            query.OrderByDescending(p => p.CreatedAt);
        }
        
        var totalCount = await query.CountAsync(cancellationToken: ct);
        
        var items = await query
            .Skip(page.Offset)
            .Take(page.PageSize)
            .ToListAsync(ct);
        List<PortfolioSummary> summaries = [];

        summaries.AddRange(items.Select(item => new PortfolioSummary
        {
            Id = item.Id, Titulo = item.Titulo, Url = item.Url
        }));
        
        return new PaginatedResult<PortfolioSummary>(summaries, totalCount, page.Page, page.PageSize);
    }
}