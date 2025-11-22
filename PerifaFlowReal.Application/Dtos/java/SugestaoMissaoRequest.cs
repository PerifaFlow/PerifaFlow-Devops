namespace PerifaFlowReal.Application.Dtos.java;

public class SugestaoMissaoRequest
{
    public string Perfil { get; set; } = default!;
    public int? UltimaEnergia { get; set; }
    public int? UltimoAmbiente { get; set; }
    public int? UltimaCondicao { get; set; }
}