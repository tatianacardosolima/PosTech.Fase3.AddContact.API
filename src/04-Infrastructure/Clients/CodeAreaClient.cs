using PosTech.Fase3.AddContact.Domain.DTOs;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            return await JsonSerializer.DeserializeAsync<RegionDto>(await response.Content.ReadAsStreamAsync());
        }
    }
}
