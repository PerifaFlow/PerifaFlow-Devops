namespace PerifaFlow.Domain.Entities;

public class User : Audit
{
    public Guid Id  { get; private set; }
    public string Username { get; private set; }
    
    public string Email { get; private set; }
    
    public string Password { get; private set; }
    
    public List<Entrega> Entrega { get; private set; }
    public Portfolio? Portfolio { get; private set; }

    public User()
    {
        
    }
    
    public User( string username,  string email, string password)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
        Password = password;
        Entrega = new List<Entrega>();
        
        SetCreated(CreatedBy);
    }
    
    public void Update(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
        
        SetUpdated(UpdatedBy);
    }
}