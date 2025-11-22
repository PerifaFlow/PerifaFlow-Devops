namespace PerifaFlowReal.Application.pagination;

public class PortfolioQuery
{
    public string? Titulo { get; init; }
    
    public DateTime? FromCreatedAtUc { get; init; }
    public bool DescendingByCreatedAt { get; init; } = true;
}