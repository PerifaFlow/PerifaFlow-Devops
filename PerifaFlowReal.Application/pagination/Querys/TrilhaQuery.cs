namespace PerifaFlowReal.Application.pagination;

public class TrilhaQuery
{
    public string? Titulo { get; init; }
    
    public DateTime? FromCreatedAtUc { get; init; }
    public bool DescendingByCreatedAt { get; init; } = true;
}