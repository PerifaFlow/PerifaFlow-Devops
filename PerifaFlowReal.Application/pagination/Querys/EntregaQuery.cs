using PerifaFlow.Domain.Enum;

namespace PerifaFlowReal.Application.pagination;

public class EntregaQuery
{
    public string? Tipo { get; init; }
    
    public DateTime? FromCreatedAtUc { get; init; }
    public bool DescendingByCreatedAt { get; init; } = true;
}