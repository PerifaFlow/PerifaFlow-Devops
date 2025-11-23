using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Application.Dtos.Request;
using PerifaFlowReal.Application.Dtos.Response;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.pagination;

namespace PerifaFlowReal.Application.UseCases;

public class TrilhaUseCase(ITrilhaRepository  trilhaRepository): ITrilhaUseCase
{
    public async Task<TrilhaResponse> CriarAsync(TrilhaRequest request)
    {
        // 1. Validar request (Required do MVC não funciona aqui)
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Titulo))
            throw new ArgumentException("Título é necessário.");

        if (string.IsNullOrWhiteSpace(request.Descricao))
            throw new ArgumentException("Descrição é necessária.");

        // 2. Criar entidade
        var trilha = new Trilha
        (
            request.Titulo,
            request.Descricao
        );

        // 3. Persistir
        await trilhaRepository.AddAsync(trilha);

        // 4. Mapear para response
        return new TrilhaResponse
        {
            Id = trilha.Id,
            Titulo = trilha.Titulo,
            Descricao = trilha.Descricao,
            Missao = new List<Missao>() // lista vazia inicialmente
        };
    }

    public Task<PaginatedResult<TrilhaSummary>> GetPageAsync(PageRequest page, TrilhaQuery? filter = null, CancellationToken ct = default)
    {
        return trilhaRepository.GetPageAsync(page, filter, ct);
    }
}