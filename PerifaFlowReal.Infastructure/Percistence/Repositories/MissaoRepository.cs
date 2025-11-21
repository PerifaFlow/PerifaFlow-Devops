using Microsoft.EntityFrameworkCore;
using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;
using PerifaFlowReal.Infastructure.Percistence.Context;

namespace PerifaFlowReal.Infastructure.Percistence.Repositories;

public class MissaoRepository(PerifaFlowContext context) : Repository<Missao>(context), IMissaoRepository
{
    private readonly PerifaFlowContext _context = context;
    
    public async Task<IEnumerable<Missao>> ListarPorTrilhaAsync(Guid trilhaId)
    {
        return await _context.Missao
            .Where(m => m.TrilhaId == trilhaId)
            .ToListAsync();
    }

    public async Task<PaginatedResult<MissaoSummary>> GetPageAsync(PageRequest page, MissaoQuery? filter = null, CancellationToken ct = default)
    { 
        page.EnsureValid();

        IQueryable<Missao> query = _context.Missao.AsNoTracking();
        if (filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(filter.Titulo))
            {
                var titulo = filter.Titulo.Trim();
                query = query.Where(m => m.Titulo.Contains(titulo));
            }

            if (filter.FromCreatedAtUc is not null)
                query = query.Where(m=> m.CreatedAt >= filter.FromCreatedAtUc);
            query = filter.DescendingByCreatedAt
                ? query.OrderByDescending(m => m.CreatedAt)
                : query.OrderBy(m => m.CreatedAt);
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
        List<MissaoSummary> summaries = [];

        summaries.AddRange(items.Select(item => new MissaoSummary()
        {
            Id = item.Id, Titulo = item.Titulo, Descricao = item.Descricao 
        }));
        
        return new PaginatedResult<MissaoSummary>(summaries, totalCount, page.Page, page.PageSize);
    }
}