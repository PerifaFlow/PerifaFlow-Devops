namespace PerifaFlowReal.Application.Configs;

public class Settings
{
    public ConnectionSettings ConnectionStrings { get; set; }
    public ServiceConnection BemEstar { get; set; }
    
    public SwaggerSettings Swagger { get; set; }
    public SwaggerSettings SwaggerV2 { get; set; }
    
    public JWTSettings Jwt { get; set; }
}