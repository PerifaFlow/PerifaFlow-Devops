using PerifaFlow.Domain.Entities;

namespace PerifaFlowReal.Application.Dtos.Request;

public class TrilhaSummary
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } 
    public string Descricao { get; set; }
    public List<Missao> Missao { get; set; }
}