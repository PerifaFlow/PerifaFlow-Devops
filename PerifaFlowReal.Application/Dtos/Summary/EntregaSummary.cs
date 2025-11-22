using PerifaFlow.Domain.Enum;

namespace PerifaFlowReal.Application.Dtos.Request;

public class EntregaSummary
{
    public Guid Id { get; set; }
    public TipoEntrega Tipo { get; set; }
    public string ConteudoUrl { get; set; }
}