namespace PerifaFlowReal.Application.pagination;

public class PaginatedResult<T>
{
    public IReadOnlyList<T> Items { get; }
    public int TotalItems { get; } //Total de mensagens = 100
    public int Page { get; } //Pagina atual
    public int PageSize { get; } // tamanho da pagina = 40
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize); //40, 40, 20
    
    public bool HasPrevious => Page > 1;
    
    public bool HasNext => Page < TotalPages;
    
    public PaginatedResult(IReadOnlyList<T> items, int totalItems, int page, int pageSize)
        => (Items, TotalItems, Page, PageSize) = (items, totalItems, page, pageSize);
}