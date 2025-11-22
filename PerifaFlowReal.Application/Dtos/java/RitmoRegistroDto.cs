namespace PerifaFlowReal.Application.Dtos.java;

public class RitmoRegistroDto
{
    public string Bairro { get; set; } = default!;
    public string Turno { get; set; } = default!; // "MANHA" | "TARDE" | "NOITE"
    public int Energia { get; set; }   // 0..2
    public int Ambiente { get; set; }  // 0..2
    public int Condicao { get; set; }  // 0..2
    public bool OptIn { get; set; }
}