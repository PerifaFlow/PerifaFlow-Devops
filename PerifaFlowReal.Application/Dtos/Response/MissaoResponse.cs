namespace PerifaFlowReal.Application.Dtos.Response;

public class MissaoResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Guid TrilhaId { get; set; }

    public MissaoResponse(Guid id, string titulo, string descricao, Guid trilhaId)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        TrilhaId = trilhaId;
        
    }
}