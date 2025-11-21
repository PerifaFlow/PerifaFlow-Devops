using Microsoft.EntityFrameworkCore;
using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;
using PerifaFlowReal.Infastructure.Percistence.Context;

namespace PerifaFlowReal.Infastructure.Percistence.Repositories;

public class EntregaRepository(PerifaFlowContext context) : Repository<Entrega>(context), IEntregaRepository
{
    private readonly PerifaFlowContext _context = context;
    
    public async Task<PaginatedResult<EntregaSummary>> GetPageAsync(PageRequest page, EntregaQuery? filter = null, CancellationToken ct = default)
    {
        page.EnsureValid();

        IQueryable<Entrega> query = _context.Entregas.AsNoTracking();
        if (filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(filter.Tipo))
            {
                var tipo = filter.Tipo.Trim();
                query = query.Where(e => e.Tipo.Equals(tipo));
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
        List<EntregaSummary> summaries = [];

        summaries.AddRange(items.Select(item => new EntregaSummary()
        {
            Id = item.Id, Tipo =  item.Tipo, ConteudoUrl = item.ConteudoUrl
        }));
        
        return new PaginatedResult<EntregaSummary>(summaries, totalCount, page.Page, page.PageSize);
    }
}