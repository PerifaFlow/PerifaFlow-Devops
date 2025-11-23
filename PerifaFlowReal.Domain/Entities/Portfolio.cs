namespace PerifaFlow.Domain.Entities;

public class Portfolio : Audit
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Url {get; private set;}
    
    public Guid UserID { get; private set; }
    public User User { get; private set; }
    
    public List<Entrega> Entrega { get; private set; }
    
    
    public Portfolio (string titulo, string url,  Guid userID)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Url = url;
        UserID = userID;
        Entrega =  new List<Entrega>();
        
        SetCreated(CreatedBy);
    }

    public void Update(string titulo, string url)
    {
        Titulo = titulo;
        Url = url;
        
        SetUpdated(UpdatedBy);
    }
}