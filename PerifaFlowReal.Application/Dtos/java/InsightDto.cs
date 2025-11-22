namespace PerifaFlowReal.Application.Dtos.java;

public class InsightDto
{
    public string Bairro { get; set; } = default!;
        public PeriodoDto Periodo { get; set; } = default!;
        public int Amostras { get; set; }
        public BarreiraDto Barreiras { get; set; } = default!;
}