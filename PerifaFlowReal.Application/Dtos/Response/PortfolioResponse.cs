using PerifaFlow.Domain.Entities;

namespace PerifaFlowReal.Application.Dtos.Response;

public class PortfolioResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Titulo { get; set; }
    public string Url { get; set; }
    public List<Entrega> Entregas { get; set; }
    
    public PortfolioResponse(Guid id, Guid userId, string titulo, string url)
        {
        Id = id;
        UserId = userId;
        Titulo = titulo;
        Url = url;
        Entregas = new List<Entrega>();
        }

    public PortfolioResponse()
    {
    }
}