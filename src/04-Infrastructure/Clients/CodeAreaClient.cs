using PosTech.Fase3.AddContact.Domain.DTOs;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using System.Text.Json;

namespace PosTech.Fase3.AddContact.Infrastructure.Clients
{
    public class CodeAreaClient : ICodeAreaClient
    {
        private readonly HttpClient _client;

        public CodeAreaClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<RegionDto?> GetRegionByCodeAsync(int code)
        {
            var response = await _client.GetAsync($"areacodes/{code}");
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<RegionDto>(json, options);                        
        }
    }
}
