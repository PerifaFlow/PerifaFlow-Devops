using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Interfaces.Repositories;

namespace PerifaFlowReal.Application.UseCases;

public class RealizarEntregaUseCase(IEntregaRepository entregaRepository): IRealizarEntregaUseCase
{
    public async Task RealizarEntrega(EntregaRequest request)
    {
        // 1. Validar request manualmente (já tem [Required], mas isso é do MVC)
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.UserId == Guid.Empty)
            throw new ArgumentException("Usuário inválido.");

        if (request.MissaoId == Guid.Empty)
            throw new ArgumentException("Missão inválida.");
        
        if (request.PortfolioId == Guid.Empty)
            throw new ArgumentException("Portfólio inválido.");
        
        if (string.IsNullOrWhiteSpace(request.ConteudoUrl))
            throw new ArgumentException("Conteúdo da URL é obrigatório.");

        // 2. Criar entidade da Entrega
        var entrega = new Entrega
        (
            request.Tipo,
            request.ConteudoUrl,
            request.UserId,
            request.MissaoId,
            request.PortfolioId
        );

        // 3. Persistir através do repositório genérico
        await entregaRepository.AddAsync(entrega);
    }
}