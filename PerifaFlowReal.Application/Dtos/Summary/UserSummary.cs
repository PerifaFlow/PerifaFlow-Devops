namespace PerifaFlowReal.Application.Dtos.Request;

public class UserSummary
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}