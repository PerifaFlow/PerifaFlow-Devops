using PerifaFlow.Domain.Entities;

namespace PerifaFlowReal.Application.Dtos.Response;

public class TrilhaResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public List<Missao> Missao { get; set; } =  new ();
    public List<LinkResponse> Links { get; set; }

    public TrilhaResponse(Guid id, string titulo, string descricao, List<Missao> trilhasMissao)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        Missao = trilhasMissao;
    }

    public TrilhaResponse()
    {
        
    }
}