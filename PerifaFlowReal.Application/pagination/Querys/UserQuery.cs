namespace PerifaFlowReal.Application.pagination;

public class UserQuery
{
    public string? Email { get; init; }
    
    public DateTime? FromCreatedAtUc { get; init; }
    public bool DescendingByCreatedAt { get; init; } = true;
}