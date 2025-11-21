using Microsoft.EntityFrameworkCore;
using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;
using PerifaFlowReal.Infastructure.Percistence.Context;

namespace PerifaFlowReal.Infastructure.Percistence.Repositories;

public class TrilhaRepository(PerifaFlowContext context) : Repository<Trilha>(context), ITrilhaRepository
{
    private readonly PerifaFlowContext _context = context;
    
    public async Task<PaginatedResult<TrilhaSummary>> GetPageAsync(PageRequest page, TrilhaQuery? filter = null, CancellationToken ct = default)
    {
        page.EnsureValid();

        IQueryable<Trilha> query = _context.Trilhas.AsNoTracking();
        if (filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(filter.Titulo))
            {
                var titulo = filter.Titulo.Trim();
                query = query.Where(u => u.Titulo.Contains(titulo));
            }

            if (filter.FromCreatedAtUc is not null)
                query = query.Where(t=> t.CreatedAt >= filter.FromCreatedAtUc);
            query = filter.DescendingByCreatedAt
                ? query.OrderByDescending(t => t.CreatedAt)
                : query.OrderBy(t => t.CreatedAt);
        }
        else
        {
            query.OrderByDescending(t => t.CreatedAt);
        }
        
        var totalCount = await query.CountAsync(cancellationToken: ct);
        
        var items = await query
            .Skip(page.Offset)
            .Take(page.PageSize)
            .ToListAsync(ct);
        List<TrilhaSummary> summaries = [];

        summaries.AddRange(items.Select(item => new TrilhaSummary()
        {
            Id = item.Id, Titulo = item.Titulo, Descricao = item.Descricao, Missao = item.Missao
        }));
        
        return new PaginatedResult<TrilhaSummary>(summaries, totalCount, page.Page, page.PageSize);
    }
}