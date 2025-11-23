using PerifaFlow.Domain.Enum;

namespace PerifaFlow.Domain.Entities;

public class Entrega : Audit
{
    public Guid Id { get; private set; }
    public TipoEntrega Tipo { get; private set; }
    public string ConteudoUrl {get; private set;}
    
    public Guid PortfolioId { get; private set; }
    public Portfolio Portfolio { get; private set; }
    
    public Guid UserID { get; private set; }
    public User User { get; private set; }
    public Guid MissaoID { get; private set; }
    public Missao Missao { get; private set; }
    

    public Entrega()
    {
        
    }

    public Entrega(TipoEntrega tipo, string conteudoUrl, Guid userID, Guid missaoID, Guid portfolioID)
    {
        Id = Guid.NewGuid();
        Tipo = tipo;
        ConteudoUrl = conteudoUrl;
        UserID = userID;
        MissaoID = missaoID;
        PortfolioId = portfolioID;
        
        SetCreated(CreatedBy);
    }
}