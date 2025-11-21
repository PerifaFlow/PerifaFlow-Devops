using Microsoft.EntityFrameworkCore;
using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;
using PerifaFlowReal.Infastructure.Percistence.Context;

namespace PerifaFlowReal.Infastructure.Percistence.Repositories;

public class UserRepository(PerifaFlowContext context) : Repository<User>(context), IUserRepository
{
    private readonly PerifaFlowContext _context = context;

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = _context.Users
            .AsEnumerable()
            .FirstOrDefault(u => u.Email== email);
        
        return user;
    }

    public async Task<PaginatedResult<UserSummary>> GetPageAsync(PageRequest page, UserQuery? filter = null, CancellationToken ct = default)
    {
        page.EnsureValid();

        IQueryable<User> query = _context.Users.AsNoTracking();
        if (filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                var email = filter.Email.Trim();
                query = query.Where(u => u.Email.Contains(email));
            }

            if (filter.FromCreatedAtUc is not null)
                query = query.Where(u => u.CreatedAt >= filter.FromCreatedAtUc);
            query = filter.DescendingByCreatedAt
                ? query.OrderByDescending(u => u.CreatedAt)
                : query.OrderBy(u => u.CreatedAt);
        }
        else
        {
            query.OrderByDescending(u => u.CreatedAt);
        }
        
        var totalCount = await query.CountAsync(cancellationToken: ct);
        
        var items = await query
            .Skip(page.Offset)
            .Take(page.PageSize)
            .ToListAsync(ct);
        List<UserSummary> summaries = [];

        summaries.AddRange(items.Select(item => new UserSummary
        {
            Id = item.Id, Username = item.Username, Email = item.Email, Password = item.Password
        }));
        
        return new PaginatedResult<UserSummary>(summaries, totalCount, page.Page, page.PageSize);
    }
}