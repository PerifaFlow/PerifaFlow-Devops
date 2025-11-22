namespace PerifaFlowReal.Application.Dtos.java;

public class SugestaoMissaoResponse
{
    public string MissaoId { get; set; } = default!;
    public string Complexidade { get; set; } = default!; // "CURTA" | "NORMAL"
    public bool Offline { get; set; }
    public string Mensagem { get; set; } = default!;
}