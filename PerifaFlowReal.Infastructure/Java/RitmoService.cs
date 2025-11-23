using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using PerifaFlowReal.Application.Dtos.java;
using PerifaFlowReal.Application.Interfaces.Services;

namespace PerifaFlowReal.Infastructure.Java;

public class RitmoService : IRitimoService
{
    private readonly HttpClient _httpClient;

    public RitmoService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(config["BemEstar:BaseUrl"]!);
    }

    // ----------------------------
    // REGISTRAR
    // ----------------------------
    public async Task RegistrarAsync(
        RitmoRegistroDto request,
        string? bearerToken,
        CancellationToken ct = default)
    {
        AddBearer(bearerToken);

        var response = await _httpClient.PostAsJsonAsync("/ritmo/registrar", request, ct);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken: ct);
            throw new Exception($"Erro ao chamar API Java (Status {response.StatusCode}): {content}");
        }
    }

    // ----------------------------
    // OBTER INSIGHTS
    // ----------------------------
    public async Task<IEnumerable<InsightDto>> ObterInsightsAsync(
        string bairro,
        DateTime de,
        DateTime ate,
        string? bearerToken,
        CancellationToken ct = default)
    {
        AddBearer(bearerToken);

        string url = $"/insights?bairro={bairro}&de={de:yyyy-MM-dd}&ate={ate:yyyy-MM-dd}";

        var response = await _httpClient.GetAsync(url, ct);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<InsightDto>>(cancellationToken: ct)
               ?? Enumerable.Empty<InsightDto>();
    }

    // ----------------------------
    // SUGERIR MISSÃO
    // ----------------------------
    public async Task<SugestaoMissaoResponse> SugerirMissaoAsync(
        SugestaoMissaoRequest request,
        string? bearerToken,
        CancellationToken ct = default)
    {
        AddBearer(bearerToken);

        var response = await _httpClient.PostAsJsonAsync("/missoes/sugerir", request, ct);

        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<SugestaoMissaoResponse>(cancellationToken: ct);

        return data!;
    }
    
    private void AddBearer(string? token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrWhiteSpace(token))
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
    }
}