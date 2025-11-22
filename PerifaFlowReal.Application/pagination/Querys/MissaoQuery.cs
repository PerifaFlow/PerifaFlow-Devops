namespace PerifaFlowReal.Application.pagination;

public class MissaoQuery
{
    public string? Titulo { get; init; }
    
    public DateTime? FromCreatedAtUc { get; init; }
    public bool DescendingByCreatedAt { get; init; } = true;
}